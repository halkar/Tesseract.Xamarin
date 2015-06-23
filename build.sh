#!/bin/bash

xbuild /p:Configuration=Release Tesseract.Xamarin.sln

mono --runtime=v4.0 nuget/NuGet.exe pack Xamarin.Tesseract.nuspec
