import {FC} from 'react';
import {useTypedSelector} from "../../../../store/hooks/hooks.ts";
import './style.scss'
import {Button} from "antd";
import {LogoutOutlined} from "@ant-design/icons";
import {useLogoutMutation} from "../../../../store/apis/authApi.ts";
import {ILogoutUserCommand} from "../../../../features/commands/auth/ILogoutUserCommand.ts";

type LogoutSelectorProps = {
    className: string
}

const LogoutSelector: FC<LogoutSelectorProps> = ({className}) => {
    const [logout] = useLogoutMutation()
    const {user, accessToken, refreshToken} = useTypedSelector(state => state.user)

    const handleLogout = async () => {
        if (!refreshToken || !accessToken) return;

        const command: ILogoutUserCommand = {
            refreshToken: refreshToken,
            accessToken: accessToken
        };
        await logout(command);
    };

    return (
        <div className={className}>
            <div className='user-content'>
                <span className='user-login'>{user?.login}</span>
                <Button icon={<LogoutOutlined/>}
                        type='link'
                        onClick={handleLogout}
                        style={{fontSize: '12pt', padding: 0}}
                >Выход</Button>
            </div>
        </div>
    );
};

export default LogoutSelector;