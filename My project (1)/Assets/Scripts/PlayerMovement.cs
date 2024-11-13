using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float runSpeed = 8f;
    [SerializeField] private float rotateSpeed = 220f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float gravity = -9.81f;

    private CharacterController controller;
    private new Camera camera;
    private Vector3 velocity;
    private float cameraPitch;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        camera = GetComponentInChildren<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        HandleMovement();
        HandleRotation();
        HandleJumping();
    }

    private void HandleMovement()
    {
        Vector2 inputDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"))
                                 * (Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed);
        Vector3 move = transform.right * inputDirection.x + transform.forward * inputDirection.y;
        velocity = new Vector3(move.x, velocity.y, move.z);
        controller.Move(velocity * Time.deltaTime);
    }

    private void HandleRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * rotateSpeed * Time.deltaTime;
        transform.Rotate(Vector3.up * mouseX);

        float mouseY = Input.GetAxis("Mouse Y") * rotateSpeed * Time.deltaTime;
        cameraPitch = Mathf.Clamp(cameraPitch - mouseY, -90f, 90f);
        camera.transform.localRotation = Quaternion.Euler(cameraPitch, 0f, 0f);
    }

    private void HandleJumping()
    {
        velocity.y = controller.isGrounded ? (Input.GetKeyDown(KeyCode.Space) ? jumpForce : -0.1f) : velocity.y + gravity * Time.deltaTime;
        controller.Move(new Vector3(0, velocity.y, 0) * Time.deltaTime);
    }
}
