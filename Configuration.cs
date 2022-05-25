using System.Collections.Generic;
using RFStorageModifier.Models;
using Rocket.API;

namespace RFStorageModifier
{
    public class Configuration : IRocketPluginConfiguration
    {
        public bool Enabled;
        public HashSet<Storage> Storages;
        public void LoadDefaults()
        {
            Enabled = true;
            Storages = new HashSet<Storage>
            {
                new() {ItemId = 328, Height = 20, Width = 10},
                new() {ItemId = 367, Height = 20, Width = 10},
            };
        }
    }
}