using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSphereGizmo : MonoBehaviour
{
    [SerializeField] float radiusV;

    private void OnDrawGizmos()
    {
        //Gizmos.DrawIcon(transform.position, "StarIcon.png");        
        Gizmos.DrawIcon(transform.position, "star.png");
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, radiusV);

        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.lossyScale);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(0, -0.5f, -1.5f), new Vector3(0, -0.5f, 1.5f));
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(new Vector3(0, -0.5f, -1.5f), new Vector3(0, 1.5f, 0f));
        Gizmos.color = Color.green;
        Gizmos.DrawLine(new Vector3(0, -0.5f, 1.5f), new Vector3(0, 1.5f, 0f));


        //Red Box
        var collider = GetComponent<BoxCollider>();
        if (collider == null) return;

        var matrix = transform.localToWorldMatrix;
        Gizmos.matrix = matrix;
        Vector3 vector1;
        vector1.x = collider.size.x - 5f;
        vector1.y = collider.size.y - 5f;
        vector1.z = collider.size.z - 5f;

        Vector3 center = new Vector3(0f, 0f, 0f);

        Gizmos.color = Color.red;
        Gizmos.DrawCube(center, vector1);

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
