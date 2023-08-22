// <copyright file="HardwareProblemGenerator.cs" company="VR4VET">
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
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DataCenter.HardwareProblems;
using UnityEngine;

/// <summary>
/// Generates hardware problems.
/// </summary>
public class HardwareProblemGenerator : MonoBehaviour
{
    /// <summary>
    /// Gets an array of server containers.
    /// </summary>
    /// <returns>An array of <see cref="ServerContainer"/> objects.</returns>
    public ServerContainer[] GetServerContainers()
    {
        return this.GetComponentsInChildren<ServerContainer>();
    }

    /// <summary>
    /// Generates a hardware problem.
    /// </summary>
    /// <param name="random">A pseudo-random number generator. Useful for testing.</param>
    /// <param name="hardwareProblemTypes">
    /// An IEnumerable of classes that inherit from <see cref="HardwareProblemType"/>.
    /// Use <see cref="GetAllHardwareProblemTypes"/> for all possible hardware problem types.
    /// </param>
    /// <returns>A hardware problem.</returns>
    public HardwareProblem GenerateProblem(System.Random random, IEnumerable<Type> hardwareProblemTypes)
    {
        ServerLocation location = this.GenerateLocation(random);
        HardwareProblemType problemType = this.GenerateProblemType(location, random, hardwareProblemTypes);

        HardwareProblem hardwareError = new(location, problemType);
        return hardwareError;
    }

    /// <summary>
    /// Generates a random <see cref="ServerLocation"/>.
    /// </summary>
    /// <param name="random">A pseudo-random number generator. Useful for testing.</param>
    /// <returns>A random server location.</returns>
    public ServerLocation GenerateLocation(System.Random random)
    {
        ServerContainer[] serverContainers = this.GetServerContainers();
        if (serverContainers.Length == 0)
        {
            throw new HardwareProblemGenerationException("No server containers found in children.");
        }

        ServerContainer serverContainer = serverContainers[random.Next(serverContainers.Length)];

        Server[] servers = serverContainer.GetServers();
        if (servers.Length == 0)
        {
            throw new HardwareProblemGenerationException("No servers found in children.");
        }

        Server server = servers[random.Next(servers.Length)];

        ServerLocation location = new(serverContainer, server);
        return location;
    }

    /// <summary>
    /// Gets all <see cref="HardwareProblemType"/> types.
    /// </summary>
    /// <returns>An IEnumerable of all classes that inherit from <see cref="HardwareProblemType"/>.</returns>
    public IEnumerable<Type> GetAllHardwareProblemTypes()
    {
        Type[] allTypes = Assembly.GetExecutingAssembly().GetTypes();

        return allTypes.Where(t => t.IsClass && !t.IsAbstract && t.BaseType == typeof(HardwareProblemType));
    }

    /// <summary>
    /// Generates a random <see cref="HardwareProblemType"/> with random parameters.
    /// </summary>
    /// <param name="location">The location of the server in which the problem is generated.</param>
    /// <param name="random">A pseudo-random number generator. Useful for testing.</param>
    /// <param name="types">An IEnumerable of classes that inherit from <see cref="HardwareProblemType"/>, of which a random element is picked.</param>
    /// <returns>A random hardware problem type.</returns>
    public HardwareProblemType GenerateProblemType(ServerLocation location, System.Random random, IEnumerable<Type> types)
    {
        if (types.Count() == 0)
        {
            throw new HardwareProblemGenerationException("No hardware problem types found.");
        }

        Type type = types.ElementAt(random.Next(types.Count()));

        MethodInfo generateRandomMethod = type.GetMethod("GenerateRandom", new[] { typeof(ServerLocation), typeof(System.Random) });
        if (generateRandomMethod == null)
        {
            throw new HardwareProblemGenerationException($"No GenerateRandom(ServerLocation, System.Random) method found in {type.Name}.");
        }

        HardwareProblemType problemType = (HardwareProblemType)generateRandomMethod.Invoke(null, new object[] { location, random });
        return problemType;
    }
}
