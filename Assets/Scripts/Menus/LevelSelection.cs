using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Constants;

public class LevelSelection : MonoBehaviour {

	// Use this for initialization
	public void LevelSelected(string levelName)
    {
        GameObject.Find(GameObjectNames.ScenesLoader).GetComponent<ScenesLoader>().Load(levelName);
    }
}
