using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSystem : MonoBehaviour
{
    public GameUI gameUI;
    public GameRanking gameRanking;

    public int score;
    public int combo;
    public int maxcombo;
    public float pourcent;
    public float life = 100;
    public int bulletHit;
    public int bulletMiss;

    void Start()
    {
        // Set other script
        gameUI = this.GetComponent<GameUI>();
        gameRanking = this.GetComponent<GameRanking>();

        life = 100;
    }

    public void hit(bool result)
    {
        //Debug.Log(result);
        
        if (result)
        {
            combo++;
            if (life + 6 > 100) {
                life = 100;
            }
            else {
                life += 6;
            }
            bulletHit++;

            if(combo > maxcombo)
            {
                maxcombo = combo;
            }
        }
        else
        {
            combo = 0;
            if(life-10 < 0) {
                life = 0;
            }
            else
            {
                life -= 10;
            }
            bulletMiss++;
        }

        score += 50 * combo;

        if(bulletMiss == 0)
        {
            pourcent = 100;
        }
        else if(bulletHit == 0)
        {
            pourcent = 0;
        }
        else
        {
            pourcent = bulletHit * 100 / (bulletHit + bulletMiss);
        }

        gameUI.UpdateUI(score, combo, pourcent, life);
        gameRanking.updateRanking(score);
    }

    public void gameEnd()
    {
        MenuUploadScore.score = score;
        MenuUploadScore.combo = maxcombo;
        MenuUploadScore.hit = bulletHit;
        MenuUploadScore.miss = bulletMiss;
        MenuUploadScore.mapname = Menu.mapSelected;

        MenuUploadScore.GameEnd = true;
        SceneManager.LoadScene("Menu");
    }
}
