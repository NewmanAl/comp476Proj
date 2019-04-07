using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Bridge : MonoBehaviour
{
    int counter = 0;
    public Transform parentTransform;
    void Start()
    {
        parentTransform = this.transform;
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "TNT")
        {
            counter++;
        }
        if(counter >= 30)
        {
            Invoke("isDestoried", 5f);
        }
 
    }


    void isDestoried()
    {
        for (int j = 0; j < parentTransform.childCount; j++)
        {
            parentTransform.GetChild(j).gameObject.SetActive(false);
        }

        Invoke("loadScene", 1f);



    }

    void loadScene()
    {
        SceneManager.LoadScene("winScene");
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }
}
