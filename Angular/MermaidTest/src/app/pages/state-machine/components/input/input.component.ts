import { Component, inject, OnInit } from '@angular/core';
import { SFSchema, SFStringWidgetSchema } from '@delon/form';
import { NzMessageService } from 'ng-zorro-antd/message';

@Component({
  selector: 'app-input',
  templateUrl: './input.component.html',
  styleUrls: ['./input.component.css'],
  standalone: false
})
export class InputComponent implements OnInit {
  private readonly msg = inject(NzMessageService);

  schema: SFSchema = {
    properties: {
      q: {
        type: 'string',
        title: 'Key',
      }
    }
  };

  constructor() { }

  ngOnInit() {
  }

  submit(value: {}): void {
    this.msg.success(JSON.stringify(value));
  }
}
