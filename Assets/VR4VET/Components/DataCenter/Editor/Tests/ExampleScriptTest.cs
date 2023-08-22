// <copyright file="ExampleScriptTest.cs" company="VR4VET">
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

using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

/// <summary>
/// A test script that test ExampleScript.
/// </summary>
public class ExampleScriptTest
{
    /// <summary>
    /// A Test behaves as an ordinary method.
    /// </summary>
    [Test]
    public void ExampleScriptTestSimplePasses()
    {
        // Use the Assert class to test conditions
        GameObject gameObject = new();
        ExampleScript exampleScript = gameObject.AddComponent<ExampleScript>();
        Assert.AreEqual(exampleScript.DeepThought(), 42);
    }

    /// <summary>
    /// A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    /// `yield return null;` to skip a frame.
    /// </summary>
    /// <returns>An Enumerator with one value per frame.</returns>
    [UnityTest]
    public IEnumerator ExampleScriptTestWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}
