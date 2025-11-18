import { Routes } from '@angular/router';
import { TodoList } from './features/todo-list/todo-list';
import { TodoCreate } from './features/todo-create/todo-create';

export const routes: Routes = [
    {path: 'tasks/list', component: TodoList},
    {path: 'create/task', component: TodoCreate},

    {path: '**', component: TodoList},
];
