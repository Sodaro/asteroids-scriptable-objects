Name: David Bång

I chose Assignment 2: Asteroid Destroyer

AsteroidSet is a runtime set with a dictionary of the asteroid instance ID:s and the corresponding Asteroid component. It is used by both the Asteroid Spawner and Destroyer scripts.

AsteroidDestroyer.cs subscribes to the AsteroidHit scriptable event.
When the event is invoked the destroyer checks whether the asteroid that was hit is large enough, in which case it invokes an AsteroidSplit event with the location and scale of the asteroid.

The AsteroidSpawner is modified so that it subscribes to the AsteroidSplit event, which allows it to spawn smaller asteroids in the position where a large enough asteroid was hit.

GameEventVector3_2 is a modified version of GameEventVector3 which has 2 Vector3 payloads (used for sending the location and scale of the bigger asteroids.