using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BetterMenuAudio : MonoBehaviour
{
    public AudioSource MusicTitle;
    public AudioSource MusicBackground;

    public Text Title;
    public Text Loading;

    void Start()
    {
        StartCoroutine(startGame());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator startGame() {
        Loading.text = "";
        MusicBackground.Play();
        yield return new WaitForSeconds(2.75f);
        MusicTitle.Play();
        yield return new WaitForSeconds(0.25f);

        Title.text = "Bienvenue";
        yield return new WaitForSeconds(0.25f);
        Title.text = "Bienvenue dans";
        yield return new WaitForSeconds(0.25f);
        Title.text = "Bienvenue dans BulletRythm";

    }
}
