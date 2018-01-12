import { Component, Output, EventEmitter, Input } from '@angular/core';
import { PersonSearch, PersonSearchMode } from '../../models/person-search';

@Component({
    selector: 'search',
    templateUrl: './search.component.html'
})
export class SearchComponent {

    @Output() 
    searchEvent = new EventEmitter();
    
    @Input()
    workingFlag: boolean;

    advanceSearch: boolean = false;
    name: string;
    male: boolean;
    female: boolean;
    advanceSearchType: string = "ancestors";
    
    toggleAdvanceSearch(): void {
        this.advanceSearch = !this.advanceSearch;
    }

    onClickSearch(): void {
        let mode = PersonSearchMode.Default;
        if(this.advanceSearch) {
            if(this.advanceSearchType == 'ancestors') {
                mode = PersonSearchMode.Ancestors;
            } else {
                mode = PersonSearchMode.Descendants;
            }
        }
        this.searchEvent.emit(new PersonSearch(this.name, this.male, this.female, mode));
    }
}
