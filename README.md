# PawfectMatch Backend (ASP.NET Core API)

This is the **backend service** for PawfectMatch.  
It provides APIs, authentication, and business logic for both the **Web App** and **Admin Desktop App**.

---

## Features
- **Authentication & Authorization**
- **Adoption Workflow**
  - Pet Owner → Adopter request flow
  - Adopter approves/rejects
  - Admin can override adopter decisions
- **Database Integration**
  - Supabase (Postgres) for accounts, pets, and adoption records
  - Supabase storage for pet photos
- **Secure Communication**
  - All client communication via HTTPS
  - Backend connects securely to Supabase

---

## Tech Stack
- ASP.NET Core (C#)
- REST APIs
- Supabase (Managed PostgreSQL + Storage)
