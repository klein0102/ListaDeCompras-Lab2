# Lista de Compras – Laboratorio 2  
**Proyecto:** Skill de Alexa + Web API .NET (Arquitectura Limpia)  
**Autor:** Delmarck Klein Jackson  
**Universidad Internacional de Las Americas**  
**Curso:** Programación IV  
**Profesor:** Santiago Astorga  
**Fecha:** Octubre 2025  

---

## Descripción general
Este proyecto implementa una **skill personalizada para Alexa** conectada a un **backend desarrollado en .NET 8 Web API**, utilizando una **arquitectura limpia por capas (BC, BW, DA)** y **Entity Framework Core** para la persistencia de datos en SQL Server.

La skill permite a los usuarios crear, listar, consultar y eliminar listas de compras mediante comandos de voz.

---

## Arquitectura de la solución
ListaDeCompras.API → Capa de presentación / API / controladores REST y Alexa
ListaDeCompras.BC → Entidades de negocio (ListaCompra, ItemLista)
ListaDeCompras.BW → Lógica de negocio (Casos de uso, interfaces)
ListaDeCompras.DA → Acceso a datos (DbContext, Repositorios EF Core)
db/ → Script SQL o migraciones EF Core

---

**Controladores principales**
- `ListasController.cs` → CRUD clásico (GET, POST, DELETE)
- `AlexaController.cs` → Endpoint para la skill Alexa (`/api/alexa`)

---

## Requisitos previos

| Herramienta | Versión recomendada |
|--------------|--------------------|
| .NET SDK     | 8.0 o superior |
| SQL Server   | 2019 – 2022 |
| Visual Studio 2022 | Con workload de ASP.NET y desarrollo .NET Core |
| Ngrok        | Última versión |
| Alexa Developer | https://developer.amazon.com/alexa/console/ask |

---

## Configuración del proyecto

1. **Clonar o descargar el repositorio**
   ```bash
   git clone https://github.com/klein0102/ListaDeCompras-Lab2.git
   cd ListaDeCompras-Lab2


