# The NeoDefaults Installer for Team Fortress 2
NeoDefaults is a no-nonsense config installer geared towards new players of TF2. It is designed to be
fast and seamless, and it will apply better game settings in under a minute. Better yet, it only needs
to be run once.

## Download and Install
The fastest way to install should take you about a minute to complete, download time included.
1. Download the latest [release](https://github.com/kqarryzada/NeoDefaults/releases).
1. Run the `.exe` file.
1. Select "Basic Installation", install the files, and exit NeoDefaults.

That's it. After this, you're ready to launch TF2 and take advantage of the new settings.

## Minimum Requirements
NeoDefaults is currently suported for Windows 8, Windows 10, and Windows 11 systems.

## Why Use NeoDefaults?
Team Fortress 2's default settings are horribly out-of-date, and terrible for new players.
Fortunately, TF2 is extremely customizable, so these bad settings can be (and are) easily
changed by seasoned players. However, this can make things worse for new players, because they are
now using completely different settings than their opponents, putting them at a disadvantage.

This program solves this issue by changing the game settings to a set of "new defaults". NeoDefaults
only focuses on necessary changes and avoids anything that could infringe on personal preference.
Some of the changes include:
* Disabling mouse acceleration to help with aim
* Improving network settings to make the game more responsive
* Updating the FOV settings so that the screen is less obscured

The installer does the work for changing game settings and graphics for you. All changes that
NeoDefaults makes are fully supported by Valve and are not considered exploits. For details on how
the NeoDefaults Installer accomplishes this, see [How It Works](docs/More-Info.md#how-it-works).

## What Gets Changed
The most noticeable differences are probably the settings related to the FOV and viewmodel:
![Soldier FOV changes](
https://raw.githubusercontent.com/kqarryzada/TF2-NeoDefaults/master/resource/readme-images/soldier_fov.png)
With the default settings, the Sniper is nearly invisible because the viewmodel covers his body.
Furthermore, the perspective in NeoDefaults has a larger field of view. This means that more of the
area can be seen (e.g., the +/- on the left, the windows at the top, etc.).

NeoDefaults also improves damage numbers. Ordinarily, this critical information is tiny and hard to
read:
![Damage Numbers](
https://raw.githubusercontent.com/kqarryzada/TF2-NeoDefaults/master/resource/readme-images/pyro_damagenumbers.png)
Notice that NeoDefaults enables `hud_combattext_batching` in the image on the right, so the
individual damage numbers are automatically summed together.

## Frequently Asked Questions
The FAQ is available [here](docs/FAQ.md).

## More Information
In the interest of keeping this page short, additional information is available on the
[More Info](docs/More-Info.md) page, which discusses:
* An explanation of what each component is and why it's added
* How to customize what NeoDefaults changes
* How to diagnose and report issues

## Special Thanks To...
* Eniere for their work on the [Improved Default HUD](https://huds.tf/forum/showthread.php?tid=276),
which inspired this project to improve the appearance of damage numbers.
* The mastercomfig team for their thorough [documentation](https://docs.mastercomfig.com/en/latest/).
* [b4nny](https://www.twitch.tv/b4nny) for sharing his hitsound and the associated pitch settings.

## License
The conditions of using this software may be viewed in the [LICENSE](LICENSE) file.

Valve, the Valve logo, Steam, the Steam logo, Team Fortress, and the Team Fortress logo are
trademarks and/or registered trademarks of Valve Corporation. The NeoDefaults project is not
affiliated with or endorsed by Valve Corporation.
