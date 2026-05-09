# TP5 – RestoManager (Fluent API / Code First)

## Description
Application ASP.NET Core MVC (.NET 8.0) – approche Code First avec Fluent API.  
Gestion de restaurants, propriétaires et avis avec schémas SQL personnalisés.

## Fluent API – Mappings
| Classe C# | Table SQL | Schéma |
|-----------|-----------|--------|
| Proprietaire | TProprietaire | resto |
| Restaurant | TRestaurant | resto |
| Avis | TAvis | admin |

## Migrations Code First
```
Add-Migration InitialCreate
Update-Database
# Après ajout de Avis :
Add-Migration AddAvis
Update-Database
```

## Jointures (section G)
- `AvisParRestaurant` : navigation property (Include)
- `AvisDuResto(codeResto)` : requête LINQ avec filtre
- `RestosTopNotes` : LINQ groupBy + Average >= 3.5
