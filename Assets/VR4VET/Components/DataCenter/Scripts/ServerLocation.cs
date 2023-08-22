// <copyright file="ServerLocation.cs" company="VR4VET">
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

/// <summary>
/// Represents a location of a server.
/// </summary>
public class ServerLocation
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ServerLocation"/> class.
    /// </summary>
    /// <param name="serverContainer">The server container containing the server.</param>
    /// <param name="server">The server.</param>
    public ServerLocation(ServerContainer serverContainer, Server server)
    {
        this.ServerContainer = serverContainer;
        this.Server = server;
    }

    /// <summary>
    /// Gets the server container containing the server.
    /// </summary>
    public ServerContainer ServerContainer { get; }

    /// <summary>
    /// Gets the server.
    /// </summary>
    public Server Server { get; }

    /// <inheritdoc/>
    public override bool Equals(object obj)
    {
        return obj is ServerLocation error &&
               EqualityComparer<ServerContainer>.Default.Equals(this.ServerContainer, error.ServerContainer) &&
               EqualityComparer<Server>.Default.Equals(this.Server, error.Server);
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        return HashCode.Combine(this.ServerContainer, this.Server);
    }
}
