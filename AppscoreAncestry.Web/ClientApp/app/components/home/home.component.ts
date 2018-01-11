import { Component } from '@angular/core';
import { PeopleService } from '../../services/people.service';

@Component({
    selector: 'home',
    templateUrl: './home.component.html'
})
export class HomeComponent {

    constructor(private peopleService: PeopleService) {  }

    onSearch($event: any) {
        //this.peopleService.search(event.)
        console.log($event);
    }
}
