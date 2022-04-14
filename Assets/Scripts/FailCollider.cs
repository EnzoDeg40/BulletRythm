using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FailCollider : MonoBehaviour
{
    GameSystem gameSystem;

    private void Start()
    {
        gameSystem = GameObject.Find("GameSystem").GetComponent<GameSystem>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Bullet")
        {
            gameSystem.hit(false);
            Destroy(collision.gameObject);
        } 
    }
}
