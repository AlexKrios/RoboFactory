using Cysharp.Threading.Tasks;
using RoboFactory.Authentication;
using RoboFactory.General.Api;
using RoboFactory.General.Audio;
using RoboFactory.General.Expedition;
using RoboFactory.General.Item.Production;
using RoboFactory.General.Item.Products;
using RoboFactory.General.Item.Raw;
using RoboFactory.General.Level;
using RoboFactory.General.Localisation;
using RoboFactory.General.Location;
using RoboFactory.General.Money;
using RoboFactory.General.Order;
using RoboFactory.General.Scene;
using RoboFactory.General.Settings;
using RoboFactory.General.Unit;
using RoboFactory.General.User;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace RoboFactory.Launcher
{
    [AddComponentMenu("Scripts/Launcher/Launcher Initialization", 0)]
    public class LauncherInitialization : MonoBehaviour
    {
        [Inject] private readonly LocalisationManager _localisationManager;
        [Inject] private readonly AudioManager _audioController;
        [Inject] private readonly AuthenticationManager _authenticationManager;
        [Inject] private readonly SettingsManager _settingsManager;

        [Inject] private readonly MoneyManager _moneyManager;
        [Inject] private readonly LevelManager _levelManager;
        [Inject] private readonly RawManager _rawManager;
        [Inject] private readonly ProductsManager _productsManager;
        [Inject] private readonly LocationManager _locationManager;
        [Inject] private readonly UnitsManager _unitsManager;
        [Inject] private readonly OrderManager _orderManager;
        [Inject] private readonly ProductionManager _productionManager;
        [Inject] private readonly ExpeditionManager _expeditionManager;

        [Inject] private readonly SceneController _sceneController;
        
        [Inject] private readonly ApiManager _apiManager;
        [Inject] private readonly UserProfile.Factory _userFactory;

        private async void Awake()
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 500;

            await InitAddressables();
            InitAudio();
            InitLocalisation();
            InitAuthentication();
        }

        private void OnDestroy()
        {
            _authenticationManager.EventSignInFailure -= LoadAuthenticationScene;
        }

        private async UniTask InitAddressables()
        {
            await Addressables.InitializeAsync();
        }

        private void InitAudio()
        {
            var musicGameObject = new GameObject("----- Music -----");
            musicGameObject.AddComponent<AudioListener>();
            _audioController.InitMusicSource(musicGameObject.AddComponent<AudioSource>());
            DontDestroyOnLoad(musicGameObject);
        }

        private void InitLocalisation()
        {
            _localisationManager.LoadLocalisationData();
        }
        
        private void InitAuthentication()
        {
            _authenticationManager.EventSignInSuccess += LoadServicesData;
            _authenticationManager.EventSignInFailure += LoadAuthenticationScene;
            _authenticationManager.Initialize();
        }
        
        private void LoadAuthenticationScene()
        {
            _sceneController.LoadScene(SceneName.Authentication, 1f);
        }
        
        private async void LoadServicesData()
        {
            var userProfile = await _apiManager.GetUserProfile();
            if (userProfile == null)
            {
                userProfile = _userFactory.Create().GetStartUserProfile();
                _settingsManager.InitData();
                await _apiManager.SetStartUserProfile(userProfile);
            }
                
            _settingsManager.LoadData();
                
            _moneyManager.LoadData(userProfile.MoneySection);
            _levelManager.LoadData(userProfile.LevelSection);

            _rawManager.LoadData(userProfile.StoresSection.Raw);
            _productsManager.LoadData(userProfile.StoresSection.Products);
            _locationManager.LoadData(userProfile.LocationsSection);
            _unitsManager.LoadData(userProfile.UnitsSection);

            _productionManager.LoadData(userProfile.ProductionsSection);
            _expeditionManager.LoadData(userProfile.ExpeditionsSection);

            _orderManager.LoadData(userProfile.OrdersSection);

            _sceneController.LoadScene(SceneName.Factory, 1f);
        }
    }
}