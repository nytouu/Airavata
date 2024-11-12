using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FreeCamController : MonoBehaviour
{
    private CharacterController _playerController;

    private Vector3 _playerVelocity;
    private InputManager _inputManager;
    private Transform _orientation;

    [SerializeField]
    private Transform mainCameraTransform;
    [SerializeField]
    [Range(0.1f, 10f)]
    private float walkSpeed = 2.0f;
    [SerializeField]
    [Range(2f, 20f)]
    private float sprintSpeed = 5.0f;
    [SerializeField]
    [Range(2f, 15f)]
    private float horizontalSpeed = 5.0f;

    private void Start()
    {
        _playerController = GetComponent<CharacterController>();
        _inputManager = GameManager.GetManager<InputManager>();
        _orientation = new GameObject("Player Orientation").transform;
    }

    void Update()
    {
        // Orientation
        _orientation.rotation = Quaternion.Euler(0, mainCameraTransform.rotation.eulerAngles.y, 0);

        // Player movement
        Vector2 movement = _inputManager.GetPlayerMovement();
        bool isSprinting = _inputManager.GetPlayerSprint();
        float playerSpeed = isSprinting ? sprintSpeed : walkSpeed;

        Vector3 move = new Vector3(movement.x, 0, movement.y);
        move = _orientation.forward * move.z + _orientation.right * move.x;
        move.y = 0f;
        _playerController.Move(move * Time.deltaTime * playerSpeed);

        _playerVelocity.y = 0;
        if (_inputManager.GetPlayerRise() != 0f)
        {
            _playerVelocity.y += horizontalSpeed;
        }
        else if (_inputManager.GetPlayerDescend() != 0f)
        {
            _playerVelocity.y -= horizontalSpeed;
        }

        _playerController.Move(_playerVelocity * Time.deltaTime);
    }
}
