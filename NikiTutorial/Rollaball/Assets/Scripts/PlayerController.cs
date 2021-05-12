using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; //access the code and functions in the input system
using TMPro; //write code relating to the UI element

public class PlayerController : MonoBehaviour
{
    public float speed = 0; //because speed variable is public we can find it in the inspector
    public TextMeshProUGUI countText;
    public GameObject winTextObject;

    private Rigidbody rb; //reference to the rigidbody you need to access
    private int count;
    private float movementX;
    private float movementY;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;

        SetCountText();
        winTextObject.SetActive(false);
    }

    void OnMove(InputValue movementValue) //InputValue is the data type
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 13)
        {
            winTextObject.SetActive(true);
        }
    }

    //Vector3 variables: x, y, z axes
    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
    }

    //other gives you a way to identify the colliders that the sphere hit
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;

            SetCountText();
        }
    }

    //want only pickup game objects to disappear, not the ground or walls
    //tags allow you to identify the game object by comparing the tag value to a string

}

