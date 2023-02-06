using Assets.Scripts.Status;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Graphs;
using UnityEngine;
using UnityEngine.UI;
public class Enemy : MonoBehaviour, IAttackable, ITakeDamage, ISlowable
{
    [Header("Required")]
    [SerializeField] private StatBlock statBlock;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Image fill;
    [SerializeField] private GameObject essence;

    [Header("Do not Edit")]
    [SerializeField] private Gradient healthGradient;
    [SerializeField] private int maxHp;
    [SerializeField] private int health;
    [SerializeField] private int attack;
    [SerializeField] private int maxSpeed;
    [SerializeField] private int speed;
    [SerializeField] private bool isSlowed = false;
    public int Speed { get => speed; }

    private static List<GameObject> enemiesList = new List<GameObject>();

    void Start()
    {
        enemiesList.Add(gameObject);
        InitializeStats();
        InitializeHealthBar();
    }

    private void InitializeHealthBar()
    {
        healthSlider.maxValue = maxHp;
        healthSlider.value = health;
        fill.color = healthGradient.Evaluate(1f);
    }

    private void InitializeStats()
    {
        maxSpeed = statBlock.speed;
        speed = maxSpeed;
        maxHp = statBlock.maxHealth;
        health = statBlock.maxHealth;
        healthGradient = statBlock.healthGradient;
        attack = statBlock.attack;
    }

    void Update()
    {
        HealthCheck();
    }

    public void TakeDamage(int damage)
    {
        health = Mathf.Max(0, health - damage);
    }

    protected void HealthCheck()
    {
        healthSlider.value = health;
        fill.color = healthGradient.Evaluate(healthSlider.normalizedValue);
        if (health <= 0)
        {
            health = 0;
            SpawnEssence();
            Destroy(gameObject);
        }
    }

    private void SpawnEssence()
    {
        var spawnEssence = Instantiate(essence, transform.position, Quaternion.identity);
        spawnEssence.transform.parent = null;
    }

    public int GetHealth()
    {
        return health;
    }
    public int GetAttack()
    {
        return attack;
    }

    public static void Kill_All()
    {
        foreach (GameObject enemy in enemiesList)
        {
            Destroy(enemy);
        }
    }

    public void Slow(bool isSlow)
    {
        speed = isSlow ? maxSpeed / 3 : maxSpeed;
        isSlowed = isSlow;
    }

    public bool IsSlowed()
    {
        return isSlowed;
    }
}
