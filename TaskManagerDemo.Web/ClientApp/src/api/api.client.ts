import {Todo} from "./Todo.ts";
import {User} from "./User.ts";
import {FakeAuthentication} from "./FakeAuthentication.ts";

export const todoClient = new Todo();
export const userClient = new User();

export const authenticationClient = new FakeAuthentication();