using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SpritesHandler : MonoBehaviour
{
    [SerializeField] private Image gun;
    [SerializeField] private Image bob;
    [SerializeField] private HealthParams healthParams;
    [SerializeField] private Health health;
    [SerializeField] private int shootDamage;
    [SerializeField] private float shootTime;

    [SerializeField] private Sprite bobDead;
    [SerializeField] private Sprite bobIdle;
    [SerializeField] private Sprite bobHungry;
    [SerializeField] private Sprite bobShoot;
    [SerializeField] private Sprite bobStarves;

    [SerializeField] private Sprite gunIdle;
    [SerializeField] private Sprite gunShoot;

    private int prevHp;
    private int prevHunger;
    private bool inShooting;

    private SpritesEnum currentState;
    private SpritesEnum state
    {
        get { return currentState; }
        set
        {
            currentState = value;
            SetImage(value);
        }
    }
    private void Awake()
    {
        state = SpritesEnum.Idle;

        health.OnHpChanged += SetStateByHp;
        health.OnHungerChanged += SetStateByHunger;
    }

    private void SetStateByHp(int hp)
    {
        if (hp < prevHp - healthParams.HungerDamage)
        {
            state = SpritesEnum.Shoot;
            inShooting = true;
            StartCoroutine(Waiter());
        }
        if (hp == 0)
        {
            state = SpritesEnum.Dead;
            health.OnHpChanged -= SetStateByHp;
            health.OnHungerChanged -= SetStateByHunger;
        }
        prevHp = hp;
    }
    private void SetStateByHunger(int hg)
    {
        if (hg>0 && hg < healthParams.MaxHunger / 2)
            state = SpritesEnum.Hungry;
        else if (hg == 0)
            state = SpritesEnum.Starves;
        else
            if (hg > prevHunger)
            state = SpritesEnum.Idle;
        hg = prevHunger;
    }

    private IEnumerator Waiter()
    {
        yield return new WaitForSeconds(shootTime);
        inShooting = false;
        SetImage(currentState);
    }

    private void SetImage(SpritesEnum en)
    {
        switch (en)
        {
            case SpritesEnum.Idle:
                bob.sprite = bobIdle;
                gun.sprite = gunIdle;
                break;

            case SpritesEnum.Dead:
                bob.sprite = bobDead;
                gun.sprite = gunIdle;
                break;

            case SpritesEnum.Hungry:
                bob.sprite = bobHungry;
                gun.sprite = gunIdle;
                break;

            case SpritesEnum.Shoot:
                bob.sprite = bobShoot;
                gun.sprite = gunShoot;
                break;

            case SpritesEnum.Starves:
                bob.sprite = bobStarves;
                gun.sprite = gunIdle;
                break;
        }
        if (inShooting)
        {
            bob.sprite = bobShoot;
            gun.sprite = gunShoot;
        }
    }
}
