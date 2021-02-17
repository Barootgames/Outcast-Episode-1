using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RemoteControlRiddle : MonoBehaviour
{
    public int pattern = 34807591;

    public string currentPattern = "";

    public Text currentPatternText;

    public GameObject symbol;
    // Start is called before the first frame update
    public void SetDigit(int digit)
    {
        currentPattern += digit;
    }

    public void ClearPattern()
    {
        currentPattern = "";
        currentPatternText.color = Color.white;
    }

    private void Start()
    {
        symbol.SetActive(false);
        GameDataController.instance.gameData.isOnCanvas = true;
    }

    private void Update()
    {
        currentPatternText.text = currentPattern;
    }

    public void OK()
    {
        if (CheckPattern()) {
            currentPatternText.color = Color.green;
            GameDataController.instance.gameData.isOnCanvas = false;
            symbol.SetActive(true);
            gameObject.SetActive(false);
        }
        else
        {
            currentPatternText.color = Color.red;
        }
    }

    public bool CheckPattern()
    {
        int currentPatternInt = int.Parse(currentPattern);

        return currentPatternInt == pattern ? true : false;
    }
}
