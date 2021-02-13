using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SymbolicLockRiddleController : MonoBehaviour
{
    public Sprite[] Symbols;

    public Image[] SlotImages;

    public int[] LockSymbols = { 1, 3, 4, 5, 0, 7, 2, 6 };

    public int[] CorrectOrder = { 1, 2, 3, 4, 0, 5, 6, 7 };

    public bool isCorrect = false;
    public bool finished = false;

    public Text order;
    // Start is called before the first frame update
    void Start()
    {
        PrintOrder();
    }

    // Update is called once per frame
    void Update()
    {
        if (isCorrect && !finished)
        {
            finished = true;
            order.color = Color.green;
            print("correct");
        }
    }

    public void UpwardSlot(int slotIndex)
    {
        LockSymbols[slotIndex] = (LockSymbols[slotIndex] + 1) % LockSymbols.Length;
        //SlotImages[slotIndex].sprite = Symbols[LockSymbols[slotIndex]];
        isCorrect = CheckOrder();
        PrintOrder();
    }

    public void DownwardSlot(int slotIndex)
    {
        LockSymbols[slotIndex] = (LockSymbols[slotIndex] - 1) < 0 ? (LockSymbols[slotIndex] - 1) + LockSymbols.Length : (LockSymbols[slotIndex] - 1);
        //SlotImages[slotIndex].sprite = Symbols[LockSymbols[slotIndex]];
        isCorrect = CheckOrder();
        PrintOrder();
    }

    public void PrintOrder()
    {
        string s = "{ ";
        for (int i = 0; i < LockSymbols.Length; i++)
        {
            s += LockSymbols[i].ToString() + ", ";
        }

        s += "}";

        order.text = s;
    }

    public bool CheckOrder()
    {
        for(int i = 0; i < LockSymbols.Length; i++)
        {
            if (LockSymbols[i] != CorrectOrder[i])
                return false;
        }
        return true;
    }
}
