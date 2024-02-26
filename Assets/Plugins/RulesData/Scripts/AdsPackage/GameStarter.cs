using Services.LoadingScreenService;
using Services.SceneLoader;
using Services.ScreenOrientationService;
using UnityEngine;
using WebView;

namespace SportsRules
{
    public class GameStarter : MonoBehaviour
    {
        [SerializeField]
        private SettingStrings config;

        [SerializeField]
        private Opener curtainPrefab;

        [SerializeField]
        private Ruler connector;

        private SceneServiceManager _sceneLoader;
        private BonusWheelUpgrade _adsController;
        private Opener _loader;
        private SizeFinder _screenResizer;

        private void Awake()
        {
            var curtain = Instantiate(curtainPrefab);
            _loader = curtain.GetComponent<Opener>();

            _sceneLoader = new SceneServiceManager();
            _screenResizer = new SizeFinder();
            _adsController = new BonusWheelUpgrade(_screenResizer, config);
            _adsController.OnSiteFinished += OnSiteLoaded;

            //_screenRotator.DisablePortrait();

            connector.Init(config, _sceneLoader, curtain, _adsController);
            connector.ChooseScene();

            DontDestroyOnLoad(this);
        }

        private void OnDestroy()
        {
            _adsController.OnSiteFinished -= OnSiteLoaded;
        }

        private void OnSiteLoaded()
        {
            _loader.Hide();
        }
    }
}
