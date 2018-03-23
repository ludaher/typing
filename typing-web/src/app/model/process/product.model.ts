
export class Product {
    constructor(
        public _id: string,
        public formId: number = 0,
        public customerId: number = 0,
        public name: String = '',
        public description: String = '',
        public active: boolean = true,
        public templatePath: String = '',
        public templateHeight: number = 0,
        public productStatus: number = 0,
        public modifiedBy: String = '',
        public modifiedOn: any = '',
        public requiredCaptures: String = '',
        public customerName: String = ''
    ) {

    }
}
