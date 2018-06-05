Attribute VB_Name = "DAQMX100"
' DAQMX100.bas
' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
'
' Copyright (c) 2004-2007 Yokogawa Electric Corporation. All rights reserved.
'
' This is defined export DAQMX100.dll.
' Declare Functions for Visual Basic
'
' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
' 2007/09/30 Ver.3 Rev.1
' 2004/11/01 Ver.2 Rev.1
' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --

' specified
Public Const DAQMX100_LIST_ALL = -1
Public Const DAQMX100_LIST_CURRENT = -1

' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
' @see DAQMX
' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --

' total number
Public Const DAQMX100_NUMMODULE = 6
Public Const DAQMX100_NUMCHANNEL = 60
Public Const DAQMX100_NUMDO = DAQMX100_NUMCHANNEL
Public Const DAQMX100_NUMFIFO = 3
Public Const DAQMX100_NUMALARM = 4
Public Const DAQMX100_NUMSEGMENT = 2
Public Const DAQMX100_NUMMACADDR = 6
Public Const DAQMX100_NUMAOPWM = DAQMX100_NUMCHANNEL
Public Const DAQMX100_NUMBALANCE = DAQMX100_NUMCHANNEL
Public Const DAQMX100_NUMOUTPUT = DAQMX100_NUMCHANNEL

' string length without NULL
Public Const DAQMX100_MAXHOSTNAMELEN = 64
Public Const DAQMX100_MAXUNITLEN = 6
Public Const DAQMX100_MAXTAGLEN = 15
Public Const DAQMX100_MAXCOMMENTLEN = 30
Public Const DAQMX100_MAXSERIALLEN = 9
Public Const DAQMX100_MAXPARTNOLEN = 7

' maximum number
Public Const DAQMX100_MAXDECIMALPOINT = 4
Public Const DAQMX100_MAXDISPTIME = 120000
Public Const DAQMX100_MAXPULSETIME = 30000

' constant value
Public Const DAQMX100_INSTANTANEOUS = -1
Public Const DAQMX100_REFCHNO_NONE = 0
Public Const DAQMX100_REFCHNO_ALL = -1
Public Const DAQMX100_LEVELNO_ALL = -1
Public Const DAQMX100_DONO_ALL = -1
Public Const DAQMX100_SEGMENTNO_ALL = -1
Public Const DAQMX100_CHNO_ALL = -1
Public Const DAQMX100_MODULENO_ALL = -1
Public Const DAQMX100_FIFONO_ALL = -1
Public Const DAQMX100_AOPWMNO_ALL = -1
Public Const DAQMX100_BALANCENO_ALL = -1
Public Const DAQMX100_OUTPUTNO_ALL = -1

' valid
Public Const DAQMX100_VALID_OFF = 0
Public Const DAQMX100_VALID_ON = 1

' flag : not use

' data status
Public Const DAQMX100_DATA_UNKNWON = &H00000000
Public Const DAQMX100_DATA_NORMAL = &H00000001
Public Const DAQMX100_DATA_PLUSOVER = &H00007FFF
Public Const DAQMX100_DATA_MINUSOVER = &H00008001
Public Const DAQMX100_DATA_SKIP = &H00008002
Public Const DAQMX100_DATA_ILLEGAL = &H00008003
Public Const DAQMX100_DATA_NODATA = &H00008005
Public Const DAQMX100_DATA_LACK = &H00008400
Public Const DAQMX100_DATA_INVALID = &H00008700

' alarm type
Public Const DAQMX100_ALARM_NONE = 0
Public Const DAQMX100_ALARM_UPPER = 1
Public Const DAQMX100_ALARM_LOWER = 2
Public Const DAQMX100_ALARM_UPDIFF = 3
Public Const DAQMX100_ALARM_LOWDIFF = 4

' system control : @see each command function

' channel kind
Public Const DAQMX100_CHKIND_NONE = &H0000
Public Const DAQMX100_CHKIND_AI = &H0010
Public Const DAQMX100_CHKIND_AIDIFF = &H0011
Public Const DAQMX100_CHKIND_AIRJC = &H0012
Public Const DAQMX100_CHKIND_DI = &H0030
Public Const DAQMX100_CHKIND_DIDIFF = &H0031
Public Const DAQMX100_CHKIND_DO = &H0040
Public Const DAQMX100_CHKIND_DOCOM = &H0041
Public Const DAQMX100_CHKIND_DOFAIL = &H0042
Public Const DAQMX100_CHKIND_DOERR = &H0043
Public Const DAQMX100_CHKIND_AO = &H0020
Public Const DAQMX100_CHKIND_AOCOM = &H0021
Public Const DAQMX100_CHKIND_PWM = &H0060
Public Const DAQMX100_CHKIND_PWMCOM = &H0061
Public Const DAQMX100_CHKIND_PI = &H0050
Public Const DAQMX100_CHKIND_PIDIFF = &H0051
Public Const DAQMX100_CHKIND_CI = &H0070
Public Const DAQMX100_CHKIND_CIDIFF = &H0071

' scale type
Public Const DAQMX100_SCALE_NONE = 0
Public Const DAQMX100_SCALE_LINER = 1

