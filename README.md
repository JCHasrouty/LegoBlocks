# LegoBlocks
> Tetris-like Simulator using Lego Blocks

[![NPM Version][npm-image]][npm-url]
[![Build Status][travis-image]][travis-url]

This is a Simulator of Lego Blocks created for the COMP-465 course at CSUN.

# Upcoming Features
### Legos
* Dynamically spawning lego blocks
  * Spawn at random position throughout the MxN base plate
  * Spawn with random rotation
  * Spawn with random color
* Placing lego blocks on the platform
* Model/Implement various lego blocks of various sizes
* Boundaries/Volume to limit legos to be placed within the plate that is spawned
* Ability to rotate legos
* Movement of legos throughout the X,Z plane
* Clear bottom _n_ levels of plate to make room for more legos (future implementation)
### Camera Movement
* Camera rotation around the plane
  * Camera will rotate and always look at center of plane
  * Camera will be allowed to move up/down/left/right
  * Zoom In/Out functionality
### UI 
* Implement GUI that will allow user to play, set plate size, or quit

# Notes
* Stay tuned for further updates. Game development is still in progress. 

# Authors
Jean Claude Hasrouty

<!-- Markdown link & img dfn's -->
[npm-image]: https://img.shields.io/npm/v/datadog-metrics.svg?style=flat-square
[npm-url]: https://npmjs.org/package/datadog-metrics
[npm-downloads]: https://img.shields.io/npm/dm/datadog-metrics.svg?style=flat-square
[travis-image]: https://img.shields.io/travis/dbader/node-datadog-metrics/master.svg?style=flat-square
[travis-url]: https://travis-ci.org/dbader/node-datadog-metrics
