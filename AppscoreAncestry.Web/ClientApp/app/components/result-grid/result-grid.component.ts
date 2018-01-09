import { Component, OnInit } from '@angular/core';
import { PersonViewModel } from '../../models/PersonViewModel';

@Component({
    selector: 'result-grid',
    templateUrl: './result-grid.component.html'
})
export class ResultGridComponent implements OnInit {

    results: PersonViewModel[];

    ngOnInit(): void {
        this.results = [];
        this.results.push(new PersonViewModel(1, "Faraj", "Male", "Kandy"));
    }
}
