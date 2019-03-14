import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Component({
  selector: 'app-benchmark',
  templateUrl: './benchmark.component.html',
  styleUrls: ['./benchmark.component.css']
})
export class BenchmarkComponent implements OnInit {
  isBenchmarking = false;
  benchmarkResults: Array<BenchmarkResult> = [new BenchmarkResult(), new BenchmarkResult()];
  timeElapsedBin = '';
  timeElapsedJson = '';
  memoryUsedBin = '';
  memoryUsedJson = '';
  countBin = '';
  countJson = '';
  constructor(private http: HttpClient) { }

  ngOnInit() {
  }

  Benchmark(){
    this.isBenchmarking = true;
    this.http.get<Array<BenchmarkResult>>('api/messages/benchmark/').subscribe(data => {
      this.benchmarkResults = data;
      this.timeElapsedBin = Number(data[0].timeElapsed) * 4 + "%";
      this.timeElapsedJson = Number(data[1].timeElapsed) * 4 + "%";
      this.memoryUsedBin = (data[0].memoryUsed) / 1000000 + "%";
      this.memoryUsedJson = data[1].memoryUsed / 1000000 + "%";
      this.countBin = data[0].n / 1000 + "%";
      this.countJson = data[1].n / 1000 + "%";
      this.isBenchmarking = false;
   },
   err =>  {}//alert(err.error)
   );
  }

}

export class BenchmarkResult
{
  public timeElapsed:string;
  public n:number;
  public memoryUsed:number; 
}
