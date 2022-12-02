using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerScript : MonoBehaviour
{
    public float healPoint;

    public float curHP;

    public EffectType EffectType;
    public float EffectStrength;
    public float EffectDuration;

    private IEnumerator HealingProccess;

    public void DealDamage(float damage)
    {
        if (curHP > 0)
        {
            curHP -= damage;
        }

        if (curHP < 0)
        {
            curHP = 0;
        }
    }
    public void HealTower(float heal)
    {
        if (curHP + heal <= healPoint)
        {
            curHP += heal;
        }
        else
        {
            curHP = healPoint;
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        curHP = healPoint;

        if (EffectType == EffectType.Mechanical)
        {
            HealingProccess = Healing(EffectStrength, 4);
            StartCoroutine(HealingProccess);
        }
    }

    IEnumerator Healing(float amount, float interval)
    {
        while (gameObject.activeInHierarchy)
        {
            HealTower((int)((healPoint - curHP) * (amount / 100)));
            yield return new WaitForSeconds(interval);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (curHP <= 0)
        {
            Death();
        }
    }

    void Death()
    {
        Destroy(gameObject);
    }
}
