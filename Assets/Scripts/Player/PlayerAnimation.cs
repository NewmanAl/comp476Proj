using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
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
}
