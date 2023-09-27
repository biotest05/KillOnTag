using BepInEx;
using System;
using UnityEngine;
using Utilla;
using HoneyLib;
using HoneyLib.Events;
using Photon.Pun;

namespace KillTag
{
    /// <summary>
    /// This is your mod's main class.
    /// </summary>

    /* This attribute tells Utilla to look for [ModdedGameJoin] and [ModdedGameLeave] */
    [ModdedGamemode]
    [BepInDependency("com.buzzbzzzbzzbzzzthe18th.gorillatag.HoneyLib", "1.0.9")]
    [BepInDependency("org.legoandmars.gorillatag.utilla", "1.5.0")]
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class Plugin : BaseUnityPlugin
    {
        bool inRoom;
        bool modEnabled = true;

        void Start()
        {
            Utilla.Events.GameInitialized += OnGameInitialized;
            HoneyLib.Events.Events.InfectionTagEvent += OnTag;
        }

        void OnTag(object sender, InfectionTagEventArgs e)
        {
            if (modEnabled && inRoom)
            {
                if (e.taggedPlayer.IsLocal && e.taggingPlayer != null)
                {
                    Application.Quit();
                }
            }
        }

        void OnEnable()
        {
            modEnabled = true;
            HarmonyPatches.ApplyHarmonyPatches();
        }

        void OnDisable()
        {
            modEnabled = false;
            HarmonyPatches.RemoveHarmonyPatches();
        }

        void OnGameInitialized(object sender, EventArgs e)
        {
            
        }

        void Update()
        {
            
        }

        [ModdedGamemodeJoin]
        public void OnJoin(string gamemode)
        {
            inRoom = true;
        }

        [ModdedGamemodeLeave]
        public void OnLeave(string gamemode)
        {
            inRoom = false;
        }
    }
}
