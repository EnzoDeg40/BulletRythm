using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameMusicPlayer : MonoBehaviour
{
    GameSystem gameSystem;

    public string MusicName;

    public AudioSource playmusic;
    public AudioClip clipMusic;

    string beatmapFolder;

    [SerializeField] bool musicStarted;

    void Start()
    {
        gameSystem = GetComponent<GameSystem>();

        beatmapFolder = Application.persistentDataPath + "/Beatmaps/";
        //Debug.Log(Application.persistentDataPath);
        StartCoroutine(LoadFile(beatmapFolder + Menu.mapSelected + "/audio.wav"));
    }

    void LateUpdate()
    {
        if (playmusic.isPlaying)
        {
            musicStarted = true;
        }

        if (!playmusic.isPlaying && musicStarted && Application.isFocused)
        {
            Debug.Log("Music is not playing");
            gameSystem.gameEnd();
        }
    }

    IEnumerator LoadFile(string fullpath)
    {
        Debug.Log("LOADING CLIP " + fullpath);

        if (!System.IO.File.Exists(fullpath))
        {
            Debug.LogError("DIDN'T EXIST: " + fullpath);
            yield break;
        }

        //clipMusic = null;
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip("file://" + fullpath, AudioType.WAV))
        {
            yield return www.SendWebRequest();
            if (www.isNetworkError)
            {
                Debug.LogError(www.error);
            }
            else
            {
                clipMusic = DownloadHandlerAudioClip.GetContent(www);
                playmusic.clip = clipMusic;
                //Debug.Log(temp);
            }
        }
    }

}
