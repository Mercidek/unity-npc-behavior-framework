# Unity NPC Behavior Framework

This tool was developed to easily implement states on a **GameObject** and switch between those states using predefined and customizable decision cards in Unity projects.

## 🔑 Key Features
* Efficient core data storage via ScriptableObjects
* Conditional state switch system with various decision cards
* Ready-to-use core object states are included
* NavMesh and Animator support
* INPCBehavior interface for applying behavior system
* AIContext for storing separate NPC behavior data
* Basic debugging visuals to improve workflow
* Composite decision card for combined state switch conditions
* Organized project folder structure for better layout

## 💻 Developer Interactions
* Creating states and decision cards via ScriptableObjects and customizing their attributes
* Customizing StateManager GameObject attributes

## 🔨 How to Use

### 💾 Installation

**1.** Clone this project to your local machine using Git or download it directly as a ZIP file 
```bash
   git clone https://github.com/Mercidek/unity-npc-behavior-framework
```

**2.** Open the project using Unity Hub *(tested in version 6000.3.18f1)*

**3.** Open the **BehaviorDemoScene** scene and run it to use the system

### 🔧 Getting Started
---
A demo scene is included to help developers easily get started with the system. This scene includes an example of a **StateManager** GameObject which has the ability to handle state operations, a **target** GameObject for related states of the manager object, and a **plane** GameObject with NavMesh baked in.
> As shown in the example, the **StateManager** script can be attached to a desired GameObject requiring state operations.

In this script, various behavior features like initial state, target object, NavMesh agent, Animator, waypoints for patrol state, state attributes and state switching list can be configured with decision cards.

![StateManager script](https://github.com/user-attachments/assets/e7af1d5a-3bb4-4c82-9ab9-d31473b8d9e8)


### ⚙ States
---
**Creating a State**

States can be created from the context menu by following `Create > NPC Behavior System > State > New {state name} State`

![State ScriptableObject](https://github.com/user-attachments/assets/4b4852ba-8b37-46b6-94e4-401bedb21923)

> Bool name for the Animator of the state can be set from here.

### 🎲 Decision Cards
---
**Creating a Decision Card**

Decision cards can be created from the context menu by following `Create > NPC Behavior System > State > Decision Card > New {card name} Decision Card`

![Decision Card ScriptableObject](https://github.com/user-attachments/assets/c62e3ce0-cabc-420e-9d75-5eb12be7b362)



> Attributes of the related decision card can be set from here.

### 🔀 State Transitions with Decision Cards
---

> A list of transitions can be set in **StateManager** script using the desired decision card.

![Transitions List in StateManager script](https://github.com/user-attachments/assets/facd4dca-a9a5-4155-87b3-0a115ec1d504)


> An "And-Composition" decision card can also be created and be used as a transition decision card. This card combines multiple decision cards and evaluates them using an 'AND' condition.

![And Composition decision card](https://github.com/user-attachments/assets/a216ada5-6246-4712-bdaa-ae6124829d0b)

## ❗ Known Limitations
* Animator support is available but it hasn't been tested
