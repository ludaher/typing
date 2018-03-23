export class FileStorage {
    constructor(
        public _id: string,
        public fileName: String = '',
        public dataBase64: String = null,
        public id: String = '',
        public jsonMetadata: String = ''
    ) {

    }
}
