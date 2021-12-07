using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    public GameObject platformPrefab;
    public GameObject playerPlatformPrefab;
    public float waitTime = 0.04f;

    public Transform originPoint;
    public float dist = 2f;
    public int width = 10;
    public int height = 10;

    public Vector3 playerSpawnOffset = new Vector3(0f, 2f, 0f);
    public Vector3 offsetFromNormalPlatforms = new Vector3(0f, 3f, 0f);

    private List<GameObject> _generatedGamePlatforms;
    private Dictionary<int, GameObject> _generatedPlayerPlatforms;

    private void Awake()
    {
        _generatedGamePlatforms = new List<GameObject>();
        _generatedPlayerPlatforms = new Dictionary<int, GameObject>();
    }

    private Vector3 GetSpawnPosFromPlatform(GameObject platform)
    {
        return platform.transform.position + playerSpawnOffset;
    }

    private GameObject CreateNewPlayerPlatform()
    {
        int currentCount = _generatedPlayerPlatforms.Count;

        // we find where to place the next platform
        int currentWidth = currentCount;
        int currentHeight = 0;
        currentCount -= width;
        while (currentCount >= 0)
        {
            currentHeight++;
            currentCount -= width;
        }

        return Instantiate(playerPlatformPrefab, originPoint.position + offsetFromNormalPlatforms + (dist * (currentWidth % width) * Vector3.right) + (dist * currentHeight * Vector3.forward), Quaternion.identity, transform);
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

    public void OnGameStart(GenericEvent evt) // event
    {
        StartCoroutine(StartingGame(evt));
    }
    
    public void OnGameEnd(GenericEvent evt) // event
    {
        StartCoroutine(EndingGame(evt));
    }
    
    private IEnumerator StartingGame(GenericEvent evt)
    {
        // spawn game platforms
        for (int w = 0; w < width; w++)
        {
            for (int h = 0; h < height; h++)
            {
                _generatedGamePlatforms.Add(Instantiate(platformPrefab, originPoint.position + (dist * w * Vector3.right) + (dist * h * Vector3.forward), Quaternion.identity, transform));
                yield return new WaitForSeconds(waitTime);
            }
        }

        // destroy player platforms
        foreach (KeyValuePair<int, GameObject> entry in _generatedPlayerPlatforms.ToList())
        {
            Destroy(entry.Value);
        }
        _generatedPlayerPlatforms.Clear();

        evt.Answer();
    }

    private IEnumerator EndingGame(GenericEvent evt)
    {
        // destroy game platforms
        foreach (var platform in _generatedGamePlatforms.ToList())
        {
            Destroy(platform);
            yield return new WaitForSeconds(waitTime);
        }
        _generatedGamePlatforms.Clear();
        
        evt.Answer();
    }
}
