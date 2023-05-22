using Components.Scripts.Modules.General.Asset;
using Components.Scripts.Modules.General.Item.Products.Object;
using Components.Scripts.Modules.General.Item.Products.Types;
using Components.Scripts.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Components.Scripts.Modules.General.Ui.Common.Menu
{
    [AddComponentMenu("Scripts/General/Menu/Spec Cell View")]
    public class SpecCellView : MonoBehaviour
    {
        #region Zenject
        
        [Inject(Id = "IconUtil")] private readonly IconUtil _iconUtil;

        #endregion

        #region Components

        [SerializeField] private SpecType specType;
        
        [Space]
        [SerializeField] private Image icon;
        [SerializeField] private TMP_Text count;

        public SpecType SpecType => specType;
        
        #endregion

        public async void SetData(SpecObject spec)
        {
            var spriteRef = _iconUtil.GetSpecIcon(spec.type);
            icon.sprite = await AssetsManager.LoadAsset<Sprite>(spriteRef);
            count.text = spec.value.ToString();
        }
    }
}
