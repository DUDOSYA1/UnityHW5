using System;
using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private HealthParams healthParams;

    public int Hp { get { return hp; } set { } }
    public int Hunger { get { return hunger; } set { } }
    public bool IsAlive { get { return isAlive; } set { } }

    public Action<int> HpChanged;
    public Action<int> HungerChanged;
    public Action Died;

    private int hp
    {
        get { return hp; }
        set
        {
            hp = Mathf.Clamp(hp, 0, healthParams.MaxHp);
            HpChanged.Invoke(hp);
            if (hp == 0)
            {
                isAlive = false;
                Died.Invoke();
            }
        }
    }
    private int hunger
    {
        get { return hunger; }
        set 
        {
            hunger = Mathf.Clamp(hunger, 0, healthParams.MaxHunger);
            HungerChanged.Invoke(hunger);
        }
    }
    private bool isAlive;


    private void Awake()
    {
        if (healthParams is null)
            Debug.LogError("HealthParams are not assigned!");

        isAlive = true;
        hp = healthParams.MaxHp;
        hunger = healthParams.MaxHunger;

        StartCoroutine(UpdateStats());
    }


    IEnumerator UpdateStats()
    {
        while (isAlive)
        {
            yield return new WaitForSeconds(healthParams.TimeForUpdate);

            hunger -= healthParams.HungerPerTime;
            if(hunger == 0)
            {
                GetDamage(healthParams.HungerDamage);
            }
        }
    }

    public void GetHeal(int healAmount) 
    {
        if(isAlive)
            hp += healAmount;
    }
    public void GetDamage(int damageAmount)
    {
        if(isAlive)
            hp -= damageAmount;
    }

    public void Eat(int hungerAmount)
    {
        if(isAlive)
            hunger += hungerAmount;
    }
}
