using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using MyServicesTelegramBotDTO.ObjectsDTO.ServicePartDetailsDTO;
using MyServicesTelegramBotDTO.ObjectsDTO.ServicePartDTO;
using MyServicesTelegramDataTier.Settings;

namespace MyServicesTelegramBotDataTier.Data.ServicePartDetailsData
{
    public static class clsServicePartDetailsData
    {
        public static List<clsServicePartDetailsTitleDTO> GetServicePartDetailsTitles(int ServicePartID, ref Exception exception)
        {
            List<clsServicePartDetailsTitleDTO> details = new List<clsServicePartDetailsTitleDTO>();

            try
            {
                using (SqlConnection connection = new SqlConnection(clsConnectionRouteData.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_GetServicePartDetailsTitles", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ServicePartID", ServicePartID);

                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var detail = new clsServicePartDetailsTitleDTO
                                {
                                    ServicePartDetailsID = reader.GetInt32(0),
                                    Title = reader.GetString(1)
                                };
                                details.Add(detail);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            return details;
        }

        public static clsServicePartDetailDTO GetServicePartDetails(int ServicePartDetailsID, ref Exception exception)
        {
            clsServicePartDetailDTO servicePartDetail = null;

            using (SqlConnection connection = new SqlConnection(clsConnectionRouteData.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_GetServicePartDetails", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ServicePartDetailsID", ServicePartDetailsID);

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                servicePartDetail = new clsServicePartDetailDTO
                                {
                                    ServicePartDetailsID = reader["ServicePartDetailsID"] as int?,
                                    ServicePartID = reader["ServicePartID"] as int?,
                                    Title = reader["Title"] as string,
                                    WorkTimePerDays = reader["WorkTimePerDays"] as short?,
                                    MinPrice = reader["MinPrice"] as double?,
                                    MaxPrice = reader["MaxPrice"] as double?,
                                    Text_Description = reader["Text_Description"] as string
                                };
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        exception = ex;
                    }
                }
            }

            return servicePartDetail;
        }


        public static int? AddServicePartDetails(clsServicePartDetailDTO servicePartDetailsDTO, ref Exception ex)
        {
            int? servicePartDetailsID = null;

            using (SqlConnection connection = new SqlConnection(clsConnectionRouteData.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_AddServicePartDetails", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ServicePartID", servicePartDetailsDTO.ServicePartID);
                    command.Parameters.AddWithValue("@Title", servicePartDetailsDTO.Title);
                    command.Parameters.AddWithValue("@WorkTimePerDays", (object)servicePartDetailsDTO.WorkTimePerDays ?? DBNull.Value);
                    command.Parameters.AddWithValue("@MinPrice", (object)servicePartDetailsDTO.MinPrice ?? DBNull.Value);
                    command.Parameters.AddWithValue("@MaxPrice", (object)servicePartDetailsDTO.MaxPrice ?? DBNull.Value);
                    command.Parameters.AddWithValue("@Text_Description", (object)servicePartDetailsDTO.Text_Description ?? DBNull.Value);

                    SqlParameter servicePartDetailsIDParam = new SqlParameter("@ServicePartDetailsID", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(servicePartDetailsIDParam);

                    try
                    {
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            servicePartDetailsID = (int)servicePartDetailsIDParam.Value;
                        }
                    }
                    catch (Exception exception)
                    {
                        ex = exception;
                    }
                }
            }
            return servicePartDetailsID;
        }

        public static bool UpdateServicePartDetails(clsUpdateServicePartDetailDTO UpdateServicePartDetailsDTO, ref Exception ex)
        {
            bool isUpdated = false;

            using (SqlConnection connection = new SqlConnection(clsConnectionRouteData.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_UpdateServicePartDetails", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ServicePartDetailsID", UpdateServicePartDetailsDTO.ServicePartDetailsID);
                    command.Parameters.AddWithValue("@Title", UpdateServicePartDetailsDTO.Title);
                    command.Parameters.AddWithValue("@WorkTimePerDays", (object)UpdateServicePartDetailsDTO.WorkTimePerDays ?? DBNull.Value);
                    command.Parameters.AddWithValue("@MinPrice", (object)UpdateServicePartDetailsDTO.MinPrice ?? DBNull.Value);
                    command.Parameters.AddWithValue("@MaxPrice", (object)UpdateServicePartDetailsDTO.MaxPrice ?? DBNull.Value);
                    command.Parameters.AddWithValue("@Text_Description", (object)UpdateServicePartDetailsDTO.Text_Description ?? DBNull.Value);

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


        public static clsServicePartDetailDTO FindServicePartDetails(int servicePartDetailsID, ref Exception ex)
        {
            clsServicePartDetailDTO servicePartDetailsDTO = null;

            using (SqlConnection connection = new SqlConnection(clsConnectionRouteData.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_FindServicePartDetails", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ServicePartDetailsID", servicePartDetailsID);

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                servicePartDetailsDTO = new clsServicePartDetailDTO
                                {
                                    ServicePartDetailsID = reader["ServicePartDetailsID"] as int?,
                                    ServicePartID = reader["ServicePartID"] as int?,
                                    Title = reader["Title"] as string,
                                    WorkTimePerDays = reader["WorkTimePerDays"] as short?,
                                    MinPrice = reader["MinPrice"] as double?,
                                    MaxPrice = reader["MaxPrice"] as double?,
                                    Text_Description = reader["Text_Description"] as string
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

            return servicePartDetailsDTO;
        }
    }

}