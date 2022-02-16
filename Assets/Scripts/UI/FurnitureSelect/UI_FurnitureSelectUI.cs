using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_FurnitureSelectUI : MonoBehaviour
{
    Canvas canvas;
    [SerializeField] RectTransform content;

    GridLayoutGroup selectGridLayout;

    Data_Furniture furnitureInfos = null;

    List<UI_FurnitureSelectItem> selectItemList = new List<UI_FurnitureSelectItem>();
    Dictionary<long, UI_FurnitureSelectItem> bookmarkItemDict = new Dictionary<long, UI_FurnitureSelectItem>(); 

    public UI_FurnitureSelectItem SelectedFurniture { get; private set; } = null;
    public bool IsSelected => SelectedFurniture != null;

    private void Awake()
    {
        canvas = GetComponent<Canvas>();
        selectGridLayout = content.GetComponent<GridLayoutGroup>();
    }

    private void Start()
    {
        InitFurnitureSelectUI(PrefabManager.Instance.furnitureInfos);
    }

    void InitFurnitureSelectUI(Data_Furniture furnitureInfos)
    {
        this.furnitureInfos = furnitureInfos;
        
        foreach (Data_FurnitureInfo info in furnitureInfos.furnitureInfo)
        {
            UI_FurnitureSelectItem selectItem = Instantiate(PrefabManager.Instance.furnitureSelectItem, content).GetComponent<UI_FurnitureSelectItem>();
            selectItem.SetItem(info);
            //selectItem.OnItemSelected.AddListener(SelectItem);
            selectItem.OnItemBookmarkAdd.AddListener(AddBookmark);
            selectItem.OnItemBookmarkRemove.AddListener(RemoveBookmark);
            selectItemList.Add(selectItem);
        }
        ResizeSelectUI(selectItemList.Count);
    }
    
    // UI

    public void EnableFurnitureUI(bool isEnable)
    {
        canvas.enabled = isEnable;
    }

    public void ShowAllFurniture()
    {
        canvas.enabled = true;

        SelectedFurniture = null;

        foreach (UI_FurnitureSelectItem item in selectItemList)
        {
            //item.ResetSlotSelection();
            item.ShowItem();
        }
        ResizeSelectUI(selectItemList.Count);
    }

    public void ShowBookmarkedFurniture()
    {
        canvas.enabled = true;

        SelectedFurniture = null;

        foreach (UI_FurnitureSelectItem item in selectItemList)
        {
            if (item.IsBookmarked)
            {
                //item.ResetSlotSelection();
                item.ShowItem();
            }
            else
            {
                item.HideItem();
            }
        }
        ResizeSelectUI(bookmarkItemDict.Count);
    }

    void ResizeSelectUI(int itemSize)
    {
        content.sizeDelta = new Vector2(0, (selectGridLayout.cellSize.y + selectGridLayout.spacing.y) * ((itemSize - 1) / selectGridLayout.constraintCount + 1) + selectGridLayout.spacing.y);
    }


    void SelectItem(UI_FurnitureSelectItem selectedItem)
    {
        //SelectedFurniture?.ResetSlotSelection();
        SelectedFurniture = selectedItem;
        //selectedItem.SetSlotSelection();
    }

    // Bookmark

    void AddBookmark(UI_FurnitureSelectItem selectItem)
    {
        bookmarkItemDict.Add(selectItem.FurnitureHashKey, selectItem);
    }

    void RemoveBookmark(UI_FurnitureSelectItem selectItem)
    {
        bookmarkItemDict.Remove(selectItem.FurnitureHashKey);
    }

    // Button

    public void SelectSelectedItem()
    {
        if (!IsSelected) return;

        FurnitureManager.Instance.SetFurniture(FurnitureManager.Instance.CreateFurniture(SelectedFurniture.FurnitureInfo));
        
        CloseItemSelectUI();
    }

    public void CloseItemSelectUI()
    {
        canvas.enabled = false;
    }
}
