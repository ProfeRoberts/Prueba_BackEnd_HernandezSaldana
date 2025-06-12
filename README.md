Esta es mi prueba de Back End. Mi nombre es Luis Roberto Hernandez Saldaña.

Segui los pasos e realice una API RESTful desarrollada en ASP.NET Core con Entity Framework que gestiona logins/logouts de usuarios, genera reportes y permite exportar un CSV con horas trabajadas.

Para la conexion a la base de datos se debe entrar a Azure Data Studio, dar click en new connection, e ingresar los siguientes datos:
Servidor: localhost
Puerto: 1433
Usuario: sa
Contraseña: YourStrong!Passw0rd

La tabla de ccRIACat_Areas incluye las columnas:
IDArea
AreaName
StatusArea
CreateDate

La tabla de ccRIACat_Areas incluye las columnas:

La tabla de ccUsers incluye las columnas:
User_id
Password
ApellidoMaterno
ApellidoPaterno
IDArea
LastLoginAttempt
Login
Nombres
Status
TipoUser_id
fCreate

La tabla de cclogLogin incluye las columnas:
Id
User_id
Extension
TipoMov
Fecha


Los Endpoints disponibles son:
GET /logins – Listar todos los registros
POST /logins – Crear nuevo login/logout
PUT /logins/{id} – Actualizar registro existente
DELETE /logins/{id} – Eliminar registro
GET /csv – Generar y descargar reporte en CSV

Para descargar el CSV generado
1. Ejecuta el proyecto.
2. Visita: https://localhost:5001/csv
3. El navegador descargará el archivo ReporteHoras.csv.

Si se usa Postman o curl se puede usar el siguiente comando
curl -X GET https://localhost:5001/csv --output ReporteHoras.csv

La declaracion de los objetos para crear las tablas en la base de datos se encuentran en la carpeta Data.

--------------------------------------------------------------------------------------------------

Para levantar el Servidor SQL:

1. Asegúrate de que Docker Desktop esté abierto y diga “Docker is running” o “Docker está en ejecución”.

2. Abrir la CMD

3. Ejecutar el siguiente comando
docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=YourStrong!Passw0rd' -p 1433:1433 --name sqlserver -d mcr.microsoft.com/mssql/server:2019-latest

4. Con el siguiente comando podemos verificar que el servidor este corriendo:
docker ps

Deberia aparecer una línea con el contenedor `sqlserver` y el puerto `1433->1433`.

5. Conectarse a Azure Data Studio con la informacion al principio del documento
