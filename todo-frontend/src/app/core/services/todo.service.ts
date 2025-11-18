import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { TodoItemDto, CreateTodoItemDto } from '../models/toDoItem';

@Injectable({
  providedIn: 'root',
})
export class TodoService {
  private readonly apiUrl = `${environment.apiBaseUrl}/api/todos`
  private http = inject(HttpClient)

  // GET /api/todos
  getAll(): Observable<TodoItemDto[]> {
    return this.http.get<TodoItemDto[]>(this.apiUrl);
  }

  // POST /api/todos
  create(dto: CreateTodoItemDto): Observable<TodoItemDto> {
    return this.http.post<TodoItemDto>(this.apiUrl, dto);
  }

  // PUT /api/todos/{id}/toggle-done
  toggleDone(id: number): Observable<TodoItemDto> {
    const url = `${this.apiUrl}/${id}/toggle-done`;
    return this.http.put<TodoItemDto>(url, {});
  }
}
