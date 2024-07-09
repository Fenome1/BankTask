import {FC, useEffect} from 'react';
import {IDialog} from "../../../../../features/models/IDialog.ts";
import {Form, FormProps, Input, Modal, Select, Spin} from "antd";
import {IAccount} from "../../../../../features/models/IAccount.ts";
import {useUpdateAccountMutation} from "../../../../../store/apis/accountApi.ts";
import {useGetCurrenciesQuery} from "../../../../../store/apis/currencyApi.ts";

type EditAccountModalProps = {
    account: IAccount
    dialog: IDialog
}

const EditAccountModal: FC<EditAccountModalProps> = ({account, dialog}) => {
    const [form] = Form.useForm();
    const [updateAccount, {isLoading}] = useUpdateAccountMutation();
    const {data: currencies} = useGetCurrenciesQuery();

    useEffect(() => {
        form.setFieldsValue({
            name: account.name,
            currencyId: account.currency.currencyId,
            balance: account.balance
        });
    }, [account, form]);

    const onClose = () => {
        form.resetFields();
        dialog.close();
    }

    const onFinish: FormProps['onFinish'] = async (values) => {
        const updatedAccount = {
            ...account,
            ...values
        };
        await updateAccount(updatedAccount);
        onClose();
    };

    return (
        <Modal
            open={dialog.open}
            centered
            title="Редактирование счета"
            okText="Сохранить"
            cancelText="Отмена"
            onOk={() => form.submit()}
            onCancel={onClose}
        >
            <Form form={form} onFinish={onFinish}>
                <Spin spinning={isLoading}>
                    <Form.Item
                        name="name"
                        label="Имя счета">
                        <Input placeholder="Название счета..." showCount maxLength={25}/>
                    </Form.Item>
                    <Form.Item
                        name="currencyId"
                        label="Валюта">
                        <Select
                            showSearch
                            placeholder="Выбор валюты..."
                            optionFilterProp="children">
                            {currencies?.map(currency => (
                                <Select.Option key={currency.currencyId} value={currency.currencyId}>
                                    {currency.code}
                                </Select.Option>
                            ))}
                        </Select>
                    </Form.Item>
                    <Form.Item
                        name="balance"
                        label="Баланс"
                        rules={[
                            {
                                validator: (_, value) => {
                                    if (value < 0 || value > 9999999) {
                                        return Promise.reject('Баланс может быть от 0 до 9 999 999');
                                    }
                                    return Promise.resolve();
                                }
                            }
                        ]}>
                        <Input type="number" placeholder="Баланс..."/>
                    </Form.Item>
                </Spin>
            </Form>
        </Modal>
    );
};

export default EditAccountModal;
