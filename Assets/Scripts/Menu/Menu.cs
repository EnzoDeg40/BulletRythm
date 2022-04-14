using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.IO;
using System.IO.Compression;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.Networking;

public class Menu : MonoBehaviour
{
    MenuBeatmapList menuBeatmapList;

    public static string mapSelected;

    public Text showinfo;

    bool downloadIsRunning;
    string beatmapFolder;
    string mapname;

    [Header("MapPanel")]
    public GameObject panelMapOption;
    public Text mapnamePanel;
    public Button playButton;
    public Button classmentButton;
    public Button deleteMapButton;
    public Button cancelButton;
    public Text currentPlayed;

    [Header("Audio")]
    public AudioSource playmusic;
    private AudioClip clipMusic;
    private string currentMusicPlayed;


    void Start()
    {
        menuBeatmapList = GetComponent<MenuBeatmapList>();
        beatmapFolder = Application.persistentDataPath + "/Beatmaps/";

        panelMapOption.SetActive(false);
    }

    public void Launch(string mapname)
    {
        Debug.Log("MUSIC SELECT IS " + mapname);
        mapSelected = mapname;

        MusicPlay(true, mapname);

        mapnamePanel.text = mapname;
        classmentButton.onClick.AddListener(() => Application.OpenURL("http://edstudio.fr/project/bulletrythm/beatmap.php?mapname=" + mapname));
        playButton.onClick.AddListener(() => StartMusic());
        deleteMapButton.onClick.AddListener(() => DeleteMap(mapname));
        cancelButton.onClick.AddListener(() => panelMapOption.SetActive(false));

        panelMapOption.SetActive(true);
    }


    public void StartMusic()
    {
        SceneManager.LoadScene("Play");
    }

    public void DeleteMap(string map)
    {
        MusicPlay(false, "");
        Directory.Delete(beatmapFolder + map, true);
        SceneManager.LoadScene("Menu");
    }


    public void MusicPlay(bool active, string beatmapname)
    {
        if(currentMusicPlayed != beatmapname) {
            StartCoroutine(LoadFile(beatmapFolder + beatmapname + "/audio.wav"));
            currentMusicPlayed = beatmapname;
            currentPlayed.text = beatmapname + " ♫";
        }
    }

    public void MusicPause() {
        if (playmusic.isPlaying) {
            playmusic.Pause();
        }
        else {
            playmusic.Play();
        }
    }

    IEnumerator LoadFile(string fullpath)
    {
        print("LOADING CLIP " + fullpath);

        if (!System.IO.File.Exists(fullpath))
        {
            print("DIDN'T EXIST: " + fullpath);
            yield break;
        }

        //clipMusic = null;
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip("file://" + fullpath, AudioType.WAV))
        {
            yield return www.SendWebRequest();
            if (www.error != null)
            {
                Debug.Log(www.error);
            }
            else
            {
                clipMusic = DownloadHandlerAudioClip.GetContent(www);
                playmusic.clip = clipMusic;
                //Debug.Log(temp);
                playmusic.Play();
            }
        }
    }

    #region Download
    public void Download(string map)
    {
        if (!downloadIsRunning)
        {
            mapname = map;
            //TryDownload();
            StartCoroutine(BetterTryDownload());
        }
    }

    private IEnumerator BetterTryDownload() {
        foreach (var item in menuBeatmapList.dir) {
            if (item == mapname) {
                yield return 0;
            }
        }

        Debug.Log("Download " + mapname);

        // Create directory beatmaps if not exist
        showinfo.text = "Create beatmap's directory";
        Directory.CreateDirectory(beatmapFolder);

        // Download beatmap
        showinfo.text = "Download " + mapname;

        downloadIsRunning = true;

        WebClient client = new WebClient();
        client.DownloadFileCompleted += new System.ComponentModel.AsyncCompletedEventHandler(DownloadComplet);
        client.DownloadFileAsync(
            // Url to download file
            new Uri(URL.url + "download.php?user=" + PlayerPrefs.GetString("username") + "&password=" + PlayerPrefs.GetString("password") + "&map=" + mapname),
            // Name of map download
            beatmapFolder + mapname + ".zip");

        //yield return new WaitForSeconds(1);
        //print("Coroutine ended: " + Time.time + " seconds");
    }

    void TryDownload()
    {
        foreach (var item in menuBeatmapList.dir)
        {
            if (item == mapname)
            {
                return;
            }
        }

        Debug.Log("Download " + mapname);

        // Create directory beatmaps if not exist
        showinfo.text = "Create beatmap's directory";
        Directory.CreateDirectory(beatmapFolder);

        // Download beatmap
        showinfo.text = "Download " + mapname;

        downloadIsRunning = true;

        WebClient client = new WebClient();
        client.DownloadFileCompleted += new System.ComponentModel.AsyncCompletedEventHandler(DownloadComplet);
        client.DownloadFileAsync(
            // Url to download file
            new Uri(URL.url + "download.php?user=" + PlayerPrefs.GetString("username") + "&password=" + PlayerPrefs.GetString("password") + "&map=" + mapname),
            // Name of map download
            beatmapFolder + mapname + ".zip");
    }

    void DownloadComplet(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
    {
        if (e.Error != null)
        {
            Debug.LogError("Error");
        }

        if (File.ReadAllText(beatmapFolder + mapname + ".zip") == "403")
        {
            Debug.LogError("IMPOSSIBLE TO DOWNLOAD MAP");
            File.Delete(beatmapFolder + mapname + ".zip");
            showinfo.text = "Impossible to download map";
            downloadIsRunning = false;
            return;
        }

        // Create directory of name of beatmap
        showinfo.text = "Extract map";
        Directory.CreateDirectory(beatmapFolder + mapname + "/");

        ZipFile.ExtractToDirectory(beatmapFolder + mapname + ".zip", beatmapFolder + mapname + "/");

        File.Delete(beatmapFolder + mapname + ".zip");

        downloadIsRunning = false;

        // Refresh list of local map
        menuBeatmapList.GetListMapsLocal();

        showinfo.text = "";
    }
    #endregion

}
