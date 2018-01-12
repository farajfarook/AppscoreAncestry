import { Component } from '@angular/core';
import { PeopleService } from '../../services/people.service';
import { PersonSearch } from '../../models/person-search';
import { PersonSearchResult } from '../../models/person-search-result';

@Component({
    selector: 'home',
    templateUrl: './home.component.html'
})
export class HomeComponent {
    workingFlag: boolean = false;
    results: PersonSearchResult = new PersonSearchResult();
    currentSearch: PersonSearch;

    constructor(private peopleService: PeopleService) {  }

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
        this.results = new PersonSearchResult();
        this.peopleService.search(this.currentSearch).subscribe((response) => {
            this.results = response;
            this.workingFlag = false;
        });
    }

}
