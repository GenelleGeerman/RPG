using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private GameObject basicAttack;
    [SerializeField] private GameObject specialAttack;
    [SerializeField] private float basicAttackDelay;
    [SerializeField] private Staff staff;
    [SerializeField] private float specialAttackDelay;

    [SerializeField] private StatusManager statusManager;
    [SerializeField] private PlayerStats stats;
    [SerializeField] private EssenceManager essenceManager;

    private bool allowedToShoot = true;
    private bool isNormalAttackButtonPressed;

    private bool canUseSpecial = false;
    private Status currentStatus;
    private bool isSpecialAttackButtonPressed;
    private void Start()
    {
        statusManager = FindFirstObjectByType<StatusManager>();
        stats = GetComponent<PlayerStats>();
        essenceManager = GetComponent<EssenceManager>();
        essenceManager.OnMaxEssence.AddListener(OnMaxEssence);

    }
    private void Update()
    {
        NormalAttackCheck();
        SpecialAbilityCheck();
    }

    public static void RapidFire()
    {
        //Todo: implement
    }


    #region Buttons

    private void OnNormalAttack(InputValue normal)
    {
        isNormalAttackButtonPressed = normal.Get<float>() > 0;
    }
    private void OnSpecialAbility(InputValue special)
    {
        isSpecialAttackButtonPressed = special.Get<float>() > 0;
        Debug.Log("Pressed");
    }
    #endregion

    private Quaternion AimAtMouse()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector3 lookDir = mousePos - staff.transform.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        return Quaternion.Euler(0, 0, angle);
    }
    #region Attacks
    private void NormalAttackCheck()
    {
        if (CanAttackNormal())
        {
            StartCoroutine(UseNormalAttack());
        }
    }

    private bool CanAttackNormal()
    {
        return isNormalAttackButtonPressed
            && allowedToShoot
            && !IsGamePaused();
    }
    private void SpecialAbilityCheck()
    {
        if (CanUseSpecial())
        {
            statusManager.CurrentStatus = currentStatus;
            StartCoroutine(UseSpecialAbility());
        }
    }
   
    private bool CanUseSpecial()
    {
        return isSpecialAttackButtonPressed
            && canUseSpecial
            && !IsGamePaused();
    }

    private IEnumerator UseNormalAttack()
    {
        PlayerBasicAttack attack = CreateAttack();

        attack.Damage = stats.Attack;
        attack.Status = statusManager.CurrentStatus;
        attack.StatusEffect = statusManager.GetStatusEffect();
        allowedToShoot = false;
        yield return new WaitForSeconds(basicAttackDelay);
        allowedToShoot = true;
    }

    private PlayerBasicAttack CreateAttack()
    {
        Transform bullet = InstantiatePrefab();
        return bullet.GetComponent<PlayerBasicAttack>();
    }

    private Transform InstantiatePrefab()
    {
        Vector3 bulletPos = staff.GetSpawnPosition();
        var bullet = Instantiate(basicAttack, bulletPos, AimAtMouse()).transform;
        Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        return bullet;
    }

    private IEnumerator UseSpecialAbility()
    {
        float essence = essenceManager.Essence;
        int maxEssence = essenceManager.MaxEssence;
        
        float buffTime = maxEssence / GameManager.Instance.GetBuffTime();
        if (essence >= maxEssence)
        {
            canUseSpecial = false;

            while (essence > 0)
            {
                yield return new WaitForSeconds(1f);
                essence -= buffTime;
                essenceManager.Essence = essence;
            }
            essenceManager.Essence = 0;
            statusManager.CurrentStatus = Status.None;
            currentStatus = Status.None;
        }
        yield return new WaitForSeconds(0f);
    }
    #endregion
    private bool IsGamePaused()
    {
        return GameManager.Instance.IsPaused;
    }
    private void OnMaxEssence(Status status)
    {
        canUseSpecial = true;
        currentStatus = status;
    }
   
}