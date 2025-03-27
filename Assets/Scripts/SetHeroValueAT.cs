using NodeCanvas.Framework;
using ParadoxNotion.Design;


namespace NodeCanvas.Tasks.Actions {

	public class SetHeroValueAT : ActionTask {

		public Stat setStat;
		public SetType setType;
		public float value;
		HeroController hero;

		//Use for initialization. This is called only once in the lifetime of the task.
		//Return null if init was successfull. Return an error string otherwise
		protected override string OnInit() {
			hero = agent.GetComponent<HeroController>();
			return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {
			switch (setType)
			{
				case SetType.SetTo:
					hero.SetStat(setStat, value);
					break;

				case SetType.DecreaseBy:
					hero.DecreaseStat(setStat, value);
					break;

				case SetType.IncreaseBy:
					hero.IncreaseStat(setStat, value);
					break;
			}

			EndAction(true);
		}

		//Called once per frame while the action is active.
		protected override void OnUpdate() {
			
		}

		//Called when the task is disabled.
		protected override void OnStop() {
			
		}

		//Called when the task is paused.
		protected override void OnPause() {
			
		}
	}
}

public enum SetType
{
	SetTo,
	DecreaseBy,
	IncreaseBy
}