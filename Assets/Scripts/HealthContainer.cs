using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Constants;

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
        var scrollSpeed = GameObject.Find(GameObjectNames.Level).GetComponent<Level>().scrollSpeed;
        this.transform.position += Vector3.down * scrollSpeed * Time.deltaTime * GameObject.Find(GameObjectNames.TimeScale).GetComponent<TimeScale>().GlobalScale;
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
