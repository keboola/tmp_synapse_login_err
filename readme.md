# Example .NET sysnapse issue 120012822003194

# RUN

```
dotnet restore
dotnet run
# run second time
dotnet run
```

## Example output
```
λ dotnet restore              
  Restore completed in 30.28 ms for /synapse-err.csproj.

# First run OK
λ dotnet run    
CREATE LOGIN MY_USER9 WITH PASSWORD = 'strong4PassWord1'
CREATE USER MY_USER9 FOR LOGIN MY_USER9
CREATE ROLE MY_ROLE9
EXEC sp_addrolemember MY_ROLE9, MY_USER9
Open connection as user MY_USER9
EXEC sp_droprolemember MY_ROLE9, MY_USER9
DROP USER MY_USER9
DROP ROLE MY_ROLE9
DROP LOGIN MY_USER9

# Second run, fail to login as USER
λ dotnet run
CREATE LOGIN MY_USER9 WITH PASSWORD = 'strong4PassWord1'
CREATE USER MY_USER9 FOR LOGIN MY_USER9
CREATE ROLE MY_ROLE9
EXEC sp_addrolemember MY_ROLE9, MY_USER9
Open connection as user MY_USER9
System.Data.SqlClient.SqlException (0x80131904): The server principal "MY_USER9" is not able to access the database "test-synapse" under the current security context.
Cannot open database "test-synapse" requested by the login. The login failed.
Login failed for user 'MY_USER9'.
   at System.Data.SqlClient.SqlInternalConnectionTds..ctor(DbConnectionPoolIdentity identity, SqlConnectionString connectionOptions, SqlCredential credential, Object providerInfo, String newPassword, SecureString newSecurePassword, Boolean redirectedUserInstance, SqlConnectionString userConnectionOptions, SessionData reconnectSessionData, Boolean applyTransientFaultHandling, String accessToken)
   at System.Data.SqlClient.SqlConnectionFactory.CreateConnection(DbConnectionOptions options, DbConnectionPoolKey poolKey, Object poolGroupProviderInfo, DbConnectionPool pool, DbConnection owningConnection, DbConnectionOptions userOptions)
   at System.Data.ProviderBase.DbConnectionFactory.CreatePooledConnection(DbConnectionPool pool, DbConnection owningObject, DbConnectionOptions options, DbConnectionPoolKey poolKey, DbConnectionOptions userOptions)
   at System.Data.ProviderBase.DbConnectionPool.CreateObject(DbConnection owningObject, DbConnectionOptions userOptions, DbConnectionInternal oldConnection)
   at System.Data.ProviderBase.DbConnectionPool.UserCreateRequest(DbConnection owningObject, DbConnectionOptions userOptions, DbConnectionInternal oldConnection)
   at System.Data.ProviderBase.DbConnectionPool.TryGetConnection(DbConnection owningObject, UInt32 waitForMultipleObjectsTimeout, Boolean allowCreate, Boolean onlyOneCheckConnection, DbConnectionOptions userOptions, DbConnectionInternal& connection)
   at System.Data.ProviderBase.DbConnectionPool.TryGetConnection(DbConnection owningObject, TaskCompletionSource`1 retry, DbConnectionOptions userOptions, DbConnectionInternal& connection)
   at System.Data.ProviderBase.DbConnectionFactory.TryGetConnection(DbConnection owningConnection, TaskCompletionSource`1 retry, DbConnectionOptions userOptions, DbConnectionInternal oldConnection, DbConnectionInternal& connection)
   at System.Data.ProviderBase.DbConnectionInternal.TryOpenConnectionInternal(DbConnection outerConnection, DbConnectionFactory connectionFactory, TaskCompletionSource`1 retry, DbConnectionOptions userOptions)
   at System.Data.ProviderBase.DbConnectionClosed.TryOpenConnection(DbConnection outerConnection, DbConnectionFactory connectionFactory, TaskCompletionSource`1 retry, DbConnectionOptions userOptions)
   at System.Data.SqlClient.SqlConnection.TryOpen(TaskCompletionSource`1 retry)
   at System.Data.SqlClient.SqlConnection.Open()
   at sqltest.Program.testLoginAsMyUser() in /home/zajca/Code/php/keboola/synapse-err/Program.cs:line 36
ClientConnectionId:1540c2ea-b7da-4507-a8ab-911ed69c37c9
Error Number:916,State:2,Class:14
EXEC sp_droprolemember MY_ROLE9, MY_USER9
DROP USER MY_USER9
DROP ROLE MY_ROLE9
DROP LOGIN MY_USER9
```
