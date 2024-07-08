import {baseApi} from "./authApi.ts";
import {IAccount} from "../../features/models/IAccount.ts";
import {ApiTag} from "../../common/enums/ApiTag.ts";
import {HttpMethod} from "../../common/enums/HttpMethod.ts";
import {message} from "antd";
import {getErrorMessageFormBaseQuery} from "../hooks/getErrorMessageFormBaseQuery.ts";
import {FetchBaseQueryError} from "@reduxjs/toolkit/query";
import {ICreateAccountCommand} from "../../features/commands/account/ICreateAccountCommand.ts";

export const accountApi = baseApi.injectEndpoints({
    endpoints: (builder) => ({
        getAccountsByUser: builder.query<IAccount[], number>({
            query: query => ({
                url: `${ApiTag.Account}/${ApiTag.User}/${query}`,
                method: HttpMethod.GET,
            }),
            providesTags: [{type: ApiTag.Account}],
        }),
        createAccount: builder.mutation<number, ICreateAccountCommand>({
            query: command => ({
                url: `${ApiTag.Account}/Create`,
                method: HttpMethod.POST,
                body: command
            }),
            invalidatesTags: [{type: ApiTag.Account}],
            async onQueryStarted(_, {queryFulfilled}) {
                try {
                    await queryFulfilled
                } catch (error) {
                    message.error(getErrorMessageFormBaseQuery(error as FetchBaseQueryError), 3)
                }
            }
        }),
        deleteAccount: builder.mutation<number, number>({
            query: id => ({
                url: `${ApiTag.Account}/Delete/${id}`,
                method: HttpMethod.DELETE,
            }),
            async onQueryStarted(_, {queryFulfilled}) {
                try {
                    await queryFulfilled
                    message.success(`Счет успешно удален`, 3)
                } catch (error) {
                    message.error(getErrorMessageFormBaseQuery(error as FetchBaseQueryError), 3)
                }
            },
            invalidatesTags: [{type: ApiTag.Account}],
        }),
    })
})

export const {
    useDeleteAccountMutation,
    useCreateAccountMutation,
    useGetAccountsByUserQuery,
} = accountApi;