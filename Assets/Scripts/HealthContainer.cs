using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthContainer : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        this.MoveWithLevel();
	}

    private void MoveWithLevel()
    {
        var scrollSpeed = GameObject.Find("Level").GetComponent<Level>().scrollSpeed;
        this.transform.position += Vector3.down * scrollSpeed * Time.deltaTime * GameObject.Find("TimeScale").GetComponent<TimeScale>().globalScale;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == this.gameObject.layer && collision.gameObject.tag == "Player")
        {
            GameObject.Destroy(this.gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        GameObject.Destroy(this.gameObject);
    }
}
