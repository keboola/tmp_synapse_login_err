using System;
using System.Data.SqlClient;
using System.Text;
using System.Threading;

namespace sqltest {
    class Program {
        public const String Database = "<db>";
        public const String DataSource = "<host>";
        public const String UserPID = "<user>";
        public const String Password = "<pass>";

        public const String CreateUserName = "MY_USER9";
        public const String CreateUserPassword = "strong4PassWord1";
        public const String CreateRole = "MY_ROLE9";

        static void Main (string[] args) {
            Program.testCreateLoginMyUser ();
            Program.testCreateMyUser ();
            Program.testLoginAsMyUser ();

            Program.testDropMyUser ();
            Program.testDropMyUserLogin ();
        }

        static void testLoginAsMyUser () {
            try {
                SqlConnectionStringBuilder builder = Program.getStringBuilder (
                    Program.CreateUserName,
                    Program.CreateUserPassword,
                    Program.Database
                );

                using (SqlConnection connection = new SqlConnection (builder.ConnectionString)) {
                    Console.WriteLine ($"Open connection as user {Program.CreateUserName}");
                    connection.Open ();
                    Thread.Sleep(2000);
                    connection.Close ();
                }

            } catch (SqlException e) {
                Console.WriteLine (e.ToString ());
            }
        }


        static void testCreateMyUser () {
            try {
                SqlConnectionStringBuilder builder = Program.getStringBuilder (
                    Program.UserPID,
                    Program.Password,
                    Program.Database
                );

                using (SqlConnection connection = new SqlConnection (builder.ConnectionString)) {
                    connection.Open ();
                    String sql = $"CREATE USER {Program.CreateUserName} FOR LOGIN {Program.CreateUserName}";
                    Program.executeSql (connection, sql);
                    sql = $"CREATE ROLE {Program.CreateRole}";
                    Program.executeSql (connection, sql);
                    sql = $"EXEC sp_addrolemember {Program.CreateRole}, {Program.CreateUserName}";
                    Program.executeSql (connection, sql);
                    connection.Close ();
                }
            } catch (SqlException e) {
                Console.WriteLine (e.ToString ());
            }
        }


        static void testCreateLoginMyUser () {
            try {
                SqlConnectionStringBuilder builder = Program.getStringBuilder (
                    Program.UserPID,
                    Program.Password,
                    "master"
                );

                using (SqlConnection connection = new SqlConnection (builder.ConnectionString)) {
                    connection.Open ();
                    String sql = $"CREATE LOGIN {Program.CreateUserName} WITH PASSWORD = '{Program.CreateUserPassword}'";
                    Program.executeSql (connection, sql);
                    connection.Close ();
                }

            } catch (SqlException e) {
                Console.WriteLine (e.ToString ());
            }

        }

        static void testDropMyUserLogin () {
            try {
                SqlConnectionStringBuilder builder = Program.getStringBuilder (
                    Program.UserPID,
                    Program.Password,
                    "master"
                );

                using (SqlConnection connection = new SqlConnection (builder.ConnectionString)) {
                    connection.Open ();
                    String sql = $"DROP LOGIN {Program.CreateUserName}";
                    Program.executeSql (connection, sql);
                    connection.Close ();
                }
            } catch (SqlException e) {
                Console.WriteLine (e.ToString ());
            }

        }

       static void testDropMyUser () {
            try {
                SqlConnectionStringBuilder builder = Program.getStringBuilder (
                    Program.UserPID,
                    Program.Password,
                    Program.Database
                );
                using (SqlConnection connection = new SqlConnection (builder.ConnectionString)) {
                    connection.Open ();
                    String sql = $"EXEC sp_droprolemember {Program.CreateRole}, {Program.CreateUserName}";
                    Program.executeSql (connection, sql);
                    sql = $"DROP USER {Program.CreateUserName}";
                    Program.executeSql (connection, sql);
                    sql = $"DROP ROLE {Program.CreateRole}";
                    Program.executeSql (connection, sql);
                    connection.Close ();
                }
            } catch (SqlException e) {
                Console.WriteLine (e.ToString ());
            }

        }

        static void executeSql (SqlConnection connection, String sql) {
            Console.WriteLine (sql);
            using (SqlCommand command = new SqlCommand (sql, connection)) {
                command.ExecuteNonQuery ();
            }
        }

        static SqlConnectionStringBuilder getStringBuilder (string user, string password, string database) {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder ();

            builder.DataSource = Program.DataSource;
            builder.UserID = user;
            builder.Password = password;
            builder.InitialCatalog = database;
            // builder.InitialSecurityInfo = false;
            builder.PersistSecurityInfo = false;
            builder.MultipleActiveResultSets = false;
            builder.Encrypt = true;
            builder.TrustServerCertificate = false;
            // builder.ConnectionTimeout = 30;

            return builder;
        }
    }
}