using System;
using ObjCRuntime;

[assembly: LinkWith ("TesseractOCR.a", LinkTarget.Simulator | LinkTarget.ArmV7, ForceLoad = true)]
