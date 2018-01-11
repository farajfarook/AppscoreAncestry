import { Injectable } from "@angular/core";
import { Observable } from "rxjs/Observable";
import { PersonSearchResult } from "../models/PersonSearchResult";

@Injectable()
export class PeopleService {
    
    search(): Observable<PersonSearchResult> {
        //@TODO
        return new Observable();
    }
}
