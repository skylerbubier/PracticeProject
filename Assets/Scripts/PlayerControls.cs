using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PlayerControls : MonoBehaviour
{

    #region const

    // Movement Constants
    private const float CENTERING_THRESHOLD = 0.01f;
    private const float CENTERING_SPEED = 0.1f;
    private const float ROTATE_RESET_SPEED = 0.1f;

    #endregion

    #region private

    // Private
    private PlayerShip _ship;
    private bool _isCentering = false;
    private bool _isMovementLocked = false;
    private bool _isFiring = false;
    private float _fireElapsed = 0f;

    #endregion

    // Player Movement Bounds
    public float moveMinY = -3.75f;
    public float moveMaxY = 3.75f;
    public float keyMoveSpeed = 0.3f;
    public float touchChaseSpeed = 0.2f;
    public float fireRate = 1f;

    /// <summary>
    /// Returns if the player is in a "reset mode" and the ship is moving to center
    /// to the screen. Movement will be locked in this time.
    /// </summary>
    public bool IsCentering
    {
        get { return _isCentering; }
    }

    /// <summary>
    /// Returns if the player cannot control the ship. This is used when the ship
    /// is Centering or at the beginning / end of the scene.
    /// </summary>
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

    public bool IsFiring
    {
        get { return _isFiring; }
        set { _isFiring = true; }
    }
    
    /// <summary>
    /// Locks Movement until the player's ship is Centered. Use IsMovementLocked = true to keep the lock.
    /// </summary>
    public void CenterPlayer()
    {
        _isCentering = true;
    }

    private void Start()
    {
        _ship = gameObject.GetComponentInChildren<PlayerShip>();
    }

    private void Update()
    {
        Update_Movement();
        Update_Firing();
    }

    private void Update_Movement()
    {
        var posYOrig = transform.position.y;
        var posYNew = posYOrig;


        if (_isCentering)
        {
            // Center Player
            posYNew = Mathf.Lerp(transform.position.y, 0, CENTERING_SPEED);

            // Finish Centering once close enough
            if (Mathf.Abs(posYNew) < CENTERING_THRESHOLD)
            {
                _isCentering = false;
            }
        }
        else if (!_isMovementLocked)
        {
            // Keyboard Input for PC Testing
            if (Input.GetKey(KeyCode.W))
            {
                posYNew += keyMoveSpeed;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                posYNew -= keyMoveSpeed;
            }

            // Handle Touch Input
            for (int i = 0; i < Input.touches.Length; i++)
            {
                var touch = Input.GetTouch(i);

                Debug.Log(string.Format("I am being touched by {0} in the {1}.", i, touch.position));

                posYNew = Mathf.Lerp(transform.position.y, touch.position.y, touchChaseSpeed);
            }

        }

        // Set New Player Position
        transform.position = new Vector3(
            transform.position.x,
            posYNew,
            transform.position.z);

        // Tilt the ship to simulate fluid movement
        if (_ship != null)
        {
            var posYdelta = Mathf.Clamp(posYNew - posYOrig, -90f, 90f);
            _ship.transform.rotation = new Quaternion(
                Mathf.Lerp(_ship.transform.rotation.x, posYdelta, ROTATE_RESET_SPEED),
                _ship.transform.rotation.y,
                _ship.transform.rotation.z,
                _ship.transform.rotation.w);

        }

        // Clamp Player Position
        transform.position = new Vector3(
            transform.position.x,
            Mathf.Clamp(transform.position.y, moveMinY, moveMaxY),
            transform.position.z);
    }

    private void Update_Firing()
    {

        if (Input.GetKeyDown(KeyCode.F))
        {
            _isFiring = !_isFiring;
        }

        if (_isFiring)
        {
            _fireElapsed += Time.deltaTime;
            if (_fireElapsed >= fireRate)
            {
                _fireElapsed -= fireRate;

                //TODO: Spawn lasers!

            }
        }
    }

}
