// <copyright file="IntroductionNPCTest.cs" company="VR4VET">
// MIT License
//
// Copyright (c) 2023 VR4VET
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
// </copyright>

using NUnit.Framework;
using TMPro;
using UnityEngine;

/// <summary>
/// Test suite for the <see cref="IntroductionNPC"/> class.
/// </summary>
public class IntroductionNPCTest
{
    /// <summary>
    /// A test to see if the text changes when called.
    /// </summary>
    [Test]
    public void TestNextPage()
    {
        GameObject textPrefab = new GameObject();
        textPrefab.AddComponent<TextMeshProUGUI>();
        textPrefab.GetComponent<TextMeshProUGUI>().text = string.Empty;
        IntroductionNPC introductionNPC = new GameObject().AddComponent<IntroductionNPC>();

        Assert.NotNull(introductionNPC);

        introductionNPC.TextPrefab = textPrefab;

        Assert.AreEqual(introductionNPC.TextPrefab.GetComponent<TextMeshProUGUI>().text, string.Empty);

        introductionNPC.NextPage();

        Assert.AreEqual(introductionNPC.TextPrefab.GetComponent<TextMeshProUGUI>().text, "First page");

        introductionNPC.NextPage();

        Assert.AreEqual(introductionNPC.TextPrefab.GetComponent<TextMeshProUGUI>().text, "Second page");
    }

    /// <summary>
    /// A test to see if the rest of intro NPC works as intended.
    /// </summary>
    [Test]
    public void TestIntroductionNPC()
    {
        GameObject npcObject = new GameObject();
        npcObject.AddComponent<Canvas>();
        npcObject.AddComponent<IntroductionNPC>();
        GameObject handsObject = new GameObject();
        handsObject.tag = "Hands";

        npcObject.GetComponent<IntroductionNPC>().Start();
        npcObject.GetComponent<IntroductionNPC>().FixedUpdate();

        npcObject.GetComponent<IntroductionNPC>().DetectionRadius = 10f;
        npcObject.GetComponent<IntroductionNPC>().FixedUpdate();

        Assert.NotNull(npcObject.GetComponent<IntroductionNPC>());
        Assert.NotNull(npcObject.GetComponent<Canvas>());
    }
}
