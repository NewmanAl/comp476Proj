using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    // Start is called before the first frame update

    [Header("==== Key Settings ====")]

    public string keyUp = "w";
    public string keyDown = "s";
    public string keyLeft = "a";
    public string keyRight = "d";

    public string keyA;
    public string keyB;
    public string keyC;
    public string keyD;

    [Header("==== Output Signals ====")]

    public bool jump;
    private bool lastJump;

    public float Dup;
    public float Dright;

    public float Dmag;
    public Vector3 Dvec;

    public bool run;

    private float targetDup;
    private float targetDright;
    private float velocityDup;
    private float velocityDright;

    public bool inputEnabled = true;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        targetDup = (Input.GetKey(keyUp) ? 1.0f : 0) - (Input.GetKey(keyDown) ? 1.0f : 0);
        targetDright = (Input.GetKey(keyRight) ? 1.0f : 0) - (Input.GetKey(keyLeft) ? 1.0f : 0);

        if(inputEnabled == false)
        {
            targetDup = 0;
            targetDright = 0;
        }

        Dup = Mathf.SmoothDamp(Dup, targetDup, ref velocityDup, 0.1f);
        Dright = Mathf.SmoothDamp(Dright, targetDright, ref velocityDright, 0.1f);

        Vector2 tempDAxis = SquareToCircle(new Vector2(Dright, Dup));
        float Dright2 = tempDAxis.x;
        float Dup2 = tempDAxis.y;

   

       
      
        Dmag = Mathf.Sqrt(Mathf.Pow(Dup2, 2) + Mathf.Pow(Dright2, 2));
        
        Dvec = Dright2 * transform.right + Dup2 * transform.forward;

        run = Input.GetKey(keyA);

        bool tempJump = Input.GetKey(keyB);
       
        if(tempJump != lastJump && tempJump == true)
        {
            jump = true;
        }
        else
        {
            jump = false;
        }

        lastJump = tempJump;


    }

    //https://arxiv.org/ftp/arxiv/papers/1509/1509.06344.pdf

    private Vector2 SquareToCircle(Vector2 input)
    {
        Vector2 output = Vector2.zero;
        output.x = input.x * Mathf.Sqrt(1 - (input.y * input.y) / 2.0f);
        output.y = input.y * Mathf.Sqrt(1 - (input.x * input.x) / 2.0f);
        return output;
    }
}
