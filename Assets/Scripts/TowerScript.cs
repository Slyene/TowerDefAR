using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerScript : MonoBehaviour
{
    public float healPoint;

    public float curHP;

    public void DealDamage(float damage)
    {
        curHP -= damage;
    }
    public void HealTower(float heal)
    {
        curHP += heal;
    }
    // Start is called before the first frame update
    void Start()
    {
        curHP = healPoint;
    }

    
    // Update is called once per frame
    void Update()
    {
        if (curHP <= 0)
        {
            Death();
        }
    }

    void Death() {
        Destroy(gameObject);
    }
}
