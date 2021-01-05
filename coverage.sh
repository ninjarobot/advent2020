#!/bin/bash
set -eu

rm -r coveragereport || true;
pushd FsAdvent2020Tests;
rm -r TestResults || true;
dotnet test --collect:"XPlat Code Coverage";
popd;
dotnet reportgenerator -reports:FsAdvent2020Tests/TestResults/*/coverage.cobertura.xml -targetdir:coveragereport -reporttypes:html;
open coveragereport/index.html;
