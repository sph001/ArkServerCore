using System;
using System.IO;

namespace ArkServerCore.Models
{
    static class Paths
    {
        internal static Uri SteamDownloadUri = new Uri("https://steamcdn-a.akamaihd.net/client/installer/steamcmd.zip");
        internal static string RootDirectory = Path.GetPathRoot(System.Reflection.Assembly.GetEntryAssembly().Location);
        internal static string ApplicationDirectory = AppDomain.CurrentDomain.BaseDirectory;
        internal static string SteamPath = RootDirectory + "SteamCmd";
        internal static string ServerPath = RootDirectory + "arkserver\\ShooterGame\\Binaries\\win64\\";
        internal static string SettingsFile = ApplicationDirectory + "settings.txt";
        internal static string ServerConfigPath = RootDirectory + "arkserver\\ShooterGame\\Saved\\Config\\WindowsServer\\";
        internal static string GameUserSettingsIni = ServerConfigPath + "GameUserSettings.ini";
        internal static string ServerExePath = ServerPath + "ShooterGameServer.exe";
    }
}
