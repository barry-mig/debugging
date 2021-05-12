using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player; //reference player game object's position
    private Vector3 offset; //store the offset value: private b/c we want to set the value in the script


    // Start is called before the first frame update
    void Start()
    {
        //Subtract transform position of player from camera game object. Only needs to be calculated once, so put in Start.
        offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //when player uses the keys to move, camera is first aligned
        //only matches position and not rotation. if made child in the inspector would match both.
        //we don't control which order all the updates happen. other scripts update functions could be called first.
        //so that's why we run LateUpdate -- runs once per frame but after all the other udpates are done.
        transform.position = player.transform.position + offset;     
    }
}
