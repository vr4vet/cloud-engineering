// <copyright file="ChangeSnapStatusTest.cs" company="VR4VET">
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

using BNG;
using NUnit.Framework;
using UnityEngine;

/// <summary>
/// Test suite for the <see cref="ChangeSnapStatus"/> class.
/// </summary>
public class ChangeSnapStatusTest
{
    /// <summary>
    /// A test to see if the method changes the snap status when it should.
    /// </summary>
    [Test]
    public void TestChangeSnapStatus()
    {
        // Create lever with script on it
        GameObject testThisLever = new GameObject();
        testThisLever.AddComponent<ChangeSnapStatus>();
        testThisLever.AddComponent<Lever>();

        Assert.NotNull(testThisLever);

        // Create the snap point where the object will be held
        GameObject testSnapPoint = new GameObject();
        testSnapPoint.AddComponent<Grabbable>();
        testSnapPoint.AddComponent<BoxCollider>();
        testSnapPoint.AddComponent<SnapZone>();
        testSnapPoint.AddComponent<SphereCollider>();

        // Create the second lever
        GameObject testLeverOther = new GameObject();
        testLeverOther.AddComponent<Lever>();

        // Connect the snap point and the second lever to the tested lever
        testThisLever.GetComponent<ChangeSnapStatus>().SetTestData(testSnapPoint, testLeverOther);
        testThisLever.GetComponent<ChangeSnapStatus>().LoadLevers();

        Assert.AreEqual(testThisLever.GetComponent<ChangeSnapStatus>().LeverThis, testThisLever.GetComponent<Lever>());
        Assert.AreEqual(testThisLever.GetComponent<ChangeSnapStatus>().LeverOther, testLeverOther.GetComponent<Lever>());
        Assert.AreNotSame(testThisLever.GetComponent<ChangeSnapStatus>().LeverThis, testThisLever.GetComponent<ChangeSnapStatus>().LeverOther);

        testThisLever.GetComponent<ChangeSnapStatus>().LoadLeverPercentage();
        Assert.AreEqual(testThisLever.GetComponent<ChangeSnapStatus>().LeverThis.LeverPercentage, 0f);
        Assert.AreEqual(testThisLever.GetComponent<ChangeSnapStatus>().LeverOther.LeverPercentage, 0f);

        // Make sure the grabbable in the snap point is disabled
        testThisLever.GetComponent<ChangeSnapStatus>().LeverCheck();
        Assert.False(testSnapPoint.GetComponent<Grabbable>().enabled);

        // Test if the grabbable gets enabled when the levers are opened
        testThisLever.GetComponent<ChangeSnapStatus>().LeverOther.LeverPercentage = 100f;
        testThisLever.GetComponent<ChangeSnapStatus>().LoadLeverPercentage();
        testThisLever.GetComponent<ChangeSnapStatus>().LeverCheck();
        Assert.True(testSnapPoint.GetComponent<Grabbable>().enabled);

        // Put object into the snap point
        Grabbable testObject = new GameObject().AddComponent<Grabbable>();
        testSnapPoint.GetComponent<SnapZone>().HeldItem = testObject;
        Assert.NotNull(testSnapPoint.GetComponent<SnapZone>().HeldItem);
        testThisLever.GetComponent<ChangeSnapStatus>().LeverCheck();
        Assert.True(testSnapPoint.GetComponent<Grabbable>().enabled);
    }
}
