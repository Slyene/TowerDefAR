using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class selectplane : MonoBehaviour
{[Header("тут маркер для установки башни")]
    [SerializeField]private GameObject Planemarkerprefab;
    private ARRaycastManager ARRaycastMAnagerScript;
    public static Vector3 position;
    [Header("Дистанция атаки, для ближников 1, для дальников как пожелаешь")]
   
    public static   bool plate = true;
    public GameObject spawnNewCastle;
    public GameObject spawnNewPlate;
    public GameObject spawnNewSpawner;

    public static Vector3 positionTower { get; set; } //Строчка для того чтобы замерить позицию башни чтобы на нее наподали крипы
    
    
    void Start()
    {
        ARRaycastMAnagerScript = FindObjectOfType<ARRaycastManager>();
        Planemarkerprefab.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        showMarker();
        
    }
    void showMarker() {
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        ARRaycastMAnagerScript.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits, TrackableType.Planes);
        if (hits.Count > 0)
        {if (plate == true)
            { Planemarkerprefab.SetActive(false); }
            else {
                Planemarkerprefab.SetActive(true); }
            Planemarkerprefab.transform.position = hits[0].pose.position;

        }
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began && plate==false) //Когда ставим
        {
            plate = true;
            Planemarkerprefab.SetActive(false);
            //  Instantiate(spawnNewPlate, hits[0].pose.position, spawnNewPlate.transform.rotation);
            //  Instantiate(spawnNewCastle, hits[0].pose.position, spawnNewCastle.transform.rotation);
            //  Instantiate(spawnNewSpawner, hits[0].pose.position, spawnNewSpawner.transform.rotation);
            spawnNewCastle.transform.position = hits[0].pose.position;
            spawnNewPlate.transform.position = hits[0].pose.position;
            spawnNewSpawner.transform.position = hits[0].pose.position;
            position = hits[0].pose.position;
            positionTower = hits[0].pose.position; //Строчка Лисёнка! это для крипа
         
        }
        //if (GameObject.FindWithTag("Enemy") == hits[0].pose.position)
        //{
        //    GameObject.FindWithTag("Enemy").GetComponent<IntelectCreeps>().DealDamage(10);
        //}
    }
}
