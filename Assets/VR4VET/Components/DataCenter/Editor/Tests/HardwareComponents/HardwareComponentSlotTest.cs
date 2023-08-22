// <copyright file="HardwareComponentSlotTest.cs" company="VR4VET">
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
/// Test suite for the <see cref="HardwareComponentSlot{T}"/> class.
/// </summary>
public class HardwareComponentSlotTest
{
    /// <summary>
    /// The getter should return null if no component is attached.
    /// </summary>
    [Test]
    public void Component_Getter()
    {
        GameObject slotGameObject = new();
        RamComponentSlot slot = slotGameObject.AddComponent<RamComponentSlot>();

        Assert.IsNull(slot.Component);
    }

    /// <summary>
    /// The setter should set the component.
    /// </summary>
    [Test]
    public void Component_Setter_SetsField()
    {
        GameObject slotGameObject = new();
        RamComponentSlot slot = slotGameObject.AddComponent<RamComponentSlot>();

        GameObject snapZoneGameObject = new();
        snapZoneGameObject.transform.parent = slotGameObject.transform;
        snapZoneGameObject.AddComponent<SnapZone>();

        GameObject componentGameObject = new();
        RamComponent component = componentGameObject.AddComponent<RamComponent>();
        componentGameObject.AddComponent<Grabbable>();

        slot.Component = component;

        Assert.AreEqual(component, slot.Component);
    }

    /// <summary>
    /// The setter should set the component as a child of the slot.
    /// </summary>
    [Test]
    public void Component_Setter_MakesComponentChild()
    {
        GameObject slotGameObject = new();
        RamComponentSlot slot = slotGameObject.AddComponent<RamComponentSlot>();

        GameObject snapZoneGameObject = new();
        snapZoneGameObject.transform.parent = slotGameObject.transform;
        snapZoneGameObject.AddComponent<SnapZone>();

        GameObject componentGameObject = new();
        RamComponent component = componentGameObject.AddComponent<RamComponent>();
        componentGameObject.AddComponent<Grabbable>();

        slot.Component = component;

        Assert.NotNull(slot.GetComponentInChildren<RamComponent>());
    }

    /// <summary>
    /// The setter should throw an exception if the slot does not have a SnapZone.
    /// </summary>
    [Test]
    public void Component_Setter_NoSnapZone()
    {
        GameObject slotGameObject = new();
        RamComponentSlot slot = slotGameObject.AddComponent<RamComponentSlot>();

        GameObject componentGameObject = new();
        RamComponent component = componentGameObject.AddComponent<RamComponent>();
        componentGameObject.AddComponent<Grabbable>();

        ServerPopulationException exception = Assert.Throws<ServerPopulationException>(() => slot.Component = component);
        Assert.AreEqual("HardwareComponentSlot<RamComponent> does not have a SnapZone script attached.", exception.Message);
    }
}
