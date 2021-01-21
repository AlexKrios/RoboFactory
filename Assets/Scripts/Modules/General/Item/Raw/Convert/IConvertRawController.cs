namespace Modules.General.Item.Raw.Convert
{
    public interface IConvertRawController
    {
        bool IsEnoughRaw(string key, int star);
        void RemoveParts();
        void AddRaw();
    }
}
