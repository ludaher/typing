export class Customer {
    constructor(
        public _id: string,
        public customerId: number = 0,
        public name: String = '',
        public address: String = '',
        public description: String = '',
        public createOn: String = '',
        public modifiedBy: String = '',
        public modifiedOn: any = '',
    ) {

    }
}
