using System.Collections.Generic;
using System.Linq;
using RFStorageModifier.Models;
using RFStorageModifier.Utils;
using Rocket.API.Collections;
using Rocket.Core.Plugins;
using Rocket.Unturned.Chat;
using SDG.Unturned;
using UnityEngine;
using Logger = Rocket.Core.Logging.Logger;

namespace RFStorageModifier
{
    public class Plugin : RocketPlugin<Configuration>
    {
        private static int Major = 1;
        private static int Minor = 0;
        private static int Patch = 2;
        
        public static Plugin Inst;
        public static Configuration Conf;
        internal static HashSet<Storage> ModifiedStorages;

        protected override void Load()
        {
            Inst = this;
            Conf = Configuration.Instance;
            if (Conf.Enabled)
            {
                ModifiedStorages = new HashSet<Storage>();
                
                Level.onPreLevelLoaded += OnPrePreLevelLoaded;
                Level.onPostLevelLoaded += OnPostLevelLoaded;

                if (Level.isLoaded)
                    OnPostLevelLoaded(0);
            }
            else
                Logger.LogWarning($"[{Name}] Plugin: DISABLED");

            Logger.LogWarning($"[{Name}] Plugin loaded successfully!");
            Logger.LogWarning($"[{Name}] {Name} v{Major}.{Minor}.{Patch}");
            Logger.LogWarning($"[{Name}] Made with 'rice' by RiceField Plugins!");
        }

        protected override void Unload()
        {
            if (Conf.Enabled)
            {
                Level.onPreLevelLoaded -= OnPrePreLevelLoaded;
            }
            
            Conf = null;
            Inst = null;

            Logger.LogWarning($"[{Name}] Plugin unloaded successfully!");
        }
        
        private static void OnPrePreLevelLoaded(int level)
        {
            foreach (var storage in Conf.Storages)
                StorageUtil.ModifyAsset(storage);
        }

        private void OnPostLevelLoaded(int level)
        {
            foreach (var region in BarricadeManager.regions)
            {
                foreach (var drop in region.drops)
                {
                    if (drop.interactable is not InteractableStorage storage)
                        continue;

                    var replace = Conf.Storages.FirstOrDefault(x => x.ItemId == drop.asset.id);
                    if (replace == null)
                        continue;
                        
                    StorageUtil.ModifyBarricade(storage, replace);
                }
            }
                
            foreach (var region in BarricadeManager.vehicleRegions)
            {
                foreach (var drop in region.drops)
                {
                    if (drop.interactable is not InteractableStorage storage)
                        continue;

                    var replace = Conf.Storages.FirstOrDefault(x => x.ItemId == drop.asset.id);
                    if (replace == null)
                        continue;
                        
                    StorageUtil.ModifyBarricade(storage, replace);
                }
            }
        }
    }
}