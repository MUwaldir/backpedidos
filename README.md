
RESUMEN: 
La API que he desarrollado en C# junto con PostgreSQL como base de datos consta de cuatro modelos principales: usuarios, productos, pedidos y detalles de pedidos. A continuación, se describen las funcionalidades clave de esta API:

Usuarios:

La API ofrece un método POST que permite a los usuarios registrarse en la plataforma. La información proporcionada se almacena en la base de datos con el password encriptado.
También se proporciona un método de inicio de sesión que genera un token de autenticación para que los usuarios puedan acceder a la aplicación de manera segura.
Productos:

Para los administradores, la API ofrece métodos POST, PUT y DELETE para gestionar los productos en la plataforma.
Además, existe un método GET que permite a tanto a usuarios como a administradores obtener información sobre los productos disponibles.
Pedidos:

Cuando un usuario realiza un pedido, la API guarda información sobre el pedido en la tabla "pedidos" y sus detalles correspondientes en la tabla "detallepedido".
Los usuarios pueden acceder a sus propios pedidos mediante métodos dedicados, lo que les permite ver el historial de sus compras.
Los administradores tienen acceso a todos los pedidos realizados en la plataforma, lo que facilita la gestión de los registros de ventas y el seguimiento de los pedidos de los clientes.

"Además de las funcionalidades mencionadas anteriormente, es importante destacar que, debido a restricciones de tiempo, no puede corregir las autenticaciones por usuario y amdinistrador pero si genran token validos para lso usuarios".

"Aclarar tambien que solo se crean con rol usuario"
--------------------

Paso 1: Clonar el Repositorio desde GitHub
Abre tu proyecto de API REST en Visual Studio Code.
-------------------
Paso 2:
 cambair a la rama master
 ejecutar: git checkout master
 -------------------
Paso 3: Configurar el Entorno

Asegúrate de tener instalado Visual Studio (o Visual Studio Code) y .NET SDK en la máquina donde vas a ejecutar la API.

Configura tu cadena de conexión a la base de datos PostgreSQL en la máquina local. Abre el archivo de configuración de la API (por lo general, appsettings.json o appsettings.Development.json) y ajusta la cadena de conexión para que apunte a tu base de datos PostgreSQL local.

 "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=namedatabase;Username=postgres;Password=password;"
  },

  Paso 4:
  ejecutar : dotnet restore

  Paso 5:
  Ejecutar : dotnet ef database update

  Paso 6:
  Ejecutar : dotnet run