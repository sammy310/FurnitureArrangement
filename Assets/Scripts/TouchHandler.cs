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
    bool isFirstTouch = true;

    float FirstDistance = 0.0f;
    float FirstAngle = 0.0f;

    float CurrentDistance = 0.0f;
    float CurrentAngle = 0.0f;

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

        Physics.Raycast(touchRay, out hit);
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
    }

    public void EditEndButtonClick()
    {
        touchMode = Mode.SelectMode;
        editEndButton.SetActive(false);
        rotateSlider.gameObject.SetActive(false);
        scaleSlider.gameObject.SetActive(false);
        target = null;
    }
}
