// <copyright file="EventBus.cs" company="VR4VET">
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

namespace DataCenter
{
    using System;
    using DataCenter.Events;
    using UnityEngine;

    /// <summary>
    /// This class contains all the events that can be invoked in the data center
    /// scenario. It is used to decouple the data center scenario from the tasks
    /// and activities.
    /// </summary>
    public class EventBus
    {
        /// <summary>
        /// Gets or sets the event that is invoked when a hardware problem is generated.
        /// </summary>
        public Action<HardwareProblemGeneratedEvent> HardwareProblemGenerated { get; set; }

        /// <summary>
        /// Gets or sets the event that is invoked when a hardware problem is generated.
        /// This event is invoked after the <see cref="HardwareProblemGenerated"/> event.
        /// </summary>
        public Action<HardwareProblemGeneratedEvent> AfterHardwareProblemGenerated { get; set; }

        /// <summary>
        /// Gets or sets the event that is invoked when a RAM module is installed.
        /// </summary>
        public Action<HardwareComponentInstalledEvent<RamComponent>> RamComponentInstalled { get; set; }

        /// <summary>
        /// Gets or sets the event that is invoked when a RAM module is removed.
        /// </summary>
        public Action<HardwareComponentRemovedEvent<RamComponent>> RamComponentRemoved { get; set; }

        /// <summary>
        /// Gets or sets the event that is invoked when a hard disk drive is installed.
        /// </summary>
        public Action<HardwareComponentInstalledEvent<HddComponent>> HddComponentInstalled { get; set; }

        /// <summary>
        /// Gets or sets the event that is invoked when a hard disk drive is removed.
        /// </summary>
        public Action<HardwareComponentRemovedEvent<HddComponent>> HddComponentRemoved { get; set; }

        /// <summary>
        /// Gets or sets the event that is invoked when a ticket is accepted.
        /// </summary>
        public Action<TicketAcceptedEvent> TicketAccepted { get; set; }

        /// <summary>
        /// Gets or sets the event that is invoked when a ticket is finished.
        /// </summary>
        public Action<TicketFinishedEvent> TicketFinished { get; set; }
    }
}