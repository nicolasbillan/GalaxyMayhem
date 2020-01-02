using Assets.Scripts.Constants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
public class BackgroundContainer : MonoBehaviour
{
    public SpriteRenderer Sprite;
    public BackgroundContainer PreviousContainer;
    //public bool shaking;
    public int index;

    private BoxCollider2D Collider;
    private bool OutOfBounds = false;

    public float BoundsY
    {
        get
        {
            if (this.Sprite != null)
            {
                return this.Sprite.bounds.size.y;
            }

            return 0;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        this.LoadCollider();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void LoadCollider()
    {
        this.Collider = this.GetComponent<BoxCollider2D>();
        this.Collider.size = new Vector2(this.Sprite.bounds.size.x, this.Sprite.bounds.size.y) / this.transform.lossyScale;
    }

    public void Scroll(float speed)
    {
        this.transform.position += Vector3.down * speed * Time.deltaTime;

        Debug.Log("moving: " + this.index);
    }

    public void Stack()
    {
        /* Place Container at previous Container location and Move it up half the sum of both Y bounds */
        this.transform.position = this.PreviousContainer.transform.position + (Vector3.up * ((this.BoundsY + this.PreviousContainer.BoundsY) / 2));
    }

    private void LateUpdate()
    {
        if (this.OutOfBounds)
        {
            Debug.Log("out of bounds");

            this.Stack();
            this.OutOfBounds = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("EXIT");

        if (collision.gameObject.CompareTag(GameObjectTags.MainCamera))
        {
            this.OutOfBounds = true;
        }
    }
}
