using UnityEngine;
using UnityEngine.Events;

namespace Events
{
	/// <summary>
	/// This class is used for Events that have no arguments (Example: Exit game event)
	/// </summary>
	[CreateAssetMenu(menuName = "Events/Void Event Channel")]
	public class VoidEventChannelSO : EventChannelBaseSO
	{
		public UnityAction OnEventRaised;

		public void RaiseEvent()
		{
			OnEventRaised?.Invoke();
		}
	}
}