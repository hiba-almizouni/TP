# Hotellerie_Hiba — ASP.NET Core MVC 8.0

Application de gestion hôtelière développée avec ASP.NET Core MVC (.NET 8.0)
et Entity Framework Core (approche Code First).

## Technologies

- ASP.NET Core MVC 8.0
- Entity Framework Core 8.0 (Code First)
- SQL Server LocalDB
- Bootstrap 5.3
- jQuery Validation Unobtrusive

## Structure du projet

```
Hotellerie_Hiba/
├── Controllers/
│   ├── HotelsController.cs          # CRUD complet Hotels
│   └── AppreciationsController.cs   # CRUD complet Appreciations
├── Models/
│   └── HotellerieModel/
│       ├── Hotel.cs                 # Modèle Hotel avec annotations
│       ├── Appreciation.cs          # Modèle Appreciation avec annotations
│       └── HotellerieDbContext.cs   # Contexte EF Core
├── Views/
│   ├── Hotels/                      # Index, Create, Edit, Details, Delete
│   ├── Appreciations/               # Index, Create, Edit, Details, Delete
│   └── Shared/
│       ├── _Layout.cshtml
│       └── _ValidationScriptsPartial.cshtml
├── Migrations/
│   ├── 20240312100000_InitialCreate.cs
│   ├── 20240312110000_AjoutTel.cs
│   ├── 20240312120000_AjoutAppreciation.cs
│   ├── 20240312130000_AjoutNote.cs
│   ├── 20240312140000_AjoutPaysHotel.cs
│   └── HotellerieDbContextModelSnapshot.cs
├── appsettings.json                 # Chaîne de connexion
└── Program.cs                       # Configuration des services
```

## Prérequis

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- Visual Studio 2022 (avec SQL Server LocalDB) **ou** SQL Server Express
- Entity Framework Core Tools

## Installation et lancement

### 1. Cloner le dépôt

```bash
git clone https://github.com/Hiba/Hotellerie_Hiba.git
cd Hotellerie_Hiba
```

### 2. Restaurer les packages NuGet

```bash
dotnet restore
```

### 3. Appliquer les migrations (création de la base)

```bash
dotnet ef database update
```

Ou via le Package Manager Console de Visual Studio :

```
PM> Update-Database
```

### 4. Lancer l'application

```bash
dotnet run
```

Accéder à : `http://localhost:5245`

---

## Commandes EF Core utiles

| Commande | Description |
|---|---|
| `Add-Migration NomMigration` | Créer une nouvelle migration |
| `Update-Database` | Appliquer toutes les migrations |
| `Update-Database -Migration NomMigration` | Rollback vers une migration précise |
| `Update-Database -Migration 0` | Supprimer toutes les tables |
| `Get-Migration` | Lister toutes les migrations |

---

## Modèle de données

### Hotel
| Propriété | Type | Contraintes |
|---|---|---|
| Id | int | PK, auto-générée |
| Nom | string | Required, 3-20 chars |
| Etoiles | int | Required, Range(1,5) |
| Ville | string | Required |
| SiteWeb | string | Required, Url |
| Tel | string? | Nullable |
| Pays | string? | Nullable, défaut "Tunisie" |

### Appreciation
| Propriété | Type | Contraintes |
|---|---|---|
| Id | int | PK, auto-générée |
| NomPers | string | Required |
| Commentaire | string | Required, MultilineText |
| Note | int | Required, Range(1,10), défaut 5 |
| HotelId | int | FK vers Hotel |

**Relation** : Hotel (1) ←→ (N) Appreciation

---

*Réalisé par Hiba — TP n°4 ASP.NET Core MVC (.NET 8.0)*
