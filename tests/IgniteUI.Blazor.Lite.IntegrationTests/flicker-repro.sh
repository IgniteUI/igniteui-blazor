#!/usr/bin/env bash
# Hunt for flickers in the Bulk API integration tests by running them under CPU pressure.
#
# Flickers here tend to come from timing: the tests drive a Blazor server component
# through a browser, so a check can outrun whatever it is waiting on. A developer box
# with cores to spare hides that, and a handful of green runs on one says very little.
# Confining the run to a single CPU makes the server, the browser and everything they
# queue compete, which is usually enough to surface an ordering assumption.
#
#   ./flicker-repro.sh                    # IgbCalendar, 10 runs, 1 core
#   ./flicker-repro.sh -c IgbTile -n 20   # another component, more runs
#   ./flicker-repro.sh -c '' -n 3         # whole suite (~50s per run)
#   ./flicker-repro.sh -p 0,1             # two cores, for a milder squeeze
#   ./flicker-repro.sh -s                 # skip the build, if you just built
#
# A run counts as failed on any test failure. The patterns below only decide which lines
# get echoed for context, so add to them when chasing a failure they do not cover.
#
# Read the result as a rate, not a verdict: this raises the odds of a race losing, it
# does not make one certain, so a clean pass is evidence and not proof. Exits non-zero
# if any run failed, so it can gate a bisect.
#
# For illustration, the #249 flickers (property values and events read before the
# wrapper had flushed them to the client) came up in roughly a third of single-CPU runs
# and in none unpinned. DOTNET_PROCESSOR_COUNT=1 did not reproduce them either - it
# resizes the thread pool without making anything else compete for the core.
set -o pipefail

COMPONENT=IgbCalendar
RUNS=10
CPUS=0
BUILD=1

while getopts "c:n:p:sh" opt; do
  case $opt in
    c) COMPONENT=$OPTARG ;;
    n) RUNS=$OPTARG ;;
    p) CPUS=$OPTARG ;;
    s) BUILD=0 ;;
    h) sed -n '2,25p' "$0"; exit 0 ;;
    *) exit 2 ;;
  esac
done

cd "$(dirname "$0")/../.." || exit 1
PROJECT=./tests/IgniteUI.Blazor.Lite.IntegrationTests

if [ "$BUILD" = 1 ]; then
  # Rebuild by default: the runs below pass --no-build for speed, so against a stale
  # binary you would be testing the branch you came from and calling it clean.
  echo "building..."
  BUILD_LOG=$(mktemp)
  if ! dotnet build "$PROJECT" -v q --nologo > "$BUILD_LOG" 2>&1; then
    grep -E ' error |error CS' "$BUILD_LOG" | head -20
    echo "build failed, see $BUILD_LOG"
    exit 1
  fi
  rm -f "$BUILD_LOG"
fi

FILTER=()
[ -n "$COMPONENT" ] && FILTER=(--filter "FullyQualifiedName~$COMPONENT")

RUNNER=()
if [ -n "$CPUS" ]; then
  if command -v taskset >/dev/null; then
    RUNNER=(taskset -c "$CPUS")
  else
    echo "warning: no taskset, running unpinned - the flickers likely will not reproduce"
  fi
fi

echo "${COMPONENT:-all components} x$RUNS on cpu(s) ${CPUS:-all}"

fails=0
for i in $(seq 1 "$RUNS"); do
  out=$("${RUNNER[@]}" dotnet test "$PROJECT" --settings ./.runsettings --no-build "${FILTER[@]}" 2>&1)
  if echo "$out" | grep -q 'Failed!'; then
    fails=$((fails + 1))
    echo "run $i: FAIL"
    echo "$out" | grep -E 'mismatch after setting|did not fire|Timeout [0-9]+ms|Exception :' \
      | sed 's/^ */  /' | sort -u | head -5
  else
    printf '.'
  fi
done

echo
echo "$fails/$RUNS runs failed"
[ "$fails" = 0 ]
