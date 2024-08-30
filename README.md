# Sylladex Project
## Overview
The Sylladex project is a prototype of an inventory system inspired by the Homestuck webcomic. The project includes various inventory management modes (Fetch Modi) such as Array, Stack, Queue, and Hash-based systems. Each mode has its own unique way of handling items.

## Project Structure
- Core: Game entry point, Entity and UI setup.
- Entities: Represent objects in the game world (items, player).
- FetchModi: Inventory management modes.
- Managers: Manage game objects (textures, animations, fonts, soundtracks, entities) and some global states (input, UI windows, inventory system).
- Graphics: Everything related to rendering on the screen (sprite logic, layering, animations, UI layout calculations).
- UI: User interface elements (button, label, canvas, inventory slot).

## Debugging
- The project uses Debug.WriteLine to log various actions and states (especially fetch modi). You can see these logs in the Output tab in Visual Studio.
