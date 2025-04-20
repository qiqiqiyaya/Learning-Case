import { Component, EventEmitter, inject, Input, OnInit, Output, output } from '@angular/core';
import { NzMessageService } from 'ng-zorro-antd/message';
import { StateMachine } from '../models/state-machine';
import { debounceTime } from 'rxjs';
import { MermaidHelper } from '../models/mermaid-helper';
import { StateRepresentation } from '../models/state-representation';

@Component({
  selector: 'app-state-machine',
  templateUrl: './state-machine.component.html',
  styleUrls: ['./state-machine.component.css'],
  standalone: false
})
export class StateMachineComponent implements OnInit {
  private readonly msg = inject(NzMessageService);

  @Output() valueChange = new EventEmitter<StateMachine>();

  @Input() sm: StateMachine;
  graph: string;

  constructor() { }

  ngOnInit() {
    this.initSm();
  }

  initSm(){
    if(this.sm){
      this.sm.valueChange.pipe(debounceTime(500)).subscribe(res => {
        this.graph = MermaidHelper.generate(res);
      });
      this.sm.valueChange.subscribe(res => this.valueChange.emit(res));
    }
  }

  add() {
    this.sm?.newStateConfiguration();
  }

  delete(sr: StateRepresentation) {
    this.sm?.remove(sr);
  }
}
