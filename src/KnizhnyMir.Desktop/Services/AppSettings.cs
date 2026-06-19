using System.IO;
using System.Text.Json;
using KnizhnyMir.DataAccess.Models;

namespace KnizhnyMir.Desktop.Services
{
    /// <summary>Данные о текущем пользователе, сохраняемые в настройках приложения.</summary>
    public class CurrentUserSettings
    {
        /// <summary>Идентификатор пользователя.</summary>
        public int UserId { get; set; }

        /// <summary>ФИО пользователя.</summary>
        public string FullName { get; set; } = string.Empty;

        /// <summary>Название роли пользователя.</summary>
        public string RoleName { get; set; } = string.Empty;
    }

    /// <summary>
    /// Сохраняет и загружает настройки приложения (данные текущего пользователя)
    /// в файл в папке профиля пользователя.
    /// </summary>
    public static class AppSettings
    {
        private static readonly string SettingsPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "КнижныйМир",
            "settings.json");

        /// <summary>Сохраняет данные текущего пользователя в настройки.</summary>
        public static void SaveCurrentUser(User user)
        {
            var settings = new CurrentUserSettings
            {
                UserId = user.Id,
                FullName = user.FullName,
                RoleName = user.RoleName ?? string.Empty
            };

            Directory.CreateDirectory(Path.GetDirectoryName(SettingsPath)!);
            File.WriteAllText(SettingsPath, JsonSerializer.Serialize(settings));
        }

        /// <summary>Загружает данные текущего пользователя из настроек или <c>null</c>.</summary>
        public static CurrentUserSettings? LoadCurrentUser()
        {
            if (!File.Exists(SettingsPath))
            {
                return null;
            }

            try
            {
                return JsonSerializer.Deserialize<CurrentUserSettings>(File.ReadAllText(SettingsPath));
            }
            catch
            {
                return null;
            }
        }

        /// <summary>Удаляет данные текущего пользователя из настроек (выход из системы).</summary>
        public static void ClearCurrentUser()
        {
            if (File.Exists(SettingsPath))
            {
                File.Delete(SettingsPath);
            }
        }
    }
}
