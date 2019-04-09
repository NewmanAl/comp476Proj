using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Items_UI : MonoBehaviour
{

    public static Items_UI instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UIAcquireResource(int index) {
        if(index > 2 || index < 0) {
            print("bad index for items UI children in Items_UI.cs");
        }

        this.transform.GetChild(index).gameObject.SetActive(true);

    }
}
