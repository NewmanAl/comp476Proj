using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aimPoint : MonoBehaviour
{
    [SerializeField] Texture2D image;
    [SerializeField] int size;
    [SerializeField] float maxAngle;
    [SerializeField] float minAngle;
    public GameObject gun;
    float lookHeight;
    void Awake()
    {


    }

    public void LookHeight(float value)
    {
        lookHeight += value;

        if (lookHeight > maxAngle || lookHeight < minAngle)
        {

            lookHeight -= value;
        }




    }

    void OnGUI()
    {
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
        screenPosition.y = Screen.height - screenPosition.y;
        Rect rec = new Rect(screenPosition.x, screenPosition.y - lookHeight, size, size);
        GUI.DrawTexture(rec, image);



        // print(gun.GetComponent<gun>().fireEnable);
        if (gun.GetComponent<gun>().fireEnable)
        {
            Ray rayOrigin = Camera.main.ScreenPointToRay(rec.position);
            RaycastHit hitInfo;
            if (Physics.Raycast(rayOrigin, out hitInfo))
            {
                if (hitInfo.collider != null)
                {
                    Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;
                    Debug.DrawRay(transform.position, forward, Color.red);
                    print("ray hit");
                }
            }
        }

        // print(screenPosition.y - lookHeight);
    }



}
