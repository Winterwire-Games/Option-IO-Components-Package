using UnityEngine;

namespace WinterwireGames.OptionIOComponents
{
	public abstract class OptionWriter<ValueType> : MonoBehaviour
	{
		[SerializeField]
		[Tooltip("The name of the option to write")]
		public string OptionName;

		[SerializeField]
		[Tooltip("Whether this should save all options when it writes")]
		private bool _shouldSaveOnWrite = false;

		public bool ShouldSaveOnWrite { get => this._shouldSaveOnWrite; }

		[SerializeField]
		[Tooltip("The option saver used to save on write")]
		private OptionSaver _saver;

		public OptionSaver Saver { get => this._saver; }

		public ValueType Value
		{
			set
			{
				OptionSaver.IsDirty = true;
				this.WriteOption(this.OptionName, value);
				if (this.ShouldSaveOnWrite)
					this.Saver.Save();
			}
		}

		protected abstract void WriteOption(string name, ValueType value);
	}
}
