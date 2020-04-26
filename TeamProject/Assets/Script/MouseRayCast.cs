using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseRayCast : MonoBehaviour
{
    // Start is called before the first frame update

    private RaycastHit hit = new RaycastHit();
    private Ray ray;
    private LineRenderer lineRenderer;

    private Color c1 = Color.red;
    private Color c2 = new Color(1, 1, 1, 0);


    public GameObject temp;
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
        lineRenderer.material.color = Color.white;
        lineRenderer.SetWidth(0.1f,0.1f);
        lineRenderer.SetPosition(0, this.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            Debug.Log("Click");
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray,out hit,100.0f))
            {
               // Debug.DrawLine(ray.origin, hit.point, Color.green);
                //temp.GetComponent<Transform>().position = hit.transform.position; 
                lineRenderer.SetPosition(1, hit.point);
                Debug.Log("Hit Point:" + hit.point.x + " , " + hit.point.y + " , " + hit.point.z);
            }
            else
            {
                //Debug.DrawLine(ray.origin, ray.direction * 100 , Color.red);
            }
        }
        else
        {
            lineRenderer.SetPosition(1, this.transform.position);
        }
        Debug.Log("Hit Point:" + hit.point.x + " , " + hit.point.y + " , " + hit.point.z);
    }
}
