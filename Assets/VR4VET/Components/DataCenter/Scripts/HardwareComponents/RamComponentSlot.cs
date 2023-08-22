// <copyright file="RamComponentSlot.cs" company="VR4VET">
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
using DataCenter;
using DataCenter.Events;
using UnityEngine;

/// <summary>
/// This MonoBehaviour represents the slot for the RAM component. It should be
/// attached to the RAM slot GameObject in the editor or with
/// <see cref="GameObject.AddComponent{T}"/>. Do not use it directly in code, use
/// <see cref="HardwareComponentSlot{T}"/> with <see cref="RamComponent"/> instead.
/// </summary>
public class RamComponentSlot : HardwareComponentSlot<RamComponent>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RamComponentSlot"/> class.
    /// </summary>
    public RamComponentSlot()
    {
        // Allow interchanging RAM modules with the same capacity.
        this.IsComponentValid = () => (this.TargetComponent == this.Component)
            || (this.TargetComponent != null && this.Component != null && this.TargetComponent.Capacity == this.Component.Capacity);
    }

    /// <inheritdoc/>
    protected override void OnGrabbableSnappedToZone(Grabbable grabbable)
    {
        HardwareComponentInstalledEvent<RamComponent> e = new(grabbable.GetComponent<RamComponent>(), this);
        DataCenterScenario.Instance.EventBus.RamComponentInstalled?.Invoke(e);
    }

    /// <inheritdoc/>
    protected override void OnGrabbableDetachedFromZone(Grabbable grabbable)
    {
        HardwareComponentRemovedEvent<RamComponent> e = new(grabbable.GetComponent<RamComponent>(), this);
        DataCenterScenario.Instance.EventBus.RamComponentRemoved?.Invoke(e);
    }
}