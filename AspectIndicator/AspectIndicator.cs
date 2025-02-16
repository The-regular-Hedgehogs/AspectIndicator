using BepInEx;
using R2API;
using RoR2;
using UnityEngine;

namespace AspectIndicator
{
    [BepInDependency(ItemAPI.PluginGUID)]
    [BepInDependency(LanguageAPI.PluginGUID)]
    [BepInPlugin(PluginGUID, PluginName, PluginVersion)]

    public class AspectIndicator : BaseUnityPlugin
    {
        public const string PluginGUID = PluginAuthor + "." + PluginName;
        public const string PluginAuthor = "AllOfThem";
        public const string PluginName = "AspectIndicator";
        public const string PluginVersion = "1.0.1";

        public void Awake()
        {
            Log.Init(Logger);
            On.RoR2.PickupDropletController.CreateCommandCube += On_PickupDropletController_CreateCommandCube;
        }

        private void On_PickupDropletController_CreateCommandCube(On.RoR2.PickupDropletController.orig_CreateCommandCube orig, PickupDropletController self)
        {
            var pickupDef = RoR2.PickupCatalog.GetPickupDef(self.pickupIndex);
            var equipmentDef = RoR2.EquipmentCatalog.GetEquipmentDef(pickupDef.equipmentIndex);

            if (equipmentDef != null && equipmentDef.passiveBuffDef != null)
            {
                Logger.LogInfo($"--- Is Elite Aspect Equipment (Buff: {equipmentDef.passiveBuffDef.name})");
                if (pickupDef != null)
                {
                    pickupDef.baseColor = new Color(0.6f, 0.2f, 1.0f);
                    pickupDef.darkColor = new Color(0.3f, 0.1f, 0.5f);
                }
            }

            orig(self);
        }
    }
}
