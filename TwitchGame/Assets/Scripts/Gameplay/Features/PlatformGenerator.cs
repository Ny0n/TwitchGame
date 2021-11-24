using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    public ScriptableGameEvent mapLoadedEvent;

    public GameObject platformPrefab;
    public Transform startPoint;
    public float dist = 2f;
    public float waitTime = 0.04f;

    public int width = 10;
    public int height = 10;

    // Start is called before the first frame update
    private IEnumerator Start()
    {
        for (int w = 0; w < width; w++)
        {
            for (int h = 0; h < height; h++)
            {
                Instantiate(platformPrefab, startPoint.position + (dist * w * Vector3.forward) + (dist * h * Vector3.right), Quaternion.identity, transform);
                yield return new WaitForSeconds(waitTime);
            }
        }
        mapLoadedEvent.Raise();
    }
}
