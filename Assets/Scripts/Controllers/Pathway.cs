using Assets.Scripts.Controllers;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pathway : MonoBehaviour
{
    public List<Waypoint> Waypoints;
    public GameObject FollowerPrefab;
    public float FollowerTolerance;
    public List<PathwayFollower> Followers;

    private void Awake()
    {
        this.Followers = new List<PathwayFollower>();
        this.Refresh();
    }

    [ContextMenu("Refresh Waypoints")]
    public void Refresh()
    {
        this.Waypoints = this.GetComponentsInChildren<Waypoint>().ToList();

        for (int i = 0; i < this.Waypoints.Count; i++)
        {
            if (i == 0)
            {
                this.Waypoints[i].PreviousWaypoint = this.Waypoints[this.Waypoints.Count - 1];
            }
            else
            {
                this.Waypoints[i].PreviousWaypoint = this.Waypoints[i - 1];
            }

            if (i == (this.Waypoints.Count - 1))
            {
                this.Waypoints[i].NextWaypoint = this.Waypoints[0];
            }
            else
            {
                this.Waypoints[i].NextWaypoint = this.Waypoints[i + 1];
            }
        }
    }

    public Waypoint ClosestWaypoint(Vector3 origin)
    {
        /* Orderno los waypoints por su distancia a origin y me quedo con el mas chico */
        return this.Waypoints.OrderBy(w => Vector3.Distance(w.transform.position, origin)).FirstOrDefault();
    }

    public PathwayFollower Follow(Transform follower)
    {
        var pathwayFollower = GameObject.Instantiate(this.FollowerPrefab).GetComponent<PathwayFollower>();
        this.Followers.Add(pathwayFollower);
        pathwayFollower.Follower = follower;
        pathwayFollower.Current = this.ClosestWaypoint(follower.position);
        return pathwayFollower;
    }

    public void Unfollow(PathwayFollower follower)
    {
        this.Followers.Remove(follower);
        GameObject.Destroy(follower);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (this.Followers.Any())
        {
            //Debug.Log("distance: " + Vector3.Distance(this.Follower.position, this.Current.NextWaypoint.transform.position));

            foreach (var follower in this.Followers)
            {
                /* Si la distancia entre el follower y su waypoint actual es menor igual que la tolerancia, cambio de waypoint al siguiente */
                if (Vector3.Distance(follower.Follower.position, follower.Current.transform.position) <= this.FollowerTolerance)
                {
                    follower.Current = follower.Current.NextWaypoint;
                }
            }
        }
    }
}
