using Assets.Scripts.Constants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    public TimeScale TimeScale;
    public Unit Unit;
    public bool Started;
    public bool OnCooldown;
    public float Power;
    public float Speed;
    public float RechargeTime;
    public float? Distance;
    public Vector3? Direction;

    private Vector3? StartPoint;

    // Start is called before the first frame update
    void Start()
    {
        this.LoadUnit();
    }

    private void LoadUnit()
    {
        this.Unit = this.GetComponent<Unit>();

        if (this.Unit == null)
        {
            Debug.LogError($"[Dash] - UNIT NOT FOUND");
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (this.Started && this.Direction.HasValue)
        {
            this.Move();
            this.CheckEnd();
        }
    }

    void CheckEnd()
    {
        if ((this.transform.position - this.StartPoint.Value).magnitude >= this.Distance)
        {
            this.EndDash();
        }
    }

    public void StartDash(Vector3 direction, float distance)
    {
        if (this.OnCooldown) { return; }

        this.Started = true;
        this.OnCooldown = true;
        this.Direction = direction;
        this.Distance = distance;
        this.StartPoint = this.transform.position;
        this.Speed = this.Unit.MovementSpeed + this.CalculateBoost();
    }

    float CalculateBoost()
    {
        return this.Unit.MovementSpeed * this.Power / 100;
    }

    void Move()
    {
        float timeScale = 1;

        if (this.TimeScale != null)
        {
            timeScale = this.TimeScale.GlobalScale;

            switch (LayerMask.LayerToName(this.gameObject.layer))
            {
                case GameObjectsLayers.Player:
                    timeScale = this.TimeScale.PlayerScale;
                    break;

                case GameObjectsLayers.Enemy:
                    timeScale = this.TimeScale.GlobalScale;
                    break;
            }
        }

        this.Unit.Move(this.Direction.Value, this.Speed * timeScale);
    }

    void EndDash()
    {
        this.Started = false;
        this.Direction = null;
        this.Distance = null;
        this.StartPoint = null;
        this.Speed = 0;
        Invoke(nameof(this.RemoveCooldown), this.RechargeTime);
    }

    void RemoveCooldown()
    {
        this.OnCooldown = false;
    }
}
