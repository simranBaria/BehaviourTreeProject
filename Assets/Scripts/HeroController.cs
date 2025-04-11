using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HeroController : MonoBehaviour
{
    [SerializeField]
    float HP, DEF, ATK, ENR;

    public float health, defense, attack, energy;

    public HeroState current, previous;

    public bool roundActive, frozen;
    public CharacterType type;
    public CharacterClasses characterClass;
    public GameObject initialTile;
    public TextMeshProUGUI classText;
    public Slider healthBar, energyBar;
    public Sprite redName, blueName;
    public Image nameDisplay;

    public void Init(CharacterType type, GameObject tile)
    {
        this.type = type;
        if (type == CharacterType.Ally)
        {
            gameObject.layer = LayerMask.NameToLayer("Allies");
            nameDisplay.sprite = blueName;
        }
        else
        {
            gameObject.layer = LayerMask.NameToLayer("Enemies");
            nameDisplay.sprite = redName;
        }

        initialTile = tile;

        switch(characterClass)
        {
            case CharacterClasses.Marksman:
                classText.text = "Marksman";
                break;

            case CharacterClasses.Tank:
                classText.text = "Tank";
                break;

            case CharacterClasses.Rogue:
                classText.text = "Rogue";
                break;

            case CharacterClasses.Mage:
                classText.text = "Mage";
                break;

            case CharacterClasses.Warrior:
                classText.text = "Warrior";
                break;

            case CharacterClasses.Support:
                classText.text = "Support";
                break;
        }

        healthBar.maxValue = 100;
        energyBar.maxValue = 100;
    }

    private void Start()
    {
        health = HP;
        defense = DEF;
        attack = ATK;
        energy = 0;
    }

    private void Update()
    {
        healthBar.value = GetHealthPercentage();
        energyBar.value = GetEnergyPercentage();
    }

    public void UpdateAnimation(HeroState state) => current = state;

    public float GetHealthPercentage() => health / HP * 100;

    public float GetEnergyPercentage() => energy / ENR * 100;

    public void TakeDamage(float damageAmount, StatChangeType changeType)
    {
        if (changeType == StatChangeType.Fixed)
        {
            float takenDamage = damageAmount - damageAmount * (GetStat(Stat.Defense) / 100);
            DecreaseStat(Stat.Health, takenDamage, changeType);
        }
        else DecreaseStat(Stat.Health, damageAmount, changeType);

        IncreaseStat(Stat.Energy, 25, StatChangeType.Fixed);
    }

    public void Heal(Stat stat, float healingAmount, StatChangeType changeType) => IncreaseStat(stat, healingAmount, changeType);

    public void Die()
    {
        Player.instance.RemoveHero(gameObject, type);
        UpdateAnimation(HeroState.Die);
    }

    public void DestroySelf()
    {
        gameObject.SetActive(false);
    }

    public void SetStat(Stat stat, float value)
    {
        switch(stat)
        {
            case Stat.Health:
                health = value;
                break;

            case Stat.Energy:
                energy = value;
                break;

            case Stat.Attack:
                attack = value;
                break;

            case Stat.Defense:
                defense = value;
                break;
        }
    }

    public float GetStat(Stat stat)
    {
        switch (stat)
        {
            case Stat.Health:
                return health;

            case Stat.Energy:
                return energy;

            case Stat.Attack:
                return attack;

            case Stat.Defense:
                return defense;
        }
        return 0;
    }

    public void SetToMax(Stat stat)
    {
        switch (stat)
        {
            case Stat.Health:
                SetStat(stat, HP);
                break;

            case Stat.Energy:
                SetStat(stat, ENR);
                break;
        }
    }

    public float GetMax(Stat stat)
    {
        switch (stat)
        {
            case Stat.Health:
                return HP;

            case Stat.Energy:
                return ENR;

            case Stat.Attack:
                break;

            case Stat.Defense:
                return 100;
        }
        return 0;
    }

    public float GetBase(Stat stat)
    {
        switch (stat)
        {
            case Stat.Health:
                return GetMax(stat);

            case Stat.Energy:
                return GetMax(stat);

            case Stat.Attack:
                return ATK;

            case Stat.Defense:
                return DEF;
        }
        return 0;
    }

    public void SetToBase(Stat stat)
    {
        switch (stat)
        {
            case Stat.Health:
                SetStat(stat, GetMax(stat));
                break;

            case Stat.Energy:
                SetStat(stat, GetMax(stat));
                break;

            case Stat.Attack:
                SetStat(stat, GetBase(stat));
                break;

            case Stat.Defense:
                SetStat(stat, GetBase(stat));
                break;
        }
    }

    public void DecreaseStat(Stat stat, float value, StatChangeType method)
    {
        switch(method)
        {
            case StatChangeType.Fixed:
                SetStat(stat, GetStat(stat) - value);
                break;

            case StatChangeType.Percentage:
                SetStat(stat, GetStat(stat) - GetBase(stat) * (value / 100));
                break;

            case StatChangeType.PercentageOfCurrent:
                SetStat(stat, GetStat(stat) - GetStat(stat) * (value / 100));
                break;
        }

        if (GetStat(stat) < 0) SetStat(stat, 0);
    }

    public void IncreaseStat(Stat stat, float value, StatChangeType method)
    {
        switch (method)
        {
            case StatChangeType.Fixed:
                SetStat(stat, GetStat(stat) + value);
                break;

            case StatChangeType.Percentage:
                SetStat(stat, GetStat(stat) + GetBase(stat) * (value / 100));
                break;

            case StatChangeType.PercentageOfCurrent:
                SetStat(stat, GetStat(stat) + GetStat(stat) * (value / 100));
                break;
        }

        if (GetStat(stat) > GetMax(stat)) SetToMax(stat);
    }

    public void SetToPercentage(Stat stat, float percentage)
    {
        SetStat(stat, GetMax(stat) * (percentage / 100));
    }

    public void SetFrozen(bool frozen) => this.frozen = frozen;

    public void SetRoundActive(bool active) => roundActive = active;
}

public enum Stat
{
    Health,
    Energy,
    Attack,
    Defense
}


public enum StatChangeType
{
    Fixed,
    Percentage,
    PercentageOfCurrent
}
