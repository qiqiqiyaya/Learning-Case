import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PagesRoutes } from './pages.routing';
import { RouterOutlet } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { DynamicGenerateFlowchartComponent } from './dynamic-generate-flowchart/dynamic-generate-flowchart.component';
import { FlowchartEventComponent } from './flowchart-event/flowchart-event.component';
import { MermaidScriptComponent } from './mermaid-script/mermaid-script.component';
import { DynamicFormComponent } from './dynamic-form/dynamic-form.component';
import { DelonFormModule } from '@delon/form';
import { StateMachineComponent } from './state-machine/state-machine/state-machine.component';
import { StateRepresentationComponent } from './state-machine/state-representation/state-representation.component';
import { TransitionComponent } from './state-machine/transition/transition.component';
import { NgZorroAntdModule } from './ng-zorro-antd.module';
import { ComponentModule } from './state-machine/components/component.module';
import { GenerateFlowComponent } from './generate-flow/generate-flow.component';
import { FlowApprovalComponent } from './flow-approval/flow-approval.component';

@NgModule({
  imports: [
    CommonModule,
    PagesRoutes,
    FormsModule,
    RouterOutlet, 
    DelonFormModule.forRoot(),
    ReactiveFormsModule,
    NgZorroAntdModule,
    ComponentModule
  ],
  declarations: [
    FlowchartEventComponent,
    DynamicGenerateFlowchartComponent,
    MermaidScriptComponent,
    DynamicFormComponent,
    StateMachineComponent,
    StateRepresentationComponent,
    TransitionComponent,
    GenerateFlowComponent,
    FlowApprovalComponent
  ],
})
export class PagesModule { }
