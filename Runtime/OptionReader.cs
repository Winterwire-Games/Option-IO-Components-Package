using UnityEngine;
using UnityEngine.Events;

namespace WinterwireGames.OptionIOComponents
{
	public abstract class OptionReader<ValueType> : MonoBehaviour
	{
		[SerializeField]
		[Tooltip("The name of the option to read")]
		public string OptionName;

		[SerializeField]
		[Tooltip("The value to use if the option cannot be read")]
		public ValueType DefaultValue;

		public ValueType Value
		{
			get => this.Read();
		}

		[SerializeField]
		[Tooltip("Whether this should read its value when enabled")]
		public bool ShouldReadOnEnable = false;

		[SerializeField]
		[Tooltip("Invoked with the red value when red")]
		private UnityEvent<ValueType> _onRead = new UnityEvent<ValueType>();

		public UnityEvent<ValueType> OnRead { get => this._onRead; }

		protected abstract ValueType ReadValue(string name, ValueType default_value);

		public ValueType Read()
		{
			var value = this.ReadValue(this.OptionName, this.DefaultValue);
			this.OnRead.Invoke(value);
			return value;
		}

		private void OnEnable()
		{
			if (this.ShouldReadOnEnable)
				this.Read();
		}
	}
}
