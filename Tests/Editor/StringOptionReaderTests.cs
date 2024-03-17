using System.Collections.Generic;
using NUnit.Framework;
using NUnit.Framework.Internal;
using UnityEngine;
using static WinterwireGames.OptionIOComponents.Editor.Tests.PlayerPrefsCacher;

namespace WinterwireGames.OptionIOComponents.Editor.Tests
{
	public class StringOptionReaderTests
	{
		public static readonly string OPTION_NAME = "Test Option";
		public static PrefSchema CACHE_SCHEMA => new(new Dictionary<string, PrefType>
		{
			{ OPTION_NAME, PrefType.String }
		});

		private GameObject GameObject;
		private StringOptionReader StringOptionReader;
		private PrefCache Cache;

		[SetUp]
		public void SetUp()
		{
			this.Cache = PlayerPrefsCacher.SavePrefs(CACHE_SCHEMA);
			this.GameObject = new GameObject("Test");
			this.StringOptionReader = this.GameObject.AddComponent<StringOptionReader>();
			this.StringOptionReader.OptionName = OPTION_NAME;
		}

		[TearDown]
		public void TearDown()
		{
			Object.DestroyImmediate(this.GameObject);
			PlayerPrefsCacher.RestorePrefs(this.Cache);
		}

		private static Randomizer Randomizer = new Randomizer();

		public static IEnumerable<string> RandomStrings()
		{
			for (int i = 0; i < 10; ++i)
				yield return StringOptionReaderTests.Randomizer.GetString();
		}

		[Test]
		public void ReadsStrings(
			[ValueSource("RandomStrings")]
			string value)
		{
			PlayerPrefs.SetString(OPTION_NAME, value);
			Assert.AreEqual(value, this.StringOptionReader.Value);
		}

		[Test]
		[Repeat(10)]
		public void ReadsDefaults(
			[ValueSource("RandomStrings")]
			string default_value)
		{
			PlayerPrefs.DeleteKey(OPTION_NAME);
			this.StringOptionReader.DefaultValue = default_value;
			Assert.AreEqual(default_value, this.StringOptionReader.Value);
		}
	}
}
