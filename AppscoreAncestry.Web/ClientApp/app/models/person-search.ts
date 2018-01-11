export class PersonSearch {
    constructor(
        public name: string,
        public male: boolean,
        public female: boolean,
        public mode: PersonSearchMode) {           
    }
}

export enum PersonSearchMode{
    Default,
    Ancestors,
    Descendants
}