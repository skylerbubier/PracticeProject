using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PlayerControls : MonoBehaviour
{

    public float minY = -3.75f;
    public float maxY = 3.75f;

    public float keyMoveSpeed = 0.3f;

    private void Start ()
    {
        
    }

    private void Update ()
    {

        // Keyboard Input for PC Testing
        if (Input.GetKey(KeyCode.W))
        {
            transform.position = new Vector3(
                transform.position.x,
                transform.position.y + keyMoveSpeed,
                transform.position.z);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.position = new Vector3(
                transform.position.x,
                transform.position.y - keyMoveSpeed,
                transform.position.z);
        }

        // Handle Touch Input
        for (int i = 0; i < Input.touches.Length; i++)
        {
            var touch = Input.GetTouch(i);

            Debug.Log(string.Format("I am being touched by {0} in the {1}.", i, touch.position));
        }

        // Clamp Player Position
        transform.position = new Vector3(
            transform.position.x,
            Mathf.Clamp(transform.position.y, minY, maxY),
            transform.position.z);
    }

}
