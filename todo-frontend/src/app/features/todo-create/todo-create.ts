import { Component, inject } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { TodoService } from '../../core/services/todo.service';
import { CreateTodoItemDto } from '../../core/models/toDoItem';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-todo-create',
  imports: [
    RouterLink,
    CommonModule,
    FormsModule
  ],
  templateUrl: './todo-create.html',
  styleUrl: './todo-create.css',
})
export class TodoCreate {
  private toDoService = inject(TodoService)
  private router = inject(Router)

   newTask: CreateTodoItemDto = {
    title: '',
    description: ''
  }

  loading = false
  error: string | null = null

  createNewTask(): void {
    const title = this.newTask.title?.trim()

    if (!title) {
      return
    }

    const dto: CreateTodoItemDto = {
      title,
      description: this.newTask.description?.trim() || null
    }

    this.loading = true;
    this.error = null;

    this.toDoService.create(dto).subscribe({
      next: () => {
        this.loading = false
        this.newTask = { title: '', description: '' }
        this.router.navigate(['/tasks/list'])
      },
      error: (err) => {
        console.error(err)
        this.loading = false
        this.error = 'Nie udało się dodać zadania.'
      }
    });
  }
}
