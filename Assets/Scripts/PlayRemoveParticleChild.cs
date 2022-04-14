using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayRemoveParticleChild : MonoBehaviour
{
    public GameObject particle;

    void Start()
    {

    }

    void Spawn()
    {
        if (gameObject.transform.childCount > 1)
        {
            Destroy(GetComponent<Transform>().GetChild(0).gameObject);
        }
    }
}
