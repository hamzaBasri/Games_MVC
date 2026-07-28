# 🎮 Games_MVC

Application web de gestion et vente de jeux vidéo, développée avec 
ASP.NET Core MVC selon une architecture en couches.

## Fonctionnalités

- Catalogue de jeux avec prix, producteur, et détails
- Authentification et gestion des utilisateurs (ASP.NET Identity)
- Espaces séparés **Admin** et **Customer** (Areas)
- Architecture Repository + Unit of Work pour l'accès aux données

## Stack technique

- **.NET 9** / ASP.NET Core MVC
- **Entity Framework Core 9** (SQL Server)
- **ASP.NET Core Identity** pour l'authentification
- HTML, CSS, JavaScript (Bootstrap)

## Architecture du projet

Games_MVC/
├── Games.DataAccess/ → Accès aux données (Repository, DbContext)
├── Games.Models/ → Entités et modèles de données
├── Games.Utility/ → Classes utilitaires partagées
└── GamesWeb/ → Application web (Controllers, Views, Areas)
├── Areas/Admin/ → Gestion des jeux et catégories
├── Areas/Customer/ → Expérience client
└── Areas/Identity/ → Authentification
