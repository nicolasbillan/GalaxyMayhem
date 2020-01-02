using Assets.Scripts.Constants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EdgeCollider2D))]
public class OuterBounds : MonoBehaviour
{
    private EdgeCollider2D Collider;

    // Start is called before the first frame update
    void Start()
    {
        this.LoadCollider();
        this.tag = GameObjectTags.CameraBounds;
    }

    // Update is called once per frame
    void LoadCollider()
    {
        var verticalSize = Camera.main.orthographicSize;
        var horizontalSize = Screen.width * verticalSize / Screen.height;

        this.Collider = this.GetComponent<EdgeCollider2D>();

        var points = new Vector2[] {
            new Vector2(-horizontalSize, verticalSize),
            new Vector2(horizontalSize, verticalSize),
            new Vector2(horizontalSize, -verticalSize),
            new Vector2(-horizontalSize, -verticalSize),
            new Vector2(-horizontalSize, verticalSize)
        };

        Collider.points = points;
    }
}
