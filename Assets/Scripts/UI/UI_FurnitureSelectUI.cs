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

    public UI_FurnitureSelectItem SelectedFurniture { get; private set; } = null;
    public bool IsSelected => SelectedFurniture != null;

    private void Awake()
    {
        canvas = GetComponent<Canvas>();
        selectGridLayout = content.GetComponent<GridLayoutGroup>();
    }

    public void InitItemSelection(Data_Furniture furnitureInfos)
    {
        canvas.enabled = true;

        SelectedFurniture = null;

        this.furnitureInfos = furnitureInfos;

        ResetSelectItems();

        foreach (Data_FurnitureInfo info in furnitureInfos.furnitureInfo)
        {
            UI_FurnitureSelectItem selectItem = Instantiate(PrefabManager.Instance.furnitureSelectItem, content).GetComponent<UI_FurnitureSelectItem>();
            selectItem.SetItem(info);
            selectItem.OnItemSelected.AddListener(SelectItem);
            selectItemList.Add(selectItem);
        }
        ResizeSelectUI(selectItemList.Count);
    }

    void ResetSelectItems()
    {
        foreach (var item in selectItemList)
        {
            Destroy(item.gameObject);
        }
        selectItemList.Clear();
    }

    void ResizeSelectUI(int itemSize)
    {
        content.sizeDelta = new Vector2(0, (selectGridLayout.cellSize.y + selectGridLayout.spacing.y) * itemSize + selectGridLayout.spacing.y);
    }


    void SelectItem(UI_FurnitureSelectItem selectedItem)
    {
        SelectedFurniture?.ResetSlotSelection();
        SelectedFurniture = selectedItem;
        selectedItem.SetSlotSelection();
    }


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
