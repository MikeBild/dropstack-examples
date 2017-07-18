#!/bin/sh
cd hello-service
nuget restore
msbuild /t:"Clean;Rebuild" /p:"Configuration=Release;OutputPath=../../bin" /p:Platform="x86"