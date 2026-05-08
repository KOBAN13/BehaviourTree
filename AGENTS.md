# Repository Guidelines

## Project Structure & Module Organization

This is a Unity 6000.4.2f1 project. The main project-owned code lives in `Packages/com.tankoduck.ai`, a Unity package for runtime AI agents, behavior trees, blackboards, sensors, enemy logic, animation helpers, stats, and state machines. Keep new runtime C# code under `Packages/com.tankoduck.ai/Runtime`, grouped by domain such as `Game/AI`, `Game/Enemy`, `Game/Stats`, or `Animation`.

Unity scenes and project assets live in `Assets`; the current sample scene is `Assets/Scenes/SampleScene.unity`. Project-wide Unity settings are in `ProjectSettings`. Third-party or generated dependencies include `Packages/com.kybernetik.animancer`, `Assets/Plugins`, `Assets/Packages`, and `Assets/NuGet`; avoid editing these unless you are intentionally updating a dependency.

## Build, Test, and Development Commands

- `Unity -projectPath "$(pwd)"`: open the project in the Unity Editor.
- `Unity -batchmode -quit -projectPath "$(pwd)"`: import and compile the project in batch mode.
- `Unity -batchmode -quit -projectPath "$(pwd)" -runTests -testPlatform EditMode -testResults TestResults/EditMode.xml`: run EditMode tests.
- `Unity -batchmode -quit -projectPath "$(pwd)" -runTests -testPlatform PlayMode -testResults TestResults/PlayMode.xml`: run PlayMode tests.

There is no committed Makefile or scripted build method yet. Use Unity Build Settings/Build Profiles for local builds, or add an `Editor` build method before wiring CI.

## Coding Style & Naming Conventions

Use C# with 4-space indentation and Allman braces, matching existing files. Use PascalCase for types, methods, and public properties; use `I` prefixes for interfaces (`IState`) and existing `E` prefixes for enums (`EEnemyType`). Private fields use `_camelCase`, often with `readonly` when dependencies are assigned in constructors. Preserve Unity `.meta` files whenever adding, moving, or deleting assets.

## Testing Guidelines

The Unity Test Framework is available via `Packages/manifest.json`, but no project-owned tests are currently present. Add tests under `Packages/com.tankoduck.ai/Tests/EditMode` or `Packages/com.tankoduck.ai/Tests/PlayMode` with matching `.asmdef` files. Prefer focused EditMode tests for behavior tree, blackboard, state machine, and stats logic. Name test classes `ClassNameTests` and methods `Method_State_ExpectedResult`.

## Commit & Pull Request Guidelines

Git history currently contains only `Initial commit`, so use a simple imperative convention: `Add melee behavior tree action`, `Fix stamina restoration`. Keep subjects under 72 characters and group related `.cs`, `.asset`, and `.meta` changes together.

Pull requests should include a concise summary, the Unity version used, test commands/results, linked issue or task, and screenshots or short clips for scene, inspector, animation, or visual changes.

## Agent-Specific Instructions

Do not commit `Library`, `Temp`, `Logs`, `obj`, `UserSettings`, or IDE metadata. Prefer changes in `Packages/com.tankoduck.ai` over modifying vendored packages. When adding dependencies, update `Packages/manifest.json` and keep `Packages/packages-lock.json` in sync.
