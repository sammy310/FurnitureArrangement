using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
public class TouchHandler : MonoBehaviour
{
    public static TouchHandler Instance { get; private set; } = null;

    Mode touchMode;

    GameObject target;
    public Sprite[] bookmarkSprite;
    public GameObject editEndButton;
    public GameObject trashButton;
    public GameObject bookmarkButton;
    public GameObject furnitureText;
    public GameObject cameraButton;
    public GameObject resetButton;
    public UI_FurnitureSelectUI furnitureSelectUI;
    public Data_Furniture furnitureDataInfo;
    public GameObject editButton;

    public GameObject floorPrefab;
    public GameObject mainUI;
    public TextMeshProUGUI furnitureName;

    GameObject floorClone;
    Collider[] collider1;
    Collider[] collider2;

    bool mIsFirstFrameWithTwoTouches;
    float mCachedTouchAngle;
    Vector3 mCachedAugmentationRotation;

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
        if (Input.touchCount == 2)
        {
            GetTouchAngleAndDistance(Input.GetTouch(0), Input.GetTouch(1), out var currentTouchAngle);

            if (mIsFirstFrameWithTwoTouches)
            {
                mCachedTouchAngle = currentTouchAngle;
                mIsFirstFrameWithTwoTouches = false;
            }

            var angleDelta = currentTouchAngle - mCachedTouchAngle;

            target.transform.localEulerAngles = mCachedAugmentationRotation - new Vector3(0, angleDelta * 3f, 0);
        }
        else if (Input.touchCount == 1)
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
        if (Input.touchCount < 1)
        {
            UItouched = false;
        }
        if (Input.touchCount < 2)
        {
            mCachedAugmentationRotation = target.transform.localEulerAngles;
            mIsFirstFrameWithTwoTouches = true;
        }
    }

    void GetTouchAngleAndDistance(Touch firstTouch, Touch secondTouch, out float touchAngle)
    {
        var diffY = firstTouch.position.y - secondTouch.position.y;
        var diffX = firstTouch.position.x - secondTouch.position.x;
        touchAngle = Mathf.Atan2(diffY, diffX) * Mathf.Rad2Deg;
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
                editButton.SetActive(true);
            }
        }
        else
        {
            editButton.SetActive(false);
            target = null;
        }
    }

    public void EditButtonClick()
    {
        touchMode = Mode.EditMode;
        mCachedAugmentationRotation = target.transform.localEulerAngles;

        mainUI.SetActive(false);

        furnitureName.text = target.name.Replace("(Clone)","");
        foreach (var furnitureinfo in furnitureDataInfo.furnitureInfo)
        {
            if (furnitureinfo.furnitureName == furnitureName.text)
            {
                if (furnitureSelectUI.BookmarkCheck(furnitureinfo.furnitureName))
                {
                    Debug.Log("가구 북마크 진입");
                    bookmarkButton.GetComponent<Image>().sprite = bookmarkSprite[1];
                }
                else
                {
                    Debug.Log("가구 북마크 진입");
                    bookmarkButton.GetComponent<Image>().sprite = bookmarkSprite[0];
                }
            }
        }
        Collider[] collider1 = target.GetComponents<Collider>();
        Collider[] collider2 = target.GetComponentsInChildren<Collider>();
        for (int i =0; i< collider1.Length;i++)
        {
            collider1[i].enabled = false;
        }
        for (int i = 0; i < collider2.Length; i++)
        {
            collider2[i].enabled = false;
        }
        CreateFloor(target.transform.position);
        editButton.SetActive(false);
        editEndButton.SetActive(true);
        trashButton.SetActive(true);
        bookmarkButton.SetActive(true);
        furnitureText.SetActive(true);
    }
    public void CreateFloor(Vector3 targetPosition)
    {
        floorClone = Instantiate(floorPrefab, targetPosition, Quaternion.identity);
    }

    public void EditEndButtonClick()
    {
        touchMode = Mode.SelectMode;
        Collider[] collider1 = target.GetComponents<Collider>();
        Collider[] collider2 = target.GetComponentsInChildren<Collider>();
        for (int i = 0; i < collider1.Length; i++)
        {
            collider1[i].enabled = true;
        }
        for (int i = 0; i < collider2.Length; i++)
        {
            collider2[i].enabled = true;
        }
        target = null;
        Destroy(floorClone);
        mainUI.SetActive(true);
        trashButton.SetActive(false);
        bookmarkButton.SetActive(false);
        furnitureText.SetActive(false);
        editEndButton.SetActive(false);
    }
    public void TrashButtonClick()
    {
        Destroy(target.gameObject);
        GameObject[] furnitureWorld = GameObject.FindGameObjectsWithTag("Furniture");
        if (furnitureWorld.Length > 1)
        {
            cameraButton.SetActive(true);
            resetButton.SetActive(true);
        }
        else
        {
            cameraButton.SetActive(false);
            resetButton.SetActive(false);
        }
        EditEndButtonClick();
    }
}
