import { TransactionalBase } from './transactionalBase.entity';

export class User extends TransactionalBase {
    public Id: string;
    public firstName: string;
    public lastName: string;
    public emailAddress: string;
    public password: string;
    public mobileNumber: string;
}

