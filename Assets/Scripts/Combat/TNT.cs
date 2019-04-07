using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TNT : MonoBehaviour
{
    public GameObject effect;
    
    // Start is called before the first frame update
  
    void Start()
    {
        transform.parent = null;
        Invoke("playeffect", 4f);
        Destroy(this.gameObject, 5f);
 
    }

    void playeffect()
    {
        effect.SetActive(true);
    }
   
}
