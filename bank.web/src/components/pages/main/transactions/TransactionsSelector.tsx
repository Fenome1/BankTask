import {FC, useState} from 'react';
import TransactionsFilterForm from "./TransactionsFilterForm.tsx";
import TransactionsTable from "./TransactionsTable.tsx";
import {Card} from "antd";
import {Content} from 'antd/es/layout/layout';
import {IListTransactionsByUserQuery} from "../../../../features/commands/transaction/IListTransactionsByUserQuery.ts";
import './style.scss'

type TransactionsSelectorProps = {
    className?: string
}

const TransactionsSelector: FC<TransactionsSelectorProps> = ({className}) => {
    const [filters, setFilters] = useState<IListTransactionsByUserQuery>({userId: 1});
    return (
        <div className={className}>
            <Content style={{padding: '10px'}} className='transactions-group'>
                <Card title="Переводы" className='transactions-table-card'>
                    <TransactionsTable filters={filters}/>
                </Card>
                <Card title="Фильтры" className='transactions-filters-card'>
                    <TransactionsFilterForm onFilter={setFilters}/>
                </Card>
            </Content>
        </div>
    );
};

export default TransactionsSelector;