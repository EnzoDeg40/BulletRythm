using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSpawnBullet : MonoBehaviour
{
    public AudioSource playmusic;
    public GameObject Bullet;
    public Transform SpawnBullet;
    public float timeDropObject = 7f;

    public float time;

    public string[] beatmap;
    public int cursor;

    public float forceDecalage;

    public Text hidetime;

    bool newcombo;

    // Start is called before the first frame update
    void Start()
    {
        // To test the game without going through the menu
        if(Menu.mapSelected == null){
            Menu.mapSelected = "Thaehan - Bwa ! (Deif)";
        }

        // Load the beatmap
        beatmap = System.IO.File.ReadAllText(Application.persistentDataPath + "/Beatmaps/" + Menu.mapSelected + "/hard.brm").Split(';');
        
        // Set the time
        time = -3000;
        StartCoroutine(StartMusic());
    }

    IEnumerator StartMusic()
    {
        while (true)
        {
            if (time >= 0)
            {
                Debug.Log("Start music");
                playmusic.Play();
                break;
            }

            yield return new WaitForSeconds(0.05f);
        }
    }

    private void Update()
    {
        time += Time.deltaTime*1000;

        hidetime.text = (time/1000).ToString("F1");

        if(cursor > beatmap.Length) {
            return;
        }


        if (time >= int.Parse(beatmap[cursor].Split(',')[0]) - (timeDropObject * forceDecalage * 1000)) {
            if(beatmap[cursor].Split(',')[1] == "1")
            {
                newcombo = !newcombo;
            }

            if (newcombo)
            {
                Bullet.name = "1";
            }
            else
            {
                Bullet.name = "0";
            }

            Instantiate(Bullet, SpawnBullet.position + new Vector3((float.Parse(beatmap[cursor].Split(',')[2])-5)/100, 5.5f, 0), new Quaternion(), SpawnBullet);
            cursor++;
        }
    }

    /*
     [map info]
     difficulty:    second (0.25 to 5)


     [beatmap]
     bullet:        ms,0,x
     newcombo:      ms,1,x
     spike:         ms,2,x
     text:          ms,3,duration,string

     
     
     */

}
