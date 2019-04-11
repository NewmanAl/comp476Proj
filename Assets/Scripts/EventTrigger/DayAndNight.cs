using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayAndNight : MonoBehaviour
{
    public Transform sun;
    public int rotationScale;

    // Start is called before the first frame update
    void Start()
    {
        rotationScale = 4;
    }

    // Update is called once per frame
    void Update()
    {
        sun.Rotate(rotationScale * Time.deltaTime, 0, 0);
    }
}
