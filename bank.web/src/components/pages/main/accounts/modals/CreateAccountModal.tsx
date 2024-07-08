import {FC} from 'react';
import {IDialog} from "../../../../../features/models/IDialog.ts";
import {Form, FormProps, Input, Modal, Select, Spin} from "antd";
import {useCreateAccountMutation} from "../../../../../store/apis/accountApi.ts";
import {useGetCurrenciesQuery} from "../../../../../store/apis/currencyApi.ts";

type CreateAccountModalProps = {
    userId: number
    dialog: IDialog
}

const CreateAccountModal: FC<CreateAccountModalProps> = ({userId, dialog}) => {
    const [form] = Form.useForm();
    const [createAccount] = useCreateAccountMutation()
    const {data: currencies, isLoading} = useGetCurrenciesQuery()

    const onClose = () => {
        form.resetFields()
        dialog.close()
    }

    const onFinish: FormProps['onFinish'] = async (command) => {
        command.userId = userId
        await createAccount(command)
        onClose()
    };

    return (
        <Modal
            open={dialog.open}
            centered
            title="Открытие счета"
            okText="Сохранить"
            cancelText="Отмена"
            onOk={() => form.submit()}
            onCancel={onClose}>
            <Form form={form}
                  onFinish={onFinish}>
                <Spin spinning={isLoading}>
                    <Form.Item
                        name='name'
                        label="Имя счета">
                        <Input placeholder='Название счета...' showCount maxLength={25}/>
                    </Form.Item>
                    <Form.Item
                        name='currencyId'
                        label="Имя счета"
                        rules={[{required: true, message: "Пожалуйста, выберите валюту!"}]}>
                        <Select
                            showSearch
                            placeholder='Выбор валюты...'
                            optionFilterProp="children"
                        >
                            {currencies?.map(currency => (
                                <Select.Option key={currency.currencyId} value={currency.currencyId}>
                                    {currency.code}
                                </Select.Option>
                            ))}
                        </Select>
                    </Form.Item>
                </Spin>
            </Form>
        </Modal>
    );
};

export default CreateAccountModal;