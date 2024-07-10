// <copyright file="InstallAdditionalHdd.cs" company="VR4VET">
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
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DataCenter.Events;
    using Tablet;
    using UnityEngine;
    using Task;

    /// <summary>
    /// A concrete class that represents a hardware problem type where the player has to install
    /// additional hard disk drives into empty slots.
    /// </summary>
    public class InstallAdditionalHdd : HardwareProblemType
    {
        /// <summary>
        /// A dictionary that maps HDD slots to activities.
        /// </summary>
        private readonly Dictionary<HardwareComponentSlot<HddComponent>, Subtask> slotToActivity = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="InstallAdditionalHdd"/> class.
        /// </summary>
        /// <param name="location">The location where the problem occurred.</param>
        /// <param name="slots">List of empty HDD slots the player has to install additional HDDs into. One activity is created for each slot.</param>
        public InstallAdditionalHdd(ServerLocation location, List<HardwareComponentSlot<HddComponent>> slots)
            : base(location)
        {
            if (slots.Any(slot => slot == null))
            {
                throw new ArgumentException("One or more slots are null.");
            }

            this.Slots = slots;
        }

        /// <summary>
        /// Gets the type of hardware that needs to be fixed.
        /// </summary>
        public static new string HardwareTypeName => "HDD";

        /// <summary>
        /// Gets the type of the task that needs to be done.
        /// </summary>
        public static new string HardwareTaskTypeName => "Install additional HDDs";

        /// <summary>
        /// Gets the hardwareTypeName.
        /// </summary>
        public override string GetHardwareTypeName => HardwareTypeName;

        /// <summary>
        /// Gets the hardwareTaskTypeName.
        /// </summary>
        public override string GetHardwareTaskTypeName => HardwareTaskTypeName;

        /// <summary>
        /// Gets the list of HDD slots where the player has to install additional HDDs.
        /// </summary>
        public List<HardwareComponentSlot<HddComponent>> Slots { get; }

        /// <inheritdoc/>
        public override string Message
        {
            get
            {
                var slotNames = this.Slots.Select(slot => slot.name).ToList();
                string prettyJoinedSlotNames = string.Join(", ", slotNames.SkipLast(1)) + (slotNames.Count > 1 ? " and " : string.Empty) + slotNames.LastOrDefault();

                return string.Format(
                    "The client has ordered additional HDD storage for server '{0}' in '{1}'.\n\nInstall new hard disk drives into {2}.",
                    this.Location.Server.name,
                    this.Location.ServerContainer.name,
                    prettyJoinedSlotNames);
            }
        }

        /// <summary>
        /// Generates a random <see cref="InstallAdditionalHdd"/> problem.
        /// </summary>
        /// <param name="location">The server location where additional HDDs should be installed.</param>
        /// <param name="random">A pseudo-random number generator. Useful for testing.</param>
        /// <returns>A random <see cref="InstallAdditionalHdd"/> problem.</returns>
        public static InstallAdditionalHdd GenerateRandom(ServerLocation location, System.Random random)
        {
            HardwareComponentSlot<HddComponent>[] allSlots = location.Server.GetHardwareComponentSlots<HddComponent>();

            // Select how many HDDs to install, must be between 2 (if there are 2 slots) and half of the slots
            int amount = random.Next(Math.Min(2, allSlots.Length), (allSlots.Length / 2) + 1);

            // Select how many slots are already filled. Must be at least 1, and filled + amount must be less than or equal to the total number of slots
            int filled = random.Next(1, allSlots.Length - amount + 1);

            List<HardwareComponentSlot<HddComponent>> slots = allSlots.Skip(filled).Take(amount).ToList();

            InstallAdditionalHdd installAdditionalHdd = new(location, slots);
            return installAdditionalHdd;
        }

        /// <summary>
        /// <inheritdoc/>
        /// <para>
        /// All HDD slots up to the slots where the new HDDs should be installed are filled with drives.
        /// </para>
        /// <para>
        /// The RAM in the server is populated using <see cref="ServerPopulator.PopulateRam(Server)"/>.
        /// </para>
        /// </summary>
        /// <param name="serverPopulator"><inheritdoc/></param>
        public override void PopulateServer(ServerPopulator serverPopulator)
        {
            var allSlots = this.Location.Server.GetHardwareComponentSlots<HddComponent>().ToList();

            var minimumIndex = this.Slots.Select(slot => allSlots.IndexOf(slot)).Min();
            var slotsToFill = allSlots.Take(minimumIndex);

            foreach (HardwareComponentSlot<HddComponent> slot in slotsToFill)
            {
                slot.TargetComponent = slot.Component = serverPopulator.CreateHddComponent();
            }

            foreach (HardwareComponentSlot<HddComponent> slot in this.Slots)
            {
                slot.IsComponentValid = () => slot.Component != null;
            }

            serverPopulator.PopulateRam(this.Location.Server);
        }

        /// <inheritdoc/>
        public override void OnRamComponentInstalled(HardwareComponentInstalledEvent<RamComponent> e)
        {
        }

        /// <inheritdoc/>
        public override void OnRamComponentRemoved(HardwareComponentRemovedEvent<RamComponent> e)
        {
        }

        /// <summary>
        /// <inheritdoc/>
        /// <para>
        /// Completes the activity of installing the new HDD.
        /// </para>
        /// </summary>
        /// <param name="e"><inheritdoc/></param>
        public override void OnHddComponentInstalled(HardwareComponentInstalledEvent<HddComponent> e)
        {
            if (e.Component == null)
            {
                throw new ArgumentException("The component is null.");
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// <para>
        /// Uncompletes the activity of installing the new HDD.
        /// The player has to install the HDD again.
        /// </para>
        /// </summary>
        /// <param name="e"><inheritdoc/></param>
        public override void OnHddComponentRemoved(HardwareComponentRemovedEvent<HddComponent> e)
        {
            if (e.Component == null)
            {
                throw new ArgumentException("The component is null.");
            }
        }
    }
}