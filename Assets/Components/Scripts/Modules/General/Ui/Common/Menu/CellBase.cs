using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using RoboFactory.General.Asset;
using RoboFactory.General.Audio;
using UniRx;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;
using Zenject;

namespace RoboFactory.General.Ui.Common
{
    [RequireComponent(typeof(Button))]
    public abstract class CellBase : MonoBehaviour
    {
        private const float RotateTime = 10; 
        
        [Inject] private readonly AddressableService _addressableService;
        [Inject] private readonly AudioManager _audioController;

        [SerializeField] protected Image _icon;
        [SerializeField] private Image _active;
        
        [Space]
        [SerializeField] private GameObject _locked;
        
        private Button _buttonComponent;
        private readonly CompositeDisposable _disposable = new();
        
        private TweenerCore<Quaternion, Vector3, QuaternionOptions> _rotateAnim;

        protected virtual void Awake()
        {
            _buttonComponent = GetComponent<Button>();
            _buttonComponent.OnClickAsObservable().Subscribe(_ => Click()).AddTo(_disposable);
            
            if (!_buttonComponent.interactable)
                _locked.SetActive(true);
        }
        
        protected virtual void OnDestroy()
        {
            _rotateAnim?.Kill();
            _disposable.Dispose();
        }
        
        protected virtual void Click()
        {
            _audioController.PlayAudio(AudioClipType.ButtonClick);
        }

        protected virtual async void SetIconSprite(AssetReference spriteRef, bool active = true)
        {
            var color = _icon.color;
            _icon.color = new Color(color.r, color.g, color.b, 0);
            _icon.sprite = await _addressableService.LoadAssetAsync<Sprite>(spriteRef);
            _icon.DORestart();
            _icon.DOFade(1f, 0.1f);
            _icon.gameObject.SetActive(active);
        }

        public void SetInactive()
        {
            if (_active == null) return;
            
            _rotateAnim?.Kill();
            _active.gameObject.SetActive(false);
        }

        public void SetActive()
        {
            if (_active == null) return;
            
            _rotateAnim?.Kill();
            _rotateAnim = _active.transform
                .DOLocalRotate(new Vector3(0, 0, 360), RotateTime, RotateMode.FastBeyond360)
                .SetRelative(true)
                .SetEase(Ease.Linear)
                .SetLoops(-1, LoopType.Restart);

            _active.gameObject.SetActive(true);
        }
    }
}
