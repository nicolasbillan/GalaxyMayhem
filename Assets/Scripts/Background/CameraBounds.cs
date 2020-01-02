using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Constants;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class CameraBounds : MonoBehaviour
{
    private BoxCollider2D Collider;

    // Start is called before the first frame update
    void Start()
    {
        this.LoadCollider();
        this.tag = GameObjectTags.MainCamera;
    }

    // Update is called once per frame
    void LoadCollider()
    {
        var verticalSize = Camera.main.orthographicSize;
        var horizontalSize = Screen.width * verticalSize / Screen.height;

        this.Collider = this.GetComponent<BoxCollider2D>();
        this.Collider.size = new Vector2(horizontalSize, verticalSize) * 2;
    }
}

