using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IntelectCreeps : MonoBehaviour
{
    [Header("�������� �������������")]
    public float speed = 1;
    [Header("��������� �����, ��� ��������� 1, ��� ��������� ��� ���������")]
    public float range = 1;
    [Header("��������� ����� ���������� ��������")]
    public float maxHealthPoint = 1f;

    public float currentHealthPoint;

    public bool IsAlive = true; // Indicator if the enemy is alive  @Serega
    [Header("��������� ����� ���������� �����")]
    public float attackDamage = 10;
    [Header("���������� ����������������� ����������� �������� 0.1, ������ � �������������!")]
    public float scale = 0.1f;
    GameObject Tawer;

    [SerializeField]
    private GameObject SelectionIndicator;
    //private selectplane selectplane; // ����� ��� ���� ����� ���������� ������� �����
    private Animator enemyAnimator;
    private NavMeshAgent behavior;
    float time, timerAction;
    public static Vector3 post;
    // Start is called before the first frame update
    public GameObject pl;
    public void GetDamage(float damage)
    {
        currentHealthPoint -= damage;

        if (currentHealthPoint <= 0)
        {
            IsAlive = false;
            FindObjectOfType<Spawnr>().SpawnedEnemies.Remove(gameObject);
        }
    }

    public void Select()
    {
        SelectionIndicator.SetActive(true);
    }
    public void Deselect()
    {
        SelectionIndicator.SetActive(false);
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
            if (currentHealthPoint <= 0)//������ ����� ���� �� ������ 0
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
                if (Vector3.Distance(post, transform.position) <= Mathf.Abs(behavior.stoppingDistance))//���� �� ����������� �� ��������
                {
                    Attack();
                    behavior.isStopped = true;
                }

                if (behavior.isOnNavMesh == false)
                {
                    Destroy(this);
                    print("�� ���������, ���� ���������(");
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
        behavior.stoppingDistance = range * 0.6f * scale;//��������� ���������
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