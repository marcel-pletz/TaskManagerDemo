/* eslint-disable */
/* tslint:disable */
/*
 * ---------------------------------------------------------------
 * ## THIS FILE WAS GENERATED VIA SWAGGER-TYPESCRIPT-API        ##
 * ##                                                           ##
 * ## AUTHOR: acacode                                           ##
 * ## SOURCE: https://github.com/acacode/swagger-typescript-api ##
 * ---------------------------------------------------------------
 */

import {AuthenticationDto} from "./data-contracts";
import {HttpClient, RequestParams} from "./http-client";

export class FakeAuthentication<SecurityDataType = unknown> extends HttpClient<SecurityDataType> {
    /**
     * No description
     *
     * @tags FakeAuthentication
     * @name GetCurrentUser
     * @request GET:/api/auth/current-user
     */
    getCurrentUser = (params: RequestParams = {}) =>
        this.request<AuthenticationDto, any>({
            path: `/api/auth/current-user`,
            method: "GET",
            format: "json",
            ...params,
        });
    /**
     * No description
     *
     * @tags FakeAuthentication
     * @name Login
     * @request POST:/api/auth/login
     */
    login = (
        query?: {
            username?: string;
        },
        params: RequestParams = {},
    ) =>
        this.request<void, any>({
            path: `/api/auth/login`,
            method: "POST",
            query: query,
            ...params,
        });
    /**
     * No description
     *
     * @tags FakeAuthentication
     * @name Logout
     * @request POST:/api/auth/logout
     */
    logout = (params: RequestParams = {}) =>
        this.request<void, any>({
            path: `/api/auth/logout`,
            method: "POST",
            ...params,
        });
}
