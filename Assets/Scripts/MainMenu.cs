using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public GameObject sceneLoaderPrefab;
    public ScenesLoader scenesLoader;

	// Use this for initialization
	void Start () {
        this.LoadScenesLoader();
        Cursor.visible = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void LoadScenesLoader()
    {
        /* Just in case another ScenesLoader already exists */
        GameObject.Destroy(GameObject.Find("ScenesLoader"));

        var loader = GameObject.Instantiate(this.sceneLoaderPrefab);
        loader.name = "ScenesLoader";
        this.scenesLoader = loader.GetComponent<ScenesLoader>();
    }

    public void LoadScene(string sceneName)
    {
        this.scenesLoader.Load(sceneName);
    }       
    
    public void Exit()
    {
        Application.Quit();
    }
}
