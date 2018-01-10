import { Component, OnInit } from '@angular/core';
import { PersonViewModel } from '../../models/PersonViewModel';

@Component({
    selector: 'result-grid',
    templateUrl: './result-grid.component.html'
})
export class ResultGridComponent implements OnInit {

    results: PersonViewModel[];
    pages: number[];
    currentPage: number;

    ngOnInit(): void {
        this.results = [];
        this.results.push(new PersonViewModel(1, "Faraj", "Male", "Kandy"));
        this.pages = new Array(5);
        this.currentPage = 2;
    }
    
    paginationEnabled(): boolean {
        return this.pages.length > 0;
    }
     
    isPageActive(pageIndex: number): boolean{
        return this.currentPage == (pageIndex + 1);
    }
    
    getPageText(pageIndex: number): string {
        return (pageIndex + 1).toString();
    }
}
