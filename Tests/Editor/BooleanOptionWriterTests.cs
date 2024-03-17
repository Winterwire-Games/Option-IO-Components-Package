using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using static WinterwireGames.OptionIOComponents.Editor.Tests.PlayerPrefsCacher;

namespace WinterwireGames.OptionIOComponents.Editor.Tests
{
	public class BooleanOptionWriterTests
	{
		public static readonly string OPTION_NAME = "Test Option";
		public static PrefSchema CACHE_SCHEMA { get; } = new(new Dictionary<string, PrefType>
		{
			{ OPTION_NAME, PrefType.Int }
		});
		private PrefCache Cache;
		private GameObject GameObject;
		private BooleanOptionWriter BooleanOptionWriter;

		[SetUp]
		public void SetUp()
		{
			this.Cache = SavePrefs(CACHE_SCHEMA);
			this.GameObject = new("Test");
			this.BooleanOptionWriter = this.GameObject.AddComponent<BooleanOptionWriter>();
			this.BooleanOptionWriter.OptionName = OPTION_NAME;
		}

		[TearDown]
		public void TearDown()
		{
			Object.DestroyImmediate(this.GameObject);
			RestorePrefs(this.Cache);
		}

		[TestCase(true, 1)]
		[TestCase(false, 0)]
		public void WritesBooleans(bool written_value, int expected_value)
		{
			this.BooleanOptionWriter.Value = written_value;
			Assert.AreEqual(expected_value, PlayerPrefs.GetInt(OPTION_NAME));
		}

		[TestCase(true, 1)]
		[TestCase(false, 0)]
		public void OverwritesBooleans(bool written_value, int expected_value)
		{
			this.BooleanOptionWriter.Value = !written_value;
			Assert.AreNotEqual(expected_value, PlayerPrefs.GetInt(OPTION_NAME));
			this.BooleanOptionWriter.Value = written_value;
			Assert.AreEqual(expected_value, PlayerPrefs.GetInt(OPTION_NAME));
		}
	}
}
