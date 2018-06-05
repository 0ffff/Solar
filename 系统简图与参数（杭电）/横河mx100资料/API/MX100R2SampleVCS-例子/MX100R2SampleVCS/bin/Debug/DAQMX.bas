Attribute VB_Name = "DAQMX"
' DAQMX.bas
' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
'
' Copyright (c) 2003-2007 Yokogawa Electric Corporation. All rights reserved.
'
' This is defined export DAQMX.dll.
' Declare Functions for Visual Basic
'
' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
' 2007/09/30 Ver.3 Rev.1
' 2007/05/30 Ver.3 Rev.0
' 2004/11/01 Ver.2 Rev.1
' 2003/05/30 Ver.1 Rev.1
' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --

' communication
Public Const DAQMX_COMMPORT = 34316

' total number
Public Const DAQMX_NUMMODULE = 6
Public Const DAQMX_NUMCHANNEL = 60
Public Const DAQMX_NUMDO = DAQMX_NUMCHANNEL
Public Const DAQMX_NUMFIFO = 3
Public Const DAQMX_NUMALARM = 4
Public Const DAQMX_NUMSEGMENT = 2
Public Const DAQMX_NUMMACADDR = 6
Public Const DAQMX_NUMAOPWM = DAQMX_NUMCHANNEL
Public Const DAQMX_NUMBALANCE = DAQMX_NUMCHANNEL
Public Const DAQMX_NUMOUTPUT = DAQMX_NUMCHANNEL

' string length without NULL
Public Const DAQMX_MAXHOSTNAMELEN = 64
Public Const DAQMX_MAXUNITLEN = 6
Public Const DAQMX_MAXTAGLEN = 15
Public Const DAQMX_MAXCOMMENTLEN = 30
Public Const DAQMX_MAXSERIALLEN = 9
Public Const DAQMX_MAXPARTNOLEN = 7

' maximum number
Public Const DAQMX_MAXDECIMALPOINT = 4
Public Const DAQMX_MAXDISPTIME = 120000
Public Const DAQMX_MAXPULSETIME = 30000

' constant value
Public Const DAQMX_INSTANTANEOUS = -1
Public Const DAQMX_REFCHNO_NONE = 0
Public Const DAQMX_REFCHNO_ALL = -1
Public Const DAQMX_LEVELNO_ALL = -1
Public Const DAQMX_DONO_ALL = -1
Public Const DAQMX_SEGMENTNO_ALL = -1
Public Const DAQMX_CHNO_ALL = -1
Public Const DAQMX_MODULENO_ALL = -1
Public Const DAQMX_FIFONO_ALL = -1
Public Const DAQMX_AOPWMNO_ALL = -1
Public Const DAQMX_BALANCENO_ALL = -1
Public Const DAQMX_OUTPUTNO_ALL = -1

' valid
Public Const DAQMX_VALID_OFF = 0
Public Const DAQMX_VALID_ON = 1

' flag
Public Const DAQMX_FLAG_OFF = &H0000
Public Const DAQMX_FLAG_ENDDATA = &H0001

' data status
Public Const DAQMX_DATA_UNKNOWN = &H00000000
Public Const DAQMX_DATA_NORMAL = &H00000001
Public Const DAQMX_DATA_PLUSOVER = &H00007FFF
Public Const DAQMX_DATA_MINUSOVER = &H00008001
Public Const DAQMX_DATA_SKIP = &H00008002
Public Const DAQMX_DATA_ILLEGAL = &H00008003
Public Const DAQMX_DATA_NODATA = &H00008005
Public Const DAQMX_DATA_LACK = &H00008400
Public Const DAQMX_DATA_INVALID = &H00008700

' alarm type
Public Const DAQMX_ALARM_NONE = 0
Public Const DAQMX_ALARM_UPPER = 1
Public Const DAQMX_ALARM_LOWER = 2
Public Const DAQMX_ALARM_UPDIFF = 3
Public Const DAQMX_ALARM_LOWDIFF = 4

' system control
Public Const DAQMX_SYSTEM_RECONSTRUCT = 1
Public Const DAQMX_SYSTEM_INITOPE = 2
Public Const DAQMX_SYSTEM_RESETALARM = 3

' channel kind
Public Const DAQMX_CHKIND_NONE = &H0000
Public Const DAQMX_CHKIND_AI = &H0010
Public Const DAQMX_CHKIND_AIDIFF = &H0011
Public Const DAQMX_CHKIND_AIRJC = &H0012
Public Const DAQMX_CHKIND_DI = &H0030
Public Const DAQMX_CHKIND_DIDIFF = &H0031
Public Const DAQMX_CHKIND_DO = &H0040
Public Const DAQMX_CHKIND_DOCOM = &H0041
Public Const DAQMX_CHKIND_DOFAIL = &H0042
Public Const DAQMX_CHKIND_DOERR = &H0043
Public Const DAQMX_CHKIND_AO = &H0020
Public Const DAQMX_CHKIND_AOCOM = &H0021
Public Const DAQMX_CHKIND_PWM = &H0060
Public Const DAQMX_CHKIND_PWMCOM = &H0061
Public Const DAQMX_CHKIND_PI = &H0050
Public Const DAQMX_CHKIND_PIDIFF = &H0051
Public Const DAQMX_CHKIND_CI = &H0070
Public Const DAQMX_CHKIND_CIDIFF = &H0071

' scale type
Public Const DAQMX_SCALE_NONE = 0
Public Const DAQMX_SCALE_LINER = 1

