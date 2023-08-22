// <copyright file="HardwareProblemGeneratorTest.cs" company="VR4VET">
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
using DataCenter.HardwareProblems;
using NUnit.Framework;
using UnityEngine;

/// <summary>
/// Test suite for the <see cref="HardwareProblemGenerator"/> class.
/// </summary>
public class HardwareProblemGeneratorTest
{
    /// <summary>
    /// Helper function to instantiate a hardware structure with the given number of containers, servers and RAM components.
    /// </summary>
    /// <param name="parent">The parent object the hardware will be a child of.</param>
    /// <param name="numContainers">The number of server containers.</param>
    /// <param name="numServers">The number of servers.</param>
    /// <param name="numRamComponents">The number of RAM memory modules.</param>
    public void InstantiateHardware(GameObject parent, int numContainers, int numServers, int numRamComponents)
    {
        for (int containerIndex = 0; containerIndex < numContainers; containerIndex++)
        {
            GameObject serverContainerObject = new("ServerContainer" + containerIndex);
            serverContainerObject.transform.parent = parent.transform;
            Assert.NotNull(serverContainerObject.AddComponent<ServerContainer>());

            for (int serverIndex = 0; serverIndex < numServers; serverIndex++)
            {
                GameObject serverObject = new("Server" + serverIndex);
                serverObject.transform.parent = serverContainerObject.transform;
                Assert.NotNull(serverObject.AddComponent<Server>());

                for (int ramIndex = 0; ramIndex < numRamComponents; ramIndex++)
                {
                    GameObject ramComponentSlotObject = new("RamComponentSlot" + ramIndex);
                    ramComponentSlotObject.transform.parent = serverObject.transform;
                    Assert.NotNull(ramComponentSlotObject.AddComponent<RamComponentSlot>());

                    GameObject ramComponentObject = new("RamComponent" + ramIndex);
                    ramComponentObject.transform.parent = ramComponentSlotObject.transform;
                    Assert.NotNull(ramComponentObject.AddComponent<RamComponent>());
                }
            }
        }
    }

    /// <summary>
    /// Helper function to get an example location.
    /// </summary>
    /// <param name="parent">The parent object the hardware will be a child of.</param>
    /// <returns>An example location.</returns>
    public ServerLocation GetExampleLocation(GameObject parent)
    {
        GameObject serverContainerObject = new("ExampleServerContainer");
        serverContainerObject.transform.parent = parent.transform;
        ServerContainer serverContainer = serverContainerObject.AddComponent<ServerContainer>();
        Assert.NotNull(serverContainer);

        GameObject serverObject = new("ExampleServer");
        serverObject.transform.parent = serverContainerObject.transform;
        Server server = serverObject.AddComponent<Server>();
        Assert.NotNull(server);

        for (int ramIndex = 0; ramIndex < 4; ramIndex++)
        {
            GameObject ramComponentSlotObject = new("RamComponentSlot" + ramIndex);
            ramComponentSlotObject.transform.parent = serverObject.transform;
            Assert.NotNull(ramComponentSlotObject.AddComponent<RamComponentSlot>());

            GameObject ramComponentObject = new("RamComponent" + ramIndex);
            ramComponentObject.transform.parent = ramComponentSlotObject.transform;
            Assert.NotNull(ramComponentObject.AddComponent<RamComponent>());
        }

        return new ServerLocation(serverContainer, server);
    }

    /// <summary>
    /// Verify that GenerateProblem generates a problem of the given type.
    /// </summary>
    [Test]
    public void GenerateProblem()
    {
        GameObject errorGeneratorObject = new();
        HardwareProblemGenerator hardwareErrorGenerator = errorGeneratorObject.AddComponent<HardwareProblemGenerator>();

        this.InstantiateHardware(errorGeneratorObject, 4, 2, 4);

        System.Random random = new(0);
        HardwareProblem problem = hardwareErrorGenerator.GenerateProblem(random, new Type[] { typeof(InstallAdditionalRam) });

        Assert.IsInstanceOf<InstallAdditionalRam>(problem.ProblemType);
        Assert.AreEqual("ServerContainer2", problem.Location.ServerContainer.name);
    }

    /// <summary>
    /// Generate an error in one of the 4 server containers in one of the 2 servers in one of the 4 RAM memory modules.
    /// </summary>
    [Test]
    public void GenerateLocation()
    {
        GameObject errorGeneratorObject = new();
        HardwareProblemGenerator hardwareErrorGenerator = errorGeneratorObject.AddComponent<HardwareProblemGenerator>();

        this.InstantiateHardware(errorGeneratorObject, 4, 2, 4);

        System.Random random = new(0);
        ServerLocation location = hardwareErrorGenerator.GenerateLocation(random);

        Assert.AreEqual("ServerContainer2", location.ServerContainer.name);
        Assert.AreEqual("Server1", location.Server.name);
    }

