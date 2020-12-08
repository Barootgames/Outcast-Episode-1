using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RemoteControlRiddle : MonoBehaviour
{
    public int pattern = 34807591;

    public string currentPattern = "";

    public Text currentPatternText;
    // Start is called before the first frame update
    public void SetDigit(int digit)
    {
        currentPattern += digit;
    }

    public void ClearPattern()
    {
        currentPattern = "";
    }

    private void Update()
    {
        currentPatternText.text = currentPattern;
    }

    public void OK()
    {
        if (CheckPattern()) {
            print("OK");
        }
        else
        {
            print("NOK");
        }
    }

    public bool CheckPattern()
    {
        int currentPatternInt = int.Parse(currentPattern);

        return currentPatternInt == pattern ? true : false;
    }
}
