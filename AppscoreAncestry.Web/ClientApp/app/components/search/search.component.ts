import { Component } from '@angular/core';

@Component({
    selector: 'search',
    templateUrl: './search.component.html'
})
export class SearchComponent {
    advanceSearch: boolean = false;
    
    toggleAdvanceSearch(): void {
        this.advanceSearch = !this.advanceSearch;
    }
}
