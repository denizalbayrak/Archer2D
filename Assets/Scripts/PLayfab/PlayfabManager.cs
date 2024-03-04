using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
public class PlayfabManager : MonoBehaviour
{
    public static PlayfabManager PFM;

    public StartSceneManager startManager;
    public GameObject startPanel;
    public GameObject leaderboardPanel;
    public GameObject loginPanel;
    public GameObject displayNamePanel;
    public GameObject rowPrefab;
    public Transform rowsParent;

    public TextMeshProUGUI message;
    public TMP_InputField emailInput;
    public TMP_InputField usernameInput;
    public TMP_InputField passwordInput;

    int maxLeaderboardCount = 0;
    private void OnEnable()
    {
        if (PlayfabManager.PFM == null)
        {
            PlayfabManager.PFM = this;
        }
        else
        {
            if (PlayfabManager.PFM != this)
            {
                Destroy(this.gameObject);
            }
        }
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {        
        Login();
        if (string.IsNullOrEmpty(PlayFabSettings.TitleId))
        {
            PlayFabSettings.TitleId = "CAADF";
        }
    }

    public void Login()
    {
        
        //var request = new LoginWithCustomIDRequest
        //{
        //    CustomId = SystemInfo.deviceUniqueIdentifier,
        //    CreateAccount = true,
        //    InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
        //    {
        //        GetPlayerProfile = true
        //    }
        //};

        //PlayFabClientAPI.LoginWithCustomID(request, OnSuccess, OnError);

        if (PlayerPrefs.HasKey("EMAIL"))
        {
            emailInput.text = PlayerPrefs.GetString("EMAIL");
            passwordInput.text = PlayerPrefs.GetString("PASSWORD");

            var request = new LoginWithEmailAddressRequest
            {
                Email = emailInput.text,
                Password = passwordInput.text,
                InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
                {
                    GetPlayerProfile = true
                }
            };
            PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnLoginFailure);

        }
        else
        {
            loginPanel.SetActive(true);
        }
        //        else
        //        {
        //#if UNITY_ANDROID
        //            var requestAndroid = new LoginWithAndroidDeviceIDRequest { AndroidDeviceId = ReturnMobileID(), CreateAccount = true };
        //            PlayFabClientAPI.LoginWithAndroidDeviceID(requestAndroid, OnLoginSuccess, OnLoginFailure);
        //#endif
        //#if UNITY_IOS
        //            var requestIOS = new LoginWithIOSDeviceIDRequest { DeviceId = ReturnMobileID(), CreateAccount = true };
        //            PlayFabClientAPI.LoginWithIOSDeviceID(requestIOS, OnLoginSuccess, OnLoginFailure);
        //#endif
        //        }
    }
    void OnSuccess(LoginResult result)
    {
        Debug.Log("Login successfully!");
        string name = null;
        if (result.InfoResultPayload.PlayerProfile != null)
        {
            name = result.InfoResultPayload.PlayerProfile.DisplayName;
        }
        Debug.Log("name " + name);
        if (name == null)
        {
            loginPanel.SetActive(false);
            displayNamePanel.SetActive(true);
        }
        else
        {
            loginPanel.SetActive(false);
        }
    }
    public static string ReturnMobileID()
    {
        string deviceID = SystemInfo.deviceUniqueIdentifier;
        return deviceID;
    }
    void OnError(PlayFabError error)
    {
        Debug.Log("Error");
        Debug.LogError(error.GenerateErrorReport());
    }

