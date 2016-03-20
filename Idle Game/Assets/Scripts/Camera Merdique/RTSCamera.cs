using UnityEngine;
using System.Collections;

// Code récupérer bien dégueulasse
public class RTSCamera : MonoBehaviour
{
    [SerializeField]
    private float ScrollSpeed = 15;
    [SerializeField]
    private float ScrollEdge = 0.01f;

    [SerializeField]
    private float PanSpeed = 10;
    [SerializeField]
    private Vector2 ZoomRange = new Vector2(-5, 5);
    private float CurrentZoom = 0;
    private float ZoomZpeed = 1;
    private float ZoomRotation = 1;
    private Vector3 InitPos;
    private Vector3 InitRotation;
     
    void Start()
    {
        InitPos = transform.position;
        InitRotation = transform.eulerAngles;
    }

    void Update()
    {
        this.Pan();
        this.ZoomInZoomOut();
    }

    private void ZoomInZoomOut()
    {
        CurrentZoom -= Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * 1000 * ZoomZpeed;
        CurrentZoom = Mathf.Clamp(CurrentZoom, ZoomRange.x, ZoomRange.y);

        transform.SetPositionY(transform.position.y - (transform.position.y - (InitPos.y + CurrentZoom)) * 0.1f);
        transform.SetRotationX(transform.eulerAngles.x - (transform.eulerAngles.x - (InitRotation.x + CurrentZoom * ZoomRotation)) * 0.1f);
    }

    private void Pan()
    {
        if (Input.GetKey("mouse 2"))
        {
            transform.Translate(Vector3.right * Time.deltaTime * PanSpeed * (Input.mousePosition.x - Screen.width * 0.5f) / (Screen.width * 0.5f), Space.World);
            transform.Translate(Vector3.forward * Time.deltaTime * PanSpeed * (Input.mousePosition.y - Screen.height * 0.5f) / (Screen.height * 0.5f), Space.World);
        }
        else
        {
            if (Input.GetKey("d") || Input.GetKey("right") || Input.mousePosition.x >= Screen.width * (1 - ScrollEdge))
                transform.Translate(Vector3.right * Time.deltaTime * ScrollSpeed, Space.World);
            else if (Input.GetKey("a") || Input.GetKey("left") || Input.mousePosition.x <= Screen.width * ScrollEdge)
                transform.Translate(Vector3.right * Time.deltaTime * -ScrollSpeed, Space.World);

            if (Input.GetKey("w") || Input.GetKey("up") || Input.mousePosition.y >= Screen.height * (1 - ScrollEdge))
                transform.Translate(Vector3.forward * Time.deltaTime * ScrollSpeed, Space.World);
            else if (Input.GetKey("s") || Input.GetKey("down") || Input.mousePosition.y <= Screen.height * ScrollEdge)
                transform.Translate(Vector3.forward * Time.deltaTime * -ScrollSpeed, Space.World);
        }
    }

}
