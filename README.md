# DUMFUC
### ***Doom's Unity Modding Framework with Useful Content***
DUMFUC is a BepInEx mod framework that aims to make modding (IL2CPP) VR games easier.
It's designed to be completely game-agnostic, so code that works in one project should work in another with minimal changes.

I tried to make the features as generic as possible, but a lot of them are specifically for my needs. You may still find them useful.

## Modules
### Input
Allows you to easily define input actions with configurable bindings that persist through sessions.
Input can come from a variety of sources, and the API can be extended to support more.

### Patching
A wrapper around Harmony that makes patching much cleaner, safer, and less "string-based". 
I specifically use this to avoid having to keep my patch classes unobfuscated.