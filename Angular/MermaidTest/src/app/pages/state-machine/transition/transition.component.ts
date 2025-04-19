import { Component, EventEmitter, inject, Input, input, OnDestroy, OnInit, output, Output } from '@angular/core';
import { FormGroup, NonNullableFormBuilder, Validators } from '@angular/forms';
import { Subscription } from 'rxjs';
import { Transition } from '../models/transition';

@Component({
  selector: 'app-transition',
  templateUrl: './transition.component.html',
  styleUrls: ['./transition.component.css'],
  standalone: false
})
export class TransitionComponent implements OnInit, OnDestroy {

  private fb = inject(NonNullableFormBuilder);
  @Input() value: Transition;
  @Input() state: string;
  @Output() delete = new EventEmitter<Transition>();

  sub: Subscription;
  form: FormGroup;
  constructor() {

  }

  ngOnDestroy(): void {
    // this.valueChange.unsubscribe();
    this.sub.unsubscribe();
  }

  ngOnInit() {
    this.form = this.fb.group({
      Trigger: this.fb.control('', [Validators.required]),
      DestinationState: this.fb.control('', [Validators.required])
    });

    this.sub = this.form.valueChanges.subscribe((value) => {
      this.value.Trigger = value.Trigger;
      this.value.DestinationState = value.DestinationState;
    });
  }

  toDelete() {
    this.delete.emit(this.value);
  }
}
