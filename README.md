# Anno FileDB Models

This Repository contains the code for a library that offers models for de-/serializing FileDB data to corresponding models in code.
This can be used if you want to deserialize or serialize a supported file type.

Currently, the Library only offers support for gamedata Files (so map data from an a7t file or island data from an a7m file) and a7tinfo Map Templates.

**AS OF GAME VERSION 16 ALL THE GAMEDATA FILES OF THE GAME MAPS AND ISLANDS AS WELL AS ALL MAP TEMPLATES HAVE BEEN VERIFIED WORKING!**

In the future more file types will be supported.

## Caveats
This repository currently ships with build 2.3 of the FileDBSerializer with the rewritten Serialization, which is needed to handle complex files like gamedata properly. If you want to use the models in your own code, make sure you use a FileDBSerializer with the rewritten Serialization in your program as well. When in doubt, use the .dll from this repo.
