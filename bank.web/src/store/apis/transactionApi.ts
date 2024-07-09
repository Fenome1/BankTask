import {baseApi} from "./authApi.ts";
import {ApiTag} from "../../common/enums/ApiTag.ts";
import {HttpMethod} from "../../common/enums/HttpMethod.ts";
import {notification} from "antd";
import {getErrorMessageFormBaseQuery} from "../hooks/getErrorMessageFormBaseQuery.ts";
import {IExecuteTransactionCommand} from "../../features/commands/transaction/IExecuteTransactionCommand.ts";
import {FetchBaseQueryError} from "@reduxjs/toolkit/query";
import {IPagedList} from "../../features/models/base/IPagedList.ts";
import {ITransaction} from "../../features/models/ITransaction.ts";
import {IListTransactionsByUserQuery} from "../../features/commands/transaction/IListTransactionsByUserQuery.ts";

export const transactionApi = baseApi.injectEndpoints({
    endpoints: (builder) => ({
        getTransactionsByUser: builder.query<IPagedList<ITransaction>, IListTransactionsByUserQuery>({
            query: (params) => ({
                url: `${ApiTag.Transaction}/User`,
                method: 'GET',
                params,
            }),
            providesTags: [{type: ApiTag.Account}],
        }),
        executeTransaction: builder.mutation<number, IExecuteTransactionCommand>({
            query: command => ({
                url: `${ApiTag.Transaction}/Execute`,
                method: HttpMethod.POST,
                body: command
            }),
            invalidatesTags: [{type: ApiTag.Account}],
            async onQueryStarted(_, {queryFulfilled}) {
                try {
                    await queryFulfilled
                } catch (error) {
                    notification.error({
                        message: "Ошибка перевода",
                        description: getErrorMessageFormBaseQuery(error as FetchBaseQueryError),
                    });
                }
            }
        }),
    })
})

export const {
    useGetTransactionsByUserQuery,
    useExecuteTransactionMutation
} = transactionApi;