//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//using UnityEngine.Advertisements;
////using UnityEngine.Purchasing;

//public class Reward : MonoBehaviour {

//    private static Reward instance;
//    public static Reward Instance
//    {
//        get
//        {
//            if (instance == null)
//            {
//                instance = GameObject.FindObjectOfType<Reward>();
//            }

//            return Reward.instance;
//        }
//    }

//    public RewardType rewardType = RewardType.None;
//    public enum RewardType
//    {
//        None,
//        Currency,
//        Resource,
//    }

//    public float CurrencyReward;
//    public float ResourceReward;
//    public int ResourceID;

//    public Button ButtonYes;
//    public Button ButtonNo;

//    public Text Text;

//    public CompleteReward CompleteReward;

//    public void Begin(float currencyReward)
//    {
//        rewardType = RewardType.Currency;
//        CurrencyReward = currencyReward;

//        ButtonYes.onClick.AddListener(GainReward);
//        ButtonNo.onClick.AddListener(Hide);

//        Show();
//    }

//    public void Begin(int resourceID, float resourceReward)
//    {
//        rewardType = RewardType.Resource;
//        ResourceID = resourceID;
//        ResourceReward = resourceReward;

//        ButtonYes.onClick.AddListener(GainReward);
//        ButtonNo.onClick.AddListener(Hide);

//        Show();
//    }

//    private void Show()
//    {
//        this.gameObject.SetActive(true);

//        switch (rewardType)
//        {
//            default:
//                break;
//            case RewardType.Currency:
//                Text.text = "Would you like to to watch a video for " +
//                    "<color=#FBFF00FF>" + CurrencyConverter.Instance.GetCurrencyIntoString(CurrencyReward) + "</color>?";
//                break;
//            case RewardType.Resource:
//                Text.text = "Would you like to to watch a video for " + "<color=#002CFFFF>" + CurrencyConverter.Instance.GetCurrencyIntoStringNoSign(ResourceReward) + "</color>?";
//                break;
//        }
//    }

//#if !UNITY_ADS // If the Ads service is not enabled...
//    public string gameId; // Set this value from the inspector.
//    public bool enableTestMode = true;
//#endif

//    IEnumerator StartAdvertisement()
//    {
//#if !UNITY_ADS // If the Ads service is not enabled...
//        if (Advertisement.isSupported)
//        { // If runtime platform is supported...
//            Advertisement.Initialize(gameId, enableTestMode); // ...initialize.
//        }
//#endif

//        // Wait until Unity Ads is initialized,
//        // and the default ad placement is ready.
//        while (!Advertisement.isInitialized || !Advertisement.IsReady())
//        {
//            yield return new WaitForSeconds(0.5f);
//        }

//        ShowOptions show = new ShowOptions(){
//            gamerSid = "Video",
//            resultCallback = HandleShowResult
//        };

//        // Show the default ad placement.
//        Advertisement.Show(show);
//    }

//    private void GainReward()
//    {
//        StartCoroutine(StartAdvertisement());
//    }

//    public void HandleShowResult(ShowResult result)
//    {
//        switch (result)
//        {
//            case ShowResult.Finished:
//                // Add code here to rewarding players for watching ads without skipping.
//                //  Note: This will be the same result for picture ads when shown.
//                Debug.Log("The ad was successfully shown.");
//                Task();
//                break;
//            case ShowResult.Skipped:
//                Debug.Log("The ad was skipped before reaching the end.");
//                Hide();
//                break;
//            case ShowResult.Failed:
//                Debug.LogError("The ad failed to be shown.");
//                Hide();
//                break;
//        }
//    }

//    private void Task()
//    {
//        string rewardText = "";

//        switch (rewardType)
//        {
//            default:
//                break;
//            case RewardType.Currency:
//                rewardText = "Reward " + "<color=#FBFF00FF>" + CurrencyConverter.Instance.GetCurrencyIntoString(CurrencyReward) + "</color>";
//                break;
//            case RewardType.Resource:
//                rewardText = "Reward " + "<color=#002CFFFF>" + CurrencyConverter.Instance.GetCurrencyIntoStringNoSign(ResourceReward) + "</color>";
//                break;
//        }

//        CompleteReward.Show(rewardText);

//        CompleteReward.ButtonOk.onClick.AddListener(Complete);
//    }

//    private void Complete()
//    {
//        RewardFireworks.Instance.Play();

//        switch (rewardType)
//        {
//            default:
//                break;
//            case RewardType.Currency:
//                Game.Instance.AddGold(CurrencyReward);
//                break;
//            case RewardType.Resource:
//                Inventory.Instance.AddItem(ResourceID, ResourceReward);
//                break;
//        }

//        Hide();
//        CompleteReward.Hide();
//    }

//    public void Hide()
//    {
//        this.gameObject.SetActive(false);
//    }
//}
