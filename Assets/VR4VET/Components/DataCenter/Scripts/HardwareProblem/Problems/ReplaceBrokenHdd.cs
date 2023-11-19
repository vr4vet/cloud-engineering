// <copyright file="ReplaceBrokenHdd.cs" company="VR4VET">
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
    /// A concrete class that represents a hardware problem type where the player has to remove
    /// broken hard disk drives and replace them with new ones.
    /// </summary>
    public class ReplaceBrokenHdd : HardwareProblemType
    {
        /// <summary>
        /// A dictionary that maps HDD slots to activities where the player has to remove the broken hard disk drives.
        /// </summary>
        private readonly Dictionary<HardwareComponentSlot<HddComponent>, Subtask> slotToRemoveActivity = new();

        /// <summary>
        /// A dictionary that maps HDD slots to activities where the player has to install replacement hard disk drives.
        /// </summary>
        private readonly Dictionary<HardwareComponentSlot<HddComponent>, Subtask> slotToInstallActivity = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="ReplaceBrokenHdd"/> class.
        /// </summary>
        /// <param name="location">The location where the problem occurred.</param>
        /// <param name="slots">List of slots with broken HDD the player has to replace. Two activities are created for each slot.</param>
        public ReplaceBrokenHdd(ServerLocation location, List<HardwareComponentSlot<HddComponent>> slots)
            : base(location)
        {
            if (slots.Any(slot => slot == null))
            {
                throw new ArgumentException("One or more slots are null.");
            }

            this.Slots = slots;

            //foreach (var slot in slots)
            //{
            //    Activity activity = new()
            //    {
            //        aktivitetName = $"Remove broken HDD from {slot.name}.",
            //    };
            //    this.Activities.Add(activity);
            //    this.slotToRemoveActivity.Add(slot, activity);
            //}

            //foreach (var slot in slots)
            //{
            //    Activity activity = new()
            //    {
            //        aktivitetName = $"Install replacement HDD into {slot.name}.",
            //    };
            //    this.Activities.Add(activity);
            //    this.slotToInstallActivity.Add(slot, activity);
            //}

            // TODO: Create steps for replacing broken HDDs

            Debug.Log("Still need to create steps for replacing broken HDDs");
        }

        /// <summary>
        /// Gets the type of hardware that needs to be fixed.
        /// </summary>
        public static new string HardwareTypeName => "HDD";

        /// <summary>
        /// Gets the type of the task that needs to be done.
        /// </summary>
        public static new string HardwareTaskTypeName => "Replace broken HDDs";

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
                    this.Slots.Count == 1
                        ? "An hard disk drive (HDD) has broken down in server '{0}' in '{1}'.\n\nReplace the HDD in {2} with a new drive."
                        : "Multiple hard disk drives (HDDs) have broken down in server '{0}' in '{1}'.\n\nReplace the HDDs in {2} with new drives.",
                    this.Location.Server.name,
                    this.Location.ServerContainer.name,
                    prettyJoinedSlotNames);
            }
        }

        /// <summary>
        /// Generates a random <see cref="ReplaceBrokenHdd"/> problem.
        /// </summary>
        /// <param name="location">The server location where additional HDDs should be installed.</param>
        /// <param name="random">A pseudo-random number generator. Useful for testing.</param>
        /// <returns>A random <see cref="ReplaceBrokenHdd"/> problem.</returns>
        public static ReplaceBrokenHdd GenerateRandom(ServerLocation location, System.Random random)
        {
            HardwareComponentSlot<HddComponent>[] allSlots = location.Server.GetHardwareComponentSlots<HddComponent>();

            int groupSize = (int)Math.Ceiling((double)allSlots.Length / 2);

            var groups = allSlots
                .Select((value, index) => new { value, index })
                .GroupBy(x => x.index / groupSize)
                .Select(x => x.Select(v => v.value).ToArray())
                .ToArray();

            // Add a random slot per group to the list
            var slots = groups.Select(group => group[random.Next(group.Length)]).ToList();

            ReplaceBrokenHdd installAdditionalHdd = new(location, slots);
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
            var allSlots = this.Location.Server.GetHardwareComponentSlots<HddComponent>();

            foreach (HardwareComponentSlot<HddComponent> slot in allSlots)
            {
                bool isBroken = this.Slots.Contains(slot);
                try
                {
                    slot.Component = serverPopulator.CreateHddComponent(isBroken);
                }
                catch (Exception ex)
                {
                    Debug.Log("Problem with creating hdd");
                }

                if (isBroken)
                {
                    slot.IsComponentValid = () => slot.Component != null && slot.Component.IsBroken == false;
                }
                else
                {
                    slot.TargetComponent = slot.Component;
                }
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

            //// If the newly installed HDD is not broken, the activity of installing replacement HDD is completed.
            //this.slotToInstallActivity
            //    .Where(slotToActivity => slotToActivity.Key == e.Slot)
            //    .Where(_ => !e.Component.IsBroken)
            //    .Select(slotToActivity => slotToActivity.Value)
            //    .ToList()
            //    .ForEach(activity => DataCenterScenario.Instance.SetActivityCompleted(activity, true));

            //// If the newly installed HDD is broken, the activity of removing the broken HDD is uncompleted.
            //this.slotToRemoveActivity
            //    .Where(slotToActivity => slotToActivity.Key == e.Slot)
            //    .Where(_ => e.Component.IsBroken)
            //    .Select(slotToActivity => slotToActivity.Value)
            //    .ToList()
            //    .ForEach(activity => DataCenterScenario.Instance.SetActivityCompleted(activity, false));

            // TODO: Set slot to install and slot to remove to true or false in HDD
            Debug.Log("Set slot to install and slot to remove to true or false in HDD");
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

            // If the removed HDD is broken, the activity of removing the RAM module is completed.
            //this.slotToRemoveActivity
            //    .Where(slotToActivity => slotToActivity.Key == e.Slot)
            //    .Where(_ => e.Component.IsBroken)
            //    .Select(slotToActivity => slotToActivity.Value)
            //    .ToList()
            //    .ForEach(activity => DataCenterScenario.Instance.SetActivityCompleted(activity, true));

            //// If the removed HDD is not broken, the activity of installing the replacement HDD is uncompleted.
            //this.slotToInstallActivity
            //    .Where(slotToActivity => slotToActivity.Key == e.Slot)
            //    .Where(_ => !e.Component.IsBroken)
            //    .Select(slotToActivity => slotToActivity.Value)
            //    .ToList()
            //    .ForEach(activity => DataCenterScenario.Instance.SetActivityCompleted(activity, false));

            // TODO: slot to remove and slot to install in replace broken hdd
            Debug.Log("still need to slot to remove and slot to install in replace broken hdd");
        }
    }
}