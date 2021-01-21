using Modules.General.Item.Products.Models.Object.Spec;
using Modules.General.Item.Products.Models.Types;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils;
using Zenject;

namespace Modules.General.Ui.Common.Menu
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
        [SerializeField] private TextMeshProUGUI count;

        public SpecType SpecType => specType;
        
        #endregion

        public void SetData(SpecObject spec)
        {
            icon.sprite = _iconUtil.GetSpecIcon(spec.type);
            count.text = spec.value.ToString();
        }
    }
}
