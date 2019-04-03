# 2D-Shading :gem:
![light](https://user-images.githubusercontent.com/43205483/55514421-54c07f80-5668-11e9-8797-5e85147e8968.png)
## What can a user do?
* Set triangles' textures(color), normal maps and height maps.
* Move triangles' vertices
* Set surface properties for both triangles (Phong factor and weight) - determining the shape and intensity of light source reflections.
* Choose the light source to be either static or animated.
* Set the color of the light source (default is white).
## What can the program do?
* Fill the triangles based on given parameters
  * Uses _Phong reflection model_
  * Calculates each pixel color independently in real time
## Other facts
All the calculations are being done on CPU. The application is multithreaded and when drawing new frames (moving light source or triangles) heats up all the processor cores very efficiently :fire:
