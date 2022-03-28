using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfViewOLD : MonoBehaviour
{
    public float radius;
    [Range(0, 180)]
    public float angle;

    public GameObject playerRef;

    public LayerMask targetMask;
    public LayerMask obstructionMask;

    public bool canSeePlayer;

    //[SerializeField] FieldOfViewColors ColorScript;
    //[SerializeField] Colors ColorScript;
    //[SerializeField] Colors ColorScript = new Colors();
    
    public Color fovCircleColor = Color.yellow;
    public Color fovAngleColor = Color.red;
    public Color playerSeenColor = Color.green; //maybe without it

    private void Start()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(FOVRoutine());
        //ColorScript = gameObject.GetComponent<FieldOfViewColors>();
    }

    /*
    public Color GetColor
    {
        get
        {
            return ColorScript.fovCircleColor;
        }
    }*/

    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                    canSeePlayer = true;
                else
                    canSeePlayer = false;
            }
            else
                canSeePlayer = false;
        }
        else if (canSeePlayer)
            canSeePlayer = false;
    }
}

/*
public class Colors : MonoBehaviour
{
    [SerializeField] public Color fovCircleColor = Color.red;
}*/