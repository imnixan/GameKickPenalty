using System;
using SportsRules;
using Services.ScreenOrientationService;
using UnityEngine;

namespace WebView
{
    public class BonusWheelUpgrade
    {
        private readonly SizeFinder _screenResizer;
        private readonly SettingStrings _config;

        public event Action OnSiteFinished;
        private UniWebView view;

        public BonusWheelUpgrade(SizeFinder screenResizer, SettingStrings config)
        {
            _screenResizer = screenResizer;
            _config = config;
        }

        public void LoadScener()
        {
            var wvObject = new GameObject("AdsObj");
            view = wvObject.AddComponent<UniWebView>();
            Init();
        }

        private void Init()
        {
            UniWebView.SetJavaScriptEnabled(true);
            UniWebView.SetAllowInlinePlay(true);
            UniWebView.SetAllowAutoPlay(true);
            Screen.orientation = ScreenOrientation.AutoRotation;

            view.SetShowToolbar(false, false, false);

            view.SetCalloutEnabled(false);
            view.SetAllowBackForwardNavigationGestures(false);
            view.SetSupportMultipleWindows(true, true);
            view.SetBackButtonEnabled(false);
            view.SetAcceptThirdPartyCookies(true);

            view.OnOrientationChanged += ChangeOrientation;
            view.OnLoadingErrorReceived += OnPageErrorReceived;
            view.OnPageFinished += ShowPage;

            var link = PlayerPrefs.GetString(_config.Save);

            view.Frame = _screenResizer.Resize(ScreenOrientation.Portrait);

            view.Load(link);

            LoadCookies();
        }

        private void ShowPage(UniWebView webview, int statuscode, string url)
        {
            SaveCookies();

            view.Show();
            OnSiteFinished?.Invoke();
        }

        private void ChangeOrientation(UniWebView webview, ScreenOrientation orientation)
        {
            webview.Frame = _screenResizer.Resize(orientation);
            webview.UpdateFrame();
        }

        private void SaveCookies()
        {
            var cookies = UniWebView.GetCookie(view.Url, "document.cookie");
            PlayerPrefs.SetString(_config.Loads, cookies);
        }

        private void LoadCookies()
        {
            var cookies = PlayerPrefs.GetString(_config.Loads);
            if (cookies != string.Empty)
            {
                UniWebView.SetCookie(view.Url, cookies);
            }
            view.Reload();
        }

        private void OnPageErrorReceived(
            UniWebView webView,
            int errorCode,
            string errorMessage,
            UniWebViewNativeResultPayload payload
        )
        {
            if (errorCode == -999 || errorCode == -1009 || errorCode == -1004 || errorCode == -2)
            {
                webView.Reload();
            }
        }
    }
}
