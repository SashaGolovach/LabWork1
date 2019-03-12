import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { forEach } from '@angular/router/src/utils/collection';

@Component({
  selector: 'app-manual',
  templateUrl: './manual.component.html',
  styleUrls: ['./manual.component.css']
})
export class ManualComponent implements OnInit {

  searchType: number = 0;
  searchOptions: Array<Object> = [
    { num: 0, name: "Message ends with..." },
    { num: 1, name: "Message between two users with certain type" },
    { num: 2, name: "Message after certain timestamp" },
    { num: 3, name: "All messages" }
  ];

  messageType: Array<Object> = [
    { num: 0, name: "Message ends with..." },
    { num: 1, name: "Message between two users with certain type" },
    { num: 2, name: "Message after certain timestamp" },
    { num: 3, name: "All messages" }
  ];

  messages: Array<Message> = [];
  editField: string;

  updateList(id: number, property: string, event: any) {
    const editField = event.target.textContent;
    this.messages[id][property] = editField;
    this.http.post('api/messages/edit/', this.messages[id]).subscribe(response => { });
  }

  remove(id: any) {
    this.http.get<Response>('api/messages/delete/' + id).subscribe(response => {
      alert(response.status);
      this.messages.splice(id, 1);
    });
  }

  add() {
    const m = new Message();
    this.messages.splice(0, 0, m);
    this.http.post('api/messages/add/', m).subscribe(response => { });
    console.log(m);
  }

  changeValue(id: number, property: string, event: any) {
    this.editField = event.target.textContent;
  }

  constructor(private http: HttpClient) {

  }

  ngOnInit() {
    this.http.get<Array<Message>>('api/messages').subscribe(data => {
      this.messages = data;
    });
  }

}

export class Message {
  public id: number;
  public timeStamp: Date;
  public messageType: string;
  public senderID: number;
  public receiverID: number;
  public content: string;
  public spamScore: number;
  constructor() {
    this.id = 111;
    this.content = "hello";
    this.messageType = "3";
    this.receiverID = 123;
    this.senderID = 456;
    this.spamScore = 32;
    this.timeStamp = new Date();
  }
}
