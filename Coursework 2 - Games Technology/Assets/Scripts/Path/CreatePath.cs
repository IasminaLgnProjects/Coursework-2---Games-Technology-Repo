using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatePath : MonoBehaviour
{
    [HideInInspector]
    public Path path;

    public Color pathColor = Color.yellow;

    public Color anchorPointColor = Color.red;
    public float anchorPointDiameter = 0.1f;

    public Color controlPointColor = Color.cyan;
    public float controlPointDiameter = .070f;

    public bool displayControlPoints = true;

    public void GeneratePath()
    {
        path = new Path(transform.position);
    }
}