using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlePortraitManager : MonoBehaviour
{
    public static BattlePortraitManager instance = null;
    public GameObject battlePortrait, portraitHolder;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddPortrait(HeroController hero)
    {
        GameObject currentPortrait = Instantiate(battlePortrait, portraitHolder.transform);
        hero.SetPortrait(currentPortrait.GetComponent<BattlePortrait>());
    }
}
