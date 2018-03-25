using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Assets.Scripts.Models;
using Assets.Scripts.Constants;

public class Highscores : MonoBehaviour {
    
    private TextAsset highscores;
    private Score[] scores;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void LoadHighscores()
    {
        var path = "Assets/highscores.txt";

        StreamReader reader = new StreamReader(path);

        var content = reader.ReadToEnd();

        if(!string.IsNullOrEmpty(content))
        {
            foreach (var score in content.Split('|'))
            {
                if(!string.IsNullOrEmpty(score))
                {

                }
            }
        }

    }

    void ReturnToMainMenu()
    {
        GameObject.Find(GameObjectNames.ScenesLoader).GetComponent<ScenesLoader>().Load(SceneNames.MainMenu);
    }
}
