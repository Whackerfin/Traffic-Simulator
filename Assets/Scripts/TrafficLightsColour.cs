using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrafficLightsColour : MonoBehaviour
{
  
    public Image Left;
    public Image Right;
    public Image Front;
    
    
    public TrafficLightManager tlm;
   
    // Start is called before the first frame update
    void Start()
    {
        SetRed(Left);
        SetRed(Right);
        SetRed(Front);

    }
    private void Awake()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        if(tlm.trafficlights[0].isRed)
        {
            SetRed(Right);
        }
        else if (tlm.trafficlights[0].isGreen)
        {
            SetGreen(Right);
            
        }

        if (tlm.trafficlights[1].isRed)
        {
            SetRed(Left);
        }
        else if (tlm.trafficlights[1].isGreen)
        {
            SetGreen(Left);
            
        }

        if (tlm.trafficlights[2].isRed)
        {
            
            SetRed(Front);
        }
        else if (tlm.trafficlights[2].isGreen)
        {
            SetGreen(Front);
        }

    }
    void SetRed(Image t)
    {
      
        t.color = Color.red;
    }
    void SetGreen(Image t)
    {
      
            t.color = Color.green;
    }
}
