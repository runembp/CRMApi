I've been using Clean Architecture / Onion architecture for a while. It works, no doubt about that, but it always feels like there has to be a lot of changes in a lot of different folders and classes, once you start to scale up.

This WebAPI is a personal project of mine, which will be migrated into my current clients repository quite soon. I wanted establish a blueprint that could be used in the future, both for my self and my new employees. 
The vertical slice architecture allows all the relevant code to be situated pretty much only one place, while still keeping up with Single-Responsibility-Principle and avoiding clutter in the code.
