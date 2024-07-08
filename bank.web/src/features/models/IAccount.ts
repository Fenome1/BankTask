import {IUser} from "./IUser.ts";
import {ICurrency} from "./ICurrency.ts";

export interface IAccount {
    accountId: number
    owner: IUser
    number: string
    name?: string
    balance: number
    currency: ICurrency
}