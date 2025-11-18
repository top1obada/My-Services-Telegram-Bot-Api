using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyServicesTelegramBotDTO.ObjectsDTO.UserDTO;
using Microsoft.Data.SqlClient;
using MyServicesTelegramDataTier.Settings;

namespace MyServicesTelegramBotDataTier.Data.UserData
{
    public static class clsUserData
    {
        public static int? UserSignUp(clsUserDTO UserDTO, ref Exception ex)
        {
            int? userID = null;

            using (SqlConnection connection = new SqlConnection(clsConnectionRouteData.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("sp_RegisterUser", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters matching the stored procedure
                    command.Parameters.AddWithValue("@UserName", UserDTO.UserName);
                    command.Parameters.AddWithValue("@PasswordHash", UserDTO.PasswordHash);
                    command.Parameters.AddWithValue("@Salt", UserDTO.Salt);

                    // Add output parameter for UserID
                    SqlParameter userIDParam = new SqlParameter("@UserID", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(userIDParam);

                    try
                    {
                        connection.Open();
                        int RowCount = command.ExecuteNonQuery();
                        if (RowCount > 0)
                        {

                            userID = (int)userIDParam.Value;
                        }
                    }
                    catch (Exception exception)
                    {
                        ex = exception;
                    }
                }
            }

            return userID;
        }

        public static clsUserDTO UserLogin(string UserName, ref Exception ex)
        {
            clsUserDTO userDTO = null;

            using (SqlConnection connection = new SqlConnection(clsConnectionRouteData.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("sp_LoginUser", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserName", UserName);

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                userDTO = new clsUserDTO
                                {
                                    UserID = reader["UserID"] as int? ?? null,
                                    UserName = UserName,
                                    PasswordHash = reader["PasswordHash"] as byte[],
                                    Salt = reader["Salt"] as byte[],
                                    JoiningDate = reader["JoiningDate"] as DateTime? ?? null
                                };
                            }
                        }
                    }
                    catch (Exception exception)
                    {
                        ex = exception;
                    }
                }
            }

            return userDTO;
        }
    }
}