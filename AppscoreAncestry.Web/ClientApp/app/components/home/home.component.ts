import { Component, OnInit } from '@angular/core';
import { PeopleService } from '../../services/people.service';
import { PersonSearch } from '../../models/person-search';
import { PersonSearchResult } from '../../models/person-search-result';

@Component({
    selector: 'home',
    templateUrl: './home.component.html'
})
export class HomeComponent implements OnInit{
    
    workingFlag: boolean = false;
    results: PersonSearchResult = new PersonSearchResult();
    currentSearch: PersonSearch = new PersonSearch();

    constructor(private peopleService: PeopleService) {  }

    ngOnInit(): void {
        this.loadData();
    }

    onSearch($event: PersonSearch) {
        this.currentSearch = $event;
        this.loadData();
    }

    onPageChange($event: number) {
        this.currentSearch.page = $event;
        this.loadData();
    }

    private loadData(){
        this.workingFlag = true;        
        this.peopleService.search(this.currentSearch).subscribe((response) => {
            this.results = response;
            this.workingFlag = false;
        });
    }

}
