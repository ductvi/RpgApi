# 🛡️ RpgApi

API REST feita com .NET 9 usando padrão MVC, para gerenciar personagens, armas e usuários num sistema baseado em *Senhor dos Anéis*.  
Testada com Postman e hospedada no Somee.

🚧 Em desenvolvimento 🚧

---

## ⚙️ Tecnologias

![.NET](https://img.shields.io/badge/.NET-9.0-purple?logo=dotnet&logoColor=white)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET_Core-MVC-blue?logo=dotnet&logoColor=white)
![Entity Framework](https://img.shields.io/badge/Entity_Framework-Core-green?logo=dotnet&logoColor=white)
![SQL Server](https://img.shields.io/badge/SQL_Server-Database-red?logo=microsoftsqlserver&logoColor=white)
![Postman](https://img.shields.io/badge/Tested_with-Postman-orange?logo=postman&logoColor=white)
![Somee](https://img.shields.io/badge/Hosted_on-Somee-lightgrey)

---

## 🚀 Funcionalidades

- 🧝‍♂️ CRUD de Personagens
- ⚔️ CRUD de Armas
- 👤 CRUD de Usuários

---

## 🖥️ Rodando localmente

```bash
git clone https://github.com/ductvi/RpgApi.git
cd RpgApi
dotnet restore
dotnet ef database update
dotnet run
API em: https://localhost:5001
