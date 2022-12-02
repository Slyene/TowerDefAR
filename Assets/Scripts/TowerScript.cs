using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TowerScript : MonoBehaviour
{
    public float MaxHealth;

    public float curHP;

    public EffectType EffectType;
    public float EffectStrength;
    public float EffectDuration;

    private IEnumerator HealingProccess;

    [Header("Leveling")]

    [SerializeField]
    private int HealthLevel = 1;
    [SerializeField]
    private int AttackLevel = 1;
    [SerializeField]
    private int AttackSpeedLevel = 1;
    [SerializeField]
    private int HealthUpgradingPrice = 1000, AttackUpgradingPrice = 1000, AttackSpeedUpgradingPrice = 1000;

    private float HealthUpgradeAmount = 100f, AttackUpgradeAmount = 10f, AttackSpeedUpgradeAmount = 0.2f;

    private score Score;
    private Shooting Shooting;
    private HealthBarScript HealthBar;
    private Button btnHealthUpgrade, btnAttackUpgrade, btnAttackSpeedUpgrade;
    private GameObject UpgradePanel;


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

        HealthBar.ChangingHealthBars(curHP, MaxHealth);
    }

    public void HealTower(float heal)
    {
        if (curHP + heal <= MaxHealth)
        {
            curHP += heal;
        }
        else
        {
            curHP = MaxHealth;
        }

        HealthBar.ChangingHealthBars(curHP, MaxHealth);
    }
    // Start is called before the first frame update
    void Start()
    {
        curHP = MaxHealth;
        Score = FindObjectOfType<score>();
        Shooting = FindObjectOfType<Shooting>();
        HealthBar = FindObjectOfType<HealthBarScript>();
        btnHealthUpgrade = GameObject.Find("UpHealth").GetComponentInChildren<Button>();
        btnAttackUpgrade = GameObject.Find("UpAttack").GetComponentInChildren<Button>();
        btnAttackSpeedUpgrade = GameObject.Find("UpAttackSpeed").GetComponentInChildren<Button>();
        UpgradePanel = GameObject.Find("UpgradePanel");

        if (EffectType == EffectType.Mechanical)
        {
            HealingProccess = Healing(EffectStrength, 4);
            StartCoroutine(HealingProccess);
        }
        HealthBar.ChangingHealthBars(curHP, MaxHealth);

        
        btnHealthUpgrade.onClick.AddListener(UpgradeHealth);
        btnAttackUpgrade.onClick.AddListener(UpgradeAttack);
        btnAttackSpeedUpgrade.onClick.AddListener(UpgradeAttackSpeed);
        UpgradePanel.SetActive(false);
    }

    IEnumerator Healing(float amount, float interval)
    {
        while (gameObject.activeInHierarchy)
        {
            HealTower((int)((MaxHealth - curHP) * (amount / 100)));
            yield return new WaitForSeconds(interval);
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpgradesTesting();

        if (curHP <= 0)
        {
            Death();
        }
    }

    void Death()
    {
        btnHealthUpgrade.onClick.RemoveListener(UpgradeHealth);
        btnAttackUpgrade.onClick.RemoveListener(UpgradeAttack);
        btnAttackSpeedUpgrade.onClick.RemoveListener(UpgradeAttackSpeed);
        Destroy(gameObject);
    }

    private void UpgradesTesting()
    {
        if(Input.GetKeyDown(KeyCode.S))
            UpgradeAttackSpeed();
        if(Input.GetKeyDown(KeyCode.A))
            UpgradeAttack();
        if(Input.GetKeyDown(KeyCode.H))
            UpgradeHealth();
    }

    public void UpgradeHealth()
    {
        if (Score.Allscore >= HealthUpgradingPrice && HealthLevel < 10)
        {
            Score.Allscore -= HealthUpgradingPrice;
            MaxHealth += HealthUpgradeAmount;
            HealthLevel++;
            HealthUpgradingPrice = (int)((float)HealthUpgradingPrice * 1.5f);

            HealthBar.ChangingHealthBars(curHP, MaxHealth);
        }
    }

    public void UpgradeAttack()
    {
        if (Score.Allscore >= AttackUpgradingPrice && AttackLevel < 10)
        {
            Score.Allscore -= AttackUpgradingPrice;
            Shooting.ProjectileDamage += AttackUpgradeAmount;
            AttackLevel++;
            AttackUpgradingPrice = (int)((float)AttackUpgradingPrice * 1.5f);
        }
    }

    public void UpgradeAttackSpeed()
    {
        if (Score.Allscore >= AttackSpeedUpgradingPrice && AttackSpeedLevel < 10)
        {
            Score.Allscore -= AttackSpeedUpgradingPrice;
            Shooting.ShootingRate += AttackSpeedUpgradeAmount;
            AttackSpeedLevel++;
            AttackSpeedUpgradingPrice = (int)((float)AttackSpeedUpgradingPrice * 1.5f);
        }
    }
}
