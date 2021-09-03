#!/bin/sh
csc -target:library -out:NEAT.dll NEAT/*.cs
csc -reference:NEAT.dll -out:Program.exe SNAKE/*.cs
