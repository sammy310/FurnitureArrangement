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

public class UI_FurnitureSelectItem : MonoBehaviour, IPointerClickHandler
{
    Image image;
    [SerializeField] TMP_Text text_name;

    public ItemSelectedEvent OnItemSelected = new ItemSelectedEvent();

    public Data_FurnitureInfo FurnitureInfo { get; private set; } = null;
    public bool IsSelected { get; private set; } = false;

    readonly Color DefaultColor = Color.black;
    readonly Color SelectionColor = new Color(1f, 0.5f, 0.5f);


    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (IsSelected) return;

        OnItemSelected.Invoke(this);
    }

    public void SetItem(Data_FurnitureInfo furnitureInfo)
    {
        FurnitureInfo = furnitureInfo;
        text_name.SetText(furnitureInfo.furnitureName);

        ResetSlotSelection();
    }

    public void SetSlotSelection()
    {
        IsSelected = true;
        image.color = SelectionColor;
    }

    public void ResetSlotSelection()
    {
        IsSelected = false;
        image.color = DefaultColor;
    }
}
