import {IListTransactionsByUserQuery} from "../../../../features/commands/transaction/IListTransactionsByUserQuery.ts";
import {FC} from "react";
import {useGetCurrenciesQuery} from "../../../../store/apis/currencyApi.ts";
import {Button, DatePicker, Form, FormProps, Select, Spin} from "antd";
import './style.scss'
import {useGetAccountsByUserQuery} from "../../../../store/apis/accountApi.ts";
import {useTypedSelector} from "../../../../store/hooks/hooks.ts";

type TransactionFilterFormProps = {
    onFilter: (filters: IListTransactionsByUserQuery) => void;
};

const TransactionsFilterForm: FC<TransactionFilterFormProps> = ({onFilter}) => {
    const user = useTypedSelector(state => state.user.user)

    const {data: currencies, isLoading: isCurrenciesLoading} = useGetCurrenciesQuery();
    const {
        data: accounts,
        isLoading: isAccountsLoading
    } = useGetAccountsByUserQuery(user?.userId ?? 0, {skip: user === null})
    const [form] = Form.useForm();

    const onFinish: FormProps['onFinish'] = async (values) => {
        const filters: IListTransactionsByUserQuery = {
            ...values,
            startDate: values.startDate?.format('YYYY-MM-DD'),
            endDate: values.endDate?.format('YYYY-MM-DD'),
        };
        onFilter(filters);
    };

    const onClearHandle = () => {
        form.resetFields()
        form.submit()
    }

    return (
        <Form form={form} onFinish={onFinish} layout='vertical' className='transactions-filters-form'>
            <Spin spinning={isCurrenciesLoading || isAccountsLoading}>
                <Form.Item name="startDate" label="Дата от">
                    <DatePicker style={{width: '100%'}}/>
                </Form.Item>
                <Form.Item name="endDate" label="Дата до">
                    <DatePicker style={{width: '100%'}}/>
                </Form.Item>
                <Form.Item name="currencyId" label="Валюта">
                    <Select
                        showSearch
                        placeholder='Выбор валюты...'
                        optionFilterProp="children">
                        {currencies?.map(currency => (
                            <Select.Option key={currency.currencyId} value={currency.currencyId}>
                                {currency.code}
                            </Select.Option>
                        ))}
                    </Select>
                </Form.Item>
                <Form.Item name="accountId" label="Номер счета">
                    <Select
                        showSearch
                        placeholder='Номер счета...'
                        optionFilterProp="children">
                        {accounts?.map(account => (
                            <Select.Option key={account.accountId} value={account.accountId}>
                                {account.number}
                            </Select.Option>
                        ))}
                    </Select>
                </Form.Item>
                <Form.Item>
                    <div className='transaction-form-buttons'>
                        <Button type="link" htmlType="submit">
                            Применить
                        </Button>
                        <Button type="link" danger onClick={onClearHandle}>
                            Сбросить
                        </Button>
                    </div>
                </Form.Item>
            </Spin>
        </Form>
    );
};

export default TransactionsFilterForm;