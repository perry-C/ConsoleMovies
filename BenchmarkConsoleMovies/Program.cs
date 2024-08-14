using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System.Data.SqlClient;
using System.Text;


[MemoryDiagnoser]
public class Program
{
    private static int iterations = 10_000;

    private static void Main(string[] args)
    {
        var summary = BenchmarkRunner.Run<Program>();
    }
    // Benchmarking functions can not be static
    // Benchmarking functions can not have parameters
    // Benchmarking functions must be public 

    [Benchmark]
    public void connectUseStrings()
    {


        string dataSource = "127.0.0.1, 1433";
        string initialCatalog = "MovieStore";
        bool trustServerCertificate = true;
        string userID = "SA";
        string password = Environment.GetEnvironmentVariable("DbPwd");




        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = $"Data Source={dataSource};" +
                                $"Initial Catalog={initialCatalog};" +
                                $"TrustServerCertificate={trustServerCertificate};" +
                                $"User id={userID};" +
                                $"Password={password};";
        conn.Open();
        conn.Close();

    }

    [Benchmark]
    public void GetConnectionStringUseStringBuilder()
    {
        SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

        builder.DataSource = "127.0.0.1, 1433";
        builder.InitialCatalog = "MovieStore";
        builder.TrustServerCertificate = true;
        builder.UserID = "SA";
        builder.Password = "200816Qq@";
        SqlConnection conn = new SqlConnection(builder.ToString());

        conn.Open();
        conn.Close();
    }
}