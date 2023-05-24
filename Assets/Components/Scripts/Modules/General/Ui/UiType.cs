namespace RoboFactory.General.Ui
{
    public enum UiType
    {
        None = 0,
        Settings = 1,
        Production = 2,
        Storage = 3,
        Conversion = 4,
        Units = 5,
        Expedition = 6,
        Battle = 7,
        Order = 8,
        
        UpgradeProduction = 121,
        UpgradeProductionQueue = 126,
        SelectUnitEquipment = 151,
        SelectExpeditionUnit = 161,
        ExpeditionComplete = 164,
        UpgradeExpeditionQueue = 166,
        
        DontHaveIngredients = 901,
        DontHaveProductionCells = 902,
        StorageFull = 903
    }
}