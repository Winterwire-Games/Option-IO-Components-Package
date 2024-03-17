using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using static WinterwireGames.OptionIOComponents.Editor.Tests.PlayerPrefsCacher;

namespace WinterwireGames.OptionIOComponents.Editor.Tests
{
	public class FloatOptionWriterTests
	{
		public static readonly string OPTION_NAME = "Test Option";

		public static PrefSchema CACHE_SCHEMA = new(new Dictionary<string, PrefType>
		{
			{ OPTION_NAME, PrefType.Float }
		});

		private FloatOptionWriter FloatOptionWriter;
		private GameObject GameObject;
		private PrefCache Cache;

		[SetUp]
		public void SetUp()
		{
			this.Cache = SavePrefs(CACHE_SCHEMA);
			this.GameObject = new("Test");
			this.FloatOptionWriter = this.GameObject.AddComponent<FloatOptionWriter>();
			this.FloatOptionWriter.OptionName = OPTION_NAME;
		}

		[TearDown]
		public void TearDown()
		{
			Object.DestroyImmediate(this.GameObject);
			RestorePrefs(this.Cache);
		}

		[Test]
		public void WritesFloats(
			[Random(float.MinValue, float.MaxValue, 10)]
			float value)
		{
			this.FloatOptionWriter.Value = value;
			Assert.AreEqual(value, PlayerPrefs.GetFloat(OPTION_NAME));
		}

		[Test]
		public void OverwritesFloats(
			[Random(float.MinValue, float.MaxValue, 10)]
			float value)
		{
			this.FloatOptionWriter.Value = value == 0 ? -1f : -value;
			Assert.AreNotEqual(value, PlayerPrefs.GetFloat(OPTION_NAME));
			this.FloatOptionWriter.Value = value;
			Assert.AreEqual(value, PlayerPrefs.GetFloat(OPTION_NAME));
		}
	}
}
