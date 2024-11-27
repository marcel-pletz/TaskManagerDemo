<script setup lang="ts">

import {ErrorMessage, useForm} from "vee-validate";
import {coerce, object, string} from "zod";
import {toTypedSchema} from "@vee-validate/zod";
import {Status, TodoCommandDto, TodoDto} from "../api/data-contracts.ts";

const props = defineProps<{
  todo?: TodoDto
  submitFn: (todo: TodoCommandDto) => Promise<void>
}>();

const validationSchema = toTypedSchema(object({
  title: string().min(1).max(100),
  description: string().min(1).max(1000),
  dueDate: coerce.date(),
}));

const {errors, defineField, handleSubmit} = useForm<TodoCommandDto>(({
  validationSchema: validationSchema,
  initialValues: props.todo
}));


const submitTodo = handleSubmit(async todo => {
  await props.submitFn(todo);
});

const [title, titleAttrs] = defineField("title");
const [description, descriptionAttrs] = defineField("description");
const [dueDate, dueDateAttrs] = defineField("dueDate");
</script>

<template>
  <form @submit="submitTodo">
    <label class="form-label">Titel</label>
    <input type="text"
           name="title"
           v-model="title"
           v-bind="titleAttrs"
           class="form-control"
           :class="{'is-invalid': !!errors.title}"/>
    <ErrorMessage name="title" class="invalid-feedback"/>

    <textarea name="description"
              v-model="description"
              v-bind="descriptionAttrs"
              title="description"
              class="form-control"
              :class="{'is-invalid': !!errors.description}"/>
    <ErrorMessage name="description" class="invalid-feedback"/>

    <input type="date"
           name="dueDate"
           v-model="dueDate"
           v-bind="dueDateAttrs"
           title="dueDate"
           class="form-control"
           :class="{'is-invalid': !!errors.dueDate}"
           :disabled="todo?.status != undefined && todo.status != Status.Todo"/>
    <ErrorMessage name="dueDate" class="invalid-feedback"/>

    <button class="btn btn-primary" type="submit">Speichern</button>
  </form>
</template>

<style scoped>
.form-control, button {
  margin-top: 0.5rem;
}
</style>