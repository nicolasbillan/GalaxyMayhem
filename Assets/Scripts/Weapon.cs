using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public TimeScale TimeScale;
    public GameObject BulletPrefab;
    public List<PH> BulletOrigins;
    public float FireRate;
    public bool FireOnCooldown;

    // Start is called before the first frame update
    void Start()
    {
        this.LoadOrigins();
        this.FireOnCooldown = false;
    }

    void LoadOrigins()
    {
        this.BulletOrigins = this.GetComponentsInChildren<PH>().ToList();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Fire()
    {
        if (!this.FireOnCooldown)
        {
            foreach (var origin in this.BulletOrigins)
            {
                var bullet = GameObject.Instantiate(this.BulletPrefab);
                bullet.layer = origin.gameObject.layer;
                bullet.transform.position = origin.transform.position;
                bullet.transform.up = origin.transform.up;
                bullet.GetComponent<Bullet>().TimeScale = this.TimeScale;

                this.Invoke(nameof(this.OnCooldown), this.FireRate);
            }

            this.FireOnCooldown = true;
        }
    }

    private void OnCooldown()
    {
        this.FireOnCooldown = false;
    }
}
