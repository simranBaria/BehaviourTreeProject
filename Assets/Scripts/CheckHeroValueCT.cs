using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace NodeCanvas.Tasks.Conditions {

	public class CheckHeroValueCT : ConditionTask {

		public Stat checkStat;
		public CheckType checkType;
		public float value;
		HeroController hero;

		//Use for initialization. This is called only once in the lifetime of the task.
		//Return null if init was successfull. Return an error string otherwise
		protected override string OnInit(){
			hero = agent.GetComponent<HeroController>();
			return null;
		}

		//Called whenever the condition gets enabled.
		protected override void OnEnable() {
			
		}

		//Called whenever the condition gets disabled.
		protected override void OnDisable() {
			
		}

		//Called once per frame while the condition is active.
		//Return whether the condition is success or failure.
		protected override bool OnCheck() {
            switch(checkType)
            {
				case CheckType.GreaterThanInclusive:
					return hero.GetStat(checkStat) >= value;

				case CheckType.LessThanInclusive:
					return hero.GetStat(checkStat) <= value;

				case CheckType.Maxed:
					return hero.GetStat(checkStat) == hero.GetMax(checkStat);

				case CheckType.EqualTo:
					return hero.GetStat(checkStat) == value;
			}

			return false;
		}
	}
}

public enum CheckType
{
	GreaterThanInclusive,
	LessThanInclusive,
	Maxed,
	EqualTo
}