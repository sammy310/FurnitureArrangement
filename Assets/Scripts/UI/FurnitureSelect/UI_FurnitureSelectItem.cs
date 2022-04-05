using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSelectedEvent : UnityEvent<UI_FurnitureSelectItem>
{

}

public class UI_FurnitureSelectItem : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI furnitureName;
    [SerializeField] Image furnitureImage;
    [SerializeField] Image bookmarkImage;

    [SerializeField] Button selectButton;
    [SerializeField] Sprite[] bookmarkSprites;
    
    public ItemSelectedEvent OnItemBookmarkAdd = new ItemSelectedEvent();
    public ItemSelectedEvent OnItemBookmarkRemove = new ItemSelectedEvent();

    public Data_FurnitureInfo FurnitureInfo { get; private set; } = null;
    public bool IsBookmarked { get; private set; } = false;

    public string FurnitureHashKey => FurnitureInfo.furnitureName;

    readonly Color DefaultColor = Color.white;
    readonly Color SelectionColor = new Color(1f, 0.5f, 0.5f);
    
    
    public void SetItem(Data_FurnitureInfo furnitureInfo)
    {
        FurnitureInfo = furnitureInfo;
        furnitureImage.sprite = furnitureInfo.furnitureImage;
        furnitureName.SetText(furnitureInfo.furnitureName);
    }

    public void SetInfoUI(UI_FurnitureInfoUI furnitureInfoUI)
    {
        selectButton.onClick.AddListener(() => furnitureInfoUI.SetFurniture(this));
    }
    
    public void ShowItem()
    {
        gameObject.SetActive(true);
    }

    public void HideItem()
    {
        gameObject.SetActive(false);
    }


    public Sprite GetBookmarkSprite(bool isBookmared)
    {
        return isBookmared ? bookmarkSprites[1] : bookmarkSprites[0];
    }

    public void ToggleBookmark()
    {
        IsBookmarked = !IsBookmarked;

        bookmarkImage.sprite = GetBookmarkSprite(IsBookmarked);

        if (IsBookmarked)
        {
            OnItemBookmarkAdd.Invoke(this);
        }
        else
        {
            OnItemBookmarkRemove.Invoke(this);
        }
    }
}
