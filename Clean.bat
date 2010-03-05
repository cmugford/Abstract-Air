call "c:\Program Files (x86)\Microsoft Visual Studio 9.0\VC\vcvarsall.bat" x86
%FrameworkDir%\%Framework35Version%\msbuild.exe /target:Clean /property:Configuration=Debug "Abstract Air.build"
