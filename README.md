# An Open Testbed for Mixed Reality Precise Rotation Guidance: Comparative case study of Arrow, Gestalt and Magnifier Cues

## Conference Paper - Ismar 2025 Daejeon - South Korea 

Welcome to the Open Testbed for Mixed Reality Precise Rotation Guidance project! This repository is designed to provide an open-source platform for exploring and evaluating mixed reality (MR) solutions for precise rotation guidance in various applications. Our focus is on evaluating the effectiveness of three distinct cueing mechanisms: Arrow, Gestalt, and Magnifier cues, through comparative case studies.

![Sample Banner](./assembly.gif 'Assembly')


**Overview**

In this project, we investigate how different visual cues in MR environments can enhance rotational precision and user interaction. This open testbed serves as a research tool to support experimentation, development, and benchmarking of MR techniques, particularly in industries requiring high-precision operations such as healthcare, engineering, and robotics.
The testbed is based on findings from the paper "An Open Testbed for Mixed Reality Precise Rotation Guidance: Comparative case study of Arrow, Gestalt, and Magnifier Cues" by Dastan et al. (2025), presented at the IEEE International Symposium on Mixed and Augmented Reality (ISMAR).

**Key Features**

Rotational Guidance Cues: Explore three distinct types of visual cues—Arrow, Gestalt, and Magnifier—that help users perform precise rotations within MR environments.

Comparative Analysis: Built to support the comparative analysis of cue effectiveness in guiding rotation with minimal error.

*Open Access: Designed to be an accessible, open-source resource for researchers, developers, and practitioners interested in advancing MR interaction techniques.*


 ## Citation Requirements

In case of any supplementary open testbed material, please cite the following paper:

- [Dastan, M., Vangi, F., Musolino, F., Coviello G., & Fiorentino, M. (2025, October). An Open Testbed for Mixed Reality Precise Rotation Guidance: Comparative case study of Arrow, Gestalt and Magnifier Cues. In 2025 IEEE International Symposium on Mixed and Augmented Reality (ISMAR) (pp. xxx-xxx). IEEE.](https://ieeexplore.ieee.org/document/xxxxx)

## Authors
Copyright (c) [2025] VR3GROUP- Polytechnic University of Bari, Italy

- [Mine Dastan](https://orcid.org/0000-0003-0555-155X),
- [Fabio Vangi](https://orcid.org/0000-0002-9378-4769),
- [Francesco Musolino](https://orcid.org/0009-0009-9052-7730),
- [Giuseppe Coviello](https://orcid.org/0000-0001-5255-2913),
- [Michele Fiorentino](https://orcid.org/0000-0003-2197-6574),


## License

[![MIT License](https://img.shields.io/badge/License-MIT-green.svg)](https://choosealicense.com/licenses/mit/)
[![GPLv3 License](https://img.shields.io/badge/License-GPL%20v3-yellow.svg)](https://opensource.org/licenses/)
[![AGPL License](https://img.shields.io/badge/license-AGPL-blue.svg)](http://www.gnu.org/licenses/agpl-3.0)



![Logo](https://www.dmmm.poliba.it/vr3lab/wp-content/uploads/2024/06/logopoliba.png)


## Hardware requirement


**Head-Mounted Display (HMD)**
- Meta Quest 3 – 2064 × 2208 px per eye, 120 Hz refresh rate, passthrough MR

**Rotary Encoder**
- LiebeWH, China — 100 pulses/rev, 6 terminals, ergonomic industrial standard

**Knob**
- Aluminum, 4.5 cm diameter × 2.5 cm height, 190 g

**Microcontroller**
- ESP32-WROOM (ESP32S Module with CP2102, Dual-Mode WiFi + Bluetooth)

**Connection Wires**
- 4 × Female-to-open jumper wires (10 cm)
- USB Cable (ESP32-PC)
- USB cable for serial communication (921600 bps)

**Foot Pedal**
- USB-connected, used for confirmation input during trials

**PC Setup**
- Intel i7 CPU, 16GB RAM, Nvidia GeForce RTX GPU

**3D Printed Structure**
- PLA, printed with BambuLab X1; includes:
– Knob Mount (encoder holder)
– Controller Mount (holds left-hand controller)

**Development Environment**
- Unity 2022.3.20f1
- XR SDK
- Meta XR Interaction SDK (OVR Rig + Passthrough layer)
- Unity Application Features
- Manages encoder + pedal input
- Launch cue scenes
- Logs trial data to .csv for each participant


## Directory Structure

**1. Unity_Project/**
Contains the full Unity project for the MR testbed.
Includes scene files, prefabs for visual cues, scripts for rotation guidance, and parameterized settings.

**2. Arduino_Sketch/**
Contains the Arduino code used for interfacing with hardware components like rotary encoders.
Includes the necessary libraries for encoding signal readings.

**3. UnityScriptReadEncoder.cs/**
Contains the Unity scripts used to process data from the rotary encoder.
Includes code for reading encoder signals and translating them into usable data within the Unity environment.

**4. Hardware_Lists.pdf/**
A detailed list of all hardware components required for the testbed setup.
Include specifications, model numbers, and suppliers for ease of replication.

**5. 3D_Printed_parts/**
Contains the STL files for 3D printed components used in the testbed.
These files represent any physical housing, mounts, or supports required for the hardware setup.

**6. Video_Presentation/**
A video showcasing the functionality of the testbed and a demonstration of how it works.

**7. Researcher_Guide_PDF/**
A comprehensive PDF guide for researchers explaining how to set up, modify, and use the testbed.
Includes installation instructions, software requirements, and how to run experiments with different cue configurations.

**8. Raw_Data/**
Contains raw experimental data collected from previous studies or testing.
Could include CSV files or other formats, with information on user performance, cue effectiveness, and test conditions.
    
## Usage

1. 3D Printing the Mouth Components
2. Assembling the Encoder to the Mouth
3. Clone repository
4. Install driver for ESP32
5. Upload arduino sketch on ESP32
6. Close Arduino IDE
7. Open Unity Project
8. Play scenes in folder (01_scene)
