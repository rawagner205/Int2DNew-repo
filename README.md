INTRO
Set sail to race on a small raft, avoiding obstacles along the way to the finish line! 

FULL DESCRIPTION
	In this game, players control a wooden raft through steering side-to-side. This can be done with either left and right arrow keys, or by using A to move left and D to move right. Players must avoid colliding with the many rocks in their way and the walls of the race path, or the raft will crash and break, ending the game. If players reach the finish line without any collisions, then they've won! Along the way, there are also fish that can be collected to give the player a five-second boost to their steering speed. 
	This game's mechanics are simple and easy to learn, but it's a challenge to get to the end of the race. There is not one singular correct route to take around the rocks, so it's up to the player to find the best approach on their own. A built-in restart buttons also allows the player to start over and try again if they crash the raft. The game features water-themed assets in a simple pixel-art style, as well as some particle effects for visual appeal. 

TECHNICAL IMPLEMENTATION
GameObjects:
	The Player GameObject contains the raft sprite that the player steers (to which the PlayerController script that runs this game is attached), as well as the camera that follows the player. This object reacts directly to input from the player to move the sprite and its camera according to the player's directions. It also reacts to the game's win and lose conditions. If the player crosses the finish line, the sprite stops moving and triggers the win message. If the player collides with an obstacle, it destroys the sprite with a particle-effect explosion and triggers the lose message. 
	The Borders and Obstacles GameObjects both function very similarly to each other. Neither of these GameObjects moves or does anything on its own, but if the player sprite contacts any of their child objects, it will register as a collision and trigger destruction of the player sprite. 
The Boosts GameObject is similar in its lack of activity, but when the player collides with a boost, it will trigger a temporary increase in the player's speed. The FinishLine also does not move, but triggers the win condition in the game when crossed (fully crossed, not just touched).

Technical Requirements:
	I believe that I have fully met the technical requirements of this project. I have one complete, fully playable level with responsive player controls, in which the objective is to reach the finish line without any collisions. All sprites are chosen to fit the game's theme, and all of the resulting objects possess Colliders and Rigidbodies. Collisions to these objects register correctly, to either trigger effects like a speed increase when a boost is collected, or the destruction of the player object and presentation of game over text when an obstacle is hit.  Because Boost, Border, and Obstacle objects are reused during the game, they are saved as prefabs. Additionally, particle systems are used to create an explosion when the raft crashes, as well as a wake behind the raft. 


FUTURE DEVELOPMENT PLANS
	There are several ways I could anticipate expanding this game. Firstly, more levels could be added with longer race paths and different obstacle layouts, reusing and modifying elements already saved as prefabs. There could also be different types of obstacles on these new levels, such as ones that move in ways the player must avoid. Additionally, different types of boosts could be added, such as a boost that grants temporary invincibility to collisions by adding another outcome to OnCollisionEnter2D. 
	I think that the theme of the game could also definitely be expanded, as there is a lot of potential to add to the nautical theme. For example, if other boosts or obstacles were to be added, they could utilize new sprites, such as other boats or buoys as obstacles, and other kinds of fish as boosts. This could add more variety to the game. To add more story to this theming, the player raft could also be changed out with a different kind of boat on a "mission" of some kind that is more specific than finishing a race. 

REFLECTION
	The thing I struggled with the most was using Cinemachine. I was not able to get my camera to behave the way I initially wanted it to, and despite spending a lot of time trying to find solutions, I eventually had to pivot to different ideas. This was very frustrating, but taught me to prioritize my concerns, as in the end, my problems with the camera weren't actually affecting gameplay and did not need to take as much of my focus and energy as they did. In the future, I will be more conscious of my workflow to not get so "stuck" on a minor annoyance again. 
	I did, however, learn quite a bit about Unity over the course of working on this project. For all my struggles with Cinemachine, I did get more familiar with its settings and their effects. I also got more comfortable using UI text, including interactive text and dynamically updating text for the boost mechanic. From working on this same mechanic, I also learned how to work with code for timers and triggers. I also think I got very good at working with the particle system to get the very specific results from it that I wanted. 

ASSETS USED
Raft Sprite - Sevarihk, https://opengameart.org/content/animated-pixel-art-raft-sprite
Rock Sprite - Yoann Sculo, Email: yoann.sculo@gmail.com, Twitter: @yoannsculo, Website: www.yoannsculo.fr, https://opengameart.org/content/animated-2d-rock-from-simerion-resources
Fish Sprite - Mepavi @ You're Perfect Studio, https://opengameart.org/content/swimming-fish

