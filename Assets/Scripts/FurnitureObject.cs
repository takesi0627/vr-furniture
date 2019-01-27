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

    bool isTarget;
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (eventData.pointerDrag == gameObject)
        {
            distance = (transform.position - Camera.main.transform.position).magnitude;
            previousFramePosition = GetPointerPositionOnSphere(distance);
            isTarget = true;
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
        isTarget = false;
    }

    public void OnDrop(PointerEventData eventData)
    {
        isTarget = false;
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

    Vector3 GetPointerPositionOnSphere(float radius)
    {
        return Geometrics.GetIntersectionPointOnSphere(inputModule.rayTransform.position,
                                                       inputModule.rayTransform.forward, radius);
    }

    public void ScaleUP() { 
        if (transform.localScale.x < 2.0f)
           transform.localScale += 0.2f * Vector3.one; 
    }
    public void ScaleDown() { 
        if (transform.localScale.x > 0.2f)
            transform.localScale -= 0.2f * Vector3.one; 
    }

    // Use this for initialization
    void Start()
    {
        inputModule = FindObjectOfType<OVRInputModule>();
        if (inputModule == null)
        {
            Debug.LogError("[FurnitureObject.cs] Cannot find input module");
            // TODO instantiate one
        }

        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isTarget)
        {
            Vector2 axis = OVRInput.Get(OVRInput.Axis2D.PrimaryTouchpad).normalized;
            if (!axis.y.Equals(0))
            {
                float scale = axis.y > 0 ? 0.2f : -0.2f;
                transform.localScale = Mathf.Clamp(transform.localScale.x + scale, 0.1f, 2.0f) * Vector3.one;
            }
        }
	}
}
