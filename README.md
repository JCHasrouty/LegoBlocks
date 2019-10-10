# LegoBlocks
> Tetris-like Simulator using Lego Blocks

[![NPM Version][npm-image]][npm-url]
[![Build Status][travis-image]][travis-url]

This is a Simulator of Lego Blocks created for the COMP-465 course at CSUN.

# Upcoming Features
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
