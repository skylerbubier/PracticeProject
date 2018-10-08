using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PlayerControls : MonoBehaviour
{

    // Movement Constants
    private const float CENTERING_THRESHOLD = 0.01f;
    private const float CENTERING_SPEED = 0.1f;

    // Player Movement Bounds
    public float moveMinY = -3.75f;
    public float moveMaxY = 3.75f;
    public float keyMoveSpeed = 0.3f;
    public float touchChaseSpeed = 0.2f;

    private bool _isCentering = false;

    public bool IsCentering
    {
        get { return _isCentering; }
    }

    private bool _isMovementLocked = false;

    public bool IsMovementLocked
    {
        get { return _isMovementLocked; }
        set
        {
            _isMovementLocked = value;
            if (!value)
                _isCentering = false;
        }
    }

    public void CenterPlayer()
    {
        _isCentering = true;
    }

    private void Update ()
    {
        if (_isCentering)
        {
            // Center Player
            transform.position = new Vector3(
                transform.position.x,
                Mathf.Lerp(transform.position.y, 0, CENTERING_SPEED),
                transform.position.z);

            // Finish Centering once close enough
            if (Mathf.Abs(transform.position.y) < CENTERING_THRESHOLD)
            {
                _isCentering = false;
            }
        }
        else if (!_isMovementLocked)
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

            // TODO: Remove
            // TEST Center Player
            if (Input.GetKeyDown(KeyCode.R))
            {
                CenterPlayer();
            }

            // Handle Touch Input
            for (int i = 0; i < Input.touches.Length; i++)
            {
                var touch = Input.GetTouch(i);

                Debug.Log(string.Format("I am being touched by {0} in the {1}.", i, touch.position));

                transform.position = new Vector3(
                    transform.position.x,
                    Mathf.Lerp(transform.position.y, touch.position.y, touchChaseSpeed),
                    transform.position.z);
            }

            // 
            if (Input.GetKey(KeyCode.R))
            {
                transform.position = new Vector3(
                    transform.position.x,
                    Mathf.Lerp(transform.position.y, 0, touchChaseSpeed),
                    transform.position.z);
            }
        }

        // Clamp Player Position
        transform.position = new Vector3(
            transform.position.x,
            Mathf.Clamp(transform.position.y, moveMinY, moveMaxY),
            transform.position.z);
    }

}