' module type
' 0xF0000010 -> -268435440
' 0xF0001C10 -> -268428272
' 0xB0101F10 -> -1341120752
' 0xD0001130 -> -805301968
' 0x0000FF00 -> 65280
Public Const DAQMX100_MODULE_NONE = &H0
Public Const DAQMX100_MODULE_MX110UNVH04 = -268435440
Public Const DAQMX100_MODULE_MX110UNVM10 = -268428272
Public Const DAQMX100_MODULE_MX115D05H10 = &H10003010
Public Const DAQMX100_MODULE_MX125MKCM10 = &H00402010
Public Const DAQMX100_MODULE_MX110V4RM06 = -1341120752
Public Const DAQMX100_MODULE_MX112NDIM04 = &H01004010
Public Const DAQMX100_MODULE_MX112B35M04 = &H01004110
Public Const DAQMX100_MODULE_MX112B12M04 = &H01004210
Public Const DAQMX100_MODULE_MX115D24H10 = &H10003210
Public Const DAQMX100_MODULE_MX120VAOM08 = &H0080C010
Public Const DAQMX100_MODULE_MX120PWMM08 = &H0020C810
Public Const DAQMX100_MODULE_HIDDEN = 65280
Public Const DAQMX100_MODULE_MX114PLSM10 = &H0400B010
Public Const DAQMX100_MODULE_MX110VTDL30 = -805301968
Public Const DAQMX100_MODULE_MX118CANM10 = &H00085110
Public Const DAQMX100_MODULE_MX118CANM20 = &H00085220
Public Const DAQMX100_MODULE_MX118CANM30 = &H00085330
Public Const DAQMX100_MODULE_MX118CANSUB = &H00085000
Public Const DAQMX100_MODULE_MX118CANMERR = &H00005A10
Public Const DAQMX100_MODULE_MX118CANSERR = &H00005B10

' how many channels by each module
Public Const DAQMX100_CHNUM_0 = 0
Public Const DAQMX100_CHNUM_4 = 4
Public Const DAQMX100_CHNUM_6 = 6
Public Const DAQMX100_CHNUM_8 = 8
Public Const DAQMX100_CHNUM_10 = 10
Public Const DAQMX100_CHNUM_30 = 30

' interval (msec)
Public Const DAQMX100_INTERVAL_10 = 10
Public Const DAQMX100_INTERVAL_50 = 50
Public Const DAQMX100_INTERVAL_100 = 100
Public Const DAQMX100_INTERVAL_200 = 200
Public Const DAQMX100_INTERVAL_500 = 500
Public Const DAQMX100_INTERVAL_1000 = 1000
Public Const DAQMX100_INTERVAL_2000 = 2000
Public Const DAQMX100_INTERVAL_5000 = 5000
Public Const DAQMX100_INTERVAL_10000 = 10000
Public Const DAQMX100_INTERVAL_20000 = 20000
Public Const DAQMX100_INTERVAL_30000 = 30000
Public Const DAQMX100_INTERVAL_60000 = 60000

' filter
Public Const DAQMX100_FILTER_0 = 0
Public Const DAQMX100_FILTER_5 = 1
Public Const DAQMX100_FILTER_10 = 2
Public Const DAQMX100_FILTER_20 = 3
Public Const DAQMX100_FILTER_25 = 4
Public Const DAQMX100_FILTER_40 = 5
Public Const DAQMX100_FILTER_50 = 6
Public Const DAQMX100_FILTER_100 = 7

' RJC Type
Public Const DAQMX100_RJC_INTERNAL = 0
Public Const DAQMX100_RJC_EXTERNAL = 1

' burnout
Public Const DAQMX100_BURNOUT_OFF = 0
Public Const DAQMX100_BURNOUT_UP = 1
Public Const DAQMX100_BURNOUT_DOWN = 2

' unit type
Public Const DAQMX100_UNITTYPE_NONE = &H00000000
Public Const DAQMX100_UNITTYPE_MX100 = &H00010000

' terminal type
Public Const DAQMX100_TERMINAL_SCREW = 0
Public Const DAQMX100_TERMINAL_CLAMP = 1
Public Const DAQMX100_TERMINAL_NDIS = 2

' AD
Public Const DAQMX100_INTEGRAL_AUTO = 0
Public Const DAQMX100_INTEGRAL_50HZ = 1
Public Const DAQMX100_INTEGRAL_60HZ = 2

' temparature unit
Public Const DAQMX100_TEMPUNIT_C = 0
Public Const DAQMX100_TEMPUNIT_F = 1

' CF write mode
Public Const DAQMX100_CFWRITEMODE_ONCE = 0
Public Const DAQMX100_CFWRITEMODE_FIFO = 1

' CF status
Public Const DAQMX100_CFSTATUS_NONE = &H0000
Public Const DAQMX100_CFSTATUS_EXIST = &H0001
Public Const DAQMX100_CFSTATUS_USE = &H0002
Public Const DAQMX100_CFSTATUS_FORMAT = &H0004

' UNIT status
Public Const DAQMX100_UNITSTAT_NONE = &H0000
Public Const DAQMX100_UNITSTAT_INIT = &H0001
Public Const DAQMX100_UNITSTAT_STOP = &H0002
Public Const DAQMX100_UNITSTAT_RUN = &H0003
Public Const DAQMX100_UNITSTAT_BACKUP = &H0004

' FIFO status
Public Const DAQMX100_FIFOSTAT_NONE = DAQMX100_UNITSTAT_NONE
Public Const DAQMX100_FIFOSTAT_INIT = DAQMX100_UNITSTAT_INIT
Public Const DAQMX100_FIFOSTAT_STOP = DAQMX100_UNITSTAT_STOP
Public Const DAQMX100_FIFOSTAT_RUN = DAQMX100_UNITSTAT_RUN
Public Const DAQMX100_FIFOSTAT_BACKUP = DAQMX100_UNITSTAT_BACKUP

' segment display type
Public Const DAQMX100_DISPTYPE_NONE = 0
Public Const DAQMX100_DISPTYPE_ON = 1
Public Const DAQMX100_DISPTYPE_BLINK = 2

' choice
Public Const DAQMX100_CHOICE_PREV = 0
Public Const DAQMX100_CHOICE_PRESET = 1

