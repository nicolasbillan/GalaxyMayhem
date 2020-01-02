using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Constants;

public class Level : MonoBehaviour {

    public SpriteRenderer background1;
    public SpriteRenderer background2;
    public float scrollSpeed;

    //private float horizontalSize;
    private float verticalSize;

    private TimeScale timeScale;

    // Use this for initialization
    void Start () {
        this.verticalSize = Camera.main.orthographicSize;
        //this.horizontalSize = Screen.width * this.verticalSize / Screen.height; 
        this.name = GameObjectNames.Level;
        this.LoadTimeScale();
	}
	
	// Update is called once per frame
	void Update () {
        this.Scroll();
        this.CheckLoop();
    }

    void LoadTimeScale()
    {
        this.timeScale = GameObject.Find(GameObjectNames.TimeScale).GetComponent<TimeScale>();
    }     

    void Scroll()
    {
        this.background1.transform.position += Vector3.down * scrollSpeed * Time.deltaTime * this.timeScale.GlobalScale;
        this.background2.transform.position += Vector3.down * scrollSpeed * Time.deltaTime * this.timeScale.GlobalScale;
    }

    void CheckLoop()
    {
        if (this.background1.transform.position.y + this.background1.bounds.size.y / 2 < -this.verticalSize)
        {
            this.background1.transform.position = this.background2.transform.position + Vector3.up * this.background2.bounds.size.y;
        }
        if (this.background2.transform.position.y + this.background2.bounds.size.y / 2 < -this.verticalSize)
        {
            this.background2.transform.position = this.background1.transform.position + Vector3.up * this.background1.bounds.size.y;
        }
    }
}
