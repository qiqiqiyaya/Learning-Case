import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { StateRepresentation } from '../models/state-representation';
import { Transition } from '../models/transition';

@Component({
  selector: 'app-state-representation',
  templateUrl: './state-representation.component.html',
  styleUrls: ['./state-representation.component.css'],
  standalone: false
})
export class StateRepresentationComponent implements OnInit {

  @Input() sr: StateRepresentation;
  @Output() delete = new EventEmitter<StateRepresentation>();
  constructor() { }

  ngOnInit() {

  }

  add() {
    this.sr.newTransition();
  }

  emit() {
    // this.valueChange.emit(this.sr);
  }

  toDelete() {
    this.delete.emit(this.sr);
  }

  trDelete(tr: Transition) {
    this.sr.remove(tr);
  }
}
