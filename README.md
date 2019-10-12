# LegoBlocks
> Tetris-like Simulator using Lego Blocks

[![NPM Version][npm-image]][npm-url]
[![Build Status][travis-image]][travis-url]

This is a Simulator of Lego Blocks created for the COMP-465 course at CSUN. The game will feature three modes. The modes are as follows: Uniform, Break-Away, and Building Blocks.

# Modes
### Uniform
Uniform mode will consist of a variety of Lego blocks spawning at random locations within a constricted boundary space set by the plate size. The mode is specific in the sense that the blocks will remain uniform upon collision with other blocks.
### Break-Away (WIP)
Break-Away will consist of the same attributes as uniform but with a twist. The blocks will be allowed to break away similar to tetris. If a block collides with another, is bigger, and partially hangs off the side, it will break-away and the remaining pieces will fall down onto other blocks or the plate. 
### Building Blocks (WIP)
Building Blocks will consist of allowing the user to spawn blocks with the click of a button (considering the restrictions are met).

# Upcoming/Current Features
### Legos
- [x] Dynamically spawning lego blocks
  - [x] Spawn at random position throughout the MxN base plate
  - [x] Spawn with random rotation
  - [x] Spawn with random color
- [x] Placing lego blocks on the platform
- [x] Model/Implement various lego blocks of various sizes
- [x] Boundaries/Volume to limit legos to be placed within the plate that is spawned
- [x] Ability to rotate legos
- [x] Movement of legos throughout the X,Z plane
- [x] Clear bottom _n_ levels of plate to make room for more legos (future implementation)
### Camera Movement
- [x] Camera rotation around the plane
  - [x] Camera will rotate and always look at center of plane
  - [x] Camera will be allowed to move up/down/left/right
  - [x] Zoom In/Out functionality
### UI 
- [x] Implement GUI that will allow user to play, set plate size, or quit
- [ ] Update Main Menu and Setup Configuration to include various modes
In Building Blocks Mode, user will be provided with the following:
  - [ ] UI Panel displaying the Lego block types for selection
  - [ ] UI Panel displaying the Lego block color or material
  - [ ] UI Panel displaying general features such as:
    - [ ] Delete
    - [ ] Duplicate
    - [ ] Copy
    - [ ] Undo
### Game Play
#### Break-Away
- [ ] Implement various Rigidbodies on a single block in order to allow pieces to break-away from each other
- [ ] Implement mode and conduct testing to ensure seamless functionality
#### Building Blocks
- [ ] Dynamically display a semi-transparent cube while user is deciding where to place the cube on the board and or other cubes
- [ ] If the mouse is on the board, display a transparent block material showing the user where they are allowed to place/not place blocks
- [ ] If the mouse is on a face of a block and placement is allowed, display a green transparent block

# Notes
* Stay tuned for further updates. Game development is still in progress. 

# Authors
[Jean Claude Hasrouty](https://www.linkedin.com/in/jean-claude-hasrouty/)

<!-- Markdown link & img dfn's -->
[npm-image]: https://img.shields.io/npm/v/datadog-metrics.svg?style=flat-square
[npm-url]: https://npmjs.org/package/datadog-metrics
[npm-downloads]: https://img.shields.io/npm/dm/datadog-metrics.svg?style=flat-square
[travis-image]: https://img.shields.io/travis/dbader/node-datadog-metrics/master.svg?style=flat-square
[travis-url]: https://travis-ci.org/dbader/node-datadog-metrics
