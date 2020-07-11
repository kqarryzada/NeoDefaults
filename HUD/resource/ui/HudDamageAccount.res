"Resource/UI/HudDamageAccount.res"
{
    // Damage numbers (AKA combat text). This is displayed above the player
    // taking damage.
    "CDamageAccountPanel"
    {
        "fieldName"             "CDamageAccountPanel"
        "text_x"                "0"
        "text_y"                "0"
        "delta_item_end_y"      "0"
        "PositiveColor"         "0 255 0 255"
        "NegativeColor"         "255 255 0 255"
        "delta_lifetime"        "1.5"
        "delta_item_font"       "HudFontMedium"
        "delta_item_font_big"   "HudFontMediumBold"
    }

    // Most recent damage value. This is displayed over the health bar.
    "CDamageAccountValue"
    {
        "ControlName"           "CExLabel"
        "fieldName"             "CDamageAccountValue"
        "fgcolor"               "255 255 80 255"
        "font"                  "HudFontBiggerBold"
        "xpos"                  "128"
        "ypos"                  "r110"
        "xpos_minmode"          "110"
        "ypos_minmode"          "r75"
        "zpos"                  "2"
        "wide"                  "100"
        "tall"                  "26"
        "labelText"             "%metal%"
        "textAlignment"         "left"

        // Set as disabled, since most new players probably won't care for this
        // feature.
        "enabled"               "0"
        "visible"               "0"
    }
}
