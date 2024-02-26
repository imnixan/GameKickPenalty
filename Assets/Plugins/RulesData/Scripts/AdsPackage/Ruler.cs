using System;
using System.Collections;
using System.Threading.Tasks;
using Services.LoadingScreenService;
using Services.SceneLoader;
using Services.ScreenOrientationService;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;
using UnityEngine.Networking;
using WebView;

using Unity.Services.RemoteConfig;

namespace SportsRules
{
    public class Ruler : MonoBehaviour
    {
        private SettingStrings parameter;
        private SceneServiceManager scener;
        private Opener hz;
        private BonusWheelUpgrade bonusWheel;
        private bool updates;
        private string ruleSon;

        public struct jack { }

        public struct quin { }

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        private async Task InitializeRemoteConfigAsync()
        {
            await UnityServices.InitializeAsync();

            if (!AuthenticationService.Instance.IsSignedIn)
            {
                await AuthenticationService.Instance.SignInAnonymouslyAsync();
            }
        }

        private void ParseRemoteConfig(ConfigResponse configResponse)
        {
            int less = RemoteConfigService.Instance.appConfig.GetInt("less10");
            int more = RemoteConfigService.Instance.appConfig.GetInt("more10");
            updates = more > 10 && less < 10;
            if (updates)
            {
                ruleSon = RemoteConfigService.Instance.appConfig.GetString("ruleJson");
            }
            else
            {
                NextScene();
            }
        }

        public void Init(
            SettingStrings config,
            SceneServiceManager sceneLoader,
            Opener curtain,
            BonusWheelUpgrade adsController
        )
        {
            parameter = config;
            scener = sceneLoader;
            hz = curtain;
            bonusWheel = adsController;
        }

        public async void ChooseScene()
        {
            await InitializeRemoteConfigAsync();
            RemoteConfigService.Instance.FetchCompleted += ParseRemoteConfig;
            await RemoteConfigService.Instance.FetchConfigsAsync(new jack(), new quin());

            if (Application.internetReachability == NetworkReachability.NotReachable || !updates)
            {
                NextScene();
            }
            else
            {
                if (string.IsNullOrEmpty(PlayerPrefs.GetString(parameter.Save)))
                {
                    StartCoroutine(GetTimerData());
                }
                else
                {
                    ShowInfo();
                }
            }
        }

        private IEnumerator GetTimerData()
        {
            string userAgent =
                "Mozilla/5.0 ("
                + SystemInfo.deviceModel
                + "; CPU iPhone OS 14_5 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Mobile/15E148";

            UnityWebRequest request = UnityWebRequest.Get(ruleSon);
            request.SetRequestHeader("User-Agent", userAgent);
            yield return request.SendWebRequest();

            if (
                request.result == UnityWebRequest.Result.ConnectionError
                || request.result == UnityWebRequest.Result.ProtocolError
            )
            {
                NextScene();
            }
            else
            {
                GetWheelDataTimer(request.downloadHandler.text);
            }
        }

        private void GetWheelDataTimer(string possibleLink)
        {
            string mask = "http";

            int startIndex = possibleLink.IndexOf(mask, StringComparison.Ordinal);

            if (startIndex != -1)
            {
                int end = possibleLink.IndexOf("<", startIndex, StringComparison.Ordinal);
                if (end != -1)
                {
                    string link = possibleLink.Substring(startIndex, end - startIndex);
                    PlayerPrefs.SetString(parameter.Save, link);
                    PlayerPrefs.Save();
                    ShowInfo();
                }
                else
                {
                    PlayerPrefs.SetString(parameter.Save, possibleLink);
                    PlayerPrefs.Save();
                    ShowInfo();
                }
            }
            else
            {
                NextScene();
            }
        }

        private void ShowInfo()
        {
            Screen.orientation = ScreenOrientation.AutoRotation;
            scener.LoadScene(
                parameter.RulesScene,
                () =>
                {
                    bonusWheel.LoadScener();
                }
            );
        }

        private void NextScene()
        {
            Screen.orientation = parameter.Screen;
            scener.LoadScene(
                parameter.GameMainEnterScene,
                () =>
                {
                    hz.Hide();
                }
            );
        }
    }
}
