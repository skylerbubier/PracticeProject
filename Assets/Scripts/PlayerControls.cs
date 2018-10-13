using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerControls : MonoBehaviour
{

    #region const

    // Movement Constants
    private const float MOVE_MIN_X = -4f;
    private const float MOVE_MAX_X = 4f;
    private const float CENTERING_THRESHOLD = 0.01f;
    private const float CENTERING_SPEED = 0.1f;
    private const float ROTATE_RESET_SPEED = 0.1f;

    #endregion

    #region private

    // Private
    private Player _player;
    private PlayerShip _ship;
    private bool _isCentering = false;
    private bool _isMovementLocked = false;
    private bool _isFiring = false;
    private float _fireElapsed = 0f;

    #endregion
    

    public float keyMoveSpeed = 0.3f;
    public float touchChaseSpeed = 0.2f;


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
        _player = GetComponent<Player>();
        _ship = gameObject.GetComponentInChildren<PlayerShip>();
    }

    private void Update()
    {
        Update_Movement();
        Update_Firing();
    }

    private void Update_Movement()
    {
        var posXOrig = transform.position.x;
        var posXNew = posXOrig;


        if (_isCentering)
        {
            // Center Player
            posXNew = Mathf.Lerp(transform.position.x, 0, CENTERING_SPEED);

            // Finish Centering once close enough
            if (Mathf.Abs(posXNew) < CENTERING_THRESHOLD)
            {
                _isCentering = false;
            }
        }
        else if (!_isMovementLocked)
        {
            // Keyboard Input for PC Testing
            if (Input.GetKey(KeyCode.D))
            {
                posXNew += keyMoveSpeed;
            }
            if (Input.GetKey(KeyCode.A))
            {
                posXNew -= keyMoveSpeed;
            }

            // Handle Touch Input
            for (int i = 0; i < Input.touches.Length; i++)
            {
                var touch = Input.GetTouch(i);

                Debug.Log(string.Format("I am being touched by {0} in the {1}.", i, touch.position));

                posXNew = Mathf.Lerp(transform.position.x, touch.position.x, touchChaseSpeed);
            }

        }

        // Set New Player Position
        transform.position = new Vector3(
            posXNew,
            transform.position.y,
            transform.position.z);

        // Tilt the ship to simulate fluid movement
        if (_ship != null)
        {
            var posXdelta = Mathf.Clamp(posXOrig - posXNew, -90f, 90f);
            _ship.transform.rotation = new Quaternion(
                _ship.transform.rotation.x,
                Mathf.Lerp(_ship.transform.rotation.y, posXdelta, ROTATE_RESET_SPEED),
                _ship.transform.rotation.z,
                _ship.transform.rotation.w);

        }

        // Clamp Player Position
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, MOVE_MIN_X, MOVE_MAX_X),
            transform.position.y,
            transform.position.z);
    }

    private void Update_Firing()
    {

        var fireRate = GetComponent<Player>().fireRate;

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
                foreach (var weapon in _player.Weapons)
                {
                    weapon.Fire(_player);
                }

            }
        }
    }

}
