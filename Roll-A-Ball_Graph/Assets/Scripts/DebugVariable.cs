using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugVariable : MonoBehaviour
{
    // Start is called before the first frame update
    // Update is called once per frame
    public void printVariable(float num)
    {
        Debug.Log(num);
    }
}
