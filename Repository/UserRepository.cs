using ManageFinances.Entities;
using Dapper;
using ManageFinances.Repository;
using Npgsql;
using static BCrypt.Net.BCrypt;

namespace AspnCrudDapper.Repository
{
    public class UserRepository : IUserRepository
    {
        IConfiguration _configuration;

        string connectionString;
        public UserRepository(IConfiguration configuration)
        {
            _configuration = configuration;

            connectionString = _configuration["ConnectionStrings:DefaultConnection"];
        }

        public User Create(User user)
        {
            user.Password = HashPassword(user.Password, GenerateSalt(13));

            User result = new User();
            using (var con = new NpgsqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    var query = @"
INSERT INTO Users(username, email, password) VALUES(@Username, @Email, @Password) RETURNING *;
";
                    result = con.Query<User>(query, user).FirstOrDefault();

                    result.Password = null;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                }
                return result;
            }
        }
        public int Delete(int id)
        {
            var count = 0;
            using (var con = new NpgsqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    var query = $"DELETE FROM Users WHERE id = {id}";
                    count = con.Execute(query);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                }
                return count;
            }
        }
        public User Update(User user)
        {
            User result = new User();
            using (var con = new NpgsqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    var query = @$"UPDATE Users SET username = @Username, email = @Email WHERE id = {user.Id} RETURNING *";
                    
                    result = con.Query<User>(query, user).FirstOrDefault();

                    result.Password = null;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                }
                return result;
            }
        }
        public User Get(int id)
        {
            User result = new User();
            using (var con = new NpgsqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    var query = $"SELECT * FROM Users WHERE id = {id}";
                    
                    result = con.Query<User>(query).FirstOrDefault();

                    result.Password = null;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                }
                return result;
            }
        }
        public User GetByUsernameOrEmail(string usernameOrEmail)
        {
            User result = new User();

            using (var con = new NpgsqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    var query = $"SELECT * FROM Users WHERE username = '{usernameOrEmail}' OR email = '{usernameOrEmail}'";

                    result = con.Query<User>(query).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                }
                return result;
            }
        }

        public List<User> GetUsers()
        {
            List<User> users = new List<User>();
            using (var con = new NpgsqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    var query = "SELECT id, username, email FROM users";
                    users = con.Query<User>(query).ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                }
                return users;
            }
        }
    }
}