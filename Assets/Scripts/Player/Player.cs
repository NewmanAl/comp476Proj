using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(MoveController))]
public class Player : MonoBehaviour
{
    public static Player instance;

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
    public PlayerAim playerAim;
    public float moveSpeed = 0;
    public GameObject TNT;
    public MoveController MoveController;
    public aimPoint crossHair;
    public bool inputEnabled = true;
    public Vector2 direction;
    public Transform hand;
    InputController playerInput;

    public bool resource1 = false;
    public bool resource3 = false;
    public bool resource2 = false;
    Vector2 mouseInput;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
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
        Move();
        lookAround();
       //when we have our items to be collected use the code below
       if (resource1 && resource2 && resource3)
       {
           placeTNT();
       }

      // placeTNT();
    }

    void Move()
    {
        moveSpeed = 0;
        if (inputEnabled == true)
        {
            moveSpeed = walkSpeed;
            if (playerInput.IsRunning)
                moveSpeed = runSpeed;
            if (playerInput.IsCrouched)
                moveSpeed = crouchSpeed;
        }

        direction = new Vector2(playerInput.Vertical * moveSpeed, playerInput.Horizontal * moveSpeed);
        MoveController.Move(direction);
    }

    void lookAround()
    {
        mouseInput.x = Mathf.Lerp(mouseInput.x, playerInput.MouseInput.x, 1f / MouseControl.Dumping.x);
        mouseInput.y = Mathf.Lerp(mouseInput.y, playerInput.MouseInput.y, 1f / MouseControl.Dumping.y);

        transform.Rotate(Vector3.up * mouseInput.x * MouseControl.Sensitivity.x);

      //  crossHair.LookHeight(mouseInput.y * MouseControl.Sensitivity.y);

     //   playerAim.SetRotation(mouseInput.y * MouseControl.Sensitivity.y);
    }

    void placeTNT()
    {
        if (playerInput.placeTNT)
        {
            GameObject TNTtemp = Instantiate(TNT, hand);
        }
        
    }



   
}
