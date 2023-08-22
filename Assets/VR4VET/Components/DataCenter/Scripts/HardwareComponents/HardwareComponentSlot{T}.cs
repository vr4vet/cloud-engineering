// <copyright file="HardwareComponentSlot{T}.cs" company="VR4VET">
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

using System;
using BNG;
using DataCenter;
using DataCenter.Events;
using UnityEngine;

/// <summary>
/// This MonoBehaviour is a generic class that is used to create a slot for a hardware component.
/// </summary>
/// <typeparam name="T">The type of the hardware component this slot is for.</typeparam>
public abstract class HardwareComponentSlot<T> : MonoBehaviour
    where T : HardwareComponent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="HardwareComponentSlot{T}"/> class.
    /// </summary>
    public HardwareComponentSlot()
    {
        this.IsComponentValid = () => this.Component == this.TargetComponent;
    }

    /// <summary>
    /// Gets or sets the hardware component that is in this slot. If the slot is
    /// empty, this value is null. Does not emit events.
    /// </summary>
    public T Component
    {
        get => this.GetComponentInChildren<T>();
        set
        {
            Grabbable grabbable = value.Grabbable;
            SnapZone snapZone = this.SnapZone;

            grabbable.transform.position = snapZone.transform.position;
            snapZone.GrabGrabbable(grabbable);
        }
    }

    /// <summary>
    /// Gets the SnapZone component of this slot.
    /// </summary>
    public SnapZone SnapZone
    {
        get
        {
            SnapZone snapZone = this.GetComponentInChildren<SnapZone>();
            if (snapZone == null)
            {
                throw new ServerPopulationException($"HardwareComponentSlot<{typeof(T).Name}> does not have a SnapZone script attached.");
            }

            return snapZone;
        }
    }

    /// <summary>
    /// Gets or sets the target component that this slot is for.
    /// If the slot is supposed empty, this value is null.
    /// For problematic servers this property is ignored.
    /// </summary>
    public T TargetComponent { get; set; }

    /// <summary>
    /// Gets or sets a function that determines if the installed component is valid for this slot.
    /// For slots part of hardware problems, this function is overwritten by
    /// <see cref="DataCenter.HardwareProblems.HardwareProblemType.PopulateServer(ServerPopulator)"/>.
    /// </summary>
    public Func<bool> IsComponentValid { get; set; }

    /// <summary>
    /// This method is called when a Grabbable is snapped to the SnapZone.
    /// </summary>
    /// <param name="grabbable">The Grabbable that was snapped to the SnapZone.</param>
    protected abstract void OnGrabbableSnappedToZone(Grabbable grabbable);

    /// <summary>
    /// This method is called when a Grabbable is detached from the SnapZone.
    /// </summary>
    /// <param name="grabbable">The Grabbable that was detached from the SnapZone.</param>
    protected abstract void OnGrabbableDetachedFromZone(Grabbable grabbable);

    /// <summary>
    /// This method is called when a hardware problem is generated, after the
    /// <see cref="EventBus.HardwareProblemGenerated"/> event in invoked.
    /// </summary>
    /// <param name="e">The event.</param>
    private void OnAfterHardwareProblemGenerated(HardwareProblemGeneratedEvent e)
    {
        this.SnapZone.OnSnapEvent.AddListener(this.OnGrabbableSnappedToZone);
        this.SnapZone.OnDetachEvent.AddListener(this.OnGrabbableDetachedFromZone);
    }

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    private void OnEnable()
    {
        DataCenterScenario.Instance.EventBus.AfterHardwareProblemGenerated += this.OnAfterHardwareProblemGenerated;
    }

    /// <summary>
    /// This function is called when the behaviour becomes disabled or inactive.
    /// </summary>
    private void OnDisable()
    {
        this.SnapZone.OnSnapEvent.RemoveListener(this.OnGrabbableSnappedToZone);
        this.SnapZone.OnDetachEvent.RemoveListener(this.OnGrabbableDetachedFromZone);

        DataCenterScenario.Instance.EventBus.AfterHardwareProblemGenerated -= this.OnAfterHardwareProblemGenerated;
    }
}