// <copyright file="HardwareComponentTest.cs" company="VR4VET">
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
/// Test suite for the <see cref="HardwareComponent"/> class.
/// </summary>
public class HardwareComponentTest
{
    /// <summary>
    /// The getter should return the Grabber component.
    /// </summary>
    [Test]
    public void Grabbable_Getter()
    {
        GameObject componentGameObject = new();
        RamComponent component = componentGameObject.AddComponent<RamComponent>();
        componentGameObject.AddComponent<Grabbable>();

        Assert.NotNull(component.Grabbable);
    }

    /// <summary>
    /// The setter should throw an exception if the component does not have a Grabbable component.
    /// </summary>
    [Test]
    public void Grabbable_Getter_NoGrabbable()
    {
        GameObject componentGameObject = new();
        RamComponent component = componentGameObject.AddComponent<RamComponent>();

        ServerPopulationException exception = Assert.Throws<ServerPopulationException>(() => _ = component.Grabbable);
        Assert.AreEqual("HardwareComponent does not have a Grabbable script attached.", exception.Message);
    }
}
