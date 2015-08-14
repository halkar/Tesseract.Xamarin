#!/bin/bash

git submodule init

git submodule update --recursive

mono --runtime=v4.0 nuget/NuGet.exe restore Tesseract.Xamarin.sln

xbuild /p:Configuration=Release Tesseract.Xamarin.sln

mono --runtime=v4.0 nuget/NuGet.exe pack Xamarin.Tesseract.nuspec
