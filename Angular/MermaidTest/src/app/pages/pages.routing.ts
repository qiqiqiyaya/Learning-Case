import { Routes, RouterModule } from '@angular/router';
import { DynamicGenerateFlowchartComponent } from './dynamic-generate-flowchart/dynamic-generate-flowchart.component';
import { FlowchartEventComponent } from './flowchart-event/flowchart-event.component';
import { MermaidScriptComponent } from './mermaid-script/mermaid-script.component';
import { DynamicFormComponent } from './dynamic-form/dynamic-form.component';
import { StateMachineComponent } from './state-machine/state-machine/state-machine.component';
import { GenerateFlowComponent } from './generate-flow/generate-flow.component';
import { FlowApprovalComponent } from './flow-approval/flow-approval.component';

const routes: Routes = [
  { path: '', component: DynamicGenerateFlowchartComponent },
  { path: 'dynamic-generate-flowchart', component: DynamicGenerateFlowchartComponent },
  { path: 'flowchart-event', component: FlowchartEventComponent },
  { path: 'mermaid-script', component: MermaidScriptComponent },
  { path: 'dynamic-form', component: DynamicFormComponent },
  { path: 'generate-flow', component: GenerateFlowComponent },
  { path: 'flow-approval', component: FlowApprovalComponent },
];

export const PagesRoutes = RouterModule.forChild(routes);
