import {authApi, baseApi} from "./apis/authApi.ts";
import userSlice from "./slices/userSlice.ts";
import {combineReducers, configureStore} from "@reduxjs/toolkit";
import storage from "redux-persist/es/storage";
import {FLUSH, PAUSE, PERSIST, persistReducer, persistStore, PURGE, REGISTER, REHYDRATE} from "redux-persist";

const rootReducer = combineReducers({
    [baseApi.reducerPath]: baseApi.reducer,
    [authApi.reducerPath]: authApi.reducer,
    user: userSlice,
})

const persistedReducer = persistReducer({
    key: "root",
    storage,
    whitelist: ["user"],
}, rootReducer);

export const setupStore = () => {
    return configureStore({
        reducer: persistedReducer,
        middleware: getDefaultMiddleware =>
            getDefaultMiddleware({
                serializableCheck: {
                    ignoredActions: [FLUSH, REHYDRATE, PAUSE, PERSIST, PURGE, REGISTER]
                }
            }).concat(baseApi.middleware)
                .concat(authApi.middleware)
    })
}

export const store = setupStore()
export const persistor = persistStore(store)
export type RootState = ReturnType<typeof rootReducer>
export type AppStore = ReturnType<typeof setupStore>
export type AppDispatch = AppStore['dispatch']

export const resetAndCleanStore = async () => {
    try {
        await persistor.purge();
    } catch (error) {
        console.log(error)
    }
};