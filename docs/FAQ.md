# FAQ

**Q:** NeoDefaults changes a setting I don't like. Can I replace it?

**A:** Yes. See
[Customizing NeoDefaults](More-Info.md/#customizing-neodefaults) for an explanation on customizing
the configuration.

&nbsp;

**Q:** I change a setting in the game, but it always goes away the next time I launch TF2. Why?

**A:** `neodefaults.cfg` is run every time TF2 starts up. Therefore, if you change something that
`neodefaults.cfg` modifies, it just will be set again the next time you launch TF2. See 
[Customizing NeoDefaults](More-Info.md/#customizing-neodefaults) for an explanation on how to make
sure your setting is not overridden.

&nbsp;

**Q:** Why is there a `custom.cfg`? Can't I edit the `neodefaults.cfg` config file directly?

**A:** The short answer is no. The `neodefaults.cfg` file is a read-only file.

The much longer answer is that this was done on purpose with future releases in mind. If there are
new versions of NeoDefaults in the future, and someone runs the program again, the newer NeoDefaults
will need a way to easily replace an old `neodefaults.cfg` file. In most practical situations,
NeoDefaults would just be able to delete the old `neodefaults.cfg` and replace it with a new one.

However, if a user manually changed parts of `neodefaults.cfg` to their own tastes, this complicates
things. It won't be easy to figure out which settings were added by the user and which ones should
be replaced. And if custom settings have been added, they should never be overwritten.

To solve this problem, a `custom.cfg` file is provided, where custom settings may be stored. This
guarantees that `neodefaults.cfg` will only ever contain changes made by NeoDefaults, so NeoDefaults
can safely delete the old file and replace it with an updated version. The file is set to read-only
to make it obvious that it should remain unmodified.

&nbsp;

**Q:** Where can I learn more about customizing TF2 with scripts/config files?

**A:** woolen has [a great video](https://youtu.be/cRGW4a1K_Io) where he explains his personal
customizations that can help give you a good idea of how config files work and what kind of settings
you might want.

&nbsp;

**Q:** Why does NeoDefaults set `cl_interp` to 0.0303 instead of 0?

**A:** TF2 sets the default value for `cl_interp` (AKA "lerp"), at 0.1, or 100 ms. According to the
the [Valve Developer Wiki](https://developer.valvesoftware.com/wiki/Interpolation), this value was
tuned for the Source engine during the era of dial-up modems (!), so it should absolutely be
lowered. It's insane that the default value for this variable hasn't been changed by the TF2 dev
team in over a decade.

Many players set their `cl_interp` to 0, and this is even recommended by wiki. However, since this
project is geared towards newer players, it's often the case that such players won't have the most
reliable internet connections, so they can benefit from having a little bit of lerp. NeoDefaults
sets this value to 0.0303 in order to strike a balance between lowering it and making the experience
slightly more playable for those with unideal network connections. It's worth mentioning that this
value is still ideal for players with stable connections that heavily use hitscan weapons, so using
0.0303 should not be considered a detriment.

&nbsp;

**Q:** Why doesn't NeoDefaults enable/disable (insert setting here)?

**A:** NeoDefaults is designed to be a short list of very cherry-picked customizations, and only
includes what is considered essential. If you think there's a critical setting missing, open [an
issue](https://github.com/kqarryzada/TF2-NeoDefaults/issues) on GitHub so that it may be considered
in a future release of the installer.
