// <copyright file="HardwareProblemType.cs" company="VR4VET">
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

namespace DataCenter.HardwareProblems
{
    using System.Collections.Generic;
    using DataCenter.Events;
    using Tablet;

    /// <summary>
    /// An abstract class that represents a hardware problem type.
    /// </summary>
    public abstract class HardwareProblemType
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HardwareProblemType"/> class.
        /// </summary>
        /// <param name="location">The server location of the problem.</param>
        protected HardwareProblemType(ServerLocation location)
        {
            this.Location = location;
        }

        /// <summary>
        /// Gets the type of the hardware component that needs to be fixed.
        /// </summary>
        public static string HardwareTypeName { get; }

        /// <summary>
        /// Gets the type of task that needs to be done.
        /// </summary>
        public static string HardwareTaskTypeName { get; }

        /// <summary>
        /// Gets the hardwareTypeName.
        /// </summary>
        public abstract string GetHardwareTypeName { get; }

        /// <summary>
        /// Gets the hardwareTypeName.
        /// </summary>
        public abstract string GetHardwareTaskTypeName { get; }

        /// <summary>
        /// Gets the message that describes the problem. This message is shown to
        /// the user when the problem occurs. It should be a short message that
        /// describes the problem in a way that is understandable to the user.
        /// </summary>
        public abstract string Message { get; }

        /// <summary>
        /// Gets the list of activities that are part of this problem type. The
        /// activities returned by this method are related only to resolving the
        /// problem. They should be performed after the server has been shut down,
        /// and before it is started again.
        /// </summary>
        //public List<Activity> Activities { get; } = new List<Activity>();

        /// <summary>
        /// Gets the server location of the problem.
        /// </summary>
        protected ServerLocation Location { get; }

        /// <summary>
        /// Populates the server with the hardware components that are part of
        /// this problem type. This method is called when the problem is
        /// encountered, and it should be used to add the hardware components
        /// that are part of this problem type to the server.
        /// </summary>
        /// <param name="serverPopulator">The server populator that is used to populate the server.</param>
        public abstract void PopulateServer(ServerPopulator serverPopulator);

        /// <summary>
        /// This method is called when the player has installed a RAM module.
        /// </summary>
        /// <param name="e">The event.</param>
        public abstract void OnRamComponentInstalled(HardwareComponentInstalledEvent<RamComponent> e);

        /// <summary>
        /// This method is called when the player has removed a RAM module.
        /// </summary>
        /// <param name="e">The event.</param>
        public abstract void OnRamComponentRemoved(HardwareComponentRemovedEvent<RamComponent> e);

        /// <summary>
        /// This method is called when the player has installed a hard disk drive.
        /// </summary>
        /// <param name="e">The event.</param>
        public abstract void OnHddComponentInstalled(HardwareComponentInstalledEvent<HddComponent> e);

        /// <summary>
        /// This method is called when the player has removed a hard disk drive.
        /// </summary>
        /// <param name="e">The event.</param>
        public abstract void OnHddComponentRemoved(HardwareComponentRemovedEvent<HddComponent> e);

        /// <summary>
        /// Adds the event listeners for this problem type.
        /// </summary>
        /// <param name="eventBus">The event bus the event listeners should be added to.</param>
        public void AddEventListeners(EventBus eventBus)
        {
            eventBus.RamComponentInstalled += this.OnRamComponentInstalled;
            eventBus.RamComponentRemoved += this.OnRamComponentRemoved;
            eventBus.HddComponentInstalled += this.OnHddComponentInstalled;
            eventBus.HddComponentRemoved += this.OnHddComponentRemoved;
        }

        /// <summary>
        /// Removes the event listeners for this problem type.
        /// </summary>
        /// <param name="eventBus">The event bus the event listeners should be removed from.</param>
        public void RemoveEventListeners(EventBus eventBus)
        {
            eventBus.RamComponentInstalled -= this.OnRamComponentInstalled;
            eventBus.RamComponentRemoved -= this.OnRamComponentRemoved;
            eventBus.HddComponentInstalled -= this.OnHddComponentInstalled;
            eventBus.HddComponentRemoved -= this.OnHddComponentRemoved;
        }
    }
}