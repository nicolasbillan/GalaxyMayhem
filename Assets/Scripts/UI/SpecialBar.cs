using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecialBar : MonoBehaviour
{
    public RectTransform RectTransform => this.gameObject.GetComponent<RectTransform>();
    public Image Fill;
    public float DisplayTime;

    // Start is called before the first frame update
    void Start()
    {
        this.Hide();
    }

    public void UpdateFill(float fill)
    {
        this.gameObject.SetActive(true);
        this.Fill.fillAmount = fill;
        this.CancelInvoke();
        this.Invoke(nameof(this.Hide), this.DisplayTime);
    }

    private void Hide()
    {
        this.gameObject.SetActive(false);
    }
}
