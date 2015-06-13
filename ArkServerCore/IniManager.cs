using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using ArkServerCore.Models;

namespace ArkServerCore
{
    public class IniManager
    {
        public Ini FormattedIni = new Ini();
        private string _rawIniText = string.Empty;

        public IniManager()
        {
            this.ReadIniFile();
            this.CreateDefaultGameUserSettingsIni();
        }

        public void SaveIni()
        { 
            this.WriteToIni(this.FormattedIni.Sections);
        }

        public void ReadIniFile()
        {
            this.FormattedIni = new Ini();
            this.GetRawIniText();
            this.ParseSections();
        }

        private void GetRawIniText()
        {
            string rawIniText = string.Empty;
            try
            {
                using (StreamReader sr = new StreamReader(Paths.GameUserSettingsIni))
                {
                    rawIniText = sr.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("File Could Not be read: " + ex.Message);
            }
            this._rawIniText = rawIniText;
        }

        private void ParseSections()
        {
            try
            {
                string[] splitSections = this._rawIniText.Split(new string[] { "\r\n\r\n" }, StringSplitOptions.None);
                foreach (string r in splitSections)
                {
                    string[] rawKeyValues = r.Split(new string[] { "\r\n" }, StringSplitOptions.None);
                    Dictionary<string, string> keyValues = new Dictionary<string, string>();

                    foreach (string s in rawKeyValues)
                    {
                        string[] splitKeyValues = s.Split(new char[] { '=' });
                        if (splitKeyValues.Length == 2)
                        {
                            keyValues.Add(splitKeyValues[0], splitKeyValues[1]);
                        }
                    }
                    if (rawKeyValues[0] != "")
                    {
                        this.FormattedIni.AddSection(rawKeyValues[0], keyValues);   
                    }
                }
            }
            catch (Exception ex)
            {
                
                Debug.WriteLine("parseFailed: " + ex.Message);
            }
        }

        public void ReplaceValue(string sectionName, string key, string value)
        {
            Section tempSection = this.FormattedIni.Sections.Find(x => x.SectionName == sectionName);
            if (tempSection != null)
            {
                this.FormattedIni.Sections.Find(x => x.SectionName == sectionName)
                                                           .KeyValues[key] = value;
            }
            else
            {
                tempSection = new Section {SectionName = sectionName};
                tempSection.KeyValues.Add(key, value);
                this.FormattedIni.AddSection(tempSection);
            }
        }

        private void WriteToIni(List<Section> dataSource)
        {
            string newRawIniText = "";
            foreach (Section r in dataSource)
            {
                newRawIniText = newRawIniText + r.SectionName + "\r\n";
                foreach (KeyValuePair<string, string> s in r.KeyValues)
                {
                    newRawIniText = newRawIniText + s.Key + "=" + s.Value + "\r\n";

                }
                newRawIniText = newRawIniText + "\r\n";
            }
            using (StreamWriter sw = new StreamWriter(Paths.GameUserSettingsIni))
            {
                sw.WriteLine(newRawIniText);
            }
        }

        private void CreateDefaultGameUserSettingsIni()
        {
            if (!File.Exists(Paths.GameUserSettingsIni))
            {
                this.FormattedIni.Sections.Add(this.GenerateDefaultSettings());
            }
        }

        private Section GenerateDefaultSettings()
        {
            Section newSection = new Section();
            Dictionary<string, string> DefaultValues = new Dictionary<string, string>();
            DefaultValues.Add("MasterAudioVolume", "1.000000");
            DefaultValues.Add("MusicAudioVolume", "1.000000");
            DefaultValues.Add("SFXAudioVolume", "1.000000");
            DefaultValues.Add("CameraShakeScale", "1.000000");
            DefaultValues.Add("bFirstPersonRiding", "False");
            DefaultValues.Add("bThirdPersonPlayer", "False");
            DefaultValues.Add("bShowStatusNotificationMessages", "True");
            DefaultValues.Add("TrueSkyQuality", "0.270000");
            DefaultValues.Add("FOVMultiplier", "1.000000");
            DefaultValues.Add("GroundClutterDensity", "1.000000");
            DefaultValues.Add("bFilmGrain", "False");
            DefaultValues.Add("bMotionBlur", "True");
            DefaultValues.Add("bUseDFAO", "True");
            DefaultValues.Add("bUseSSAO", "True");
            DefaultValues.Add("bShowChatBox", "True");
            DefaultValues.Add("bCameraViewBob", "True");
            DefaultValues.Add("bInvertLookY", "False");
            DefaultValues.Add("bFloatingNames", "True");
            DefaultValues.Add("bChatBubbles", "True");
            DefaultValues.Add("bHideServerInfo", "False");
            DefaultValues.Add("bJoinNotifications", "False");
            DefaultValues.Add("bCraftablesShowAllItems", "True");
            DefaultValues.Add("LookLeftRightSensitivity", "1.000000");
            DefaultValues.Add("LookUpDownSensitivity", "1.000000");
            DefaultValues.Add("GraphicsQuality", "2");
            DefaultValues.Add("ActiveLingeringWorldTiles", "10");
            DefaultValues.Add("LastServerSearchType", "0");
            DefaultValues.Add("LastServerSearchHideFull", "False");
            DefaultValues.Add("LastServerSearchProtected", "False");
            DefaultValues.Add("HideItemTextOverlay", "False");
            DefaultValues.Add("bDistanceFieldShadowing", "True");
            DefaultValues.Add("LODScalar", "1.000000");
            DefaultValues.Add("HighQualityMaterials", "True");
            DefaultValues.Add("HighQualitySurfaces", "True");
            DefaultValues.Add("bTemperatureF", "False");
            DefaultValues.Add("bDisableTorporEffect", "False");
            DefaultValues.Add("bUseVSync", "False");
            DefaultValues.Add("ResolutionSizeX", "1280");
            DefaultValues.Add("ResolutionSize", "720");
            DefaultValues.Add("LastUserConfirmedResolutionSizeX", "1280");
            DefaultValues.Add("LastUserConfirmedResolutionSizeY", "720");
            DefaultValues.Add("WindowPosX", "-1");
            DefaultValues.Add("WindowPosY", "-1");
            DefaultValues.Add("bUseDesktopResolutionForFullscreen", "False");
            DefaultValues.Add("FullscreenMode", "2");
            DefaultValues.Add("LastConfirmedFullscreenMode", "2");
            DefaultValues.Add("Version", "5");
            newSection.SectionName = "[/Script/ShooterGame.ShooterGameUserSettings]";
            newSection.KeyValues = DefaultValues;
            return newSection;
        }
    }
}
