using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_FurnitureSelectUI : MonoBehaviour
{
    Canvas canvas;
    [SerializeField] RectTransform content;

    [SerializeField] UI_FurnitureInfoUI furnitureInfoUI;
    [SerializeField] UI_FurnitureTypeSelectUI furnitureTypeSelectUI;

    [SerializeField] TextMeshProUGUI pageName;

    GridLayoutGroup selectGridLayout;

    Data_Furniture furnitureInfos = null;

    List<UI_FurnitureSelectItem> selectItemList = new List<UI_FurnitureSelectItem>();
    Dictionary<string, UI_FurnitureSelectItem> bookmarkItemDict = new Dictionary<string, UI_FurnitureSelectItem>();
    bool isBeforeStarting = true;

    private void Awake()
    {
        canvas = GetComponent<Canvas>();
        selectGridLayout = content.GetComponent<GridLayoutGroup>();
    }

    private void Start()
    {
        furnitureInfoUI.SetFurnitureSelectUI(this);
        InitFurnitureSelectUI(PrefabManager.Instance.furnitureInfos);

        isBeforeStarting = false;
    }

    void InitFurnitureSelectUI(Data_Furniture furnitureInfos)
    {
        this.furnitureInfos = furnitureInfos;

        List<string> bookmarkData = SaveManager.LoadBookmarkData();

        foreach (Data_FurnitureInfo info in furnitureInfos.furnitureInfo)
        {
            UI_FurnitureSelectItem selectItem = Instantiate(PrefabManager.Instance.furnitureSelectItem, content).GetComponent<UI_FurnitureSelectItem>();
            selectItem.SetItem(info);
            selectItem.SetInfoUI(furnitureInfoUI);
            selectItem.OnItemBookmarkAdd.AddListener(AddBookmark);
            selectItem.OnItemBookmarkRemove.AddListener(RemoveBookmark);
            selectItemList.Add(selectItem);

            if (bookmarkData != null && bookmarkData.Remove(selectItem.FurnitureHashKey))
            {
                selectItem.ToggleBookmark();
            }
        }
        ResizeSelectUI(selectItemList.Count);
    }
    
    // UI

    public void EnableUI(bool isEnable)
    {
        canvas.enabled = isEnable;
        furnitureInfoUI.EnableUI(false);
    }

    public void ShowTypeFurniture(FurnitureType type)
    {
        EnableUI(true);
        pageName.SetText(type.ToString());
        int typeCount = 0;
        foreach (UI_FurnitureSelectItem item in selectItemList)
        {
            if(item.FurnitureInfo.furnitureType == type)
            {
                item.ShowItem();
                typeCount++;
            }
            else
            {
                item.HideItem();
            }
            ResizeSelectUI(typeCount);
        }
    }

    public void ShowAllFurniture()
    {
        EnableUI(true);
        
        foreach (UI_FurnitureSelectItem item in selectItemList)
        {
            item.ShowItem();
        }
        ResizeSelectUI(selectItemList.Count);
    }

    public void ShowBookmarkedFurniture()
    {
        EnableUI(true);
        pageName.SetText("Your wishlist");
        foreach (UI_FurnitureSelectItem item in selectItemList)
        {
            if (item.IsBookmarked)
            {
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

    
    // Bookmark

    void AddBookmark(UI_FurnitureSelectItem selectItem)
    {
        bookmarkItemDict.Add(selectItem.FurnitureHashKey, selectItem);
        if (!isBeforeStarting)
            SaveManager.SaveBookmarkData(bookmarkItemDict);
    }

    void RemoveBookmark(UI_FurnitureSelectItem selectItem)
    {
        bookmarkItemDict.Remove(selectItem.FurnitureHashKey);

        if (!isBeforeStarting)
            SaveManager.SaveBookmarkData(bookmarkItemDict);
    }

    public bool BookmarkCheck(long hashkey)
    {
        bool result = false;
        if (bookmarkItemDict.ContainsKey(hashkey))
        {
            Debug.Log("가구 찾음");
            UI_FurnitureSelectItem clone = bookmarkItemDict[hashkey];
            result = clone.IsBookmarked;
        }
        else
        {
            Debug.Log("가구 못찾음");
        }
        return result;
    }
}
