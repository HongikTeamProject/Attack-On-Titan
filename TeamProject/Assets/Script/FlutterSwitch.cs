using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlutterSwitch : MonoBehaviour
{
    public GameObject[] fluttereffects;

    bool onState;
    Vector3 my_position;

    float cooltime;
    // Start is called before the first frame update
    void Start()
    {
        onState = false;
        my_position = transform.position;
        cooltime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(onState)
        {
            transform.position = my_position - new Vector3(0f, 0.499f, 0f);
            foreach (var item in fluttereffects)
                item.SetActive(true);
        }
        else
        {
            transform.position = my_position;
            foreach (var item in fluttereffects)
                item.SetActive(false);
        }

        if (cooltime > 0)
            cooltime -= Time.deltaTime;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (cooltime > 0)
            return;

        if (other.gameObject.name == "Player")
        {
            onState = !onState;
            cooltime = 0.5f;
        }
    }
}
