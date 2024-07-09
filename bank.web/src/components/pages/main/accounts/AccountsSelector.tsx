import {useTypedSelector} from "../../../../store/hooks/hooks.ts";
import {useGetAccountsByUserQuery} from "../../../../store/apis/accountApi.ts";
import './style.scss'
import AccountCard from "./AccountCard.tsx";
import {Button, message, Skeleton} from "antd";
import {FC} from "react";
import {PlusOutlined} from "@ant-design/icons";
import {useDialog} from "../../../../hok/useDialog.ts";
import CreateAccountModal from "./modals/CreateAccountModal.tsx";

type AccountsSelectorProps = {
    className?: string
}

const AccountsSelector: FC<AccountsSelectorProps> = ({className}) => {
    const user = useTypedSelector(state => state.user.user)
    const {data: accounts, isLoading} = useGetAccountsByUserQuery(user!.userId, {skip: user == null})

    const createDialog = useDialog()

    const createHandle = () => {

        if (accounts && accounts.length >= 5) {
            message.info("Превышен лимит открытых счетов 5")
            return
        }

        createDialog.show()
    }

    return (
        <div className={className}>
            <div className='accounts-group'>
                <div className='accounts-group-title'>
                    <span className='accounts-group-text'>
                        Счета
                    </span>
                    <span className='accounts-group-count'>
                        {accounts?.length ?? 0} / 5
                    </span>
                </div>
                <Button className='account-create-button'
                        type='link'
                        icon={<PlusOutlined/>} onClick={createHandle}>Открыть</Button>
                <Skeleton loading={isLoading}>
                    <div className='accounts-list'>
                        {accounts?.map((account) => (
                            <AccountCard account={account} key={account.accountId}/>
                        ))}
                    </div>
                </Skeleton>
            </div>
            <CreateAccountModal userId={user!.userId} dialog={createDialog}/>
        </div>
    );
};

export default AccountsSelector;