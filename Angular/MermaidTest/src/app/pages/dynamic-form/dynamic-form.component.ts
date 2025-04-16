import { Component, OnInit, inject } from '@angular/core';
import { SFSchema } from '@delon/form';
import { NzMessageService } from 'ng-zorro-antd/message';

@Component({
  selector: 'app-dynamic-form',
  templateUrl: './dynamic-form.component.html',
  styleUrls: ['./dynamic-form.component.css'],
  standalone: false
})
export class DynamicFormComponent implements OnInit {
  private readonly msg = inject(NzMessageService);

  constructor() { }

  ngOnInit() {
  }
  
  schema: SFSchema = {
    properties: {
      name: { type: 'string' },
      age: { type: 'number' }
    },
    required: ['name', 'age'],
    ui: {
      // 指定 `label` 和 `control` 在一行中各占栅格数
      spanLabel: 4,
      spanControl: 5
    }
  };

  submit(value: {}): void {
    this.msg.success(JSON.stringify(value));
  }
}
