using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PH))]
public class Waypoint : MonoBehaviour
{
    public Waypoint NextWaypoint;
    public Waypoint PreviousWaypoint;

    private void OnDrawGizmos()
    {
        if (this.NextWaypoint != null)
        {
            Gizmos.DrawLine(this.transform.position, this.NextWaypoint.transform.position);
        }
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
