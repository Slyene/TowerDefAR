using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IntelectCreeps : MonoBehaviour
{
    [Header("Скорость перердвижения")]
    public float speed = 1;
    [Header("Дистанция атаки, для ближников 1, для дальников как пожелаешь")]
    public float range = 1;
    [Header("Назначаем здесь количество здоровья")]
    public float maxHealthPoint = 1;

    public float currentHealthPoint;
    [Header("Назначаем здесь количество урона")]
    public float attackDamage = 10;
    [Header("Обозначает машташтабирование изначальное значение 0.1, менять с калькулятором!")]
    public float scale = 0.1f;
    GameObject Tawer;
    //private selectplane selectplane; // нужно для того чтобы определить позицию башни
    private Animator enemyAnimator;
    private NavMeshAgent behavior;
    float time, timerAction;
    public static Vector3 post;
    // Start is called before the first frame update
    public GameObject pl;
    public void GetDamage(float damage)
    {
        currentHealthPoint -= damage;
    }


    // Update is called once per frame
   
    void Start()
    {
        if (selectplane.plate == true)
        {
            Tawer = GameObject.FindGameObjectWithTag("Tawer");
            if (Tawer == null)
            {
                Destroy(this);
            }
            else { }
            enemyAnimator = GetComponent<Animator>();
            currentHealthPoint = maxHealthPoint;
            MoveCreep();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (selectplane.plate == true)
        {
            post = Tawer.transform.position;
            timerAction += Time.deltaTime;
            time += Time.deltaTime;
      behavior.SetDestination(post);
            if (currentHealthPoint <= 0)//смерть крипа если хп меньше 0
            {
                Death();
                behavior.isStopped = true;
                if (timerAction >= 5)
                {
                    Destroy(gameObject);
                }

            }
            else
            {
              if (transform.position.magnitude <= Mathf.Abs(behavior.stoppingDistance))//если он остановился то аттакует
              {
                   Attack();
                    behavior.isStopped = true;
               }

                if (behavior.isOnNavMesh == false)
                {
                    Destroy(this);
                    print("НЕ фартануло, крип потерялся(");
                }
            }
        }
    }

    void MoveCreep()
    {
        behavior = GetComponent<NavMeshAgent>();
       behavior.SetDestination(post);
        enemyAnimator.Play("Run");
        behavior.speed = speed * scale;
        behavior.stoppingDistance = range * 0.6f * scale;//дистанция остановки
    }

    void Attack()
    {

        enemyAnimator.SetBool("Attack", true);
        enemyAnimator.SetFloat("Cooldown", time);
        if (timerAction > 5)
        {
            Tawer.GetComponent<TowerScript>().DealDamage(attackDamage);
            timerAction = 0;
        }
        if (time > 2)
        {
            time = 0;
        }
    }
    
    public void Death()
    {
        if (!enemyAnimator.GetBool("Death"))
        {
            enemyAnimator.SetBool("Death", true);
            timerAction = 0;
        }
        else
        { 
            
        }
        
        
       // Destroy(gameObject);
    }
}
