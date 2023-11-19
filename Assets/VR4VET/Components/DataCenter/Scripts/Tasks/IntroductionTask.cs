//// <copyright file="IntroductionTask.cs" company="VR4VET">
//// MIT License
////
//// Copyright (c) 2023 VR4VET
////
//// Permission is hereby granted, free of charge, to any person obtaining a copy
//// of this software and associated documentation files (the "Software"), to deal
//// in the Software without restriction, including without limitation the rights
//// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//// copies of the Software, and to permit persons to whom the Software is
//// furnished to do so, subject to the following conditions:
////
//// The above copyright notice and this permission notice shall be included in all
//// copies or substantial portions of the Software.
////
//// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//// SOFTWARE.
//// </copyright>

//namespace DataCenter.Tasks
//{
//    using Tablet;

//    /// <summary>
//    /// A ScriptableObject that represents the introduction task.
//    /// </summary>
//    public class IntroductionTask : Task
//    {
//        /// <summary>
//        /// Gets the object combining this task to a target in the scene of the task.
//        /// </summary>
//        public TaskxTarget TaskxTarget { get; } = new TaskxTarget();

//        /// <summary>
//        /// Gets the activity of entering the data center.
//        /// </summary>
//        public Activity EnterDataCenterActivity { get; } = new Activity()
//        {
//            aktivitetName = "Enter the data center.",
//        };

//        /// <summary>
//        /// Gets the activity of talking to the supervisor.
//        /// </summary>
//        public Activity TalkToSupervisorActivity { get; } = new Activity()
//        {
//            aktivitetName = "Talk to the supervisor.",
//        };

//        /// <summary>
//        /// Gets the activity of entering the control room.
//        /// </summary>
//        public Activity EnterControlRoomActivity { get; } = new Activity()
//        {
//            aktivitetName = "Enter the control room.",
//        };

//        /// <summary>
//        /// This method is called when the script instance is being loaded.
//        /// </summary>
//        private void Awake()
//        {
//            this.TaskxTarget.task = this;
//            this._taskName = "Data center introduction";
//            this.description = "The supervisor will introduce you to the data center.";

//            this.activities.Add(this.EnterDataCenterActivity);
//            this.activities.Add(this.TalkToSupervisorActivity);
//            this.activities.Add(this.EnterControlRoomActivity);
//        }
//    }
//}