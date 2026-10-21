using UnityEngine;

[CreateAssetMenu(fileName = "HealthParams", menuName = "Scriptable Objects/HealthParams")]
public class HealthParams : ScriptableObject
{
    public int MaxHp;
    public int MaxHunger;
    public int HungerPerTime;
    public int HungerDamage;
    public float TimeForUpdate;
}
