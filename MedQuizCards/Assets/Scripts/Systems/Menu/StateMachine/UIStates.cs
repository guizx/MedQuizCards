using Nato.Singleton;
using UnityEngine;

namespace Nato.StateMachine
{
    public class UIStates : Singleton<UIStates>
    {
        [field: Header("Template States")]
        [field: SerializeField] public UIAchievementsState AchievementsState { get; private set; }


        protected override void Awake()
        {
            base.Awake();
            //Global
            AchievementsState = new UIAchievementsState();
        }

        private void OnDestroy()
        {
            //Global
            AchievementsState.OnDestroyTick();
        }
    }
}
