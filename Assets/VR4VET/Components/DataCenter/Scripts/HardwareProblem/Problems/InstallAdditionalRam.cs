// <copyright file="InstallAdditionalRam.cs" company="VR4VET">
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

    /// <summary>
    /// A concrete class that represents a hardware problem type where the player has to install
    /// additional RAM modules into empty slots.
    /// </summary>
    public class InstallAdditionalRam : HardwareProblemType
    {
        /// <summary>
        /// A dictionary that maps RAM slots to activities.
        /// </summary>
        private readonly Dictionary<HardwareComponentSlot<RamComponent>, Activity> slotToActivity = new();

        /// <summary>
        /// The capacity of the RAM modules that are already installed and still need to be installed.
        /// </summary>
        private readonly int ramModuleCapacity;

        /// <summary>
        /// Initializes a new instance of the <see cref="InstallAdditionalRam"/> class.
        /// </summary>
        /// <param name="location">The location where the problem occurred.</param>
        /// <param name="slots">List of empty RAM slots the player has to install additional RAM modules into. One activity is created for each slot.</param>
        /// <param name="ramModuleCapacity">The capacity of the RAM modules that need to be installed.</param>
        public InstallAdditionalRam(ServerLocation location, List<HardwareComponentSlot<RamComponent>> slots, int ramModuleCapacity)
            : base(location)
        {
            if (slots.Any(slot => slot == null))
            {
                throw new ArgumentException("One or more slots are null.");
            }

            this.Slots = slots;
            this.ramModuleCapacity = ramModuleCapacity;

            foreach (var slot in slots)
            {
                Activity activity = new()
                {
                    aktivitetName = $"Install {ramModuleCapacity} GiB RAM into {slot.name}.",
                    AktivitetIsCompeleted = false,
                };
                this.Activities.Add(activity);
                this.slotToActivity.Add(slot, activity);
            }
        }

        /// <summary>
        /// Gets the type of hardware that needs to be fixed.
        /// </summary>
        public static new string HardwareTypeName => "Ram";

        /// <summary>
        /// Gets the type of the task that needs to be done.
        /// </summary>
        public static new string HardwareTaskTypeName => "Install additional ram";

        /// <summary>
        /// Gets the hardwareTypeName.
        /// </summary>
        public override string GetHardwareTypeName => HardwareTypeName;

        /// <summary>
        /// Gets the hardwareTaskTypeName.
        /// </summary>
        public override string GetHardwareTaskTypeName => HardwareTaskTypeName;

        /// <summary>
        /// Gets the list of RAM slots where the player has to install additional RAM modules.
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
                    "The client has ordered additional RAM for server '{0}' in '{1}'.\n\nInstall new RAM modules, each with capacity {2} GiB, into {3}.",
                    this.Location.Server.name,
                    this.Location.ServerContainer.name,
                    this.ramModuleCapacity,
                    prettyJoinedSlotNames);
            }
        }

        /// <summary>
        /// Generates a random <see cref="InstallAdditionalRam"/> problem.
        /// </summary>
        /// <param name="location">The server location where the RAM upgrade is required.</param>
        /// <param name="random">A pseudo-random number generator. Useful for testing.</param>
        /// <returns>A random <see cref="InstallAdditionalRam"/> problem.</returns>
        public static InstallAdditionalRam GenerateRandom(ServerLocation location, System.Random random)
        {
            HardwareComponentSlot<RamComponent>[] allSlots = location.Server.GetHardwareComponentSlots<RamComponent>();
            List<HardwareComponentSlot<RamComponent>> slots = new();

            // Install RAM modules in powers of 2 (1, 2, 4, 8, ...), with at least 2 and at most n-1 slots
            int logMaxAmount = (int)Math.Ceiling(Math.Log(allSlots.Length, 2));
            int logAmount = random.Next(2, logMaxAmount + 1);
            int amount = Math.Min((int)Math.Pow(2, logAmount), allSlots.Length - 1);

            // Divide the slots into n=amount groups, add the first slot of each group to the list
            for (int i = 0; i < amount; i++)
            {
                int slotIndex = (int)Math.Floor((double)i / amount * allSlots.Length);
                slots.Add(allSlots[slotIndex]);
            }

            int log2Capacity = random.Next(3, 6); // 2^3 = 8 GiB, 2^4 = 16 GiB, 2^5 = 32 GiB
            int capacity = (int)Math.Pow(2, log2Capacity);

            InstallAdditionalRam installAdditionalRam = new(location, slots, capacity);
            return installAdditionalRam;
        }

        /// <summary>
        /// <inheritdoc/>
        /// <para>
        /// All RAM slots that the player does not have to install RAM modules into are filled with
        /// RAM modules. These modules have the same capacity as the modules that the player has to
        /// install.
        /// </para>
        /// <para>
        /// The HDDs in the server are populated using <see cref="ServerPopulator.PopulateHdd(Server)"/>.
        /// </para>
        /// </summary>
        /// <param name="serverPopulator"><inheritdoc/></param>
        public override void PopulateServer(ServerPopulator serverPopulator)
        {
            var allSlots = this.Location.Server.GetHardwareComponentSlots<RamComponent>();

            foreach (HardwareComponentSlot<RamComponent> slot in allSlots.Except(this.Slots))
            {
                slot.Component = serverPopulator.CreateRamComponent(this.ramModuleCapacity);
            }

            foreach (HardwareComponentSlot<RamComponent> slot in allSlots)
            {
                slot.IsComponentValid = () => slot.Component != null && slot.Component.Capacity == this.ramModuleCapacity;
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

            // TODO: Check if the RAM module is the correct capacity
            this.slotToActivity
                .Where(slotToActivity => slotToActivity.Key == e.Slot)
                .Select(slotToActivity => slotToActivity.Value)
                .ToList()
                .ForEach(activity => DataCenterScenario.Instance.SetActivityCompleted(activity, true));
        }

        /// <summary>
        /// <inheritdoc/>
        /// <para>
        /// Uncompletes the activity of installing the new RAM module.
        /// The player has to install the RAM module again.
        /// </para>
        /// </summary>
        /// <param name="e"><inheritdoc/></param>
        public override void OnRamComponentRemoved(HardwareComponentRemovedEvent<RamComponent> e)
        {
            if (e.Component == null)
            {
                throw new ArgumentException("The component is null.");
            }

            this.slotToActivity
                .Where(slotToActivity => slotToActivity.Key == e.Slot)
                .Select(slotToActivity => slotToActivity.Value)
                .ToList()
                .ForEach(activity => DataCenterScenario.Instance.SetActivityCompleted(activity, false));
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