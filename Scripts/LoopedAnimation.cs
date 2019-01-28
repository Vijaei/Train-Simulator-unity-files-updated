using UnityEngine;
using Pixelplacement;

public class LoopedAnimation : MonoBehaviour
{
    public Spline mySpline;
    public Transform myObject;

    void Awake()
    {
        Tween.Spline(mySpline, myObject, 0, 1, true, 10, 0, Tween.EaseInOut, Tween.LoopType.PingPong);
    }
}