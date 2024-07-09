export interface IExecuteTransactionCommand {
    fromAccountId: number
    toAccountNumber: string
    amount: number
}