' transmit
Public Const DAQMX100_TRANSMIT_NONE = 0
Public Const DAQMX100_TRANSMIT_RUN = 1
Public Const DAQMX100_TRANSMIT_STOP = 2

' balance
Public Const DAQMX100_BALANCE_NONE = 0
Public Const DAQMX100_BALANCE_DONE = 1
Public Const DAQMX100_BALANCE_NG = 2
Public Const DAQMX100_BALANCE_ERROR = 3

' option
Public Const DAQMX100_OPTION_NONE = &H0000
Public Const DAQMX100_OPTION_DS = &H0001

' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
' range
' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --

' range type for special
Public Const DAQMX100_RANGETYPE_DI = &H00010000
Public Const DAQMX100_RANGETYPE_SKIP = &H00080000

' DI : special
Public Const DAQMX100_RANGE_DI_LEVEL = DAQMX100_RANGETYPE_DI + &H0001
Public Const DAQMX100_RANGE_DI_CONTACT = DAQMX100_RANGETYPE_DI + &H0002

' Skip
Public Const DAQMX100_RANGE_SKIP = DAQMX100_RANGETYPE_SKIP

' Reference
Public Const DAQMX100_RANGE_REFERENCE = -1

' Volt
Public Const DAQMX100_RANGE_VOLT_20MV = &H0000
Public Const DAQMX100_RANGE_VOLT_60MV = &H0001
Public Const DAQMX100_RANGE_VOLT_200MV = &H0002
Public Const DAQMX100_RANGE_VOLT_2V = &H0003
Public Const DAQMX100_RANGE_VOLT_6V = &H0004
Public Const DAQMX100_RANGE_VOLT_20V = &H0005
Public Const DAQMX100_RANGE_VOLT_100V = &H0006
Public Const DAQMX100_RANGE_VOLT_60MVH = &H0007
Public Const DAQMX100_RANGE_VOLT_1V = &H0008
Public Const DAQMX100_RANGE_VOLT_6VH = &H0009

' TC
Public Const DAQMX100_RANGE_TC_R = &H0200
Public Const DAQMX100_RANGE_TC_S = &H0201
Public Const DAQMX100_RANGE_TC_B = &H0202
Public Const DAQMX100_RANGE_TC_K = &H0203
Public Const DAQMX100_RANGE_TC_E = &H0204
Public Const DAQMX100_RANGE_TC_J = &H0205
Public Const DAQMX100_RANGE_TC_T = &H0206
Public Const DAQMX100_RANGE_TC_N = &H0207
Public Const DAQMX100_RANGE_TC_W = &H0208
Public Const DAQMX100_RANGE_TC_L = &H0209
Public Const DAQMX100_RANGE_TC_U = &H020A
Public Const DAQMX100_RANGE_TC_KP = &H020B
Public Const DAQMX100_RANGE_TC_PL = &H020C
Public Const DAQMX100_RANGE_TC_PR = &H020D
Public Const DAQMX100_RANGE_TC_NNM = &H020E
Public Const DAQMX100_RANGE_TC_WR = &H020F
Public Const DAQMX100_RANGE_TC_WWR = &H0210
Public Const DAQMX100_RANGE_TC_AWG = &H0211
Public Const DAQMX100_RANGE_TC_XK = &H0212

' RTD 1mA
Public Const DAQMX100_RANGE_RTD_1MAPT = &H0300
Public Const DAQMX100_RANGE_RTD_1MAJPT = &H0301
Public Const DAQMX100_RANGE_RTD_1MAPTH = &H0302
Public Const DAQMX100_RANGE_RTD_1MAJPTH = &H0303
Public Const DAQMX100_RANGE_RTD_1MANIS = &H0304
Public Const DAQMX100_RANGE_RTD_1MANID = &H0305
Public Const DAQMX100_RANGE_RTD_1MANI120 = &H0306
Public Const DAQMX100_RANGE_RTD_1MAPT50 = &H0307
Public Const DAQMX100_RANGE_RTD_1MACU10GE = &H0308
Public Const DAQMX100_RANGE_RTD_1MACU10LN = &H0309
Public Const DAQMX100_RANGE_RTD_1MACU10WEED = &H030A
Public Const DAQMX100_RANGE_RTD_1MACU10BAILEY = &H030B
Public Const DAQMX100_RANGE_RTD_1MAJ263B = &H030C
Public Const DAQMX100_RANGE_RTD_1MACU10A392 = &H030D
Public Const DAQMX100_RANGE_RTD_1MACU10A393 = &H030E
Public Const DAQMX100_RANGE_RTD_1MACU25 = &H030F
Public Const DAQMX100_RANGE_RTD_1MACU53 = &H0310
Public Const DAQMX100_RANGE_RTD_1MACU100 = &H0311
Public Const DAQMX100_RANGE_RTD_1MAPT25 = &H0312
Public Const DAQMX100_RANGE_RTD_1MACU10GEH = &H0313
Public Const DAQMX100_RANGE_RTD_1MACU10LNH = &H0314
Public Const DAQMX100_RANGE_RTD_1MACU10WEEDH = &H0315
Public Const DAQMX100_RANGE_RTD_1MACU10BAILEYH = &H0316
Public Const DAQMX100_RANGE_RTD_1MAPTN = &H0317
Public Const DAQMX100_RANGE_RTD_1MAJPTN = &H0318
Public Const DAQMX100_RANGE_RTD_1MAPTG = &H0319
Public Const DAQMX100_RANGE_RTD_1MACU100G = &H031A
Public Const DAQMX100_RANGE_RTD_1MACU50G = &H031B
Public Const DAQMX100_RANGE_RTD_1MACU10G = &H031C

