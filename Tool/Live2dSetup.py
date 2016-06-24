# -*- coding: utf-8 -*-

import sys
import os
import shutil

# 引数1 : Live2DのSDKフォルダーパス
# 引数2 : Platform
# 引数3 : vsVersion
# 引数4 : Live2DForDLLの依存フォルダーのパス

sdkPath = sys.argv[ 1 ]
platform = sys.argv[ 2 ]
vsVersion = sys.argv[ 3 ]
outputPath = sys.argv[ 4 ]

# SDKPathが空または存在しない場合はエラーとする
try:
    if sdkPath == "none":
        raise Exception("[Error] File path is empty.")
    if os.path.isdir(sdkPath) == False:
        raise Exception("[Error] File does not exist.")
except Exception as e:
    sys.exit( e.message )

Live2DFolder = "Live2D_SDK_OpenGL"
include = "include"
lib = "lib"

# パス確認
print( "path check" )
print( "sdkPath = %s" ) % ( sdkPath )
print( "platform = %s" ) % ( platform )
print( "vsVersion = %s" ) % ( vsVersion )
print( "outputPath = %s" ) % ( outputPath )

# すでにフォルダーがある場合は一回削除してからコピー開始
if os.path.isdir(outputPath + "\\" + Live2DFolder):
    t = shutil.rmtree(outputPath + "\\" + Live2DFolder)

# コピー開始
# includeコピー
shutil.copytree(sdkPath + "\\" + include, outputPath + "\\" + Live2DFolder + "\\" + include)
# libコピー
shutil.copytree(sdkPath + "\\" + lib + "\\" + "windows" + "\\" + platform + "\\" + vsVersion, outputPath + "\\" + Live2DFolder + "\\" + lib)

print( "\nLive2D SDK Setup Complete!!" )
