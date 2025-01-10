import { Component, ContentChild, Input, TemplateRef } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrl: './list.component.css'
})
export class ListComponent {
  @Input() itemList!: any[];
  @Input() linkPath: string = "id";
  @Input() showPaginator: Boolean = false;
  @ContentChild(TemplateRef) templateRef!: TemplateRef<any>;

  pageSize = 10;
  pageIndex = 0;
  pageSizeOptions = [5, 10, 25];
  pageRangeStart = 0;
  pageRangeEnd = 10;

  handlePageEvent(e: PageEvent) {
    this.pageSize = e.pageSize;
    this.pageIndex = e.pageIndex;
    this.pageRangeStart = this.pageIndex * this.pageSize;
    this.pageRangeEnd = Math.min((this.pageIndex + 1) * this.pageSize, this.itemList.length);
  }
}
