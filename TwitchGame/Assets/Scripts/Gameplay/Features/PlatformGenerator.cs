using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    public ScriptableGameEvent mapLoadedEvent;

    public GameObject platformPrefab;
    public GameObject playerPlatformPrefab;
    public float waitTime = 0.04f;

    public Transform originPoint;
    public float dist = 2f;

    public int width = 10;
    public int height = 10;

    public Vector3 playerSpawnOffset = new Vector3(0f, 2f, 0f);

    private GameObject[,] _generatedPlatforms;

    private Dictionary<int, GameObject> _generatedPlayerPlatforms;

    private void Awake()
    {
        _generatedPlayerPlatforms = new Dictionary<int, GameObject>();
    }

    private Vector3 GetSpawnPosFromPlatform(GameObject platform)
    {
        platform.GetComponent<PlatformPlayerInfo>().HasPlayer = true;
        return platform.transform.position + playerSpawnOffset;
    }

    private GameObject CreateNewPlayerPlatform()
    {
        Vector3 offsetFromNormalPlatforms = new Vector3(0f, 3f, 0f);
        int currentCount = _generatedPlayerPlatforms.Count;
        return Instantiate(playerPlatformPrefab, originPoint.position + offsetFromNormalPlatforms + (dist * currentCount%width * Vector3.forward) + (dist * currentCount%height * Vector3.right), Quaternion.identity, transform);
    }

    private bool PlatformHasPlayer(GameObject platform)
    {
        return platform.GetComponent<PlatformPlayerInfo>().HasPlayer;
    }

    public Vector3 GetPlayerSpawn()
    {
        foreach (KeyValuePair<int, GameObject> entry in _generatedPlayerPlatforms)
        {
            if (!PlatformHasPlayer(entry.Value))
            {
                return GetSpawnPosFromPlatform(entry.Value);
            }
        }

        int currentIndex = _generatedPlayerPlatforms.Count;
        _generatedPlayerPlatforms[currentIndex] = CreateNewPlayerPlatform();
        return GetSpawnPosFromPlatform(_generatedPlayerPlatforms[currentIndex]);
    }

    public void OnGameStart() // event
    {
        StartCoroutine(StartingGame());
    }

    private IEnumerator StartingGame()
    {
        for (int w = 0; w < width; w++)
        {
            for (int h = 0; h < height; h++)
            {
                Instantiate(platformPrefab, originPoint.position + (dist * w * Vector3.forward) + (dist * h * Vector3.right), Quaternion.identity, transform);
                yield return new WaitForSeconds(waitTime);
            }
        }

        foreach (KeyValuePair<int, GameObject> entry in _generatedPlayerPlatforms)
        {
            Destroy(entry.Value);
        }

        mapLoadedEvent.Raise();
    }
}