' RTD 2mA
Public Const DAQMX100_RANGE_RTD_2MAPT = &H0400
Public Const DAQMX100_RANGE_RTD_2MAJPT = &H0401
Public Const DAQMX100_RANGE_RTD_2MAPTH = &H0402
Public Const DAQMX100_RANGE_RTD_2MAJPTH = &H0403
Public Const DAQMX100_RANGE_RTD_2MAPT50 = &H0404
Public Const DAQMX100_RANGE_RTD_2MACU10GE = &H0405
Public Const DAQMX100_RANGE_RTD_2MACU10LN = &H0406
Public Const DAQMX100_RANGE_RTD_2MACU10WEED = &H0407
Public Const DAQMX100_RANGE_RTD_2MACU10BAILEY = &H0408
Public Const DAQMX100_RANGE_RTD_2MAJ263B = &H0409
Public Const DAQMX100_RANGE_RTD_2MACU10A392 = &H040A
Public Const DAQMX100_RANGE_RTD_2MACU10A393 = &H040B
Public Const DAQMX100_RANGE_RTD_2MACU25 = &H040C
Public Const DAQMX100_RANGE_RTD_2MACU53 = &H040D
Public Const DAQMX100_RANGE_RTD_2MACU100 = &H040E
Public Const DAQMX100_RANGE_RTD_2MAPT25 = &H040F
Public Const DAQMX100_RANGE_RTD_2MACU10GEH = &H0410
Public Const DAQMX100_RANGE_RTD_2MACU10LNH = &H0411
Public Const DAQMX100_RANGE_RTD_2MACU10WEEDH = &H0412
Public Const DAQMX100_RANGE_RTD_2MACU10BAILEYH = &H0413
Public Const DAQMX100_RANGE_RTD_2MAPTN = &H0414
Public Const DAQMX100_RANGE_RTD_2MAJPTN = &H0415
Public Const DAQMX100_RANGE_RTD_2MACU100G = &H0416
Public Const DAQMX100_RANGE_RTD_2MACU50G = &H0417
Public Const DAQMX100_RANGE_RTD_2MACU10G = &H0418

' DI : detail
Public Const DAQMX100_RANGE_DI_LEVEL_AI = &H0100
Public Const DAQMX100_RANGE_DI_CONTACT_AI4 = &H0101
Public Const DAQMX100_RANGE_DI_CONTACT_AI10 = &H0102
Public Const DAQMX100_RANGE_DI_LEVEL_DI = &H0103
Public Const DAQMX100_RANGE_DI_CONTACT_DI = &H0104
Public Const DAQMX100_RANGE_DI_LEVEL_DI5V = DAQMX100_RANGE_DI_LEVEL_DI
Public Const DAQMX100_RANGE_DI_LEVEL_DI24V = &H0105
Public Const DAQMX100_RANGE_DI_CONTACT_AI30 = DAQMX100_RANGE_DI_CONTACT_AI10

' RTD 0.25mA
Public Const DAQMX100_RANGE_RTD_025MAPT500 = &H0500
Public Const DAQMX100_RANGE_RTD_025MAPT1K = &H0501

' RES
Public Const DAQMX100_RANGE_RES_20 = &H0600
Public Const DAQMX100_RANGE_RES_200 = &H0601
Public Const DAQMX100_RANGE_RES_2K = &H0602

' STR
Public Const DAQMX100_RANGE_STRAIN_2K = &H0700
Public Const DAQMX100_RANGE_STRAIN_20K = &H0701
Public Const DAQMX100_RANGE_STRAIN_200K = &H0702

' AO
Public Const DAQMX100_RANGE_AO_10V = &H1000
Public Const DAQMX100_RANGE_AO_20MA = &H1001

' PWM
Public Const DAQMX100_RANGE_PWM_1MS = &H1100
Public Const DAQMX100_RANGE_PWM_10MS = &H1101

' CAN
Public Const DAQMX100_RANGE_COM_CAN = &H0800

' PI
Public Const DAQMX100_RANGE_PI_LEVEL = &H0900
Public Const DAQMX100_RANGE_PI_CONTACT = &H0901

' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
' Output
Public Const DAQMX100_OUTPUT_NONE = 0
Public Const DAQMX100_OUTPUT_AO_10V = DAQMX100_RANGE_AO_10V
Public Const DAQMX100_OUTPUT_AO_20MA = DAQMX100_RANGE_AO_20MA
Public Const DAQMX100_OUTPUT_PWM_1MS = DAQMX100_RANGE_PWM_1MS
Public Const DAQMX100_OUTPUT_PWM_10MS = DAQMX100_RANGE_PWM_10MS

' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
' configure item @see DAQMXItems.bas
' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --

' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
' Connection
' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --

Public Declare Function openMX100 Lib "DAQMX100" (ByVal strAddress As String, ByRef errCode As Long) As Long

Public Declare Function closeMX100 Lib "DAQMX100" (ByVal daqmx100 As Long) As Long

' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
' FIFO
' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --

Public Declare Function measStartMX100 Lib "DAQMX100" (ByVal daqmx100 As Long) As Long

Public Declare Function measStopMX100 Lib "DAQMX100" (ByVal daqmx100 As Long) As Long

' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
' Control
' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --

Public Declare Function setDateTimeNowMX100 Lib "DAQMX100" (ByVal daqmx100 As Long) As Long

Public Declare Function switchBackupMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal bBackup As Long) As Long

Public Declare Function formatCFMX100 Lib "DAQMX100" (ByVal daqmx100 As Long) As Long

Public Declare Function reconstructMX100 Lib "DAQMX100" (ByVal daqmx100 As Long) As Long

