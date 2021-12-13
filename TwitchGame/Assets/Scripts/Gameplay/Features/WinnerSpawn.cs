using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinnerSpawn : MonoBehaviour
{
    public ScriptablePlayersList playersList;
    public ScriptableGameEvent nextLevelEvent;
    
    public GameObject playerPrefab;

    private IEnumerator Start()
    {
        Player winner = (from player in playersList.Players where player.Value.IsAlive select player.Value).FirstOrDefault();

        if (winner is Player)
        {
            print("winner " + winner.Name);
            winner.Instantiate(playerPrefab, transform.position, transform.rotation);
            yield return null;
            winner.Win();
        }

        StartCoroutine(ReturnCoroutine());
    }

    private IEnumerator ReturnCoroutine()
    {
        yield return new WaitForSeconds(5);
        nextLevelEvent.Raise();
    }
}
