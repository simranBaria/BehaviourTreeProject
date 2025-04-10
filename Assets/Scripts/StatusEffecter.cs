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
    }

    public StatusEffect statusEffect;
    public float duration;
    public GameObject target;

    Blackboard heroBB;
    float timer;

    public void Init(Effect effect)
    {
        statusEffect = effect.statusEffect;
        target = effect.target;
        duration = effect.duration;
    }

    // Start is called before the first frame update
    void Start()
    {
        heroBB = target.GetComponent<Blackboard>();
        timer = duration;

        switch(statusEffect)
        {
            case StatusEffect.Target:
                target.tag = "Control Target";
                break;

            case StatusEffect.Freeze:
                heroBB.enabled = false;
                target.GetComponent<NavMeshAgent>().isStopped = true;
                target.GetComponent<HeroController>().UpdateAnimation(HeroState.Idle);
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
        if(statusEffect == StatusEffect.Freeze)
        {
            heroBB.enabled = true;
            target.gameObject.GetComponent<NavMeshAgent>().isStopped = false;
        }

        switch (statusEffect)
        {
            case StatusEffect.Target:
                target.tag = "Untagged";
                break;

            case StatusEffect.Freeze:
                heroBB.enabled = true;
                target.GetComponent<NavMeshAgent>().isStopped = false;
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