' module type
' 0xF0000010 -> -268435440
' 0xF0001C10 -> -268428272
' 0xB0101F10 -> -1341120752
' 0xD0001130 -> -805301968
' 0x0000FF00 -> 65280
Public Const DAQMX_MODULE_NONE = &H0
Public Const DAQMX_MODULE_MX110UNVH04 = -268435440
Public Const DAQMX_MODULE_MX110UNVM10 = -268428272
Public Const DAQMX_MODULE_MX115D05H10 = &H10003010
Public Const DAQMX_MODULE_MX125MKCM10 = &H00402010
Public Const DAQMX_MODULE_MX110V4RM06 = -1341120752
Public Const DAQMX_MODULE_MX112NDIM04 = &H01004010
Public Const DAQMX_MODULE_MX112B35M04 = &H01004110
Public Const DAQMX_MODULE_MX112B12M04 = &H01004210
Public Const DAQMX_MODULE_MX115D24H10 = &H10003210
Public Const DAQMX_MODULE_MX120VAOM08 = &H0080C010
Public Const DAQMX_MODULE_MX120PWMM08 = &H0020C810
Public Const DAQMX_MODULE_HIDDEN = 65280
Public Const DAQMX_MODULE_MX114PLSM10 = &H0400B010
Public Const DAQMX_MODULE_MX110VTDL30 = -805301968
Public Const DAQMX_MODULE_MX118CANM10 = &H00085110
Public Const DAQMX_MODULE_MX118CANM20 = &H00085220
Public Const DAQMX_MODULE_MX118CANM30 = &H00085330
Public Const DAQMX_MODULE_MX118CANSUB = &H00085000
Public Const DAQMX_MODULE_MX118CANMERR = &H00005A10
Public Const DAQMX_MODULE_MX118CANSERR = &H00005B10

' how many channels by each module
Public Const DAQMX_CHNUM_0 = 0
Public Const DAQMX_CHNUM_4 = 4
Public Const DAQMX_CHNUM_6 = 6
Public Const DAQMX_CHNUM_8 = 8
Public Const DAQMX_CHNUM_10 = 10
Public Const DAQMX_CHNUM_30 = 30

' interval (msec)
Public Const DAQMX_INTERVAL_10 = 10
Public Const DAQMX_INTERVAL_50 = 50
Public Const DAQMX_INTERVAL_100 = 100
Public Const DAQMX_INTERVAL_200 = 200
Public Const DAQMX_INTERVAL_500 = 500
Public Const DAQMX_INTERVAL_1000 = 1000
Public Const DAQMX_INTERVAL_2000 = 2000
Public Const DAQMX_INTERVAL_5000 = 5000
Public Const DAQMX_INTERVAL_10000 = 10000
Public Const DAQMX_INTERVAL_20000 = 20000
Public Const DAQMX_INTERVAL_30000 = 30000
Public Const DAQMX_INTERVAL_60000 = 60000

' filter
Public Const DAQMX_FILTER_0 = 0
Public Const DAQMX_FILTER_5 = 1
Public Const DAQMX_FILTER_10 = 2
Public Const DAQMX_FILTER_20 = 3
Public Const DAQMX_FILTER_25 = 4
Public Const DAQMX_FILTER_40 = 5
Public Const DAQMX_FILTER_50 = 6
Public Const DAQMX_FILTER_100 = 7

' RJC Type
Public Const DAQMX_RJC_INTERNAL = 0
Public Const DAQMX_RJC_EXTERNAL = 1

' burnout
Public Const DAQMX_BURNOUT_OFF = 0
Public Const DAQMX_BURNOUT_UP = 1
Public Const DAQMX_BURNOUT_DOWN = 2

' unit type
Public Const DAQMX_UNITTYPE_NONE = &H00000000
Public Const DAQMX_UNITTYPE_MX100 = &H00010000

' terminal type
Public Const DAQMX_TERMINAL_SCREW = 0
Public Const DAQMX_TERMINAL_CLAMP = 1
Public Const DAQMX_TERMINAL_NDIS = 2
Public Const DAQMX_TERMINAL_DSUB = 3

' AD
Public Const DAQMX_INTEGRAL_AUTO = 0
Public Const DAQMX_INTEGRAL_50HZ = 1
Public Const DAQMX_INTEGRAL_60HZ = 2

' temparature unit
Public Const DAQMX_TEMPUNIT_C = 0
Public Const DAQMX_TEMPUNIT_F = 1

' CF write mode
Public Const DAQMX_CFWRITEMODE_ONCE = 0
Public Const DAQMX_CFWRITEMODE_FIFO = 1

' CF status
Public Const DAQMX_CFSTATUS_NONE = &H0000
Public Const DAQMX_CFSTATUS_EXIST = &H0001
Public Const DAQMX_CFSTATUS_USE = &H0002
Public Const DAQMX_CFSTATUS_FORMAT = &H0004

' UNIT status
Public Const DAQMX_UNITSTAT_NONE = &H0000
Public Const DAQMX_UNITSTAT_INIT = &H0001
Public Const DAQMX_UNITSTAT_STOP = &H0002
Public Const DAQMX_UNITSTAT_RUN = &H0003
Public Const DAQMX_UNITSTAT_BACKUP = &H0004

' FIFO status
Public Const DAQMX_FIFOSTAT_NONE = DAQMX_UNITSTAT_NONE
Public Const DAQMX_FIFOSTAT_INIT = DAQMX_UNITSTAT_INIT
Public Const DAQMX_FIFOSTAT_STOP = DAQMX_UNITSTAT_STOP
Public Const DAQMX_FIFOSTAT_RUN = DAQMX_UNITSTAT_RUN
Public Const DAQMX_FIFOSTAT_BACKUP = DAQMX_UNITSTAT_BACKUP

' segment display type
Public Const DAQMX_DISPTYPE_NONE = 0
Public Const DAQMX_DISPTYPE_ON = 1
Public Const DAQMX_DISPTYPE_BLINK = 2

