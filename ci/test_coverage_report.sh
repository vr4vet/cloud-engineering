#!/usr/bin/env bash

set -x

echo "Generating coverage report"

CODE_COVERAGE_PACKAGE="com.unity.testtools.codecoverage"
PACKAGE_MANIFEST_PATH="Packages/manifest.json"

if ! grep $CODE_COVERAGE_PACKAGE $PACKAGE_MANIFEST_PATH; then
  {
    echo -e "\033[33mCode Coverage package not found in $PACKAGE_MANIFEST_PATH. Please install the package \"Code Coverage\" through Unity's Package Manager to enable coverage reports.\033[0m"
  } 2> /dev/null
  exit 1
fi

${UNITY_EXECUTABLE:-xvfb-run --auto-servernum --server-args='-screen 0 640x480x24' unity-editor} \
  -projectPath $UNITY_DIR \
  -logFile /dev/stdout \
  -batchmode \
  -nographics \
  -enableCodeCoverage \
  -coverageResultsPath $UNITY_DIR/CodeCoverage \
  -coverageOptions "generateHtmlReport;generateBadgeReport;" \
  -debugCodeOptimization \
  -quit

UNITY_EXIT_CODE=$?

if [ $UNITY_EXIT_CODE -eq 0 ]; then
  echo "Run succeeded, no failures occurred";
else
  echo "Unexpected exit code $UNITY_EXIT_CODE";
fi

if [ ! -f "$UNITY_DIR/CodeCoverage/Report/Summary.xml" ]; then
  echo "ERROR: $UNITY_DIR/CodeCoverage/Report/Summary.xml file not found."
  exit 1
fi

awk -F'[<>]' '/<Coveredlines>/{a=$3}/<Coverablelines>/{b=$3}END{printf "Line coverage: %.2f%%\n", a/b*100}' $UNITY_DIR/CodeCoverage/Report/Summary.xml

exit $UNITY_EXIT_CODE
