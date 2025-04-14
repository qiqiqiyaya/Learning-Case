import { Routes, RouterModule } from '@angular/router';
import { DynamicGenerateFlowchartComponent } from './dynamic-generate-flowchart/dynamic-generate-flowchart.component';
import { FlowchartEventComponent } from './flowchart-event/flowchart-event.component';
import { MermaidScriptComponent } from './mermaid-script/mermaid-script.component';

const routes: Routes = [
  { path: '', component: DynamicGenerateFlowchartComponent },
  { path: 'dynamic-generate-flowchart', component: DynamicGenerateFlowchartComponent },
  { path: 'flowchart-event', component: FlowchartEventComponent },
  { path: 'mermaid-script', component: MermaidScriptComponent },
];

export const PagesRoutes = RouterModule.forChild(routes);
