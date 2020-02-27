using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Constants;
using System;

public class TimeScale : MonoBehaviour
{
    public float MainScale = 1;
    public float PlayerScale = 1;
    public float GlobalScale = 1;

    public SpecialBar SlowMotionFill;
    public float SlowMotionMaxTime;
    public float SlowMotionMinTime;
    public float SlowMotionCurrentTime;
    public float SlowMotionPower;
    public bool SlowMotionActive;

    private float SlowMotionFillAmount => this.SlowMotionCurrentTime / this.SlowMotionMaxTime;

    // Use this for initialization
    void Start()
    {
        this.name = GameObjectNames.TimeScale;
        this.LoadSpecialBar();
        this.LoadSlowMotion();
    }

    private void LoadSpecialBar()
    {
        this.SlowMotionFill = GameObject.Find(GameObjectNames.UI).GetComponent<UI>().SpecialBar;
    }

    private void LoadSlowMotion()
    {
        this.SlowMotionCurrentTime = this.SlowMotionMaxTime;
        this.SlowMotionActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.PlayerScale <= 0) { return; }

        if (this.SlowMotionActive)
        {
            /* Consume available time */
            this.Consume();

            /* Change global scale */
            this.GlobalScale = this.SlowMotionPower;
        }
        else
        {
            /* Recharge consumed time */
            this.Recharge();

            /* Reset global scale */
            this.GlobalScale = this.MainScale;
        }

        if (this.SlowMotionFill == null || this.SlowMotionFillAmount >= 1) { return; }

        /* Update slow motion indicator */
        this.SlowMotionFill.UpdateFill(this.SlowMotionFillAmount);
    }

    private void Recharge()
    {
        /* Check if you have time to restore */
        if (this.SlowMotionCurrentTime < this.SlowMotionMaxTime)
        {
            /* Restore current time */
            this.SlowMotionCurrentTime += Time.deltaTime;
        }
        else
        {
            if (this.SlowMotionFill == null) { return; }

            /* Once fully charged, hide indicator */
            this.SlowMotionFill.enabled = false;
        }
    }

    private void Consume()
    {
        /* Consume current time */
        this.SlowMotionCurrentTime -= Time.deltaTime;

        if (this.SlowMotionFill == null) { return; }

        /* Display current time */
        this.SlowMotionFill.enabled = true;
    }

    public void SlowMotion(bool active)
    {
        /* Check if you have enough time */
        if (active && this.SlowMotionCurrentTime >= this.SlowMotionMinTime)
        {
            /* The first time it is used, consume minimum time */
            if (!this.SlowMotionActive)
            {
                this.SlowMotionCurrentTime -= this.SlowMotionMinTime;
            }

            /* Turn slow motion on */
            this.SlowMotionActive = true;
        }
        else
        {
            /* Turn slow motion off */
            this.SlowMotionActive = false;
        }
    }

    public void SetPauseStatus(bool paused)
    {
        this.PlayerScale = paused ? 0 : 1;
        this.GlobalScale = paused ? 0 : 1;
    }
}
