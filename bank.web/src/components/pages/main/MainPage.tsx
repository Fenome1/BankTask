import AccountsSelector from "./accounts/AccountsSelector.tsx";
import './style.scss'
import {Layout} from "antd";
import LogoutSelector from "./logout/LogoutSelector.tsx";
import TransactionsSelector from "./transactions/TransactionsSelector.tsx";

const MainPage = () => {
    return (
        <Layout className="page-content">
            <AccountsSelector className='accounts-selector'/>
            <TransactionsSelector className='transactions-selector'/>
            <LogoutSelector className='logout-selector'/>
        </Layout>
    );
};

export default MainPage;