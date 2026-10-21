using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    [SerializeField] private Health health;
    [SerializeField] private HealthParams healthParams;

    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Slider hpBar;
    [SerializeField] private Slider hungerBar;
    [SerializeField] private CanvasGroup btns;

    private void Awake()
    {
        hpBar.interactable = false;
        hungerBar.interactable = false;
        hpBar.maxValue = healthParams.MaxHp;
        hungerBar.maxValue = healthParams.MaxHunger;

        health.OnHpChanged += ChangeHpBar;
        health.OnHungerChanged += ChangeHungerBar;
        health.OnDied += ChangeLabel;
    }
    
    private void ChangeHpBar(int hp)
    {
        hpBar.value = hp;
    }

    private void ChangeHungerBar(int hg)
    {
        hungerBar.value = hg;
    }

    private void ChangeLabel()
    {
        text.text = "Bob's dead";
        btns.interactable = false;
    }
}
