using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    // Update is called once per frame
    //Don't need forces so update is enough
    //Make pickup game object spin while the game is active
    //Rotate the game object's transform. Translate and rotate. Can use Vector3 variable or separate x, y, z floats.
    void Update()
    {
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime); //deltaTime dynamically changes
    }
}
