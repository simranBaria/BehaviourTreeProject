using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattlePortrait : MonoBehaviour
{
    public CharacterClasses character;

    public Sprite[] portraits;
    public Sprite[] ultimatePortraits;

    public Sprite portrait, ultimate;

    public Image portraitObject;

    public Slider healthSlider, energySlider;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetCharacter(CharacterClasses character)
    {
        this.character = character;
        portrait = portraits[(int)character - 1];
        ultimate = ultimatePortraits[(int)character - 1];

        portraitObject.sprite = portrait;
    }

    public void DefaultPortraitMode()
    {
        portraitObject.sprite = portrait;
    }

    public void UltimatePortraitMode()
    {
        portraitObject.sprite = ultimate;
    }
}
