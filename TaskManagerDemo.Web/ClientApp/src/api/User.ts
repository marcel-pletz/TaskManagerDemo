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

import {UserDto} from "./data-contracts";
import {HttpClient, RequestParams} from "./http-client";

export class User<SecurityDataType = unknown> extends HttpClient<SecurityDataType> {
    /**
     * No description
     *
     * @tags User
     * @name GetAllUsers
     * @request GET:/api/users
     */
    getAllUsers = (params: RequestParams = {}) =>
        this.request<UserDto[], any>({
            path: `/api/users`,
            method: "GET",
            format: "json",
            ...params,
        });
}
