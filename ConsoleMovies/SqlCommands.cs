using System.Data.SqlClient;


namespace ConsoleMovies
{
    internal class SqlCommands
    {
        public static SqlCommand GetInsertCommandMovies(SqlConnection conn, string movieName, string movieReleaseDate, string moviePrice)
        {
            string tableName = "Movies";
            List<string> columnNames = ["title", "release_date", "price"];

            string mn = $"'{movieName}'";
            string mrd = $"'{DateTime.Now}'";
            string mp = $"{moviePrice}";

            List<string> columnValues = [mn, mrd, mp];
            var sql = $"""
                INSERT INTO {tableName} ({string.Join(',', columnNames)})
                VALUES ({string.Join(',', columnValues)});
            """;
            Console.WriteLine(sql);

            var cmd = new SqlCommand(sql, conn);
            return cmd;
        }

        public static SqlCommand GetInsertCommandActors(SqlConnection conn, string firstName, string lastName, string country)
        {
            string tableName = "Actors";
            List<string> columnNames = ["first_name", "last_name", "country"];

            string fn = $"'{firstName}'";
            string ln = $"'{lastName}'";
            string c = $"'{country}'";

            List<string> columnValues = [fn, ln, c];
            var sql = $"""
                INSERT INTO {tableName} ({string.Join(',', columnNames)})
                VALUES ({string.Join(',', columnValues)});
            """;
            Console.WriteLine(sql);

            var cmd = new SqlCommand(sql, conn);
            return cmd;
        }

        public static SqlCommand GetSelectCommand(SqlConnection conn, string db)
        {
            var sql = $"SELECT * FROM {db}";

            var cmd = new SqlCommand(sql, conn);
            return cmd;
        }

        public static SqlCommand GetUpdateCommand(SqlConnection conn, string db, string id, string[] columnNames, string[] columnValues)
        {
            string s = string.Join(',', columnNames.Zip(columnValues, (n, v) => n + " = " + $"'{v}'"));


            var sql = $"""
            UPDATE {db}
            SET {s}
            WHERE id = {id};
            """;


            var cmd = new SqlCommand(sql, conn);
            return cmd;
        }

        public static SqlCommand GetDeleteCommand(SqlConnection conn, string db, string id)
        {


            var sql = $"""
            DELETE FROM {db} WHERE id={id}
            """;


            var cmd = new SqlCommand(sql, conn);
            return cmd;
        }


        public static string GetConnectionStringUseStringBuilder()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

            builder.DataSource = "127.0.0.1, 1433";
            builder.InitialCatalog = "MovieStore";
            builder.TrustServerCertificate = true;
            builder.UserID = "SA";
            builder.Password = "200816Qq@";

            return builder.ConnectionString;
        }
    }
}
