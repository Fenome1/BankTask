import {ConfigProvider} from 'antd'
import {persistor, store} from "./store/store.ts";
import {Provider} from 'react-redux'
import {PersistGate} from 'redux-persist/integration/react'
import {RouterProvider} from 'react-router-dom'
import {router} from "./Router.tsx";
import ruRU from "antd/lib/locale/ru_RU";

function App() {
    return (
        <ConfigProvider locale={ruRU}>
            <Provider store={store}>
                <PersistGate loading={null} persistor={persistor}>
                    <RouterProvider router={router}/>
                </PersistGate>
            </Provider>
        </ConfigProvider>
    )
}

export default App
