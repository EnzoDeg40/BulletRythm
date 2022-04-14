using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    void Start()
    {
        Transform start = transform;

        Vector3 moveTo = new Vector3(transform.position.x, -5.75f, 0);

        StartCoroutine(MoveToPosition(start, moveTo, 1.25f));
    }

    public IEnumerator MoveToPosition(Transform transform, Vector3 position, float timeToMove)
    {
        var currentPos = transform.position;
        var t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime / timeToMove;
            transform.position = Vector3.Lerp(currentPos, position, t);
            yield return null;
        }
    }
}
