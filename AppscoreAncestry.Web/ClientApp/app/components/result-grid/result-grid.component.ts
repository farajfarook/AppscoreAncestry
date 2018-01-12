import { Component, Input, Output, EventEmitter } from '@angular/core';
import { Person } from '../../models/Person';
import { PersonSearchResult } from '../../models/person-search-result';

@Component({
    selector: 'result-grid',
    templateUrl: './result-grid.component.html'
})
export class ResultGridComponent{

    paginationSize: number = 11;

    @Input()
    results: PersonSearchResult;

    @Input()
    workingFlag: boolean;

    @Output() 
    pageChange = new EventEmitter();
    
    paginationEnabled(): boolean {
        return this.results.pages > 0;
    }

    getPagesArray(): number[] {
        if(this.results.pages <= this.paginationSize){
            return Array(this.results.pages);
        } else {
            return Array(this.paginationSize);
        }
    }
     
    isPageActive(pageIndex: number): boolean{
        return this.results.currentPage == this.getPageNumber(pageIndex);
    }
    
    getPageNumber(pageIndex: number): number {
        let halfPage = Math.floor(this.paginationSize / 2);
        let pageBase = 0;
        if((this.results.pages > this.paginationSize) && (this.results.currentPage > halfPage)) {
            if(this.results.currentPage <= (this.results.pages - halfPage))
                pageBase = this.results.currentPage - (halfPage + 1);
            else
                pageBase = this.results.pages - this.paginationSize;
        }
        let page = pageIndex + pageBase + 1;
        return page;
    }

    onClickPage(pageIndex: number): void {                 
        this.results.currentPage = this.getPageNumber(pageIndex);
        this.pageChange.emit(this.results.currentPage);
    }
}
