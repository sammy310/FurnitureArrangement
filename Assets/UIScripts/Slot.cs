using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Slot : MonoBehaviour
{
    public Data_FurnitureInfo furnitureInfo;
    Data_Furniture furnitureInfos = null;

    public Image furnitureIcon;
    public Image furnitureBookMark;
    public TextMeshProUGUI furnitureText;
    public Sprite marked;
    public Sprite n_marked;

    void Awake()
    {
        furnitureInfos = PrefabManager.Instance.furnitureInfos;
        Button slotBtn = transform.Find("SlotBtn").GetComponent<Button>();
        slotBtn.onClick.AddListener(()=> GameObject.Find("Canvas").GetComponent<Menu>().InfoBtnClicked(furnitureInfo.furnitureNum));
        //slotBtn.onClick.AddListener(()=> GameObject.Find("InfoPanel").GetComponent<InfoPanel>().UpdateInfo(furniture));
    }

    public void UpdateSlotUI()
    {
        furnitureText.text = furnitureInfo.furnitureName;
        furnitureIcon.sprite = furnitureInfo.furnitureImage;
        furnitureIcon.gameObject.SetActive(true);

        if(furnitureInfo.bookMark) furnitureBookMark.sprite = marked;
        else furnitureBookMark.sprite = n_marked;
    }

    public void RemoveSlot()
    {
        furnitureInfo = null;
        furnitureIcon.gameObject.SetActive(false);

    }

    public void ClickBookMark()
    {
        furnitureInfo.bookMark = !furnitureInfo.bookMark;
        furnitureInfos.furnitureInfo[furnitureInfo.furnitureNum].bookMark = furnitureInfo.bookMark;

        if (furnitureInfo.bookMark) furnitureBookMark.sprite = marked;
        else furnitureBookMark.sprite = n_marked;
    }

    public void ClickBookMarkBMTab()
    {
        furnitureInfo.bookMark = !furnitureInfo.bookMark;

        if (furnitureInfo.bookMark) furnitureBookMark.sprite = marked;
        else furnitureBookMark.sprite = n_marked;
    }
}
