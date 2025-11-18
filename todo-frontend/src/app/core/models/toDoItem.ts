export interface TodoItemDto {
  id: number
  title: string
  description?: string | null
  isDone: boolean
}

export interface CreateTodoItemDto {
  title: string
  description?: string | null
}
