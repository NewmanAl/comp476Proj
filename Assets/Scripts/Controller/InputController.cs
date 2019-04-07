using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public float Vertical;
    public float Horizontal;
    public Vector2 MouseInput;
    public bool fire;
    public bool Reload;
    public bool jump;
    public bool placeTNT;
    private bool lastJump;
    public bool IsWalking;
    public bool IsRunning;
    public bool IsCrouched;
  
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vertical = Input.GetAxis("Vertical");
        Horizontal = Input.GetAxis("Horizontal");
        MouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        //return true if pressed
        fire = Input.GetButton("Fire1");
        placeTNT = Input.GetKey(KeyCode.T);
        Reload = Input.GetKey(KeyCode.R);
        IsRunning = Input.GetKey(KeyCode.LeftShift);
        IsCrouched = Input.GetKey(KeyCode.C);
        bool newJump = Input.GetKey(KeyCode.Space);
        if(newJump != lastJump && newJump == true)
        {
            print("jump trigger");
            jump = true;
        }
        else
        {
            jump = false;
        }
        lastJump = newJump;
    }
}
