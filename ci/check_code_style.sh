#!/usr/bin/env bash

set -e
set -x

echo "Generating .sln files"

${UNITY_EXECUTABLE:-xvfb-run --auto-servernum --server-args='-screen 0 640x480x24' unity-editor} \
  -projectPath $UNITY_DIR \
  -quit \
  -batchmode \
  -nographics \
  -executeMethod Packages.Rider.Editor.RiderScriptEditor.SyncSolution \
  -logFile /dev/stdout

UNITY_EXIT_CODE=$?

if [ $UNITY_EXIT_CODE -eq 0 ]; then
  echo "Run succeeded, no failures occurred";
else
  echo "Unexpected exit code $UNITY_EXIT_CODE";
  exit $UNITY_EXIT_CODE
fi

ls -la .

SOLUTION_FILE=$(find $UNITY_DIR -maxdepth 1 -type f -name "*.sln" -print -quit)

dotnet msbuild $SOLUTION_FILE -t:Rebuild > stylecop_log_raw.txt

# If the file is suspiciously small, run it again
# I have no idea why this is necessary, but it is
if [ $(wc -l < stylecop_log_raw.txt) -lt 10 ]; then
  echo "StyleCop log suspiciously small, running again"
  dotnet msbuild $SOLUTION_FILE -t:Rebuild > stylecop_log_raw.txt

  # If the file is still suspiciously small, fail the build
  if [ $(wc -l < stylecop_log_raw.txt) -lt 10 ]; then
    echo "StyleCop log suspiciously small, failing build"
    exit 1
  fi
fi

grep -f style_check_grep.txt stylecop_log_raw.txt | sed "s,$(pwd),.,g" > stylecop_log.txt
cat stylecop_log.txt

LINES=$(wc -l < stylecop_log.txt)

if [ $LINES -gt 0 ]; then
  echo "StyleCop found $LINES violations"
  exit 1
else
  echo "StyleCop found no violations"
  exit 0
fi