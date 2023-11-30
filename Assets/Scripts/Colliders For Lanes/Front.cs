using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Front : MonoBehaviour
{
    // Start is called before the first frame update
    PathManager pm;

    // Start is called before the first frame update
    private void Awake()
    {
        pm = GameObject.FindWithTag("Paths").GetComponent<PathManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Car"))
        {
            pm.IncrementWaitingCarsCount(Paths.LaneType.FrontLane);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Car"))
        {
            pm.DecrementWaitingCarsCount(Paths.LaneType.FrontLane);
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
