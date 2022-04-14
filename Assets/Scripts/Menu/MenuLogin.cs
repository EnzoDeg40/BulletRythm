using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class MenuLogin : MonoBehaviour
{
    [Header("Panels")]
    public GameObject loginPanel;
    public GameObject registerPanel;
    public GameObject loadingPanel;

    [Header("LoginButtons")]
    public Button newPlayer;
    public Button login;

    [Header("LoginInput")]
    public InputField usernameLogin;
    public InputField passwordLogin;
    
    [Header("RegisterButtons")]
    public Button oldPlayer;
    public Button register;

    [Header("RegisterInput")]
    public InputField usernameRegister;
    public InputField emailRegister;
    public InputField passwordRegister;

    [Header("SkipLoginButtons")]
    public Button skipLogin;

    [Header("CGU")]
    public Button CGU;

    // Start is called before the first frame update
    void Start()
    {
        usernameLogin.text = PlayerPrefs.GetString("username", "");
        passwordLogin.text = PlayerPrefs.GetString("password", "");

        /*
        if(usernameLogin.text != "" && passwordLogin.text != null)
        {
            GoLogin();
        }
        */

        newPlayer.onClick.AddListener(() => changePanel(true));
        oldPlayer.onClick.AddListener(() => changePanel(false));

        login.onClick.AddListener(() => GoLogin());
        register.onClick.AddListener(() => GoRegister());

        skipLogin.onClick.AddListener(() => GoSkipLogin());

        CGU.onClick.AddListener(() => Application.OpenURL("http://unity3d.com/"));
    }

    void changePanel(bool active)
    {
        loginPanel.SetActive(!active);
        registerPanel.SetActive(active);
    }

    void GoLogin()
    {
        if (usernameLogin.text == "" || passwordLogin.text == "")
        {
            Debug.LogError("No input enter");
            return;
        }

        loadingPanel.SetActive(true);

        StartCoroutine(GetRequestLogin(URL.url + "login.php?user=" + usernameLogin.text + "&password=" + passwordLogin.text));
   
    }

    void GoRegister()
    {
        if(usernameRegister.text == "" || passwordRegister.text == "" || emailRegister.text == "")
        {
            Debug.LogError("No input enter");
            return;
        }

        loadingPanel.SetActive(true);

        StartCoroutine(GetRequestLogin(URL.url + "register.php?user=" + usernameRegister.text + "&password=" + passwordRegister.text + "&email=" + emailRegister.text));
    }

    void GoSkipLogin()
    {
        PlayerPrefs.SetString("username", "Guest");
        PlayerPrefs.SetString("password", "Offline");

        SceneManager.LoadScene("Menu");
    }

    IEnumerator GetRequestLogin(string url)
    {
        //yield return new WaitForSeconds(2);

        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = url.Split('/');
            int page = pages.Length - 1;

            if (webRequest.isNetworkError)
            {
                Debug.Log(pages[page] + ": Error: " + webRequest.error);
            }
            else
            {
                Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);

                if(webRequest.downloadHandler.text == "true")
                {
                    PlayerPrefs.SetString("username", usernameLogin.text);
                    PlayerPrefs.SetString("password", passwordLogin.text);

                    SceneManager.LoadScene("Menu");
                } 
            }

            loadingPanel.SetActive(false);

            
        }
    }

    

}
