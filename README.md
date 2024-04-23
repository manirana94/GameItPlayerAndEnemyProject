# GameItPlayerAndEnemyProject
 A Unity test tasks given by GameIT


Project Overview

This project is a game developed using Unity and C#. It employs several key programming concepts and approaches, 

which I have outlined below:  


Object-Oriented Programming (OOP): 

The project uses classes and inheritance, which are key concepts in OOP. For example, the ExplosiveBullet class is a subclass of the Bullet class, which allows it to inherit properties and methods from the Bullet class and override them as needed.  

Scriptable Objects: 

The project uses Unity's Scriptable Objects to store bullet data. Scriptable Objects are a flexible and efficient way to create assets that hold data. They are used to reduce the memory footprint of your game, and increase your workflow speed and flexibility.  

Pooling: 
The project uses a pooling system for bullets and visual effects. Pooling is a performance optimization strategy where you reuse objects instead of constantly creating and destroying them. This is particularly useful in a game where bullets and effects are frequently created and destroyed.  

Coroutines: 
The project uses coroutines, which are a kind of function that allows pausing and resuming execution over multiple frames. This is useful for creating delays or waiting for certain conditions to be met.  

Singleton Pattern: 
The BulletPooling class uses the Singleton pattern, which ensures that only one instance of the class exists in the project. This is useful for managing global state and providing a point of access to the system.  

Component-Based Design: 

Unity's approach to game development is heavily based on components. Each GameObject in Unity can have multiple components, each of which adds different functionality. This project follows this approach, with different scripts adding different behaviors to the game objects.  

Event-Driven Programming: 
The AIState class uses events to handle state transitions. This is a common pattern in Unity and C#, where events are used to respond to user input, system messages, or other triggers. 
 
Audio Management: 
The project uses Unity's audio system to play sound effects when a bullet hits a target. This involves using the AudioSource.PlayClipAtPoint method to play an audio clip at a specific position.  


Animation Management: The project uses Unity's animation system to control the animations of game characters. This involves using the Animator class and the Animator Controller to define animation states and transitions.  

These approaches and concepts are common in game development and are part of what makes Unity a powerful and flexible tool for creating games. I have implemented these concepts in this project to create an engaging and efficient game experience. 