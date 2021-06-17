# Mars Rover Instructions
Following the instructions you provide, moves two rovers in series to explore the surface of Mars.

NASA intends to land robotic rovers on Mars to explore a particularly curious-looking plateau. 
The rovers must navigate this rectangular plateau in a way so that their on board cameras can get a complete image of the surrounding terrain to send back to Earth.

Since according to the [NASA Rover Communications](https://mars.nasa.gov/mars2020/spacecraft/rover/communications/) page, it takes 5 - 20 minutes for a signal to travel from Earth to the rover Mars through the Deep Space Network, you will need to send all of the instructions before either rover moves. The rovers will move one at a time to minimize the likelihood of a collision.

## Input:
* The first imput will define a bounding box. The 0, 0 coordinate will be the furthest Southwest corner, while the coordinates you enter will be the furthest Northeast corner. If the commands you provide the rover will take it outside this bounding box, it will stop and return an error.
* The next inputs will be sets of positions and commands for each rover.
* The position is in the format X, Y, Z
    * where X and Y are integer coordaintes on the bounding box you have established
    * and Z is the first letter in the Cardinal direction the rover is facing.

Your input will look something like this:
```
5 5
1 2 N
LMLMLMLMM
3 3 E
MMRMMRMRRM
```

For more details on how NASA moves rovers around Mars, check out this [NASA Article](https://mars.nasa.gov/mer/mission/timeline/surfaceops/navigation/).

## To run in Visual Studio Code
* After cloning the repo, open the folder in VSCode.
* Launch the program using the `.NET Core Launch (console)` configuration.
* The Console output will be in the Integrated Terminal.