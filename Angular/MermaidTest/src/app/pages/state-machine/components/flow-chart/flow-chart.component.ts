import { Component, Input, OnInit, SimpleChanges } from '@angular/core';
import { DomSanitizer, SafeHtml } from '@angular/platform-browser';
import mermaid from 'mermaid';

@Component({
  selector: 'app-flow-chart',
  templateUrl: './flow-chart.component.html',
  styleUrls: ['./flow-chart.component.css'],
  standalone: false
})
export class FlowChartComponent implements OnInit {

  @Input() graph: string;
  innHtml: SafeHtml;

  constructor(private dom: DomSanitizer) { }

  ngOnInit() {
  }

  async ngOnChanges(changes: SimpleChanges) {
    await this.load();
  }

  async load() {
    if (!this.graph) return;
    /* diagram html id，必须要添加的 */
    const result = await mermaid.render("diagram", this.graph);
    /* bypassSecurityTrustHtml 生成安全 html */
    this.innHtml = this.dom.bypassSecurityTrustHtml(result.svg);
  }
}
