using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTrigger : MonoBehaviour
{

    private SphereCollider m_collider;
    private enum State { IDLE, DECREASING };
    private State m_currentState;

    [SerializeField]
    private float m_decreaseTime;
    private float m_decreaseScale;
    private float m_minRadius;

    [SerializeField]
    private bool m_debug;
    private GameObject m_debugSphere;
    
    // Start is called before the first frame update
    void Start()
    {
        m_collider = gameObject.GetComponent<SphereCollider>();
        m_minRadius = m_collider.radius;
        m_currentState = State.IDLE;

        m_debugSphere = transform.Find("DebugSphere").gameObject;
        if(m_debug)
        {
            updateDebugSphereRadius();
            m_debugSphere.SetActive(true);
            invertDebugSphereNormals();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(m_currentState == State.DECREASING)
        {
            float distance = m_collider.radius - m_minRadius;
            float delta = m_decreaseScale * Time.deltaTime;

            
            m_collider.radius = Mathf.Max(m_minRadius, m_collider.radius - delta);

            if(m_debug)
            {
                updateDebugSphereRadius();
            }

            if(m_collider.radius == m_minRadius)
            {
                m_currentState = State.IDLE;
            }
        }

        if(m_debug)
        { 
            if(Input.GetKeyDown(KeyCode.J))
            {
                AddSound(5);
            }
            if (Input.GetKeyDown(KeyCode.K))
            {
                AddSound(3);
            }
        }
    }

    public void AddSound(float soundRadius)
    {
        if (m_collider.radius < soundRadius)
        {
            m_currentState = State.DECREASING;
            m_collider.radius = soundRadius;
            m_decreaseScale = (soundRadius - m_minRadius) / m_decreaseTime;
        }
    }

    private void updateDebugSphereRadius()
    {
        float scale = m_collider.radius * 2;
        m_debugSphere.transform.localScale = new Vector3(scale, scale, scale);
    }

    private void invertDebugSphereNormals()
    {
        //credit: https://wiki.unity3d.com/index.php/ReverseNormals

        MeshFilter filter = m_debugSphere.GetComponent<MeshFilter>();
        if(filter != null)
        {
            Mesh mesh = filter.mesh;

            Vector3[] normals = mesh.normals;
            for(int i = 0; i < normals.Length; i++)
            {
                normals[i] = -normals[i];
            }
            mesh.normals = normals;

            for(int m=0; m <mesh.subMeshCount; m++)
            {
                int[] triangles = mesh.GetTriangles(m);
                for(int i = 0; i < triangles.Length; i+=3)
                {
                    int temp = triangles[i];
                    triangles[i] = triangles[i + 1];
                    triangles[i + 1] = temp;
                }
                mesh.SetTriangles(triangles, m);
            }
        }
    }
}
