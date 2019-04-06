using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class menu : MonoBehaviour
{
   public void ChangeSence(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
