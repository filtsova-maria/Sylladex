# Sylladex Project
## Overview
The Sylladex project is a prototype of an inventory system inspired by the Homestuck webcomic. The project includes various inventory management modes (Fetch Modi) such as Array, Stack, Queue, and Hash-based systems. Each mode has its own unique way of handling items.

## Project Structure
- **Core**: Game entry point, Entity and UI setup.
- **Entities**: Represent objects in the game world (items, player).
- **FetchModi**: Inventory management modes.
- **Managers**: Manage game objects (textures, animations, fonts, soundtracks, entities) and some global states (input, UI windows, inventory system).
- **Graphics**: Everything related to rendering on the screen (sprite logic, layering, animations, UI layout calculations).
- **UI**: User interface elements (button, label, canvas, inventory slot).

## How to play
- Use WASD to move and E to pick up items. Click on items to fetch them. Some item slots can be disabled based on your selected fetch modus, you can configure your inventory system in the settings menu (top right corner).

![image](https://github.com/user-attachments/assets/bd75596b-eb38-487d-90d3-755ec1714f3f)
![image](https://github.com/user-attachments/assets/abcf8517-4235-422e-9534-f3f4537b1f40)

### Modus logic:
- **Array**: You can fetch any item by clicking on the slot. Inserting a new item puts it into the first available slot. When full, first item is replaced.
- **Stack**: You can fetch the leftmost item by clicking on the slot. Inserting a new item appends it to the left and pushes the rest to the right. When full, the leftmost item is replaced.
- **Queue**: You can fetch the rightmost item by clicking on the slot. Inserting a new item appends it to the left. When full, the rightmost item is ejected, causing the inventory to cycle through.
- **Hash**: You can fetch any item by clicking on the slot. Inserting a new item puts it into an index based on its name hash. When that index is full, the item with colliding hash is replaced.

## Debugging
- The project uses `Debug.WriteLine` to log various actions and states (especially fetch modi). You can see these logs in the Output tab in Visual Studio.

