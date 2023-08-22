![](ReadMePictures/coverImage.png)

# VR Data Center Internship Scenario

Welcome to the VR Data Center Internship Scenario! This immersive experience offers an exciting opportunity to explore the world of data centers and gain practical knowledge through virtual reality (VR) simulations. This README.md file serves as a guide to help you navigate through the internship scenario and get the most out of your learning journey.

## Table of Contents

- [Overview](#overview)
- [Getting Started](#getting-started)
- [Usage](#usage)
- [Testing](#testing)
- [Code](#code)

## Overview

In this VR data center internship scenario, you will step into the shoes of a data center intern and take on various tasks and challenges related to managing and maintaining a data center environment. The scenario is designed to provide you with hands-on experience and insights into the world of data centers, including networking, infrastructure, security, and more.

## Getting Started

To get started with the VR data center internship scenario, follow these steps:

Instalation/Requirements


1. Download Unity 2021.3.5f1 (this was the latest LTS at the time the project was started )
2. Clone the Develop branch using git clone.
3. Open unity
4. Go to ‘Window’ -> ‘Package manage’ and make sure Open XR Plugin is installed. If  already there ignore this step.
6. Load the Data Center Scene 
5. Follow the in-app instructions to begin your internship scenario.

## Usage

During the VR data center internship scenario, you will encounter various tasks and challenges that simulate real-world scenarios. You will be guided through these tasks and provided with instructions and resources to complete them successfully. Make sure to take advantage of the interactive nature of the VR environment and explore different aspects of data center operations.

## Testing

To ensure a smooth experience with the VR data center internship scenario, we have written two types of tests: edit mode tests and play mode tests.

### Edit Mode Tests

The edit mode tests are designed to validate the functionality of the VR data center scenario in the Unity editor. These tests focus on verifying the correctness of scripts, components, and interactions within the editor environment.

To run the edit mode tests:

1. Open the Unity editor.
2. From the top menu, go to **Window > General > Test Runner**. This will open the Unity Test Runner window.
3. In the Test Runner window, select the **Edit Mode** tab.
4. Click on the **Run All** button to execute all the edit mode tests.

### Play Mode Tests

The play mode tests allow you to test the VR data center scenario while it is running in the Unity editor. These tests validate the behavior and functionality of the scenario from the perspective of a user interacting with the VR environment.

To run the play mode tests:
1. Open the Unity editor.
2. go to projectssettings.asset text file and change the playModeTestRunnerEnabled: 0 to playModeTestRunnerEnabled: 1
3. From the top menu, go to **Window > General > Test Runner**. This will open the Unity Test Runner window.
4. In the Test Runner window, select the **Play Mode** tab.
5. Click on the **Run All** button to execute all the play mode tests.

Please note that before running the tests, ensure that you have selected the appropriate tests in the Unity Test Runner window. This allows you to choose specific tests or groups of tests to run, providing more flexibility in the testing process.

We recommend regularly running these tests to catch any potential issues and ensure the integrity of the VR data center internship scenario.

## Code

The source code for the VR data center internship scenario can be found in the [src](Assets/VR4VET/Components/DataCenter) directory of this repository. You are encouraged to explore the code, make modifications, and customize the scenario to suit your specific needs. In this folder we have scripts, prefabs, tests and scenes. 