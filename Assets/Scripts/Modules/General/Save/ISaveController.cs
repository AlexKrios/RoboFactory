namespace Modules.General.Save
{
    public interface ISaveController
    {
        void InitSave();
        void CreateSave();
        void Save();

        void SaveSettings(bool isInit = false);
        void SaveMoney(bool isInit = false);
        void SaveLevel(bool isInit = false);
        void SaveStores(bool isInit = false);
        void SaveUnits(bool isInit = false);
        void SaveExpeditions(bool isInit = false);
        void SaveProduction(bool isInit = false);
        void SaveOrders(bool isInit = false);
    }
}