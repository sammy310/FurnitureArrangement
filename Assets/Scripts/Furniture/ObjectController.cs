using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

[RequireComponent(typeof(AnchorBehaviour))]
public abstract class ObjectController : MonoBehaviour
{
    AnchorBehaviour anchor;
    public AnchorBehaviour anchorBehaviour => anchor;
    
    public Transform ObjectTransform { get; private set; } = null;

    public ObjectMesh MeshObject { get; private set; } = null;

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
        if (ObjectTransform != null && ObjectTransform.parent != transform)
        {
            Destroy(ObjectTransform.gameObject);
        }
    }
    
    public void SetNewPosition(Vector3 newPosition)
    {
        if (ObjectTransform == null) return;

        ObjectTransform.position = newPosition;
    }

    protected void SetObject(GameObject newObject)
    {
        ObjectTransform = newObject.transform;

        MeshObject = ObjectTransform.GetComponent<ObjectMesh>();
        if (MeshObject == null)
        {
            Debug.LogError("ObjectMesh cannot found!!");
        }
        else
        {
            MeshObject.SetController(this);
        }

        renderers = ObjectTransform.GetComponentsInChildren<Renderer>();
        DetachObjectFromAnchor();

        OnSetObject(newObject);
    }

    protected virtual void OnSetObject(GameObject newObject)
    {

    }

    public void PlaceObjectAtAnchor()
    {
        if (ObjectTransform == null) return;

        ObjectTransform.SetParent(anchor.transform, true);
        ObjectTransform.localPosition = Vector3.zero;
        
        IsPlaced = true;
    }

    public void DetachObjectFromAnchor()
    {
        if (ObjectTransform == null) return;

        ObjectTransform.SetParent(null);

        ObjectTransform.position = Vector3.zero;
        ObjectTransform.localEulerAngles = Vector3.zero;
        ObjectTransform.localScale = Vector3.one;

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
