// <copyright file="UpgradeRam.cs" company="VR4VET">
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
    using Task;
    using UnityEngine;


    /// <summary>
    /// A concrete class that represents a hardware problem type where the player has to upgrade
    /// the RAM by removing the old RAM modules and installing new ones.
    /// </summary>
    public class UpgradeRam : HardwareProblemType
    {
        /// <summary>
        /// A dictionary that maps RAM slots to activities where the player has to remove the old RAM modules.
        /// </summary>
        private readonly Dictionary<HardwareComponentSlot<RamComponent>, Subtask> slotToRemoveActivity = new();

        /// <summary>
        /// A dictionary that maps RAM slots to activities where the player has to install new RAM modules.
        /// </summary>
        private readonly Dictionary<HardwareComponentSlot<RamComponent>, Subtask> slotToInstallActivity = new();

        /// <summary>
        /// The capacity of the RAM modules the player has to install.
        /// </summary>
        private readonly int upgradeToCapacity;

        /// <summary>
        /// A dictionary that maps RAM slots to the original RAM modules that were installed in them.
        /// </summary>
        private readonly Dictionary<HardwareComponentSlot<RamComponent>, RamComponent> originalRamComponents = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="UpgradeRam"/> class.
        /// </summary>
        /// <param name="location">The location where the problem occurred.</param>
        /// <param name="slots">List of RAM slots requiring an upgarde. Activities are created for each slot.</param>
        /// <param name="upgradeToCapacity">The capacity of the RAM modules the player has to install.</param>
        public UpgradeRam(ServerLocation location, List<HardwareComponentSlot<RamComponent>> slots, int upgradeToCapacity)
            : base(location)
        {
            if (slots.Any(slot => slot == null))
            {
                throw new ArgumentException("One or more slots are null.");
            }

            this.Slots = slots;
            this.upgradeToCapacity = upgradeToCapacity;

        }

        /// <summary>
        /// Gets the type of hardware that needs to be fixed.
        /// </summary>
        public static new string HardwareTypeName => "Ram";

        /// <summary>
        /// Gets the type of task that needs to be done.
        /// </summary>
        public static new string HardwareTaskTypeName => "Upgrade ram";

        /// <summary>
        /// Gets the hardwareTypeName.
        /// </summary>
        public override string GetHardwareTypeName => HardwareTypeName;

        /// <summary>
        /// Gets the hardwareTaskTypeName.
        /// </summary>
        public override string GetHardwareTaskTypeName => HardwareTaskTypeName;

        /// <summary>
        /// Gets the list of RAM slots of which the player has to upgrade the RAM modules.
        /// </summary>
        public List<HardwareComponentSlot<RamComponent>> Slots { get; }

        /// <inheritdoc/>
        public override string Message
        {
            get
            {
                var slotNames = this.Slots.Select(slot => slot.name).ToList();
                string prettyJoinedSlotNames = string.Join(", ", slotNames.SkipLast(1)) + (slotNames.Count > 1 ? " and " : string.Empty) + slotNames.LastOrDefault();

                return string.Format(
                    "The client has ordered a RAM upgrade of server '{0}' in '{1}'.\n\nReplace the old {2} GiB RAM modules of {3} with new {4} GiB modules.",
                    this.Location.Server.name,
                    this.Location.ServerContainer.name,
                    this.upgradeToCapacity / 2,
                    prettyJoinedSlotNames,
                    this.upgradeToCapacity);
            }
        }

        /// <summary>
        /// Generates a random <see cref="UpgradeRam"/> problem.
        /// </summary>
        /// <param name="location">The server location where the RAM upgrade is required.</param>
        /// <param name="random">A pseudo-random number generator. Useful for testing.</param>
        /// <returns>A random <see cref="UpgradeRam"/> problem.</returns>
        public static UpgradeRam GenerateRandom(ServerLocation location, System.Random random)
        {
            HardwareComponentSlot<RamComponent>[] allSlots = location.Server.GetHardwareComponentSlots<RamComponent>();
            List<HardwareComponentSlot<RamComponent>> slots = new();
            Debug.Log($"All slots: {allSlots}");

            // Upgrade RAM modules in powers of 2 (1, 2, 4, 8, ...)
            int logMaxAmount = (int)Math.Ceiling(Math.Log(allSlots.Length, 2));
            int logAmount = random.Next(0, logMaxAmount + 1);
            int amount = Math.Min((int)Math.Pow(2, logAmount), allSlots.Length);
            Debug.Log($"Amount: {amount}");

            // Divide the slots into n=amount groups, add the first slot of each group to the list
            for (int i = 0; i < amount; i++)
            {
                int slotIndex = (int)Math.Floor((double)i / amount * allSlots.Length);
                slots.Add(allSlots[slotIndex]);
            }

            Debug.Log($"Slots: {slots}");

            int log2Capacity = random.Next(4, 7); // 2^4 = 16 GiB, 2^5 = 32 GiB, 2^6 = 64 GiB
            int capacity = (int)Math.Pow(2, log2Capacity);

            Debug.Log($"Capacity: {capacity}");

            UpgradeRam upgradeRam = new(location, slots, capacity);
            return upgradeRam;
        }

        /// <summary>
        /// <inheritdoc/>
        /// <para>
        /// All RAM slots are filled with RAM modules of half the capacity of the new RAM modules.
        /// </para>
        /// <para>
        /// The HDDs in the server are populated using <see cref="ServerPopulator.PopulateHdd(Server)"/>.
        /// </para>
        /// </summary>
        /// <param name="serverPopulator"><inheritdoc/></param>
        public override void PopulateServer(ServerPopulator serverPopulator)
        {
            var slotsToFill = this.Location.Server.GetHardwareComponentSlots<RamComponent>();
            int oldCapacity = this.upgradeToCapacity / 2;

            this.originalRamComponents.Clear();

            foreach (HardwareComponentSlot<RamComponent> slot in slotsToFill)
            {
                slot.Component = serverPopulator.CreateRamComponent(oldCapacity);
                this.originalRamComponents.Add(slot, slot.Component);
            }

            foreach (HardwareComponentSlot<RamComponent> slot in slotsToFill)
            {
                slot.IsComponentValid = () =>
                {
                    // TODO: Sometimes slot.Component.Capacity is null, need a fix for this
                    if (slot != null && slot.Component != null && slot.Component.Capacity != null && this.upgradeToCapacity != null)
                    {
                        if (this.Slots.Contains(slot))
                        {
                            return slot.Component.Capacity == this.upgradeToCapacity;
                        }
                        else
                        {
                            return slot.Component.Capacity == oldCapacity;
                        }
                    }
                    else
                    {
                        return true;
                    }
                };
            }

            serverPopulator.PopulateHdd(this.Location.Server);
        }

        /// <summary>
        /// <inheritdoc/>
        /// <para>
        /// Completes the activity of installing the new RAM module.
        /// </para>
        /// </summary>
        /// <param name="e"><inheritdoc/></param>
        public override void OnRamComponentInstalled(HardwareComponentInstalledEvent<RamComponent> e)
        {
            if (e.Component == null)
            {
                throw new ArgumentException("The component is null.");
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// <para>
        /// If the RAM module is the original RAM module, the activity of removing the RAM module is completed.
        /// </para>
        /// </summary>
        /// <param name="e"><inheritdoc/></param>
        public override void OnRamComponentRemoved(HardwareComponentRemovedEvent<RamComponent> e)
        {
            if (e.Component == null)
            {
                throw new ArgumentException("The component is null.");
            }
        }

        /// <inheritdoc/>
        public override void OnHddComponentInstalled(HardwareComponentInstalledEvent<HddComponent> e)
        {
        }

        /// <inheritdoc/>
        public override void OnHddComponentRemoved(HardwareComponentRemovedEvent<HddComponent> e)
        {
        }
    }
}