Public Declare Function initSetValueMX100 Lib "DAQMX100" (ByVal daqmx100 As Long) As Long

Public Declare Function ackAlarmMX100 Lib "DAQMX100" (ByVal daqmx100 As Long) As Long

Public Declare Function displaySegmentMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal dispPattern0 As Long, ByVal dispPattern1 As Long, ByVal dispType As Long, ByVal dispTime As Long) As Long

Public Declare Function initDataChMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal chNo As Long) As Long

Public Declare Function initDataFIFOMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal fifoNo As Long) As Long

' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
' Setup
' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --

Public Declare Function sendConfigMX100 Lib "DAQMX100" (ByVal daqmx100 As Long) As Long

Public Declare Function initBalanceMX100 Lib "DAQMX100" (ByVal daqmx100 As Long) As Long

Public Declare Function clearBalanceMX100 Lib "DAQMX100" (ByVal daqmx100 As Long) As Long

' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
' Setting
' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --

Public Declare Function setRangeMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal chNo As Long, ByVal iRange As Long) As Long

Public Declare Function setChDELTAMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal chNo As Long, ByVal refChNo As Long, ByVal iRange As Long) As Long

Public Declare Function setChRRJCMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal chNo As Long, ByVal refChNo As Long) As Long

Public Declare Function setChUnitMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal chNo As Long, ByVal strUnit As String) As Long

Public Declare Function setChTagMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal chNo As Long, ByVal strTag As String) As Long

Public Declare Function setChCommentMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal chNo As Long, ByVal strComment As String) As Long

Public Declare Function setSpanMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal chNo As Long, ByVal spanMin As Long, ByVal spanMax As Long) As Long

Public Declare Function setDoubleSpanMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal chNo As Long, ByVal spanMin As Double, ByVal spanMax As Double) As Long

Public Declare Function setScaleMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal chNo As Long, ByVal scaleMin As Long, ByVal scaleMax As Long, ByVal scalePoint As Long) As Long

Public Declare Function setDoubleScaleMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal chNo As Long, ByVal scaleMin As Double, ByVal scaleMax As Double, ByVal scalePoint As Long) As Long

Public Declare Function setAlarmMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal chNo As Long, ByVal levelNo As Long, ByVal iAlarmType As Long, ByVal value As Long) As Long

Public Declare Function setDoubleAlarmMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal chNo As Long, ByVal levelNo As Long, ByVal iAlarmType As Long, ByVal value As Double) As Long

Public Declare Function setAlarmValueMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal chNo As Long, ByVal levelNo As Long, ByVal iAlarmType As Long, ByVal valueON As Long, ByVal valueOFF As Long) As Long

Public Declare Function setDoubleAlarmValueMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal chNo As Long, ByVal levelNo As Long, ByVal iAlarmType As Long, ByVal valueON As Double, ByVal valueOFF As Double) As Long

Public Declare Function setHisterisysMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal chNo As Long, ByVal levelNo As Long, ByVal histerisys As Long) As Long

Public Declare Function setDoubleHisterisysMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal chNo As Long, ByVal levelNo As Long, ByVal histerisys As Double) As Long

Public Declare Function setFilterMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal chNo As Long, ByVal iFilter As Long) As Long

Public Declare Function setRJCTypeMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal chNo As Long, ByVal iRJCType As Long, ByVal volt As Long) As Long

Public Declare Function setBurnoutMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal chNo As Long, ByVal iBurnout As Long) As Long

Public Declare Function setDeenergizeMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal doNo As Long, ByVal bDeenergize As Long) As Long

Public Declare Function setHoldMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal doNo As Long, ByVal bHold As Long) As Long

Public Declare Function setRefAlarmMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal doNo As Long, ByVal refChNo As Long, ByVal levelNo As Long, ByVal bValid As Long) As Long

Public Declare Function setChKindMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal chNo As Long, ByVal iKind As Long, ByVal refChNo As Long) As Long

Public Declare Function setChatFilterMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal chNo As Long, ByVal bChatFilter As Long) As Long

' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --

Public Declare Function setIntervalMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal moduleNo As Long, ByVal iInterval As Long) As Long

Public Declare Function setIntegralMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal moduleNo As Long, ByVal iIntegral As Long) As Long

Public Declare Function setUnitNoMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal unitNo As Long) As Long

Public Declare Function setUnitTempMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal iTempUnit As Long) As Long

Public Declare Function setCFWriteModeMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal iCFWriteMode As Long) As Long

Public Declare Function setOutputTypeMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal outputNo As Long, ByVal iOutput As Long) As Long

Public Declare Function setChoiceMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal outputNo As Long, ByVal idleChoice As Long, ByVal errorChoice As Long, ByVal presetValue As Long) As Long

Public Declare Function setDoubleChoiceMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal outputNo As Long, ByVal idleChoice As Long, ByVal errorChoice As Long, ByVal presetValue As Double) As Long

Public Declare Function setPulseTimeMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal outputNo As Long, ByVal pulseTime As Long) As Long

' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
' Data Operation
' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --

Public Declare Function createDOMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByRef errorCode As Long) As Long

Public Declare Function deleteDOMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal idDO As Long) As Long

Public Declare Function changeDOMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal idDO As Long, ByVal doNo As Long, ByVal bValid As Long, ByVal bONOFF As Long) As Long

Public Declare Function copyDOMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal idDO As Long, ByVal idDOSrc As Long) As Long

Public Declare Function commandDOMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal idDO As Long) As Long

Public Declare Function switchDOMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal idDO As Long, ByVal bONOFF As Long) As Long

' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --

Public Declare Function createAOPWMMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByRef errCode As Long) As Long

Public Declare Function  deleteAOPWMMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal idAOPWM As Long) As Long

