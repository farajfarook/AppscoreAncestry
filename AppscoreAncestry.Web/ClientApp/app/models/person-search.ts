export class PersonSearch{
    constructor(
        public name: string,
        public genders: string[],
        public mode: PersonSearchMode) {           
    }
}

export enum PersonSearchMode{
    Default,
    Ancestors,
    Descendants
}