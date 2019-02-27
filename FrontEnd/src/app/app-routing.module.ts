import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ManualComponent } from './manual/manual.component';
import { AutoComponent } from './auto/auto.component';
import { BenchmarkComponent } from './benchmark/benchmark.component';

const routes: Routes = [
  { path: 'manual', component: ManualComponent },
  { path: 'auto', component: AutoComponent },
  { path: 'benchmark', component: BenchmarkComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
