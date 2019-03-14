using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum KinematicStates
{
    Wander,
    Seek,
    Flee,
    Arrive,
    Idle
}

public class Kinematics : MonoBehaviour
{

    [SerializeField]
    float maxSpeed;
    [SerializeField]
    float wanderSpeed;
    [SerializeField]
    float maxAngularVelocity;
    [SerializeField]
    float arriveSlowRadius = 5;
    [SerializeField]
    float arriveRadius = 2;


    private bool arrived;

    Transform target;

    KinematicStates currentState;

    Rigidbody rb;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentState = KinematicStates.Wander;
    }

    // Update is called once per frame
    void Update()
    {
        arrived = false;

        if (currentState == KinematicStates.Wander)
        {
            KinematicWander();
        }
        else if (currentState == KinematicStates.Seek)
        {
            KinematicSeek(target);
        }
        else if (currentState == KinematicStates.Arrive)
        {
            KinematicArrive(target);
        }
        else if (currentState == KinematicStates.Flee)
        {
            KinematicFlee(target);
        }
        else if (currentState == KinematicStates.Idle)
        {
            KinematicIdle();
        }

    }

    void KinematicSeek(Transform target)
    {
        transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
        MoveForwardAtSpeed(maxSpeed);
    }

    void KinematicWander()
    {
        SetAngularVelocityRandom();
        MoveForwardAtSpeed(wanderSpeed);
    }

    void KinematicArrive(Transform target)
    {
        //Distance to target / arrive radius * max speed
        transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
        float distanceToTarget = CustomTools.GetDistanceToTarget(transform, target);
        float distanceComponent = Mathf.Max(distanceToTarget / arriveSlowRadius, 1);
        print(distanceComponent);
        //print("boom: info");
        //print(distanceToTarget);
        //print(distanceComponent);
        //newVelocity *= (distanceToTarget / arriveRadius);
        if (distanceToTarget > arriveRadius)
        {
            if (distanceToTarget < arriveSlowRadius)
            {
                //rb.velocity = newVelocity;
                //print("haven't arrived");
                MoveForwardAtSpeed(distanceComponent * maxSpeed);
            }
            else {
                MoveForwardAtSpeed(maxSpeed);
            }
        }
        else
        {
            //print("ARRIVED");
            MoveForwardAtSpeed(0);
            arrived = true;

        }
    }

    void KinematicFlee(Transform target)
    {
        transform.LookAt(transform.position * 2 - target.position);
        MoveForwardAtSpeed(maxSpeed);
    }

    void KinematicIdle()
    {
        MoveForwardAtSpeed(0);
    }

    // Getters and Setters --------------------------------------------

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    public void SetState(KinematicStates newState)
    {
        currentState = newState;
    }

    public KinematicStates GetState()
    {
        return currentState;
    }

    public float GetSpeed() {
        return maxSpeed;
    }

    public bool isArrived() {
        return arrived;
    }

    // Helper functions --------------------------------------------

    private void MoveForwardAtSpeed(float speed)
    {
        rb.velocity = new Vector3(transform.forward.x * speed, 0, transform.forward.z * speed);

        //if (transform.position.y > 0.5)
        //{
        //    transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);
        //}
        if (transform.rotation.x != 0 || transform.rotation.y != 0)
        {
            transform.rotation = new Quaternion(0, transform.rotation.y, 0, transform.rotation.w);
        }
    }

    private void AddAngularVelocityY(float velocity)
    {
        if (rb.angularVelocity.y < maxAngularVelocity && rb.angularVelocity.y > -maxAngularVelocity)
        {
            rb.angularVelocity = new Vector3(0f, rb.angularVelocity.y + velocity, 0f);
        }
    }

    private void SetAngularVelocityRandom()
    {
        float newAngularVelocity = Random.Range(-maxAngularVelocity / 6, maxAngularVelocity / 6);

        AddAngularVelocityY(newAngularVelocity);
    }
}
