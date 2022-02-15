using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InfoPanel : MonoBehaviour
{
    Menu menu;
    //CSL
    public Data_FurnitureInfo furnitureInfo;
    Data_Furniture furnitureInfos = null;

    public Image furnitureIcon;
    public Image furnitureBookMark;
    public TextMeshProUGUI furnitureNameText;
    public TextMeshProUGUI furnitureInfoText;

    public Sprite marked;
    public Sprite n_marked;

    private void Awake() {
        furnitureInfos = PrefabManager.Instance.furnitureInfos;
        menu = Menu.instance;
    }

    public void BackBtn()
    {
        //slot update. edit later.
        menu.RedrawAllPanel();
        menu.RedrawBookMarkPanel();

        gameObject.SetActive(false);
    }

    public void UpdateInfo(int num)
    {
        furnitureInfo = furnitureInfos.furnitureInfo[num];
        furnitureNameText.text = furnitureInfo.furnitureName;
        furnitureInfoText.text = furnitureInfo.furnitureSize;
        furnitureIcon.sprite = furnitureInfo.furnitureImage;

        if(furnitureInfo.bookMark) furnitureBookMark.sprite = marked;
        else furnitureBookMark.sprite = n_marked;
    }

    public void ClickBookMark()
    {
        furnitureInfo.bookMark = !furnitureInfo.bookMark;
        furnitureInfos.furnitureInfo[furnitureInfo.furnitureNum].bookMark = furnitureInfo.bookMark;

        if (furnitureInfo.bookMark) furnitureBookMark.sprite = marked;
        else furnitureBookMark.sprite = n_marked;
    }
}
