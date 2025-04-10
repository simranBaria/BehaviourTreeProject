using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public List<HeroState> animationStates;
    public SpriteRenderer SR;

    HeroController hero;
    Animator animator;
    List<AnimatorState> states;

    [Serializable]
    public class AnimatorState
    {
        public HeroState state;
        public int hash;

        public AnimatorState(HeroState state, int hash)
        {
            this.state = state;
            this.hash = hash;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        hero = gameObject.GetComponent<HeroController>();
        animator = gameObject.GetComponent<Animator>();
        states = new();

        foreach (HeroState heroState in animationStates) states.Insert(animationStates.IndexOf(heroState), new(heroState, Animator.StringToHash(StateToString(heroState))));
    }

    // Update is called once per frame
    void Update()
    {
        if(hero.current != hero.previous)
        {
            foreach(AnimatorState state in states)
            {
                if (hero.current == state.state)
                {
                    animator.CrossFade(state.hash, 0f);
                    break;
                }
            }
        }

        hero.previous = hero.current;

        if (transform.rotation.eulerAngles.y < Camera.main.transform.rotation.eulerAngles.y && !SR.flipX) SR.flipX = true;
        else if(transform.rotation.eulerAngles.y >= Camera.main.transform.rotation.eulerAngles.y && SR.flipX) SR.flipX = false;
    }

    public string StateToString(HeroState state)
    {
        switch (state)
        {
            case HeroState.Idle:
                return "Idle";

            case HeroState.Walk:
                return "Walk";

            case HeroState.Action:
                return "Action";

            case HeroState.Dash:
                return "Dash";

            case HeroState.Die:
                return "Die";
            case HeroState.Frozen:
                return "Frozen";
            default:
                return "";
        }
    }
}

public enum HeroState
{
    Idle,
    Walk,
    Action,
    Dash,
    Die,
    Frozen
}
