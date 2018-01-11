import { Component } from '@angular/core';
import { PeopleService } from '../../services/people.service';
import { PersonSearch } from '../../models/person-search';

@Component({
    selector: 'home',
    templateUrl: './home.component.html'
})
export class HomeComponent {

    constructor(private peopleService: PeopleService) {  }

    onSearch($event: PersonSearch) {
        this.peopleService.search($event).subscribe((response) => {
            console.log(response);
        });
    }
}
