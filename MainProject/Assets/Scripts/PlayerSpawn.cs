using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    public Transform[] spawnPoints = new Transform[6];
    public GameObject player;

    

    
    void Start()
    {
        SpawnPlayer();        
    }

    void SpawnPlayer()
    {
            int spawn = Random.Range(0, spawnPoints.Length);
            
            GameObject.Instantiate(player, spawnPoints[spawn].position, spawnPoints[spawn].rotation);    
    }     
    
}