' choice
Public Const DAQMX_CHOICE_PREV = 0
Public Const DAQMX_CHOICE_PRESET = 1

' transmit
Public Const DAQMX_TRANSMIT_NONE = 0
Public Const DAQMX_TRANSMIT_RUN = 1
Public Const DAQMX_TRANSMIT_STOP = 2

' balance
Public Const DAQMX_BALANCE_NONE = 0
Public Const DAQMX_BALANCE_DONE = 1
Public Const DAQMX_BALANCE_NG = 2
Public Const DAQMX_BALANCE_ERROR = 3

' option
Public Const DAQMX_OPTION_NONE = &H0000
Public Const DAQMX_OPTION_DS = &H0001

' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
' range
' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --

' Reference
Public Const DAQMX_RANGE_REFERENCE = -1

' Volt
Public Const DAQMX_RANGE_VOLT_20MV = &H0000
Public Const DAQMX_RANGE_VOLT_60MV = &H0001
Public Const DAQMX_RANGE_VOLT_200MV = &H0002
Public Const DAQMX_RANGE_VOLT_2V = &H0003
Public Const DAQMX_RANGE_VOLT_6V = &H0004
Public Const DAQMX_RANGE_VOLT_20V = &H0005
Public Const DAQMX_RANGE_VOLT_100V = &H0006
Public Const DAQMX_RANGE_VOLT_60MVH = &H0007
Public Const DAQMX_RANGE_VOLT_1V = &H0008
Public Const DAQMX_RANGE_VOLT_6VH = &H0009

' TC
Public Const DAQMX_RANGE_TC_R = &H0200
Public Const DAQMX_RANGE_TC_S = &H0201
Public Const DAQMX_RANGE_TC_B = &H0202
Public Const DAQMX_RANGE_TC_K = &H0203
Public Const DAQMX_RANGE_TC_E = &H0204
Public Const DAQMX_RANGE_TC_J = &H0205
Public Const DAQMX_RANGE_TC_T = &H0206
Public Const DAQMX_RANGE_TC_N = &H0207
Public Const DAQMX_RANGE_TC_W = &H0208
Public Const DAQMX_RANGE_TC_L = &H0209
Public Const DAQMX_RANGE_TC_U = &H020A
Public Const DAQMX_RANGE_TC_KP = &H020B
Public Const DAQMX_RANGE_TC_PL = &H020C
Public Const DAQMX_RANGE_TC_PR = &H020D
Public Const DAQMX_RANGE_TC_NNM = &H020E
Public Const DAQMX_RANGE_TC_WR = &H020F
Public Const DAQMX_RANGE_TC_WWR = &H0210
Public Const DAQMX_RANGE_TC_AWG = &H0211
Public Const DAQMX_RANGE_TC_XK = &H0212

' RTD 1mA
Public Const DAQMX_RANGE_RTD_1MAPT = &H0300
Public Const DAQMX_RANGE_RTD_1MAJPT = &H0301
Public Const DAQMX_RANGE_RTD_1MAPTH = &H0302
Public Const DAQMX_RANGE_RTD_1MAJPTH = &H0303
Public Const DAQMX_RANGE_RTD_1MANIS = &H0304
Public Const DAQMX_RANGE_RTD_1MANID = &H0305
Public Const DAQMX_RANGE_RTD_1MANI120 = &H0306
Public Const DAQMX_RANGE_RTD_1MAPT50 = &H0307
Public Const DAQMX_RANGE_RTD_1MACU10GE = &H0308
Public Const DAQMX_RANGE_RTD_1MACU10LN = &H0309
Public Const DAQMX_RANGE_RTD_1MACU10WEED = &H030A
Public Const DAQMX_RANGE_RTD_1MACU10BAILEY = &H030B
Public Const DAQMX_RANGE_RTD_1MAJ263B = &H030C
Public Const DAQMX_RANGE_RTD_1MACU10A392 = &H030D
Public Const DAQMX_RANGE_RTD_1MACU10A393 = &H030E
Public Const DAQMX_RANGE_RTD_1MACU25 = &H030F
Public Const DAQMX_RANGE_RTD_1MACU53 = &H0310
Public Const DAQMX_RANGE_RTD_1MACU100 = &H0311
Public Const DAQMX_RANGE_RTD_1MAPT25 = &H0312
Public Const DAQMX_RANGE_RTD_1MACU10GEH = &H0313
Public Const DAQMX_RANGE_RTD_1MACU10LNH = &H0314
Public Const DAQMX_RANGE_RTD_1MACU10WEEDH = &H0315
Public Const DAQMX_RANGE_RTD_1MACU10BAILEYH = &H0316
Public Const DAQMX_RANGE_RTD_1MAPTN = &H0317
Public Const DAQMX_RANGE_RTD_1MAJPTN = &H0318
Public Const DAQMX_RANGE_RTD_1MAPTG = &H0319
Public Const DAQMX_RANGE_RTD_1MACU100G = &H031A
Public Const DAQMX_RANGE_RTD_1MACU50G = &H031B
Public Const DAQMX_RANGE_RTD_1MACU10G = &H031C

