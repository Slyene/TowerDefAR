using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ShootScript : MonoBehaviour
{
    public static bool PlateAllreadySpawned = true;
    private ARRaycastManager ARRaycastManagerScript;
    RaycastHit hitObject;
    public Transform Pointer;
    public float damage = 30;

    void Start()
    {
        ARRaycastManagerScript = FindObjectOfType<ARRaycastManager>();

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

                if (hit.collider.gameObject.GetComponent<EnemyHeal>())
                {
                    hit.collider.gameObject.GetComponent<EnemyHeal>().GetDamage(damage); ;
                }
            }
        }
    }
}
