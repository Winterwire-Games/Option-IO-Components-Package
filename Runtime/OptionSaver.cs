using UnityEngine;

namespace WinterwireGames.OptionIOComponents
{
	[AddComponentMenu("Options/Option Saver")]
	public class OptionSaver : MonoBehaviour
	{
		[Header("Save Triggers")]

		[SerializeField]
		[Tooltip("Whether this should save all option changes when it is destroyed")]
		private bool _shouldSaveOnDestroy = false;

		public bool ShouldSaveOnDestroy { get => this._shouldSaveOnDestroy; }

		[SerializeField]
		[Tooltip("Whether this should save all option changes when it is disabled")]
		private bool _shouldSaveOnDisable = false;

		public bool ShouldSaveOnDisable { get => this._shouldSaveOnDisable; }

		/** Whether unsaved changes to options exist */
		public static bool IsDirty { get; set; } = false;

		public void Save()
		{
			if (OptionSaver.IsDirty)
			{
				OptionSaver.IsDirty = false;
				PlayerPrefs.Save();
			}
		}

		protected void OnDisable()
		{
			if (this.ShouldSaveOnDisable)
				this.Save();
		}

		protected void OnDestroy()
		{
			if (this.ShouldSaveOnDestroy)
				this.Save();
		}
	}
}