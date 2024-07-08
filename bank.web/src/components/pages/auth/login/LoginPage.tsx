import {Button, Form, Input, Spin} from 'antd';
import {ILoginUserCommand} from "../../../../features/commands/auth/ILoginUserCommand.ts";
import {useNavigate} from 'react-router-dom';
import {useLoginMutation} from "../../../../store/apis/authApi.ts";
import {LockOutlined, UserOutlined} from '@ant-design/icons';
import '../style.scss';

const LoginForm = () => {
    const navigate = useNavigate();

    const [form] = Form.useForm();

    const [login, {isLoading}] = useLoginMutation();

    const onFinish = async (values: ILoginUserCommand) => {
        try {
            const result = await login(values);
            if ("data" in result && result.data) {
                form.resetFields()
                navigate("/main")
            }
        } catch (error) {
            console.error(error);
        }
    };

    return (
        <div className="auth-page">
            <div className="auth-card">
                <b className="auth-card-header">Авторизация</b>
                <Spin spinning={isLoading}>
                    <Form
                        name="auth-form"
                        form={form}
                        onFinish={onFinish}
                        className="auth-form">
                        <Form.Item
                            name="login"
                            rules={[
                                {required: true, message: "Введите логин!"},
                                {type: 'string', message: 'Некорректный формат логина!'},
                            ]}>
                            <Input
                                style={{fontSize: "1rem"}}
                                prefix={<UserOutlined/>}
                                placeholder="Логин..."
                                disabled={isLoading}
                            />
                        </Form.Item>

                        <Form.Item
                            name="password"
                            rules={[{required: true, message: "Пожалуйста, введите ваш пароль!"}]}>
                            <Input.Password
                                style={{fontSize: "1rem"}}
                                prefix={<LockOutlined/>}
                                placeholder="Пароль..."
                                disabled={isLoading}
                            />
                        </Form.Item>
                        <div className='auth-button-group'>
                            <Button type="primary" htmlType="submit" className="auth-button"
                                    disabled={isLoading}>
                                Войти
                            </Button>
                            <Button
                                onClick={() => navigate('/reg')}
                                type="link"
                                className="auth-link-button"
                                disabled={isLoading}>
                                Зарегистрируйтесь сейчас!
                            </Button>
                        </div>
                    </Form>
                </Spin>
            </div>
        </div>
    );
};

export default LoginForm;