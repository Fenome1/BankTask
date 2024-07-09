import {FC, useState} from 'react';
import {Form, Input, Modal, notification, Slider, Spin, Typography} from "antd";
import {IDialog} from "../../../../../features/models/IDialog.ts";
import {IAccount} from "../../../../../features/models/IAccount.ts";
import {useExecuteTransactionMutation} from "../../../../../store/apis/transactionApi.ts";
import {IExecuteTransactionCommand} from "../../../../../features/commands/transaction/IExecuteTransactionCommand.ts";

type TransferAccountModalProps = {
    dialog: IDialog
    account: IAccount
}

const TransferAccountModal: FC<TransferAccountModalProps> = ({dialog, account}) => {

    const [form] = Form.useForm();

    const [executeTransaction, data] = useExecuteTransactionMutation()
    const [selectedAmount, setSelectedAmount] = useState(0);

    const onClose = () => {
        form.resetFields()
        setSelectedAmount(0)
        dialog.close()
    }

    const onFinish = async (values: IExecuteTransactionCommand) => {

        if (account.number === values.toAccountNumber) {
            notification.error({
                message: 'Ошибка перевода',
                description: `Перевод на один и тот же счет не возможен`,
            });
            form.resetFields(['toAccountNumber'])
            return
        }

        const command = {
            ...values,
            fromAccountId: account.accountId,
            amount: selectedAmount
        };

        const result = await executeTransaction(command)

        if ("data" in result && result.data) {
            notification.success({
                message: 'Перевод выполнен',
                description: `Переведено ${selectedAmount} ${account.currency.code}`,
            });
            onClose();
        }
    };

    const onAmountChange = (value: number) => {
        setSelectedAmount(value);
        form.setFieldsValue({amount: value});
    }

    return (
        <Modal
            open={dialog.open}
            centered
            title={`Перевод со счета: ${account.number}`}
            okText="Перевести"
            cancelText="Отмена"
            onOk={() => form.submit()}
            onCancel={onClose}
        >
            <Form
                form={form}
                layout="vertical"
                onFinish={onFinish}>
                <Spin spinning={data.isLoading}>
                    <div style={{display: 'flex', flexDirection: "column", gap: '10px'}}>
                        <Typography style={{fontSize: '15pt'}}><b>Доступный
                            баланс: {account.balance} {account.currency.code}</b></Typography>
                        <Form.Item
                            name="toAccountNumber"
                            label="Перевести на"
                            rules={[
                                {required: true, message: 'Пожалуйста, введите номер счета!'},
                                {len: 20, message: "Номер счета должен состоять из 20 символов!"}
                            ]}
                        >
                            <Input placeholder='Номер счета...' maxLength={20}/>
                        </Form.Item>
                        <Form.Item
                            name="amount"
                            label="Сумма"
                            rules={[
                                {required: true, message: 'Пожалуйста, введите сумму!'},
                                {
                                    type: 'number',
                                    min: 0.01,
                                    max: account.balance,
                                    message: `Сумма должна состовлять от 0.01 до ${account.balance}!`
                                }
                            ]}>
                            <Input
                                type="number"
                                placeholder='Введите сумму...'
                                value={selectedAmount}
                                onChange={e => onAmountChange(Number(e.target.value))}
                            />
                        </Form.Item>
                        <Slider
                            min={0}
                            tooltip={{}}
                            max={account.balance}
                            value={selectedAmount}
                            onChange={onAmountChange}
                        />
                    </div>
                </Spin>
            </Form>
        </Modal>
    );
};

export default TransferAccountModal;