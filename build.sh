#!/usr/bin/env bash
HELP_ARG="${1}"

VERSION_NUM="v1.0.0-SNAPSHOT"

HELP_TEXT="
This shell script takes source components (e.g., the HUD) in the components/ directory and packages
them into a distributable format (e.g., .vpk files). The output files are located in build/. To run,
enter the name of the script (no arguments are necessary):
\$ ${0}\n

The 'TF2_PATH' environment variable must be defined and point to a TF2 installation. You can fix
this temporarily by running the following in your terminal before launching the shell script:
\$ export TF2_PATH='<path-to-common-folder>/Team\ Fortress\ 2'

This file was written for use on the 'Ubuntu for Windows' shell. It may not be well-behaved on other
types of systems.\n"


COMPONENTS_DIR="components"

# The location where the source cfg files are stored.
SRC_CFG_DIR="${COMPONENTS_DIR}/cfg"

HUD_OUTPUT_FILE="build/neodefaults-hud-tweaks.vpk"
HIT_OUTPUT_DIR="build/neodefaults-quake-hitsound"

# The location of the main output cfg file.
NEODEFAULTS_CFG_NAME="build/NeoDefaults-${VERSION_NUM}.cfg"

# The header that is inserted at the top of the output cfg file.
CFG_HEADER=\
"// NeoDefaults config ${VERSION_NUM}
//"

# The footer that is inserted at the end of the output cfg file. This value is
# set in the set_footer() function.
CFG_FOOTER=""


# Prints the provided text in red to STDERR.
function print_error() {
    >&2 printf "\033[0;31m"
    >&2 printf "${1}\n"
    >&2 printf "\033[0m"
}


# Prints the provided text in green to STDOUT.
function print_green() {
    printf "\033[0;32m"
    printf "${1}\n"
    printf "\033[0m"
}


function check_helptext() {
    if [[ -z ${HELP_ARG} ]]; then
        return
    fi

    if [[ ${HELP_ARG} == "--help" || ${HELP_ARG} == "-?" || ${HELP_ARG} == "help" ]]; then
        printf "${HELP_TEXT}"
        retval=0
    else
        >&2 echo "Invalid input detected: '${HELP_ARG}'"
        retval=1
    fi

    exit "${retval}"
}


#
# This function ensures that script dependencies are all installed and
# available.
#
function init() {
    # Environment variable for TF2 path
    if [ -z "${TF2_PATH}" ]; then
        >&2 echo "The 'TF2_PATH' environment variable is not defined. See the help text for more info."
        exit 1

    # If the path doesn't end in a slash, append one.
    elif [ "${TF2_PATH: -1}" != "/" ]; then
        TF2_PATH="${TF2_PATH}/"
    fi

    VPK="${TF2_PATH}bin/vpk.exe"
    if ! check_vpk; then
        >&2 echo "Tried to run the vpk.exe file at: '${VPK}', but failed. Check your path."
        exit 1
    fi

    # Check for 'unix2dos' binary
    if ! command -v unix2dos > /dev/null; then
        >&2 echo "'unix2dos' failed to run. Make sure this is installed."
        exit 1
    fi
}


# Checks that the VPK variable has been correctly set up.
function check_vpk() {
    eval "${VPK}" "-?" &> /dev/null || return 1
}


#
# Bundles a folder into a .vpk file. VPK files are are used by Source engine
# games to store game assets. More information is available here:
# https://developer.valvesoftware.com/wiki/VPK_File_Format
#
function package_vpk() {
    if [ "$#" -eq 1 ]; then
        print_green "Packaging ${1}..."
        eval "${VPK}" "${1}" || return 1
        echo ""
    else
        print_error "package_vpk() received $# arguments, but was only expecting one."
        exit 1
    fi
}


