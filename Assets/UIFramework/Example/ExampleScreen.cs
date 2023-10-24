using FAwesome.ScenarioCore.GameFramework.Example;
using FAwesome.ScenarioCore.UIFramework.UIElements;
using UnityEngine;

namespace ScenarioCore.UIFramework.Example
{
    public class ExampleScreen : ScreenView<ExampleBus>
    {
        [SerializeField] private CounterView sessionsCounter;
        [SerializeField] private CounterView timeCounter;
        
        protected override void OnInit()
        {
            sessionsCounter.Init();
            timeCounter.Init();
            
            Bus.GameTimer.E += OnTimerCount;

            base.OnInit();
        }

        protected override void OnShowStart(bool immediately)
        {
            sessionsCounter.SetValue(Bus.GameSession.LastInvokeData.ToString());
            
            sessionsCounter.Show();
            timeCounter.Show();
        }

        private void OnTimerCount(float time)
        {
            timeCounter.SetValue(time.ToString());
        }
    }
}