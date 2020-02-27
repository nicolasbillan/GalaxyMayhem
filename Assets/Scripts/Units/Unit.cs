using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public float MovementSpeed;
    public float RotationSpeed;
    public float MaximumHitPoints;
    public float CurrentHitPoints;
    public HealthBar HitPoints;

    public delegate void DestroyEvent();
    public DestroyEvent destroy;

    private void Start()
    {
        this.CurrentHitPoints = this.MaximumHitPoints;
    }

    public void Move(Vector3 direction, float speed = 1)
    {
        this.transform.position += direction.normalized * this.MovementSpeed * speed * Time.deltaTime;
        this.MoveHitPoints();
    }

    private void MoveHitPoints()
    {
        if (this.HitPoints != null)
        {
            this.HitPoints.RectTransform.position = RectTransformUtility.WorldToScreenPoint(Camera.main, this.transform.position);
        }
    }

    public void Rotate(Vector3 direction, float speed = 1)
    {
        this.transform.up = Vector3.Lerp(this.transform.up, direction, this.RotationSpeed * speed * Time.deltaTime);
    }

    public void Damage(float amount)
    {
        this.CurrentHitPoints -= amount;
        this.TakeDamage();
    }

    /* virtual modifier lets you override this method from a children class  */
    public virtual void TakeDamage()
    {
        if (this.HitPoints != null)
        {
            this.HitPoints.UpdateFill(this.CurrentHitPoints / this.MaximumHitPoints);
        }

        if (this.CurrentHitPoints <= 0)
        {
            this.destroy();
        }
    }
}
