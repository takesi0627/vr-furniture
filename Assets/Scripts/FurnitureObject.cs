using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using hekira.Utilities;

public class FurnitureObject : MonoBehaviour, 
    IPointerDownHandler, IPointerEnterHandler, IPointerUpHandler,
    IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler 
{
    OVRInputModule inputModule;
    Vector3 previousFramePosition;
    float distance; // obeject distance from camera
    Rigidbody rb;

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (eventData.pointerDrag == gameObject)
        {
            distance = (transform.position - Camera.main.transform.position).magnitude;
            previousFramePosition = GetPointerPositionOnSphere(distance);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 currentPointerPosition = GetPointerPositionOnSphere(distance);
        Vector3 moveVector = currentPointerPosition - previousFramePosition;

        rb.MovePosition(currentPointerPosition);
        previousFramePosition = currentPointerPosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
    }

    public void OnDrop(PointerEventData eventData)
    {
    }

    public void OnPointerDown(PointerEventData eventData)
    {
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // TODO Gizmo triggered
    }

    public void OnPointerUp(PointerEventData eventData)
    {
    }

    Vector3 GetPointerPositionOnSphere (float radius) {
        return Geometrics.GetIntersectionPointOnSphere(inputModule.rayTransform.position,
                                                       inputModule.rayTransform.forward, radius);
    }

    // Use this for initialization
    void Start () {
        inputModule = FindObjectOfType<OVRInputModule> ();
        if (inputModule == null)
        {
            Debug.LogError("[FurnitureObject.cs] Cannot find input module");
            // TODO instantiate one
        }

        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
	}
}
