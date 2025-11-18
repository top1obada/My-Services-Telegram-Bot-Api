using System;
using System.Data;
using Microsoft.Data.SqlClient;
using MyServicesTelegramBotDTO.ObjectsDTO.ImageDTO;
using MyServicesTelegramDataTier.Settings;

namespace MyServicesTelegramBotDataTier.Data.ImageData
{
    public static class clsImageData
    {
        public static int? AddImage(clsImageDTO imageDTO, ref Exception ex)
        {
            int? imageID = null;

            using (SqlConnection connection = new SqlConnection(clsConnectionRouteData.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_AddImage", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ServicePartDetailID", (object)imageDTO.ServicePartDetailID ?? DBNull.Value);
                    command.Parameters.AddWithValue("@ImagePath", imageDTO.ImagePath);

                    SqlParameter imageIDParam = new SqlParameter("@ImageID", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(imageIDParam);

                    try
                    {
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            imageID = (int)imageIDParam.Value;
                        }
                    }
                    catch (Exception exception)
                    {
                        ex = exception;
                    }
                }
            }
            return imageID;
        }

        public static bool UpdateImage(clsUpdateImageDTO imageDTO, ref Exception ex)
        {
            bool isUpdated = false;

            using (SqlConnection connection = new SqlConnection(clsConnectionRouteData.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_UpdateImage", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ImageID", imageDTO.ImageID);
                    
                    command.Parameters.AddWithValue("@ImagePath", imageDTO.ImagePath);

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

        public static clsImageDTO FindImage(int imageID, ref Exception ex)
        {
            clsImageDTO imageDTO = null;

            using (SqlConnection connection = new SqlConnection(clsConnectionRouteData.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_FindImage", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ImageID", imageID);

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                imageDTO = new clsImageDTO
                                {
                                    ImageID = reader["ImageID"] as int?,
                                    ServicePartDetailID = reader["ServicePartDetailID"] as int?,
                                    ImagePath = reader["ImagePath"] as string
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

            return imageDTO;
        }

    }
}