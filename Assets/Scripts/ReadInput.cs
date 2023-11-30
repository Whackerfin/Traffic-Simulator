using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class ReadInput : MonoBehaviour
{
    private string filepath = "C:/Users/jake0/My files/Unity projects/Traffic/Assets/Text/" + "Input.txt";
    string[] values;
    private System.DateTime lastModifiedTime;
    
    public float wait_L;
    public float wait_R;
    public float wait_F;
    

    // Start is called before the first frame update
    void Awake()
    { 
        
        
        lastModifiedTime = File.GetLastWriteTime(filepath);
        ReadTextFile(filepath);
       
    }

    // Update is called once per frame
    void Update()
    {
        System.DateTime currentModifiedTime = File.GetLastWriteTime(filepath);
        if(currentModifiedTime != lastModifiedTime)
        {
            ReadTextFile(filepath);
           
           
            lastModifiedTime = currentModifiedTime;
           
        }
    }
    void ReadTextFile(string path)
    {
        try
        {
        
           string[] lines = File.ReadAllLines(path);

           
            if (lines.Length > 0)
            {
                string[] laneOrder = lines[0].Split('-');

             
                for (int i = 1; i < lines.Length; i++)
                {
                    if (string.IsNullOrWhiteSpace(lines[i]))
                        continue;
                    values = lines[i].Split('-');
                    if(values != null)
                    {
                        wait_L = float.Parse(values[0]);
                        wait_R = float.Parse(values[1]);
                        wait_F = float.Parse(values[2]);
                    }
                }
            }
        }
        catch (FileNotFoundException e)
        {
            Debug.LogError($"File not found: {e.Message}");
        }
        catch (IOException e)
        {
            Debug.LogError($"An error occurred while reading the file: {e.Message}");
        }
    }
 }

