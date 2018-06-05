Imports System.Runtime.InteropServices

' DAQMXItems.vb
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

Module DAQMXItems
    
    ' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
    ' item
    ' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
    Public Const DAQMX_ITEM_NONE              As Integer = -1     ' 0xFFFFFFFF
    ' MXNetInfo
    Public Const DAQMX_ITEM_NETHOST           As Integer = &H0000 ' 00000000 00000000
    Public Const DAQMX_ITEM_NETADDRESS        As Integer = &H0001
    Public Const DAQMX_ITEM_NETPORT           As Integer = &H0002
    Public Const DAQMX_ITEM_NETSUBMASK        As Integer = &H0003
    Public Const DAQMX_ITEM_NETGATEWAY        As Integer = &H0004
    ' MXUnitData
    Public Const DAQMX_ITEM_UNITTYPE          As Integer = &H0005 ' 00000000 00000101
    Public Const DAQMX_ITEM_UNITSTYLE         As Integer = &H0006
    Public Const DAQMX_ITEM_UNITNO            As Integer = &H0007
    Public Const DAQMX_ITEM_UNITTEMP          As Integer = &H0008
    Public Const DAQMX_ITEM_UNITCFTIMEOUT     As Integer = &H0009
    Public Const DAQMX_ITEM_UNITCFWRITEMODE   As Integer = &H000A
    Public Const DAQMX_ITEM_UNITFREQUENCY     As Integer = &H000B
    Public Const DAQMX_ITEM_UNITPARTNO        As Integer = &H000C
    Public Const DAQMX_ITEM_UNITOPTION        As Integer = &H000D
    Public Const DAQMX_ITEM_UNITSERIAL        As Integer = &H000E
    Public Const DAQMX_ITEM_UNITMAC           As Integer = &H000F ' 00000000 00001111
    ' MXStatus
    Public Const DAQMX_ITEM_UNITSTATUS        As Integer = &H0010 ' 00000000 00010000
    Public Const DAQMX_ITEM_CNTCONFIG         As Integer = &H0011
    Public Const DAQMX_ITEM_CNTTIME           As Integer = &H0012
    Public Const DAQMX_ITEM_FIFONUM           As Integer = &H0013
    Public Const DAQMX_ITEM_BACKUP            As Integer = &H0014
    ' MXCFInfo
    Public Const DAQMX_ITEM_CFSTATUS          As Integer = &H0015 ' 00000000 00010101
    Public Const DAQMX_ITEM_CFSIZE            As Integer = &H0016
    Public Const DAQMX_ITEM_CFREMAIN          As Integer = &H0017
    ' MXFIFOInfo by FIFO
    Public Const DAQMX_ITEM_FIFOSTATUS        As Integer = &H0018 ' 00000000 000110xx
    Public Const DAQMX_ITEM_FIFOINTERVAL      As Integer = &H001C ' 00000000 000111xx
    Public Const DAQMX_ITEM_FIFOOLDNO         As Integer = &H0020 ' 00000000 001000xx
    Public Const DAQMX_ITEM_FIFONEWNO         As Integer = &H0024 ' 00000000 001001xx
    ' MXModuleData by Module
    Public Const DAQMX_ITEM_MODULETYPE        As Integer = &H0028 ' 00000000 00101xxx
    Public Const DAQMX_ITEM_MODULECHNUM       As Integer = &H0030 ' 00000000 00110xxx
    Public Const DAQMX_ITEM_MODULEINTERVAL    As Integer = &H0038 ' 00000000 00111xxx
    Public Const DAQMX_ITEM_MODULEINTEGRAL    As Integer = &H0040 ' 00000000 01000xxx
    Public Const DAQMX_ITEM_MODULESTANDBY     As Integer = &H0048 ' 00000000 01001xxx
    Public Const DAQMX_ITEM_MODULEREALTYPE    As Integer = &H0050 ' 00000000 01010xxx
    Public Const DAQMX_ITEM_MODULESTATUS      As Integer = &H0058 ' 00000000 01011xxx
    Public Const DAQMX_ITEM_MODULEVERSION     As Integer = &H0060 ' 00000000 01100xxx
    Public Const DAQMX_ITEM_MODULETERMINAL    As Integer = &H0068 ' 00000000 01101xxx
    Public Const DAQMX_ITEM_MODULEFIFONO      As Integer = &H0070 ' 00000000 01110xxx
    Public Const DAQMX_ITEM_MODULESERIAL      As Integer = &H0078 ' 00000000 01111xxx
    ' MXChID by Channel
    Public Const DAQMX_ITEM_CHVALID           As Integer = &H0080 ' 00000000 10xxxxxx
    Public Const DAQMX_ITEM_CHPOINT           As Integer = &H00C0 ' 00000000 11xxxxxx
    Public Const DAQMX_ITEM_CHKIND            As Integer = &H0100 ' 00000001 00xxxxxx
    Public Const DAQMX_ITEM_CHRANGE           As Integer = &H0140 ' 00000001 01xxxxxx
    Public Const DAQMX_ITEM_CHSCALE           As Integer = &H0180 ' 00000001 10xxxxxx
    Public Const DAQMX_ITEM_CHUNIT            As Integer = &H01C0 ' 00000001 11xxxxxx
    Public Const DAQMX_ITEM_CHTAG             As Integer = &H0200 ' 00000010 00xxxxxx
    Public Const DAQMX_ITEM_CHCOMMENT         As Integer = &H0240 ' 00000010 01xxxxxx
    ' MXAlarm by Channel
    Public Const DAQMX_ITEM_ALARMTYPE         As Integer = &H0280 ' 00000010 1yxxxxxx
    Public Const DAQMX_ITEM_ALARMON           As Integer = &H0300 ' 00000011 0yxxxxxx
    Public Const DAQMX_ITEM_ALARMOFF          As Integer = &H0380 ' 00000011 1yxxxxxx
    ' MXChConfigAIDI by Channel
    Public Const DAQMX_ITEM_CHSPANMIN         As Integer = &H0400 ' 00000100 00xxxxxx
    Public Const DAQMX_ITEM_CHSPANMAX         As Integer = &H0440 ' 00000100 01xxxxxx
    Public Const DAQMX_ITEM_CHSCALEMIN        As Integer = &H0480 ' 00000100 10xxxxxx
    Public Const DAQMX_ITEM_CHSCALEMAX        As Integer = &H04C0 ' 00000100 11xxxxxx
    Public Const DAQMX_ITEM_CHREFCHNO         As Integer = &H0500 ' 00000101 00xxxxxx
    ' MXChConfigAI by Channel
    Public Const DAQMX_ITEM_CHFILTER          As Integer = &H0540 ' 00000101 01xxxxxx
    Public Const DAQMX_ITEM_CHRJCTYPE         As Integer = &H0580 ' 00000101 10xxxxxx
    Public Const DAQMX_ITEM_CHRJCVOLT         As Integer = &H05C0 ' 00000101 11xxxxxx
    Public Const DAQMX_ITEM_CHBURNOUT         As Integer = &H0600 ' 00000110 00xxxxxx
    ' MXChConfigDO by Channel
    Public Const DAQMX_ITEM_CHDEENERGIZE      As Integer = &H0640 ' 00000110 01xxxxxx
    Public Const DAQMX_ITEM_CHREFALARM        As Integer = &H0680 ' 00000110 1yxxxxxx
    Public Const DAQMX_ITEM_CHHOLD            As Integer = &H0700 ' 00000111 00xxxxxx
    ' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
    ' since R2.01
    ' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
    ' MXChConfigAOPWM by Channel
    Public Const DAQMX_ITEM_CHOUTPUTTYPE      As Integer = &H0740 ' 00000111 01xxxxxx
    Public Const DAQMX_ITEM_CHIDLECHOICE      As Integer = &H0780 ' 00000111 10xxxxxx
    Public Const DAQMX_ITEM_CHERRCHOICE       As Integer = &H07C0 ' 00000111 11xxxxxx
    Public Const DAQMX_ITEM_CHPRESETVALUE     As Integer = &H0800 ' 00001000 00xxxxxx
    Public Const DAQMX_ITEM_CHPULSETIME       As Integer = &H0840 ' 00001000 01xxxxxx
    ' MXBalance by Channel
    Public Const DAQMX_ITEM_CHBALANCEVALID    As Integer = &H0880 ' 00001000 10xxxxxx
    Public Const DAQMX_ITEM_CHBALANCEVALUE    As Integer = &H08C0 ' 00001000 11xxxxxx
    ' MXStatus
    Public Const DAQMX_ITEM_STATUSTIME        As Integer = &H0900 ' 00001001 00000000
    Public Const DAQMX_ITEM_STATUSMSEC        As Integer = &H0901
    ' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
    ' since R3.01
    ' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
    ' MXChConfigPI by channel
    Public Const DAQMX_ITEM_CHCHATFILTER      As Integer = &H0940 ' 00001001 01xxxxxx
    ' Alarm : for No. 3,4
    Public Const DAQMX_ITEM_ALARMTYPE2        As Integer = &H0980 ' 00001001 0yxxxxxx
    Public Const DAQMX_ITEM_ALARMON2          As Integer = &H0A00 ' 00001010 0yxxxxxx
    Public Const DAQMX_ITEM_ALARMOFF2         As Integer = &H0A80 ' 00001010 1yxxxxxx
    Public Const DAQMX_ITEM_CHREFALARM2       As Integer = &H0B00 ' 00001011 0yxxxxxx
    ' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
    ' All and Mask
    ' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
    ' max
    Public Const DAQMX_MAX_INDEX_FIFO         As Integer = 2
    Public Const DAQMX_MAX_INDEX_MODULE       As Integer = 5
    Public Const DAQMX_MAX_INDEX_CHANNEL      As Integer = 59
    ' All
    Public Const DAQMX_ITEM_ALL_START         As Integer = 0
    Public Const DAQMX_ITEM_ALL_END_R1        As Integer = DAQMX_ITEM_CHHOLD + DAQMX_MAX_INDEX_CHANNEL ' R1.01
    Public Const DAQMX_ITEM_ALL_END_R2        As Integer = DAQMX_ITEM_STATUSMSEC ' R2.01
    Public Const DAQMX_ITEM_ALL_END_R3        As Integer = DAQMX_ITEM_CHREFALARM2 + &H0040 + DAQMX_MAX_INDEX_CHANNEL ' R3.01
    Public Const DAQMX_ITEM_ALL_END           As Integer = DAQMX_ITEM_ALL_END_R3 ' newest
    ' mask
    Public Const DAQMX_MASK_BYMODULE          As Integer = &H0007
    Public Const DAQMX_MASK_BYCHANNEL         As Integer = &H003F
    Public Const DAQMX_MASK_BYFIFO            As Integer = &H0003
    Public Const DAQMX_MASK_BYALARM           As Integer = &H0040 ' NOTE: position
    ' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
End Module
