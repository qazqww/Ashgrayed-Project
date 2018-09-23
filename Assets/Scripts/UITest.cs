using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UITest : MonoBehaviour
         , IPointerClickHandler
         , IDragHandler
         , IPointerEnterHandler
         , IPointerExitHandler
{
    SpriteRenderer sprite;
    Color target = Color.red;

    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (sprite)
            sprite.color = Vector4.MoveTowards(sprite.color, target, Time.deltaTime * 10);
    }

    public void OnPointerClick(PointerEventData eventData) // 3
    {
        print("I was clicked");
        target = Color.blue;
    }

    public void OnDrag(PointerEventData eventData)
    {
        print("I'm being dragged!");
        target = Color.magenta;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        print("Hi!");
        target = Color.green;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        print("Bye!");
        target = Color.red;
    }
}
