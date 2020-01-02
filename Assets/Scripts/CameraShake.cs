using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public bool shaking;
    public float shakeSpeed;
    public float shakeDistance;
    //public Vector3 shakeDirection;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (this.shaking)
        {
            //Debug.Log();

            this.transform.position += Vector3.right * Mathf.Sin(Time.time / shakeSpeed) * Time.deltaTime;
        }
        else
        {
            this.transform.position = Vector3.zero;
        }
    }
}
