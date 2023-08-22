// <copyright file="ServerTest.cs" company="VR4VET">
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

/// <summary>
/// Test suite for the <see cref="Server"/> class.
/// </summary>
public class ServerTest
{
    /// <summary>
    /// No hardware components in the server.
    /// </summary>
    [Test]
    public void GetHardwareComponents_Empty()
    {
        GameObject gameObject = new();
        Server server = gameObject.AddComponent<Server>();
        RamComponent[] hardwareComponents = server.GetHardwareComponents<RamComponent>();
        Assert.AreEqual(0, hardwareComponents.Length);
    }

    /// <summary>
    /// 4 RAM modules in the server.
    /// </summary>
    [Test]
    public void GetHardwareComponents_Ram()
    {
        GameObject gameObject = new();
        Server server = gameObject.AddComponent<Server>();
        for (int i = 0; i < 4; i++)
        {
            GameObject ramGameObject = new();
            ramGameObject.transform.parent = gameObject.transform;
            ramGameObject.AddComponent<RamComponent>();
        }

        RamComponent[] hardwareComponents = server.GetHardwareComponents<RamComponent>();
        Assert.AreEqual(4, hardwareComponents.Length);
    }

    /// <summary>
    /// No RAM installed in the server.
    /// </summary>
    [Test]
    public void GetInstalledRamCapacity_Empty()
    {
        GameObject gameObject = new();
        Server server = gameObject.AddComponent<Server>();
        Assert.AreEqual(0, server.GetInstalledRamCapacity());
    }

    /// <summary>
    /// 4 RAM modules in the server.
    /// </summary>
    [Test]
    public void GetInstalledRamCapacity_Four()
    {
        GameObject gameObject = new();
        Server server = gameObject.AddComponent<Server>();
        for (int i = 0; i < 4; i++)
        {
            GameObject ramGameObject = new();
            ramGameObject.transform.parent = gameObject.transform;
            RamComponent ramComponent = ramGameObject.AddComponent<RamComponent>();
            ramComponent.Capacity = 16;
        }

        Assert.AreEqual(64, server.GetInstalledRamCapacity());
    }
}
