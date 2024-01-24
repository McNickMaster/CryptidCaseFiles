using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TackConnect : MonoBehaviour
{
    public LineRenderer line;
    public Transform pos1;
    public Transform pos2;

    private void Start()
    {
        line.positionCount = 2;
    }

    private void Update()
    {
        line.SetPosition(0, pos1.position);
        line.SetPosition(1, pos2.position);
    }

    /*private LineRenderer lineRend;
    private bool hasClicked;
    private Vector3 sp, ep;
    
    void Awake()
    {
        hasClicked = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1) && !hasClicked)
        {
            Vector3 sp = Input.mousePosition;
            hasClicked = true;
        }
        if (Input.GetMouseButtonDown(1) && hasClicked)
        {
            Vector3 ep = Input.mousePosition;
            hasClicked = false;
            lineRend = Instantiate();
            lineRend.SetPosition(0, sp);
            lineRend.SetPosition(1, ep);
        }
    }*/
}

    /*void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        Vector3[] positions = new Vector3[3] { new Vector3(0, 0, 0), new Vector3(-1, 1, 0), new Vector3(1, 1, 0) };
        DrawTriangle(positions);
    }

    void DrawTriangle(Vector3[] vertexPositions)
    {
        lineRenderer.positionCount = 3;
        lineRenderer.SetPositions(vertexPositions);
    }*/

/**\

/*public class TackConnect : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool _connecting;
    private Vector2 offset;

    public void OnPointerDown(PointerEventData data)
    {
        if (PointerEventData.Inpu == PointerEventData.InputButton.Right)
        {
            _connecting = true;
        }
    }

    public void OnPointerUp(PointerEventData data)
    {
        _connecting = false;
    }

    private void Update()
    {
        if (_connecting == true)
            transform.position = new Vector2(Input.mousePosition.x + offset.x, Input.mousePosition.y + offset.y);
    }
}*/
