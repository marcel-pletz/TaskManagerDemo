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

import {TodoCommandDto, TodoDto, TodoListDto} from "./data-contracts";
import {ContentType, HttpClient, RequestParams} from "./http-client";

export class Todo<SecurityDataType = unknown> extends HttpClient<SecurityDataType> {
    /**
     * No description
     *
     * @tags Todo
     * @name ListAllTodosOfUser
     * @request GET:/api/todos
     */
    listAllTodosOfUser = (params: RequestParams = {}) =>
        this.request<TodoListDto, any>({
            path: `/api/todos`,
            method: "GET",
            format: "json",
            ...params,
        });
    /**
     * No description
     *
     * @tags Todo
     * @name CreateTodo
     * @request POST:/api/todos
     */
    createTodo = (data: TodoCommandDto, params: RequestParams = {}) =>
        this.request<void, any>({
            path: `/api/todos`,
            method: "POST",
            body: data,
            type: ContentType.Json,
            ...params,
        });
    /**
     * No description
     *
     * @tags Todo
     * @name GetTodo
     * @request GET:/api/todos/{id}
     */
    getTodo = (id: string, params: RequestParams = {}) =>
        this.request<TodoDto, any>({
            path: `/api/todos/${id}`,
            method: "GET",
            format: "json",
            ...params,
        });
    /**
     * No description
     *
     * @tags Todo
     * @name UpdateTodo
     * @request PUT:/api/todos/{id}
     */
    updateTodo = (id: string, data: TodoCommandDto, params: RequestParams = {}) =>
        this.request<void, any>({
            path: `/api/todos/${id}`,
            method: "PUT",
            body: data,
            type: ContentType.Json,
            ...params,
        });
    /**
     * No description
     *
     * @tags Todo
     * @name Delete
     * @request DELETE:/api/todos/{id}
     */
    delete = (id: string, params: RequestParams = {}) =>
        this.request<void, any>({
            path: `/api/todos/${id}`,
            method: "DELETE",
            ...params,
        });
    /**
     * No description
     *
     * @tags Todo
     * @name StartProgress
     * @request POST:/api/todos/{id}/start-progress
     */
    startProgress = (id: string, params: RequestParams = {}) =>
        this.request<void, any>({
            path: `/api/todos/${id}/start-progress`,
            method: "POST",
            ...params,
        });
    /**
     * No description
     *
     * @tags Todo
     * @name Finish
     * @request POST:/api/todos/{id}/finish
     */
    finish = (id: string, params: RequestParams = {}) =>
        this.request<void, any>({
            path: `/api/todos/${id}/finish`,
            method: "POST",
            ...params,
        });
}
