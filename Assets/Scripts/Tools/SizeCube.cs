using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class SizeCube : MonoBehaviour
{
    SizeCubeManager sizeCubeManager = null;

    Camera cam;

    [SerializeField] Transform anchorTransform;

    [SerializeField] Transform cubeTransform;

    [SerializeField] Transform widthTextTransform;
    [SerializeField] Transform heightTextTransform;
    [SerializeField] Transform depthTextTransform;

    TextMeshPro widthText;
    TextMeshPro heightText;
    TextMeshPro depthText;

    const float MinTextResizeSize = 0.5f;

    Coroutine textFacingCoroutine = null;
    
    public bool IsActive => gameObject.activeSelf;

    private void Awake()
    {
        cam = Camera.main;

        widthText = widthTextTransform.GetComponentInChildren<TextMeshPro>();
        heightText = heightTextTransform.GetComponentInChildren<TextMeshPro>();
        depthText = depthTextTransform.GetComponentInChildren<TextMeshPro>();
    }

    private void OnDestroy()
    {
        sizeCubeManager.SizeCubeActivateEvent.Invoke(false);
    }

    public void SetSizeCubeManager(SizeCubeManager sizeCubeManager)
    {
        this.sizeCubeManager = sizeCubeManager;
    }

    /*
     * height
     * |
     * |   /
     * |  /  depth
     * | /
     * |/
     * -------------
     *         width
     */
    public void SetSizeCube(Transform anchor, Vector3 position, Quaternion rotation, float width, float height, float depth)
    {
        gameObject.SetActive(true);

        transform.SetParent(anchor);
        transform.localPosition = Vector3.zero;
        transform.localRotation = rotation;

        anchorTransform.localPosition = position;

        cubeTransform.localScale = new Vector3(width, height, depth);
        widthTextTransform.localPosition = new Vector3(0, -height / 2, -depth / 2);
        heightTextTransform.localPosition = new Vector3(-width / 2, 0, -depth / 2);
        depthTextTransform.localPosition = new Vector3(width / 2, -height / 2, 0);

        float maxSize = Mathf.Max(width, height, depth);
        if (maxSize < MinTextResizeSize)
        {
            float resize = Mathf.Lerp(0.3f, 1.0f, maxSize / MinTextResizeSize);
            Vector3 newScale = new Vector3(resize, resize, resize);
            widthTextTransform.localScale = newScale;
            heightTextTransform.localScale = newScale;
            depthTextTransform.localScale = newScale;
        }

        widthText.SetText(GetMilimeterStringFromMeter(width));
        heightText.SetText(GetMilimeterStringFromMeter(height));
        depthText.SetText(GetMilimeterStringFromMeter(depth));

        if (textFacingCoroutine != null)
        {
            StopCoroutine(textFacingCoroutine);
            textFacingCoroutine = null;
        }
        textFacingCoroutine = StartCoroutine(TextFacingToCamera());

        sizeCubeManager.SizeCubeActivateEvent.Invoke(true);
    }

    public void SetSizeCube(Transform anchor, Vector3 position, Vector3 size)
    {
        SetSizeCube(anchor, position, Quaternion.identity, size.x, size.y, size.z);
    }

    public void SetSizeCube(Vector3 position, Quaternion rotation, float width, float height, float depth)
    {
        SetSizeCube(sizeCubeManager.SizeCubeAnchor, position, rotation, width, height, depth);
    }

    IEnumerator TextFacingToCamera()
    {
        while (true)
        {
            widthTextTransform.LookAt(cam.transform);
            heightTextTransform.LookAt(cam.transform);
            depthTextTransform.LookAt(cam.transform);
            yield return null;
        }
    }
    

    public void DisableSizeCube()
    {
        if (textFacingCoroutine != null)
        {
            StopCoroutine(textFacingCoroutine);
            textFacingCoroutine = null;
        }

        gameObject.SetActive(false);
        
        sizeCubeManager.SizeCubeActivateEvent.Invoke(false);
    }

    public static float MeterToMilimeter(float meter)
    {
        return meter * 1000f;
    }

    public static float MilimeterToMeter(float milimeter)
    {
        return milimeter / 1000f;
    }

    public static string GetMilimeterString(int milimeter)
    {
        return string.Format("{0}mm", milimeter);
    }

    public static string GetMilimeterStringFromMeter(float meter)
    {
        return GetMilimeterString((int)MeterToMilimeter(meter));
    }
}
