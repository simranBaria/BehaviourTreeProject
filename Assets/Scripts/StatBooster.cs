using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatBooster : MonoBehaviour
{
    //public StatBoost statBoost;

    [Serializable]
    public class StatBoost
    {
        public SetType type;
        public HeroController hero;
        public Stat stat;
        public float duration, amount;
        public StatChangeType method;
    }

    public SetType type;
    public HeroController hero;
    public Stat stat;
    public float duration, amount;
    public StatChangeType method;

    float timer;

    public void Init(StatBoost boost)
    {
        type = boost.type;
        hero = boost.hero;
        stat = boost.stat;
        duration = boost.duration;
        amount = boost.amount;
        method = boost.method;
    }

    // Start is called before the first frame update
    void Start()
    {
        switch(type)
        {
            case SetType.IncreaseBy:
                hero.IncreaseStat(stat, amount, method);
                break;

            case SetType.DecreaseBy:
                hero.DecreaseStat(stat, amount, method);
                break;

            case SetType.SetTo:
                if (method == StatChangeType.Fixed) hero.SetStat(stat, amount);
                else hero.SetToPercentage(stat, amount);
                break;
        }

        timer = duration;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0) EndBoost();
    }

    //public void Set(StatBoost boost) => statBoost = boost;

    public void EndBoost()
    {
        if (type == SetType.IncreaseBy) hero.DecreaseStat(stat, amount, method);
        else hero.IncreaseStat(stat, amount, method);

        Destroy(gameObject);
    }
}
