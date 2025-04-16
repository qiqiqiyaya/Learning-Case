import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PagesRoutes } from './pages.routing';
import { RouterOutlet } from '@angular/router';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { NzLayoutModule } from 'ng-zorro-antd/layout';
import { NzMenuModule } from 'ng-zorro-antd/menu';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { DynamicGenerateFlowchartComponent } from './dynamic-generate-flowchart/dynamic-generate-flowchart.component';
import { FlowchartEventComponent } from './flowchart-event/flowchart-event.component';
import { MermaidScriptComponent } from './mermaid-script/mermaid-script.component';
import { DynamicFormComponent } from './dynamic-form/dynamic-form.component';
import { DelonFormModule } from '@delon/form';

@NgModule({
  imports: [
    CommonModule,
    PagesRoutes,
    FormsModule,
    RouterOutlet, 
    NzIconModule,
    NzLayoutModule, 
    NzMenuModule,
    NzButtonModule ,
    DelonFormModule.forRoot(),
    ReactiveFormsModule
  ],
  declarations: [
    FlowchartEventComponent,
    DynamicGenerateFlowchartComponent,
    MermaidScriptComponent,
    DynamicFormComponent
  ],
})
export class PagesModule { }
