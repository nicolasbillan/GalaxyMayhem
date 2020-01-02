using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public float MovementSpeed;
    public float RotationSpeed;
    public float MaximumHitPoints;
    public float CurrentHitPoints;

    public void Move(Vector3 direction, float speed = 1)
    {
        this.transform.position += direction * this.MovementSpeed * speed * Time.deltaTime;
    }

    public void Rotate(Vector3 direction, float speed = 1)
    {
        this.transform.up = Vector3.Lerp(this.transform.up, direction, this.RotationSpeed * speed * Time.deltaTime);
    }
}