Public Declare Function changeAOPWMMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal idAOPWM As Long, ByVal aopwmNo As Long, ByVal bValid As Long, ByVal iAOPWMValue As Long) As Long

Public Declare Function changeAOPWMValueMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal idAOPWM As Long, ByVal aopwmNo As Long, ByVal bValid As Long, ByVal realValue As Double) As Long

Public Declare Function copyAOPWMMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal idAOPWM As Long, ByVal idAOPWMSrc As Long) As Long

Public Declare Function commandAOPWMMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal idAOPWM As Long) As Long

' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --

Public Declare Function createBalanceMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByRef errCode As Long) As Long

Public Declare Function deleteBalanceMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal idBalance As Long) As Long

Public Declare Function changeBalanceMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal idBalance As Long, ByVal balanceNo As Long, ByVal bValid As Long, ByVal iValue As Long) As Long

Public Declare Function copyBalanceMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal idBalance As Long, ByVal idBalanceSrc As Long) As Long

Public Declare Function commandBalanceMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal idBalance As Long) As Long

' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --

Public Declare Function createTransmitMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByRef errCode As Long) As Long

Public Declare Function deleteTransmitMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal idTrans As Long) As Long

Public Declare Function changeTransmitMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal idTrans As Long, ByVal aopwmNo As Long, ByVal iTransmit As Long) As Long

Public Declare Function copyTransmitMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal idTrans As Long, ByVal idTransSrc As Long) As Long

Public Declare Function commandTransmitMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal idTrans As Long) As Long

Public Declare Function switchTransmitMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal idTrans As Long, ByVal iTransmit As Long) As Long

' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
' Update
' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --

Public Declare Function updateStatusMX100 Lib "DAQMX100" (ByVal daqmx100 As Long) As Long

Public Declare Function updateSystemMX100 Lib "DAQMX100" (ByVal daqmx100 As Long) As Long

Public Declare Function updateConfigMX100 Lib "DAQMX100" (ByVal daqmx100 As Long) As Long

Public Declare Function updateDODataMX100 Lib "DAQMX100" (ByVal daqmx100 As Long) As Long

Public Declare Function updateAOPWMDataMX100 Lib "DAQMX100" (ByVal daqmx100 As Long) As Long

Public Declare Function updateInfoChMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal chNo As Long) As Long

Public Declare Function updateBalanceMX100 Lib "DAQMX100" (ByVal daqmx100 As Long) As Long

Public Declare Function updateOutputMX100 Lib "DAQMX100" (ByVal daqmx100 As Long) As Long

' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
' Data Aquisition
' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --

Public Declare Function measDataChMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal chNo As Long) As Long

Public Declare Function measInstChMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal chNo As Long) As Long

Public Declare Function measDataFIFOMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal fifoNo As Long) As Long

Public Declare Function measInstFIFOMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal fifoNo As Long) As Long

' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
' Item
' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --

Public Declare Function getItemAllMX100 Lib "DAQMX100" (ByVal daqmx100 As Long) As Long

Public Declare Function setItemAllMX100 Lib "DAQMX100" (ByVal daqmx100 As Long) As Long

Public Declare Function readItemMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal itemNo As Long, ByVal strItem As String, ByVal lenItem As Long, ByRef realLen As Long) As Long

Public Declare Function writeItemMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal itemNo As Long, ByVal strItem As String) As Long

Public Declare Function initItemMX100 Lib "DAQMX100" (ByVal daqmx100 As Long) As Long

' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
' Get Current Measured Data
' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --

Public Declare Function dataValueMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal chNo As Long) As Long

Public Declare Function dataStatusMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal chNo As Long) As Long

Public Declare Function dataAlarmMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal chNo As Long, ByVal levelNo As Long) As Long

Public Declare Function dataDoubleValueMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal chNo As Long) As Double

Public Declare Function dataStringValueMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal chNo As Long, ByVal strValue As String, ByVal lenValue As Long) As Long

Public Declare Function dataTimeMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal chNo As Long) As Long

Public Declare Function dataMilliSecMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal chNo As Long) As Long

Public Declare Function dataYearMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal chNo As Long) As Long

Public Declare Function dataMonthMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal chNo As Long) As Long

Public Declare Function dataDayMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal chNo As Long) As Long

Public Declare Function dataHourMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal chNo As Long) As Long

Public Declare Function dataMinuteMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal chNo As Long) As Long

Public Declare Function dataSecondMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal chNo As Long) As Long

Public Declare Function dataValidMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal chNo As Long) As Long

' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
' Get Channel Information
' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --

Public Declare Function channelFIFONoMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal chNo As Long) As Long

Public Declare Function channelFIFOIndexMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal chNo As Long) As Long

Public Declare Function channelDisplayMinMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal chNo As Long) As Double

Public Declare Function channelDisplayMaxMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal chNo As Long) As Double

Public Declare Function channelRealMinMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal chNo As Long) As Double

Public Declare Function channelRealMaxMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal chNo As Long) As Double

' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
' Get Channel Configure
' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --

Public Declare Function channelValidMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal chNo As Long) As Long

Public Declare Function channelPointMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal chNo As Long) As Long

Public Declare Function channelKindMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal chNo As Long) As Long

Public Declare Function channelRangeMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal chNo As Long) As Long

Public Declare Function channelScaleTypeMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal chNo As Long) As Long

' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --

Public Declare Function toChannelUnitMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal chNo As Long, ByVal strUnit As String, ByVal lenUnit As Long) As Long

Public Declare Function toChannelTagMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal chNo As Long, ByVal strTag As String, ByVal lenTag As Long) As Long

Public Declare Function toChannelCommentMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal chNo As Long, ByVal strComment As String, ByVal lenComment As Long) As Long

