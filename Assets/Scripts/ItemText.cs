using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemText : MonoBehaviour
    , IPointerClickHandler
    , IPointerEnterHandler
    , IPointerExitHandler
{
    public GameObject textImage;
    public ItemManager itemManager;

    void Start()
    {
        //textImage = GetComponentInChildren<GameObject>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        switch(gameObject.name)
        {
            case "SlimeSlot":
                if (itemManager.slime > 0)
                {
                    itemManager.slime--;
                    itemManager.slimeSpice++;
                }
                break;
            case "FairySlot":
                if (itemManager.fairy > 0)
                {
                    itemManager.fairy--;
                    itemManager.fairySpice++;
                }
                break;
            case "FireSlot":
                if (itemManager.fire > 0)
                {
                    itemManager.fire--;
                    itemManager.fireSpice++;
                }
                break;
            case "BansheeSlot":
                if (itemManager.banshee > 0)
                {
                    itemManager.banshee--;
                    itemManager.bansSpice++;
                }
                break;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        textImage.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        textImage.SetActive(false);
    }
}
