using Assets.Scripts.Constants;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    public TimeScale TimeScale;
    public BackgroundContainer[] Containers;

    [Range(0, 5)]
    public float ScrollSpeed;

    // Start is called before the first frame update
    void Start()
    {
        this.name = GameObjectNames.Background;
        this.LoadTimeScale();
        this.LoadContainers();
    }

    void LoadTimeScale()
    {
        this.TimeScale = GameObject.Find(GameObjectNames.TimeScale).GetComponent<TimeScale>();
    }

    void LoadContainers()
    {
        this.Containers = this.transform.GetComponentsInChildren<BackgroundContainer>();

        for (var i = 0; i < this.Containers.Length; i++)
        {
            this.Containers[i].index = i;

            /* Set previous container */
            this.Containers[i].PreviousContainer = this.Containers[(i == 0 ? this.Containers.Length : i) - 1];

            if (i == 0)
            {
                /* First Container is set to origin */
                this.Containers[i].transform.localPosition = Vector2.zero;
            }
            else
            {
                /* Stack all Containers */
                this.Containers[i].Stack();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Backspace))
        {
            this.Scroll();
        }
    }

    void Scroll()
    {
        if (this.TimeScale == null) { return; }

        for (var i = 0; i < this.Containers.Length; i++)
        {
            this.Containers[i].Scroll(this.ScrollSpeed * this.TimeScale.GlobalScale);
        }
    }
}
