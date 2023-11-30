using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Right_TF : MonoBehaviour
{
    float timer;
   
    GameObject temp;
    float redlighttime=4f;
    float sp =0.4f;
    int c = 0;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Car"))
        { 
           if(c==0)
            {
                other.GetComponent<Paths>().speed = 0;
                temp = other.gameObject;
                
                StartCoroutine(WaitForRed());
                
            }
            
        }
      

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Car"))
        {  
            if(c==0)
            {
                other.GetComponent<Paths>().speed = 0;
              
            }
            
        }
    }
    IEnumerator WaitForRed()
    {
        yield return new WaitForSeconds(redlighttime);
        Debug.Log("this worlks");
        
        c++;
        Debug.LogWarning(c);
    }
    private void Awake()
    {
        temp = null;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(timer);
       
        if (c == 0)
        {
            // Debug.Log("Wall is up");
        }
        if (c == 1)
        {
            timer = timer + Time.deltaTime;
            //if (temp != null)
            temp.GetComponent<Paths>().speed = sp;
            //  Debug.Log("Wall is down");

        }
        if (timer > redlighttime)
        {

            c = 0;
            timer = 0;

        }
        
    }
}
