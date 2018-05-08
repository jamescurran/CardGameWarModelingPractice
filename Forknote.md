Changes from the original:

- Created a "Deck" class which holds the cards.  Can be based on either a List<> (as the original) or a Queue<>, deoending of an #if true
- Allows 3 levels of verbosity.  (there no UI for this.  Changes require editing the code & recompiling)
- Made the infinite game limit and game count variables.
- Allows an explicit seed for Random, so games can be repeatable, for testing.
- Added a stopwatch
- Count games by moves (in a range)
