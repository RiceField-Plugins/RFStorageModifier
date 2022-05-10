using HarmonyLib;
using RFStorageModifier.Models;
using SDG.Unturned;

namespace RFStorageModifier.Utils
{
    internal static class StorageUtil
    {
        internal static void Modify(Storage storage)
        {
            var asset = Assets.find(EAssetType.ITEM, storage.ItemId);
            if (asset is not ItemStorageAsset storageAsset)
                return;

            var bag = Traverse.Create(storageAsset);
            bag.Field("_storage_x").SetValue(storage.Width);
            bag.Field("_storage_y").SetValue(storage.Height);
        }
    }
}