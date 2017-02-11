@echo off
del *.nupkg
nuget pack Plugin.IO.SerialPort.nuspec
nuget pack Plugin.IO.SerialPort.Rx.nuspec
pause