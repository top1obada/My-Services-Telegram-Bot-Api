using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using MyServicesTelegramBotDTO.ObjectsDTO.ServicesDTO;
using MyServicesTelegramDataTier.Settings;

namespace MyServicesTelegramBotDataTier.Data.ServiceData
{
    public static class clsServiceData
    {
        public static List<clsServiceDTO> GetAllServices(long BotID, ref Exception exception)
        {
            List<clsServiceDTO> services = new List<clsServiceDTO>();

            using (SqlConnection connection = new SqlConnection(clsConnectionRouteData.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_GetServices", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@BotID", BotID);

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                clsServiceDTO service = new clsServiceDTO
                                {
                                    ServiceID = reader["ServiceID"] as int?,
                                    ServiceName = reader["ServiceName"] as string,
                                    BotID = reader["BotID"] as int?
                                };
                                services.Add(service);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        exception = ex;
                    }
                }
            }
            return services;
        }

        public static int? AddService(clsServiceDTO serviceDTO, ref Exception ex)
        {
            int? serviceID = null;

            using (SqlConnection connection = new SqlConnection(clsConnectionRouteData.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_AddService", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ServiceName", serviceDTO.ServiceName);
                    command.Parameters.AddWithValue("@BotID", serviceDTO.BotID);

                    SqlParameter serviceIDParam = new SqlParameter("@ServiceID", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(serviceIDParam);

                    try
                    {
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            serviceID = (int)serviceIDParam.Value;
                        }
                    }
                    catch (Exception exception)
                    {
                        ex = exception;
                    }
                }
            }
            return serviceID;
        }

        public static bool UpdateService(clsUpdateServiceDTO serviceDTO, ref Exception ex)
        {
            bool isUpdated = false;

            using (SqlConnection connection = new SqlConnection(clsConnectionRouteData.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_UpdateService", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ServiceID", serviceDTO.ServiceID);
                    command.Parameters.AddWithValue("@ServiceName", serviceDTO.ServiceName);
                  

                    try
                    {
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        isUpdated = rowsAffected > 0;
                    }
                    catch (Exception exception)
                    {
                        ex = exception;
                    }
                }
            }
            return isUpdated;
        }


        public static clsServiceDTO FindService(int serviceID, ref Exception ex)
        {
            clsServiceDTO serviceDTO = null;

            using (SqlConnection connection = new SqlConnection(clsConnectionRouteData.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_FindService", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ServiceID", serviceID);

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                serviceDTO = new clsServiceDTO
                                {
                                    ServiceID = reader["ServiceID"] as int?,
                                    ServiceName = reader["ServiceName"] as string,
                                    BotID = reader["BotID"] as int?
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

            return serviceDTO;
        }
    }
}