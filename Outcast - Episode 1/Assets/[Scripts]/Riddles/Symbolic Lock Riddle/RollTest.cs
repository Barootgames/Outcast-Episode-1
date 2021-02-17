using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RollTest : MonoBehaviour
{

    public Image Up;
    public Image Middle;
    public Image Down;

    public int[] order = { 0, 1, 2 };
    Transform self;
    // Start is called before the first frame update
    void Start()
    {
        self = transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RollUp()
    {
        //GetComponent<Animator>().enabled = false;
        GetComponent<RectTransform>().position = new Vector3(GetComponent<RectTransform>().position.x, -31f, GetComponent<RectTransform>().position.z);
        Color tempU = Up.color;
        Up.color = Middle.color;
        Middle.color = Down.color;
        Down.color = tempU;
    }
}
