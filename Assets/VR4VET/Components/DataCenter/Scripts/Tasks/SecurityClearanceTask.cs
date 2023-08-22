// <copyright file="SecurityClearanceTask.cs" company="VR4VET">
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

namespace DataCenter.Tasks
{
    using Tablet;

    /// <summary>
    /// A ScriptableObject that represents the getting security clearance task.
    /// </summary>
    public class SecurityClearanceTask : Task
    {
        /// <summary>
        /// Gets the object combining this task to a target in the scene of the task.
        /// </summary>
        public TaskxTarget TaskxTarget { get; } = new TaskxTarget();

        /// <summary>
        /// Gets the activity of entering the security room.
        /// </summary>
        public Activity EnterSecurityRoomActivity { get; } = new Activity()
        {
            aktivitetName = "Enter the security room.",
        };

        /// <summary>
        /// Gets the activity of talking to the security person.
        /// </summary>
        public Activity TalkToSecurityPersonActivity { get; } = new Activity()
        {
            aktivitetName = "Talk to the security person.",
        };

        /// <summary>
        /// Gets the activity of getting the keys.
        /// </summary>
        public Activity GetKeysActivity { get; } = new Activity()
        {
            aktivitetName = "Get the keys to open the server cabinet door.",
        };

        /// <summary>
        /// This method is called when the script instance is being loaded.
        /// </summary>
        private void Awake()
        {
            this.TaskxTarget.task = this;
            this._taskName = "Security clearance";
            this.description = "Security is of high importance in the data center. You have to " +
                "get security clearance before you can perform maintenance.";

            this.activities.Add(this.EnterSecurityRoomActivity);
            this.activities.Add(this.TalkToSecurityPersonActivity);
            this.activities.Add(this.GetKeysActivity);
        }
    }
}