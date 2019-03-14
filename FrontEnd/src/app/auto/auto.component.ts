import { Component, OnInit, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { forEach } from '@angular/router/src/utils/collection';
import 'rxjs/add/operator/map'
import 'rxjs/add/operator/catch';
import { DatePipe } from '@angular/common'

@Component({
  selector: 'app-auto',
  templateUrl: './auto.component.html',
  styleUrls: ['./auto.component.css']
})
export class AutoComponent implements OnInit {
  searchOptions: Array<Object> = [
    { num: 0, name: "Message ends with..."},
    { num: 1, name: "Message between two users with certain type"},
    { num: 2, name: "Message after certain timestamp"},
    { num: 3, name: "All messages"}
  ];

  messageTypes: Array<Object> = [
    { num: 0, name: "News" },
    { num: 1, name: "Question" },
    { num: 2, name: "Answer" },
    { num: 3, name: "Invite" },
    { num: 4, name: "Comment" }
  ];

  messages: Array<Message> = [];

  fromUserForm:number;
  toUserForm:number;
  spamScoreForm:number;
  contentForm:string;
  timeForm:string;
  typeForm: MessageTypeEnum;

  delay(ms: number) {
    return new Promise( resolve => setTimeout(resolve, ms) );
  }

  addNMessages(n:number){
    this.http.get<Message>('api/messages/add/random').subscribe(message => {
      this.messages.splice(0, 0, message);
      if(n == 1){
        alert("Added automatically 10 messages.");
        this.deleteNMessages(3);
        return;
      }
      this.addNMessages(n - 1);
    },
    err =>  {}//alert(err.error)
    );
  }

  deleteNMessages(n:number){
    this.http.delete<Message>('api/messages/' + this.messages[0].id).subscribe(response => {
      this.messages.splice(0, 1);
      if(n <= 1){
        alert("Deleted automatically 3 messages.");
        return;
      }
      this.deleteNMessages(n - 1);
    },
    err =>  {}//alert(err.error)
    );
  }

  start() {
    this.addNMessages(10);
  }

  getAllMessages(){
    this.http.get<Array<Message>>('api/messages').subscribe(data => {
      this.messages = data;
    });
  }

  timeStamp: string;

  getMessagesAfterTimeStampClick(){
    this.timeStamp = (<HTMLInputElement>document.getElementById("timeStampInput")).value;
    let headers = new HttpHeaders();
    headers = headers.append('timeStamp', this.timeStamp.toString());
    this.http.get<Array<Message>>('api/messages/after/', {headers: headers}).subscribe(data => {
      this.messages = data;
    });
  }

  userFrom:string;
  userTo:string;
  messageType:number;

  getMessagesWithCertaingTypeClick(){
    this.userFrom = (<HTMLInputElement>document.getElementById("firstUserInput")).value;
    this.userTo = (<HTMLInputElement>document.getElementById("secondUserInput")).value;
    let headers = new HttpHeaders();
    headers = headers.append('fromUserID', this.userFrom);
    headers = headers.append('toUserID', this.userTo);
    headers = headers.append('messageType', this.messageType.toString());
    this.http.get<Array<Message>>('api/messages/find/', {headers: headers}).subscribe(data => {
      this.messages = data;
    });
  }

  pattern:string;

  getMessagesEndWithClick(){
    this.pattern = (<HTMLInputElement>document.getElementById("patternInput")).value;

    let headers = new HttpHeaders();
    headers = headers.append('pattern', this.pattern);
    this.http.get<Array<Message>>('api/messages/ends/', {headers: headers}).subscribe(data => {
      this.messages = data;
    });
  }

  constructor(private http: HttpClient, public datepipe: DatePipe) {
  }

  ngOnInit() {
    this.getAllMessages();
  }

}

export class Message {
  public id: number;
  public timeStamp: Date;
  public messageType: MessageTypeEnum;
  public senderID: number;
  public receiverID: number;
  public content: string;
  public spamScore: number;
  constructor() {
  }
}

enum MessageTypeEnum { News, Question, Answer, Invite, Comment };