' RTD 2mA
Public Const DAQMX_RANGE_RTD_2MAPT = &H0400
Public Const DAQMX_RANGE_RTD_2MAJPT = &H0401
Public Const DAQMX_RANGE_RTD_2MAPTH = &H0402
Public Const DAQMX_RANGE_RTD_2MAJPTH = &H0403
Public Const DAQMX_RANGE_RTD_2MAPT50 = &H0404
Public Const DAQMX_RANGE_RTD_2MACU10GE = &H0405
Public Const DAQMX_RANGE_RTD_2MACU10LN = &H0406
Public Const DAQMX_RANGE_RTD_2MACU10WEED = &H0407
Public Const DAQMX_RANGE_RTD_2MACU10BAILEY = &H0408
Public Const DAQMX_RANGE_RTD_2MAJ263B = &H0409
Public Const DAQMX_RANGE_RTD_2MACU10A392 = &H040A
Public Const DAQMX_RANGE_RTD_2MACU10A393 = &H040B
Public Const DAQMX_RANGE_RTD_2MACU25 = &H040C
Public Const DAQMX_RANGE_RTD_2MACU53 = &H040D
Public Const DAQMX_RANGE_RTD_2MACU100 = &H040E
Public Const DAQMX_RANGE_RTD_2MAPT25 = &H040F
Public Const DAQMX_RANGE_RTD_2MACU10GEH = &H0410
Public Const DAQMX_RANGE_RTD_2MACU10LNH = &H0411
Public Const DAQMX_RANGE_RTD_2MACU10WEEDH = &H0412
Public Const DAQMX_RANGE_RTD_2MACU10BAILEYH = &H0413
Public Const DAQMX_RANGE_RTD_2MAPTN = &H0414
Public Const DAQMX_RANGE_RTD_2MAJPTN = &H0415
Public Const DAQMX_RANGE_RTD_2MACU100G = &H0416
Public Const DAQMX_RANGE_RTD_2MACU50G = &H0417
Public Const DAQMX_RANGE_RTD_2MACU10G = &H0418

' DI
Public Const DAQMX_RANGE_DI_LEVEL = 1
Public Const DAQMX_RANGE_DI_CONTACT = 2

' DI : detail
Public Const DAQMX_RANGE_DI_LEVEL_AI = &H0100
Public Const DAQMX_RANGE_DI_CONTACT_AI4 = &H0101
Public Const DAQMX_RANGE_DI_CONTACT_AI10 = &H0102
Public Const DAQMX_RANGE_DI_LEVEL_DI = &H0103
Public Const DAQMX_RANGE_DI_CONTACT_DI = &H0104
Public Const DAQMX_RANGE_DI_LEVEL_DI5V = DAQMX_RANGE_DI_LEVEL_DI
Public Const DAQMX_RANGE_DI_LEVEL_DI24V = &H0105
Public Const DAQMX_RANGE_DI_CONTACT_AI30 = DAQMX_RANGE_DI_CONTACT_AI10

' RTD 0.25mA
Public Const DAQMX_RANGE_RTD_025MAPT500 = &H0500
Public Const DAQMX_RANGE_RTD_025MAPT1K = &H0501

' RES
Public Const DAQMX_RANGE_RES_20 = &H0600
Public Const DAQMX_RANGE_RES_200 = &H0601
Public Const DAQMX_RANGE_RES_2K = &H0602

' STR
Public Const DAQMX_RANGE_STRAIN_2K = &H0700
Public Const DAQMX_RANGE_STRAIN_20K = &H0701
Public Const DAQMX_RANGE_STRAIN_200K = &H0702

' AO
Public Const DAQMX_RANGE_AO_10V = &H1000
Public Const DAQMX_RANGE_AO_20MA = &H1001

' PWM
Public Const DAQMX_RANGE_PWM_1MS = &H1100
Public Const DAQMX_RANGE_PWM_10MS = &H1101

' CAN
Public Const DAQMX_RANGE_COM_CAN = &H0800

' PI
Public Const DAQMX_RANGE_PI_LEVEL = &H0900
Public Const DAQMX_RANGE_PI_CONTACT = &H0901

' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
' Output
Public Const DAQMX_OUTPUT_NONE = 0
Public Const DAQMX_OUTPUT_AO_10V = DAQMX_RANGE_AO_10V
Public Const DAQMX_OUTPUT_AO_20MA = DAQMX_RANGE_AO_20MA
Public Const DAQMX_OUTPUT_PWM_1MS = DAQMX_RANGE_PWM_1MS
Public Const DAQMX_OUTPUT_PWM_10MS = DAQMX_RANGE_PWM_10MS

' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
' Structures
' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --

' 64bit data
Public Type MXDataNo
	aLow		As Long
	aHigh		As Long
End Type
Public Type MXUserTime
	aLow		As Long
	aHigh		As Long
End Type

' Date Time
Public Type MXDateTime
	aTime		As Long
	aMilliSecond	As Long
End Type

' Alarm
Public Type MXAlarm
	aType		As Long
	aReserve	As Long
	aON		As Long	
	aOFF		As Long
End Type

' Measured Data
Public Type MXDataInfo
	aValue		As Long
	aStatus		As Long
	aAlarm(1 To 4)	As Long
End Type

' Channel
Public Type MXChConfigAIDI
	aSpanMin	As Long
	aSpanMax	As Long
	aScaleMin	As Long
	aScaleMax	As Long
	aRefChNo	As Long
	aChatFilter	As Long
End Type
Public Type MXChConfigAI
	aFilter		As Long
	aRJCType	As Long
	aRJCVolt	As Long
	aBurnout	As Long
End Type
Public Type MXChConfigDO
	aDeenergize	As Long
	aHold		As Long
	aRefAlarm(1 To 4, 1 To 60)	As Byte
End Type
Public Type MXChID
	aChNo		As Long
	aPoint		As Long
	aValid		As Long
	aKind		As Long
	aRange		As Long
	aScaleType	As Long
	aUnit		As String * DAQMX_MAXUNITLEN
	align1(0 To 1)	As Byte
	aTag		As String * DAQMX_MAXTAGLEN
	aNULL		As Byte
	aComment	As String * DAQMX_MAXCOMMENTLEN
	align2(0 To 1)	As Byte
	aAlarm(1 To 4)	As MXAlarm
