# Zombie Survival – Unity Test Assignment

## Overview
This project is a **top-down zombie survival prototype** built in Unity.

The goal of this assignment was to demonstrate **gameplay systems, AI behavior, and clean architecture**, including:

- Player movement and health
- Noise-based enemy detection
- Zombie AI using a State Machine
- Player weapon shooting
- Enemy chasing and attacking
- Game Over UI with restart functionality

The focus of the project was **system design and modular code architecture** rather than visual polish.

---

# Gameplay Loop

1. Player moves around the environment.
2. Player footsteps generate **noise**.
3. Zombies detect the noise and **investigate the position**.
4. If zombies **see the player**, they switch to **chase mode**.
5. Zombies attack the player when close enough.
6. Player can shoot zombies using a gun.
7. If the player dies, a **Game Over screen** appears with a **Restart button**.

---

# Core Systems

## Player System
Handles player movement, animation, health, and shooting.

### Features
- CharacterController based movement
- Footstep sound and particle system
- Footstep noise emission
- Player health and death logic
- Gun shooting using raycasts

# Controls

| Input | Action |
|------|------|
| WASD | Move player |
| Mouse Left Click | Shoot |
| Footsteps | Generate noise |

---

# Implemented Features

- Player movement and animation
- Footstep noise system
- Zombie hearing detection
- Zombie vision detection
- Zombie AI state machine
- Zombie chasing behavior
- Zombie melee attack
- Zombie death
- Game Over UI
- Restart functionality
