using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCameraMovement : MonoBehaviour
{
    //public Rigidbody2D player;
    public Camera cam;
    //public Rigidbody2D rb;
    public Transform ft;
    Vector2 mousePosition;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    void FixedUpdate()
    {
        if (!gameObject.GetComponent<Updated_Player_Stats>().Check_Dialogue_Status() && !gameObject.GetComponent<Updated_Player_Stats>().Check_Grapple_Status() && !gameObject.GetComponent<Updated_Player_Stats>().Check_Dash_Status())
        {
            Vector2 fireDir = new Vector2(ft.position.x, ft.position.y);
            Vector2 lookDirection = mousePosition - fireDir;
            float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
            ft.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        }

    }
    void Aim()
    {

    }
}
