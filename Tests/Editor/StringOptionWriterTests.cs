using System.Collections.Generic;
using NUnit.Framework;
using NUnit.Framework.Internal;
using UnityEngine;
using static WinterwireGames.OptionIOComponents.Editor.Tests.PlayerPrefsCacher;

namespace WinterwireGames.OptionIOComponents.Editor.Tests
{
	public class StringOptionWriterTests
	{
		public static readonly string OPTION_NAME = "Test Option";
		public static PrefSchema CACHE_SCHEMA { get; } = new(new Dictionary<string, PrefType>
		{
			{ OPTION_NAME, PrefType.String }
		});

		private static Randomizer Randomizer = new Randomizer();
		private PrefCache Cache;
		private GameObject GameObject;
		private StringOptionWriter StringOptionWriter;

		[SetUp]
		public void SetUp()
		{
			this.Cache = SavePrefs(CACHE_SCHEMA);
			this.GameObject = new("Test");
			this.StringOptionWriter = this.GameObject.AddComponent<StringOptionWriter>();
			this.StringOptionWriter.OptionName = OPTION_NAME;
		}

		[TearDown]
		public void TearDown()
		{
			Object.DestroyImmediate(this.GameObject);
			RestorePrefs(this.Cache);
		}

		public static IEnumerable<string> RandomStrings()
		{
			for (int i = 0; i < 10; ++i)
				yield return StringOptionWriterTests.Randomizer.GetString();
		}

		[Test]
		public void WritesStrings(
			[ValueSource("RandomStrings")]
			string value)
		{
			this.StringOptionWriter.Value = value;
			Assert.AreEqual(value, PlayerPrefs.GetString(OPTION_NAME));
		}

		[Test]
		public void OverwritesStrings(
			[ValueSource("RandomStrings")]
			string value)
		{
			this.StringOptionWriter.Value = value + "a";
			Assert.AreNotEqual(value, PlayerPrefs.GetString(OPTION_NAME));
			this.StringOptionWriter.Value = value;
			Assert.AreEqual(value, PlayerPrefs.GetString(OPTION_NAME));
		}
	}
}
