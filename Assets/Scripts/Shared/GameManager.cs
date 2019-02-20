using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    //hold gamemanager
    private GameObject gameObject;
    private static GameManager m_Instance;
    public static GameManager Instance
    {
        get
        {   //instance the gameObject
            if(m_Instance == null)
            {
                m_Instance = new GameManager();
                m_Instance.gameObject = new GameObject("_gameManager");
                m_Instance.gameObject.AddComponent<InputController>();
                m_Instance.gameObject.AddComponent<Timer>();
                m_Instance.gameObject.AddComponent<Respwaner>();
                
            }

            return m_Instance;
        }
    }

    private InputController m_InputController;
    public InputController InputController
    {
        get
        {
            if(m_InputController == null)
            {
                m_InputController = gameObject.GetComponent<InputController>();
            }
            return m_InputController;
        }

    }

    private Timer m_Timer;
    public Timer Timer
    {
        get
        {
            if (m_Timer == null)
                m_Timer = gameObject.GetComponent<Timer>();
            return m_Timer;
        }
    }

    private Respwaner m_Respwaner;
    public Respwaner Respwaner
    {
        get
        {
            if (m_Respwaner == null)
                m_Respwaner = gameObject.GetComponent<Respwaner>();
            return m_Respwaner;
        }
    }


}