#
# Sets the value of 'CFG_FOOTER', which corresponds to the footer string in the
# core Neodefaults-[VERSION].cfg file. This will eventually print a message in
# the TF2 console that looks like:
#
# -------------------------------------------------------------------------
# ----------------------- NEODEFAULTS v1.0.0 LOADED -----------------------
# -------------------------------------------------------------------------
#
function set_footer() {
    # A string of dashes that will be printed in the TF2 console.
    local divider="-------------------------------------------------------------------------"
    local version_text="NEODEFAULTS ${VERSION_NUM} LOADED"

    # We want the NeoDefaults version to be displayed on a line in the middle of
    # a divider, with appropriate spacing. This can vary based on the version
    # number, e.g., '1.0.0' is a different length than '2.14.11'. Dynamically
    # determine how many dashes are needed.
    local max_chars=${#divider}
    local num_chars_in_version=${#version_text}
    local num_total_dashes=$((max_chars - num_chars_in_version))

    # The longest allowed case would be a header like:
    # '-- NEODEFAULTS VeryLongVersionNumber LOADED --'. Thus, there has to be
    # room for at least 3 additional characters on each side, including the
    # space character.
    if [[ ${num_total_dashes} -lt 6 ]]; then
        print_error "Invalid version number: ${VERSION_NUM}"
        print_error "The full version string would be longer than the footer: '${version_text}'."
        exit 1
    fi


    # Get the number of dashes that should be printed on each side. Subtract the
    # two additional spaces.
    local num_side_dashes=$(( (num_total_dashes - 2) / 2))
    local side_dashes="${divider:0:num_side_dashes}"

    # Assemble the resulting output version, surrounded by dash characters.
    local output_version="${side_dashes} ${version_text} ${side_dashes}"

    # If 'num_total_dashes' is odd, then we will need an additional dash at the
    # end (due to integer division).
    if [[ $((${num_total_dashes} % 2)) == 1 ]]; then
        output_version="${output_version}-"
    fi


    # Now that 'output_version' has been assembled, prepare the footer. Note
    # that the raw "echo" lines are printed in NeoDefaults-[VERSION].cfg, since
    # echo is also a Source Engine command. These echo statements will not be
    # executed by this script or any bash script.
    local footer="\n\n"
    footer+="echo \"\"\n"
    footer+="echo \"\"\n"
    footer+="echo \"${divider}\"\n"
    footer+="echo \"${output_version}\"\n"
    footer+="echo \"${divider}\"\n"
    footer+="echo \"\"\n"

    CFG_FOOTER="${footer}"
}


function clean() {
    # If a previous run ended abruptly, it could leave dirty references in components/.
    rm ${COMPONENTS_DIR}/*.vpk &> /dev/null

    rm -rf ./build
    mkdir build
}


function build() {
    err=""

    printf "Building ${VERSION_NUM} components...\n"

    # Build HUD
    {
        package_vpk "${COMPONENTS_DIR}/HUD" && \
        mv ${COMPONENTS_DIR}/hud.vpk "${HUD_OUTPUT_FILE}"
    } || {
        print_error "Failed to build the HUD VPK."
        err="True"
    }


    # Build hitsound
    {
        # Ordinarily, the hitsound file must be placed under <folder name>/sound/ui/hitsound.wav.
        # Create this file structure, then package the parent folder as the VPK.
        src_file="${COMPONENTS_DIR}/hitsound.wav"
        folder="build/neodefaults-quake-hitsound/sound/ui"

        mkdir -p "${folder}" && \
        cp "${src_file}" "${folder}" && \
        package_vpk "${HIT_OUTPUT_DIR}"
    } || {
        print_error "Failed to build the hitsound VPK."
        err="True"
    }

    # Clean up extra files from hitsound
    warn_msg="Warning: Failed to delete the generated folder '${HIT_OUTPUT_DIR}'. This can be"
    warn_msg="$warn_msg deleted manually."
    rm -r "${HIT_OUTPUT_DIR}" || echo "${warn_msg}"


    # Build cfg
    {
        print_green "Building cfg files..."

        # Copy all existing cfg files to the build directory
        cp ${SRC_CFG_DIR}/*.cfg build && \

        # Create the main output file and add the header
        echo "${CFG_HEADER}" > "${NEODEFAULTS_CFG_NAME}" && \

        # Append the contents of the source to the output file
        cat "${SRC_CFG_DIR}/main-cfg.txt" >> "${NEODEFAULTS_CFG_NAME}" && \

        # Add the footer at the end of the output file. Use printf to expand the
        # newline characters.
        printf "${CFG_FOOTER}" >> "${NEODEFAULTS_CFG_NAME}" && \

        # Ensure all cfg files have Windows-style line endings
        for i in build/*.cfg; do
            unix2dos "$i"
        done && \

        # Make the output file read-only
        chmod 444 "${NEODEFAULTS_CFG_NAME}"
    } || {
        print_error "Failed to build the cfg files."
        err="True"
    }


    # If any errors occurred (i.e., $err is not empty), return error code 1.
    [ -z "${err}" ] || return 1
}


function main() {
    # Change the current directory to the script location before beginning execution. This will allow
    # the script to run correctly without having to be launched from the root folder.
    cd $(dirname "$0")

    clean
    build
    return_code="$?"
    if [ "${return_code}" -eq 0 ]; then
        echo "Build complete."
    else
        >&2 echo "Error: A failure occurred during the build process."
    fi
    exit "${return_code}"
}


check_helptext
init
set_footer
main
