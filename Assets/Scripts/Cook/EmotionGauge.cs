using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmotionGauge : MonoBehaviour {

    Image emoGauge;

    void Start()
    {
        emoGauge = GetComponent<Image>();
        emoGauge.fillAmount = 0f;
    }

    public void FillGauge()
    {
        emoGauge.fillAmount += 0.08f;
    }
}
