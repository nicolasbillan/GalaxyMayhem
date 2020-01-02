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
    public Weapon Weapon;

    // Use this for initialization
    void Start()
    {
        this.name = GameObjectNames.PlayerShip;

        this.LoadUI();
        this.LoadTimeScale();
        this.LoadUnit();
        this.LoadWeapon();
    }

    void LoadUI()
    {
        this.UI = GameObject.Find(GameObjectNames.UI).GetComponent<UI>();
    }

    private void LoadTimeScale()
    {
        /* Find the TimeScale component in the Scene */
        this.TimeScale = GameObject.Find(GameObjectNames.TimeScale).GetComponent<TimeScale>();
    }

    private void LoadUnit()
    {
        /* Find the Unit component within myself */
        this.Unit = this.GetComponent<Unit>();
    }

    private void LoadWeapon()
    {
        /* Find the Weapon component in my children */
        this.Weapon = this.GetComponentInChildren<Weapon>();
        this.Weapon.TimeScale = this.TimeScale;
    }

    // Update is called once per frame
    void Update()
    {
        this.CheckMovement();
        this.CheckRotation();
        this.CheckFire();
        this.CheckSlowMotion();
    }

    private void CheckMovement()
    {
        if (this.Unit == null) { return; }

        /* Get the sum of both axis */
        var vertical = Vector3.up * Input.GetAxis(InputAxesNames.Vertical);
        var horizontal = Vector3.right * Input.GetAxis(InputAxesNames.Horizontal);
        var result = vertical + horizontal;

        /* Move the Unit in desired direction */
        this.Unit.Move(result, this.TimeScale.PlayerScale);

        this.UI.SlowMotionIndicator.rectTransform.position = RectTransformUtility.WorldToScreenPoint(Camera.main, this.transform.position);
    }

    private void CheckRotation()
    {
        if (this.Unit == null) { return; }

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
        //GameObject.Destroy(this.gameObject);
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
