using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_FurnitureTypeItem : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI furnitureTypeText;
    [SerializeField] Image furnitureTypeImage;
    [SerializeField] Button selectButton;

    //[SerializeField] UI_FurnitureTypeSelectUI furnitureTypeSelectUI;


    FurnitureType furnitureType;

    public void SetItem(FurnitureType type)
    {
        furnitureType = type;
        //furnitureImage.sprite = furnitureInfo.furnitureImage;
        furnitureTypeText.SetText(type.ToString());
    }

    public void ShowTypeItems(UI_FurnitureTypeSelectUI furnitureTypeSelectUI, UI_FurnitureSelectUI furnitureSelectUI)
    {
        Debug.Log("Click");
        selectButton.onClick.AddListener(() => furnitureTypeSelectUI.EnableUI(false));
        selectButton.onClick.AddListener(() => furnitureSelectUI.ShowTypeFurniture(furnitureType));
    }

    public void ShowItem()
    {
        gameObject.SetActive(true);
    }

    public void HideItem()
    {
        gameObject.SetActive(false);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
