using UnityEngine;
using System.Collections;

public class RTSCamera : MonoBehaviour
{
    float ScrollSpeed = 15;
    float ScrollEdge = 0.01f;
     
    private int HorizontalScroll = 1;
    private int VerticalScroll = 1;
    private int DiagonalScroll = 1;
     
    private float PanSpeed = 10;
    private Vector2 ZoomRange = new Vector2(-5,5);
    private float CurrentZoom = 0;
    private float ZoomZpeed = 1;
    private float ZoomRotation = 1;
    private Vector3 InitPos;
    private Vector3 InitRotation;
     
    void Start()
    {
        //Instantiate(Arrow, Vector3.zero, Quaternion.identity);
       
        InitPos = transform.position;
        InitRotation = transform.eulerAngles;
       
    }

    void Update()
    {
        //PAN
        if ( Input.GetKey("mouse 2") )
        {
            //(Input.mousePosition.x - Screen.width * 0.5)/(Screen.width * 0.5)
            transform.Translate(Vector3.right * Time.deltaTime * PanSpeed * (Input.mousePosition.x - Screen.width * 0.5f)/(Screen.width * 0.5f), Space.World);
            transform.Translate(Vector3.forward * Time.deltaTime * PanSpeed * (Input.mousePosition.y - Screen.height * 0.5f)/(Screen.height * 0.5f), Space.World);
        }
        else
        {
            if ( Input.GetKey("d") || Input.mousePosition.x >= Screen.width * (1 - ScrollEdge) )
                transform.Translate(Vector3.right * Time.deltaTime * ScrollSpeed, Space.World);
            else if ( Input.GetKey("a") || Input.mousePosition.x <= Screen.width * ScrollEdge )
                transform.Translate(Vector3.right * Time.deltaTime * -ScrollSpeed, Space.World);
           
            if ( Input.GetKey("w") || Input.mousePosition.y >= Screen.height * (1 - ScrollEdge) )
                transform.Translate(Vector3.forward * Time.deltaTime * ScrollSpeed, Space.World);
            else if ( Input.GetKey("s") || Input.mousePosition.y <= Screen.height * ScrollEdge )
                transform.Translate(Vector3.forward * Time.deltaTime * -ScrollSpeed, Space.World);
        }
       
    //ZOOM IN/OUT
        CurrentZoom -= Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * 1000 * ZoomZpeed;
        CurrentZoom = Mathf.Clamp(CurrentZoom,ZoomRange.x,ZoomRange.y);

        transform.SetPositionY(transform.position.y - (transform.position.y - (InitPos.y + CurrentZoom)) * 0.1f);
        transform.SetRotationX(transform.eulerAngles.x - (transform.eulerAngles.x - (InitRotation.x + CurrentZoom * ZoomRotation)) * 0.1f);
    }

}