    /// <summary>
    /// Verify that the error generator throws an exception if there are no server containers in the children.
    /// </summary>
    [Test]
    public void GenerateLocation_NoServerContainers()
    {
        GameObject errorGeneratorObject = new();
        HardwareProblemGenerator hardwareErrorGenerator = errorGeneratorObject.AddComponent<HardwareProblemGenerator>();

        this.InstantiateHardware(errorGeneratorObject, 0, 2, 4);

        System.Random random = new(0);
        HardwareProblemGenerationException ex = Assert.Throws<HardwareProblemGenerationException>(() => hardwareErrorGenerator.GenerateLocation(random));
        Assert.That(ex.Message, Is.EqualTo("No server containers found in children."));
    }

    /// <summary>
    /// Verify that the error generator throws an exception if there are no servers in the children.
    /// </summary>
    [Test]
    public void GenerateLocation_NoServers()
    {
        GameObject errorGeneratorObject = new();
        HardwareProblemGenerator hardwareErrorGenerator = errorGeneratorObject.AddComponent<HardwareProblemGenerator>();

        this.InstantiateHardware(errorGeneratorObject, 4, 0, 4);

        System.Random random = new(0);
        HardwareProblemGenerationException ex = Assert.Throws<HardwareProblemGenerationException>(() => hardwareErrorGenerator.GenerateLocation(random));
        Assert.That(ex.Message, Is.EqualTo("No servers found in children."));
    }

    /// <summary>
    /// Verify that all and only the hardware problem types are returned in any order.
    /// </summary>
    [Test]
    public void GetAllHardwareProblemTypes()
    {
        GameObject errorGeneratorObject = new();
        HardwareProblemGenerator hardwareErrorGenerator = errorGeneratorObject.AddComponent<HardwareProblemGenerator>();

        Assert.That(hardwareErrorGenerator.GetAllHardwareProblemTypes(), Is.SupersetOf(new Type[]
        {
            typeof(InstallAdditionalRam),
            typeof(UpgradeRam),
            typeof(InstallAdditionalHdd),
            typeof(ReplaceBrokenHdd),
        }));
    }

    /// <summary>
    /// Verify that the problem generator throws an exception if there are no hardware problem types in the list it picks from.
    /// </summary>
    [Test]
    public void GenerateProblemType_NoTypes()
    {
        GameObject errorGeneratorObject = new();
        HardwareProblemGenerator hardwareErrorGenerator = errorGeneratorObject.AddComponent<HardwareProblemGenerator>();

        ServerLocation location = this.GetExampleLocation(errorGeneratorObject);

        System.Random random = new(0);
        HardwareProblemGenerationException ex = Assert.Throws<HardwareProblemGenerationException>(() => hardwareErrorGenerator.GenerateProblemType(location, random, new Type[] { }));
        Assert.That(ex.Message, Is.EqualTo("No hardware problem types found."));
    }

    /// <summary>
    /// Verify that the problem generator throws an exception if the chosen type does not have a GenerateRandom method.
    /// </summary>
    [Test]
    public void GenerateProblemType_NoGenerateRandomMethod()
    {
        GameObject errorGeneratorObject = new();
        HardwareProblemGenerator hardwareErrorGenerator = errorGeneratorObject.AddComponent<HardwareProblemGenerator>();

        ServerLocation location = this.GetExampleLocation(errorGeneratorObject);

        System.Random random = new(0);
        HardwareProblemGenerationException ex = Assert.Throws<HardwareProblemGenerationException>(() => hardwareErrorGenerator.GenerateProblemType(location, random, new Type[] { typeof(object) }));
        Assert.That(ex.Message, Is.EqualTo("No GenerateRandom(ServerLocation, System.Random) method found in Object."));
    }

    /// <summary>
    /// Verify that all HardwareProblemTypes can be generated.
    /// </summary>
    [Test]
    public void GenerateProblemType_AllTypesGenerateHardwareProblemType()
    {
        GameObject errorGeneratorObject = new();
        HardwareProblemGenerator hardwareErrorGenerator = errorGeneratorObject.AddComponent<HardwareProblemGenerator>();

        ServerLocation location = this.GetExampleLocation(errorGeneratorObject);

        foreach (Type type in hardwareErrorGenerator.GetAllHardwareProblemTypes())
        {
            Assert.IsInstanceOf<HardwareProblemType>(hardwareErrorGenerator.GenerateProblemType(location, new System.Random(0), new Type[] { type }));
        }
    }
}
