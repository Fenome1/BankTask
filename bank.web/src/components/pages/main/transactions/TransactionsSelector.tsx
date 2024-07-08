import {FC} from 'react';

type TransactionsSelectorProps = {
    className?: string
}

const TransactionsSelector: FC<TransactionsSelectorProps> = ({className}) => {
    return (
        <div className={className}>
            Transactions
        </div>
    );
};

export default TransactionsSelector;