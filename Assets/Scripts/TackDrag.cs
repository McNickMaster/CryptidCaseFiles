using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TackDrag : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool _dragging;
    private Vector2 offset;

    public void OnPointerDown(PointerEventData data)
    {
        _dragging = true;
        //Vector2 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        offset = transform.position = new Vector2(transform.position.x - Input.mousePosition.x, transform.position.y - Input.mousePosition.y);
        transform.SetAsLastSibling();
    }

    public void OnPointerUp(PointerEventData data)
    {
        _dragging = false;
    }

    private void Update()
    {
        if (_dragging == true)
            transform.position = new Vector3(Input.mousePosition.x + offset.x, Input.mousePosition.y + offset.y);
    }
}
