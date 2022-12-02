using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class SpawnScript : MonoBehaviour
{
    [Header("���� ������ ������� ������")]
    public GameObject[] enemyPrefab;
    [Header("�������� � ������� ����� ���������� �������")]
    public float  interval = 2f;
    [Header("���������� ������ ������� �� ����� �������")]
    public int[] countEnemySpawn; //������� ���� �����, ����� ������� �������� ���, �������� �������� � ������ �������� ���������� ������������ ��������



    [Header("��� ���������� �������������� �������� ������ � �������������, ��������� �������� 0.1")]
    public float scale;

    [Header("�������� ���� ������")]
    public int radius;


    private float time = 0f;
    private int count = 0, //���������� ����� �� ����� ��������
    enemySpawned = 0; // ���������� ����� ������� �� ��������
    //private int[,] countsCreeep; //������� ���� �����, ������ ���������� ��� ���, ������ ���������� �����

    

    // Start is called before the first frame update
    void Start()
    {

        foreach (int i in countEnemySpawn)
        {
            count += i;
        }
        if (countEnemySpawn.Length != enemyPrefab.Length)
        {
            Array.Resize<System.Int32>(ref countEnemySpawn, enemyPrefab.Length);
        }
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;


        int[] countSpawned = new int[countEnemySpawn.Length];
        if ((time >= interval) && (enemySpawned < count))
        {
            int rndCreep = UnityEngine.Random.Range(0, countSpawned.Length - 1); //�������� ���������� ������
            SelCreep:
            if (countSpawned[rndCreep] == countEnemySpawn[rndCreep]) //����� �������� ������, �� ���� ��� ��������� ��� �� � ������� ���� ������ �����������
            { 
                rndCreep = UnityEngine.Random.Range(0, enemyPrefab.Length - 1);
                goto SelCreep;
                
            }
            else 
            {
               Instantiate(enemyPrefab[rndCreep], RandomPointOnCircleEdge(radius), enemyPrefab[rndCreep].transform.rotation);
                countSpawned[rndCreep]++;
                enemySpawned++;
            }
            time = 0;
            

        }
    }
    private Vector3 RandomPointOnCircleEdge(float radius)
    {
        var vector2 = UnityEngine.Random.insideUnitCircle.normalized * radius * scale;
        return new Vector3(vector2.x, 0, vector2.y);
    }
}




