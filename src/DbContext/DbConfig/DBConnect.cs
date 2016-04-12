using System;
using System.Configuration;
using Mimo.Dbcontext.DBloginservice;
using Mimo.Dbcontext.DbConfig.Model;

namespace Mimo.Dbcontext.DbConfig
{
    public class DBConnect
    {
        public static String ConnectionStringAdapter
        {
            get
            {
                return GetConnectionString();
            }
        }

        private static string GetWebserviceDatabaseConfig()
        {
            var webDBconfigService = new WebDBConfigService();
            var dbAccount = webDBconfigService.WS_AUTHENDB_DBConfig(DBConfig.ProjectID);

            if (String.IsNullOrEmpty(dbAccount.Status) || dbAccount.Status != "0000")
            {
                String strError = "Project ID : " + DBConfig.ProjectID + " >> " + dbAccount.Status + "  : " + dbAccount.Detail;
            }

            var dbConfig = new DataBaseConfig();
            dbConfig.DecrptedServername = AuthenDecrypt.Decrypt.textDecrpyt(dbAccount.ServerName.ToString());
            dbConfig.DatabaseName = DBConfig.POPDBname;
            dbConfig.DecryptedUsername = AuthenDecrypt.Decrypt.textDecrpyt(dbAccount.UserName.ToString());
            dbConfig.DecryptedPassword = AuthenDecrypt.Decrypt.textDecrpyt(dbAccount.Password.ToString());

            var connectionString = String.Format("Data Source={0};Initial Catalog={1};User Id={2};Password={3};Max Pool Size=400;Connect Timeout=600"
               , dbConfig.DecrptedServername
               , dbConfig.DatabaseName
               , dbConfig.DecryptedUsername
               , dbConfig.DecryptedPassword);
            return (connectionString);
        }

        private static String GetConnectionString()
        {
            String connectionString = "";
            var connStr = ConfigurationManager.ConnectionStrings["AGILEDBConnectionString"];
            if (connStr.ProviderName.Equals("System.Data.SqlClient"))
                connectionString = connStr.ConnectionString;
            else
                connectionString = GetWebserviceDatabaseConfig();

            return connectionString;
        }

    }
}
