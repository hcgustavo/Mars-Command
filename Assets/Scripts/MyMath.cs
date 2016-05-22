using UnityEngine;
using System.Collections;

public class MyMath {

    public static float Vector3Norm(Vector3 v)
    {
        return Mathf.Sqrt(Mathf.Pow(v.x, 2) + Mathf.Pow(v.y, 2) + Mathf.Pow(v.z, 2));
    }
}
