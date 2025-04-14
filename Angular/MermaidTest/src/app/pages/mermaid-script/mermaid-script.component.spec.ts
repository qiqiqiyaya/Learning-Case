/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { MermaidScriptComponent } from './mermaid-script.component';

describe('MermaidScriptComponent', () => {
  let component: MermaidScriptComponent;
  let fixture: ComponentFixture<MermaidScriptComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MermaidScriptComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MermaidScriptComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
