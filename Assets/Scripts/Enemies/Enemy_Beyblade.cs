using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Beyblade : MonoBehaviour {

    public float movementSpeed;
    public float rotationSpeed;
    public Vector3 movementDirection;

    public Enemy stats;

	// Use this for initialization
	void Start ()
    {
        this.stats = this.GetComponent<Enemy>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        this.Rotate();
        this.Move();
	}

    void Rotate()
    {
        this.transform.Rotate(0, 0, rotationSpeed * this.stats.timeScale);
    }

    void Move()
    {
        this.transform.position += this.movementDirection * this.movementSpeed * Time.deltaTime * this.stats.timeScale;
    }
}