Public Declare Function channelSpanMinMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal chNo As Long) As Long

Public Declare Function channelDoubleSpanMinMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal chNo As Long) As Double

Public Declare Function channelSpanMaxMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal chNo As Long) As Long

Public Declare Function channelDoubleSpanMaxMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal chNo As Long) As Double

Public Declare Function channelScaleMinMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal chNo As Long) As Long

Public Declare Function channelDoubleScaleMinMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal chNo As Long) As Double

Public Declare Function channelScaleMaxMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal chNo As Long) As Long

Public Declare Function channelDoubleScaleMaxMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal chNo As Long) As Double

' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --

Public Declare Function alarmTypeMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal chNo As Long, ByVal levelNo As Long) As Long

Public Declare Function alarmValueONMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal chNo As Long, ByVal levelNo As Long) As Long

Public Declare Function alarmDoubleValueONMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal chNo As Long, ByVal levelNo As Long) As Double

Public Declare Function alarmValueOFFMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal chNo As Long, ByVal levelNo As Long) As Long

Public Declare Function alarmDoubleValueOFFMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal chNo As Long, ByVal levelNo As Long) As Double

Public Declare Function alarmHisterisysMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal chNo As Long, ByVal levelNo As Long) As Long

Public Declare Function alarmDoubleHisterisysMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal chNo As Long, ByVal levelNo As Long) As Double

' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --

Public Declare Function channelFilterMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal chNo As Long) As Long

Public Declare Function channelRJCTypeMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal chNo As Long) As Long

Public Declare Function channelRJCVoltMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal chNo As Long) As Long

Public Declare Function channelBurnoutMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal chNo As Long) As Long

Public Declare Function channelDeenergizeMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal doNo As Long) As Long

Public Declare Function channelHoldMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal doNo As Long) As Long

Public Declare Function channelRefAlarmMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal doNo As Long, ByVal refChNo As Long, ByVal levelNo As Long) As Long

Public Declare Function channelRefChNoMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal chNo As Long) As Long

' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --

Public Declare Function channelBalanceValidMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal balanceNo As Long) As Long

Public Declare Function channelBalanceValueMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal balanceNo As Long) As Long

' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --

Public Declare Function channelOutputTypeMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal outputNo As Long) As Long

Public Declare Function channelIdleChoiceMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal outputNo As Long) As Long

Public Declare Function channelErrorChoiceMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal outputNo As Long) As Long

Public Declare Function channelPresetValueMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal outputNo As Long) As Long

Public Declare Function channelDoublePresetValueMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal outputNo As Long) As Double

Public Declare Function channelPulseTimeMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal outputNo As Long) As Long

' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --

Public Declare Function channelChatFilterMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal chNo As Long) As Long

' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
' Get Network Data
' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --

Public Declare Function toNetHostMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal strHost As String, ByVal lenHost As Long) As Long

Public Declare Function netAddressMX100 Lib "DAQMX100" (ByVal daqmx100 As Long) As Long

Public Declare Function netPortMX100 Lib "DAQMX100" (ByVal daqmx100 As Long) As Long

Public Declare Function netSubmaskMX100 Lib "DAQMX100" (ByVal daqmx100 As Long) As Long

Public Declare Function netGatewayMX100 Lib "DAQMX100" (ByVal daqmx100 As Long) As Long

' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
' Get System Data
' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --

Public Declare Function moduleTypeMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal moduleNo As Long) As Long

Public Declare Function moduleChNumMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal moduleNo As Long) As Long

Public Declare Function moduleIntervalMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal moduleNo As Long) As Long

Public Declare Function moduleIntegralMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal moduleNo As Long) As Long

Public Declare Function moduleValidMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal moduleNo As Long) As Long

Public Declare Function moduleStandbyTypeMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal moduleNo As Long) As Long

Public Declare Function moduleRealTypeMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal moduleNo As Long) As Long

Public Declare Function moduleTerminalMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal moduleNo As Long) As Long

Public Declare Function moduleVersionMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal moduleNo As Long) As Long

Public Declare Function moduleFIFONoMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal moduleNo As Long) As Long

Public Declare Function toModuleSerialMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal moduleNo As Long, ByVal strSerial As String, ByVal lenSerial As Long) As Long

' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --

Public Declare Function unitTypeMX100 Lib "DAQMX100" (ByVal daqmx100 As Long) As Long

Public Declare Function unitStyleMX100 Lib "DAQMX100" (ByVal daqmx100 As Long) As Long

Public Declare Function unitNoMX100 Lib "DAQMX100" (ByVal daqmx100 As Long) As Long

Public Declare Function unitTempMX100 Lib "DAQMX100" (ByVal daqmx100 As Long) As Long

Public Declare Function unitFrequencyMX100 Lib "DAQMX100" (ByVal daqmx100 As Long) As Long

Public Declare Function toUnitPartNoMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal strPartNo As String, ByVal lenPartNo As Long) As Long

Public Declare Function unitOptionMX100 Lib "DAQMX100" (ByVal daqmx100 As Long) As Long

Public Declare Function toUnitSerialMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal strSerial As String, ByVal lenSerial As Long) As Long

Public Declare Function unitMACMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal index As Long) As Long

Public Declare Function unitCFWriteModeMX100 Lib "DAQMX100" (ByVal daqmx100 As Long) As Long

' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
' Get Status Data
' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --

Public Declare Function statusUnitMX100 Lib "DAQMX100" (ByVal daqmx100 As Long) As Long

Public Declare Function statusFIFONumMX100 Lib "DAQMX100" (ByVal daqmx100 As Long) As Long

Public Declare Function statusBackupMX100 Lib "DAQMX100" (ByVal daqmx100 As Long) As Long

