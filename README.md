Adventure (Unity Prototype)

A systems-focused Unity prototype featuring a tile-based world, resource economy, inventory management, and foundational gameplay systems such as AI, building placement, and save/load.

Overview

This project is an in-progress prototype focused on building scalable gameplay systems rather than a finished game.
It explores resource flow, spatial decision-making, and inventory constraints within a structured world.

Features
Resource & Economy System
Mines can be interacted with to gather resources
Auto resource collection system
Resources flow through a structured inventory system
Transaction system supports flexible spending logic
Inventory System (Bag & Vault)

Bag (Player Inventory):
Always accessible
Limited capacity
Used for immediate actions and spending


Vault (World Structure):
Placed in the world as a buildable structure
Stores excess resources
Acts as long-term storage

Core Mechanic:
Player must physically move resources from Bag → Vault
Spending prioritizes Bag, then pulls from Vault if needed

Encourages:
Movement and positioning decisions
Resource routing and planning
Strategic base setup

Mine System
Mines exist as interactable world objects
Supports automated resource collection
Feeds directly into the player’s inventory system

Building / Summoning System
Real-time placement validation: 
Green indicator → valid placement
Red indicator → invalid placement
Supports spawning of structures (e.g., towers etc)
Prevents invalid placement using environment checks

Save & Load System (JSON)
Prototype save/load using JSON serialization
Currently saves: 
Mine locations
Spawned/summoned structures (including vaults)
Purchased sphere data
Designed to be extendable for full game-state persistence

AI System (Prototype)
Basic AI entities implemented
Can: 
Take damage
Deal damage
Serves as a foundation for future behavior systems

Technical Highlights
Built in Unity (C#)
Modular system architecture (economy, inventory, AI, saving)
Spatial inventory design (player vs world storage)
JSON-based persistence system
Real-time placement validation logic
Integrated automation (resource collection)

Current State
This is an active prototype.
Focus so far:
Core systems integration
Spatial inventory + resource flow
Persistent world data via save/load
Planned Improvements
Expand AI behavior (pathfinding, decision-making)
UI for inventory and system feedback
Economy balancing and tuning
Full world-state saving
Performance optimization

Author
David Adebesin
Unity Game Developer
