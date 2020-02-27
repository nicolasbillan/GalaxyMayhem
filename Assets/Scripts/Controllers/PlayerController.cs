using Assets.Scripts.Constants;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public UI UI;
    public TimeScale TimeScale;
    public Unit Unit;
    public Dash Dash;
    public Weapon Weapon;

    // Use this for initialization
    void Start()
    {
        this.name = GameObjectNames.PlayerShip;

        this.LoadUI();
        this.LoadTimeScale();
        this.LoadUnit();
        this.LoadDash();
        this.LoadWeapon();
    }

    void LoadUI()
    {
        /* Find the UI component in the Scene */
        this.UI = GameObject.Find(GameObjectNames.UI).GetComponent<UI>();

        if (this.UI == null)
        {
            Debug.LogWarning("[PlayerController] - UI NOT FOUND");
        }
    }

    private void LoadTimeScale()
    {
        /* Find the TimeScale component in the Scene */
        this.TimeScale = GameObject.Find(GameObjectNames.TimeScale).GetComponent<TimeScale>();

        if (this.TimeScale == null)
        {
            Debug.LogWarning("[PlayerController] - TIMESCALE NOT FOUND");
        }
    }

    private void LoadUnit()
    {
        /* Find the Unit component within myself */
        this.Unit = this.GetComponent<Unit>();

        if (this.Unit != null)
        {
            this.Unit.destroy = this.TriggerDeathSecuence;

            if (this.UI != null)
            {
                this.Unit.HitPoints = this.UI.HealthBar;
            }
        }
        else
        {
            Debug.LogWarning("[PlayerController] - UNIT NOT FOUND");
        }
    }

    private void LoadDash()
    {
        /* Find the Unit component within myself */
        this.Dash = this.GetComponent<Dash>();

        if (this.Dash == null)
        {
            Debug.LogWarning("[PlayerController] - BOOSTER NOT FOUND");
        }
    }

    private void LoadWeapon()
    {
        /* Find the Weapon component in my children */
        this.Weapon = this.GetComponentInChildren<Weapon>();

        if (this.Weapon != null)
        {
            this.Weapon.TimeScale = this.TimeScale;
        }
        else
        {
            Debug.LogWarning("[PlayerController] - WEAPON NOT FOUND");
        }
    }

    // Update is called once per frame
    void Update()
    {
        this.CheckDash();
        this.CheckMovement();
        this.CheckRotation();
        this.CheckFire();
        this.CheckSlowMotion();
    }

    private void CheckMovement()
    {
        if (this.Unit == null || this.Dash.Started) { return; }

        /* Get the sum of both axis */
        var vertical = Vector3.up * Input.GetAxis(InputAxesNames.Vertical);
        var horizontal = Vector3.right * Input.GetAxis(InputAxesNames.Horizontal);
        var result = vertical + horizontal;

        /* Move the Unit in desired direction */
        this.Unit.Move(result, this.TimeScale.PlayerScale);

        this.UI.SpecialBar.RectTransform.position = RectTransformUtility.WorldToScreenPoint(Camera.main, this.transform.position);
    }

    private void CheckRotation()
    {
        if (this.Unit == null || this.Dash.Started) { return; }

        /* Get the direction to the current position of the mouse */
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var direction = (new Vector3(mousePosition.x, mousePosition.y) - this.transform.position).normalized;

        /* Rotate the Unit to desired direction */
        this.Unit.Rotate(direction, this.TimeScale.PlayerScale);
    }

    private void CheckFire()
    {
        if (this.Weapon == null) { return; }

        /* Get the input from Weapon axis */
        if (Input.GetAxis(InputAxesNames.Weapon) == 1)
        {
            this.Weapon.Fire();
        }
    }

    private void CheckSlowMotion()
    {
        if (this.TimeScale == null) { return; }

        /* Get the input from SlowMotion axis */
        this.TimeScale.SlowMotion(Input.GetAxis("SlowMotion") == 1);
    }

    private void CheckDash()
    {
        if (Input.GetAxis(InputAxesNames.Dash) == 0) { return; }

        /* Get the bigger axis */
        var vertical = Vector3.up * Input.GetAxis(InputAxesNames.Vertical);
        var horizontal = Vector3.right * Input.GetAxis(InputAxesNames.Horizontal);
        Vector3 result;

        if (horizontal.magnitude >= vertical.magnitude)
        {
            result = horizontal;
        }
        else
        {
            result = vertical;
        }

        /* Dash in the desired direction */
        this.Dash.StartDash(result, 2);
    }

    void UpdateHealth()
    {
        this.Unit.Damage(1);
    }

    void BulletHit(GameObject bullet)
    {
        if (bullet.layer != this.gameObject.layer)
        {
            this.HitTaken();
        }
    }

    void EnemyHit()
    {
        this.HitTaken();
    }

    void ApplyPowerUp(GameObject powerUp)
    {
        //var container = powerUp.GetComponent<PowerUpContainer>();

        //var bulletPrefab = container.bulletPrefabs[container.activePrefab];

        //if (this.bulletPrefab.GetComponent<Bullet>().type == bulletPrefab.GetComponent<Bullet>().type)
        //{
        //    if (this.firePower < 3)
        //    {
        //        this.firePower++;
        //    }
        //}
        //else
        //{
        //    this.LoadBullet(bulletPrefab);
        //    this.ui.UpdateActiveBullet(container.bulletPrefabs[container.activePrefab]);
        //}

        //this.LoadPlaceHolders();
    }

    void ApplyHealPack()
    {
        //if (currentHealth < maxHealth)
        //{
        //    this.ui.UpdatePlayerHealth(this.currentHealth, true);

        //    this.currentHealth++;
        //}
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Bullet":
                this.BulletHit(collision.gameObject);
                break;

            case "Buff":
                this.ApplyPowerUp(collision.gameObject);
                break;

            case "HealPack":
                this.ApplyHealPack();
                break;

            case "Shield":
                this.EnemyHit();
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            this.EnemyHit();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            this.EnemyHit();
        }
    }

    private void HitTaken()
    {
        //if (!hitShield)
        //{
        //    this.currentHealth--;

        //    this.ui.UpdatePlayerHealth(this.currentHealth, false);

        //    if (this.currentHealth <= 0)
        //    {
        //        this.TriggerDeathSecuence();
        //    }
        //    else
        //    {
        //        this.hitShield = true;

        //        this.InvisibleOn();
        //        Invoke("ResetHitShield", hitShieldTime);
        //    }
        //}
    }

    private void TriggerDeathSecuence()
    {
        //SceneManager.LoadScene("GameOver");
        GameObject.Destroy(this.gameObject);
    }

    private void ResetHitShield()
    {
        //this.hitShield = false;
        //this.GetComponent<SpriteRenderer>().enabled = true;
    }

    private void InvisibleOn()
    {
        //if (this.hitShield)
        //{
        //    this.GetComponent<SpriteRenderer>().enabled = false;
        //    Invoke("InvisibleOff", 0.1f);
        //}
    }

    private void InvisibleOff()
    {
        //if (this.hitShield)
        //{
        //    this.GetComponent<SpriteRenderer>().enabled = true;
        //    Invoke("InvisibleOn", 0.1f);
        //}
    }
}
