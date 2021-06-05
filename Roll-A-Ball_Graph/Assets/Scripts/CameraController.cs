using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static DebugVariable;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset;
    public TextMeshProUGUI varName;
    public TextMeshProUGUI varValue;
    public GameObject myDebugVariable;
    private DebugVariable DebugVariableScript;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - player.transform.position;
        varName.text = "Var: transform.position.x\nType: float\nObject: Camera\nScript: CameraController.cs\nLast Line Altered: 22";
        varValue.text = "Value: " + transform.position.x.ToString();
        DebugVariableScript = myDebugVariable.GetComponent<DebugVariable>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //offset = new Vector3(1, 0, 0);
        //transform.position = offset; //player.transform.position + offset; //Bug: incorrect offset

        transform.position = player.transform.position + offset;
        DebugVariableScript.AddPoint(transform.position.x); //was previously in OnMove
        varValue.text = "Value: " + transform.position.x.ToString(); //was previously in OnMove
    }
}
