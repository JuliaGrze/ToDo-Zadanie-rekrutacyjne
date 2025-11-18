# ToDo App – zadanie rekrutacyjne (.NET + Angular + PostgreSQL)

## Technologie

- Backend: .NET 8, ASP.NET Core Web API, Entity Framework Core, PostgreSQL
- Frontend: Angular, TypeScript
- Testy: xUnit (backend)

## Struktura repozytorium

```text
backend/TodoApp/TodoApp        # API
backend/TodoApp/TodoApp.Tests  # testy backendu
todo-frontend                  # frontend Angular
```

---

## Jak uruchomić aplikację i jak wszystko się łączy

### 1. Baza danych (PostgreSQL)

1. Uruchom PostgreSQL.
2. Utwórz bazę danych:

   ```sql
   CREATE DATABASE todoapp;
   ```

3. W pliku:

   `backend/TodoApp/TodoApp/appsettings.json`

   ustaw connection string, np.:

   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Host=localhost;Port=5432;Database=todoapp;Username=postgres;Password=TwojeHasloTutaj"
   }
   ```

   U mnie lokalnie hasło to `2004`, u siebie wpisz własne.

---

### 2. Backend (API)

Przejdź do katalogu API:

```bash
cd backend/TodoApp/TodoApp
dotnet restore
dotnet ef database update   # utworzenie tabel na bazie migracji
dotnet run
```
Dostępne endpointy:

- `GET  /api/todos`
- `POST /api/todos`
- `PUT  /api/todos/{id}/toggle-done`

---

### 3. Testy backendu

```bash
cd backend/TodoApp/TodoApp.Tests
dotnet test
```

---

### 4. Frontend (Angular)

Przejdź do katalogu frontendu:

```bash
cd todo-frontend
npm install
```

Uruchom frontend:

```bash
ng serve
```

Aplikacja będzie dostępna pod adresem:

- `http://localhost:4200`

---

### 5. Połączenie frontendu z backendem

- Frontend (Angular) wysyła żądania pod:
  `https://localhost:7234/api/todos`
- Backend ma włączony CORS dla `http://localhost:4200`, więc przeglądarka może łączyć się bezpośrednio z API.

---

### 6. Szybki start 

1. PostgreSQL: baza `todoapp` + connection string w `backend/TodoApp/TodoApp/appsettings.json`.
2. Backend:

   ```bash
   cd backend/TodoApp/TodoApp
   dotnet ef database update
   dotnet run
   ```

3. Frontend:

   ```bash
   cd todo-frontend
   npm install
   ng serve
   ```

4. Wejdź na `http://localhost:4200`.
