using System;
using System.Data;
using Microsoft.Data.SqlClient;
using MyServicesTelegramBotDTO.ObjectsDTO.TelegramBotDTO;
using MyServicesTelegramDataTier.Settings;

namespace MyServicesTelegramBotDataTier.Data.TelegramBotData
{
    public static class clsTelegramBotData
    {
        public static bool InsertTelegramBot(clsTelegramBotDTO telegramBotDTO, ref Exception ex)
        {
            bool isInserted = false;

            using (SqlConnection connection = new SqlConnection(clsConnectionRouteData.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_InsertTelegramBot", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters matching the stored procedure
                    command.Parameters.AddWithValue("@BotID", telegramBotDTO.BotID);
                    command.Parameters.AddWithValue("@BotUserName", telegramBotDTO.BotUserName);
                    command.Parameters.AddWithValue("@BotName", telegramBotDTO.BotName);
                    command.Parameters.AddWithValue("@UserID", telegramBotDTO.UserID);

                    try
                    {
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            isInserted = true;
                        }
                    }
                    catch (Exception exception)
                    {
                        ex = exception;
                    }
                }
            }

            return isInserted;
        }
    }
}