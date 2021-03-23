using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour {
    public GameObject[] tilePrefabs;
    private Transform playerTransform;
    private float spawnZ = -16.0f;
    private float tileLength = 28;
    private int amnTilesOnScreen = 5;
    private float safeZone = 28.0f; //before delete
    private List<GameObject> activeTiles;
    private int lastPrefabIndex = 0;
    // Use this for initialization
	private void Start () {
        activeTiles = new List<GameObject>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        for(int i =0; i< amnTilesOnScreen; i++)   //infinite
        {
            if (i < 2)              ///3 First Floor
                SpawnTile(0);
            else
            SpawnTile();
          
        }
    }
	
	// Update is called once per frame
	private void Update () {
	if(playerTransform.position.z -safeZone> (spawnZ -amnTilesOnScreen * tileLength))  //spawnafterstepinnewtile
        {
            SpawnTile();
            DeleteTile();
        }	
	}
    private void SpawnTile(int prefabIndex =-1)
    {
        GameObject go;
        if (prefabIndex == -1)
            go = Instantiate(tilePrefabs[RandomPrefabIndex()]) as GameObject;            //0
        else
            go = Instantiate(tilePrefabs[prefabIndex]) as GameObject;
        go.transform.SetParent(transform);
        go.transform.position = Vector3.forward * spawnZ;
        spawnZ += tileLength;
        activeTiles.Add(go);
    }
    private void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }
    private int RandomPrefabIndex()  //RANDOM ROADS
    {
        if (tilePrefabs.Length <= 1)
            return 0;
        int RandomIndex = lastPrefabIndex;
        while(RandomIndex==lastPrefabIndex)
        {
            RandomIndex = Random.Range(0, tilePrefabs.Length);
        }
        lastPrefabIndex = RandomIndex;
        return RandomIndex;
    }
}
