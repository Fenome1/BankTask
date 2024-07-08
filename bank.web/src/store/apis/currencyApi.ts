import {baseApi} from "./authApi.ts";
import {ApiTag} from "../../common/enums/ApiTag.ts";
import {HttpMethod} from "../../common/enums/HttpMethod.ts";
import {ICurrency} from "../../features/models/ICurrency.ts";

export const currencyApi = baseApi.injectEndpoints({
    endpoints: (builder) => ({
        getCurrencies: builder.query<ICurrency[], void>({
            query: () => ({
                url: `${ApiTag.Currency}`,
                method: HttpMethod.GET,
            })
        }),
    })
})

export const {
    useGetCurrenciesQuery,
} = currencyApi;