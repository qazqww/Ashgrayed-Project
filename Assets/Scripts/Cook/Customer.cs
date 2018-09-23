using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Customer : MonoBehaviour {

    public string[] foods;

    public Plate plate;

    Text text;
    Image image;
    Animator anim;    
    Entrance entrance;
    BoxCollider2D box;
    EmotionGauge emoGauge;
    ItemManager itemManager;

    int index;
    int emotion;

    float timeCount;
    bool timeCountOn;
    float speechCount;
    bool speechCountOn;
    float eatingCount;
    bool eatingCountOn;

    [HideInInspector] public bool received;
    [HideInInspector] public string received_name;
    [HideInInspector] public int received_emotion;

    private void Awake()
    {
        itemManager = FindObjectOfType<ItemManager>();
        emoGauge = FindObjectOfType<EmotionGauge>();
    }

    void Start () {
        anim = GetComponent<Animator>();

        text = GetComponentInChildren<Text>();
        image = GetComponentInChildren<Image>();
        entrance = GetComponent<Entrance>();
        box = GetComponent<BoxCollider2D>();            

        text.gameObject.SetActive(false);
        image.gameObject.SetActive(false);

        emotion = Random.Range(0, 250);
        emotion = emotion % 10;
        Debug.Log(emotion);
        timeCount = 0f;
        timeCountOn = false;
        speechCount = 0f;
        speechCountOn = false;
        eatingCount = 0f;
        eatingCountOn = false;
        received = false;
	}
	
	void Update () {
        
        // 주문 대기시간 관련
        if (timeCountOn)
        {
            timeCount += Time.deltaTime;

            if (timeCount >= 50f)
                Late();

            else if (timeCount >= 35f && timeCount <= 36f)
                SpeechBubble("음식은 언제 나오죠?");
        }

        // 말풍선 관련
        if (speechCountOn)
            speechCount += Time.deltaTime;

        if(speechCount >= 3f)
        {
            text.gameObject.SetActive(false);
            image.gameObject.SetActive(false);
            speechCount = 0f;
            speechCountOn = false;
        }

        if (eatingCountOn)
        {
            eatingCount += Time.deltaTime;
            box.enabled = false;
        }

        if (eatingCount >= 15f)
        {
            eatingCountOn = false;
            CheckGrade();
        }

        if (received)
        {
            if (emotion % 5 == received_emotion && received_emotion != 0 && timeCountOn)
                emoGauge.FillGauge();

            timeCountOn = false;
            timeCount = 0f;

            switch (index)
            {
                case 0:
                    if (received_name == "tomatoSoup")
                    {
                        eatingCountOn = true;
                        anim.SetInteger("behavior", 2);
                    }
                    else
                        Fail();
                    break;
                case 1:
                    if (received_name == "onionSoup")
                    {
                        eatingCountOn = true;
                        anim.SetInteger("behavior", 2);
                    }
                    else
                        Fail();
                    break;
                case 2:
                    if (received_name == "steak")
                    {
                        eatingCountOn = true;
                        anim.SetInteger("behavior", 2);
                    }
                    else
                        Fail();
                    break;
                case 3:
                    if (received_name == "salad")
                    {
                        eatingCountOn = true;
                        anim.SetInteger("behavior", 2);
                    }
                    else
                        Fail();
                    break;
                case 4:
                    if (received_name == "bread")
                    {
                        eatingCountOn = true;
                        anim.SetInteger("behavior", 2);
                    }
                    else
                        Fail();
                    break;
            }
        }
    }

    public void Monologue()
    {
        switch(emotion)
        {
            case 0: // 일반
            case 5:
                Order();
                break;

            case 1: // 사랑 필요
                SpeechBubble("외롭긴 한데 혼자가 더 편한 것 같아.");
                break;
            case 6:
                SpeechBubble("인생은 무의미한 것 같아.");
                break;

            case 2: // 기쁨 필요
                SpeechBubble("쉴 시간이 갑자기 생기긴 했는데... 어쩌지?");
                break;
            case 7:
                SpeechBubble("수당 받는 날인데 돈 나갈 게 걱정이네...");
                break;

            case 3: // 분노 필요
                SpeechBubble("어차피 무시당하며 살아가는 게 인생인걸?");
                break;
            case 8:
                SpeechBubble("말도 없이 약속을 파토내다니.. 뭐, 상관없어.");
                break;

            case 4: // 슬픔 필요
                SpeechBubble("이별했지만 난 괜찮아.");
                break;
            case 9:
                SpeechBubble("큰 병에 걸렸지만 시간이 지나면 괜찮아지겠지?");
                break;
        }
    }

    public void Order()
    {
        box.enabled = true;
        anim.SetInteger("behavior", 1);
        index = Random.Range(0, foods.Length);
        SpeechBubble(foods[index] + " 주세요.");
        timeCountOn = true;
    }

    void CheckGrade()
    {
        if (plate.grade >= 2)
            Success();
        else
            Bad();
    }

    void Success()
    {
        plate.clean = true;
        GoToHome();
        SpeechBubble("맛있게 잘 먹고 갑니다.");
        itemManager.money += 25;
    }

    void Bad()
    {
        plate.clean = true;
        GoToHome();
        SpeechBubble("그럭저럭이네. 수고하세요.");
        itemManager.money += 10;
    }

    void Fail()
    {
        GoToHome();
        SpeechBubble("제가 주문한 음식이 아니네요.");
    }

    void Late()
    {
        timeCount = 0f;
        timeCountOn = false;
        received = false;
        SpeechBubble("음식이 안 나오네...");
        anim.SetInteger("behavior", 3);
    }

    void GoToHome()
    {
        received = false;
        eatingCount = 0f;
        plate.finished = true;
        plate.gameObject.tag = "OldPlate";
        anim.SetInteger("behavior", 3);
    }

    void GoToRealHome()
    {
        entrance.enabled = true;
    }

    void SpeechBubble(string speech)
    {
        speechCountOn = true;
        text.gameObject.SetActive(true);
        image.gameObject.SetActive(true);
        text.text = speech;
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        plate = coll.gameObject.GetComponent<Plate>();
    }

}
