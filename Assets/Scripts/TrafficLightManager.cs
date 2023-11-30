using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLightManager : MonoBehaviour
{
    public List<TrafficLight> trafficlights;
    public List<GameObject> lights;
    int size;
    //0 is right 1 is left
    public float delay;
    public float timer;
    public ReadInput Input;
    int j;
    // Start is called before the first frame update
    void Start()
    {
        j = 0;
        size =3;
       for(int i=0;i<size;i++)
        {
            trafficlights[i] = lights[i].GetComponent<TrafficLight>();
            trafficlights[i].TurnRed();
        }
       // Debug.Log("0 is Green now");
       trafficlights[0].TurnGreen();
        SetDelay(0);
       
    }

    // Update is called once per frame
    void Update()
    { 
    
        timer = timer + Time.deltaTime;
       
     
        if(timer>delay)
        {
            
            // Debug.Log(j);
            if (j >= size-1)
            {
                j = 0;
            }
            else
            {
                j++;
            }

            Rotate(j);
            timer = 0;
        }
    }
    void Rotate(int i)
    {
      
        if (i>0)
        {
            
            trafficlights[i - 1].TurnRed();
        }
        else if(i<=0)
        {
            trafficlights[size - 1].TurnRed();
        }
        if(i<size)
        {
            trafficlights[i].TurnGreen();
        }
        else if(i == size)
        {
            trafficlights[0].TurnGreen();
        }
        
        SetDelay(i);
      // Debug.Log(i + " is Green Now");
    }
    public string GreenLane()
    {
        if(trafficlights[0].isGreen)
        {
            return "Right";
        }
        else if(trafficlights[1].isGreen)
        {
            return "Left";
        }
        else if (trafficlights[2].isGreen)
        {
            return "Front";
        }
        else
        {
            return "Unknown";
        }

    }
    void SetDelay(int l)
    {  
        
        if (l==0)
        {
            delay = Input.wait_R;

        }
        else if( l==1)
        {
            delay =Input.wait_L;
        }
        else if(l==2)
        {
            delay = Input.wait_F;
        }
       
    }
}
