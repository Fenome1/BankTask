import {LockOutlined, UserOutlined} from '@ant-design/icons';
import {Button, Form, FormProps, Input, message, Spin} from 'antd';
import {ILoginUserCommand} from "../../../../features/commands/ILoginUserCommand.ts";
import {useLoginMutation} from "../../../../store/apis/authApi.ts";
import {useNavigate} from 'react-router-dom';
import {useRegiserUserMutation} from "../../../../store/apis/userApi.ts";
import {IRegisterUserCommand} from "../../../../features/commands/IRegisterUserCommand.ts";

type Fields = {
    login: string
    password: string
    confirmPassword: string
}

const RegisterPage = () => {
    const navigate = useNavigate()

    const [registerUser, {isLoading: isRegisterLoading}] = useRegiserUserMutation();
    const [login, {isLoading: isLoginLoading}] = useLoginMutation();

    const [form] = Form.useForm();

    const onFinish: FormProps<Fields>['onFinish'] = async (values) => {
        if (values.password !== values.confirmPassword) {
            message.error("Пароли не совпадают", 5)
            return
        }

        const authUserCommand: IRegisterUserCommand | ILoginUserCommand = {
            login: values.login,
            password: values.password
        }

        try {
            const result = await registerUser(authUserCommand);

            if ("data" in result && result.data) {

                const result = await login(authUserCommand);

                if ("data" in result && result.data) {
                    navigate("/main")
                }

                form.resetFields()
            }
        } catch (error) {
            console.error(error)
        }
    };

    return (
        <div className="auth-page">
            <div className="auth-card">
                <b className="auth-card-header">Регистрация</b>
                <Spin spinning={isRegisterLoading || isLoginLoading}>
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
                                disabled={isRegisterLoading || isLoginLoading}
                            />
                        </Form.Item>

                        <Form.Item
                            name="password"
                            rules={[{required: true, message: "Пожалуйста, введите ваш пароль!"}]}>
                            <Input.Password
                                style={{fontSize: "1rem"}}
                                prefix={<LockOutlined/>}
                                placeholder="Пароль..."
                                disabled={isRegisterLoading || isLoginLoading}
                            />
                        </Form.Item>

                        <Form.Item
                            name="confirmPassword"
                            dependencies={['password']}
                            rules={[
                                {required: true, message: 'Пожалуйста, подтвердите ваш пароль!'},
                                ({getFieldValue}) => ({
                                    validator(_, value) {
                                        if (!value || getFieldValue('password') === value) {
                                            return Promise.resolve();
                                        }
                                        return Promise.reject(new Error('Пароли не совпадают!'));
                                    },
                                }),
                            ]}>
                            <Input.Password prefix={<LockOutlined/>}
                                            disabled={isRegisterLoading || isLoginLoading}
                                            style={{fontSize: '1rem'}}
                                            placeholder="Подтверждение..."/>
                        </Form.Item>

                        <div className='auth-button-group'>
                            <Button type="primary" htmlType="submit" className="auth-button"
                                    disabled={isRegisterLoading || isLoginLoading}>
                                Зарегистрироваться
                            </Button>
                            <Button
                                onClick={() => navigate('/login')}
                                type="link"
                                className="auth-link-button"
                                disabled={isRegisterLoading || isLoginLoading}>
                                Уже есть аккаунт? Войдите сейчас!
                            </Button>
                        </div>
                    </Form>
                </Spin>
            </div>
        </div>
    );
};

export default RegisterPage;