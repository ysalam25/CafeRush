using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool canMove = true;
    public CharacterController playerController;


    [SerializeField] private Camera playerCamera;
    public float mouseSensitivity = 100f;
    public float walkSpeed = 4f;

    private float xRotation = 0f;

    private void Update()
    {
        Move();
        HandleCamera();
    }

    private void Move()
    {
        if (!canMove)
            return;
        Vector3 moveDirection = transform.right * Input.GetAxisRaw("Horizontal") + transform.forward * Input.GetAxisRaw("Vertical");
        playerController.Move(moveDirection * walkSpeed * Time.deltaTime);
    }

    private void HandleCamera()
    {
        xRotation -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        float yRotation = Input.GetAxis("Mouse X") * mouseSensitivity;
        transform.Rotate(new Vector3(0f,yRotation,0f));
    }

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
}
