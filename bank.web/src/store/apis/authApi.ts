import {baseQuery, fetchQueryWithReauth} from "../fetchBaseQueryWithReauth.ts";
import {ILoginUserResponse} from "../../features/responces/ILoginUserRespose.ts";
import {ILoginUserCommand} from "../../features/commands/auth/ILoginUserCommand.ts";
import {ApiTag} from "../../common/enums/ApiTag.ts";
import {HttpMethod} from "../../common/enums/HttpMethod.ts";
import {login} from "../slices/userSlice.ts";
import {message} from "antd";
import {getErrorMessageFormBaseQuery} from "../hooks/getErrorMessageFormBaseQuery.ts";
import {ILogoutUserCommand} from "../../features/commands/auth/ILogoutUserCommand.ts";
import {IRefreshUserCommand} from "../../features/commands/auth/IRefreshUserCommand.ts";
import {resetAndCleanStore, RootState} from "../store.ts";
import {createApi, FetchBaseQueryError} from "@reduxjs/toolkit/query/react";

export const baseApi = createApi({
    reducerPath: "baseApi",
    baseQuery: fetchQueryWithReauth,
    tagTypes: Object.values(ApiTag),
    refetchOnReconnect: true,
    refetchOnFocus: true,
    keepUnusedDataFor: 0,
    endpoints: () => ({})
})

export const authApi = createApi({
    reducerPath: "authApi",
    baseQuery: baseQuery,
    refetchOnReconnect: true,
    refetchOnFocus: true,
    keepUnusedDataFor: 0,
    endpoints: (builder) => ({
        login: builder.mutation<ILoginUserResponse, ILoginUserCommand>({
            query: command => ({
                url: `${ApiTag.Auth}/Login`,
                method: HttpMethod.POST,
                body: command
            }),
            async onQueryStarted(_, {dispatch, queryFulfilled}) {
                try {
                    const {data} = await queryFulfilled
                    await dispatch(login(data));
                    message.success("Добро пожаловать", 3)
                } catch (error) {
                    message.error(getErrorMessageFormBaseQuery(error as FetchBaseQueryError), 3)
                }
            }
        }),
        logout: builder.mutation<void, ILogoutUserCommand>({
            query: command => ({
                url: `${ApiTag.Auth}/Logout`,
                method: HttpMethod.POST,
                body: command,
            }),
            async onQueryStarted(_, {queryFulfilled}) {
                await resetAndCleanStore()
                try {
                    await queryFulfilled
                } catch (error) {
                    console.log(error)
                }
            }
        }),
        refresh: builder.mutation<ILoginUserResponse, IRefreshUserCommand>({
            queryFn: async (command, api, extraOptions) => {
                const response = await baseQuery({
                    url: `${ApiTag.Auth}/Refresh`,
                    method: HttpMethod.POST,
                    body: command,
                }, api, extraOptions)
                if (response.data) {
                    const result = response.data as ILoginUserResponse
                    await api.dispatch(login(result))
                    return {data: result}
                }
                const authState = (api.getState() as RootState).user;
                await api.dispatch(authApi.endpoints.logout.initiate({
                    accessToken: authState.accessToken,
                    refreshToken: authState.refreshToken
                } as ILogoutUserCommand))
                return {error: response.error as FetchBaseQueryError}
            },
        })
    })
})

export const {
    useLoginMutation,
    useLogoutMutation,
} = authApi;