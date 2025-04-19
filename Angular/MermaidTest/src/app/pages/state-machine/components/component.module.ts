import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgZorroAntdModule } from '../../ng-zorro-antd.module';
import { ReactiveFormsModule } from '@angular/forms';
import { DelonFormModule } from '@delon/form';
import { InputComponent } from './input/input.component';
import { FlowChartComponent } from './flow-chart/flow-chart.component';

@NgModule({
  imports: [
    CommonModule,
    NgZorroAntdModule,
    ReactiveFormsModule,
    DelonFormModule.forRoot(),
  ],
  declarations: [
    InputComponent,
    FlowChartComponent
  ],
  exports: [
    InputComponent,
    FlowChartComponent
  ]
})
export class ComponentModule { }
