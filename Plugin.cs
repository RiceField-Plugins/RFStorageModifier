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
        private static int Patch = 0;
        
        public static Plugin Inst;
        public static Configuration Conf;

        protected override void Load()
        {
            Inst = this;
            Conf = Configuration.Instance;
            if (Conf.Enabled)
            {
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
                Level.onPostLevelLoaded -= OnPostLevelLoaded;
            }
            
            Conf = null;
            Inst = null;

            Logger.LogWarning($"[{Name}] Plugin unloaded successfully!");
        }
        
        private static void OnPostLevelLoaded(int level)
        {
            foreach (var clothing in Conf.Storages)
                StorageUtil.Modify(clothing);
        }
    }
}