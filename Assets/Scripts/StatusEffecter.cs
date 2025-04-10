using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine.AI;

public class StatusEffecter : MonoBehaviour
{
    [Serializable]
    public class Effect
    {
        public StatusEffect statusEffect;
        public GameObject target;
        public float duration;

        public Effect(StatusEffect statusEffect, GameObject target, float duration)
        {
            this.statusEffect = statusEffect;
            this.target = target;
            this.duration = duration;
        }
    }

    public StatusEffect statusEffect;
    public float duration;
    public GameObject target;

    float timer;
    HeroController hero;

    public void Init(Effect effect)
    {
        statusEffect = effect.statusEffect;
        target = effect.target;
        duration = effect.duration;
        hero = target.GetComponent<HeroController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        timer = duration;

        switch(statusEffect)
        {
            case StatusEffect.Target:
                target.tag = "Control Target";
                break;

            case StatusEffect.Freeze:
                hero.SetFrozen(true);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0) EndEffect();
    }

    public void EndEffect()
    {
        switch (statusEffect)
        {
            case StatusEffect.Target:
                target.tag = "Untagged";
                break;

            case StatusEffect.Freeze:
                hero.SetFrozen(false);
                break;
        }

        Destroy(gameObject);
    }
}

public enum StatusEffect
{
    Target,
    Freeze
}
