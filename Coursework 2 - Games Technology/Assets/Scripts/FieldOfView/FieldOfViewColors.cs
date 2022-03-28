using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfViewColors : MonoBehaviour
{
    public Color fovCircleColor = Color.yellow;
    public Color angleColor = Color.red;
    public Color playerSeenColor = Color.green;

    public Color GetColor
    {
        get
        {
            return fovCircleColor;
        }
    }
}
