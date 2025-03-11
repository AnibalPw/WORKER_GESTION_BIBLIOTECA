# WORKER_GESTION_BIBLIOTECA

El proyecto consiste en el desarrollo de un sistema de gestión bibliotecaria que implementa un worker en C# para la automatización de procesos críticos relacionados con el control de préstamos, devoluciones y gestión de inventario de libros.

## Configuración appsetting
 - Renombre el archivo appsetting.example.json por appsetting.json
 - Configurar el ConnectionString del appsetting.json con su conexión de base de datos
```sh
Opción 1:
  "WCS_CONEXION": "Data Source=su_servidor;Database=su_base;User Id=su_usuario;Password=su_contraseña;Encrypt=False;TrustServerCertificate=True;" 
Opción 2:
  "WCS_CONEXION": "Data Source=su_servidor;Initial Catalog=su_base;  integrated security=true;Encrypt=False;TrustServerCertificate=True;"
```

## Restaurar Base de Datos
Para restaurar desde el script SQL:
   - Abrir SSMS y crear una nueva base de datos.
   - Ejecutar el script `BD/WCS_BIBLIOTECA_SCRIPT.sql` en la base de datos creada.

## Ejecute el proyecto
Ejecute el proyecto desde Visual Studio 2022 o VS Code y observe como se modifica la BD (actualmente sólo modifica la información en la primera ejecución)
