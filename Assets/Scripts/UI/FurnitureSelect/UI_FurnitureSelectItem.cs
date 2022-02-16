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

public class UI_FurnitureSelectItem : MonoBehaviour//, IPointerClickHandler
{
    [SerializeField] Image image;
    [SerializeField] TMP_Text text_name;
    [SerializeField] Image furnitureImage;
    [SerializeField] Image bookmarkImage;

    [SerializeField] Sprite[] bookmarkSprites;

    //public ItemSelectedEvent OnItemSelected = new ItemSelectedEvent();
    public ItemSelectedEvent OnItemBookmarkAdd = new ItemSelectedEvent();
    public ItemSelectedEvent OnItemBookmarkRemove = new ItemSelectedEvent();

    public Data_FurnitureInfo FurnitureInfo { get; private set; } = null;
    //public bool IsSelected { get; private set; } = false;
    public bool IsBookmarked { get; private set; } = false;

    public long FurnitureHashKey => FurnitureInfo.hashKey;

    readonly Color DefaultColor = Color.white;
    readonly Color SelectionColor = new Color(1f, 0.5f, 0.5f);
    

    //public void OnPointerClick(PointerEventData eventData)
    //{
    //    if (IsSelected) return;

    //    OnItemSelected.Invoke(this);
    //}

    public void SetItem(Data_FurnitureInfo furnitureInfo)
    {
        FurnitureInfo = furnitureInfo;
        furnitureImage.sprite = furnitureInfo.furnitureImage;
        text_name.SetText(furnitureInfo.furnitureName);

        //ResetSlotSelection();
    }

    //public void SetSlotSelection()
    //{
    //    IsSelected = true;
    //    image.color = SelectionColor;
    //}

    //public void ResetSlotSelection()
    //{
    //    IsSelected = false;
    //    image.color = DefaultColor;
    //}

    public void ShowItem()
    {
        gameObject.SetActive(true);
    }

    public void HideItem()
    {
        gameObject.SetActive(false);
    }


    public void ToggleBookmark()
    {
        IsBookmarked = !IsBookmarked;

        if (IsBookmarked)
        {
            bookmarkImage.sprite = bookmarkSprites[1];
            OnItemBookmarkAdd.Invoke(this);
        }
        else
        {
            bookmarkImage.sprite = bookmarkSprites[0];
            OnItemBookmarkRemove.Invoke(this);
        }
    }
}
