export class PersonSearch {
    constructor(
        public name: string = "",
        public male: boolean = false,
        public female: boolean = false,
        public mode: PersonSearchMode = PersonSearchMode.Default,
        public page: number = 1,
        public pagesize: number = 10) {           
    }
}

export enum PersonSearchMode{
    Default,
    Ancestors,
    Descendants
}