#!/bin/bash

VERSION="$(git describe --abbrev=0 --tags | tr -d v)"
echo $VERSION > VERSION
