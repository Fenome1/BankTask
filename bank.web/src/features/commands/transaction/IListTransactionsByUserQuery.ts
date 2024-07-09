export interface IListTransactionsByUserQuery {
    userId: number;
    startDate?: string;
    endDate?: string;
    currencyId?: number;
    accountId?: number;
    page?: number;
    pageSize?: number;
}