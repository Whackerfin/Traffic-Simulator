using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }
    public GameObject LS; // LeftSpawner
    public GameObject RS;
    public GameObject FS;
    //Singleton
    public static ObjectPooler Instance;
    PathManager pm;
    GameObject g;
    public int maxWaitingCarsPerLane;
    private void Awake()
    {
        g = GameObject.FindWithTag("Paths");
        pm = g.GetComponent<PathManager>();
        Instance = this;
        maxWaitingCarsPerLane = 3;
    }
    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;
    // Start is called before the first frame update
    void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectpool = new Queue<GameObject>();
            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectpool.Enqueue(obj);
            }
            poolDictionary.Add(pool.tag, objectpool);
        }

    }
    //could try if path ends up not working once all 12 cars move is to try taking paths from update rather than in the spawnfrompool function
    public GameObject SpawnFromPool(string tag)
    {
        GameObject objectToSpawn = poolDictionary[tag].Dequeue();
        Paths paths = objectToSpawn.GetComponent<Paths>();
        int pathno = paths.pathno;
        Paths.LaneType laneType = DetermineLaneType(pathno);



        //Debug.Log("This Works");
      
        if (pm.waitingCarsCount[laneType] < maxWaitingCarsPerLane)
        {
          
            // ... (rest of your spawning logic)

            if (pathno == 0 || pathno == 1)
            {
               
                objectToSpawn.transform.position = LS.transform.position;
                objectToSpawn.transform.rotation = LS.transform.rotation;
                // Debug.Log("pathno is "+pathno);
            }
            else if (pathno == 2 || pathno == 3)
            {
              
                objectToSpawn.transform.position = RS.transform.position;
                objectToSpawn.transform.rotation = RS.transform.rotation;
                //Debug.Log("pathno is " + pathno);
            }
            else if (pathno == 4 || pathno == 5)
            {
                
                objectToSpawn.transform.position = FS.transform.position;
                objectToSpawn.transform.rotation = FS.transform.rotation;
                // Debug.Log("pathno is " + pathno);
            }
            else
            {
                Debug.Log("Error Wrong Path no Script ObjectPooler");
                return null;
            }
            objectToSpawn.SetActive(true);
            //this might be heavy on computer
            IPooledObject Pooledobject = objectToSpawn.GetComponent<IPooledObject>();
            if (Pooledobject != null)
            {
                Pooledobject.OnObjectSpawn();
                // Debug.Log("if null statement works");
            }
            poolDictionary[tag].Enqueue(objectToSpawn);
            //  Debug.Log("enque works");
            return objectToSpawn;
           
        }
        else
        {
            // If the lane is full, enqueue the object back to the pool
            poolDictionary[tag].Enqueue(objectToSpawn);
            return null;
        }



            // ... (rest of the spawning logic)

           
        



        /* L1 0,1 LS
         * R1 2,3 RS
         * F1 4,5 FS
         */

        // Debug.Log("Set Active works");
        
    }
 

    Paths.LaneType DetermineLaneType(int pathno)
    {
        if (pathno == 0 || pathno == 1)
        {
            return Paths.LaneType.LeftLane;
        }
        else if(pathno == 2 || pathno == 3)
        {
            return Paths.LaneType.RightLane;
        }
        else if (pathno == 4 || pathno == 5)
        {
            return Paths.LaneType.FrontLane;
        }
        else
        {
            return Paths.LaneType.Unknown;
        }
    }

}