using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy_DeathDonut : MonoBehaviour {

    public Transform stopDistance;

    public float rotationSpeed;
    public float movementSpeed;

    public float timeScale;
    
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        this.CheckDestruction();
        this.CheckTimeScale();
        this.Rotate();

        if (this.CheckStopDistance())
        {
            this.Move();
        }
    }

    private void Move()
    {
        this.transform.position += Vector3.down * movementSpeed * Time.deltaTime * this.timeScale;        
    }

    void Rotate()
    {        
        this.transform.Rotate(0, 0, rotationSpeed * this.timeScale);
    }

    private void CheckTimeScale()
    {
        this.timeScale = GameObject.Find("TimeScale").GetComponent<TimeScale>().globalScale;
    }

    private bool CheckStopDistance()
    {
        var realDistance = this.transform.position.y - stopDistance.position.y;

        if (realDistance <= 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }      

    void CheckDestruction()
    {
        var gameObject = this.transform.FindChild("DeathDonut");

        if(gameObject == null)
        {
            SceneManager.LoadScene("Credits");
            GameObject.Destroy(this.gameObject);
        }
    }
}