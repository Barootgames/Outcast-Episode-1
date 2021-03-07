using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Step : MonoBehaviour
{

    private static bool[] _steps = new bool[50];
    public bool[] Steps = new bool [50];

    // info
    /*
      // 0 = tutorail_walk  // 1 = tutorail_run
    // 2 = tutorail_interaction  // 3 = tutorail_rest
    // 4 = First thunder    // 5 = secend thunder
    // 6 = telephone_bird  // 7 = dialog_jamshid_1
    // 8 = pickup_fuse2   // 9 = fuse_in_place
    // 10 = button_fuse_on   // 11 =



    */

    private void Awake ()
    {

        for (int i = 0; i < 50; i++)
        {           
            Steps[i] = _steps[i];
        }


    }

    public void  DoWork (int element)
    {
        _steps[element] = true;
        Steps[element] = true;
    }

}
