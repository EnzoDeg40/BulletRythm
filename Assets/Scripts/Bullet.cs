using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Sprite redBullet;
    public Sprite blueBullet;
    public Sprite spikeBullet;

    SpriteRenderer spriteBullet;

    void Start()
    {
        spriteBullet = GetComponent<SpriteRenderer>();

        int name = int.Parse(gameObject.name.Split('(')[0]);

        if(name == 1)
        {
            spriteBullet.sprite = blueBullet;
        }
        else if(name == 2)
        {
            spriteBullet.sprite = spikeBullet;
        }

    }

}
