using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    public GameObject target;

    private bool isMoving;
    private bool isDead;
    [SerializeField]
    public KinematicStates ks;

    private Attack attack;
    private Kinematics kinematics;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        attack = GetComponent<Attack>();

        kinematics = GetComponent<Kinematics>();
        kinematics.SetTarget(target.transform);
        kinematics.SetState(ks);
        isMoving = true;
    }

    // Update is called once per frame
    void Update()
    {

        if(kinematics.isArrived()) {
            // Player is in range, attack
            isMoving = false;
            Attack();
        } else if (kinematics.GetState() == KinematicStates.Arrive && !kinematics.isArrived()) {
            // Player is out of range, resume chasing
            isMoving = true;
        }

        if(isMoving) {
            animator.SetFloat("Speed", kinematics.GetSpeed()/2);
            animator.SetTrigger("Walk");
        }
      
        if(isDead) {
            animator.SetTrigger("Die");
        }
        if(!isMoving  && !isDead) {
            animator.SetBool("isIdle", true);
        } else {
            animator.SetBool("isIdle", false);
        }

    }


    void Attack() {
        animator.SetTrigger("Attack");
        attack.SetTarget(target);
        attack.NormalAttack();
    }

}
