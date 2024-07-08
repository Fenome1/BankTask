import {createBrowserRouter, createRoutesFromElements, Navigate, Route} from "react-router-dom";
import LoginPage from "./components/pages/auth/login/LoginPage.tsx";
import RegisterPage from "./components/pages/auth/register/RegisterPage.tsx";
import RequireAuth from "./hok/RequireAuth.tsx";
import MainPage from "./components/pages/main/MainPage.tsx";

export const router = createBrowserRouter(
    createRoutesFromElements([
        <Route>
            <Route path='login' element={<LoginPage/>}/>
            <Route path='reg' element={<RegisterPage/>}/>
            <Route path='main' element={
                <RequireAuth>
                    <MainPage/>
                </RequireAuth>
            }/>
            <Route path="*" element={<Navigate to="/main" replace={true}/>}/>
        </Route>
    ])
)