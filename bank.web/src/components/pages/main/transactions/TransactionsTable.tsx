import {FC, useEffect, useState} from 'react';
import {IListTransactionsByUserQuery} from "../../../../features/commands/transaction/IListTransactionsByUserQuery.ts";
import {useGetTransactionsByUserQuery} from "../../../../store/apis/transactionApi.ts";
import {Table, TableColumnsType} from "antd";
import {useTypedSelector} from "../../../../store/hooks/hooks.ts";
import {ITransaction} from "../../../../features/models/ITransaction.ts";
import dayjs from 'dayjs';

type TransactionTableProps = {
    filters: IListTransactionsByUserQuery;
};

const TransactionsTable: FC<TransactionTableProps> = ({filters}) => {
    const user = useTypedSelector(state => state.user.user)
    const [page, setPage] = useState(1);
    const {data: transactions, isLoading} = useGetTransactionsByUserQuery({
        ...filters,
        userId: user?.userId ?? 0,
        page
    });

    useEffect(() => {
        console.log(transactions)
    }, [transactions]);

    const columns: TableColumnsType<ITransaction> = [
        {
            title: 'Списание с',
            dataIndex: 'fromAccount',
            key: 'fromAccount',
            align: 'left',
            render: (_, transaction) => (
                <div>
                    {transaction.fromAccount.number}
                </div>
            )
        },
        {
            title: 'Перевод на',
            dataIndex: 'toAccount',
            key: 'toAccount',
            align: 'left',
            render: (_, transaction) => (
                <div>
                    {transaction.toAccount.number}
                </div>
            )
        },
        {
            title: 'Количество',
            dataIndex: 'amount',
            key: 'amount',
            align: 'center',
        },
        {
            title: 'Валюта',
            dataIndex: 'currencyCode',
            key: 'currencyCode',
            align: 'center',
            render: (_, transaction) => (
                <div>
                    {transaction.fromCurrency.code}
                </div>
            )
        },
        {
            title: 'Дата перевода',
            dataIndex: 'date',
            key: 'date',
            align: 'right',
            render: (_, transaction) => (
                <div>
                    {dayjs(transaction.transferDate).toDate().toLocaleDateString()}
                </div>
            )
        }
    ];

    return (
        <Table
            dataSource={transactions?.items}
            columns={columns}
            rowKey="transactionId"
            pagination={{
                current: page,
                pageSize: transactions?.pageSize,
                total: transactions?.totalCount,
                onChange: (page) => setPage(page),
                position: ['bottomCenter']
            }}
            loading={isLoading}
        />
    );
};

export default TransactionsTable;