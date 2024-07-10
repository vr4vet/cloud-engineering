![](ReadMePictures/coverImage.png)

# VR Data Center Internship Scenario

Welcome to the VR Data Center Internship Scenario! This immersive experience offers an exciting opportunity to explore the world of data centers and gain practical knowledge through virtual reality (VR) simulations. This README.md file serves as a guide to install the 

## Table of Contents

- [Overview](#overview)
- [Getting Started](#getting-started)
- [Usage](#usage)
- [Testing](#testing)
- [Code](#code)

## Overview

In this VR data center internship scenario, you will step into the shoes of a data center intern and take on various tasks and challenges related to managing and maintaining a data center environment. The scenario is designed to provide you with hands-on experience and insights into the world of data centers, including networking, infrastructure, security, and more. The user is guided with the use of the tablet. The tablet contains tasks and subtasks that are stored within the taskholder, which is present in the hierachy of the scene. The player can also be guided by the NPC, this might be a more natural way of guiding the player.

## Getting Started

To get started with the VR data center internship scenario, follow these steps:

Instalation/Requirements


1. Download Unity 2021.3.5f1 (this was the latest LTS at the time the project was started )
2. Clone the Develop branch using git clone.
3. Open unity
4. Go to ‘Window’ -> ‘Package manage’ and make sure Open XR Plugin is installed. If  already there ignore this step.
5. Within Assets there is an empty folder called BNG framework. This contents of the folder are not allowed to be in a public git. Ask coworkers for the contents of this folder.
6. Load the Data Center Scene 

## Usage

During the VR data center internship scenario, you will encounter various tasks and challenges that simulate real-world scenarios. You will be guided through these tasks and provided with instructions and resources to complete them successfully. Make sure to take advantage of the interactive nature of the VR environment and explore different aspects of data center operations.

## Code

The source code for the VR data center internship scenario can be found in the [src](Assets/VR4VET/Components/DataCenter) directory of this repository. All changes made for this scenario specifically should be within this folder. For the prefab folder, try to only have prefabs used in the scene in the folder and use folders when a prefab is made from different materials and textures etc.

The scenario uses a lot of prefabs from the core repository, these can be implemented using packages, which can be found in the releases of the core repository.

## Sideloading to the Quest headset

There is a text file on the VR4VET computer on the desktop with instructions on how to load the application onto the headset. This file also contains the password for signing the application.

## Releases
For every update in the scenario, a release should be made. For the release notes please use the following structure:
### **Added:**
- Something new in the build that was not present in the previous build.
### **Changed**
- Something that has changed in comparison to the last build.
### **Fixed:**
- Problems/bugs that were present in the last build that are fixed.

The release should always have a signed build next to its source code.
