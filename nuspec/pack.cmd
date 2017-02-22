@echo off
del *.nupkg
nuget pack Plugin.IO.SerialPort.nuspec
pause