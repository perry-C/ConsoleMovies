using System.Data;
using System.Data.SqlClient;
using static ConsoleMovies.SqlCommands;
namespace ConsoleMovies
{
    public class Program
    {
        public static void Main()
        {
            DisplayMainMenu();

        }

        private static void DisplayMainMenu()
        {
            Console.Clear();
            Console.WriteLine("Welcome to the Movies & Actors management console");
            Console.WriteLine("=================================================");
            Console.WriteLine();

            // -----------------------------------------------------------------------
            Console.WriteLine("1. CREATE new entries into the database");
            Console.WriteLine("2. READ database information");
            Console.WriteLine("3. UPDATE existing database information");
            Console.WriteLine("4. Delete existing database information");
            // -----------------------------------------------------------------------

            Console.WriteLine("Please select one of the option above to proceed:");
            string optionSelected = Console.ReadLine();
            ProcessOptionSelected(optionSelected);
        }

        private static void ProcessOptionSelected(string optionSelected)
        {

            bool optionNotValid = true;
            Console.WriteLine("Please select your database(Movies, Actors)");
            string db = Console.ReadLine();
            while (optionNotValid)
                switch (optionSelected)
                {
                    case "1":
                        InsertIntoDb(db);
                        optionNotValid = false;
                        break;
                    case "2":
                        ReadFromDb(db);
                        optionNotValid = false;
                        break;

                    case "3":
                        UpdateDb(db);
                        optionNotValid = false;

                        break;
                    case "4":
                        DeleteDb(db);
                        optionNotValid = false;

                        break;
                    default:

                        Console.WriteLine("Option not valid");
                        Console.WriteLine("Please select one of the option above to proceed:");
                        optionSelected = Console.ReadLine();
                        break;
                }

            bool exit = false;
            Console.WriteLine("Operation successful, press 'x' to return to the main menu!");
            while (!exit)
            {

                if (Console.ReadKey(true).Key == ConsoleKey.X)
                {
                    exit = true;
                }

            }
            DisplayMainMenu();
        }

        private static void DeleteDb(string? db)
        {
            using (var conn = new SqlConnection(GetConnectionStringUseStringBuilder()))
            {
                Console.WriteLine($"Input {db} id:");
                string id = Console.ReadLine();


                var cmd = GetDeleteCommand(conn, db, id);
                cmd.Connection.Open();

                cmd.ExecuteNonQuery();

                cmd.Connection.Close();
            }
        }

        private static void UpdateDb(string? db)
        {

            using (var conn = new SqlConnection(GetConnectionStringUseStringBuilder()))
            {
                Console.WriteLine($"Input {db} id:");
                string id = Console.ReadLine();

                Console.WriteLine($"Input column names");
                string[] columnNames = Console.ReadLine().Trim().Split(',');

                Console.WriteLine($"Input column values");
                string[] columnValues = Console.ReadLine().Trim().Split(',');

                var cmd = GetUpdateCommand(conn, db, id, columnNames, columnValues);
                cmd.Connection.Open();

                cmd.ExecuteNonQuery();

                cmd.Connection.Close();
            }
        }

        private static void InsertIntoDb(string db)
        {
            using (var conn = new SqlConnection(GetConnectionStringUseStringBuilder()))
            {
                SqlCommand cmd = null;

                switch (db)
                {
                    case "Movies":
                        Console.WriteLine("Input movie name:");
                        string movieName = Console.ReadLine();
                        Console.WriteLine("Input movie date:");
                        string movieDate = Console.ReadLine();
                        Console.WriteLine("Input movie price:");
                        string moviePrice = Console.ReadLine();

                        cmd = GetInsertCommandMovies(conn, movieName, movieDate, moviePrice);
                        cmd.Connection.Open();

                        cmd.ExecuteNonQuery();
                        cmd.Connection.Close();

                        break;

                    case "Actors":

                        Console.WriteLine("Input actor first name:");
                        string actorFirstName = Console.ReadLine();
                        Console.WriteLine("Input actor last name:");
                        string actorLastName = Console.ReadLine();
                        Console.WriteLine("Input actor country:");
                        string actorCountry = Console.ReadLine();
                        cmd = GetInsertCommandActors(conn, actorFirstName, actorLastName, actorCountry);

                        cmd.Connection.Open();
                        cmd.ExecuteNonQuery();
                        cmd.Connection.Close();

                        break;

                    default:
                        break;
                }
            }
        }

        private static void ReadFromDb(string db)
        {

            static void ReadSingleRow(IDataRecord dataRecord)
            {
                for (int i = 0; i < dataRecord.FieldCount; i++)
                {
                    Console.Write($"{dataRecord[i]}, ");
                }
                Console.WriteLine();

            }


            using (var conn = new SqlConnection(GetConnectionStringUseStringBuilder()))
            {
                SqlCommand cmd = GetSelectCommand(conn, db);


                cmd.Connection.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ReadSingleRow((IDataRecord)reader);
                    }
                }
                cmd.Connection.Close();


            }
        }
    }
}
