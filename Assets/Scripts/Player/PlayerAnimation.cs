using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator animator;
    private Vector3 thrustVec;
    Rigidbody rb;
    InputController playerInput;
    Player player;
    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
        playerInput = GameManager.Instance.InputController;
        player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {

        animator.SetFloat("Vertical", GameManager.Instance.InputController.Vertical);
        animator.SetFloat("Horizontal", GameManager.Instance.InputController.Horizontal);
        animator.SetBool("IsRunning", GameManager.Instance.InputController.IsRunning);
        animator.SetBool("IsCrouch", GameManager.Instance.InputController.IsCrouched);
        if (GameManager.Instance.InputController.Reload)
        {
            print("reloadin");
            animator.SetTrigger("Reload");
        }
        if (GameManager.Instance.InputController.jump)
        {
            animator.SetTrigger("Jump");
        }
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, rb.velocity.z) + thrustVec;
        thrustVec = Vector3.zero;
        
    }
    //set threehold to 1 when play the reload animation
    public void OnReloadEnter()
    {
        animator.SetLayerWeight(1, 1.0f); 
    }
    public void OnReloadIdle()
    {
        animator.SetLayerWeight(1, 0);
    }
    //recive signal from animation
    public void OnJumpEnter()
    {
    
        thrustVec = new Vector3(0, 3.0f, 0);
       

    }
    public void OnJumpExit()
    {
      
        print("OnJumpExit");
    }
}
