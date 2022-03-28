using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Path
{
    [SerializeField, HideInInspector]
    List<Vector2> points;
    [SerializeField, HideInInspector]
    bool isClosed;

    public Path(Vector2 centre)
    {
        points = new List<Vector2>
        {
            centre+Vector2.left,
            centre+(Vector2.left+Vector2.up) /2f,
            centre + (Vector2.right+Vector2.down) /2f,
            centre + Vector2.right
        };
    }

    //Add point
    public void AddSegment(Vector2 anchorPos)
    {
        points.Add(points[points.Count - 1] * 2 - points[points.Count - 2]);
        points.Add((points[points.Count - 1] + anchorPos) /2f);
        points.Add(anchorPos);
    }

    public Vector2[] GetPointsInSegment(int i)
    {
        return new Vector2[] { points[i * 3], points[i * 3 + 1], points[i * 3 + 2], points[LoopIndex(i * 3 + 3)] };
    }

    public int NumberOfPoints
    {
        get
        {
            return points.Count;
        }
    }

    public int NumberOfSegments
    {
        get
        {
            return (points.Count / 3);
        }
    }

    public Vector2 this[int i]
    {
        get
        {
            return points[i];
        }
    }

    //Delete
    public void DeleteSegment(int anchorPointIndex)
    {
        if (NumberOfSegments > 2 || !isClosed && NumberOfSegments > 1)
        {
            if (anchorPointIndex == 0) //first point (i = 0) + closed path 
            {
                if (isClosed)
                {
                    points[points.Count - 1] = points[2];
                }
                points.RemoveRange(0, 3);
            }
            else if (anchorPointIndex == points.Count - 1 && !isClosed) //last point (i = -1) and open path
            {
                points.RemoveRange(anchorPointIndex - 2, 3);
            }
            else //default case i-1, i, i+1 - achor point and 2 control points
            {
                points.RemoveRange(anchorPointIndex - 1, 3);
            }
        }
    }

    //Move
    public void MovePoint(int i, Vector2 pointPosition)
    {
        Vector2 deltaMove = pointPosition - points[i];
        points[i] = pointPosition;
                if (i % 3 == 0)
                {
                    if (i + 1 < points.Count || isClosed)
                    {
                        points[LoopIndex(i + 1)] += deltaMove;
                    }
                    if (i - 1 >= 0 || isClosed)
                    {
                        points[LoopIndex(i - 1)] += deltaMove;
                    }
                }
                else
                {
                    bool nextPointIsAnchor;
                    if ((i + 1) % 3 == 0)
                    {
                        nextPointIsAnchor = true;
                    }
                    else
                    {
                        nextPointIsAnchor = false;
                    }

                    int correspondingControlIndex;
                    if (nextPointIsAnchor)
                    {
                        correspondingControlIndex = i + 2;
                    }
                    else
                    {
                        correspondingControlIndex = i - 2;
                    }

                    int anchorIndex;
                    if (nextPointIsAnchor)
                    {
                        anchorIndex = i + 1;
                    }
                    else
                    {
                        anchorIndex = i - 1;
                    }

                    if (correspondingControlIndex >= 0 && correspondingControlIndex < points.Count || isClosed)
                    {
                        float dst = (points[LoopIndex(anchorIndex)] - points[LoopIndex(correspondingControlIndex)]).magnitude;
                        Vector2 dir = (points[LoopIndex(anchorIndex)] - pointPosition).normalized;
                        points[LoopIndex(correspondingControlIndex)] = points[LoopIndex(anchorIndex)] + dir * dst;
                    }

        }
    }
    
    //Closed
    public bool GetIsClosed
    {
        get
        {
            return isClosed;
        }
        set
        {
            if (isClosed != value)
            {
                isClosed = value;

                if (isClosed)
                {
                    points.Add(points[points.Count - 1] * 2 - points[points.Count - 2]);
                    points.Add(points[0] * 2 - points[1]);
                }
                else
                {
                    points.RemoveRange(points.Count - 2, 2);
                }
            }
        }
    }

    int LoopIndex(int i) //loop all instances of the anchorIndex, as well as the correspondingControlIndex
    {
        return (i + points.Count) % points.Count;
    }
}