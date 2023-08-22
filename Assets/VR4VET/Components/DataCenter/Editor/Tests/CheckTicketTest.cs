// <copyright file="CheckTicketTest.cs" company="VR4VET">
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
using UnityEngine.UI;

/// <summary>
/// Test suite for the <see cref="CheckTicket"/> class.
/// </summary>
public class CheckTicketTest
{
    /// <summary>
    /// A test to see if the method actually gets the selected test.
    /// </summary>
    [Test]
    public void TestGetSelectedText()
    {
        // Creating a new dropdown object.
        GameObject testDropdownObject = new GameObject("TMP_Dropdown");
        TMP_Dropdown testDropdownComponent = testDropdownObject.AddComponent<TMP_Dropdown>();
        Assert.AreNotEqual(testDropdownComponent, null);

        // Options need to be added
        testDropdownComponent.options.Add(new TMP_Dropdown.OptionData() { text = "test1" });

        // A gameobject is created, to which the script is added as a component in order to access the methods.
        GameObject gameObject = new();
        CheckTicket checkTicket = gameObject.AddComponent<CheckTicket>();
        Assert.AreEqual("test1", checkTicket.GetSelectedText(testDropdownComponent));
    }
}
