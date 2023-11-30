using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoBehaviour
{
    
    public GameObject L1;
    public GameObject L2;
    public GameObject L3;

    public GameObject R1;
    public GameObject R2;
    public GameObject R3;

    public GameObject F1;
    public GameObject F2;
    public GameObject F3;

    public GameObject L1_F2;
    public GameObject R1_F2;
    public GameObject F1_L2;
    public GameObject F1_R2;

    // Start is called before the first frame update
    public Transform[,] path;

    public Dictionary<Paths.LaneType, int> waitingCarsCount;

    void Awake()
    {
        waitingCarsCount = new Dictionary<Paths.LaneType, int>();
        waitingCarsCount[Paths.LaneType.LeftLane] = 0;
        waitingCarsCount[Paths.LaneType.RightLane] = 0;
        waitingCarsCount[Paths.LaneType.FrontLane] = 0;
        int rows = 6; 
        int columns = 4; 

        path = new Transform[rows, columns];

        SetWayPoints();
        // Populate the path array with your Transform objects or set them through the Unity Editor
        // ...
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    void SetWayPoints()
    {
        /*
          L1 TO F2 is 0
          L1 to R2 is 1
          R1 TO F2 IS 2
         R1 TO L2 IS 3
        F1 TO L2 IS 4
        F1 TO R2 IS 5
         */
        path[0, 0] = L1.transform;
        path[0, 1] = L1_F2.transform;
        path[0, 2] = F2.transform;
        path[0, 3] = F3.transform;

        path[1, 0] = L1.transform;
        path[1, 1] = R2.transform;
        path[1, 2] = R3.transform;
        path[1, 3] = R3.transform;

        path[2, 0] = R1.transform;
        path[2, 1] = R1_F2.transform;
        path[2, 2] = F2.transform;
        path[2, 3] = F3.transform;

        path[3, 0] = R1.transform;
        path[3, 1] = L2.transform;
        path[3, 2] = L3.transform;
        path[3, 3] = L3.transform;

        path[4, 0] = F1.transform;
        path[4, 1] = F1_L2.transform;
        path[4, 2] = L2.transform;
        path[4, 3] = L3.transform;

        path[5, 0] = F1.transform;
        path[5, 1] = F1_R2.transform;
        path[5, 2] = R2.transform;
        path[5, 3] = R3.transform;



    }
    public void IncrementWaitingCarsCount(Paths.LaneType laneType)
    {
        if (waitingCarsCount.ContainsKey(laneType))
        {
            waitingCarsCount[laneType]++;
        }
        else
        {
            Debug.LogError("Invalid lane type.");
        }
    }
    public void DecrementWaitingCarsCount(Paths.LaneType laneType)
    {
        if (waitingCarsCount.ContainsKey(laneType) && waitingCarsCount[laneType] > 0)
        {
            waitingCarsCount[laneType]--;
        }
        else
        {
            Debug.LogError("Invalid lane type or waiting cars count is already zero.");
        }
    }
}
