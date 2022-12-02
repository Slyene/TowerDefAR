using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EffectType
{
    None, Ignite, Freeze, Weakness, Mechanical
}

public class Effects : MonoBehaviour
{
    IEnumerator igniteEffect;
    IEnumerator freezeEffect;
    IEnumerator weaknessEffect;


    private float originalDamage;
    private float originalSpeed;

    IntelectCreeps Target;

    IEnumerator Weakness(float weaknessPercentage, float duration)
    {
        Target.attackDamage = originalDamage * (1 - weaknessPercentage / 100);

        yield return new WaitForSeconds(duration);

        Target.attackDamage = originalDamage;
    }

    IEnumerator Freeze(float freezePercentage, float duration)
    {
        Target.Speed = originalSpeed * (1 - freezePercentage / 100);

        yield return new WaitForSeconds(duration);

        Target.Speed = originalSpeed;
    }

    IEnumerator Igniting(float percentageOfMaxHealth, float duration)
    {
        float timer = duration;
        float fireDamage = Target.maxHealthPoint * (percentageOfMaxHealth / 100);

        while (timer > 0)
        {
            Target.GetDamage(fireDamage);
            timer -= 1;
            yield return new WaitForSeconds(1);
        }
    }

    public void IgniteTarget(float _strength, float _time)
    {
        if (igniteEffect != null)
            StopCoroutine(igniteEffect);
        igniteEffect = Igniting(_strength, _time);
        StartCoroutine(igniteEffect);
    }

    public void FreezeTarget(float _freezePercentage, float _duration)
    {
        if (freezeEffect != null)
            StopCoroutine(freezeEffect);

        Target.speed = originalSpeed;
        freezeEffect = Freeze(_freezePercentage, _duration);
        StartCoroutine(freezeEffect);
    }

    public void WeaknessTarget(float _weaknessPercentage, float _duration)
    {
        if (weaknessEffect != null)
            StopCoroutine(weaknessEffect);

        Target.attackDamage = originalDamage;
        weaknessEffect = Weakness(_weaknessPercentage, _duration);
        StartCoroutine(weaknessEffect);
    }

    // Start is called before the first frame update
    void Start()
    {
        Target = GetComponent<IntelectCreeps>();
        originalDamage = Target.attackDamage;
        originalSpeed = Target.Speed;
    }
}
