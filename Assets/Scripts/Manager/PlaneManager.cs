using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class PlaneManager : MonoBehaviour
{
    public static PlaneManager Instance { get; private set; } = null;
    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    PlaneFinderBehaviour mPlaneFinder;
    ContentPositioningBehaviour mContentPositioningBehaviour;
    public GameObject cameraButton;
    public GameObject resetButton;
    

    public Furniture CurrentFurniture => FurnitureManager.Instance.CurrentFurniture;
    //public ObjectController CurrentFurnitureController => FurnitureManager.Instance.CurrentFurniture.Controller;

    //ObjectController[] placeableObjects;
    //List<ObjectController> placedObjects = new List<ObjectController>();
    //public int CurrentObejctIndex { get; private set; } = 0;
    //public ObjectController CurrentObject => placeableObjects[CurrentObejctIndex];

    private void Start()
    {
        mPlaneFinder = FindObjectOfType<PlaneFinderBehaviour>();
        mContentPositioningBehaviour = mPlaneFinder.GetComponent<ContentPositioningBehaviour>();
        //placeableObjects = FindObjectsOfType<ObjectController>();

        mPlaneFinder.HitTestMode = HitTestMode.AUTOMATIC;
    }


    public void HandleAutomaticHitTest(HitTestResult result)
    {
        if (CurrentFurniture == null) return;

        //Debug.Log("Auto - IsPlaced : " + CurrentFurniture.IsPlaced);
        if (!CurrentFurniture.IsPlaced)
        {
            CurrentFurniture.DetachObjectFromAnchor();
            CurrentFurniture.SetNewPosition(result.Position);
            //string s = "";
            //s += "result : " + result.Position.ToString();
            //s += "\n\ntrans : " + CurrentFurniture.transform.position.ToString();
            //s += "\n\nanchor : " + CurrentFurniture.anchorBehaviour.transform.position.ToString();
            //Debug.Log(s);
        }
    }

    public void HandleInteractiveHitTest(HitTestResult result)
    {
        if (result == null)
        {
            Debug.LogError("Invalid hit test result!");
            return;
        }

        if (CurrentFurniture == null) return;

        if (!CurrentFurniture.IsPlaced)
        {
            if (UI_Manager.Instance.IsPointerOverUI()) return;

            Debug.Log("HandleInteractiveHitTest() called.");

            mContentPositioningBehaviour.DuplicateStage = false;

            // With a tap a new anchor is created, so we first check that
            // Status=TRACKED/EXTENDED_TRACKED and StatusInfo=NORMAL before proceeding.
            //if (TrackingStatusIsTrackedAndNormal)
            //{
            //mContentPositioningBehaviour.AnchorStage = mPlacementAnchor;
            mContentPositioningBehaviour.AnchorStage = CurrentFurniture.anchorBehaviour;
            mContentPositioningBehaviour.PositionContentAtPlaneAnchor(result);
            //FurnitureManager.EnableRendererColliderCanvas(CurrentFurniture.gameObject, true);

            // If the product has not been placed in the scene yet, we attach it to the anchor
            // while rotating it to face the camera. Then we activate the content, also
            // enabling rotation input detection.
            // Otherwise, we simply attach the content to the new anchor, preserving its rotation.
            // The placement methods will set the IsPlaced flag to true if the 
            // transform argument is valid and to false if it is null.

            //CurrentObject.SetPlaced(!CurrentObject.IsPlaced);
            //}

            CurrentFurniture.PlaceObjectAtAnchor();

            GameObject[] furnitureWorld = GameObject.FindGameObjectsWithTag("Furniture");
            if (furnitureWorld.Length > 0)
            {
                cameraButton.SetActive(true);
                resetButton.SetActive(true);
            }
            else
            {
                cameraButton.SetActive(false);
                resetButton.SetActive(false);
            }
        }
    }

    //public void HandleAutomaticHitTest(HitTestResult result)
    //{
    //    if (CurrentFurniture == null) return;

    //    Debug.LogError("Auto - IsPlaced : " + CurrentFurnitureController.IsPlaced);
    //    if (!CurrentFurnitureController.IsPlaced)
    //    {
    //        CurrentFurnitureController.DetachProductFromAnchor();
    //        CurrentFurnitureController.transform.position = result.Position;

    //        string s = "";
    //        s += "result : " + result.Position.ToString();
    //        s += "\n\ntrans : " + CurrentFurnitureController.transform.position.ToString();
    //        s += "\n\nanchor : " + CurrentFurnitureController.anchorBehaviour.transform.position.ToString();
    //        Debug.LogError(s);
    //    }
    //}

    //public void HandleInteractiveHitTest(HitTestResult result)
    //{
    //    if (result == null)
    //    {
    //        Debug.LogError("Invalid hit test result!");
    //        return;
    //    }

    //    if (CurrentFurniture == null) return;

    //    Debug.Log("HandleInteractiveHitTest() called.");

    //    mContentPositioningBehaviour.DuplicateStage = false;

    //    // With a tap a new anchor is created, so we first check that
    //    // Status=TRACKED/EXTENDED_TRACKED and StatusInfo=NORMAL before proceeding.
    //    //if (TrackingStatusIsTrackedAndNormal)
    //    //{
    //    //mContentPositioningBehaviour.AnchorStage = mPlacementAnchor;
    //    mContentPositioningBehaviour.AnchorStage = CurrentFurnitureController.anchorBehaviour;
    //    mContentPositioningBehaviour.PositionContentAtPlaneAnchor(result);
    //    ObjectController.EnableRendererColliderCanvas(CurrentFurnitureController.gameObject, true);

    //    // If the product has not been placed in the scene yet, we attach it to the anchor
    //    // while rotating it to face the camera. Then we activate the content, also
    //    // enabling rotation input detection.
    //    // Otherwise, we simply attach the content to the new anchor, preserving its rotation.
    //    // The placement methods will set the IsPlaced flag to true if the 
    //    // transform argument is valid and to false if it is null.

    //    //CurrentObject.SetPlaced(!CurrentObject.IsPlaced);
    //    //}

    //    CurrentFurnitureController.PlaceProductAtAnchor();
    //}
}
