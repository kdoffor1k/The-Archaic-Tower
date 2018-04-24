# The-Archaic-Tower


## Release Notes:

### Features: 

#### Wizards Lab:

- EXP Shop Menu is located in front of the bookshelf and comprised of spell components that the player can purchase by losing the correct amount of EXP. This allows their purchased spell components to be visible within the spell crafter menu and available to use when assembling spells.

- Spell Crafter Menu located above the caudron is initially comprised of only the arrow spell given to the user. To obtain more spells in the spell crafter, the player must purchase items in the Experience Shop. By clicking on items available in a player’s Spell Crafter Menu, a player is able to assemble new spells. Spell components that are chosen appear on the empty canvas on the menu, and when the player clicks save the spells are added to their hotbar. Spell combinations are restricted to the guidelines listed below:
 - Must have one Spell Core.
 - At most One Spell Element.
 - Multiple Spell Modifiers, with Multiples of the same type.
- Go to practice scene, Leave Wizard Lab, leave the tower, and go to practice arena allows a player to teleport to different scenes within the game. 
#### Assembled Spells Inventory Management:
- Hot Bar is a feature allowing a player to have a limited number of spells they can bring into battle. The hot bar is comprised of four slots that are filled with each spell combination you saved when assembling spells in the spell crafter menu. The Hot Bar is shown in the bottom of the player’s field of view at all times.
#### Top of Tower:
- Tower Health is visible within the tower to the left of the main window, where the player can easily view when defending the tower from upcoming enemies.
- When completing a level a script will appear in front of the main window in the tower urging the user to click on the button below the script to be transported to the wizard lab. 
- When clearing a wave of enemies a text will appear in front of the main window of the tower urging the player to either click the button below the text to move on to the next wave of enemies or leave the tower.
#### Enemies:
- Enemies and their corresponding portals are color coded based on their elemental alignment of either fire, nature, or water.
- Attacking Enemies is based of a rock, paper, scissors methodology corresponding with the elemental alignments of fire, nature, and water (fire beats nature, nature beats water, water beats fire). When choosing spell elements a player needs to think about the type of elemental alignments the enemies might have in the game. This way the player can use an appropriate spell element against the correct enemy in order to deal damage to that enemy.  
- Enemies are spawned based on the player’s behavior. When players assemble spells with one spell element more so than the others, portals of a certain elemental alignment will be made to counteract the player’s behavior by forcing them to switch up their assembled spells. For example, if a pyromaniac player casts nothing but fire spells, they will be faced with a plethora of water enemies.
- Enemies can deal damage to tower.
#### EXP and Gold:
- A player can gain EXP and Gold by defeating enemies.
- EXP can be used to purchase new spell components from the EXP shop that will then appear on the spell crafter menu where you can use them for assembling spells.
- Gold is used to upgrade the tower health when in between levels using the gold shop in the wizard lab.
#### Gestures: 
- Each slot in a players quick bar has a corresponding gesture to it in either the shape of a line, circle, or upside down triangle. These gestures are used to cast the assembled spells from the player’s hotbar.
#### Practice Arena:
- Test out new assembled spells to see how they pair up against other spells a player has made.

#### Full List of all Spell Components:
- Elements 
 - Fire
 - Water
 - Nature
- Core 
 - Arrow
 - Shield
 - Beam
 - Meteor shower
 - Lightning
 - Gravity ball
 - Walls
 - Summon defender golems 
 - Shockwave 
- Modifiers
 - Size
 - Damage
 - Speed
 - Gravity (pulls enemies in)
 - Multiplier (double the amount, 2 arrows -> 4 arrows)
 - Poison 
 - Explosion 
 - Paralyze 

Bugs Fixed Since Last Sprint:
Enemies randomly running away from tower.
Enemies being thrown out of map after being hit by gravity modifier
Portals randomly increase to gigantic size
Portals keep spawning enemies even after game says you've won.
After towers health reached zero, game would still advance to next level 
Enemies all spawn with the same element attached
Meteor Shower portal spawned on ground instead of in the air
Arrow not being casted when in the first quickbar slot
player was flipped around when switching scenes
Spells would break if where not ordered correctly in the crafting menu
Player could erase spells from the quick bar while in level
Player could return to lab button while a level is playing
Going back to wizard tower directional light would not be counted for rendering, causing the scene to become extremely dark
GUI would be hidden if was inside an object
Several attributes were not saved correctly in the save data.
Gold shop did not shows price to upgrade tower
Controller UI cursor gets drawn behind the UI elements
Multiplier modifier would put the spell too high
Multiplier modifier would always place the second copy in the same place as the first copy
Gravity modifier caused the spell to despawn before dealing damage
Meteor shower cloud would  spawn on the floor
Meteor shower used to have graphical anomalies.
Arrow prefab was pointing the wrong way and colliders were not set correctly
Assembled spell quickbar UI was too low, always dragged on the floor

Known Bugs: 
Player can place spells inside the tower
Player can draw a spell while already having one ready to fire (causes spell effects to stack)
Game sometimes says player has beaten level even though there are still portals up, causing new enemies to spawn after the level is declared over
one component spells will sometime not be cast due to a problem reading them from the save data writer reader
Very very rarely a beam prefab ill appear around the controller when saving spells
A paralyzed enemy never moves again
Haptic feedback is inconsistent, will not happen if enemy is killed in one shot
Very rarely magic ball will shoot off in a random direction
Shockwave spell does not instantiate, probably a problem with the shockwave prefab


Installation Guide: 
Prerequisites:
Hardware
OS: Windows 7 SP1+, 8, 10, 64-bit versions only; Mac OS X 10.9+. 
CPU: Intel® Core™ i7-6700 Processor, equivalent or better
GPU: NVIDIA GeForce™ GTX 970, equivalent or better
Virtual Reality System: HTC Vive headset, 2 HTC Vive Controllers, 2 HTC Vive lighthouses (set up correctly)

Software
Unity 2017.3.0+

Dependent libraries that must be installed:​ 
SteamVR (Most recent release)
All other dependencies are already included in release directory



Download instructions:​ 
To download our project click on the green “clone or download” button near the right top of the screen on our Github repository (or from direct link to google drive https://drive.google.com/file/d/1uWjeS--5SZFSLLLGgadBEsi0rQWvn0DQ/view?usp=sharing).
Choose to download the .zip file 
Extract the file to the location of your Unity projects folder. 

Installation of actual application and Running App:
Download Unity: https://store.unity.com/download?ref=personal
Open UnityDownloadAssistant
Select highlighted components:

Setup HTC Vive: https://support.steampowered.com/steamvr/HTC_Vive/
NOTE: this is the most fragile part of the insulation process, make sure to get your computer to recognize the VIVE headset as well as make sure your area is set up for VR and configured correctly. 
Open Unity
Select the “Open” option in the top right toolbar
Select project archive and launch the project


Troubleshooting:

Steam VR Help Page: https://support.steampowered.com/kb_article.php?ref=8566-SDZC-9326
Unity Support Page:
 	https://unity3d.com/learn/support
HTC VIVE Support Page:
	https://www.vive.com/us/support/vive/

