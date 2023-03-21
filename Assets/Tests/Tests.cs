using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.TestTools.Utils;

public class Tests
{
    [Test]
    [TestCase("Gutter", new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, "--------------------", new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 })]
    [TestCase("Threes", new int[] { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 }, "33333333333333333333", new int[] { 6, 12, 18, 24, 30, 36, 42, 48, 54, 60 })]
    [TestCase("Spare", new int[] { 4, 6, 3, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, "4/35----------------", new int[] { 13, 21, 21, 21, 21, 21, 21, 21, 21, 21 })]
    [TestCase("Spare2", new int[] { 4, 6, 5, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, "4/53----------------", new int[] { 15, 23, 23, 23, 23, 23, 23, 23, 23, 23 })]
    [TestCase("Strike", new int[] { 10, 5, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, "X53----------------", new int[] { 18, 26, 26, 26, 26, 26, 26, 26, 26, 26 })]
    [TestCase("Strike final frame", new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 10, 5, 3 }, "------------------X53", new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 18 })]
    [TestCase("Spare final frame", new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 6, 5 }, "------------------4/5", new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 15 })]
    [TestCase("Perfect game", new int[] { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 }, "XXXXXXXXXXXX", new int[] { 30, 60, 90, 120, 150, 180, 210, 240, 270, 300 })]
    [TestCase("Alternating", new int[] { 10, 4, 6, 10, 4, 6, 10, 4, 6, 10, 4, 6, 10, 4, 6, 10 }, "X4/X4/X4/X4/X4/X", new int[] { 20, 40, 60, 80, 100, 120, 140, 160, 180, 200 })]
    [TestCase("Example game", new int[] { 8, 1, 0, 9, 2, 8, 10, 6, 3, 7, 0, 5, 2, 10, 0, 6, 2, 8, 10 }, "81-92/X637-52X-62/X", new int[] { 9, 18, 38, 57, 66, 73, 80, 96, 102, 122 })]
    public void TestGameScoring(string name, int[] rolls, string expectedMarks, int[] expectedScores)
    {
        var scorecard = new Scorecard(name);
        Scoring.Score(new Rolls(rolls), scorecard);
        var expected = new Scorecard(name, expectedMarks, expectedScores);
        Assert.That(scorecard, Is.EqualTo(expected));
    }
}
