import {BaseQueryFn, FetchArgs, fetchBaseQuery, FetchBaseQueryError} from "@reduxjs/toolkit/query";
import {Mutex} from "async-mutex";
import {RootState} from "./store.ts";
import {authApi} from "./apis/authApi.ts";
import {IRefreshUserCommand} from "../features/commands/auth/IRefreshUserCommand.ts";

const mutex = new Mutex()

export const baseQuery = fetchBaseQuery({
    baseUrl: `${import.meta.env.VITE_API_URL}/api`,
    headers: {
        "Accept": "application/json",
        "Content-Type": "application/json",
    },
    mode: "cors",
    prepareHeaders: (headers, {getState}) => {
        const accessToken = (getState() as RootState).user.accessToken ?? null;
        if (accessToken) {
            headers.set("Authorization", `Bearer ${accessToken}`)
        }
        return headers
    }
})

export const fetchQueryWithReauth: BaseQueryFn<
    string | FetchArgs,
    unknown,
    FetchBaseQueryError
> = async (args, api, extraOptions) => {
    await mutex.waitForUnlock()
    let result = await baseQuery(args, api, extraOptions)
    if (result.error && result.error.status === 401) {
        if (!mutex.isLocked()) {
            const release = await mutex.acquire()
            const authState = (api.getState() as RootState).user;

            try {
                const user = authState.user

                if (user) {
                    await api.dispatch(authApi.endpoints.refresh.initiate({
                        accessToken: authState.accessToken,
                        refreshToken: authState.refreshToken
                    } as IRefreshUserCommand))
                }

                result = await baseQuery(args, api, extraOptions)

            } finally {
                await release()
            }
        } else {
            await mutex.waitForUnlock()
            result = await baseQuery(args, api, extraOptions)
        }
    }
    return result
}