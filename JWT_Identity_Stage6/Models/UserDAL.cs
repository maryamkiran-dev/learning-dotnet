using Microsoft.Data.SqlClient;

namespace JWT_Identity_Stage6.Models
{
    public class UserDAL
    {
        private readonly string _connectionString;
        public UserDAL(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public UserModel validateUser(string userName, string password)
        {
            UserModel model = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {

                connection.Open();
                string query = @"INSERT INTO  Users (Username, Password)
                             VALUES (@Username, @Password)";

                var command = new SqlCommand(query, connection);


                command.Parameters.AddWithValue("@Username", userName);
                command.Parameters.AddWithValue("@Password", password);

                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    model = new UserModel
                    (
                       model.Id = Convert.ToInt32(reader["Id"]),
                       model.Username = Convert.ToString(reader["Username"]),
                       model.Role = Convert.ToString(reader["Role"])

                    );

                }
                reader.Close();
            }
            return model;
        }
    }
}
