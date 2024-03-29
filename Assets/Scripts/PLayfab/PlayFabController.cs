using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using System.Collections.Generic;

public class PlayFabController : MonoBehaviour
{
//    public static PlayFabController PFC;

//    private string userEmail;
//    private string userPassword;
//    private string username;
//    public GameObject loginPanel;
//    public GameObject addLoginPanel;
//    public GameObject recoverButton;

//    private void OnEnable()
//    {
//        if (PlayFabController.PFC == null)
//        {
//            PlayFabController.PFC = this;
//        }
//        else
//        {
//            if (PlayFabController.PFC != this)
//            {
//                Destroy(this.gameObject);
//            }
//        }
//        DontDestroyOnLoad(this.gameObject);
//    }

//    public void Start()
//    {
//        PlayerPrefs.DeleteAll();
//        //Note: Setting title Id here can be skipped if you have set the value in Editor Extensions already.
//        if (string.IsNullOrEmpty(PlayFabSettings.TitleId))
//        {
//            PlayFabSettings.TitleId = "8741"; // Please change this value to your own titleId from PlayFab Game Manager
//        }
//        //var request = new LoginWithCustomIDRequest { CustomId = "GettingStartedGuide", CreateAccount = true };
//        //PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);
//        if (PlayerPrefs.HasKey("EMAIL"))
//        {
//            userEmail = PlayerPrefs.GetString("EMAIL");
//            userPassword = PlayerPrefs.GetString("PASSWORD");
//            var request = new LoginWithEmailAddressRequest { Email = userEmail, Password = userPassword };
//            PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnLoginFailure);
//        }
//        else
//        {
//#if UNITY_ANDROID
//            var requestAndroid = new LoginWithAndroidDeviceIDRequest { AndroidDeviceId = ReturnMobileID(), CreateAccount = true };
//            PlayFabClientAPI.LoginWithAndroidDeviceID(requestAndroid, OnLoginMobileSuccess, OnLoginMobileFailure);
//#endif
//#if UNITY_IOS
//            var requestIOS = new LoginWithIOSDeviceIDRequest { DeviceId = ReturnMobileID(), CreateAccount = true };
//            PlayFabClientAPI.LoginWithIOSDeviceID(requestIOS, OnLoginMobileSuccess, OnLoginMobileFailure);
//#endif
//        }


//    }

//    #region Login
//    private void OnLoginSuccess(LoginResult result)
//    {
//        Debug.Log("Congratulations, you made your first successful API call!");
//        PlayerPrefs.SetString("EMAIL", userEmail);
//        PlayerPrefs.SetString("PASSWORD", userPassword);
//        loginPanel.SetActive(false);
//        recoverButton.SetActive(false);
//        GetStats();
//        //StartCloudHelloWorld();

//    }

//    private void OnLoginMobileSuccess(LoginResult result)
//    {
//        Debug.Log("Congratulations, you made your first successful API call!");
//        GetStats();
//        loginPanel.SetActive(false);
//    }

//    private void OnRegisterSuccess(RegisterPlayFabUserResult result)
//    {
//        Debug.Log("Congratulations, you made your first successful API call!");
//        PlayerPrefs.SetString("EMAIL", userEmail);
//        PlayerPrefs.SetString("PASSWORD", userPassword);

//        PlayFabClientAPI.UpdateUserTitleDisplayName(new UpdateUserTitleDisplayNameRequest { DisplayName = username }, OnDisplayName, OnLoginMobileFailure);
//        GetStats();
//        loginPanel.SetActive(false);
//    }

//    void OnDisplayName(UpdateUserTitleDisplayNameResult result)
//    {
//        Debug.Log(result.DisplayName + " is your new display name");
//    }

//    private void OnLoginFailure(PlayFabError error)
//    {
//        var registerRequest = new RegisterPlayFabUserRequest { Email = userEmail, Password = userPassword, Username = username };
//        PlayFabClientAPI.RegisterPlayFabUser(registerRequest, OnRegisterSuccess, OnRegisterFailure);
//    }

//    private void OnLoginMobileFailure(PlayFabError error)
//    {
//        Debug.Log(error.GenerateErrorReport());
//    }

//    private void OnRegisterFailure(PlayFabError error)
//    {
//        Debug.LogError(error.GenerateErrorReport());
//    }


//    public void GetUserEmail(string emailIn)
//    {
//        userEmail = emailIn;
//    }

//    public void GetUserPassword(string passwordIn)
//    {
//        userPassword = passwordIn;
//    }

//    public void GetUsername(string usernameIn)
//    {
//        username = usernameIn;
//    }

//    public void OnClickLogin()
//    {
//        var request = new LoginWithEmailAddressRequest { Email = userEmail, Password = userPassword };
//        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnLoginFailure);
//    }

//    public static string ReturnMobileID()
//    {
//        string deviceID = SystemInfo.deviceUniqueIdentifier;
//        return deviceID;
//    }

//    public void OpenAddLogin()
//    {
//        addLoginPanel.SetActive(true);
//    }

//    public void OnClickAddLogin()
//    {
//        var addLoginRequest = new AddUsernamePasswordRequest { Email = userEmail, Password = userPassword, Username = username };
//        PlayFabClientAPI.AddUsernamePassword(addLoginRequest, OnAddLoginSuccess, OnRegisterFailure);
//    }

//    private void OnAddLoginSuccess(AddUsernamePasswordResult result)
//    {
//        Debug.Log("Congratulations, you made your first successful API call!");
//        GetStats();
//        PlayerPrefs.SetString("EMAIL", userEmail);
//        PlayerPrefs.SetString("PASSWORD", userPassword);
//        addLoginPanel.SetActive(false);
//    }
//    #endregion Login

//    public int playerCollectedCoin;
//    public int playerKilledMonster;

//    public int playerHighScore;

//    #region PlayerStats

//    public void SetStats()
//    {
//        PlayFabClientAPI.UpdatePlayerStatistics(new UpdatePlayerStatisticsRequest
//        {
//            // request.Statistics is a list, so multiple StatisticUpdate objects can be defined if required.
//            Statistics = new List<StatisticUpdate> {
//                new StatisticUpdate { StatisticName = "PlayerKilledMonster", Value = playerKilledMonster },
//                new StatisticUpdate { StatisticName = "HighScoreTime", Value = playerHighScore },
//                new StatisticUpdate { StatisticName = "PlayerCollectedCoin", Value = playerCollectedCoin },

//            }
//        },
//        result => { Debug.Log("User statistics updated"); },
//        error => { Debug.LogError(error.GenerateErrorReport()); });
//    }

//    void GetStats()
//    {
//        PlayFabClientAPI.GetPlayerStatistics(
//            new GetPlayerStatisticsRequest(),
//            OnGetStats,
//            error => Debug.LogError(error.GenerateErrorReport())
//        );
//    }

//    void OnGetStats(GetPlayerStatisticsResult result)
//    {
//        Debug.Log("Received the following Statistics:");
//        foreach (var eachStat in result.Statistics)
//        {
//            Debug.Log("Statistic (" + eachStat.StatisticName + "): " + eachStat.Value);
//            switch (eachStat.StatisticName)
//            {
//                case "PlayerKilledMonster":
//                    playerKilledMonster = eachStat.Value;
//                    break;
//                case "PlayerCollectedCoin":
//                    playerCollectedCoin = eachStat.Value;
//                    break;
//                case "HighScoreTime":
//                    playerHighScore = eachStat.Value;
//                    break;
//            }
//        }
//    }

//    // Build the request object and access the API
//    public void StartCloudUpdatePlayerStats()
//    {
//        PlayFabClientAPI.ExecuteCloudScript(new ExecuteCloudScriptRequest()
//        {
//            FunctionName = "UpdatePlayerStats", // Arbitrary function name (must exist in your uploaded cloud.js file)
//            FunctionParameter = new { killedMonster = playerKilledMonster, highScore = playerHighScore, collectedCoin = playerCollectedCoin }, // The parameter provided to your function
//            GeneratePlayStreamEvent = true, // Optional - Shows this event in PlayStream
//        }, OnCloudUpdateStats, OnErrorShared);
//    }
//    // OnCloudHelloWorld defined in the next code block

//    private static void OnCloudUpdateStats(ExecuteCloudScriptResult result)
//    {
//        // Cloud Script returns arbitrary results, so you have to evaluate them one step and one parameter at a time
//        Debug.Log(JsonWrapper.SerializeObject(result.FunctionResult));
//        PlayFab.Json.JsonObject jsonResult = (PlayFab.Json.JsonObject)result.FunctionResult;
//        object messageValue;
//        jsonResult.TryGetValue("messageValue", out messageValue); // note how "messageValue" directly corresponds to the JSON values set in Cloud Script
//        Debug.Log((string)messageValue);
//    }

//    private static void OnErrorShared(PlayFabError error)
//    {
//        Debug.Log(error.GenerateErrorReport());
//    }

//    #endregion PlayerStats

//    public GameObject leaderboardPanel;
//    public GameObject listingPrefab;
//    public Transform listingContainer;

//    #region Leaderboard
//    public void GetLeaderboarder()
//    {
//        var requestLeaderboard = new GetLeaderboardRequest { StartPosition = 0, StatisticName = "PlayerHighScore", MaxResultsCount = 20 };
//        PlayFabClientAPI.GetLeaderboard(requestLeaderboard, OnGetLeadboard, OnErrorLeaderboard);
//    }

//    void OnGetLeadboard(GetLeaderboardResult result)
//    {
//        leaderboardPanel.SetActive(true);
//        Debug.Log(result.Leaderboard[0].StatValue);
//        foreach (PlayerLeaderboardEntry player in result.Leaderboard)
//        {
//            GameObject tempListing = Instantiate(listingPrefab, listingContainer);
//            LeaderboardListing LL = tempListing.GetComponent<LeaderboardListing>();
//            LL.playerNameText.text = player.DisplayName;
//            LL.playerScoreText.text = player.StatValue.ToString();
//            Debug.Log(player.DisplayName + ": " + player.StatValue);
//        }
//    }

//    public void CloseLeaderboardPanel()
//    {
//        leaderboardPanel.SetActive(false);
//        for (int i = listingContainer.childCount - 1; i >= 0; i--)
//        {
//            Destroy(listingContainer.GetChild(i).gameObject);
//        }
//    }

//    void OnErrorLeaderboard(PlayFabError error)
//    {
//        Debug.LogError(error.GenerateErrorReport());
//    }

//    #endregion Leaderboard
}