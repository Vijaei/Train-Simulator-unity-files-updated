using System.Collections;
using System.Collections.Generic;

using Pixelplacement;
using UnityEngine;

public class splineUser : MonoBehaviour
{
    public Transform target;
    public Spline spline;
    [Range(0, 1)] public float percentage;

    void Update()
    {
        target.position = spline.GetPosition(percentage);
    }
}