using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respwaner : MonoBehaviour
{
    public void Despwan(GameObject go, float inSeconds)
    {
        go.SetActive(false);
        GameManager.Instance.Timer.Add(() =>
        {
            go.SetActive(true);
        }, inSeconds);
    }
}
