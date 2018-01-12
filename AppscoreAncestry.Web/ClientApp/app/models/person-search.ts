export class PersonSearch {
    constructor(
        public name: string,
        public male: boolean,
        public female: boolean,
        public mode: PersonSearchMode,
        public page: number = 1,
        public pagesize: number = 10) {           
    }
}

export enum PersonSearchMode{
    Default,
    Ancestors,
    Descendants
}