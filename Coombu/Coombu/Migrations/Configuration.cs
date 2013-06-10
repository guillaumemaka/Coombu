namespace Coombu.Migrations
{
    using Coombu.Models;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Data.OleDb;
    using System.Linq;
    using WebMatrix.WebData;

    internal sealed class Configuration : DbMigrationsConfiguration<Coombu.Models.CoombuContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Coombu.Models.CoombuContext context)
        {            
            String path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../App_Data");

            
            WebSecurity.InitializeDatabaseConnection(
                "AzureContext",
                "UserProfile",
                "UserId",
                "UserName", autoCreateTables: true);
            
            

            DataTable data = ReadCSVFile(path, "Employees.csv");
            for (int i = 0; i < data.Rows.Count; i++)
            {                
                DataRow userRow = data.Rows[i];                

                String userName = String.Format("{0}.{1}", userRow["Firstname"].ToString(), userRow["Lastname"].ToString()).ToLower();

                String emailAddress = userName + "@coombu.com";
                if (!WebSecurity.UserExists(userName))
                {
                    WebSecurity.CreateUserAndAccount(userName, "supinfo",
                        new
                        {
                            FirstName = userRow["Firstname"].ToString(),
                            LastName = userRow["Lastname"].ToString(),
                            EmailAddress = emailAddress,
                            Department = userRow["Department"].ToString(),
                            DateOfBirth = new DateTime(1985, 11, 3)
                        });
                }                                                
            }            
        }

        private DataTable ReadCSVFile(string filePath, string fileName)
        {
            string connString;
            OleDbConnection conn;
            OleDbDataAdapter da;
            OleDbCommand cmd;
            DataTable dt;

            connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath +
                          ";Extended Properties='text;HDR=Yes;FMT=Delimited'";
            


            try
            {
                conn = new OleDbConnection(connString);
                conn.Open();

                cmd = new OleDbCommand("SELECT * FROM " + fileName, conn);
                da = new OleDbDataAdapter(cmd);
                dt = new DataTable();

                da.Fill(dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
