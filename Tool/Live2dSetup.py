# -*- coding: utf-8 -*-

import sys
import os
import shutil

# 引数1 : Live2DのSDKフォルダーパス
# 引数2 : Platform
# 引数3 : vsVersion
# 引数4 : Live2DForDLLの依存フォルダーのパス

print( "check" )

sdkPath = sys.argv[ 1 ]
platform = sys.argv[ 2 ]
vsVersion = sys.argv[ 3 ]
outputPath = sys.argv[ 4 ]

Live2DFolder = "Live2D_SDK_OpenGL"
include = "include"
lib = "lib"

print( "path check" )
print( "sdkPath = %s" ) % ( sdkPath )
print( "platform = %s" ) % ( platform )
print( "vsVersion = %s" ) % ( vsVersion )
print( "outputPath = %s" ) % ( outputPath )

# 一気にフォルダー作れないので
try:
    os.mkdir(outputPath)
    os.mkdir(outputPath + "\\" + Live2DFolder)
    os.mkdir(outputPath + "\\" + Live2DFolder + "\\" + include)
    os.mkdir(outputPath + "\\" + Live2DFolder + "\\" + lib)
except OSError:
    pass
    
# コピー開始
# includeコピー
shutil.copytree(sdkPath + "\\" + include, outputPath + "\\" + Live2DFolder + "\\" + include)
# libコピー
shutil.copytree(sdkPath + "\\" + lib + "\\" + "windows" + "\\" + platform + "\\" + vsVersion, outputPath + "\\" + Live2DFolder + "\\" + lib)

