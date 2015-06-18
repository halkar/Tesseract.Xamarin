using System;
using ObjCRuntime;

[assembly: LinkWith ("libTesseract.a", LinkTarget.ArmV7 | LinkTarget.Simulator | LinkTarget.Simulator64 | LinkTarget.Arm64, SmartLink = true, ForceLoad = true, IsCxx = true, Frameworks = "CoreFoundation CoreImage", LinkerFlags = "-ObjC -lstdc++")]

