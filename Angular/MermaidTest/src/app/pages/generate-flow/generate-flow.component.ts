import { Component, OnInit } from '@angular/core';
import { StateMachine } from '../state-machine/models/state-machine';

@Component({
  selector: 'app-generate-flow',
  templateUrl: './generate-flow.component.html',
  styleUrls: ['./generate-flow.component.css'],
  standalone: false
})
export class GenerateFlowComponent implements OnInit {

  initalState: string;
  sm: StateMachine = new StateMachine("Edit");
  json:string;
  constructor() { }

  ngOnInit() {
  }

  valueChange(sm: StateMachine) {
    this.sm = sm;
    this.json=JSON.stringify(this.sm.JsonObject);
  }
}
