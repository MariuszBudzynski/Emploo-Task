# Emplo-zadanie

INFO - rozwiazanie zadan 2 i 6 jest bezposrednio w readme reszta reszta zadan zostala podzielona na projekty typu class library oraz zadanie 5 jako XUnit

## Zadanie 2 – Zapytania LINQ

//e => Employee class

a) 
```
var from = new DateTime(2019, 1, 1);
var to = new DateTime(2019, 12, 31);

var employeesNet2019 = context.Employees
    .Where(e => e.Team.Name == ".NET"
        && e.Vacations.Any(v => v.DateSince <= to && v.DateUntil >= from))
    .ToList();
```
b)
```
var year = DateTime.Today.Year;
var today = DateTime.Today;

var result = context.Employees
    .Select(e => new {
        Employee = e,
        DaysUsed = e.Vacations
            .Where(v => v.DateSince.Year == year
                     && v.DateUntil.Year == year
                     && v.DateUntil < today)
            .Sum(v => v.IsPartialVacation 
                ? v.NumberOfHours / 8.0 
                : (v.DateUntil - v.DateSince).Days + 1)
    })
    .ToList();
```
c)
```
var from = new DateTime(2019, 1, 1);
var to = new DateTime(2019, 12, 31);

var teams = context.Teams
    .Where(t => !t.Employees
        .Any(e => e.Vacations.Any(v => v.DateSince <= to && v.DateUntil >= from)))
    .ToList();
```


## Zadanie 6 – Optymalizacja zapytań SQL (z przykładami)

### 1. Użycie `Include()` i `ThenInclude()`

Pozwala na załadowanie powiązanych danych w jednym zapytaniu, co eliminuje problem tzw. "N+1 query".

```csharp
var employees = context.Employees
    .Include(e => e.Vacations)
        .ThenInclude(v => v.VacationPackage)
    .ToList();
```

---

### 2. Projekcja danych (`Select`)

Zamiast pobierać całe encje, warto pobierać tylko potrzebne pola — zmniejsza to ilość przesyłanych danych.

```csharp
var vacationSummaries = context.Vacations
    .Select(v => new {
        v.EmployeeId,
        v.DateSince,
        v.DateUntil
    })
    .ToList();
```

---

### 3. Filtrowanie danych przed załadowaniem

Filtrowanie danych już na poziomie zapytania SQL pozwala ograniczyć ilość danych trafiających do aplikacji.

```csharp
var filteredEmployees = context.Employees
    .Where(e => e.Team.Name == ".NET")
    .ToList();
```

---

### 4. Unikanie zapytań w pętli

Zamiast wykonywać wiele zapytań w pętli, należy pobrać dane zbiorczo.

```csharp
var employees = context.Employees
    .Where(e => employeeIds.Contains(e.Id))
    .ToList();
```

---

### 5. Użycie `AsNoTracking()` dla danych tylko do odczytu

Wyłączenie śledzenia zmian przez Entity Framework przyspiesza zapytania i zmniejsza zużycie pamięci.

```csharp
var readonlyEmployees = context.Employees
    .AsNoTracking()
    .Where(e => e.IsActive)
    .ToList();
```

---

### 6. Batching i paginacja

Przy dużych zbiorach danych warto stosować paginację, aby pobierać tylko potrzebne fragmenty.

```csharp
int pageSize = 50;
int pageNumber = 2;

var pagedEmployees = context.Employees
    .OrderBy(e => e.Id)
    .Skip((pageNumber - 1) * pageSize)
    .Take(pageSize)
    .ToList();
```

---

