using System.IO;
using Modules.Authentication;
using Modules.General;
using Modules.General.Audio;
using Modules.General.Item.Production;
using Modules.General.Item.Products;
using Modules.General.Item.Raw;
using Modules.General.Level;
using Modules.General.Localisation;
using Modules.General.Location;
using Modules.General.Money;
using Modules.General.Order;
using Modules.General.Save;
using Modules.General.Scene;
using Modules.General.Settings;
using Modules.General.Unit;
using Newtonsoft.Json;
using Unity.Services.Core;
using UnityEngine;
using Zenject;

namespace Modules.Launcher
{
    [AddComponentMenu("Scripts/Launcher/Launcher Initialization", 0)]
    public class LauncherInitialization : MonoBehaviour
    {
        [Inject] private readonly ILocalisationController _localisationController;
        [Inject] private readonly IAudioController _audioController;
        [Inject] private readonly AuthenticationManager _authenticationManager;
        [Inject] private readonly ISettingsController _settingsController;

        [Inject] private readonly IMoneyController _moneyController;
        [Inject] private readonly ILevelController _levelController;
        [Inject] private readonly IRawController _rawController;
        [Inject] private readonly IProductsController _productsController;
        [Inject] private readonly IUnitsController _unitsController;
        [Inject] private readonly IOrderController _orderController;
        [Inject] private readonly IProductionController _productionController;
        [Inject] private readonly IExpeditionController _expeditionController;

        [Inject] private readonly ISaveController _saveController;

        [Inject] private readonly SceneController _sceneController;

        private async void Awake()
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 500;
            
            await UnityServices.InitializeAsync();
            
            InitAudio();
            InitLocalisation();
            InitAuthentication();
        }
        
        private void OnDestroy()
        {
            _authenticationManager.EventSignInFailure -= LoadAuthenticationScene;
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
            _localisationController.LoadLocalisationData();
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
        
        private void LoadServicesData()
        {
            if (File.Exists(Constants.SavePath))
            {
                var jsonText = File.ReadAllText(Constants.SavePath);
                var startData = JsonConvert.DeserializeObject<LoadObject>(jsonText);

                _settingsController.LoadSettingsData(startData.settingsInfo);

                _moneyController.LoadData(startData.moneyInfo);
                _levelController.LoadData(startData.levelInfo);

                _rawController.LoadRawData(startData.storesInfo.raw);
                _productsController.LoadProductsData(startData.storesInfo.products);
                _unitsController.LoadUnitsInfo(startData.unitsInfo);

                _productionController.LoadStoreData(startData.productionsInfo);
                _expeditionController.LoadStoreData(startData.expeditionsInfo);

                _orderController.LoadOrders(startData.ordersInfo);

                _saveController.InitSave();
            }
            else
                _saveController.CreateSave();

            _sceneController.LoadScene(SceneName.Factory, 1f);
        }
    }
}