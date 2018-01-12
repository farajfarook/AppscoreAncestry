import { Person } from "./Person";

export class PersonSearchResult {
    public people: Person[];
    public pages: number;
    public currentPage: number;
    public skip: number = 0;
    public take: number = 0;
    public total: number = 0;
}