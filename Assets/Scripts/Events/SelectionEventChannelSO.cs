using UnityEngine;
using UnityEngine.Events;

namespace Events
{
    /// <summary>
    /// This class is used for Events that have one SelectableObject Argument.
    /// </summary>
    [CreateAssetMenu(menuName = "Events/Object Event Channel")]
	public class SelectionEventChannelSO : ScriptableObject
	{
		public UnityAction<SelectableObject, bool> OnSelected;
        public UnityAction<SelectableObject> OnDeselected;

        public void Select(SelectableObject value, bool exclusive)
		{
            OnSelected?.Invoke(value, exclusive);
		}

        public void Deselect(SelectableObject value)
        {
            OnDeselected?.Invoke(value);
        }
    }
}