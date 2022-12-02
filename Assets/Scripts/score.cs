using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class score : MonoBehaviour
{
    public int Allscore=0;

    public int[] Kills = new int[2];
    public int AllKills=0;

    public Dictionary<int, int> mobsType = new Dictionary<int, int>();
    

    // Start is called before the first frame update
    void Start()
    {
        Kills[0] = 0;
        Kills[1] = 0;

        //mobs dictionary
        mobsType.Add(0, 100); //Simple skeleton
        mobsType.Add(1, 250); //heavy skeleton
        
    }


    //Adding points from kills

    //start
    public void IncreaseScore(int mob)
    {
        AllKills++;
        Kills[mob]++;
        Allscore += mobsType[mob];
        
    }
    //end

    //Removing points
    //start
    //here must be shit that not done
    //end

    // Update is called once per frame
    void Update()
    {
        
    }
}
