using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController : MonoBehaviour
{
    public float HP, DEF, ATK;

    float health, defense, attack, energy;

    public float GetCurrentHealth() => health;

    public float GetMaxHealth() => HP;

    public void SetCurrentHealth(float health) { this.health = health; }

    public float GetCurrentEnergy() => energy;

    // I will make this a variable if it ever needs to not be 100
    // But right now it just makes sense for it to be 100
    // The only reason I could see it needing to be a variable rather than a hardcoded value is if some hero had an ability that increases how much energy you need for an ultimate
    public float GetMaxEnergy() => 100;

    public void SetCurrentEnergy(float energy) { this.energy = energy; }

    public float GetAttack() => attack;

    public void TakeDamage(float damage) { SetCurrentHealth(health - damage % defense); }

    // Just deactivate for now
    public void Die() { gameObject.SetActive(false); }

    public void SetStat(Stat stat, float value)
    {
        switch(stat)
        {
            case Stat.Health:
                SetCurrentHealth(value);
                break;

            case Stat.Energy:
                SetCurrentEnergy(value);
                break;
        }
    }

    public float GetStat(Stat stat)
    {
        switch (stat)
        {
            case Stat.Health:
                return GetCurrentHealth();

            case Stat.Energy:
                return GetCurrentEnergy();
        }
        return 0;
    }

    public void SetToMax(Stat stat)
    {
        switch (stat)
        {
            case Stat.Health:
                SetCurrentHealth(GetMaxHealth());
                break;

            case Stat.Energy:
                SetCurrentEnergy(GetMaxEnergy());
                break;
        }
    }

    public float GetMax(Stat stat)
    {
        switch (stat)
        {
            case Stat.Health:
                return GetMaxHealth();

            case Stat.Energy:
                return GetMaxEnergy();
        }
        return 0;
    }

    public void DecreaseStat(Stat stat, float value)
    {
        SetStat(stat, GetStat(stat) - value);
        if (GetStat(stat) < 0) SetStat(stat, 0);
    }

    public void IncreaseStat(Stat stat, float value)
    {
        SetStat(stat, GetStat(stat) + value);
        if (GetStat(stat) > GetMax(stat)) SetToMax(stat);
    }
}

public enum Stat
{
    Health,
    Energy
}
