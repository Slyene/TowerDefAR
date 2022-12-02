using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnr : MonoBehaviour
{
    public GameObject[] enemiPrefab;    
    public float time = 0f, interval = 2f;
    public int enemiCauntInBatel = 0, enemiCaunt = 0 ;   
    public float radius = 0.5f;
    public float mobSceal = 0.03f;  
    public int[] spawnPercent;
    public static Vector3 post;
    GameObject Tawer;
    GameObject pla;

    public List<GameObject> SpawnedEnemies = new List<GameObject>(); // Tracking of spawned enemies @Serega
    // Start is called before the first frame update
    void Start()
    {
        if (selectplane.plate == true)
        {
            enemiCauntInBatel = 0;
            spawnPercent = new int[enemiPrefab.Length];
            enemiCaunt = 2;

            spawnPercent[0] = 80;
            spawnPercent[1] = 20;
        }
    }

    // Update is called once per frame
    void Update()
    {

        Tawer = GameObject.FindGameObjectWithTag("Tawer");
      //  pla = GameObject.FindGameObjectWithTag("pla");
        post = Tawer.transform.position;
      // pla.transform.position = post;
        if (selectplane.plate == true)
        {
            time += Time.deltaTime;

            if ((time >= interval) && (enemiCauntInBatel < enemiCaunt))
            {
                SpawnedEnemies.Add(Instantiate
                    (
                    enemiPrefab[MobSpawn(Random.Range(1, 100))],
                    RandomPointOnCircleEdge(radius) + post,
                    enemiPrefab[0].transform.rotation
                    )); // Added to the list of spawned enemies @Serega
                enemiCauntInBatel++;
                time = 0f;
            }

            if ((time >= 10f) && (enemiCauntInBatel == enemiCaunt))
            {
                for (int i = 0; i < spawnPercent.Length - 1; i++)
                {
                    if (spawnPercent[i] > 0)
                    {
                        if (spawnPercent[i + 1] != 95)
                        {
                            spawnPercent[i] -= 5;
                            spawnPercent[i + 1] += 5;
                        }
                    }
                }

                enemiCaunt += 3;
                enemiCauntInBatel = 0;
            }


        }
    }
    private Vector3 RandomPointOnCircleEdge(float radius)
    {
        var vector2 = Random.insideUnitCircle.normalized * (radius - mobSceal);
        return new Vector3(vector2.x, 0, vector2.y);
    }

    private int MobSpawn(int mobChance)
    {
        int i = 0;
       while(i < spawnPercent.Length)
        {
            if(mobChance <= spawnPercent[i])
            {
                return i;
            }
            else
            {
                mobChance -= spawnPercent[i];
                i++;
            }
        }
        return -1;
    }
}
