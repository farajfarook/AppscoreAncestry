import { Component, Output, EventEmitter } from '@angular/core';
import { PersonSearch, PersonSearchMode } from '../../models/PersonSearch';

@Component({
    selector: 'search',
    templateUrl: './search.component.html'
})
export class SearchComponent {

    @Output() searchEvent = new EventEmitter();
    
    advanceSearch: boolean = false;
    name: string;
    male: boolean;
    female: boolean;
    advanceSearchType: string = "ancestors";
    
    toggleAdvanceSearch(): void {
        this.advanceSearch = !this.advanceSearch;
    }

    onClickSearch(): void {
        let genders = [];
        if(this.male) { 
            genders.push("M")
        }
        if(this.female) { 
            genders.push("F")
        }
        let mode = PersonSearchMode.Default;
        if(this.advanceSearch) {
            if(this.advanceSearchType == 'ancestors') {
                mode = PersonSearchMode.Ancestors;
            } else {
                mode = PersonSearchMode.Descendants;
            }
        }
        this.searchEvent.emit(new PersonSearch(this.name, genders, mode));
    }
}
