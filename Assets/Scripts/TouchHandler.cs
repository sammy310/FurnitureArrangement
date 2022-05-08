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
    ObjectMesh targetMesh;

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
    private Data_FurnitureInfo targetInfo;
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

    int floorLayer = 0;

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

        floorLayer = 1 << LayerMask.NameToLayer("Floor");
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
                if (Input.touchCount == 1) {
                    Touch touch = Input.GetTouch(0);
                    if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId) == false)
                    {
                        if (touch.phase == TouchPhase.Began)
                        {
                            SelectObject(ref target);
                        }
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
            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId) == false && EventSystem.current.IsPointerOverGameObject(Input.GetTouch(1).fingerId) == false)
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
        }
        else if (Input.touchCount == 1)
        {
            if (!UItouched && EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId) == false)
            {

                Ray touchRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(touchRay, out var cameraToPlaneHit, 1000f, floorLayer) && cameraToPlaneHit.collider.gameObject.tag == "Floor")
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
            GameObject rayObject = null;
            rayObject = hit.collider.gameObject;
            while (rayObject.tag != "Furniture")
            {
                rayObject = rayObject.transform.parent.gameObject;
            }
            if (target != null && target.tag == "Furniture" && target.gameObject != rayObject.gameObject)
            {
                Outline outline = target.GetComponent<Outline>();
                if (outline != null)
                    outline.OutlineWidth = 0.0f;
                target = null;
                targetMesh = null;
            }
            if (rayObject.tag == "Furniture")
            {
                target = rayObject.gameObject;
                targetMesh = target.GetComponent<ObjectMesh>();
                Outline outline = target.GetComponent<Outline>();
                if (outline != null)
                    outline.OutlineWidth = 2.0f;
                UItouched = true;
                touchMode = Mode.EditMode;
                mCachedAugmentationRotation = target.transform.localEulerAngles;

                mainUI.SetActive(false);

                foreach (var furnitureinfo in furnitureDataInfo.furnitureInfo)
                {
                    if (furnitureinfo.furnitureObject.name == target.name.Replace("(Clone)", ""))
                    {
                        targetInfo = furnitureinfo;
                        furnitureName.text = furnitureinfo.furnitureName;
                        if (UI_Manager.Instance.FurnitureSelectUI.GetFurnitureSelectItem(furnitureinfo).IsBookmarked)
                        {
                            bookmarkButton.GetComponent<Image>().sprite = bookmarkSprite[1];
                        }
                        else
                        {
                            bookmarkButton.GetComponent<Image>().sprite = bookmarkSprite[0];
                        }
                    }
                }
                Collider[] collider1 = target.GetComponents<Collider>();
                Collider[] collider2 = target.GetComponentsInChildren<Collider>();
                for (int i = 0; i < collider1.Length; i++)
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

                if (targetMesh != null)
                    FurnitureManager.Instance.SetFurniture(targetMesh.Controller as Furniture);
            }
        }
        else
        {
            if (target)
            {
                Outline outline = target.GetComponent<Outline>();
                if (outline != null)
                    outline.OutlineWidth = 0.0f;
            }
            editButton.SetActive(false);
            target = null;
            targetMesh = null;
        }
    }

    //public void EditButtonClick()
    //{
    //    UItouched = true;
    //    touchMode = Mode.EditMode;
    //    mCachedAugmentationRotation = target.transform.localEulerAngles;

    //    mainUI.SetActive(false);

    //    foreach (var furnitureinfo in furnitureDataInfo.furnitureInfo)
    //    {
    //        if (furnitureinfo.furnitureObject.name == target.name.Replace("(Clone)", ""))
    //        {
    //            targetInfo = furnitureinfo;
    //            furnitureName.text = furnitureinfo.furnitureName;
    //            if (UI_Manager.Instance.FurnitureSelectUI.GetFurnitureSelectItem(furnitureinfo).IsBookmarked)
    //            {
    //                bookmarkButton.GetComponent<Image>().sprite = bookmarkSprite[1];
    //            }
    //            else
    //            {
    //                bookmarkButton.GetComponent<Image>().sprite = bookmarkSprite[0];
    //            }
    //        }
    //    }
    //    Collider[] collider1 = target.GetComponents<Collider>();
    //    Collider[] collider2 = target.GetComponentsInChildren<Collider>();
    //    for (int i =0; i< collider1.Length;i++)
    //    {
    //        collider1[i].enabled = false;
    //    }
    //    for (int i = 0; i < collider2.Length; i++)
    //    {
    //        collider2[i].enabled = false;
    //    }
    //    CreateFloor(target.transform.position);
    //    editButton.SetActive(false);
    //    editEndButton.SetActive(true);
    //    trashButton.SetActive(true);
    //    bookmarkButton.SetActive(true);
    //    furnitureText.SetActive(true);

    //    if (targetMesh != null)
    //        FurnitureManager.Instance.SetFurniture(targetMesh.Controller as Furniture);
    //}

    public void BookmarkButtonClick()
    {
        UItouched = true;
        UI_Manager.Instance.FurnitureSelectUI.GetFurnitureSelectItem(targetInfo).ToggleBookmark();
        if (UI_Manager.Instance.FurnitureSelectUI.GetFurnitureSelectItem(targetInfo).IsBookmarked)
        {
            bookmarkButton.GetComponent<Image>().sprite = bookmarkSprite[1];
        }
        else
        {
            bookmarkButton.GetComponent<Image>().sprite = bookmarkSprite[0];
        }
    }
    public void CreateFloor(Vector3 targetPosition)
    {
        floorClone = Instantiate(floorPrefab, targetPosition, Quaternion.identity);
    }

    public void EditEndButtonClick()
    {
        UItouched = true;
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
        Outline outline = target.GetComponent<Outline>();
        if (outline != null)
            outline.OutlineWidth = 0.0f;
        target = null;
        Destroy(floorClone);
        mainUI.SetActive(true);
        trashButton.SetActive(false);
        bookmarkButton.SetActive(false);
        furnitureText.SetActive(false);
        editEndButton.SetActive(false);

        FurnitureManager.Instance.DisableFurniture();
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
