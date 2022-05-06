using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeCubeManager : MonoBehaviour
{
    Camera cam;

    public Transform SizeCubeAnchor { get; private set; } = null;
    SizeCube sizeCube = null;

    private void Awake()
    {
        cam = Camera.main;
    }

    private void Start()
    {
        SizeCubeAnchor = new GameObject("SizeCubeAnchor").transform;
    }

    public void TestCreateSizeCube()
    {
        float width = Random.Range(0.3f, 1.0f);
        float height = Random.Range(0.3f, 1.0f);
        float depth = Random.Range(0.3f, 1.0f);
        

        CreateSizeCube(width, height, depth);
    }

    void CheckSizeCube()
    {
        if (sizeCube == null)
        {
            sizeCube = Instantiate(PrefabManager.Instance.sizeCubePrefab, SizeCubeAnchor).GetComponent<SizeCube>();
            sizeCube.SetSizeCubeManager(this);
        }
    }

    public void CreateSizeCube(Vector3 position, Quaternion rotation, float width, float height, float depth)
    {
        CheckSizeCube();

        Debug.Log("pos: " + position.ToString() + "\nrot: " + rotation.ToString());

        sizeCube.SetSizeCube(position, rotation, width, height, depth);
    }

    public void CreateSizeCube(float width, float height, float depth)
    {
        Vector3 position = PlaneManager.Instance.LastHitTestResult?.Position ?? cam.transform.position + cam.transform.forward * 8f;
        Quaternion rotation = PlaneManager.Instance.LastHitTestResult?.Rotation ?? Quaternion.identity;

        CreateSizeCube(position, rotation, width, height, depth);
    }

    public void CreateSizeCubeWithMilimeter(int width, int height, int depth)
    {
        float meterWidth = width / 1000.0f;
        float meterHeight = height / 1000.0f;
        float meterDepth = depth / 1000.0f;

        CreateSizeCube(meterWidth, meterHeight, meterDepth);
    }

    public bool CreateSizeCubeFromCurrentFurniture()
    {
        if (!FurnitureManager.Instance.IsFurnitureExists) return false;

        CheckSizeCube();

        Furniture furniture = FurnitureManager.Instance.CurrentFurniture;
        if (furniture.SizeMeasure == null) return false;

        sizeCube.SetSizeCube(furniture.ObjectTransform, furniture.SizeMeasure.GetCenter(), furniture.SizeMeasure.GetSize());

        return true;
    }

    public void DisableSizeCube()
    {
        sizeCube.DisableSizeCube();
    }
}
