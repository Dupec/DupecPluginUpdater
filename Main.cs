using Exiled.API.Features;
using Exiled.Events;
using GServer = Exiled.Events.Handlers.Server;
using GPlayer = Exiled.Events.Handlers.Player;
using Exiled.API.Enums;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using UnityEngine;
using System;
using Exiled.Events.EventArgs;
using System.Diagnostics;
//This code is weird haha
namespace DupecExiledUpdater
{
    public static class DupecUpdater
    {
        private static string pluginPath;
        public static string plugin;
        public static string pluginDLL;
        public static string pluginVersion;
        public static string githubAuthor;
        public static string githubName;
        public static string pastebinID;
        public static string ppt = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + $"\\EXILED\\DupecUpdater\\";
        public static void StartUpdate(string pluginName, string pluginDllName, string pluginVersions, string githubAuthors, string githubRepositoryName, string pastebinId)
        {
            plugin = pluginName;
            pluginDLL = pluginDllName;
            pluginVersion = pluginVersions;
            githubAuthor = githubAuthors;
            githubName = githubRepositoryName;
            pastebinID = pastebinId;
            pluginPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + $"\\EXILED\\DupecUpdater\\{plugin}\\";
            
            ex2led();
    }
        static void downloadFile()
        {
            Exiled.API.Features.Log.Debug($"Initializing Updater...");
            File.WriteAllText(pluginPath + "installedVersion.txt", $"{pluginVersion}");
            WebClient webClient = new WebClient();
            webClient.DownloadFile($"https://pastebin.com/raw/{pastebinID}", pluginPath + "websiteVersion.txt");
            Task.Delay(1000).ContinueWith(t =>
            {

                checkingVersion();
            });
        }

        static void checkingVersion()
        {
            
            string clientVersion = File.ReadAllText(pluginPath + "installedVersion.txt");
            WebClient webClient = new WebClient();
            Exiled.API.Features.Log.Debug($"Checking {plugin} version...");
            string webVersion = File.ReadAllText(pluginPath + $"websiteVersion.txt");
            
            if (clientVersion == webVersion)
            {
                Exiled.API.Features.Log.Debug($"You are Up-To date!");
            }
            else
            {
                Exiled.API.Features.Log.Debug($"{plugin} {webVersion} is available for download at https://github.com/{githubAuthor}/{githubName}/");
            }
        }
        static void ex2led()
        {
            if (!Directory.Exists(ppt))
            {
                Directory.CreateDirectory(ppt);
                Exiled.API.Features.Log.Debug($"Directory %appdata%/EXILED/DupecUpdater/ does not exist server is restarting...");
                Round.Restart();
            }
            else if(!Directory.Exists(pluginPath))
            {
                Directory.CreateDirectory(pluginPath);
                File.Create(pluginPath + "installedVersion.txt");
                File.Create(pluginPath + "websiteVersion.txt");
                Exiled.API.Features.Log.Debug($"Directory %appdata%/EXILED/DupecUpdater/{plugin}/ does not exist server is restarting...");
                Round.Restart();
            }
            else
            {
                downloadFile();
            }
        }
    }
}