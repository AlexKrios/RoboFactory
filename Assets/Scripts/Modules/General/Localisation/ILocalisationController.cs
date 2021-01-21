namespace Modules.General.Localisation
{
    public interface ILocalisationController
    {
        void LoadLocalisationData();
        string GetLanguageValue(string key);
    }
}