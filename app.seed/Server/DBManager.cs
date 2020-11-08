using Npgsql;
using Server.Repository;
using System.Windows;

namespace Server
{
    class DBManager
    {
        public static NpgsqlConnection con;
        public const string DBController = "controller_con";

        private const string connectionString = "{0};User Id={1};Password={2}; Database={3}";
        private const string DBUser = "postgres";
        private const string DBPassword = "postgres";
        private const string HostName = "plants.cagsxko3nzn2.us-east-2.rds.amazonaws.com";

        public DBManager()
        {
            InitializeDBConnection();
        }

        private void InitializeDBConnection()
        {
        
            con = new NpgsqlConnection(string.Format(connectionString, Resource.ConnectionString, DBUser, DBPassword, "plant"));
            con.Open();

            NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.Connection = con;
            cmd.CommandText = string.Format("select dblink_connect('{1}','host={0} dbname=controller user=postgres password=postgres')", HostName, DBController);
            using (NpgsqlDataReader dr = cmd.ExecuteReader()){}

        }

        public void CloseConnection()
        {
            con.Close();
        }


        public void test()
        {

            NpgsqlCommand cmd = new NpgsqlCommand(string.Format("SELECT op_col,name FROM dblink('{0}','SELECT test_id,name from test') AS t(op_col int, name varchar)", DBController), con);
            NpgsqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                MessageBox.Show(reader.GetInt16(0).ToString());
                MessageBox.Show(reader.GetString(1));
            }

            reader.Dispose();
            cmd.Dispose();
        }
    }
}
