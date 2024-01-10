using FeedBackPro.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Security.Cryptography;


namespace FeedBackPro.DAL
{
    public class QandA_DAL
    {


        SqlConnection _connection = null;
        SqlCommand _command = null;
        public static IConfiguration Configuration { get; set; }
        private string GetConnectionString()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            Configuration = builder.Build();
            return Configuration.GetConnectionString("DefaultConnection");
        }

        public List<QandA> GetAllUsers()
        {
            List<QandA> usersList = new List<QandA>();
            using (_connection = new SqlConnection(GetConnectionString()))
            {
                _command = _connection.CreateCommand();
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "[DBO].[GETQUESTIONS]";


                _connection.Open();
                SqlDataReader dr = _command.ExecuteReader();
                while (dr.Read())
                {
                    QandA user = new QandA();
                    user.Qid = Convert.ToInt32(dr["Qid"]);
                    user.Ques = dr["Ques"].ToString();


                    usersList.Add(user);
                }
                _connection.Close();
            }
            return usersList;
        }

        public bool CreateUser(QandA user)
        {
            int val = 0;
            using (_connection = new SqlConnection(GetConnectionString()))
            {
                _command = _connection.CreateCommand();
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "[DBO].[AddQues]";
                _command.Parameters.AddWithValue("@Qid", user.Qid);
                _command.Parameters.AddWithValue("@Ques", user.Ques);

                _connection.Open();
                val = _command.ExecuteNonQuery();
                _connection.Close();
            }
            return val > 0 ? true : false;
        }


        public bool DeleteUser(int Id)
        {
            int val = 0;
            using (_connection = new SqlConnection(GetConnectionString()))
            {
                _command = _connection.CreateCommand();
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "[DBO].[DELETEQUES]";
                _command.Parameters.AddWithValue("@Id", Id);
                _connection.Open();
                val = _command.ExecuteNonQuery();
                _connection.Close();
            }
            return val > 0 ? true : false;
        }
      
        public bool AdminUser(Yeswanth user)
        {
            int val = 0;
            using (_connection = new SqlConnection(GetConnectionString()))
            {
                _command = _connection.CreateCommand();
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "[DBO].[InsertYeswanthData]";
                _command.Parameters.AddWithValue("@Qid", user.Qid);
                _command.Parameters.AddWithValue("@Sid", user.Sid);
                _command.Parameters.AddWithValue("@Name", user.Name);
                
                _command.Parameters.AddWithValue("@Course", user.Course);
                _command.Parameters.AddWithValue("@Answer", user.Answer);

                _connection.Open();
                val = _command.ExecuteNonQuery();
                _connection.Close();
            }
            return val > 0 ? true : false;
        }



    }
}





//public bool UpdateUser(QandA user)
//{
//    int val = 0;
//    using (_connection = new SqlConnection(GetConnectionString()))
//    {
//        _command = _connection.CreateCommand();
//        _command.CommandType = CommandType.StoredProcedure;
//        _command.CommandText = "[DBO].[UpdateQues]"; // Replace with your update stored procedure

//        _command.Parameters.AddWithValue("@Qid", user.Qid);
//        _command.Parameters.AddWithValue("@Ques", user.Ques);

//        _connection.Open();
//        val = _command.ExecuteNonQuery();
//        _connection.Close();
//    }
//    return val > 0 ? true : false;
//}

