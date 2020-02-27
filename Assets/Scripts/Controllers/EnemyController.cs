using Assets.Scripts.Controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Pathway Pathway;
    private PathwayFollower PathwayFollower;

    public Unit Unit;

    [ContextMenu("Toggle Follow")]
    public void ToggleFollow()
    {
        if (this.PathwayFollower != null)
        {
            this.Pathway.Unfollow(this.PathwayFollower);
            this.PathwayFollower = null;
        }
        else
        {
            this.PathwayFollower = this.Pathway.Follow(this.transform);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        this.Unit = this.GetComponent<Unit>();
        this.Pathway = this.GetComponent<Pathway>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.PathwayFollower == null) { return; }

        this.Unit.Move((this.PathwayFollower.Current.transform.position - this.transform.position));
    }
}
