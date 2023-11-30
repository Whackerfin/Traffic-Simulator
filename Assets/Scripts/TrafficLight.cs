using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLight : MonoBehaviour
{
    public bool isRed;
    public bool isGreen;
    public void TurnRed()
    {
        isRed = true;
        isGreen = false;
    }
    public void TurnGreen()
    {
        isGreen = true;
        isRed = false;
    }

}
