﻿using System.IO;
using UnityEngine;

namespace RP0
{
    public class KCTSettings : HostedSingleton
    {
        private const string _fileName = "KCT_Config.txt";
        private readonly string _directory = KSPUtil.ApplicationRootPath + "GameData/RP-1/PluginData/";

        public static new KCTSettings Instance { get; private set; }

        [Persistent]
        public int MaxTimeWarp;
        [Persistent]
        public bool DisableAllMessages;
        [Persistent]
        public bool ShowSimWatermark;
        [Persistent]
        public bool AutoKACAlarms;
        [Persistent]
        public bool OverrideLaunchButton;
        [Persistent]
        public bool PreferBlizzyToolbar;
        [Persistent]
        public bool RandomizeCrew;
        [Persistent]
        public int WindowMode = 1;
        [Persistent]
        public bool CleanUpKSCDebris;
        [Persistent]
        public bool UseDates;
        [Persistent]
        public bool InPlaceEdit;

        public KCTSettings(SingletonHost host) : base(host)
        {
            DisableAllMessages = true;
            ShowSimWatermark = true;
            OverrideLaunchButton = true;
            AutoKACAlarms = false;
            PreferBlizzyToolbar = false;
            CleanUpKSCDebris = true;
            UseDates = true;
            InPlaceEdit = false;
        }

        public override void Awake()
        {
            Instance = this;
        }

        public override void OnDestroy()
        {
            if (Instance == this)
                Instance = null;
        }

        public void Load()
        {
            if (TimeWarp.fetch != null)
            {
                MaxTimeWarp = TimeWarp.fetch.warpRates.Length - 1;
            }

            if (File.Exists(Path.Combine(_directory, _fileName)))
            {
                ConfigNode cnToLoad = ConfigNode.Load(Path.Combine(_directory, _fileName));
                ConfigNode.LoadObjectFromConfig(this, cnToLoad);

                KCT_GUI.AssignRandomCrew = RandomizeCrew;
            }
        }

        public void Save()
        {
            Directory.CreateDirectory(_directory);
            ConfigNode cnTemp = ConfigNode.CreateConfigFromObject(this, new ConfigNode());
            cnTemp.Save(Path.Combine(_directory, _fileName));
        }
    }
}

/*
    KerbalConstructionTime (c) by Michael Marvin, Zachary Eck

    KerbalConstructionTime is licensed under a
    Creative Commons Attribution-NonCommercial-ShareAlike 4.0 International License.

    You should have received a copy of the license along with this
    work. If not, see <http://creativecommons.org/licenses/by-nc-sa/4.0/>.
*/
