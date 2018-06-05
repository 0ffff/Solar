Attribute VB_Name = "DAQMXItems"
' DAQMXItems.bas
' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
'
' Copyright (c) 2004-2007 Yokogawa Electric Corporation. All rights reserved.
'
' This is defined item number in configure.
'
' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
' 2007/05/30 Ver.3 Rev.0
' 2004/11/01 Ver.2 Rev.1
' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --

' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
' item
' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
Public Const DAQMX_ITEM_NONE              = -1     ' 0xFFFFFFFF
' MXNetInfo
Public Const DAQMX_ITEM_NETHOST           = &H0000 ' 00000000 00000000
Public Const DAQMX_ITEM_NETADDRESS        = &H0001
Public Const DAQMX_ITEM_NETPORT           = &H0002
Public Const DAQMX_ITEM_NETSUBMASK        = &H0003
Public Const DAQMX_ITEM_NETGATEWAY        = &H0004
' MXUnitData
Public Const DAQMX_ITEM_UNITTYPE          = &H0005 ' 00000000 00000101
Public Const DAQMX_ITEM_UNITSTYLE         = &H0006
Public Const DAQMX_ITEM_UNITNO            = &H0007
Public Const DAQMX_ITEM_UNITTEMP          = &H0008
Public Const DAQMX_ITEM_UNITCFTIMEOUT     = &H0009
Public Const DAQMX_ITEM_UNITCFWRITEMODE   = &H000A
Public Const DAQMX_ITEM_UNITFREQUENCY     = &H000B
Public Const DAQMX_ITEM_UNITPARTNO        = &H000C
Public Const DAQMX_ITEM_UNITOPTION        = &H000D
Public Const DAQMX_ITEM_UNITSERIAL        = &H000E
Public Const DAQMX_ITEM_UNITMAC           = &H000F ' 00000000 00001111
' MXStatus
Public Const DAQMX_ITEM_UNITSTATUS        = &H0010 ' 00000000 00010000
Public Const DAQMX_ITEM_CNTCONFIG         = &H0011
Public Const DAQMX_ITEM_CNTTIME           = &H0012
Public Const DAQMX_ITEM_FIFONUM           = &H0013
Public Const DAQMX_ITEM_BACKUP            = &H0014
' MXCFInfo
Public Const DAQMX_ITEM_CFSTATUS          = &H0015 ' 00000000 00010101
Public Const DAQMX_ITEM_CFSIZE            = &H0016
Public Const DAQMX_ITEM_CFREMAIN          = &H0017
' MXFIFOInfo by FIFO
Public Const DAQMX_ITEM_FIFOSTATUS        = &H0018 ' 00000000 000110xx
Public Const DAQMX_ITEM_FIFOINTERVAL      = &H001C ' 00000000 000111xx
Public Const DAQMX_ITEM_FIFOOLDNO         = &H0020 ' 00000000 001000xx
Public Const DAQMX_ITEM_FIFONEWNO         = &H0024 ' 00000000 001001xx
' MXModuleData by Module
Public Const DAQMX_ITEM_MODULETYPE        = &H0028 ' 00000000 00101xxx
Public Const DAQMX_ITEM_MODULECHNUM       = &H0030 ' 00000000 00110xxx
Public Const DAQMX_ITEM_MODULEINTERVAL    = &H0038 ' 00000000 00111xxx
Public Const DAQMX_ITEM_MODULEINTEGRAL    = &H0040 ' 00000000 01000xxx
Public Const DAQMX_ITEM_MODULESTANDBY     = &H0048 ' 00000000 01001xxx
Public Const DAQMX_ITEM_MODULEREALTYPE    = &H0050 ' 00000000 01010xxx
Public Const DAQMX_ITEM_MODULESTATUS      = &H0058 ' 00000000 01011xxx
Public Const DAQMX_ITEM_MODULEVERSION     = &H0060 ' 00000000 01100xxx
Public Const DAQMX_ITEM_MODULETERMINAL    = &H0068 ' 00000000 01101xxx
Public Const DAQMX_ITEM_MODULEFIFONO      = &H0070 ' 00000000 01110xxx
Public Const DAQMX_ITEM_MODULESERIAL      = &H0078 ' 00000000 01111xxx
' MXChID by Channel
Public Const DAQMX_ITEM_CHVALID           = &H0080 ' 00000000 10xxxxxx
Public Const DAQMX_ITEM_CHPOINT           = &H00C0 ' 00000000 11xxxxxx
Public Const DAQMX_ITEM_CHKIND            = &H0100 ' 00000001 00xxxxxx
Public Const DAQMX_ITEM_CHRANGE           = &H0140 ' 00000001 01xxxxxx
Public Const DAQMX_ITEM_CHSCALE           = &H0180 ' 00000001 10xxxxxx
Public Const DAQMX_ITEM_CHUNIT            = &H01C0 ' 00000001 11xxxxxx
Public Const DAQMX_ITEM_CHTAG             = &H0200 ' 00000010 00xxxxxx
Public Const DAQMX_ITEM_CHCOMMENT         = &H0240 ' 00000010 01xxxxxx
' MXAlarm by Channel
Public Const DAQMX_ITEM_ALARMTYPE         = &H0280 ' 00000010 1yxxxxxx
Public Const DAQMX_ITEM_ALARMON           = &H0300 ' 00000011 0yxxxxxx
Public Const DAQMX_ITEM_ALARMOFF          = &H0380 ' 00000011 1yxxxxxx
' MXChConfigAIDI by Channel
Public Const DAQMX_ITEM_CHSPANMIN         = &H0400 ' 00000100 00xxxxxx
Public Const DAQMX_ITEM_CHSPANMAX         = &H0440 ' 00000100 01xxxxxx
Public Const DAQMX_ITEM_CHSCALEMIN        = &H0480 ' 00000100 10xxxxxx
Public Const DAQMX_ITEM_CHSCALEMAX        = &H04C0 ' 00000100 11xxxxxx
Public Const DAQMX_ITEM_CHREFCHNO         = &H0500 ' 00000101 00xxxxxx
' MXChConfigAI by Channel
Public Const DAQMX_ITEM_CHFILTER          = &H0540 ' 00000101 01xxxxxx
Public Const DAQMX_ITEM_CHRJCTYPE         = &H0580 ' 00000101 10xxxxxx
Public Const DAQMX_ITEM_CHRJCVOLT         = &H05C0 ' 00000101 11xxxxxx
Public Const DAQMX_ITEM_CHBURNOUT         = &H0600 ' 00000110 00xxxxxx
' MXChConfigDO by Channel
Public Const DAQMX_ITEM_CHDEENERGIZE      = &H0640 ' 00000110 01xxxxxx
Public Const DAQMX_ITEM_CHREFALARM        = &H0680 ' 00000110 1yxxxxxx
Public Const DAQMX_ITEM_CHHOLD            = &H0700 ' 00000111 00xxxxxx
' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
' since R2.01
' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
' MXChConfigAOPWM by Channel
Public Const DAQMX_ITEM_CHOUTPUTTYPE      = &H0740 ' 00000111 01xxxxxx
Public Const DAQMX_ITEM_CHIDLECHOICE      = &H0780 ' 00000111 10xxxxxx
Public Const DAQMX_ITEM_CHERRCHOICE       = &H07C0 ' 00000111 11xxxxxx
Public Const DAQMX_ITEM_CHPRESETVALUE     = &H0800 ' 00001000 00xxxxxx
Public Const DAQMX_ITEM_CHPULSETIME       = &H0840 ' 00001000 01xxxxxx
' MXBalance by Channel
Public Const DAQMX_ITEM_CHBALANCEVALID    = &H0880 ' 00001000 10xxxxxx
Public Const DAQMX_ITEM_CHBALANCEVALUE    = &H08C0 ' 00001000 11xxxxxx
' MXStatus
Public Const DAQMX_ITEM_STATUSTIME        = &H0900 ' 00001001 00000000
Public Const DAQMX_ITEM_STATUSMSEC        = &H0901
' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
' since R3.01
' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
' MXChConfigPI by channel
Public Const DAQMX_ITEM_CHCHATFILTER      = &H0940 ' 00001001 01xxxxxx
' Alarm : for No. 3,4
Public Const DAQMX_ITEM_ALARMTYPE2        = &H0980 ' 00001001 0yxxxxxx
Public Const DAQMX_ITEM_ALARMON2          = &H0A00 ' 00001010 0yxxxxxx
Public Const DAQMX_ITEM_ALARMOFF2         = &H0A80 ' 00001010 1yxxxxxx
Public Const DAQMX_ITEM_CHREFALARM2       = &H0B00 ' 00001011 0yxxxxxx
' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
' All and Mask
' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
' max
Public Const DAQMX_MAX_INDEX_FIFO         = 2
Public Const DAQMX_MAX_INDEX_MODULE       = 5
Public Const DAQMX_MAX_INDEX_CHANNEL      = 59
' All
Public Const DAQMX_ITEM_ALL_START         = 0
Public Const DAQMX_ITEM_ALL_END_R1        = DAQMX_ITEM_CHHOLD + DAQMX_MAX_INDEX_CHANNEL ' R1.01
Public Const DAQMX_ITEM_ALL_END_R2        = DAQMX_ITEM_STATUSMSEC ' R2.01
Public Const DAQMX_ITEM_ALL_END_R3        = DAQMX_ITEM_CHREFALARM2 + &H0040 + DAQMX_MAX_INDEX_CHANNEL ' R3.01
Public Const DAQMX_ITEM_ALL_END           = DAQMX_ITEM_ALL_END_R3 ' newest
' mask
Public Const DAQMX_MASK_BYMODULE          = &H0007
Public Const DAQMX_MASK_BYCHANNEL         = &H003F
Public Const DAQMX_MASK_BYFIFO            = &H0003
Public Const DAQMX_MASK_BYALARM           = &H0040 ' NOTE: position
' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
