using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StuffSpawner : MonoBehaviour {

	public Transform[] stuffSpawnPoints;
	public GameObject coin;
	public GameObject obstacle;
    
	void Start () 
	{
       
	}

    void Update () 
	{
        
	}

    GameObject SpawnObject()
    {
        int obstacleOrCoinChance = Random.Range(0, 2);
        if (obstacleOrCoinChance == 0)
        {
            return coin;
        }
        else
        {
            return obstacle;
        }
    }

    Vector3 RandomSpawnLaneValue()
    {
        int temp = Random.Range(0, 3);
        if (temp == 0)
        {
            return new Vector3(-1,0,154); //leftlane
        }
        else if(temp == 1)
        {
            return new Vector3(1,0,154); //rightlane
        }
        else
        {
            return new Vector3(0, 0, 154); //midlane
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        

        for (int i = 0; i < stuffSpawnPoints.Length; i++)
        {
            GameObject tempSpawnObject = SpawnObject();
            if (tempSpawnObject == coin)
            {
                Instantiate(tempSpawnObject, stuffSpawnPoints[i].position + RandomSpawnLaneValue(), Quaternion.identity);
            }

            if (tempSpawnObject == obstacle)
            {
                RaycastHit hitDown = new RaycastHit();
                Ray downRay = new Ray(obstacle.transform.position, Vector3.down);
                Vector3 targetPos = new Vector3();
                targetPos.y = hitDown.point.y + 0.1f;
                targetPos.x = stuffSpawnPoints[i].position.x;
                targetPos.z = stuffSpawnPoints[i].position.z;
                Instantiate(tempSpawnObject, targetPos + RandomSpawnLaneValue(), Quaternion.Euler(90,0,0));
            }
            
        }
    }
}
