call "c:\Program Files\Microsoft Visual Studio 9.0\VC\vcvarsall.bat" x86
%FrameworkDir%\%Framework35Version%\msbuild.exe /target:Test /property:Configuration=Debug "Abstract Air.build"
