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

export interface AuthenticatedUserDto {
    userName?: string;
    role?: Role;
}

export interface AuthenticationDto {
    isAuthenticated?: boolean;
    user?: AuthenticatedUserDto;
}

export enum Role {
    User = "User",
    Administrator = "Administrator",
}

export enum Status {
    Todo = "Todo",
    InProgress = "InProgress",
    Finished = "Finished",
}

export interface TodoCommandDto {
    title: string;
    description: string;
    /** @format date */
    dueDate?: string | null;
}

export interface TodoDto {
    /** @format uuid */
    id: string;
    title: string;
    description: string;
    status: Status;
    /** @format date */
    dueDate?: string | null;
}

export interface TodoEntryDto {
    /** @format uuid */
    id: string;
    title: string;
    status: Status;
    /** @format date */
    dueDate?: string | null;
}

export interface TodoListDto {
    entries?: TodoEntryDto[];
}

export interface UserDto {
    /** @format uuid */
    id?: string;
    userName?: string;
    email?: string;
    role?: Role;
}
