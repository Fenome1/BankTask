import {baseApi} from "./authApi.ts";
import {ApiTag} from "../../common/enums/ApiTag.ts";
import {HttpMethod} from "../../common/enums/HttpMethod.ts";
import {message} from "antd";
import {getErrorMessageFormBaseQuery} from "../hooks/getErrorMessageFormBaseQuery.ts";
import {FetchBaseQueryError} from "@reduxjs/toolkit/query";
import {IRegisterUserCommand} from "../../features/commands/auth/IRegisterUserCommand.ts";

export const userApi = baseApi.injectEndpoints({
    endpoints: (builder) => ({
        regiserUser: builder.mutation<number, IRegisterUserCommand>({
            query: command => ({
                url: `${ApiTag.User}/Register`,
                method: HttpMethod.POST,
                body: command
            }),
            async onQueryStarted(_, {queryFulfilled}) {
                try {
                    await queryFulfilled
                    message.success(`Вы успешно зарегистрированы`, 3)
                } catch (error) {
                    message.error(getErrorMessageFormBaseQuery(error as FetchBaseQueryError), 3)
                }
            }
        }),
    }),
});

export const {
    useRegiserUserMutation,
} = userApi;