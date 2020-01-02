using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PH : MonoBehaviour {

	[Range(0.1f, 1)]
	public float DrawSize;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(this.transform.position, this.DrawSize);
    }
}