End Type
Public Type MXChConfig
	aChID	As MXChID
	aAIDI	As MXChConfigAIDI
	aAI	As MXChConfigAI
	aDO	As MXChConfigDO
End Type
Public Type MXChInfo
	aChID		As MXChID
	aFIFONo		As Long
	aFIFOIndex	As Long
	aOrigMin	As Double
	aOrigMax	As Double
	aDispMin	As Double
	aDispMax	As Double
	aRealMin	As Double
	aRealMax	As Double
End Type

' System
Public Type MXProductInfo
	aOption		As Long
	aCheck		As Long
	aSerial		As String * DAQMX_MAXSERIALLEN
	aNULL		As Byte
	aMAC(0 To 5)	As Byte
End Type
Public Type MXUnitData
	aType		As Long
	aStyle		As Long
	aNo		As Long
	aTempUnit	As Long
	aCFTimeout	As Long
	aCFWriteMode    As Long
	aFrequency	As Long
	aReserve        As Long
	aPartNo		As String * DAQMX_MAXPARTNOLEN
	aNULL		As Byte
	aProduct	As MXProductInfo
End Type
Public Type MXModuleData
	aType		As Long
	aChNum		As Long
	aInterval	As Long
	aIntegralTime	As Long
	aStandbyType	As Long
	aRealType	As Long
	aStatus		As Long
	aVersion	As Long
	aTerminalType	As Long
	aFIFONo		As Long
	aProduct	As MXProductInfo
End Type
Public Type MXSystemInfo
	aUnit           As MXUnitData
	aModule(0 To 5) As MXModuleData
End Type

' Status
Public Type MXCFInfo
	aStatus		As Long
	aSize		As Long
	aRemain		As Long
	aReserve	As Long
End Type
Public Type MXFIFOInfo
	aNo		As Long
	aStatus		As Long
	aInterval	As Long
	aReserve	As Long
	aOldNo		As MXDataNo
	aNewNo		As MXDataNo
End Type
Public Type MXStatus
	aUnitStatus		As Long
	aConfigCnt		As Long
	aTimeCnt		As Long
	aFIFONum		As Long
	aBackup			As Long
	aReserve		As Long
	aCFInfo			As MXCFInfo
	aFIFOInfo(0 To 2)	As MXFIFOInfo
	aDateTime		As MXDateTime
End Type

' Network
Public Type MXNetInfo
	aAddress	As Long
	aPort		As Long
	aSubMask	As Long
	aGateway	As Long
	aHost		As String * DAQMX_MAXHOSTNAMELEN
	align(0 To 7)	As Byte
End Type

' DO
Public Type MXDO
	aValid	As Long
	aONOFF	As Long
End Type
Public Type MXDOData
	aDO(1 To 60)    As MXDO
End Type

' Segment
Public Type MXSegment
	aPattern(0 To 1) As Long
End Type

' Balance
Public Type MXBalance
	aValid As Long
	aValue As Long
End Type
Public Type MXBalanceData
	aBalance(1 To 60) As MXBalance
End Type
Public Type MXBalanceResult
	aResult(1 To 60) As Long
End Type

' Output
Public Type MXOutput
	aType		As Long
	aIdleChoice     As Long
	aErrorChoice    As Long
	aPresetValue    As Long
	aPulseTime      As Long
	aReserve	As Long
End Type
Public Type MXOutputData
	aOutput(1 To 60) As MXOutput
End Type

' AO/PWM
Public Type MXAOPWM
	aValid  As Long
	aValue  As Long
End Type
Public Type MXAOPWMData
	aAOPWM(1 To 60) As MXAOPWM
End Type
Public Type MXTransmit
	aTransmit(1 To 60) As Long
End Type

' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
' Low level communications
' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --

Public Declare Function openMX Lib "DAQMX" (ByVal strAddress As String, ByRef errorCode As Long) As Long

Public Declare Function closeMX Lib "DAQMX" (ByVal daqmx As Long) As Long

' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
' Middle level communications
' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --

' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
' FIFO control commands
' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --

Public Declare Function startFIFOMX Lib "DAQMX" (ByVal daqmx As Long) As Long

Public Declare Function stopFIFOMX Lib "DAQMX" (ByVal daqmx As Long) As Long

Public Declare Function autoFIFOMX Lib "DAQMX" (ByVal daqmx As Long, ByVal bAuto As Long) As Long

' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
' Date time commands
' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --

Public Declare Function setDateTimeMX Lib "DAQMX" (ByVal daqmx As Long, ByRef pMXDateTime As MXDateTime) As Long

Public Declare Function setDateTimeNowMX Lib "DAQMX" (ByVal daqmx As Long) As Long

' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
' Control Commands
' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --

Public Declare Function setBackupMX Lib "DAQMX" (ByVal daqmx As Long, ByVal bBakcup As Long, ByVal iCFWriteMode As Long) As Long

Public Declare Function formatCFMX Lib "DAQMX" (ByVal daqmx As Long) As Long

Public Declare Function initSystemMX Lib "DAQMX" (ByVal daqmx As Long, ByVal iCtrl As Long) As Long

Public Declare Function setSegmentMX Lib "DAQMX" (ByVal daqmx As Long, ByVal iDispType As Long, ByVal dispTime As Long, ByRef newSegment As MXSegment, ByRef oldSegment As MXSegment) As Long

' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
' Get status
' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --

Public Declare Function getStatusDataMX Lib "DAQMX" (ByVal daqmx As Long, ByRef pMXStatus As MXStatus) As Long

' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
' Get system
' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --

Public Declare Function getSystemConfigMX Lib "DAQMX" (ByVal daqmx As Long, ByRef pMXSystemInfo As MXSystemInfo) As Long

' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
' Configurature -> @see for Visual Basic
' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --

' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
' DO
' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --

Public Declare Function getDODataMX Lib "DAQMX" (ByVal daqmx As Long, ByRef pMXDOData As MXDOData) As Long

Public Declare Function setDODataMX Lib "DAQMX" (ByVal daqmx As Long, ByRef pMXDOData As MXDOData) As Long

Public Declare Function changeDODataMX Lib "DAQMX" (ByRef pMXDOData As MXDOData, ByVal doNo As Long, ByVal bValid As Long, ByVal bONOFF As Long) As Long

Public Declare Function setDOTypeMX Lib "DAQMX" (ByVal daqmx As Long, ByVal doNo As Long, ByVal iKind As Long, ByVal bDeenergize As Long, ByVal bHold As Long) As Long

' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
' Get channel information
' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --

Public Declare Function talkChInfoMX Lib "DAQMX" (ByVal daqmx As Long, ByVal startChNo As Long, ByVal endChNo As Long) As Long

Public Declare Function getChInfoMX Lib "DAQMX" (ByVal daqmx As Long, ByRef pMXChInfo As MXChInfo, ByRef pFlag As Long) As Long

' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
' Talker measured data by each channels
' talkChDataMX -> talkChDataVBMX @see for Visual Basic
' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --

Public Declare Function getChDataNoMX Lib "DAQMX" (ByVal daqmx As Long, ByVal chNo As Long, ByRef startDataNo As MXDataNo, ByRef endDataNo As MXDataNo) As Long

Public Declare Function talkChDataInstMX Lib "DAQMX" (ByVal daqmx As Long, ByVal chNo As Long) As Long

' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
' Talker measured data by each FIFO
' talkFIFODataMX -> talkFIFODataVBMX @see for Visual Basic
' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --

Public Declare Function getFIFODataNoMX Lib "DAQMX" (ByVal daqmx As Long, ByVal fifoNo As Long, ByRef startDataNo As MXDataNo, ByRef endDataNo As MXDataNo) As Long

Public Declare Function talkFIFODataInstMX Lib "DAQMX" (ByVal daqmx As Long, ByVal fifoNo As Long) As Long

' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
' Get measured data after talker
' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --

Public Declare Function getTimeDataMX Lib "DAQMX" (ByVal daqmx As Long, ByRef pMXDataNo As MXDataNo, ByRef pMXDateTime As MXDateTime, ByRef userTime as MXUserTime, ByRef pFlag As Long) As Long

Public Declare Function getChDataMX Lib "DAQMX" (ByVal daqmx As Long, ByRef pMXDataNo As MXDataNo, ByRef pMXChInfo As MXChInfo, ByRef pMXDataInfo As MXDataInfo, ByRef pFlag As Long) As Long

' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
' Misc
' setUserTimeMX ->setUserTimeVBMX @see for Visual Basic
' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --

Public Declare Function getLastErrorMX Lib "DAQMX" (ByVal daqmx As Long, ByRef lastErr As Long) As Long

' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
' Set range
' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --

Public Declare Function setSKIPMX Lib "DAQMX" (ByVal daqmx As Long, ByVal startChNo As Long, ByVal endChNo As Long) As Long

Public Declare Function setVOLTMX Lib "DAQMX" (ByVal daqmx As Long, ByVal iRangeVOLT As Long, ByVal startChNo As Long, ByVal endChNo As Long, ByVal spanMin As Long, ByVal spanMax As Long, ByVal scaleMin As Long, ByVal scaleMax As Long, ByVal scalePoint As Long) As Long

Public Declare Function setTCMX Lib "DAQMX" (ByVal daqmx As Long, ByVal iRangeTC As Long, ByVal startChNo As Long, ByVal endChNo As Long, ByVal spanMin As Long, ByVal spanMax As Long, ByVal scaleMin As Long, ByVal scaleMax As Long, ByVal scalePoint As Long) As Long

Public Declare Function setRTDMX Lib "DAQMX" (ByVal daqmx As Long, ByVal iRangeRTD As Long, ByVal startChNo As Long, ByVal endChNo As Long, ByVal spanMin As Long, ByVal spanMax As Long, ByVal scaleMin As Long, ByVal scaleMax As Long, ByVal scalePoint As Long) As Long

Public Declare Function setDIMX Lib "DAQMX" (ByVal daqmx As Long, ByVal iRangeDI As Long, ByVal startChNo As Long, ByVal endChNo As Long, ByVal spanMin As Long, ByVal spanMax As Long, ByVal scaleMin As Long, ByVal scaleMax As Long, ByVal scalePoint As Long) As Long

Public Declare Function setDELTAMX Lib "DAQMX" (ByVal daqmx As Long, ByVal refChNo As Long, ByVal startChNo As Long, ByVal endChNo As Long, ByVal spanMin As Long, ByVal spanMax As Long, ByVal scaleMin As Long, ByVal scaleMax As Long, ByVal scalePoint As Long, ByVal iRange As Long) As Long

Public Declare Function setRRJCMX Lib "DAQMX" (ByVal daqmx As Long, ByVal refChNo As Long, ByVal startChNo As Long, ByVal endChNo As Long, ByVal spanMin As Long, ByVal spanMax As Long) As Long

' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
' Scalling
' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --

Public Declare Function setScallingUnitMX Lib "DAQMX" (ByVal daqmx As Long, ByVal strUnit As String, ByVal startChNo As Long, ByVal endChNo As Long) As Long

' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
' Alarm
' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --

Public Declare Function setAlarmMX Lib "DAQMX" (ByVal daqmx As Long, ByVal levelNo As Long, ByVal startChNo As Long, ByVal endChNo As Long, ByVal iAlarmType As Long, ByVal value As Long, ByVal histerisys As Long) As Long

' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
' Channel configure
' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --

Public Declare Function setTagMX Lib "DAQMX" (ByVal daqmx As Long, ByVal strTag As String, ByVal startChNo As Long, ByVal endChNo As Long) As Long

Public Declare Function setCommentMX Lib "DAQMX" (ByVal daqmx As Long, ByVal strComment As String, ByVal startChNo As Long, ByVal endChNo As Long) As Long

Public Declare Function setRJCTypeMX Lib "DAQMX" (ByVal daqmx As Long, ByVal iRJCType As Long, ByVal startChNo As Long, ByVal endChNo As Long, ByVal volt As Long) As Long

Public Declare Function setFilterMX Lib "DAQMX" (ByVal daqmx As Long, ByVal iFilter As Long, ByVal startChNo As Long, ByVal endChNo As Long) As Long

Public Declare Function setBurnoutMX Lib "DAQMX" (ByVal daqmx As Long, ByVal iBurnout As Long, ByVal startChNo As Long, ByVal endChNo As Long) As Long

Public Declare Function setRefAlarmMX Lib "DAQMX" (ByVal daqmx As Long, ByVal refChNo As Long, ByVal startChNo As Long, ByVal endChNo As Long, ByVal levelNo As Long, ByVal bValid As Long) As Long

' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
' Unit configure
' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --

Public Declare Function setIntervalMX Lib "DAQMX" (ByVal daqmx As Long, ByVal moduleNo As Long, ByVal iInterval As Long, ByVal iHz As Long) As Long

Public Declare Function setTempUnitMX Lib "DAQMX" (ByVal daqmx As Long, ByVal iTempUnit As Long) As Long

Public Declare Function setUnitNoMX Lib "DAQMX" (ByVal daqmx As Long, ByVal unitNo As Long) As Long

Public Declare Function setSystemTimeoutMX Lib "DAQMX" (ByVal daqmx As Long, ByVal timeout As Long) As Long

' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
' Utilities
' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --

Public Declare Function toDoubleValueMX Lib "DAQMX" (ByVal dataValue As Long, ByVal point As Long) As Double

Public Declare Function toStringValueMX Lib "DAQMX" (ByVal dataValue As Long, ByVal point As Long, ByVal strValue As String, ByVal lenValue As Long) As Long

Public Declare Function toAlarmNameMX Lib "DAQMX" (ByVal iAlarmType As Long, ByVal strAlarm As String, ByVal lenAlarm As Long) As Long

' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
' Messages
' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --

Public Declare Function getVersionAPIMX Lib "DAQMX" () As Long

Public Declare Function getRevisionAPIMX  Lib "DAQMX" () As Long

Public Declare Function toErrorMessageMX Lib "DAQMX" (ByVal errCode As Long, ByVal errStr As String, ByVal errLen As Long) As Long

Public Declare Function getMaxLenErrorMessageMX Lib "DAQMX" () As Long

' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
' Deprecated command
' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --

Public Declare Function setTimeOutMX Lib "DAQMX" (ByVal daqmx As Long, ByVal seconds As Long) As Long

' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
' for Visual Basic
' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --

Public Declare Function talkConfigMX Lib "DAQMX" (ByVal daqmx As Long, ByRef pMXSystemInfo As MXSystemInfo, ByRef pMXStatus As MXStatus, ByRef pMXNetInfo As MXNetInfo) As Long

Public Declare Function getChConfigMX Lib "DAQMX" (ByVal daqmx As Long, ByRef pMXChConfig As MXChConfig, ByRef pFlag As Long) As Long

Public Declare Function setSystemConfigMX Lib "DAQMX" (ByVal daqmx As Long, ByRef pMXSystemInfo As MXSystemInfo) As Long

Public Declare Function setChConfigMX Lib "DAQMX" (ByVal daqmx As Long, ByRef pMXChConfig As MXChConfig) As Long

Public Declare Function talkChDataVBMX Lib "DAQMX" (ByVal daqmx As Long, ByVal chNo As Long, ByRef startDataNo As MXDataNo, ByRef endDataNo As MXDataNo) As Long

Public Declare Function talkFIFODataVBMX Lib "DAQMX" (ByVal daqmx As Long, ByVal fifoNo As Long, ByRef startDataNo As MXDataNo, ByRef endDataNo As MXDataNo) As Long

Public Declare Function setUserTimeVBMX Lib "DAQMX" (ByVal daqmx As Long, ByRef userTime As MXUserTime) As Long

Public Declare Sub incrementDataNoMX Lib "DAQMX" (ByRef dataNo As MXDataNo, ByVal increment As Long)

Public Declare Sub decrementDataNoMX Lib "DAQMX" (ByRef dataNo As MXDataNo, ByVal decrement As Long)

Public Declare Function compareDataNoMX Lib "DAQMX" (ByRef prevDataNo As MXDataNo, ByRef nextDataNo As MXDataNo) As Long

Public Declare Sub toDateTimeMX Lib "DAQMX" (ByRef pMXDateTime As MXDateTime, ByRef pYear As Long, ByRef pMonth As Long, ByRef pDay As Long, ByRef pHour As Long, ByRef pMinute As Long, ByRef pSecond As Long)

' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
' since R2.01
' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --

Public Declare Function setAOPWMDataMX Lib "DAQMX" (ByVal daqmx As Long, ByRef pMXAOPWMData As MXAOPWMData) As Long

Public Declare Function setTransmitMX Lib "DAQMX" (ByVal daqmx As Long, ByRef pMXTransmit As MXTransmit) As Long

Public Declare Function runBalanceMX Lib "DAQMX" (ByVal daqmx As Long, ByRef pMXBalanceData As MXBalanceData, ByRef pMXBalanceResult As MXBalanceResult) As Long

