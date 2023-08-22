// <copyright file="HardwareProblem.cs" company="VR4VET">
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
using DataCenter.HardwareProblems;

/// <summary>
/// Represents a problem related to hardware components.
/// </summary>
public class HardwareProblem
{
    /// <summary>
    /// Initializes a new instance of the <see cref="HardwareProblem"/> class.
    /// </summary>
    /// <param name="location">The location of the server where the problem occurred.</param>
    /// <param name="problemType">The type of the hardware problem, including more detailed information.</param>
    public HardwareProblem(ServerLocation location, HardwareProblemType problemType)
    {
        this.Location = location;
        this.ProblemType = problemType;
    }

    /// <summary>
    /// Gets the location of the server where the problem occurred.
    /// </summary>
    public ServerLocation Location { get; }

    /// <summary>
    /// Gets the type of the hardware problem.
    /// </summary>
    public HardwareProblemType ProblemType { get; }

    /// <summary>
    /// Gets the message of the hardware problem.
    /// </summary>
    public string Message => this.ProblemType.Message;

    /// <inheritdoc/>
    public override bool Equals(object obj)
    {
        return obj is HardwareProblem problem &&
               EqualityComparer<ServerLocation>.Default.Equals(this.Location, problem.Location) &&
               EqualityComparer<HardwareProblemType>.Default.Equals(this.ProblemType, problem.ProblemType);
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        return HashCode.Combine(this.Location, this.ProblemType);
    }
}
