export interface IUpdateAccountCommand {
    accountId: number
    name?: string
    currencyId?: number
    balance?: number
}