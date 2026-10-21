using System;
using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private HealthParams healthParams;

    public int Hp
    {
        get => hp;
        private set
        {
            hp = Mathf.Clamp(value, 0, healthParams.MaxHp);
            OnHpChanged?.Invoke(hp);

            if (hp == 0 && isAlive)
            {
                isAlive = false;
                OnDied?.Invoke();
            }
        }
    }

    public int Hunger
    {
        get => hunger;
        private set
        {
            hunger = Mathf.Clamp(value, 0, healthParams.MaxHunger);
            OnHungerChanged?.Invoke(hunger);
        }
    }

    public bool IsAlive => isAlive;

    public Action<int> OnHpChanged;
    public Action<int> OnHungerChanged;
    public Action OnDied;

    private int hp;
    private int hunger;
    private bool isAlive;

    private void Awake()
    {
        if(healthParams == null)
            Debug.LogError("HealthParams are not assigned!");

        isAlive = true;
        Hp = healthParams.MaxHp;
        Hunger = healthParams.MaxHunger;

        StartCoroutine(UpdateStats());
    }

    private IEnumerator UpdateStats()
    {
        while (isAlive)
        {
            yield return new WaitForSeconds(healthParams.TimeForUpdate);

            Hunger -= healthParams.HungerPerTime;
            if(Hunger == 0)
                GetDamage(healthParams.HungerDamage);
        }
    }

    public void GetHeal(int healAmount)
    {
        if(isAlive)
            Hp += healAmount;
    }

    public void GetDamage(int damageAmount)
    {
        if(isAlive)
            Hp -= damageAmount;
    }

    public void Eat(int hungerAmount)
    {
        if(isAlive)
            Hunger += hungerAmount;
    }
}