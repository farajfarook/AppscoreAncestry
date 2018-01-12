import { Component, Input, Output, EventEmitter } from '@angular/core';
import { Person } from '../../models/Person';
import { PersonSearchResult } from '../../models/person-search-result';

@Component({
    selector: 'result-grid',
    templateUrl: './result-grid.component.html'
})
export class ResultGridComponent{

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
        if(this.results.pages <= 10){
            return Array(this.results.pages);
        } else {
            return Array(10);
        }
    }
     
    isPageActive(pageIndex: number): boolean{
        if(this.results.currentPage <= 10){
            return this.results.currentPage == (pageIndex + 1);
        } else {
            return (this.results.currentPage%10) == (pageIndex + 1);
        }
    }
    
    getPageText(pageIndex: number): string {
        let pageBase = Math.floor(this.results.currentPage / 10);
        return (pageIndex + pageBase + 1).toString();
    }

    onClickPage(pageIndex: number): void {
        let pageBase = Math.floor(this.results.currentPage / 10);
        this.pageChange.emit(pageIndex + pageBase + 1);
    }
}
