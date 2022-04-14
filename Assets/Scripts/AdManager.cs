using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // ONLY FOR ANDROID !!
        Advertisement.Initialize("3762865", false);
    }

    public void CreateAd()
    {
        if (Advertisement.IsReady("video"))
        {
            Advertisement.Show("video");
        }
        else
        {
            Debug.LogWarning("Ad is not ready");
        }
    }
}
