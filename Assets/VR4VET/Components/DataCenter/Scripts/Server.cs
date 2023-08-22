// <copyright file="Server.cs" company="VR4VET">
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

using System.Linq;
using UnityEngine;

/// <summary>
/// This class is used to identify the server in the error generation.
/// </summary>
public class Server : MonoBehaviour
{
    /// <summary>
    /// Gets or sets a value indicating whether the server is online.
    /// </summary>
    public bool IsOnline { get; set; } = true;

    /// <summary>
    /// Gets an array of hardware components in the server.
    /// </summary>
    /// <typeparam name="T">The type of the hardware component.</typeparam>
    /// <returns>An array of <see cref="HardwareComponent"/> objects.</returns>
    public T[] GetHardwareComponents<T>()
        where T : HardwareComponent
    {
        return this.GetComponentsInChildren<T>();
    }

    /// <summary>
    /// Gets an array of hardware component slots in the server.
    /// </summary>
    /// <typeparam name="T">The type of the hardware component this slot is for.</typeparam>
    /// <returns>An array of <see cref="HardwareComponentSlot{T}"/> objects.</returns>
    public HardwareComponentSlot<T>[] GetHardwareComponentSlots<T>()
        where T : HardwareComponent
    {
        return this.GetComponentsInChildren<HardwareComponentSlot<T>>();
    }

    /// <summary>
    /// Gets the combined capacity of all RAM modules installed in the server.
    /// </summary>
    /// <returns>The total RAM capacity in gibibytes (2^30 bytes).</returns>
    public int GetInstalledRamCapacity()
    {
        RamComponent[] ramComponents = this.GetHardwareComponents<RamComponent>();
        int totalCapacity = 0;

        foreach (var ramComponent in ramComponents)
        {
            totalCapacity += ramComponent.Capacity;
        }

        return totalCapacity;
    }

    /// <summary>
    /// Checks if all components in the server are valid.
    /// </summary>
    /// <returns>True if all components are valid, false otherwise.</returns></returns>
    public bool AreAllComponentsValid()
    {
        return new[]
        {
            this.GetHardwareComponentSlots<RamComponent>().All(slot => slot.IsComponentValid()),
            this.GetHardwareComponentSlots<HddComponent>().All(slot => slot.IsComponentValid()),
        }.All(x => x);
    }
}
