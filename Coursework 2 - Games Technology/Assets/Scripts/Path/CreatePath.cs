using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatePath : MonoBehaviour
{
    [HideInInspector]
    public Path path;

    public Color anchorPointColor = Color.red;
    public Color controlPointColor = Color.cyan;
    public Color pathColor = Color.yellow;

    public float anchorPointDiameter = 0.1f;
    public float controlPointDiameter = .070f;

    public bool displayControlPoints = true;

    public void GeneratePath()
    {
        path = new Path(transform.position);
    }
}