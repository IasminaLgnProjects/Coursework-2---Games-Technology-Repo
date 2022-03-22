using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFOV : MonoBehaviour
{
    [SerializeField] float fov;
    public Color myColor;

    public float GetFov()
    {
        return fov;
    }

    public Color GetColor()
    {
        return myColor;
    }
}
