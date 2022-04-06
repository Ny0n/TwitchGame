using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SpawnerWindow : EditorWindow
{
    string prefix = "Spawned_";
    int spawnCount = 0, spawnDistance = 0;
    GameObject toSpawn, parent;

    float minVal = 10;
    float minLimit = 0.1f;
    float maxVal = 10;
    float maxLimit = 500;

    // Add menu item named "My Window" to the Window menu
    [MenuItem("8PRO135/Object Spawner Window")]
    public static void ShowWindow()
    {
        //Show existing window instance. If one doesn't exist, make one.
        EditorWindow.GetWindow(typeof(SpawnerWindow));
    }

    void OnGUI()
    {
        GUILayout.Label("What to spawn", EditorStyles.boldLabel);

        toSpawn = (GameObject)EditorGUILayout.ObjectField("Prefab to spawn", toSpawn, typeof(GameObject));
        parent = (GameObject)EditorGUILayout.ObjectField("Parent", parent, typeof(GameObject));

        GUILayout.Space(10);

        GUILayout.Label("Spawn options", EditorStyles.boldLabel);
        prefix = EditorGUILayout.TextField("Spawned_", prefix);
        spawnCount = EditorGUILayout.IntField("Spawn count", spawnCount);
        spawnDistance = EditorGUILayout.IntField("Max Spawn Distance", spawnDistance);

        EditorGUILayout.LabelField("Min Scale Factor:", minVal.ToString());
        EditorGUILayout.LabelField("Max Scale Factor:", maxVal.ToString());
        EditorGUILayout.MinMaxSlider(ref minVal, ref maxVal, minLimit, maxLimit);

        if (GUILayout.Button("Spawn " + spawnCount.ToString() +" object(s)"))
        {
            spawn();
        }
    }

    private void spawn()
    {
        if (toSpawn && parent)
        {
            var parentPos = parent.transform.position;
            float x = parentPos.x;
            float y = parentPos.y;
            float z = parentPos.z;
            
            for (int i = 0; i < spawnCount; i++)
            {
                float xPos = Random.Range(-spawnDistance, spawnDistance) + x;
                float yPos = Random.Range(-spawnDistance, spawnDistance) + y;
                float zPos = Random.Range(-spawnDistance, spawnDistance) + z;
                Vector3 pos = new Vector3(xPos, yPos, zPos);
                
                GameObject newOne = Instantiate(toSpawn, pos, Quaternion.identity, parent.transform);
                newOne.name = prefix + i;
                float scale = Random.Range(minVal, maxVal);
                newOne.transform.localScale = new Vector3(scale, scale, scale);
            }
        }
        else
        {
            Debug.Log("The prefab or the parent is not set in the editor");
        }
    }

}
