using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shoot_wire : MonoBehaviour
{
    public Camera leftwire_cam;
    public Camera rightwire_cam;
    Vector3 player_pos;
    
    float ray_distance;
    GameObject hitObject;    

    LineRenderer left_lr;
    LineRenderer right_lr;
    public Material mt;
    public GameObject left_wire;
    public GameObject right_wire;

    void Start()
    {       
        left_lr = this.gameObject.AddComponent<LineRenderer>();
        right_lr = this.gameObject.AddComponent<LineRenderer>();

        left_lr.material = mt;
        left_lr.SetWidth(0.1f, 0.1f);
        //lr.SetColors(Color.green, Color.green);

        right_lr.material = mt;
        right_lr.SetWidth(0.1f, 0.1f);

        ray_distance = 30.0f;
    }

    // Update is called once per frame
    void Update()
    {     
        CheckWireHit(leftwire_cam, left_wire, left_lr);
        CheckWireHit(rightwire_cam, right_wire, right_lr);
    }

    void CheckWireHit(Camera wire_cam, GameObject wire, LineRenderer line)
    {
        Ray ray;
        RaycastHit rayHit;

        ray = wire_cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.0f));

        if (Physics.Raycast(ray, out rayHit, ray_distance) && rayHit.transform.gameObject.tag == "wall")
        {
            hitObject = rayHit.transform.gameObject;
            wire.transform.position = hitObject.transform.position;
            LineRender(wire, line);
            Debug.DrawRay(ray.origin, ray.direction * ray_distance, Color.black);
        }
        else
        {
            wire.transform.position = Vector3.Lerp(wire.transform.position, this.transform.position, Time.deltaTime * 1.0f);
            LineRender(wire, line);
        }
    }

    void LineRender(GameObject obj, LineRenderer line)
    {       
        line.SetPosition(0, this.transform.position);
        line.SetPosition(1, obj.transform.position);
    }
}
