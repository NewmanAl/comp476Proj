using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    private CharacterController characterController;
    private Animator animator;

    
    public float turnSpeed = 1f;
    public float forwardSpeed = 50;
    public float backwardSpeed = 50;

    // Start is called before the first frame update
    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, 0, vertical);


        animator.SetFloat("forward", vertical);
        transform.Rotate(Vector3.up, horizontal * turnSpeed * Time.deltaTime);


        if(vertical != 0)
        {
            float chooseSpeed = (vertical > 0) ? forwardSpeed : backwardSpeed;
            characterController.SimpleMove(transform.forward * chooseSpeed * vertical);

        }






      
    }
}
