using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float Speed = 0.5f;
    public IntelectCreeps Target;
    [SerializeField]
    private float Damage = 0.3f;
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
    }

    private void FixedUpdate()
    {
        if (Target != null)
        {
            if (Vector3.Distance(transform.position, Target.transform.position) > 0.01)
            {
                transform.rotation = Quaternion.LookRotation(Target.transform.position - transform.position);
                transform.position += transform.forward.normalized * Speed * Time.deltaTime;
            }
            else
            {
                Target.GetDamage(Damage);
                Destroy(gameObject, 0);
            }
        }
        else
        {
            Destroy(gameObject, 5);
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