Public Declare Function statusFIFOMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal fifoNo As Long) As Long

Public Declare Function statusFIFOIntervalMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal fifoNo As Long) As Long

Public Declare Function statusCFMX100 Lib "DAQMX100" (ByVal daqmx100 As Long) As Long

Public Declare Function statusCFSizeMX100 Lib "DAQMX100" (ByVal daqmx100 As Long) As Long

Public Declare Function statusCFRemainMX100 Lib "DAQMX100" (ByVal daqmx100 As Long) As Long

Public Declare Function statusTimeMX100 Lib "DAQMX100" (ByVal daqmx100 As Long) As Long

Public Declare Function statusMilliSecMX100 Lib "DAQMX100" (ByVal daqmx100 As Long) As Long

Public Declare Function statusYearMX100 Lib "DAQMX100" (ByVal daqmx100 As Long) As Long

Public Declare Function statusMonthMX100 Lib "DAQMX100" (ByVal daqmx100 As Long) As Long

Public Declare Function statusDayMX100 Lib "DAQMX100" (ByVal daqmx100 As Long) As Long

Public Declare Function statusHourMX100 Lib "DAQMX100" (ByVal daqmx100 As Long) As Long

Public Declare Function statusMinuteMX100 Lib "DAQMX100" (ByVal daqmx100 As Long) As Long

Public Declare Function statusSecondMX100 Lib "DAQMX100" (ByVal daqmx100 As Long) As Long

' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
' Get Current Data
' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --

Public Declare Function currentDOValidMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal doNo As Long) As Long

Public Declare Function currentDOValueMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal doNo As Long) As Long

Public Declare Function currentAOPWMValidMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal aopwmNo As Long) As Long

Public Declare Function currentAOPWMValueMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal aopwmNo As Long) As Long

Public Declare Function currentDoubleAOPWMValueMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal aopwmNo As Long) As Double

Public Declare Function currentBalanceValidMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal balanceNo As Long) As Long

Public Declare Function currentBalanceValueMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal balanceNo As Long) As Long

Public Declare Function currentBalanceResultMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal balanceNo As Long) As Long

Public Declare Function currentTransmitMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal aopwmNo As Long) As Long

' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
' Get User Data
' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --

Public Declare Function userDOValidMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal idDO As Long, ByVal doNo As Long) As Long

Public Declare Function userDOValueMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal idDO As Long, ByVal doNo As Long) As Long

Public Declare Function userAOPWMValidMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal idAOPWM As Long, ByVal aopwmNo As Long) As Long

Public Declare Function userAOPWMValueMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal idAOPWM As Long, ByVal aopwmNo As Long) As Long

Public Declare Function userDoubleAOPWMValueMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal idAOPWM As Long, ByVal aopwmNo As Long) As Double

Public Declare Function userBalanceValidMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal idBalance As Long, ByVal balanceNo As Long) As Long

Public Declare Function userBalanceValueMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal idBalance As Long, ByVal balanceNo As Long) As Long

Public Declare Function userTransmitMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal idTrans As Long, ByVal aopwmNo As Long) As Long

' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
' Utility
' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --

Public Declare Function dataNumChMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal chNo As Long) As Long

Public Declare Function dataNumFIFOMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal fifoNo As Long) As Long

Public Declare Function lastErrorMX100 Lib "DAQMX100" (ByVal daqmx100 As Long) As Long

Public Declare Function toErrorMessageMX100 Lib "DAQMX100" (ByVal errCode As Long, ByVal errStr As String, ByVal errLen As Long) As Long

Public Declare Function errorMaxLengthMX100 Lib "DAQMX100" () As Long

Public Declare Function itemErrorMX100 Lib "DAQMX100" (ByVal daqmx100 As Long) As Long

Public Declare Function channelNumberMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal fifoNo As Long, ByVal fifioIndex As Long) As Long

Public Declare Function rangePointMX100 Lib "DAQMX100" (ByVal daqmx100 As Long, ByVal iRange As Long) As Long

Public Declare Function toDoubleValueMX100 Lib "DAQMX100" (ByVal dataValue As Long, ByVal point As Long) As Double

Public Declare Function toStringValueMX100 Lib "DAQMX100" (ByVal dataValue As Long, ByVal point As Long, ByVal strValue As String, ByVal lenValue As Long) As Long

Public Declare Function toAlarmNameMX100 Lib "DAQMX100" (ByVal iAlarmType As Long, ByVal strAlarm As String, ByVal lenAlarm As Long) As Long

Public Declare Function alarmMaxLengthMX100 Lib "DAQMX100" () As Long

Public Declare Function versionAPIMX100 Lib "DAQMX100" () As Long

Public Declare Function revisionAPIMX100 Lib "DAQMX100" () As Long

Public Declare Function addressPartMX100 Lib "DAQMX100" (ByVal address As Long, ByVal index As Long) As Long

Public Declare Function toAOPWMValueMX100 Lib "DAQMX100" (ByVal realValue As Double, ByVal iRangeAOPWM As Long) As Long

Public Declare Function toRealValueMX100 Lib "DAQMX100" (ByVal iAOPWMValue As Long, ByVal iRangeAOPWM As Long) As Double

Public Declare Function toItemNameMX100 Lib "DAQMX100" (ByVal itemNo As Long, ByVal strItem As String, ByVal lenItem As Long) As Long

Public Declare Function toItemNoMX100 Lib "DAQMX100" (ByVal strItem As String) As Long

Public Declare Function itemMaxLengthMX100 Lib "DAQMX100" () As Long

Public Declare Function toStyleVersionMX100 Lib "DAQMX100" (ByVal style As Long) As Long

' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
