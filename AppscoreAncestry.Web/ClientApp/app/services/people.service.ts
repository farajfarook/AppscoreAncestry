import { Injectable } from "@angular/core";
import { Observable } from "rxjs/Observable";
import { PersonSearchResult } from "../models/person-search-result";
import { Http, Response, RequestOptions, URLSearchParams } from "@angular/http";
import 'rxjs/Rx';
import { PersonSearch } from "../models/person-search";

@Injectable()
export class PeopleService {

    constructor (private http: Http) {}
    
    search(search: PersonSearch): Observable<PersonSearchResult> {

        let params = new URLSearchParams();
        params.set('mode', search.mode.toString());
        params.set('name', search.name);
        params.set('male', search.male ? "true" : "false");
        params.set('female', search.female ? "true" : "false");

        let requestOptions = new RequestOptions();
        requestOptions.params = params;

        return this.http.get("api/People", requestOptions)
            .map((res:Response) => res.json())         
            .catch((error:any) => Observable.throw(error.json().error || 'Server error'));
    }
}
