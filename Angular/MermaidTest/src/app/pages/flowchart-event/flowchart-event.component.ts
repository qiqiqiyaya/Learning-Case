import { Component, ElementRef, HostListener, Inject, OnInit, PLATFORM_ID, ViewChild } from '@angular/core';
import { SafeHtml, DomSanitizer } from '@angular/platform-browser';
import mermaid from 'mermaid';

@Component({
  selector: 'app-flowchart-event',
  templateUrl: './flowchart-event.component.html',
  styleUrls: ['./flowchart-event.component.css'],
  standalone: false
})
export class FlowchartEventComponent implements OnInit {

  value: string;
  // 初始流程图定义
  graph = `
  flowchart LR
      A-->B
      B-->C
      C-->D
      click A call mermaidFunction()
      click C call mermaidFunction()
  `;

  graphDefinition = `graph TD\nA[Christmas] -->|Get money| B(Go shopping)\nB --> C(Let me think)\nC -->|One| D[Laptop]\nC -->|Two| E[iPhone]\nC -->|Three| F[fa:fa-car Car]\nA[Christmas] -->|Get money| D[Laptop]\nB --> E \nclick A CustomFunction`;

  @ViewChild('mermaid', { static: true }) mermaid: ElementRef;
  innHtml: SafeHtml = "";

  @HostListener('window:mermaid-click', ['$event'])
  mermaidClick(e: CustomEvent) {
  }

  constructor(@Inject(PLATFORM_ID) private _platformId: object,
    private dom: DomSanitizer,
    private html: ElementRef
  ) {
  }

  ngAfterViewInit() {
    mermaid.initialize({
      theme: "default"
    });
  }

  ngOnInit() {
    const aa = window as any;
    aa.callback = function (e: any, b: any, c: any) {
      alert('A callback was triggered');
    };
  }

  async reload() {
    // this.graph = this.updateSpecificNode(this.graph, "A", this.value);

    /* diagram html id，必须要添加的 */
    const result = await mermaid.render("diagram", this.graph);
    /* bypassSecurityTrustHtml 生成安全 html */
    this.innHtml = this.dom.bypassSecurityTrustHtml(result.svg);
  }

  // 使用更精确的正则匹配（带节点类型检查）
  updateSpecificNode = (source: string, nodeId: string, newName: string) => {
    const regex = new RegExp(`(${nodeId})\\[(.*?)\\]`, 'g');
    source = source.replace(
      regex,
      `$1[${newName}]`
    );

    return source;
  };
}
