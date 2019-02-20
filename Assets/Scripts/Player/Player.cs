using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(MoveController))]
public class Player : MonoBehaviour
{
    [System.Serializable]
    public class MouseInput //soomth dumping
    {
        public Vector2 Dumping;
        public Vector2 Sensitivity;
        public bool LockMouse;
    }

    [SerializeField] float runSpeed;
    [SerializeField] float walkSpeed;
    [SerializeField] float crouchSpeed;
    [SerializeField] MouseInput MouseControl;

    public MoveController MoveController;

    public aimPoint crossHair;
    InputController playerInput;

    Vector2 mouseInput;

    // Start is called before the first frame update
    void Awake()
    {
        playerInput = GameManager.Instance.InputController;
        MoveController = GetComponent<MoveController>();
        crossHair = GetComponentInChildren<aimPoint>();
        if (MouseControl.LockMouse)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Horizontal : " + inputController.Horizontal);
        //print("Mouse : " + inputController.MouseInput);

        Move();
        lookAround();

    }

    void Move()
    {
        float moveSpeed = walkSpeed;
        if (playerInput.IsRunning)
            moveSpeed = runSpeed;
        if (playerInput.IsCrouched)
            moveSpeed = crouchSpeed;
        Vector2 direction = new Vector2(playerInput.Vertical * moveSpeed, playerInput.Horizontal * moveSpeed);
        MoveController.Move(direction);
    }

    void lookAround()
    {
        mouseInput.x = Mathf.Lerp(mouseInput.x, playerInput.MouseInput.x, 1f / MouseControl.Dumping.x);
        mouseInput.y = Mathf.Lerp(mouseInput.y, playerInput.MouseInput.y, 1f / MouseControl.Dumping.y);

        transform.Rotate(Vector3.up * mouseInput.x * MouseControl.Sensitivity.x);

        crossHair.LookHeight(mouseInput.y * MouseControl.Sensitivity.y);
    }

   
}
