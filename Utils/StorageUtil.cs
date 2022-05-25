using HarmonyLib;
using RFStorageModifier.Models;
using SDG.Unturned;

namespace RFStorageModifier.Utils
{
    internal static class StorageUtil
    {
        internal static void ModifyAsset(Storage storage)
        {
            var asset = Assets.find(EAssetType.ITEM, storage.ItemId);
            if (asset is not ItemStorageAsset storageAsset)
                return;

            var bag = Traverse.Create(storageAsset);
            bag.Field("_storage_x").SetValue(storage.Width);
            bag.Field("_storage_y").SetValue(storage.Height);
            Plugin.ModifiedStorages.Add(storage);
        }

        internal static void ModifyBarricade(InteractableStorage interactableStorage, Storage storage)
        {
            if (!Plugin.ModifiedStorages.Contains(storage))
                ModifyAsset(storage);

            interactableStorage.items.resize(storage.Width, storage.Height);
        }
    }
}