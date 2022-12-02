using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    public Slider HealthBar;
    public Text text;
    public Color Low, High;

    private TowerScript tower;

    //// Start is called before the first frame update
    void Start()
    {
        tower = FindObjectOfType<TowerScript>();
        HealthBar.maxValue = tower.healPoint;
    }

    //// Update is called once per frame
    void Update()
    {
        HealthBar.value = tower.curHP;
        HealthBar.maxValue = tower.healPoint;
        text.text = tower.curHP + " / " + tower.healPoint;
        HealthBar.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(Low, High, HealthBar.normalizedValue);
    }
}