using UnityEngine;
using UnityEngine.SceneManagement;
using Assets.Scripts.Constants;

public class Enemy_DeathDonut : MonoBehaviour {

    public Transform stopDistance;

    public float rotationSpeed;
    public float movementSpeed;

    public Enemy stats;
    
	// Use this for initialization
	void Start () {
        this.name = GameObjectNames.BossShip;
    }
	
	// Update is called once per frame
	void Update () {
        this.CheckDestruction();
        this.Rotate();

        if (this.CheckStopDistance())
        {
            this.Move();
        }
    }

    private void Move()
    {
        this.transform.position += Vector3.down * movementSpeed * Time.deltaTime * this.stats.timeScale;        
    }

    void Rotate()
    {        
        this.transform.Rotate(0, 0, rotationSpeed * this.stats.timeScale);
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