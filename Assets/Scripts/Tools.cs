using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tools {

    public static float MagnitudeVec3(Vector3 vector) {
        return Mathf.Sqrt(Mathf.Pow(vector.x, 2) + Mathf.Pow(vector.y, 2) + Mathf.Pow(vector.z, 2));
    }

    public static float GetDistanceToTarget(Transform origin, Transform target)
    {
        return Tools.MagnitudeVec3(target.position - origin.position);
    }
}
