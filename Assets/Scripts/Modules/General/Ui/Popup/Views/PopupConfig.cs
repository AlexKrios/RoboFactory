namespace Modules.General.Ui.Popup.Views
{
    public class PopupConfig
    {
        public PopupObject InitPopupHaveNotParts()
        {
            return new PopupObject
            {
                text = "Нехватает ингредиентов"
            };
        }

        public PopupObject InitPopupHaveNotCells()
        {
            return new PopupObject
                
            {
                text = "Нехватает ячеек для крафта"
            };
        }

        public PopupObject InitPopupBuyNewCraftCell()
        {
            return new PopupObject
            {
                text = "Вы хотите купить новый слот?"
            };
        }
    }
}
