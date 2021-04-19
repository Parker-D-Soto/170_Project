using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteChanger : MonoBehaviour
{
    public SpriteRenderer render;
    public Sprite leftSprite;
    public Sprite rightSprite;
    public Sprite upSprite;
    public Sprite downSprite;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Horizontal") < 0)
        {
            Debug.Log(Input.GetAxis("Horizontal"));
            render.sprite = leftSprite;
        }
        else if(Input.GetAxis("Horizontal") > 0)
        {
            render.sprite = rightSprite;
        }
        else if(Input.GetAxis("Vertical") < 0)
        {
            render.sprite = downSprite;
        }
        else if(Input.GetAxis("Vertical") > 0)
        {
            render.sprite = upSprite;
        }
    }
}
