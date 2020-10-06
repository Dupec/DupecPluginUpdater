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
//This code is weird haha
namespace VotingSystem
{
    public static class DupecUpdater
    {

        public static string plugin;
        public static string pluginDLL;
        public static string pluginVersion;
        public static string githubAuthor;
        public static string githubName;
        public static string pastebinID;
        public static void StartUpdate(string pluginName, string pluginDllName, string pluginVersions, string githubAuthors, string githubRepositoryName, string pastebinId)
        {
            plugin = pluginName;
            pluginDLL = pluginDllName;
            pluginVersion = pluginVersions;
            githubAuthor = githubAuthors;
            githubName = githubRepositoryName;
            pastebinID = pastebinId;
            ex2led();
    }
        static void downloadFile()
        {
            Exiled.API.Features.Log.Debug($"Initializing Updater...");
            WebClient webClient = new WebClient();
            webClient.DownloadFile($"https://pastebin.com/raw/{pastebinID}", $"C:\\{plugin}\\Server\\version.txt");
            Task.Delay(1000).ContinueWith(t =>
            {

                checkingVersion();
            });
        }

        static void checkingVersion()
        {
            
            string clientVersion = File.ReadAllText($"C:\\{plugin}\\Client\\version.txt");
            WebClient webClient = new WebClient();
            Exiled.API.Features.Log.Debug($"Checking {plugin} version...");
            string webVersion = File.ReadAllText($"C:\\{plugin}\\Server\\version.txt");
            
            if (clientVersion == webVersion)
            {
                Exiled.API.Features.Log.Debug($"You are Up-To date!");
            }
            else
            {
                Exiled.API.Features.Log.Debug($"{plugin} {pluginVersion} is available for download at https://github.com/{githubAuthor}/{githubName}/");
            }
        }
        static void ex2led()
        {
            System.Console.WriteLine($"Checking for Updates...");
            if (!Directory.Exists($"C:\\{plugin}\\"))
            {
                System.Console.WriteLine($"Directory {plugin} not found!, Creating...");
                Directory.CreateDirectory($"C:\\{plugin}\\");
                Directory.CreateDirectory($"C:\\{plugin}\\dll\\");

            }
            else if (!Directory.Exists($"C:\\{plugin}\\Server\\"))
            {
                System.Console.WriteLine($"Directory {plugin}/Server/ not found!, Creating...");
                Directory.CreateDirectory($"C:\\{plugin}\\Server\\");
                ex2led();
            }
            else if (!Directory.Exists($"C:\\{plugin}\\Client\\"))
            {
                Exiled.API.Features.Log.Debug($"Directory {plugin}/Client/ not found!, Creating...");
                Directory.CreateDirectory($"C:\\{plugin}\\Client\\");
                File.Create($"C:\\{plugin}\\Client\\version.txt");

                File.WriteAllText($"C:\\{plugin}\\Client\\version.txt", $"{pluginVersion}");
                ex2led();
            }
            else
            {
                File.WriteAllText($"C:\\{plugin}\\Client\\version.txt", $"{pluginVersion}");
                downloadFile();
            }
        }
    }
}