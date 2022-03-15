using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_FurnitureInfoUI : MonoBehaviour
{
    [SerializeField] Image furnitureImage;
    [SerializeField] TextMeshProUGUI furnitureName;
    [SerializeField] TextMeshProUGUI furnitureDescription;

    [SerializeField] Image bookmarkImage;

    UI_FurnitureSelectUI furnitureSelectUI;

    UI_FurnitureSelectItem furnitureItem;
    Data_FurnitureInfo furnitureInfo;

    public void EnableUI(bool isEnable)
    {
        gameObject.SetActive(isEnable);
    }

    public void SetFurnitureSelectUI(UI_FurnitureSelectUI furnitureSelectUI)
    {
        this.furnitureSelectUI = furnitureSelectUI;
    }

    public void SetFurniture(UI_FurnitureSelectItem furnitureItem)
    {
        this.furnitureItem = furnitureItem;
        SetFurniture(furnitureItem.FurnitureInfo);

        UpdateBookmark();
    }

    void SetFurniture(Data_FurnitureInfo furnitureInfo)
    {
        EnableUI(true);

        if (this.furnitureInfo == furnitureInfo) return;

        this.furnitureInfo = furnitureInfo;
        furnitureImage.sprite = furnitureInfo.furnitureImage;
        furnitureName.SetText(furnitureInfo.furnitureName);
        furnitureDescription.SetText(furnitureInfo.description);
    }

    public void CreateFurniture()
    {
        if (furnitureInfo == null) return;

        FurnitureManager.Instance.SetFurniture(FurnitureManager.Instance.CreateFurniture(furnitureInfo));
        EnableUI(false);
        furnitureSelectUI.EnableUI(false);
    }

    public void ToggleBookmark()
    {
        if (furnitureItem == null) return;

        furnitureItem.ToggleBookmark();
        UpdateBookmark();
    }

    void UpdateBookmark()
    {
        if (furnitureItem == null) return;

        bookmarkImage.sprite = furnitureItem.GetBookmarkSprite(furnitureItem.IsBookmarked);
    }
}
