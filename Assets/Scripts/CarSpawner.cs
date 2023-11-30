using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    ObjectPooler objectpooler;

    public float spawnDelay; // Adjust this value to control the spawn rate
    public float timer = 0;
   
    void Start()
    {
        objectpooler = ObjectPooler.Instance;
        spawnDelay = 2f;
      
    }




    private void Update()
    {
        timer = timer + Time.deltaTime;

        if (timer > spawnDelay)
        {
            
            GameObject spawnedCar = objectpooler.SpawnFromPool("Car");
            if (spawnedCar != null)
            {
                // ... (adjust car position and rotation)

                timer = 0;
            }
            //spawnDelay = Random.Range(1f, 1.5f);
            //  Debug.Log("Spawned: ");
        }
    }


}
