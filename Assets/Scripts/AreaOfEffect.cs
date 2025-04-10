using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaOfEffect : MonoBehaviour
{
    public float lifetime;
    public EffectType effect;
    public HealStat effectedStat;
    public StatChangeType changeType;
    public float effectAmount;
    public LayerMask targetLayer;

    public bool addedEffect;
    public StatBooster.StatBoost additionalEffect;
    public GameObject statBooster;

    public void Init(AOE aoe)
    {
        lifetime = aoe.lifetime;
        effect = aoe.effect;
        effectedStat = aoe.effectedStat;
        changeType = aoe.changeType;
        effectAmount = aoe.effectAmount;
        targetLayer = aoe.targetLayer;

        addedEffect = aoe.addedEffect;

        if (addedEffect)
        {
            additionalEffect = aoe.additionalEffect;
            statBooster = aoe.statBooster;
        }
    }

    [Serializable]
    public class AOE
    {
        public float lifetime;
        public EffectType effect;
        public HealStat effectedStat;
        public StatChangeType changeType;
        public float effectAmount;
        public LayerMask targetLayer;

        public bool addedEffect;
        public StatBooster.StatBoost additionalEffect;
        public GameObject statBooster;
    }

    // Update is called once per frame
    void Update()
    {
        lifetime -= Time.deltaTime;
        if (lifetime <= 0) Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        HeroController hero = other.gameObject.GetComponent<HeroController>();
        if (LayerMask.GetMask(LayerMask.LayerToName(other.gameObject.layer)) == targetLayer)
        {
            if (effect == EffectType.Damaging) hero.TakeDamage(effectAmount, changeType);
            else
            {
                switch(effectedStat)
                {
                    case HealStat.Heatlh:
                        hero.Heal(Stat.Health, effectAmount, changeType);
                        break;

                    case HealStat.Energy:
                        hero.Heal(Stat.Energy, effectAmount, changeType);
                        break;

                    case HealStat.Both:
                        hero.Heal(Stat.Health, effectAmount, changeType);
                        hero.Heal(Stat.Energy, effectAmount, changeType);
                        break;
                }
            }

            if (addedEffect)
            {
                GameObject statBoost = Instantiate(statBooster, hero.transform);
                additionalEffect.hero = hero;
                statBoost.GetComponent<StatBooster>().Init(additionalEffect);
            }

        }
    }
}

public enum EffectType
{
    Healing,
    Damaging
}

public enum HealStat
{
    Heatlh,
    Energy,
    Both
}
