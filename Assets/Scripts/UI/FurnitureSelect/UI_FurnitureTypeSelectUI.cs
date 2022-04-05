using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UI_FurnitureTypeSelectUI : MonoBehaviour
{
    Canvas canvas;
    [SerializeField] RectTransform content;

    [SerializeField] UI_FurnitureInfoUI furnitureInfoUI;
    [SerializeField] UI_FurnitureSelectUI furnitureSelectUI;

    GridLayoutGroup selectGridLayout;

    List<UI_FurnitureTypeItem> typeItemList = new List<UI_FurnitureTypeItem>();

    private void Awake()
    {
        canvas = GetComponent<Canvas>();
        selectGridLayout = content.GetComponent<GridLayoutGroup>();
    }

    private void Start()
    {   
        InitFurnitureSelectUI();
        furnitureInfoUI.SetFurnitureTypeSelectUI(this);
    }

    void InitFurnitureSelectUI()
    {
        foreach(FurnitureType type in System.Enum.GetValues(typeof(FurnitureType)))
        {
            UI_FurnitureTypeItem typeItem = Instantiate(PrefabManager.Instance.furnitureTypeItem, content).GetComponent<UI_FurnitureTypeItem>();
            typeItem.SetItem(type);
            typeItem.ShowTypeItems(this,furnitureSelectUI);
            typeItemList.Add(typeItem);
        }
        ResizeSelectUI(typeItemList.Count);
    }

    public void EnableUI(bool isEnable)
    {
        canvas.enabled = isEnable;
    }

    public void ShowAllType()
    {
        EnableUI(true);
        foreach(UI_FurnitureTypeItem typeItem in typeItemList)
        {
            typeItem.ShowItem();
        }
        ResizeSelectUI(typeItemList.Count);
    }


    void ResizeSelectUI(int itemSize)
    {
        content.sizeDelta = new Vector2(0, (selectGridLayout.cellSize.y + selectGridLayout.spacing.y) * ((itemSize - 1) / selectGridLayout.constraintCount + 1) + selectGridLayout.spacing.y);
    }
}
