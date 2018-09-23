using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recipe : MonoBehaviour {

    public Sprite closed;
    public Sprite opened;

    bool button;

	void Start () {
        button = false;
	}
	
	void Update () {
        
	}

    private void OnMouseDrag()
    {
        Vector2 mousePos;
        mousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 objPos = Camera.current.ScreenToWorldPoint(mousePos);
        transform.position = objPos;
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.name == "RecipeOpen")
        {
            button = !button;
            if (button)
            {
                GetComponent<SpriteRenderer>().sprite = opened;
                //box.size = new Vector2(4.5f, 6.1f);
            }
            else
            {
                GetComponent<SpriteRenderer>().sprite = closed;
                //box.size = new Vector2(1.2f, 1.6f);
            }
        }
    }
}
