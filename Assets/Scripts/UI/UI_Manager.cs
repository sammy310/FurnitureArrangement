using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    public static UI_Manager Instance { get; private set; } = null;

    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    int UILayer;

    GraphicRaycaster[] mGraphicRayCaster;
    PointerEventData mPointerEventData;
    EventSystem mEventSystem;


    public UI_FurnitureSelectUI FurnitureSelectUI { get; private set; } = null;


    private void Start()
    {
        UILayer = LayerMask.NameToLayer("UI");

        mEventSystem = FindObjectOfType<EventSystem>();
        mGraphicRayCaster = FindObjectsOfType<GraphicRaycaster>();

        FurnitureSelectUI = FindObjectOfType<UI_FurnitureSelectUI>();
    }


    public void OpenFurnitureSelectUI()
    {
        FurnitureSelectUI?.ShowAllFurniture();
    }


    public bool IsPointerOverUI()
    {
        PointerEventData mPointerEventData = new PointerEventData(mEventSystem) { position = Input.mousePosition };
        foreach (var rayCaster in mGraphicRayCaster)
        {
            var results = new List<RaycastResult>();
            rayCaster.Raycast(mPointerEventData, results);
            foreach (var result in results)
            {
                if (result.gameObject.layer == UILayer)
                {
                    return true;
                }
            }
        }
        return false;
    }
}
