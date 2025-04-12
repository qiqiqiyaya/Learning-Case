import { Routes, RouterModule } from '@angular/router';
import { DynamicGenerateFlowchartComponent } from './dynamic-generate-flowchart/dynamic-generate-flowchart.component';
import { FlowchartEventComponent } from './flowchart-event/flowchart-event.component';

const routes: Routes = [
  { path: '', component: DynamicGenerateFlowchartComponent },
  { path: 'dynamic-generate-flowchart', component: DynamicGenerateFlowchartComponent },
  { path: 'flowchart-event', component: FlowchartEventComponent },
];

export const PagesRoutes = RouterModule.forChild(routes);
