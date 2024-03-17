using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using static WinterwireGames.OptionIOComponents.Editor.Tests.PlayerPrefsCacher;

namespace WinterwireGames.OptionIOComponents.Editor.Tests
{
	public class IntegerOptionWriterTests
	{
		public static readonly string OPTION_NAME = "Test Option";
		public static PrefSchema CACHE_SCHEMA { get; } = new(new Dictionary<string, PrefType>
		{
			{ OPTION_NAME, PrefType.Int }
		});
		private PrefCache Cache;
		private GameObject GameObject;
		private IntegerOptionWriter IntegerOptionWriter;

		[SetUp]
		public void SetUp()
		{
			this.Cache = SavePrefs(CACHE_SCHEMA);
			this.GameObject = new("Test");
			this.IntegerOptionWriter = this.GameObject.AddComponent<IntegerOptionWriter>();
			this.IntegerOptionWriter.OptionName = OPTION_NAME;
		}

		[TearDown]
		public void TearDown()
		{
			Object.DestroyImmediate(this.GameObject);
			RestorePrefs(this.Cache);
		}

		[Test]
		public void WritesIntegers(
			[Random(int.MinValue, int.MaxValue, 10)]
			int value)
		{
			this.IntegerOptionWriter.Value = value;
			Assert.AreEqual(value, PlayerPrefs.GetInt(OPTION_NAME));
		}

		[Test]
		public void OverwritesIntegers(
			[Random(int.MinValue, int.MaxValue, 10)]
			int value)
		{
			this.IntegerOptionWriter.Value = value == int.MaxValue ? value - 1 : value + 1;
			Assert.AreNotEqual(value, PlayerPrefs.GetInt(OPTION_NAME));
			this.IntegerOptionWriter.Value = value;
			Assert.AreEqual(value, PlayerPrefs.GetInt(OPTION_NAME));
		}
	}
}
