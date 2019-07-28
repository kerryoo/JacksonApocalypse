using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{

    [SerializeField] Texture2D image;
    [SerializeField] int size;
    [SerializeField] float maxAngle;
    [SerializeField] float minAngle;

    [SerializeField] float widthMultiplier;
    [SerializeField] float heightMultiplier;

    float lookHeight;
    float lookWidth;

    public void Start()
    {
        lookHeight = 0;
    }
    public void Look (Vector2 value)
    {
        //Debug.Log(value);

        lookHeight = (lookHeight + value.y) * heightMultiplier;
        if (lookHeight > maxAngle || lookHeight < minAngle)
            lookHeight = value.y;

        lookWidth = (lookWidth + value.x) * widthMultiplier;
        if (lookWidth > maxAngle || lookWidth < minAngle)
            lookWidth = value.x;


    }

    private void OnGUI()
    {
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
        screenPosition.y = Screen.height - screenPosition.y;
        screenPosition.x = Screen.width - screenPosition.x;
        GUI.DrawTexture(new Rect(screenPosition.x - lookWidth, screenPosition.y - lookHeight, size, size), image);
    }
}
