using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Constants;

public class Main : MonoBehaviour
{
    public UI ui;

    public GameObject backgroundPrefab;
    public GameObject[] playerPrefabs;
    public GameObject powerUpContainerPrefab;
    public GameObject healPackPrefab;
    public GameObject uiPrefab;
    public GameObject timeScalePrefab;
    public GameObject pauseMenuPrefab;

    public int killCount;
    public int dropRate;
    public int healthDropRate;
    private int dropLastCount;
    private int healthDropLastCount;

    public float horizontalSize;
    public float verticalSize;

    // Use this for initialization
    void Start()
    {
        /*remove*/
        //GameObject.Find("ScenesLoader").GetComponent<ScenesLoader>().SetParameter(ParametersKeys.PlayerShip, "0");
        this.name = GameObjectNames.MainCamera;

        this.LoadUI();
        this.LoadTimeScale();
        this.LoadPlayer();
        this.LoadPauseMenu();
        this.LoadCameraSize();
        this.LoadBackground();
    }

    // Update is called once per frame
    void Update()
    {
        if (!this.ui.pauseMenu.enabled)
        {
            this.CheckPowerUpDropRate();
            this.CheckHealthDropRate();
        }
    }
    
    private void LoadTimeScale()
    {
        GameObject.Instantiate(this.timeScalePrefab);
    }

    private void LoadCameraSize()
    {
        this.verticalSize = Camera.main.orthographicSize;
        this.horizontalSize = Screen.width * this.verticalSize / Screen.height;
    }

    private void LoadUI()
    {
        var ui = GameObject.Instantiate(this.uiPrefab);
        this.ui = ui.GetComponent<UI>();
    }

    private void LoadPauseMenu()
    {
        var pauseMenu = GameObject.Instantiate(this.pauseMenuPrefab);
        pauseMenu.name = GameObjectNames.PauseMenu;
        this.ui.pauseMenu = pauseMenu.GetComponent<Canvas>();
        this.ui.pauseMenu.enabled = false;

        for (int i = 0; i < this.ui.pauseMenu.transform.childCount; i++)
        {
            var name = this.ui.pauseMenu.transform.GetChild(i).name;
            var button = this.ui.pauseMenu.transform.GetChild(i).gameObject.GetComponent<Button>();

            switch (name)
            {
                case "ContinueButton":
                    button.onClick.AddListener(this.ui.TriggerPauseMenu);
                    break;

                case "ExitButton":
                    button.onClick.AddListener(this.ui.ExitGame);
                    break;
            }
        }
    }

    private void LoadPlayer()
    {
        var playerShip = int.Parse(GameObject.Find(GameObjectNames.ScenesLoader).GetComponent<ScenesLoader>().GetParameter(ParametersKeys.PlayerShip));

        var player = GameObject.Instantiate(this.playerPrefabs[playerShip]);

        player.transform.position = new Vector3(0, -3, 0);
    }

    private void LoadBackground()
    {
        GameObject.Instantiate(this.backgroundPrefab);
    }

    private void CheckPowerUpDropRate()
    {
        if (killCount != 0 && killCount % dropRate == 0 && dropLastCount != killCount)
        {
            this.SpawnPowerUpContainer();
            dropLastCount = killCount;
        }
    }

    private void CheckHealthDropRate()
    {
        if (killCount != 0 && killCount % healthDropRate == 0 && healthDropLastCount != killCount)
        {
            this.SpawnHealPack();
            healthDropLastCount = killCount;
        }
    }

    private void SpawnPowerUpContainer()
    {
        var container = GameObject.Instantiate(this.powerUpContainerPrefab);

        container.transform.position = new Vector3(Random.Range(-this.horizontalSize, this.horizontalSize), Random.Range(this.verticalSize, this.verticalSize + 5), 0);
    }

    private void SpawnHealPack()
    {
        var container = GameObject.Instantiate(this.healPackPrefab);

        container.transform.position = new Vector3(Random.Range(-this.horizontalSize, this.horizontalSize), Random.Range(this.verticalSize, this.verticalSize + 5), 0);
    }
}