Public Declare Function resetBalanceMX Lib "DAQMX" (ByVal daqmx As Long, ByRef pMXBalanceData As MXBalanceData, ByRef pMXBalanceResult As MXBalanceResult) As Long

Public Declare Function setBalanceMX Lib "DAQMX" (ByVal daqmx As Long, ByRef pMXBalanceData As MXBalanceData) As Long

Public Declare Function getBalanceMX Lib "DAQMX" (ByVal daqmx As Long, ByRef pMXBalanceData As MXBalanceData) As Long

Public Declare Function getAOPWMDataMX Lib "DAQMX" (ByVal daqmx As Long, ByRef pMXAOPWMData As MXAOPWMData, ByRef pMXTransmit As MXTransmit) As Long

Public Declare Function setRESMX Lib "DAQMX" (ByVal daqmx As Long, ByVal iRangeRES As Long, ByVal startChNo As Long, ByVal endChNo As Long, ByVal spanMin As Long, ByVal spanMax As Long, ByVal scaleMin As Long, ByVal scaleMax As Long, ByVal scalePoint As Long) As Long

Public Declare Function setSTRAINMX Lib "DAQMX" (ByVal daqmx As Long, ByVal iRangeSTRAIN As Long, ByVal startChNo As Long, ByVal endChNo As Long, ByVal spanMin As Long, ByVal spanMax As Long, ByVal scaleMin As Long, ByVal scaleMax As Long, ByVal scalePoint As Long) As Long

Public Declare Function setAOMX Lib "DAQMX" (ByVal daqmx As Long, ByVal iRangeAO As Long, ByVal startChNo As Long, ByVal endChNo As Long, ByVal spanMin As Long, ByVal spanMax As Long) As Long

Public Declare Function setPWMMX Lib "DAQMX" (ByVal daqmx As Long, ByVal iRangePWM As Long, ByVal startChNo As Long, ByVal endChNo As Long, ByVal spanMin As Long, ByVal spanMax As Long) As Long

Public Declare Function setOutputTypeMX Lib "DAQMX" (ByVal daqmx As Long, ByVal iOutput As Long, ByVal startChNo As Long, ByVal endChNo As Long) As Long

Public Declare Function setChoiceMX Lib "DAQMX" (ByVal daqmx As Long, ByVal startChNo As Long, ByVal endChNo As Long, ByVal idleChoice As Long, ByVal errorChoice As Long, ByVal presetValue As Long) As Long

Public Declare Function setPulseTimeMX Lib "DAQMX" (ByVal daqmx As Long, ByVal pulseTime As Long, ByVal startChNo As Long, ByVal endChNo As Long) As Long

Public Declare Function setAOTypeMX Lib "DAQMX" (ByVal daqmx As Long, ByVal aoNo As Long, ByVal iKind As Long, ByVal refChNo As Long) As Long

Public Declare Function setPWMTypeMX Lib "DAQMX" (ByVal daqmx As Long, ByVal pwmNo As Long, ByVal iKind As Long, ByVal refChNo As Long) As Long

Public Declare Function getOutputMX Lib "DAQMX" (ByVal daqmx As Long, ByRef pMXOutputData As MXOutputData) As Long

Public Declare Function setOutputMX Lib "DAQMX" (ByVal daqmx As Long, ByRef pMXOutputData As MXOutputData) As Long

Public Declare Function changeAOPWMDataMX Lib "DAQMX" (ByRef pMXAOPWMData As MXAOPWMData, ByVal aopwmNo As Long, ByVal bValid As Long, ByVal iAOPWMValue As Long) As Long

Public Declare Function changeBalanceMX Lib "DAQMX" (ByRef pMXBalanceData As MXBalanceData, ByVal balanceNo As Long, ByVal bValid As Long, ByVal iValue As Long) As Long

Public Declare Function changeTransmitMX Lib "DAQMX" (ByRef pMXTransmit As MXTransmit, ByVal pwmNo As Long, ByVal iTrans As Long) As Long

Public Declare Function getMaxLenAlarmNameMX Lib "DAQMX" () As Long

Public Declare Function toAOPWMValueMX Lib "DAQMX" (ByVal realValue As Double, ByVal iRangeAOPWM As Long) As Long

Public Declare Function toRealValueMX Lib "DAQMX" (ByVal iAOPWMValue As Long, ByVal iRangeAOPWM As Long) As Double

Public Declare Function getItemErrorMX Lib "DAQMX" (ByVal daqmx As Long, ByRef itemErr As Long) As Long

Public Declare Function isDataNoVBMX Lib "DAQMX" (ByRef dataNo As MXDataNo) As Long

Public Declare Function toStyleVersionMX Lib "DAQMX" (ByVal style As Long) As Long

' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
' since R3.01
' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --

Public Declare Function setCOMMX Lib "DAQMX" (ByVal daqmx As Long, ByVal iRangeCOM As Long, ByVal startChNo As Long, ByVal endChNo As Long, ByVal spanMin As Long, ByVal spanMax As Long, ByVal scaleMin As Long, ByVal scaleMax As Long, ByVal scalePoint As Long) As Long

Public Declare Function setPULSEMX Lib "DAQMX" (ByVal daqmx As Long, ByVal iRangePULSE As Long, ByVal startChNo As Long, ByVal endChNo As Long, ByVal spanMin As Long, ByVal spanMax As Long, ByVal scaleMin As Long, ByVal scaleMax As Long, ByVal scalePoint As Long) As Long

Public Declare Function setChatFilterMX Lib "DAQMX" (ByVal daqmx As Long, ByVal bChatFilter As Long, ByVal startChNo As Long, ByVal endChNo As Long) As Long

' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
