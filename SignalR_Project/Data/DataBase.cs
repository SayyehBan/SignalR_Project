using Microsoft.Data.SqlClient;

namespace SignalR_Project.Data;

public class DataBase
{
    public static string ConnectionString()
    {
        SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
        builder.DataSource = ".";
        builder.InitialCatalog = "SignalR_DB";
        builder.UserID = "TestConnection";
        builder.Password = "@123456";
        builder.ConnectTimeout = 0;
        builder.MaxPoolSize = 20000;
        builder.IntegratedSecurity = false;
        builder.TrustServerCertificate = true;

        return builder.ConnectionString;
    }
}
