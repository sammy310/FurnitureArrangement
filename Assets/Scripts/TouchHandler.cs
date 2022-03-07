using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class TouchHandler : MonoBehaviour
{
    Mode touchMode;

    GameObject target;
    public GameObject editEndButton;
    public GameObject editButton;
    public Slider scaleSlider;
    public Slider rotateSlider;
    public GameObject floorPrefab;
    public GameObject mainUI1;
    public GameObject mainUI2;
    GameObject floorClone;


    float FirstScale;
    Vector3 FirstRotation;

    bool UItouched;

    public enum Mode
    {
        ReadyMode,
        SelectMode,
        EditMode
    }
    void Start()
    {
        touchMode = Mode.SelectMode;
        target = null;
        UItouched = false;
    }
    public void SetTouchMode(Mode i)
    {
        touchMode = i;
    }
    // Update is called once per frame
    void Update()
    {
        switch(touchMode)
        {
            case Mode.SelectMode:
                if (Input.GetMouseButtonDown(0)) {
                    if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId) == false)
                    {
                        SelectObject(ref target);
                    }
                }
                break;

            case Mode.EditMode:
                FurnitureControl();
                break;
        }
    }


    void FurnitureControl()
    {
        if (Input.touchCount == 1)
        {
            if (!UItouched && EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId) == false)
            {

                Ray touchRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(touchRay, out var cameraToPlaneHit) && cameraToPlaneHit.collider.gameObject.tag == "Floor")
                {
                    target.transform.position = cameraToPlaneHit.point;
                }
            }
        }
        else if (Input.touchCount < 1)
        {
            UItouched = false;
        }
    }

    void SelectObject(ref GameObject target)
    {
        RaycastHit hit;

        Ray touchRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(touchRay, out hit))
        {
            if (target != null && target.tag == "Furniture" && target.gameObject != hit.collider.gameObject)
            {
                target = null;
            }
            if (hit.collider.tag == "Furniture")
            {
                target = hit.collider.gameObject;
                FirstScale = hit.collider.gameObject.transform.localScale.x;
                FirstRotation = hit.collider.gameObject.transform.localEulerAngles;
                editButton.SetActive(true);
            }
        }
        else
        {
            editButton.SetActive(false);
            target = null;
        }
    }

    public void ScaleScroll()
    {
        target.transform.localScale = new Vector3(scaleSlider.value, scaleSlider.value, scaleSlider.value);
        UItouched = true;
    }

    public void rotateScroll()
    {
        target.transform.localEulerAngles = new Vector3(0, rotateSlider.value, 0);
        UItouched = true;
    }

    public void EditButtonClick()
    {
        touchMode = Mode.EditMode;
        editButton.SetActive(false);
        editEndButton.SetActive(true);
        rotateSlider.gameObject.SetActive(true);
        scaleSlider.gameObject.SetActive(true);
        scaleSlider.minValue = target.transform.lossyScale.x;
        scaleSlider.maxValue = target.transform.lossyScale.x+1.0f;
        scaleSlider.value = scaleSlider.minValue;
        rotateSlider.value = rotateSlider.minValue;
        CreateFloor(target.transform.position);
        mainUI1.SetActive(false);
        mainUI2.SetActive(false);
    }
    public void CreateFloor(Vector3 targetPosition)
    {
        floorClone = Instantiate(floorPrefab, targetPosition, Quaternion.identity);
    }
    public void EditEndButtonClick()
    {
        touchMode = Mode.SelectMode;
        editEndButton.SetActive(false);
        rotateSlider.gameObject.SetActive(false);
        scaleSlider.gameObject.SetActive(false);
        target = null;
        Destroy(floorClone);
        mainUI1.SetActive(true);
        mainUI2.SetActive(true);
    }
}
