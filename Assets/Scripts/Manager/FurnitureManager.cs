using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureManager : MonoBehaviour
{
    public static FurnitureManager Instance { get; private set; } = null;

    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        Init();
    }


    Transform furnitureAnchor = null;

    //public int FurnitureSize => PrefabManager.Instance.furniturePrefab.furniturePrefabs.Length;

    List<Furniture> furnitures = new List<Furniture>();
    public Furniture CurrentFurniture { get; private set; } = null;
    public bool IsFurnitureExists => CurrentFurniture != null;
    

    void Init()
    {
        furnitureAnchor = (new GameObject("Furniture Anchor")).transform;
    }


    public Furniture CreateFurniture(Data_FurnitureInfo furnitureInfo)
    {
        Furniture furniture = Instantiate(PrefabManager.Instance.furniturePrefab, furnitureAnchor).GetComponent<Furniture>();
        furniture.InitFurniture(furnitureInfo);
        furnitures.Add(furniture);
        return furniture;
    }

    public void SetFurniture(Furniture furniture)
    {
        CurrentFurniture = furniture;
    }

    public void ResetFurniture()
    {
        foreach (var furniture in furnitures)
        {
            Destroy(furniture.gameObject);
        }
        furnitures.Clear();

        CurrentFurniture = null;
    }

    public static void EnableRendererColliderCanvas(GameObject gameObject, bool enable)
    {
        var rendererComponents = gameObject.GetComponentsInChildren<Renderer>(true);
        var colliderComponents = gameObject.GetComponentsInChildren<Collider>(true);
        var canvasComponents = gameObject.GetComponentsInChildren<Canvas>(true);
        // Enable rendering:
        foreach (var component in rendererComponents)
            component.enabled = enable;
        // Enable colliders:
        foreach (var component in colliderComponents)
            component.enabled = enable;
        // Enable canvas':
        foreach (var component in canvasComponents)
            component.enabled = enable;
    }
}
