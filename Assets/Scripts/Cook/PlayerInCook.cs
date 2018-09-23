using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerInCook : PlayerMovement {

    public ChangeImage having;
    public Frypan frypan;
    public GameObject SprinkleSpice;

    public Text text;
    public Image gaugeBar;
    public Image emptyBar;

    public Sprite tomatoCut;
    public Sprite onionCut;
    public Sprite lettuceCut;

    Plate plate;
    GameObject playerTemp;
    Camera cameraTemp;
    SpriteRenderer spriteTemp;
    GameObject other;
    ItemManager itemManager;

    bool IngredientBtn;
    bool SpiceBtn;
    bool CutBoardBtn;
    bool FrypanBtn;
    bool PlateBtn;

    private bool isCooking;
    private float cookSpeed;

    [HideInInspector] public string haveIngredient;
    [HideInInspector] public string haveSpice;
    [HideInInspector] public bool isFinishing;
    [HideInInspector] public bool isSpicing;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        playerTemp = GameObject.FindGameObjectWithTag("Player");
        itemManager = FindObjectOfType<ItemManager>();
        //spriteTemp = playerTemp.GetComponent<SpriteRenderer>();
        //cameraTemp = playerTemp.GetComponentInChildren<Camera>();
    }

    private void Start()
    {
        IngredientBtn = false;
        SpiceBtn = false;
        CutBoardBtn = false;
        FrypanBtn = false;
        PlateBtn = false;
        isCooking = false;
        isFinishing = false;
        isSpicing = false;
        gaugeBar.fillAmount = 0f;
        gaugeBar.gameObject.SetActive(false);
        emptyBar.gameObject.SetActive(false);
        SprinkleSpice.SetActive(false);
        playerTemp.SetActive(false);
       // cameraTemp.gameObject.SetActive(false);
        //spriteTemp.enabled = false;
    }

    private void Update()
    {
        if (isCooking) {
            gaugeBar.fillAmount += Time.deltaTime * cookSpeed;

            if (Input.GetKeyDown(KeyCode.R)) // 요리를 마친다.
            {
                isCooking = false;
                isFinishing = true;
            }
        }

        if (IngredientBtn)
        {
            text.gameObject.SetActive(true);
            text.text = "[E] 키로 재료 집기";

            if (Input.GetKeyDown(KeyCode.E))
            {
                haveIngredient = other.name;
                having.changeSprite(other.GetComponent<SpriteRenderer>().sprite);
            }
        }

        else if (SpiceBtn)
        {
            text.gameObject.SetActive(true);
            text.text = "[E] 키로 향신료 집기";
            if (Input.GetKeyDown(KeyCode.E))
            {
                switch(other.name)
                {
                    case "spice_slime":
                        if (itemManager.slimeSpice > 0)
                        {
                            itemManager.slimeSpice--;
                            haveSpice = other.name;
                            having.changeSprite(other.GetComponent<SpriteRenderer>().sprite);
                        }
                        break;
                    case "spice_fairy":
                        if (itemManager.fairySpice > 0)
                        {
                            itemManager.fairySpice--;
                            haveSpice = other.name;
                            having.changeSprite(other.GetComponent<SpriteRenderer>().sprite);
                        }
                        break;
                    case "spice_fire":
                        if (itemManager.fireSpice > 0)
                        {
                            itemManager.fireSpice--;
                            haveSpice = other.name;
                            having.changeSprite(other.GetComponent<SpriteRenderer>().sprite);
                        }
                        break;
                    case "spice_banshee":
                        if (itemManager.bansSpice > 0)
                        {
                            itemManager.bansSpice--;
                            haveSpice = other.name;
                            having.changeSprite(other.GetComponent<SpriteRenderer>().sprite);
                        }
                        break;
                    default:
                        haveSpice = other.name;
                        having.changeSprite(other.GetComponent<SpriteRenderer>().sprite);
                        break;
                }
                
            }
        }

        else if (CutBoardBtn)
        {
            text.gameObject.SetActive(true);
            text.text = "[E] 키로 재료 손질";
            if (Input.GetKeyDown(KeyCode.E))
            {
                switch (haveIngredient)
                {
                    case "tomato":
                        having.changeSprite(tomatoCut);
                        haveIngredient += "_cut";
                        break;
                    case "onion":
                        having.changeSprite(onionCut);
                        haveIngredient += "_cut";
                        break;
                    case "lettuce":
                        having.changeSprite(lettuceCut);
                        haveIngredient += "_cut";
                        break;
                }
            }
        }

        else if (FrypanBtn)
        {
            text.gameObject.SetActive(true);

            if (isCooking)
            {
                text.text = "[R] 키로 요리 마치기";
            }

            else
            {
                text.text = "[E] 키로 요리하기";

                if (Input.GetKeyDown(KeyCode.E)) // 요리를 시작한다.
                {
                    switch (haveIngredient)
                    {
                        case "tomato_cut":
                            Cook("tomato");
                            cookSpeed = 0.1f;
                            break;
                        case "onion_cut":
                            Cook("onion");
                            cookSpeed = 0.1f;
                            break;
                        case "lettuce_cut":
                            Cook("lettuce");
                            cookSpeed = 0.3f;
                            break;
                        case "meat":
                            Cook("meat");
                            cookSpeed = 0.2f;
                            break;
                        case "bread":
                            Cook("bread");
                            cookSpeed = 0.25f;
                            break;
                    }

                }
            }
        }

        else if (PlateBtn)
        {
            if (isSpicing == true && haveSpice != "")
            {
                plate = other.GetComponent<Plate>();
                text.gameObject.SetActive(true);
                text.text = "[E] 키로 향신료 뿌리기";
                if (Input.GetKeyDown(KeyCode.E))
                {
                    switch (haveSpice)
                    {
                        case "spice_slime":
                            plate.emotion_plate = 1;
                            break;
                        case "spice_fairy":
                            plate.emotion_plate = 2;
                            break;
                        case "spice_fire":
                            plate.emotion_plate = 3;
                            break;
                        case "spice_banshee":
                            plate.emotion_plate = 4;
                            break;
                        default:
                            plate.emotion_plate = 0;
                            break;
                    }

                    plate.grade++;
                    haveSpice = "";
                    having.changeSprite(null);
                    SprinkleSpice.SetActive(true);
                    isSpicing = false;
                }
            }
        }

        if(Input.GetKeyDown(KeyCode.P))
        {
            CafeteriaEnd();
        }
    }

    private void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        MoveAnim(h, v);

        movement.Set(h, v, 0f);
        movement = movement.normalized * moveSpeed * Time.deltaTime;

        rb.MovePosition(transform.position + movement);
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Ingredient")
        {
            other = coll.gameObject;
            IngredientBtn = true;
        }
        else if (coll.gameObject.tag == "Spice")
        {
            other = coll.gameObject;
            SpiceBtn = true;
        }
        else if (coll.gameObject.tag == "CuttingBoard")
        {
            CutBoardBtn = true;
        }
        else if (coll.gameObject.tag == "Frypan")
        {
            FrypanBtn = true;
        }
        else if (coll.gameObject.tag == "Plate")
        {
            other = coll.gameObject;
            PlateBtn = true;
        }
    }

    void Cook(string ingredient)
    {
        isCooking = true;
        frypan.changeSprite(ingredient);
        having.changeSprite(null);
        haveIngredient = "";

        emptyBar.gameObject.SetActive(true);
        gaugeBar.gameObject.SetActive(true);
    }

    void CafeteriaEnd()
    {
        playerTemp.SetActive(true);
        playerTemp.transform.position = new Vector2(19f, -39f);
        SceneManager.LoadScene("forest town", LoadSceneMode.Single);
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        text.gameObject.SetActive(false);
        SprinkleSpice.SetActive(false);
        AllBtnOff();
    }

    void AllBtnOff()
    {
        IngredientBtn = false;
        SpiceBtn = false;
        CutBoardBtn = false;
        FrypanBtn = false;
        PlateBtn = false;
    }
}
