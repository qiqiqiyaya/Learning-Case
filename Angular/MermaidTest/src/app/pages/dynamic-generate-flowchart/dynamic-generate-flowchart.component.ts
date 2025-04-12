import { Component, ElementRef, Inject, OnInit, PLATFORM_ID } from '@angular/core';
import { SafeHtml, DomSanitizer } from '@angular/platform-browser';
import mermaid from 'mermaid';

@Component({
  selector: 'app-dynamic-generate-flowchart',
  templateUrl: './dynamic-generate-flowchart.component.html',
  styleUrls: ['./dynamic-generate-flowchart.component.css'],
  standalone: false
})
export class DynamicGenerateFlowchartComponent implements OnInit {

  value: string;
  // 初始流程图定义
  graph = `
  flowchart TD
        A[初始节点] --> B[子节点]
        B --> C[结束节点]
  `;

  innHtml: SafeHtml = "11111111111111";

  constructor(@Inject(PLATFORM_ID) private _platformId: object,
    private dom: DomSanitizer,
    private html: ElementRef
  ) {
  }

  ngAfterViewInit(): void {

  }

  ngOnInit() {
  }

  async reload() {
    this.graph = this.updateSpecificNode(this.graph, "A", this.value);

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
