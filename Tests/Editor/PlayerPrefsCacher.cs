using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WinterwireGames.OptionIOComponents.Editor.Tests
{
	public static class PlayerPrefsCacher
	{
		public enum PrefType
		{
			Int,
			Float,
			String
		}

		public class PrefSchema : IReadOnlyDictionary<string, PrefType>
		{
			private IReadOnlyDictionary<string, PrefType> dictionary = new Dictionary<string, PrefType>();

			public PrefType this[string key] => this.dictionary[key];

			public IEnumerable<string> Keys => this.dictionary.Keys;

			public IEnumerable<PrefType> Values => this.dictionary.Values;

			public int Count => this.dictionary.Count;

			public bool ContainsKey(string key) => this.dictionary.ContainsKey(key);

			public IEnumerator<KeyValuePair<string, PrefType>> GetEnumerator() => this.dictionary.GetEnumerator();

			public bool TryGetValue(string key, out PrefType value) => this.dictionary.TryGetValue(key, out value);

			IEnumerator IEnumerable.GetEnumerator() => this.dictionary.GetEnumerator();

			public PrefSchema(IReadOnlyDictionary<string, PrefType> dictionary)
			{
				this.dictionary = dictionary;
			}
		}

		public struct PrefValue
		{
			public object Value;
			public PrefType Type;
			public PrefValue(object value, PrefType type)
			{
				this.Value = value;
				this.Type = type;
			}
		}

		public class PrefCache : IReadOnlyDictionary<string, PrefValue>
		{
			private IReadOnlyDictionary<string, PrefValue> dictionary = new Dictionary<string, PrefValue>();

			public PrefValue this[string key] => this.dictionary[key];

			public IEnumerable<string> Keys => this.dictionary.Keys;

			public IEnumerable<PrefValue> Values => this.dictionary.Values;

			public int Count => this.dictionary.Count;

			public bool ContainsKey(string key) => this.dictionary.ContainsKey(key);

			public IEnumerator<KeyValuePair<string, PrefValue>> GetEnumerator() => this.dictionary.GetEnumerator();

			public bool TryGetValue(string key, out PrefValue value) => this.dictionary.TryGetValue((string)key, out value);

			IEnumerator IEnumerable.GetEnumerator() => this.dictionary.GetEnumerator();

			public PrefCache(IReadOnlyDictionary<string, PrefValue> dictionary)
			{
				this.dictionary = dictionary;
			}
		}

		private static object GetPref(string key, PrefType type)
		{
			if (!PlayerPrefs.HasKey(key))
				return null;
			return type switch
			{
				PrefType.Int => PlayerPrefs.GetInt(key),
				PrefType.Float => PlayerPrefs.GetFloat(key),
				PrefType.String => PlayerPrefs.GetString(key),
				_ => throw new System.Exception("Invalid pref type: " + type),
			};
		}

		private static void SetPref(string key, PrefType type, object value)
		{
			if (value is null)
			{
				PlayerPrefs.DeleteKey(key);
				return;
			}
			switch (type)
			{
				case PrefType.Int:
					PlayerPrefs.SetInt(key, (int)value);
					break;
				case PrefType.Float:
					PlayerPrefs.SetFloat(key, (float)value);
					break;
				case PrefType.String:
					PlayerPrefs.SetString(key, (string)value);
					break;
				default:
					throw new System.Exception("Invalid pref type: " + type);
			}
		}

		public static PrefCache SavePrefs(PrefSchema schema)
		{
			Dictionary<string, PrefValue> cached_prefs = new();
			foreach (var pref in schema)
			{
				object value = GetPref(pref.Key, pref.Value);
				cached_prefs.Add(pref.Key, new(value, pref.Value));
			}
			return new(cached_prefs);
		}

		public static void RestorePrefs(PrefCache cache)
		{
			foreach (var pref in cache)
				SetPref(pref.Key, pref.Value.Type, pref.Value.Value);
			PlayerPrefs.Save();
		}
	}
}
