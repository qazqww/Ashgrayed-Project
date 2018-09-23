using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plate : MonoBehaviour {

    public Sprite plate_normal;
    public Sprite plate_tomato;
    public Sprite plate_onion;
    public Sprite plate_lettuce;
    public Sprite plate_meat;
    public Sprite plate_bread;
    public Sprite plate_clean;
    public GameObject newPlate;

    [HideInInspector] public bool tomatoSoup;
    [HideInInspector] public bool onionSoup;
    [HideInInspector] public bool salad;
    [HideInInspector] public bool steak;
    [HideInInspector] public bool bread;

    [HideInInspector] public bool canMove;
    [HideInInspector] public bool clean;
    [HideInInspector] public bool finished;
    [HideInInspector] public int grade;

    public int emotion_plate;

    BoxCollider2D box;
    public CircleCollider2D circle;
    Vector2 curPos;
    Customer customer;

    private string foodToCustomer;
    private bool triggerOn;

    void Start () {
        tomatoSoup = false;
        onionSoup = false;
        steak = false;
        salad = false;
        bread = false;
        canMove = false;
        clean = false;
        finished = false;
        grade = 0;
        triggerOn = false;
        curPos = transform.position;
        gameObject.GetComponent<SpriteRenderer>().sprite = plate_normal;
        box = GetComponent<BoxCollider2D>();
        circle = GetComponent<CircleCollider2D>();
    }
	
	void FixedUpdate () {
		if(tomatoSoup)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = plate_tomato;
            canMove = true;
            foodToCustomer = "tomatoSoup";
            tomatoSoup = false;
        }
        else if (onionSoup)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = plate_onion;
            canMove = true;
            foodToCustomer = "onionSoup";
            onionSoup = false;
        }
        else if (salad)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = plate_lettuce;
            canMove = true;
            foodToCustomer = "salad";
            salad = false;
        }
        else if (steak)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = plate_meat;
            canMove = true;
            foodToCustomer = "steak";
            steak = false;
        }
        else if (bread)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = plate_bread;
            canMove = true;
            foodToCustomer = "bread";
            bread = false;
        }

        if (clean)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = plate_clean;
        }

        if (finished)
        {
            triggerOn = false;
            canMove = true;
        }
    }

    public void MakeNewPlate(Vector2 curPos)
    {
        Instantiate(newPlate, curPos, Quaternion.identity);
    }

    private void OnMouseDrag()
    {
        if(canMove)
        {
            Vector2 mousePos;
            mousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            Vector2 objPos = Camera.current.ScreenToWorldPoint(mousePos);
            transform.position = objPos;
        }
    }

    private void OnMouseUp()
    {
        if (finished)
        {

        }
        else if (triggerOn)
        {
            MakeNewPlate(curPos);
            customer.received = true;
            canMove = false;
        }
        else if (!triggerOn)
        {
            transform.position = curPos;
            box.enabled = true;
        }
    }

    private void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Customer")
        {
            customer = coll.GetComponent<Customer>();
            customer.received_name = foodToCustomer;
            customer.received_emotion = emotion_plate;
            triggerOn = true;
        }
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        triggerOn = false;
    }
}
