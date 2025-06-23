# 📦 Sistema de Gerenciamento de Estoque

Aplicação fullstack para controle de estoque, com autenticação segura, operações CRUD e dashboards interativos.

[🔗 Acesse o Frontend](https://estoquemanager.netlify.app)

---

## 🎯 Objetivo

Oferecer uma solução eficiente para gerenciamento de estoques, com funcionalidades robustas e uma interface moderna, utilizando integração entre backend e frontend.

---

## 🛠️ Stack Tecnológica

### Backend
- ✅ C# / .NET 9
- ✅ PostgreSQL
- ✅ Dapper (ORM)
- ✅ Autenticação JWT
- ✅ Swagger para documentação da API
- ☁️ Hospedagem: Render

### Frontend
- ✅ React + TypeScript
- ✅ Tailwind CSS
- ✅ React Router DOM
- ✅ Context API para autenticação
- ☁️ Hospedagem: Netlify

---

## 🏛️ Arquitetura

### Backend
- Clean Architecture com separação em camadas:
  - **Domain**
  - **Application**
  - **Infrastructure**
  - **API (Presentation)**

### Frontend
- Componentes reutilizáveis
- Gerenciamento de estado e autenticação via Context API
- Navegação fluida com React Router

---

## 🔐 Funcionalidades

- Login seguro com controle de acesso por **roles** (Admin/Usuário)
- CRUD completo de:
  - Produtos
  - Pedidos
  - Movimentações
- Dashboard com gráficos dinâmicos
- Aplicação de descontos diretamente na interface
- Menu lateral com navegação entre funcionalidades

---

## 📊 Dashboard

- Gráficos de produtos por categoria
- Status dos pedidos em tempo real
- Visualização de movimentações recentes

---

## 🚀 Benefícios

- **Escalável:** Backend no Render com deploy automático
- **Responsivo:** UI moderna com Tailwind CSS
- **Seguro:** Autenticação JWT protegendo rotas e dados
- **Flexível:** Alterações de preços diretamente via frontend

---

## 📂 Executando o projeto localmente

### Pré-requisitos
- [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download)
- [Node.js](https://nodejs.org/)
- [PostgreSQL](https://www.postgresql.org/)
- Docker (opcional)

### Backend

```bash
cd Backend
dotnet restore
dotnet run
