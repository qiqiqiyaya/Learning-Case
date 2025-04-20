import { Component, OnInit } from '@angular/core';
import { PeApproveFlow } from '../state-machine/test-data/Pe-approve-flow';
import { StateMachine } from '../state-machine/models/state-machine';

@Component({
  selector: 'app-flow-approval',
  templateUrl: './flow-approval.component.html',
  styleUrls: ['./flow-approval.component.css'],
  standalone: false
})
export class FlowApprovalComponent implements OnInit {

  sm: StateMachine;

  constructor() { }

  ngOnInit() {
    this.sm = PeApproveFlow.Create();
  }

}
