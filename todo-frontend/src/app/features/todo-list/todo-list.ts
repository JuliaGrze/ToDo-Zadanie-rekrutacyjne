import { Component, inject, OnInit } from '@angular/core';
import { TodoItemDto } from '../../core/models/toDoItem';
import { TodoService } from '../../core/services/todo.service';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-todo-list',
  imports: [
    RouterLink
  ],
  templateUrl: './todo-list.html',
  styleUrl: './todo-list.css',
})
export class TodoList implements OnInit {
  private toDoService = inject(TodoService)

  todos: TodoItemDto[] = []

  loading = false
  error: string | null = null

  ngOnInit(): void {
    this.loadAllToDoesTasks()
  }

  loadAllToDoesTasks(){
    this.loading = true
    this.error = null

    this.toDoService.getAll().subscribe({
      next: tasks => {
        this.todos = tasks
        this.loading = false
      },
      error: err => {
        this.error = "Nie udało się pobrać zadań!"
        this.loading = false
      }
    })
  }

  onToggleDone(toDo: TodoItemDto){
    this.toDoService.toggleDone(toDo.id).subscribe({
      next: updated => {
        // const idx = this.todos.findIndex(t => t.id === updated.id)
        // if (idx !== -1) {
        //   this.todos[idx] = updated
        // }
        this.loadAllToDoesTasks()
      },
      error: err => {
        console.error(err);
        this.error = 'Nie udało się zaktualizować zadania.'
      }
    })
  }
}
