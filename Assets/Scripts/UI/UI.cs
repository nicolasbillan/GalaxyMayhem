using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Assets.Scripts.Constants;

public class UI : MonoBehaviour {
    public Text KillCount;
    public Text Condition;

    public HealthBar HealthBar;

    public Image SlowMotionIndicator;
    public Image activeBullet;
    public Image[] hitPoints;
    public Image bossHealthBarEmpty;
    public Image bossHealthBarFull;
    public Image target;

    public Canvas pauseMenu;
    private ScenesLoader scenesLoader;

    // Use this for initialization
    void Start () {
        this.name = GameObjectNames.UI;
        //this.scenesLoader = GameObject.Find(GameObjectNames.ScenesLoader).GetComponent<ScenesLoader>();
        this.LoadTarget();
        Invoke("RemoveConditionText", 2);
	}
	
	// Update is called once per frame
	void Update () {
        this.CheckPause();
        this.MoveTarget();
        this.UpdateKillCount();
    }    

    void LoadTarget()
    {
        Cursor.visible = false;
        //this.target.transform.SetParent(GameObject.Find("UI").transform);
        //this.target.rectTransform.localScale = new Vector3(1, 1, 1);
    }

    void MoveTarget()
    {
        this.target.rectTransform.position = Input.mousePosition;
    }

    void CheckPause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            this.TriggerPauseMenu();
        }
    }

    public void TriggerPauseMenu()
    {
        this.pauseMenu.enabled = !this.pauseMenu.enabled;
        
        GameObject.Find(GameObjectNames.TimeScale).GetComponent<TimeScale>().SetPauseStatus(this.pauseMenu.enabled);        

        Cursor.visible = this.pauseMenu.enabled;

        this.target.enabled = !this.pauseMenu.enabled;
    }

    public void ExitGame()
    {
        this.scenesLoader.Load("MainMenu");
    }

    public void UpdateKillCount()
    {
        //this.KillCount.text = "KILLS: " + GameObject.Find(GameObjectNames.MainCamera).GetComponent<Main>().killCount;
    }

    public void DisplayBossHealth()
    {
        this.bossHealthBarEmpty.enabled = true;
        this.bossHealthBarFull.enabled = true;
    }

    public void UpdateBossHealth(float current, float max)
    {
        this.bossHealthBarFull.fillAmount = current / max;
    }

    public void UpdatePlayerHealth(int currentHealth, bool heal)
    {
        this.hitPoints[currentHealth].enabled = heal;
    }

    public void UpdateActiveBullet(GameObject bullet)
    {
        this.activeBullet.sprite = bullet.GetComponent<SpriteRenderer>().sprite;
    }

    public void DisplayConditionText(int objective)
    {
        this.Condition.text = "KILL " + objective + " ENEMIES";
    }

    private void RemoveConditionText()
    {
        this.Condition.enabled = false;
    }
}
