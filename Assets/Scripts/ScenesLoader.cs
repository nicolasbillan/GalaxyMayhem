using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesLoader : MonoBehaviour {

    private Dictionary<string, string> parameters;

    public void Load(string name)
    {
        GameObject.DontDestroyOnLoad(this.gameObject);
        SceneManager.LoadScene(name);
    }

    public void Load(string name, Dictionary<string, string> parameters = null)
    {
        this.parameters = parameters;
        this.Load(name);
    }

    public void Load(string name, string key, string value)
    {
        this.parameters = new Dictionary<string, string>();
        this.parameters.Add(key, value);        
        this.Load(name);
    }

    public Dictionary<string, string> GetParameters()
    {
        return parameters;
    }

    public string GetParameter(string key)
    {
        if (parameters == null)
        {
            return "";
        }

        return parameters[key];
    }

    public void SetParameter(string key, string value)
    {
        if (parameters == null)
        {
            this.parameters = new Dictionary<string, string>();
        }

        this.parameters.Add(key, value);
    }
}
