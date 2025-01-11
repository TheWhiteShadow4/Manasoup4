using UnityEngine;
using UnityEngine.Events;

namespace Events
{
    /// <summary>
    /// This class is used for Events that have one PointGeneration Argument.
    /// </summary>
    [CreateAssetMenu(menuName = "Events/Raid Event Channel")]
	public class RaidEventChannelSO : ScriptableObject
	{
		public UnityAction<PointGeneration> OnEventRaised;

        public void RaiseEvent(PointGeneration value)
		{
            OnEventRaised?.Invoke(value);
		}
    }
}