using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using MyServicesTelegramBotDTO.ObjectsDTO.ServicePartDTO;
using MyServicesTelegramDataTier.Settings;

namespace MyServicesTelegramBotDataTier.Data.ServicePartData
{
    public static class clsServicePartData
    {
        public static List<clsServicePartDTO> GetServiceParts(int ServiceID, ref Exception exception)
        {
            List<clsServicePartDTO> serviceParts = new List<clsServicePartDTO>();

            using (SqlConnection connection = new SqlConnection(clsConnectionRouteData.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_GetServiceParts", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ServiceID", ServiceID);

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                clsServicePartDTO servicePart = new clsServicePartDTO
                                {
                                    ServicePartID = reader["ServicePartID"] as int?,
                                    ServiceID = reader["ServiceID"] as int?,
                                    ServicePartName = reader["ServicePartName"] as string
                                };

                                serviceParts.Add(servicePart);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        exception = ex;
                    }
                }
            }

            return serviceParts;
        }

        public static int? AddServicePart(clsServicePartDTO servicePartDTO, ref Exception ex)
        {
            int? servicePartID = null;

            using (SqlConnection connection = new SqlConnection(clsConnectionRouteData.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_InsertServicePart", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ServiceID", servicePartDTO.ServiceID);
                    command.Parameters.AddWithValue("@ServicePartName", servicePartDTO.ServicePartName);

                    SqlParameter servicePartIDParam = new SqlParameter("@ServicePartID", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(servicePartIDParam);

                    try
                    {
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            servicePartID = (int)servicePartIDParam.Value;
                        }
                    }
                    catch (Exception exception)
                    {
                        ex = exception;
                    }
                }
            }
            return servicePartID;
        }

        public static bool UpdateServicePart(clsUpdateServicePartDTO servicePartDTO, ref Exception ex)
        {
            bool isUpdated = false;

            using (SqlConnection connection = new SqlConnection(clsConnectionRouteData.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_UpdateServicePart", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ServicePartID", servicePartDTO.ServicePartID);
                    command.Parameters.AddWithValue("@ServicePartName", servicePartDTO.ServicePartName);

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

        public static clsServicePartDTO FindServicePart(int servicePartID, ref Exception ex)
        {
            clsServicePartDTO servicePartDTO = null;

            using (SqlConnection connection = new SqlConnection(clsConnectionRouteData.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_FindServicePart", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ServicePartID", servicePartID);

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                servicePartDTO = new clsServicePartDTO
                                {
                                    ServicePartID = reader["ServicePartID"] as int?,
                                    ServiceID = reader["ServiceID"] as int?,
                                    ServicePartName = reader["ServicePartName"] as string
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

            return servicePartDTO;
        }
    }

}