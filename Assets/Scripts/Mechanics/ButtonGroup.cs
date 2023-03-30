using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public enum ButtonGroupType
    {
        All,
        Any
    }
    
    public class ButtonGroup : MonoBehaviour
    {
        [SerializeField] private UnityEvent onGroupEnabled;
        [SerializeField] private UnityEvent onGroupDisabled;
        [SerializeField] private ButtonGroupType groupType = ButtonGroupType.All;

        [SerializeField] private Button[] buttons;

        private bool _groupActive;
        
        public void UpdateButtonsEnableState()
        {
            if (!enabled)
                return;
            
            bool newGroupState = UpdateGroupActiveState();
            if (newGroupState == _groupActive)
                return;
            _groupActive = newGroupState;
            
            if (newGroupState)
                onGroupEnabled?.Invoke();
            else
                onGroupDisabled?.Invoke();
        }

        private bool UpdateGroupActiveState()
        {
            if (groupType == ButtonGroupType.All)
                return buttons.All(button => button.buttonEnabled);
            return buttons.Any(button => button.buttonEnabled);
        }
    }
}