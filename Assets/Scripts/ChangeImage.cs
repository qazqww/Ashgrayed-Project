using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeImage : MonoBehaviour {

    public Sprite otherImage;

    public void changeSprite(Sprite other)
    {
        otherImage = other;
        gameObject.GetComponent<SpriteRenderer>().sprite = otherImage;
    }
}
