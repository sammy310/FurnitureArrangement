using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SizeCube : MonoBehaviour
{
    Camera cam;

    [SerializeField] Transform anchorTransform;

    [SerializeField] Transform cubeTransform;

    [SerializeField] Transform widthTextTransform;
    [SerializeField] Transform heightTextTransform;
    [SerializeField] Transform depthTextTransform;

    TextMeshPro widthText;
    TextMeshPro heightText;
    TextMeshPro depthText;

    Coroutine textFacingCoroutine = null;

    private void Awake()
    {
        cam = Camera.main;

        widthText = widthTextTransform.GetComponentInChildren<TextMeshPro>();
        heightText = heightTextTransform.GetComponentInChildren<TextMeshPro>();
        depthText = depthTextTransform.GetComponentInChildren<TextMeshPro>();
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
    public void SetSizeCube(Vector3 position, Quaternion rotation, float width, float height, float depth)
    {
        gameObject.SetActive(true);

        transform.position = position;
        transform.rotation = rotation;

        anchorTransform.localPosition = new Vector3(0, height / 2, 0);

        cubeTransform.localScale = new Vector3(width, height, depth);
        widthTextTransform.localPosition = new Vector3(0, -height / 2, -depth / 2);
        heightTextTransform.localPosition = new Vector3(-width / 2, 0, -depth / 2);
        depthTextTransform.localPosition = new Vector3(width / 2, -height / 2, 0);
        
        widthText.SetText(GetCentimeterStringFromMeter(width));
        heightText.SetText(GetCentimeterStringFromMeter(height));
        depthText.SetText(GetCentimeterStringFromMeter(depth));

        if (textFacingCoroutine != null)
        {
            StopCoroutine(textFacingCoroutine);
            textFacingCoroutine = null;
        }
        textFacingCoroutine = StartCoroutine(TextFacingToCamera());
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
    }

    public static float MeterToCM(float meter)
    {
        return meter * 100f;
    }

    public string GetCentimeterStringFromMeter(float meter)
    {
        return string.Format("{0}{1}", (int)MeterToCM(meter), "cm");
    }
}
