using RoboFactory.Auth;
using RoboFactory.General.Api;
using RoboFactory.General.Audio;
using RoboFactory.General.Expedition;
using RoboFactory.General.Item.Production;
using RoboFactory.General.Item.Products;
using RoboFactory.General.Item.Raw;
using RoboFactory.General.Level;
using RoboFactory.General.Location;
using RoboFactory.General.Money;
using RoboFactory.General.Order;
using RoboFactory.General.Scene;
using RoboFactory.General.Services;
using RoboFactory.General.Unit;
using RoboFactory.General.User;
using UnityEngine;
using Zenject;

namespace RoboFactory.Launcher
{
    public class LauncherInitialization : MonoBehaviour
    {
        [Inject] private readonly DiContainer _container;
        
        [Inject] private readonly AudioManager _audioController;
        [Inject] private readonly AuthService authService;

        [Inject] private readonly MoneyManager _moneyManager;
        [Inject] private readonly LevelManager _levelManager;
        [Inject] private readonly RawManager _rawManager;
        [Inject] private readonly ProductsManager _productsManager;
        [Inject] private readonly LocationManager _locationManager;
        [Inject] private readonly UnitsManager _unitsManager;
        [Inject] private readonly OrderManager _orderManager;
        [Inject] private readonly ProductionManager _productionManager;
        [Inject] private readonly ExpeditionManager _expeditionManager;

        [Inject] private readonly SceneService _sceneService;
        
        [Inject] private readonly ApiService _apiService;
        [Inject] private readonly UserProfile.Factory _userFactory;

        private async void Awake()
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 60;

            _sceneService.LoadScene(SceneName.Factory);
            //await SceneManager.LoadSceneAsync(SceneName.Loader.ToString(), LoadSceneMode.Additive).ToUniTask();

            InitAudio();
            InitAuthentication();
            
            var services = _container.ResolveAll<Service>();
            foreach (var service in services)
            {
                await service.Initialize();
            }
            
            foreach (var service in services)
            {
                await service.Load();
            }

            _sceneService.ProgressText.Value = "initialize_default";
            _sceneService.LoadState.Value = SceneLoadState.Finish;
        }

        private void OnDestroy()
        {
            authService.EventSignInFailure -= LoadAuthenticationScene;
        }

        private void InitAudio()
        {
            var musicGameObject = new GameObject("----- Music -----");
            musicGameObject.AddComponent<AudioListener>();
            _audioController.InitMusicSource(musicGameObject.AddComponent<AudioSource>());
            DontDestroyOnLoad(musicGameObject);
        }

        private void InitAuthentication()
        {
            authService.EventSignInSuccess += LoadServicesData;
            authService.EventSignInFailure += LoadAuthenticationScene;
            //authenticationService.Initialize();
        }
        
        private void LoadAuthenticationScene()
        {
            //_sceneService.LoadScene(SceneName.Authentication, 1f);
        }
        
        private async void LoadServicesData()
        {
            var userProfile = await _apiService.GetUserProfile();
            if (userProfile == null)
            {
                userProfile = _userFactory.Create().GetStartUserProfile();
                await _apiService.SetStartUserProfile(userProfile);
            }

            _moneyManager.LoadData(userProfile.MoneySection);
            _levelManager.LoadData(userProfile.LevelSection);

            _rawManager.LoadData(userProfile.StoresSection.Raw);
            _productsManager.LoadData(userProfile.StoresSection.Products);
            _locationManager.LoadData(userProfile.LocationsSection);
            _unitsManager.LoadData(userProfile.UnitsSection);

            _productionManager.LoadData(userProfile.ProductionsSection);
            _expeditionManager.LoadData(userProfile.ExpeditionsSection);

            _orderManager.LoadData(userProfile.OrdersSection);
        }
    }
}