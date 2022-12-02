using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float Speed = 0.5f;
    private IntelectCreeps Target;
    [SerializeField]
    private float Damage = 0.3f;
    private TowerScript Tower;
    // Start is called before the first frame update



    public void Initialize(IntelectCreeps target, float damage, float speed)
    {
        Target = target;
        Damage = damage;
        Speed = speed;
    }

    void Start()
    {
        ParticleSystem sys = GetComponentInChildren<ParticleSystem>();
        sys.Play();

        Tower = FindObjectOfType<TowerScript>();
    }

    private void FixedUpdate()
    {
        if (Target != null)
        {
            Vector3 destination = new Vector3(Target.transform.position.x, Target.transform.position.y + Target.transform.localScale.y / 2, Target.transform.position.z);
            if (Vector3.Distance(transform.position, destination) > 0.01)
            {
                transform.rotation = Quaternion.LookRotation(destination - transform.position);
                transform.position += transform.forward.normalized * Speed * Time.deltaTime;
            }
            else
            {
                Target.GetComponent<IntelectCreeps>().GetDamage(Damage);

                switch (Tower.EffectType)
                {
                    case EffectType.Ignite:
                        Target.GetComponent<Effects>().IgniteTarget(Tower.EffectStrength, Tower.EffectDuration);
                        break;
                    case EffectType.Freeze:
                        Target.GetComponent<Effects>().FreezeTarget(Tower.EffectStrength, Tower.EffectDuration);
                        break;
                    case EffectType.Weakness:
                        Target.GetComponent<Effects>().WeaknessTarget(Tower.EffectStrength, Tower.EffectDuration);
                        break;
                }

                Destroy(gameObject, 0);
            }
        }
        else
        {
            Destroy(gameObject, 0);
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
