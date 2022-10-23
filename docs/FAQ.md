# FAQ

**Q:** NeoDefaults changes a setting I don't like. Can I replace it?

**A:** Yes. See
[Customizing NeoDefaults](More-Info.md/#customizing-neodefaults) for an explanation on customizing
the configuration.

&nbsp;

**Q:** I change a setting in the game, but it always goes away the next time I launch TF2. Why?

**A:** `neodefaults.cfg` is run every time TF2 starts up. Therefore, if you change something that
`neodefaults.cfg` modifies, it just will be set again the next time you launch TF2. To avoid this,
you will need to override that setting in `custom.cfg`. See 
[Customizing NeoDefaults](More-Info.md/#customizing-neodefaults) for an explanation on how to do
this.

&nbsp;

**Q:** My custom HUD looks a bit different after installing NeoDefaults. How do I change it back?

**A:** NeoDefaults makes some minor HUD adjustments that are designed for use with the stock HUD.
If you installed everything NeoDefaults recommends, these HUD modifications may not play nicely with
your HUD. You can resolve this by deleting the `neodefaults-hud-tweaks.vpk ` file in the
`tf/custom` directory.

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
lowered. It's insane that the default value for this variable hasn't been changed by the TF2
development team in over 15 years.

Many players set their `cl_interp` to 0, and this is even recommended by wiki. However, since this
project is geared towards newer players, it's often the case that such players won't have the most
reliable internet connections, so they can benefit from having a little bit of lerp. NeoDefaults
sets this value to 0.0303 in order to strike a balance between lowering it and making the experience
slightly more playable for those with unideal network connections. It's worth mentioning that this
value is still ideal for players with stable connections that heavily use hitscan weapons, so using
0.0303 should not be considered a detriment.

&nbsp;

**Q:** Can NeoDefaults install [mastercomfig](https://mastercomfig.com/) in the future?

**A:** mastercomfig is an excellent plugin that we strongly recommend for anyone who plays TF2
regularly. However, it is not included with NeoDefaults because different players would want
different graphics settings. There are also technical reasons for avoiding this. For example, if
mastercomfig was bundled into NeoDefaults, NeoDefaults would probably have to release a new version
every time that mastercomfig did (so that people are always installing the latest version). This
is undesirable, so it is not included with NeoDefaults.

&nbsp;

**Q:** Why doesn't NeoDefaults enable/disable (insert setting here)?

**A:** NeoDefaults is designed to be a short list of very cherry-picked customizations, and only
includes what is considered essential. If you think there's a critical setting missing, open [an
issue](https://github.com/kqarryzada/TF2-NeoDefaults/issues) on GitHub so that it may be considered
in a future release of the installer.

&nbsp;

**Q:** I got a warning that said "Tried to modify autoexec.cfg and failed". What happened?

**A:** The autoexec.cfg file is checked by TF2 every time the game starts up. In order for
NeoDefaults to run on startup, this file must indicate that we want the `neodefaults.cfg` file to
run. This error is not expected to happen, but in case it did, here are some steps you can take to
fix this:

1. Open the cfg folder located at `<your TF2 install path>/tf/cfg`.
1. If you use mastercomfig, open the "user" folder. If you don't know what this is, you likely don't
have it installed, so ignore this step.
1. Find the "autoexec.cfg" file, and open it using a text editor like Notepad.
1. Add the following lines to the bottom of the file:
    ```
    //--------Added by the NeoDefaults Installer--------//
    exec NeoDefaults/neodefaults
    //--------------------------------------------------//
    ```
    This tells `autoexec.cfg` to run the `neodefaults.cfg` file located in `cfg/NeoDefaults`.
1. Save the file, and launch TF2. When TF2 has launched, hit the `~` character to open the
console.
1. There will be a bunch of text written there, but among all of it, you should see the following
    message:
    ```
    -------------------------------------------------------------------------
    ------------------ NEODEFAULTS (version number) LOADED ------------------
    -------------------------------------------------------------------------
    ```
    If you see this message, then you've fixed the autoexec file successfully. If not, something
    went wrong. Consider opening an [issue](https://github.com/kqarryzada/TF2-NeoDefaults/issues)
     on GitHub for additional help.


&nbsp;

**Q:** I installed the hitsound through the Advanced Install, but I don't hear it in-game.

**A:** There's an in-game setting that turns hitsounds on and off, and it is off by default.
`neodefaults.cfg` enables this for you, but it's likely that you opted out of the config install.
To fix this, open the Advanced Options menu in TF2 and enable the "Play a hit sound" setting.
