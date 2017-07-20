#!/bin/sh
dropstack deploy -v build=./ds-build.sh -v test=./ds-test.sh -v run=./ds-run.sh --verbose