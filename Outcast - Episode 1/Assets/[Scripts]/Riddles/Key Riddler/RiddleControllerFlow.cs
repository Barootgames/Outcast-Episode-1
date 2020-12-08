using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiddleControllerFlow : MonoBehaviour
{
    Dictionary<string, int> riddleMap = new Dictionary<string, int>();
    Dictionary<string, int> riddleMap1 = new Dictionary<string, int>();

    private void Awake()
    {
        riddleMap.Add("green", 2);
        riddleMap.Add("blue", 4);
        riddleMap.Add("red", 8);
        riddleMap.Add("grey", 5);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addToMap(string name, int index)
    {
        if (!riddleMap1.ContainsKey(name))
        {
            riddleMap1.Add(name, index);
        }
        else
        {
            riddleMap1[name] = index;
        }

        if (checkAllCorrect())
        {
            print("WIN");
        }
    }

    bool checkAllCorrect()
    {
        
        if(riddleMap1.ContainsKey("red") && riddleMap1.ContainsKey("grey") && riddleMap1.ContainsKey("green") && riddleMap1.ContainsKey("blue"))
        {
            if (riddleMap1["red"] == riddleMap["red"] && riddleMap1["green"] == riddleMap["green"] && riddleMap1["grey"] == riddleMap["grey"] && riddleMap1["blue"] == riddleMap["blue"])
            {
                return true;
            }
            else
                return false;
        }
        else 
            return false;
    }
}
