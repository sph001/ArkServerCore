using System.IO;
using System.Web.Script.Serialization;
using ArkServerCore.Models;

namespace ArkServerCore
{
    public class ServerSettings
    {

        public string SessionName { get; set; }
        public string MessageOfTheDay { get; set; }
        public int MaxPlayers { get; set; }
        public decimal Difficulty { get; set; }
        public string ServerPassword { get; set; }
        public string ServerAdminPassword { get; set; }
        public int QueryPort { get; set; }

        public bool ServerPVE { get; set; }
        public bool ServerHardcore { get; set; }
        public bool GlobalVoiceChat { get; set; }
        public bool ProximityTextChat { get; set; }
        public bool NoTributeDownloads { get; set; }
        public bool AllowsNotifyPlayerLeft { get; set; }
        public bool DontAlwaysNotifyPlayerJoined { get; set; }
        public bool ServerForceNoHud { get; set; }
        public bool AllowThirdPersonPlayer { get; set; }
        public bool ServerCrosshair { get; set; }
        public bool MapPlayerLocation { get; set; }

        public ServerSettings ReadSettingsFile()
        {
            JavaScriptSerializer ser = new JavaScriptSerializer();
            this.CreateFile(Paths.SettingsFile);
            string jsonString = File.ReadAllText(Paths.SettingsFile);
            return ser.Deserialize<ServerSettings>(jsonString);
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

        internal string BuildServerConfigString()
        {
            string parameters = "TheIsland?listen";
            parameters = parameters + "?MaxPlayers=" + this.MaxPlayers;
            parameters = parameters + "?QueryPort=" + this.QueryPort;
            return parameters;
        }
    }
}
