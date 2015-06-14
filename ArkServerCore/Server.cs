using System.Diagnostics;
using System.IO;
using System.Web.Script.Serialization;
using ArkServerCore.Models;

namespace ArkServerCore
{
    public class Server
    {
        public ServerSettings Settings = new ServerSettings();
        public Process ServerProcess { get; set; }
        public SteamManager SteamCmd = new SteamManager();
        public IniManager IniManager = new IniManager();

        public Server()
        {
            this.LoadSettings();
            this.CreateFile(Paths.GameUserSettingsIni);
        }

        ~Server()
        {
            this.SaveSettings();
        }

        public void LoadSettings()
        {
            ServerSettings tempSettings = new ServerSettings();
            tempSettings = this.Settings.ReadSettingsFile();
            if (tempSettings != null)
            {
                this.Settings = tempSettings;
                if (this.Settings.MessageOfTheDay != null)
                {
                    this.Settings.MessageOfTheDay = this.Settings.MessageOfTheDay.Replace("\\n", "\n");
                }               
            }
        }

        public void StartServer()
        {
            this.IniManager.ReadIniFile();
            this.SaveSettings();
            string parameters = this.Settings.BuildServerConfigString();
            ProcessStartInfo processSettings = new ProcessStartInfo(Paths.ServerExePath, parameters);
            processSettings.WindowStyle = ProcessWindowStyle.Minimized;
            this.ServerProcess = Process.Start(processSettings);
            
        }

        public void CloseSever()
        {
            if (!this.ServerProcess.HasExited)
            {
                this.ServerProcess.CloseMainWindow();
            }    
        }
        public void SaveSettings()
        {
            JavaScriptSerializer ser = new JavaScriptSerializer();
            this.CreateFile(Paths.SettingsFile);
            this.CreateFile(Paths.GameUserSettingsIni);
            File.WriteAllText(Paths.SettingsFile, ser.Serialize(this.Settings));
            this.UpdateServerIni();
            this.IniManager.SaveIni();
        }

        private void CreateFile(string path)
        {
            string directory = path.Replace(Path.GetFileName(path), "");
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            if (!File.Exists(path))
            {
                File.Create(path).Dispose();
            }
        }

        private void UpdateServerIni()
        {
            this.IniManager.ReplaceValue("[SessionSettings]", "SessionName", this.Settings.SessionName);
            this.IniManager.ReplaceValue("[MessageOfTheDay]", "Message", this.Settings.MessageOfTheDay);
            this.IniManager.ReplaceValue("[ServerSettings]", "ServerPassword", this.Settings.ServerPassword);
            this.IniManager.ReplaceValue("[ServerSettings]", "ServerAdminPassword", this.Settings.ServerAdminPassword);
            this.IniManager.ReplaceValue("[ServerSettings]", "ShowMapPlayerLocation", this.Settings.MapPlayerLocation.ToString());
            this.IniManager.ReplaceValue("[ServerSettings]", "ServerHardcore", this.Settings.ServerHardcore.ToString());
            this.IniManager.ReplaceValue("[ServerSettings]", "GlobalVoiceChat", this.Settings.GlobalVoiceChat.ToString());
            this.IniManager.ReplaceValue("[ServerSettings]", "ProximityChat", this.Settings.ProximityTextChat.ToString());
            this.IniManager.ReplaceValue("[ServerSettings]", "NoTributeDownloads", this.Settings.NoTributeDownloads.ToString());
            this.IniManager.ReplaceValue("[ServerSettings]", "AllowThirdPersonPlayer", this.Settings.AllowThirdPersonPlayer.ToString());
            this.IniManager.ReplaceValue("[ServerSettings]", "DontAlwaysAllowNotifyPlayerJoined", this.Settings.DontAlwaysNotifyPlayerJoined.ToString());
            this.IniManager.ReplaceValue("[ServerSettings]", "ServerHardcore", this.Settings.ServerHardcore.ToString());
            this.IniManager.ReplaceValue("[ServerSettings]", "ServerPVE", this.Settings.ServerPVE.ToString());
            this.IniManager.ReplaceValue("[ServerSettings]", "ServerCrosshair", this.Settings.ServerCrosshair.ToString());
            this.IniManager.ReplaceValue("[ServerSettings]", "ServerForceNoHud", this.Settings.ServerForceNoHud.ToString());
            this.IniManager.ReplaceValue("[ServerSettings]", "DifficultyOffset", this.Settings.Difficulty.ToString("0.000"));
            this.IniManager.ReplaceValue("[ServerSettings]", "bDisableStructureDecayPvE", this.Settings.bDisableStructureDecayPve.ToString());
            this.IniManager.ReplaceValue("[ServerSettings]", "bAllowFlyerCarryPvE", this.Settings.bAllowFlyerCarryPve.ToString());
            this.IniManager.ReplaceValue("[ServerSettings]", "MaxStructuresInRange", this.Settings.MaxStructuresInRange.ToString());
            this.IniManager.ReplaceValue("[ServerSettings]", "EnablePvPGamma", this.Settings.EnablePvpGama.ToString());
        }
    }
}
