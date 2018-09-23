using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frypan : MonoBehaviour {

    public PlayerInCook cook;
    public GameObject newPlate;

    public Sprite frypan_normal;
    public Sprite frypan_tomato;
    public Sprite frypan_onion;
    public Sprite frypan_lettuce;
    public Sprite frypan_bread;
    public Sprite frypan_meat;

    private string cookingFood;
    private bool onPlate;

    Plate plate;
    Vector2 curPos;

    private void Start()
    {
        onPlate = false;
        curPos = transform.position;
        Instantiate(newPlate, new Vector2(transform.position.x+1.66f, transform.position.y), Quaternion.identity);
    }

    public void changeSprite(string food)
    {
        switch(food)
        {
            case "tomato":
                gameObject.GetComponent<SpriteRenderer>().sprite = frypan_tomato;
                cookingFood = "tomato";
                break;
            case "onion":
                gameObject.GetComponent<SpriteRenderer>().sprite = frypan_onion;
                cookingFood = "onion";
                break;
            case "lettuce":
                gameObject.GetComponent<SpriteRenderer>().sprite = frypan_lettuce;
                cookingFood = "lettuce";
                break;
            case "meat":
                gameObject.GetComponent<SpriteRenderer>().sprite = frypan_meat;
                cookingFood = "meat";
                break;
            case "bread":
                gameObject.GetComponent<SpriteRenderer>().sprite = frypan_bread;
                cookingFood = "bread";
                break;
        }
    }

    private void OnMouseDrag()
    {
        if (cook.isFinishing)
        {
            Vector2 mousePos;
            mousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            Vector2 objPos = Camera.current.ScreenToWorldPoint(mousePos);
            transform.position = objPos;
        }
    }

    private void OnMouseUp()
    {
        if(onPlate)
        {
            if (cook.gaugeBar.fillAmount >= 0.63f && cook.gaugeBar.fillAmount <= 0.78f) // 성공적인 요리
                plate.grade++;

            if (cookingFood == "tomato")
                plate.tomatoSoup = true;
            else if (cookingFood == "onion")
                plate.onionSoup = true;
            else if (cookingFood == "lettuce")
                plate.salad = true;
            else if (cookingFood == "meat")
                plate.steak = true;
            else if (cookingFood == "bread")
                plate.bread = true;

            gameObject.GetComponent<SpriteRenderer>().sprite = frypan_normal;
            cook.isFinishing = false;
            cook.isSpicing = true;
            cook.gaugeBar.fillAmount = 0f;
            cook.emptyBar.gameObject.SetActive(false);
            cook.gaugeBar.gameObject.SetActive(false);
            cookingFood = "";
        }

        transform.position = curPos;
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.tag == "Plate")
        {
            plate = coll.gameObject.GetComponent<Plate>();
            onPlate = true;
        }
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        onPlate = false;
    }
}
