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
    public GameObject ver1Button;
    public GameObject ver2Button;
    public Slider scaleSlider;
    public Slider rotateSlider;
    bool isFirstTouch = true;

    float FirstDistance = 0.0f;
    float FirstAngle = 0.0f;

    float CurrentDistance = 0.0f;
    float CurrentAngle = 0.0f;

    float FirstScale;
    Vector3 FirstRotation;

    bool verChange;
    bool UItouched;

    enum Mode
    {
        SelectMode,
        EditMode
    }
    void Start()
    {
        touchMode = Mode.SelectMode;
        target = null;
        verChange = false;
        UItouched = false;
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
                if (!verChange)
                {
                    Ver1Control();
                }
                else
                {
                    Ver2Control();
                }
                break;
        }
    }

    void Ver1Control()
    {
        if (Input.touchCount < 2)
        {
            FirstScale = target.transform.localScale.x;
            FirstRotation = target.transform.localEulerAngles;
            isFirstTouch = true;
        }
        if (Input.touchCount == 1)
        {
            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId) == false)
            {
                Ray touchRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(touchRay, out var cameraToPlaneHit) && cameraToPlaneHit.collider.gameObject.tag == "Floor")
                {
                    target.transform.position = cameraToPlaneHit.point;
                }
            }
        }
        else if (Input.touchCount == 2)
        {
            CurrentDistance = Vector2.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position);
            var diffY = Input.GetTouch(0).position.y - Input.GetTouch(1).position.y;
            var diffX = Input.GetTouch(0).position.x - Input.GetTouch(1).position.x;
            CurrentAngle = Mathf.Atan2(diffY, diffX) * Mathf.Rad2Deg;

            if (isFirstTouch)
            {
                FirstDistance = CurrentDistance;
                FirstAngle = CurrentAngle;
                isFirstTouch = false;
            }

            var angleDelta = CurrentAngle - FirstAngle;
            var scaleMultiplier = CurrentDistance / FirstDistance;
            var scaleAmount = FirstScale * scaleMultiplier;
            var scaleAmountClamped = Mathf.Clamp(scaleAmount, 0.1f, 2.0f);

            target.transform.localEulerAngles = FirstRotation - new Vector3(0, angleDelta * 3f, 0);
            target.transform.localScale = new Vector3(scaleAmountClamped, scaleAmountClamped, scaleAmountClamped);
        }
    }

    void Ver2Control()
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
        if (target != null && target.tag == "Cube" && target.gameObject != hit.collider.gameObject)
        {
            target.GetComponent<Renderer>().material.SetVector("_OutlineColor", new Vector4(255f, 255f, 255f, 0f));
            target = null;
        }
        if (hit.collider.tag == "Cube")
        {
            target = hit.collider.gameObject;
            target.GetComponent<Renderer>().material.SetVector("_OutlineColor", new Vector4(255f, 255f, 255f, 255f));
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

    public void ButtonVer1()
    {
        verChange = false;
        scaleSlider.gameObject.SetActive(false);
        rotateSlider.gameObject.SetActive(false);
    }

    public void ButtonVer2()
    {
        verChange = true;
        scaleSlider.gameObject.SetActive(true);
        rotateSlider.gameObject.SetActive(true);
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
        ver1Button.SetActive(true);
        ver2Button.SetActive(true);
        editEndButton.SetActive(true);
        target.GetComponent<BoxCollider>().enabled = false;
    }

    public void EditEndButtonClick()
    {
        touchMode = Mode.SelectMode;
        ver1Button.SetActive(false);
        ver2Button.SetActive(false);
        editEndButton.SetActive(false);
        rotateSlider.gameObject.SetActive(false);
        scaleSlider.gameObject.SetActive(false);
        verChange = false;
        target.GetComponent<BoxCollider>().enabled = true;
        target.GetComponent<Renderer>().material.SetVector("_OutlineColor", new Vector4(255f, 255f, 255f, 0f));
        target = null;
    }
}
