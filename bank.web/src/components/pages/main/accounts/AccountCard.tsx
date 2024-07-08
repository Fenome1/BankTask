import {IAccount} from "../../../../features/models/IAccount.ts";
import {FC} from "react";
import {Button, message, Popconfirm} from "antd";
import {DeleteOutlined, EditOutlined, SendOutlined} from "@ant-design/icons";
import CopyToClipboard from "react-copy-to-clipboard";
import {useDialog} from "../../../../hok/useDialog.ts";
import {useDeleteAccountMutation} from "../../../../store/apis/accountApi.ts";

type AccountCardProps = {
    account: IAccount
}

const AccountCard: FC<AccountCardProps> = ({account}) => {

    const editDialog = useDialog()
    const startTransferDialog = useDialog()

    const [deleteAccount] = useDeleteAccountMutation()

    const copyHandle = () => {
        message.success(`Номер счета скопирован`)
    }

    const handleDelete = async (accountId: number) => {
        await deleteAccount(accountId)
    }

    return (
        <div className='account-card'>
            <div className='account-card-header'>
                <div className='account-title'>
                    <span className='account-name'>
                        {account.name ?? 'Без имени'}
                    </span>
                    <CopyToClipboard text={account.number}
                                     onCopy={copyHandle}>
                        <Button type='link' style={{padding: 0, justifyContent: 'start'}} className='account-number'>
                            № {account.number}
                        </Button>
                    </CopyToClipboard>
                </div>
                <Popconfirm title="Удалить счет?"
                            onConfirm={() => handleDelete(account.accountId)}>
                    <Button type='link' size='large' danger icon={<DeleteOutlined/>} className='account-delete-button'/>
                </Popconfirm>
            </div>
            <div className='account-content'>
                <div className='account-money-info'>
                    <span className='account-balance'>
                        {account.balance}
                    </span>
                    <span className='account-currency'>
                        {account.currency.code}
                    </span>
                </div>
                <div className='account-buttons'>
                    <Button type='primary' style={{width: '100%'}} icon={<SendOutlined/>}>Перевод</Button>
                    <Button type='text' style={{width: '100%'}} icon={<EditOutlined/>}>Изменение</Button>
                </div>
            </div>
        </div>
    );
};

export default AccountCard;