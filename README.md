Descripción del proyecto:

Categorías de videojuegos (CRUD).

Videojuegos (CRUD) con precio, plataforma, editorial y categoría.

Inventario 1–1 con cada videojuego + movimientos de inventario (ingresos/ventas).

Autenticación con JWT y roles: Admin (gestiona todo) y Vendedor (operaciones autenticadas).

Instalación y ejecución
Requisitos

.NET SDK 8

SQL Server (cualquiera):

Recomendado para local: (localdb)\MSSQLLocalDB

O tu instancia: DESKTOP-XXXX\MSSQLSERVER / SQLEXPRESS

Visual Studio 2022
1) Clonar e instalar paquetes
   2) Configurar la cadena de conexión
En appsettings.json
3) Migraciones de base de datos
4) Ejecutar
Visual Studio: selecciona el perfil http (recomendado) y Ejecutar.
Credenciales de prueba (login)

Usuario administrador creado por el seeder:

Usuario: admin

Contraseña: Admin123*

Rol: Admin

Puedes crear más usuarios con POST /api/auth/register (Define rolId: 1 = Admin, rolId: 2 = Vendedor).
