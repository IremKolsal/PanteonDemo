using DataAccess.Entities;
using System.Data;
using Dapper;
using DataAccess.Base;

namespace DataAccess.Repository.Account
{
    public class AccountRepository
    {
        private readonly IDbConnection _dbConnection;

        public AccountRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public ReturnViewModel GetLoginCheck(string userName, string password)
        {
            ReturnViewModel returnViewModel = new ReturnViewModel();
            try
            {
                var query = @"SELECT * FROM panteondemo.user WHERE (UserName = @userName OR Email = @userName) AND Password = @password";

                returnViewModel.Data = _dbConnection.QueryFirstOrDefault<User>(query, new { userName, password });
                returnViewModel.IsSuccess = true;
                return returnViewModel;

            }
            catch (Exception ex)
            {
                returnViewModel.IsSuccess = false;
                returnViewModel.Message = "Server Error";
                returnViewModel.Data = null;
                return returnViewModel;
            }

        }

        public ReturnViewModel Register(User user)
        {
            ReturnViewModel returnViewModel = new ReturnViewModel();
            try
            {
                if (CheckUserName(user.UserName) != null)
                {
                    returnViewModel.IsSuccess = false;
                    returnViewModel.Message = "Böyle bir kullanıcı ismi var.";
                    return returnViewModel;
                }
                if (CheckEmail(user.Email) != null)
                {
                    returnViewModel.IsSuccess = false;
                    returnViewModel.Message = "Böyle bir mail var.";
                    return returnViewModel;
                }
                var query = @"INSERT INTO panteondemo.user(UserName,Email,Password)VALUES(@UserName,@Email,@Password)";

                int result = _dbConnection.Execute(query, new { user.UserName, user.Email, user.Password });

                returnViewModel.IsSuccess = result > 0;
                returnViewModel.Message = "Kullanıcı başarıyla eklendi.";


                return returnViewModel;

            }
            catch (Exception ex)
            {
                returnViewModel.IsSuccess = false;
                returnViewModel.Message = "Server Error";
                returnViewModel.Data = null;
                return returnViewModel;
            }

        }

        private User CheckEmail(string email)
        {
            ReturnViewModel returnViewModel = new ReturnViewModel();
            try
            {
                var query = @"SELECT * FROM panteondemo.user WHERE  Email = @email";

                return _dbConnection.QueryFirstOrDefault<User>(query, new { email });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private User CheckUserName(string userName)
        {
            ReturnViewModel returnViewModel = new ReturnViewModel();
            try
            {
                var query = @"SELECT * FROM panteondemo.user WHERE UserName = @userName";

                return _dbConnection.QueryFirstOrDefault<User>(query, new { userName });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
