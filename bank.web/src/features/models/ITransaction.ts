import {IAccount} from "./IAccount.ts";
import {ICurrency} from "./ICurrency.ts";

export interface ITransaction {
    transactionId: number
    amount: number
    fromAccount: IAccount
    toAccount: IAccount
    transferDate: string
    fromCurrency: ICurrency
    toCurrency: ICurrency
}