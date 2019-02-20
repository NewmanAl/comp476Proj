using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorController : MonoBehaviour
{

    public GameObject model;
    public PlayerInput pi;
    public float walkSpeed = 2.0f;
    public float runMultifier = 2.0f;
    public float jumpVelocity = 5.0f;

    [SerializeField]
    private Animator anim;
    private Rigidbody rigid;
    private Vector3 planarVec;
    private bool lockPlanner = false;
    public Vector3 thrustVec;
    // Start is called before the first frame update
    void Awake()
    {
        pi = GetComponent<PlayerInput>();
        anim = model.GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {

        float targetRunMulti = ((pi.run) ? 2.0f : 1.0f);
       // Debug.Log(pi.Dmag);

        if (Input.GetKeyDown(pi.keyDown) || Input.GetKeyDown(pi.keyLeft))
        {
            pi.Dmag = pi.Dmag * -1;
            Debug.Log(pi.Dmag);
        }
      
            // anim.SetFloat("forward",pi.Dmag * Mathf.Lerp(anim.GetFloat("forward"), targetRunMulti, 0.5f));
        anim.SetFloat("forward", pi.Dmag);
        if (pi.jump)
        {
            anim.SetTrigger("jump");
        }
        
        if(Mathf.Abs(pi.Dmag) > 0.1f)
        {
            Vector3 targetForward = Vector3.Slerp(model.transform.forward, pi.Dvec, 0.2f);
            model.transform.forward = targetForward;

        }
        if(lockPlanner == false)
        {

            planarVec = pi.Dmag * model.transform.forward * walkSpeed * ((pi.run) ? runMultifier : 1.0f);
        }
    }


    void FixedUpdate()
    {
        rigid.velocity = new Vector3(planarVec.x, rigid.velocity.y, planarVec.z) + thrustVec;
        thrustVec = Vector3.zero;
    }

    public void OnJumpEnter()
    {
        pi.inputEnabled = false;
        lockPlanner = true;
        thrustVec = new Vector3(0, jumpVelocity, 0);
        print("on jump enter");
    }

    public void OnJumpExit()
    {
        pi.inputEnabled = true;
        print("on jump exit");
        lockPlanner = false;
    }
}
