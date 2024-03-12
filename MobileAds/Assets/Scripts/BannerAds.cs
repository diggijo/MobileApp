using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class BannerAds : MonoBehaviour
{
    [SerializeField] string _androidAdUnitId = "Banner_Android";
    [SerializeField] string _iOsAdUnitId = "Banner_iOS";
    string _adUnitId;

    private void Awake()
    {
        #if UNITY_IOS
            _adUnitId = _iOSAdUnitId;
        #elif UNITY_ANDROID
            _adUnitId = _androidAdUnitId;
#endif

        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
    }

    public void LoadBannerAd()
    {
        BannerLoadOptions bannerLoadOptions = new BannerLoadOptions
        {
            loadCallback = BannerLoaded,
            errorCallback = BannerLoadedError
        };

        Advertisement.Banner.Load(_adUnitId, bannerLoadOptions);
    }

    public void ShowBannerAd()
    {
        BannerOptions bannerOptions = new BannerOptions
        {
            showCallback = BannerShown,
            clickCallback = BannerClicked,
            hideCallback = BannerHidden
        };

        Advertisement.Banner.Show(_adUnitId, bannerOptions);
    }

    private void BannerShown()  {   }

    private void BannerClicked()    {   }

    private void BannerHidden() {   }

    private void BannerLoaded()
    {
        throw new NotImplementedException();
    }

    private void BannerLoadedError(string message)
    {
        throw new NotImplementedException();
    }

    public void HideBannerAd()
    {
        Advertisement.Banner.Hide();
    }
}
