**Inventory & Hotbar System (Unity)**
**Overview**

This package provides a fully functional inventory and hotbar system for Unity projects.
It includes item definitions, inventory management, drag-and-drop functionality, tooltips, and a customizable hotbar.

The system is designed to be modular, lightweight, and easy to integrate into your own projects.

**Features**

Item system with ScriptableObjects (easy to create new items)

Inventory with stackable items

Hotbar with quick access slots

Drag & drop support (between inventory and hotbar)

Tooltips (show item name, description, and icon)

Shift / Right-click drag split system (move one item or half a stack)

Clean UI setup (ready to be customized with your own graphics)

Demo scene included

**Setup**

Import the package into your Unity project.

Open the DemoScene located in:

Assets/Drag & Drop Inventory & Hotbar Framework/Scenes/Really Simple.unity

Play the scene to test the inventory and hotbar system.

**How to Use**

You can just use the Player Prefab and change it to your liking or copy the Inventory (Canvas) and the ToolParent (under Main Camera).
Or if you don't want to do that, just look at the Player structure.

Create new items:

Right-click in your Project window → Create → Inventory → Item.

Configure item properties (icon, name, description, max stack, model (leave null if you have no model, that's fine) etc).

Add items to the inventory (in code):

On the Player is a ItemPickupHandler. Get that using GetComponent and then call the function PickupItem with the parameters Item and optionally the amount

**Requirements**

TextMeshPro (included with Unity)

**Notes**

The demo uses models and sprites from different Asset Packs. You can easily replace them with your own models, icons, and UI graphics.

License

This package is provided under the Unity Asset Store EULA
.
You may use it in personal or commercial projects, but you may not redistribute or resell the package itself outside the Asset Store.