    public void SendLeaderBoard(int playerCollectedCoin, int playerHighScore, int playerKilledMonster)
    {
        PlayFabClientAPI.UpdatePlayerStatistics(new UpdatePlayerStatisticsRequest
        {
            // request.Statistics is a list, so multiple StatisticUpdate objects can be defined if required.
            Statistics = new List<StatisticUpdate> {
                new StatisticUpdate { StatisticName = "PlayerKilledMonster", Value = playerKilledMonster },
                new StatisticUpdate { StatisticName = "HighScoreTime", Value = playerHighScore },
                new StatisticUpdate { StatisticName = "PlayerCollectedCoin", Value = playerCollectedCoin },

            }
        },
        result => { Debug.Log("User statistics updated"); },
        error => { Debug.LogError(error.GenerateErrorReport()); });
    }
    public void GetLeaderboardTime()
    {
        var request = new GetLeaderboardRequest
        {
            StatisticName = "HighScoreTime",
            StartPosition = 0,
            MaxResultsCount = 10
        };
        PlayFabClientAPI.GetLeaderboard(request, OnGetLeadboardTime, OnError);
    }
    public void GetLeaderboardGold()
    {
        var request = new GetLeaderboardRequest
        {
            StatisticName = "PlayerCollectedCoin",
            StartPosition = 0,
            MaxResultsCount = 10
        };
        PlayFabClientAPI.GetLeaderboard(request, OnGetLeadboard, OnError);
    }
    public void GetLeaderboardKilled()
    {
        var request = new GetLeaderboardRequest
        {
            StatisticName = "PlayerKilledMonster",
            StartPosition = 0,
            MaxResultsCount = 10
        };
        PlayFabClientAPI.GetLeaderboard(request, OnGetLeadboard, OnError);
    }
    void OnGetLeadboardTime(GetLeaderboardResult result)
    {
        
        leaderboardPanel.SetActive(true); 
        maxLeaderboardCount=0;
        foreach (Transform item in rowsParent)
        {
            Destroy(item.gameObject);
        }
        
        foreach (var item in result.Leaderboard) 
        {
            maxLeaderboardCount++;
            if (maxLeaderboardCount==9)
            {
                return;
            }
            GameObject newGo = Instantiate(rowPrefab, rowsParent);       
            TextMeshProUGUI[] texts = newGo.GetComponentsInChildren<TextMeshProUGUI>();
        
            int minutes = Mathf.FloorToInt(item.StatValue / 60f);
            int seconds = Mathf.FloorToInt(item.StatValue % 60f);
            texts[0].text = (item.Position + 1).ToString();
            texts[1].text = item.DisplayName;
            texts[2].text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }


    }
    void OnGetLeadboard(GetLeaderboardResult result)
    {
        leaderboardPanel.SetActive(true);
        maxLeaderboardCount = 0;
        foreach (Transform item in rowsParent)
        {
            Destroy(item.gameObject);
        }

        foreach (var item in result.Leaderboard)
        {
            maxLeaderboardCount++;
            if (maxLeaderboardCount == 9)
            {
                return;
            }
            GameObject newGo = Instantiate(rowPrefab, rowsParent);
            TextMeshProUGUI[] texts = newGo.GetComponentsInChildren<TextMeshProUGUI>();
            texts[0].text = (item.Position + 1).ToString();
            texts[1].text = item.DisplayName;
            texts[2].text = item.StatValue.ToString();
        }
    }
    public void CloseLeaderboardPanel()
    {
        leaderboardPanel.SetActive(false);
        foreach (Transform item in rowsParent)
        {
            Destroy(item.gameObject);
        }
    }

    public void RegisterButton()
    {
        if (passwordInput.text.Length < 6)
        {
            message.text = "Password too short!";
            return;
        }
        var request = new RegisterPlayFabUserRequest
        {
            Email = emailInput.text,
            Password = passwordInput.text,
            RequireBothUsernameAndEmail = false
        };
        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnRegisterError);
    }
    void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        message.text = "Register Success";
        Debug.Log("Register Success");
        PlayerPrefs.SetString("EMAIL", emailInput.text);
        PlayerPrefs.SetString("PASSWORD", passwordInput.text);
        PlayerPrefs.Save();
    }
    void OnRegisterError(PlayFabError error)
    {
        message.text = error.ErrorMessage;
    }
    public void LoginButton()
    {
        var request = new LoginWithEmailAddressRequest
        {
            Email = emailInput.text,
            Password = passwordInput.text,
            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
            {
                GetPlayerProfile=true
            }
        };
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnLoginFailure);
    }
    void OnLoginSuccess(LoginResult result)
    {
        message.text = "Login Success";
        Debug.Log("Login Success");
        PlayerPrefs.SetString("EMAIL", emailInput.text);
        PlayerPrefs.SetString("PASSWORD", passwordInput.text);
        PlayerPrefs.Save();
        string name = null;
        if (result.InfoResultPayload.PlayerProfile != null)
        {
            name = result.InfoResultPayload.PlayerProfile.DisplayName;
        }

        if (name == null)
        {
            loginPanel.SetActive(false);
            displayNamePanel.SetActive(true);
        }
        else
        {
            loginPanel.SetActive(false);
        }
    }
    void OnLoginFailure(PlayFabError error)
    {
        message.text = error.ErrorMessage;
    }
    public void ResetPasswordButton()
    {
        var request = new SendAccountRecoveryEmailRequest
        {
            Email = emailInput.text,
            TitleId = "CAADF"
        };
        PlayFabClientAPI.SendAccountRecoveryEmail(request, OnPasswordReset, OnError);
    }
    void OnPasswordReset(SendAccountRecoveryEmailResult result)
    {
        message.text = "Password reset mail sent!";
    }

    public void SubmitNameButton()
    {
        var request = new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = usernameInput.text

        };
        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnDisplayNameUpdate, OnError);
    }
    void OnDisplayNameUpdate(UpdateUserTitleDisplayNameResult result)
    {
        Debug.Log("Name Change Success");
        displayNamePanel.SetActive(false);
    }
}
