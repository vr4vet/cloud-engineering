// <copyright file="MoveJointTest.cs" company="VR4VET">
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
/// Test suite for the <see cref="MoveJoint"/> class.
/// </summary>
public class MoveJointTest
{
    /// <summary>
    /// A test to see if the method makes the lever return to center.
    /// </summary>
    [Test]
    public void TestMovingJoint()
    {
        GameObject testLever = new GameObject();
        testLever.AddComponent<MoveJoint>();
        testLever.AddComponent<Lever>();

        Assert.NotNull(testLever);

        testLever.GetComponent<Lever>().LeverPercentage = 45f;
        testLever.GetComponent<MoveJoint>().Lever = testLever.GetComponent<Lever>();

        Assert.AreEqual(testLever.GetComponent<MoveJoint>().Lever.LeverPercentage, 45);
        testLever.GetComponent<MoveJoint>().StartMovingLever();
        Assert.True(testLever.GetComponent<MoveJoint>().Lever.ReturnToCenter);
    }
}
