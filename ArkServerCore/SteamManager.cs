using System;
using System.IO;
using System.Net;
using System.Diagnostics;
using System.IO.Compression;
using ArkServerCore.Models;

namespace ArkServerCore
{
    public class SteamManager
    {
        public int DownloadPercent { get; set; }
        public Process SteamProcess { get; set; }

        public void StartUpdate()
        {
            if (!this.CheckSteamExistance()) 
                this.DownloadNewSteamCmd();
            this.StartSteamClient();
        }

        public void DownloadNewSteamCmd()
        {
            this.TryDownloadnewSteamClient();
            ZipFile.ExtractToDirectory(Paths.RootDirectory + "\\steamcmd.zip", Paths.SteamPath);
        }
        
        private void StartSteamClient()
        {
            string parameters = "+login anonymous +force_install_dir ..\\ArkServer +app_update 376030 +quit";
            try
            {
                this.SteamProcess = Process.Start(Paths.SteamPath + "\\steamcmd.exe", parameters);
            }
            catch (Exception)
            {
                
            }
        }

        private void TryDownloadnewSteamClient()
        {
            using (WebClient client = new WebClient())
            {
                client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(this.ProgressChanged);
                try
                {
                    client.DownloadFile(Paths.SteamDownloadUri, Paths.RootDirectory + "\\steamcmd.zip");
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        private bool CheckSteamExistance()
        {
            if (!Directory.Exists(Paths.SteamPath)) 
                return false;
            else 
                return true;
        }

        private void ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            this.DownloadPercent = e.ProgressPercentage;
        }
    }
}
