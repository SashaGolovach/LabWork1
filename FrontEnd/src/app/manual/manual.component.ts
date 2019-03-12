import { Component, OnInit, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { forEach } from '@angular/router/src/utils/collection';
import 'rxjs/add/operator/map'
import 'rxjs/add/operator/catch';
import { stringify } from '@angular/core/src/util';
import { PatternValidator } from '@angular/forms';

@Component({
  selector: 'app-manual',
  templateUrl: './manual.component.html',
  styleUrls: ['./manual.component.css']
})

export class ManualComponent implements OnInit {

  searchType: number = 3;
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
  editField: string;

  updateList(id: number, property: string, event: any) {
    const editField = event.target.textContent;
    let data = this.messages[id][property];
    this.messages[id][property] = editField;
    this.http.post('api/messages/edit/', this.messages[id]).subscribe(response => {
    },
      err => {
        this.messages[id][property] = data;
        alert("Could not edit message, please check the input data is correct.");
      });
  }

  changeValue(id: number, property: string, event: any) {
    this.editField = event.target.textContent;
  }

  remove(id: any) {
    this.http.get('api/messages/delete/' + this.messages[id].id).map((response: Response) => {
      this.messages.splice(id, 1);
    }).subscribe(
      data => {},
      err => {}//alert(err.error)
      );
  }

  add() {
    this.http.get<Message>('api/messages/add/').subscribe(message => {
        this.messages.splice(0, 0, message);
     },
     err =>  {}//alert(err.error)
     );
  }

  selectOnChange(){
    if(this.searchType == 3)
      this.getAllMessages();
    else
      this.messages = [];
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

  constructor(private http: HttpClient) {

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