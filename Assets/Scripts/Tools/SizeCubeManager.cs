using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SizeCubeManager : MonoBehaviour
{
    Camera cam;

    SizeCube sizeCube = null;
    
    [SerializeField] TextMeshProUGUI debugText;

    private void Awake()
    {
        cam = Camera.main;
    }

    public void TestCreateSizeCube()
    {
        float width = Random.Range(0.3f, 1.0f);
        float height = Random.Range(0.3f, 1.0f);
        float depth = Random.Range(0.3f, 1.0f);
        

        CreateSizeCube(width, height, depth);
    }

    public void CreateSizeCube(float width, float height, float depth)
    {
        if (sizeCube == null)
        {
            sizeCube = Instantiate(PrefabManager.Instance.sizeCubePrefab, transform).GetComponent<SizeCube>();
        }
        
        Vector3 position = PlaneManager.Instance.LastHitTestResult?.Position ?? cam.transform.position + cam.transform.forward * 8f;
        Quaternion rotation = PlaneManager.Instance.LastHitTestResult?.Rotation ?? Quaternion.identity;

        
        debugText.SetText("Cam: " + cam.transform.position.ToString() + "\npos: " + position.ToString() + "\nrot: " + rotation.ToString());

        sizeCube.SetSizeCube(position, rotation, width, height, depth);
    }

    public void DisableSizeCube()
    {
        sizeCube.DisableSizeCube();
    }
}
