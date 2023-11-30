using UnityEngine;
using TMPro;

public class Traffic : MonoBehaviour
{
    public TrafficLightManager tlm;
    [SerializeField] private TextMeshProUGUI traff;
    [SerializeField] private TextMeshProUGUI counter;
    
    // Start is called before the first frame update
  

    // Update is called once per frame
    void Update()
    {
     
        traff.text = tlm.GreenLane();
        counter.text = (tlm.delay - tlm.timer).ToString("0");


    }
}
