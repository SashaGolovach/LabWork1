import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Component({
  selector: 'app-benchmark',
  templateUrl: './benchmark.component.html',
  styleUrls: ['./benchmark.component.css']
})
export class BenchmarkComponent implements OnInit {

  constructor(private http: HttpClient) { }

  ngOnInit() {
  }

  Benchmark(){
    this.http.get('api/messages/benchmark/').subscribe(response => {
   },
   err =>  {}//alert(err.error)
   );
  }

}
