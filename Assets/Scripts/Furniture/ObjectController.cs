using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

[RequireComponent(typeof(AnchorBehaviour))]
public abstract class ObjectController : MonoBehaviour
{
    AnchorBehaviour anchor;
    public AnchorBehaviour anchorBehaviour => anchor;
    
    Transform objectTransform = null;
    Renderer[] renderers = null;

    public bool IsPlaced { get; private set; } = false;

    protected virtual void Awake()
    {
        anchor = GetComponentInParent<AnchorBehaviour>();
    }

    private void LateUpdate()
    {
        if (!IsPlaced)
        {
            EnableRenderer();
        }
    }

    private void OnDestroy()
    {
        if (objectTransform != null && objectTransform.parent != transform)
        {
            Destroy(objectTransform.gameObject);
        }
    }
    
    public void SetNewPosition(Vector3 newPosition)
    {
        if (objectTransform == null) return;

        objectTransform.position = newPosition;
    }

    protected void SetObject(GameObject newObject)
    {
        objectTransform = newObject.transform;
        renderers = objectTransform.GetComponentsInChildren<Renderer>();
        DetachObjectFromAnchor();
    }

    public void PlaceObjectAtAnchor()
    {
        if (objectTransform == null) return;

        objectTransform.SetParent(anchor.transform, true);
        objectTransform.localPosition = Vector3.zero;
        
        IsPlaced = true;
    }

    public void DetachObjectFromAnchor()
    {
        if (objectTransform == null) return;

        objectTransform.SetParent(null);

        objectTransform.position = Vector3.zero;
        objectTransform.localEulerAngles = Vector3.zero;
        objectTransform.localScale = Vector3.one;

        IsPlaced = false;
    }

    void EnableRenderer()
    {
        foreach (var mesh in renderers)
        {
            mesh.enabled = true;
        }
    }
}
