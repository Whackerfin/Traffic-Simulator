using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Paths : MonoBehaviour, IPooledObject
{
    GameObject p;
    PathManager pm;

    TrafficLightManager traffic;
    public bool testmoving = true;
    public bool isinjunc = false;
    public float speed;
    public float sp;
  public bool itsred = false;

    public float rot;
    float rotsp;

    [SerializeField]  public GameObject left;
   [SerializeField] public GameObject right;
    [SerializeField] public GameObject front;

    int c = 0;
    public int pathno;
   
   
    private enum CarState
    {
        Moving,
        Stopped
    }
    private CarState currentState = CarState.Moving;
    public enum LaneType
    {
        LeftLane,
        RightLane,
        FrontLane,
        Unknown
    }
    public LaneType lanetype;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Car"))
        {

            AdjustSpeed(other.gameObject, true);
            // Debug.Log("Hit a car");

        }
        //just to check
        if (other.CompareTag("test"))
        {


            speed = 0;
            Debug.Log("Hit a block");

        }

    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Car"))
        {

            AdjustSpeed(other.gameObject, false);
            itsred = false;
            currentState = CarState.Moving;
            // Debug.Log("No longer hitting");
        }

    }

    public void OnObjectSpawn()
    {

        p = GameObject.FindWithTag("Paths");
        pm = p.GetComponent<PathManager>();
        if (pm == null)
        {
            Debug.LogError("PathManager not found on the specified GameObject.");
        }

    }
    void Awake()
    {
      
        traffic = GameObject.FindWithTag("TrafficLight").GetComponent<TrafficLightManager>();
        sp = speed;
        rotsp = rot;
        pathno = getRandomPath();//dont touch this it works only in awake
        ChangeTag();

        p = GameObject.FindWithTag("Paths");
        pm = p.GetComponent<PathManager>();
        if (pm == null)
        {
            Debug.LogError("PathManager not found on the specified GameObject.");
        }

    }
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
       
        // Debug.Log(speed);
    }
    void FixedUpdate()
    {
        if(isinjunc)
          {
             currentState = CarState.Moving;
            itsred = false;
        }

      
        CheckForRed();
        if (currentState != CarState.Stopped)
        {
            
            MoveToWayPoint();
        }
        if(pathno == 1)
        {
            CheckForRed();
        }
        /*
       if(currentState == CarState.Stopped)
        {
            speed = 0;
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }
        else if(currentState == CarState.Moving)
        {
            speed = sp;
        }
       
            */

    }
    int getRandomPath()
    {
        int column = Random.Range(0, 6);
        return column;
    }
    void SetcurrentPath()
    {

        c = (c + 1);



    }
    void MoveToWayPoint()
    {

        if (c < 4)
        {
            Vector3 target = pm.path[pathno, c].position;
            Quaternion targetR = pm.path[pathno, c].rotation;

            Vector3 a = transform.position;






            transform.position = Vector3.MoveTowards(a, target, speed);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetR, rot);


            if (Vector3.Distance(target, a) < 0.2f)
            {
                SetcurrentPath();
            }
        }

        else
        {
            // Debug.Log("Reached end of path");                                                              end of path

            this.gameObject.SetActive(false);
            // Debug.Log("Deactivated");

            pathno = getRandomPath();
            c = 0;

            ChangeTag();

        }



        
    }
     
        void AdjustSpeed(GameObject other, bool isColliding)
        {
            Paths otherPaths = other.GetComponent<Paths>();
        
        if (isColliding)
        {
            if (otherPaths.lanetype == this.lanetype)
            {

                //Debug.Log(" the tag so stopped");
                if (CheckForDistance(other.gameObject))
                {

                    currentState = CarState.Stopped;
                    speed = 0f;
                    itsred = true;
                }
                else if (CheckForDistance(this.gameObject))
                {

                    otherPaths.currentState = CarState.Stopped;
                    otherPaths.speed = 0f;
                    itsred = true;

                }
                else if (otherPaths.itsred)
                //  else if(otherPaths.c<2&&!itsred)
                {
                    speed = 0;
                    itsred = true;
                    //   currentState =CarState.Stopped;
                    //  otherPaths.speed = 0;
                    // Debug.Log("Speed is set to zero");
                    // otherPaths.currentState = CarState.Stopped;
                    //  currentState = CarState.Stopped;
                    // otherPaths.speed = 0;
                    // speed = 0f;
                    //  GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                }
            }

            else if (otherPaths.speed != 0)//
            {

                //   GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                // currentState = CarState.Stopped;
                speed = 0;
            }

            // Reduce speed gradually instead of stopping completely

        }
        else if (!isColliding)
        {
                    //Debug.LogWarning("Speed should be back to sp");
               // Gradually increase speed back to normal
              //currentState = CarState.Moving;
               speed = sp;
        }
    
     }
          void ChangeTag()
         {
        if (pathno == 0 || pathno == 1)
        {
            lanetype = LaneType.LeftLane;
        }
        else if (pathno == 2 || pathno == 3)
        {
            lanetype = LaneType.RightLane;
        }
        else if (pathno == 4 || pathno == 5)
        {
            lanetype = LaneType.FrontLane;
        }

           }
    GameObject FindLane()
    {
        
            if (pathno == 0 || pathno == 1)
            {
                return left;
            }
            else if (pathno == 2 || pathno == 3)
            {
                 return right;
            }
            else if (pathno == 4 || pathno == 5)
            {
            return front;
            }
        return null;

    }
    int Road()
    {

        if (pathno == 0 || pathno == 1)
        {
            return 0;
        }
        else if (pathno == 2 || pathno == 3)
        {
            return 1;
        }
        else if (pathno == 4 || pathno == 5)
        {
            return 2;
        }
        return 0;
    }
        void CheckForRed()
        {
       // Debug.Log(Vector3.Distance(this.gameObject.transform.position, FindLane().transform.position));
        if (CheckForDistance(this.gameObject))
            {
              
            
                int l = Road();
                if (traffic.trafficlights[l].isRed)
                {
                // Debug.Log("it is red");
                itsred = true;
                    currentState = CarState.Stopped;
                   
                }
                else
                {
                itsred = false;
                    currentState = CarState.Moving;
                }
            
                 
            }
        }
    bool CheckForDistance(GameObject t)
    {   
      if(isinjunc)
        {
            return false;
        }
        if (Vector3.Distance(t.transform.position,FindLane().transform.position) < 3f)
        {
            return true;
        }
        return false;
    }



    }

  