# ToDo App – zadanie rekrutacyjne (.NET + Angular + PostgreSQL)

Aplikacja ToDo z backendem w ASP.NET Core (.NET 8), frontendem w Angularze i bazą PostgreSQL.

## Technologie

- Backend: .NET 8, ASP.NET Core Web API, Entity Framework Core, PostgreSQL  
- Frontend: Angular 20, TypeScript  
- Testy: xUnit (backend)

## Struktura repozytorium

```text
backend/TodoApp/TodoApp        # API
backend/TodoApp/TodoApp.Tests  # testy backendu
todo-frontend                  # frontend Angular
```

## Uproszczenia

- Foldery `Domain` / `Infrastructure` zamiast osobnych projektów – dla małego zadania ważniejsza była prostota uruchomienia niż pełny podział na projekty.
- Brak osobnej warstwy repozytoriów – serwis korzysta bezpośrednio z `DbContext`; przy jednej encji repozytoria byłyby nadmiarowe.
- Login/hasło w `appsettings.json` – zostawione dla łatwiejszego uruchomienia; w produkcji użyłabym secrets/zmiennych środowiskowych.

## Wymagane wersje narzędzi

- .NET SDK 8.0  
- Node.js ≥ 20.10.0 (u mnie 22.17.1)  
- Angular CLI 20.1.3  
- PostgreSQL (projekt testowany na wersji 18.1)  
- (opcjonalnie) `dotnet-ef` – do migracji z linii komend

## Jak uruchomić aplikację

### 1. Baza danych (PostgreSQL)

1. Uruchom PostgreSQL.  
2. Utwórz bazę:

   ```sql
   CREATE DATABASE todoapp;
   ```

3. W pliku `backend/TodoApp/TodoApp/appsettings.json` ustaw connection string pod siebie, np.:

   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Host=localhost;Port=5432;Database=todoapp;Username=postgres;Password=TwojeHasloTutaj"
   }
   ```

### 2. Backend (API)

```bash
cd backend/TodoApp/TodoApp
dotnet restore
dotnet ef database update   # migracja bazy
dotnet run
```

### 3. Testy backendu

```bash
cd backend/TodoApp/TodoApp.Tests
dotnet test
```

### 4. Frontend (Angular)

```bash
cd todo-frontend
npm install
ng serve
```

Aplikacja: `http://localhost:4200`

Frontend wysyła żądania do API pod adresem:  
`https://localhost:7234/api/todos`

## Szybki start

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
