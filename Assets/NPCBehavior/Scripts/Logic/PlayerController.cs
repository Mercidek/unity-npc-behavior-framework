using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector2 moveInput;

    private Quaternion targetRotation;

    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float rotationSpeed = 10f;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        targetRotation = transform.rotation;
    }

    private void Update()
    {
        Vector3 moveDir = new Vector3(-moveInput.y, 0f, moveInput.x);
        controller.Move(moveDir * moveSpeed * Time.deltaTime);

        if(moveDir != Vector3.zero)
        {
            targetRotation = Quaternion.LookRotation(moveDir);
        }

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
}
