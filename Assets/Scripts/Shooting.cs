using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
public class shooting : MonoBehaviour
{ public static bool PlateAllreadySpawned = true;
    private ARRaycastManager ARRaycastMAnagerScript;
    RaycastHit hitObject;
    public Transform Pointer;
    GameObject Tawer;

    void Start()
    {
        ARRaycastMAnagerScript = FindObjectOfType<ARRaycastManager>();
       
    }

    // Update is called once per frame
    void Update()
    {
    
        if (PlateAllreadySpawned)
        {
            Ray ray = new Ray();
            ray.origin = transform.position; //начало  
            ray.direction = transform.forward;//направление
            Debug.DrawRay(transform.position, transform.forward, Color.red);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            { 
                Pointer.position = hit.point;
          
                if (hit.collider.gameObject.GetComponent<IntelectCreeps>())
                {
                    
                    hit.collider.gameObject.GetComponent<IntelectCreeps>().GetDamage(  10);
                }
            }
        }
    }
}
