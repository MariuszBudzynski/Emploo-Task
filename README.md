# Emplo-zadanie

**Opis To do**

Zadanie 2 – Zapytania LINQ

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

