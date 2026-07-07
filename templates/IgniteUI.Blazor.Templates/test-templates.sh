#!/usr/bin/env bash
#
# test-templates.sh
#
# Smoke-tests the IgniteUI.Blazor.Templates package by:
#   1. Installing the templates from this source folder.
#   2. Scaffolding a fresh Blazor *Server* project under ./tmp via `dotnet new igb-blazor`.
#   3. Generating every item template (igb-*) into the project's Components/Pages folder.
#   4. Wiring each generated page into Components/Layout/NavMenu.razor as an IgbNavDrawerItem.
#
# Usage:  ./test-templates.sh
#
# Env overrides:
#   PROJECT_NAME   name of the generated project        (default: IgbTemplateTest)
#   NAV_ICON       material-icons name used for nav items (default: widgets)
#   SKIP_RESTORE   1 to skip NuGet restore on create     (default: 1)

set -euo pipefail

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
TMP_DIR="$SCRIPT_DIR/tmp"
ITEMS_DIR="$SCRIPT_DIR/templates/item"

PROJECT_NAME="${PROJECT_NAME:-IgbTemplateTest}"
NAV_ICON="${NAV_ICON:-widgets}"
SKIP_RESTORE="${SKIP_RESTORE:-1}"

echo "==> Cleaning $TMP_DIR"
rm -rf "$TMP_DIR"
mkdir -p "$TMP_DIR"

echo "==> Installing templates from $SCRIPT_DIR"
dotnet new install "$SCRIPT_DIR" --force

echo "==> Creating Blazor Server project '$PROJECT_NAME'"
CREATE_ARGS=(igb-blazor -n "$PROJECT_NAME" -o "$TMP_DIR/$PROJECT_NAME" --Hosting Server)
if [[ "$SKIP_RESTORE" == "1" ]]; then
    CREATE_ARGS+=(--SkipRestore)
fi
dotnet new "${CREATE_ARGS[@]}"

# Locate the generated app folder via its NavMenu (robust to the project sourceName rename).
NAVMENU="$(find "$TMP_DIR/$PROJECT_NAME" -path '*/Components/Layout/NavMenu.razor' | head -n1)"
if [[ -z "$NAVMENU" ]]; then
    echo "ERROR: NavMenu.razor not found in generated project." >&2
    exit 1
fi
APP_DIR="$(cd "$(dirname "$NAVMENU")/../.." && pwd)"   # .../Components/Layout -> app dir
PAGES_DIR="$APP_DIR/Components/Pages"
echo "==> App dir:   $APP_DIR"
echo "==> Pages dir: $PAGES_DIR"
echo "==> NavMenu:   $NAVMENU"

# Generate every item template into Components/Pages and collect a nav snippet for each.
NAV_SNIPPET=""
echo "==> Generating item templates"
for d in "$ITEMS_DIR"/*/; do
    tj="$d/.template.config/template.json"
    [[ -f "$tj" ]] || continue
    short=$(grep -oP '"shortName"\s*:\s*"\K[^"]+' "$tj")
    source=$(grep -oP '"sourceName"\s*:\s*"\K[^"]+' "$tj")
    echo "    - $source  (dotnet new $short)"
    dotnet new "$short" -n "$source" -o "$PAGES_DIR"

    NAV_SNIPPET+="<IgbNavDrawerItem Active=\"@IsActive(\"$source\")\" @onclick=@(() => Nav.NavigateTo(\"$source\"))>"$'\n'
    NAV_SNIPPET+="    <span slot=\"icon\">"$'\n'
    NAV_SNIPPET+="        <span class=\"material-icons icon\">"$'\n'
    NAV_SNIPPET+="            $NAV_ICON"$'\n'
    NAV_SNIPPET+="        </span>"$'\n'
    NAV_SNIPPET+="    </span>"$'\n'
    NAV_SNIPPET+="    <div slot=\"content\">$source</div>"$'\n'
    NAV_SNIPPET+="</IgbNavDrawerItem>"$'\n'
done

# Insert the collected nav items right before the @code block (i.e. after the existing nav items).
echo "==> Updating NavMenu.razor"
awk -v snip="$NAV_SNIPPET" '
    /^@code/ && !done { printf "%s", snip; done=1 }
    { print }
' "$NAVMENU" > "$NAVMENU.new"
mv "$NAVMENU.new" "$NAVMENU"

echo "==> Done."
echo "    Project: $TMP_DIR/$PROJECT_NAME"
echo "    To run:  dotnet run --project \"$APP_DIR\""
