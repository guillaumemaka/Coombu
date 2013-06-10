using log4net;
using System;
using System.Data;
using System.Linq;
using LinqKit;
using System.Data.Entity;
using System.Data.OleDb;
using WebMatrix.WebData;
using Microsoft.WindowsAzure;

namespace Coombu.Models
{
    public class SampleData : DropCreateDatabaseIfModelChanges<CoombuContext>
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(SampleData));

        protected override void Seed(CoombuContext context)
        {

            String path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../App_Data");

            log.Debug(path);
            WebSecurity.InitializeDatabaseConnection(
                CloudConfigurationManager.GetSetting("DbContext"),
                "UserProfile",
                "UserId",
                "UserName", autoCreateTables: true);

            DataTable data = ReadCSVFile(path, "Employees.csv");
            for (int i = 0; i < data.Rows.Count; i++)
            {
                DataRow userRow = data.Rows[i];

                log.Debug(userRow);

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

            foreach (UserProfile user in context.Users)
            {
                for (int j = 1; j < 100; j++)
                {
                    user.Posts.Add(new Post { Content = GetPostContent() });
                }

                context.Entry<UserProfile>(user);
                context.SaveChanges();
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

        private string GetPostContent()
        {
            return "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam diam enim, lobortis in congue dignissim, molestie ut ligula. Pellentesque nec risus a ipsum feugiat molestie a eget nisl. Donec at elit mauris, non blandit elit. In in mollis mi. Duis pellentesque ornare orci, nec auctor neque ullamcorper a. Aliquam erat volutpat. Ut erat velit, volutpat vitae porttitor in, sollicitudin ac lacus. Donec magna quam, ultrices et pretium vel, elementum ac magna. Sed ut erat quis dui vulputate faucibus. Praesent vestibulum nisl ut nibh accumsan id volutpat ligula ornare.";
        }
        
    }
}
