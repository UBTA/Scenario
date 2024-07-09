using System;
using UnityEngine;

namespace EblanDev.ScenarioCore.UIFramework.UIElements
{
    public class SwitchView : ClickableView
    {
        public event Action<bool> OnSwitchEvent;

        [SerializeField] private View trueState;
        [SerializeField] private View falseState;
        
        private bool state;

        public bool GetState => state;
        
        public override void Init()
        {
            base.Init();
            
            if (trueState != null)
            {
                trueState.Init();
            }
                
            if (falseState != null)
            {
                falseState.Init();
            }

            OnClickEvent += () => SetState(!state);
        }

        public void SetState(bool condition)
        {
            state = condition;
            ApplyViews();
            OnSwitchEvent?.Invoke(state);
        }

        private void ApplyViews()
        {
            if (state)
            {
                if (trueState != null)
                {
                    trueState.Show();
                }
                
                if (falseState != null)
                {
                    falseState.Hide();
                }
            }
            else
            {
                if (trueState != null)
                {
                    trueState.Hide();
                }
                
                if (falseState != null)
                {
                    falseState.Show();
                }
            }
        }
    }
}