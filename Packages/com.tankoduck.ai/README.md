# Tankoduck AI

Runtime package for the Tankoduck enemy AI stack.

The package contains:

- behavior tree nodes, actions, policies, beliefs, and debugger contracts;
- blackboard data/controller/runtime values;
- enemy agent base classes, melee enemy logic, sensors, strategies, and enemy weapon runtime;
- supporting animation, stats, state machine, layer, and enemy contract code required by the AI.

Project dependencies that must be installed by the host project:

- R3 Unity package
- UniTask
- VContainer
- Animancer
- Odin Inspector
- DOTween

Keep project-specific scene setup, prefabs, spawn configuration, and visual assets outside this package unless they are required for reusable AI runtime behavior.
