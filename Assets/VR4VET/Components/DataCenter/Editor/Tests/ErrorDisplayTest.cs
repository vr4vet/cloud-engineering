// <copyright file="ErrorDisplayTest.cs" company="VR4VET">
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
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Test suite for the <see cref="ErrorDisplay"/> class.
/// </summary>
public class ErrorDisplayTest
{
    /// <summary>
    /// A test to see if the method actually changes the text.
    /// </summary>
    [Test]
    public void TestChangeText()
    {
        // Creating a new text object.
        GameObject testObject = new GameObject("Text");
        Text testTextComponent = testObject.AddComponent<Text>();

        // The script is added as a component to the test gameobject to access the methods.
        ErrorDisplay errorDisplay2 = testObject.AddComponent<ErrorDisplay>();
        errorDisplay2.ChangeText(testTextComponent, "The client has requested additional ram in rack 4, server 3.");
        Assert.AreEqual(
            "The client has requested additional ram in rack 4, server 3.",
            testTextComponent.text.ToString());
    }
}
