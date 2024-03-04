using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneManager : MonoBehaviour
{
    public GameObject aboutPage;
    public GameObject howToPlayPage;

    public PlayfabManager playfabManager;

    public GameObject rowPrefab;
    public Transform rowsParent;

    public StartSceneManager startManager;
    public GameObject startPanel;
    public GameObject loginPanel;
    public GameObject displayNamePanel;
    public GameObject leaderboardPanel;

    public TextMeshProUGUI message;
    public TMP_InputField emailInput;
    public TMP_InputField usernameInput;
    public TMP_InputField passwordInput;
    private void Start()
    {
        PlayfabManagerEdit();
    }
    public void OnStartButtonClicked()
    {
        SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
    }
    public void OnAboutButtonClicked()
    {
        aboutPage.SetActive(true);
    }
    public void OnAboutButtonCloseClicked()
    {
        aboutPage.SetActive(false);
    }
    public void OnHowToPlayButtonClicked()
    {
        howToPlayPage.SetActive(true);
    }
    public void OnHowToPlayButtonCloseClicked()
    {
        howToPlayPage.SetActive(false);
    }
    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }

    public void PlayfabManagerEdit()
    {
        playfabManager = FindObjectOfType<PlayfabManager>();
        playfabManager.rowPrefab = rowPrefab;
        playfabManager.rowsParent = rowsParent.transform;
        playfabManager.message = message;
        playfabManager.emailInput = emailInput;
        playfabManager.usernameInput = usernameInput;
        playfabManager.passwordInput = passwordInput;
        playfabManager.startManager = gameObject.GetComponent<StartSceneManager>();
        playfabManager.startPanel = gameObject;
        playfabManager.loginPanel = loginPanel;
        playfabManager.displayNamePanel = displayNamePanel;
        playfabManager.leaderboardPanel = leaderboardPanel;
      //  playfabManager.Login();        

}

    public void GetLeaderboardTime()
    {
        playfabManager.GetLeaderboardTime();
    }
    public void GetLeaderboardGold()
    {
        playfabManager.GetLeaderboardGold();
    }  
    public void GetLeaderboardKilledMonster()
    {
        playfabManager.GetLeaderboardKilled();
    } 
    public void CloseLeaderboardPanel()
    {
        playfabManager.CloseLeaderboardPanel();
    }
}
