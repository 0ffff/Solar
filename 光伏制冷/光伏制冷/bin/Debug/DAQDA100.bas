Attribute VB_Name = "DAQDA100"
' DAQDA100.bas
' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
'
' Copyright (c) 2004 Yokogawa Electric Corporation. All rights reserved.
'
' This is defined export DAQDA100.dll.
' Declare Functions for Visual Basic
'
' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
' 2004/11/01 Ver.2 Rev.1
' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --

' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
' @see DAQDARWIN
' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --

' communication
Public Const DAQDA100_COMMPORT = 34150

' total number
Public Const DAQDA100_NUMCHANNEL = 360
Public Const DAQDA100_NUMALARM = 4
Public Const DAQDA100_NUMUNIT = 6
Public Const DAQDA100_NUMSLOT = 6
Public Const DAQDA100_NUMTERM = 10

' string length without NULL
Public Const DAQDA100_MAXCHNAMELEN = 3
Public Const DAQDA100_MAXCHRANGLEN = 6
Public Const DAQDA100_MAXUNITLEN = 6
Public Const DAQDA100_MAXMODULELEN = 6
Public Const DAQDA100_MAXRELAYLEN = DAQDA100_MAXCHNAMELEN

' maximum value
Public Const DAQDA100_MAXDECIMALPOINT = 4

' valid
Public Const DAQDA100_VALID_OFF = 0
Public Const DAQDA100_VALID_ON = 1

' flag
Public Const DAQDA100_FLAG_OFF = &H0000
Public Const DAQDA100_FLAG_ENDDATA = &H0001

' data status
Public Const DAQDA100_DATA_UNKNWON = &H00000000
Public Const DAQDA100_DATA_NORMAL = &H00000001
Public Const DAQDA100_DATA_DIFFINPUT = &H00000002
Public Const DAQDA100_DATA_READER = &H00000003
Public Const DAQDA100_DATA_PLUSOVER = &H00007FFF
Public Const DAQDA100_DATA_MINUSOVER = &H00008001
Public Const DAQDA100_DATA_SKIP = &H00008002
Public Const DAQDA100_DATA_ILLEGAL = &H00008003
Public Const DAQDA100_DATA_ABNORMAL = &H00008004
Public Const DAQDA100_DATA_NODATA = &H00008005

' alarm type
Public Const DAQDA100_ALARM_NONE = 0
Public Const DAQDA100_ALARM_UPPER = 1
Public Const DAQDA100_ALARM_LOWER = 2
Public Const DAQDA100_ALARM_UPDIFF = 3
Public Const DAQDA100_ALARM_LOWDIFF = 4
Public Const DAQDA100_ALARM_INCRATE = 5
Public Const DAQDA100_ALARM_DECRATE = 6

' channel/relay type
Public Const DAQDA100_CHTYPE_MAINUNIT = -1
Public Const DAQDA100_CHTYPE_STANDALONE = 0
Public Const DAQDA100_CHTYPE_MATHTYPE = &H0080
Public Const DAQDA100_CHTYPE_SWITCH = &H0040
Public Const DAQDA100_CHTYPE_COMMDATA = &H0020
Public Const DAQDA100_CHTYPE_CONSTANT = &H0010
Public Const DAQDA100_CHTYPE_REPORT = &H0100

' mode
Public Const DAQDA100_MODE_OPE = 0
Public Const DAQDA100_MODE_SETUP = 1
Public Const DAQDA100_MODE_CALIB = 2

' talker output type
Public Const DAQDA100_TALK_MEASUREDDATA = 0
Public Const DAQDA100_TALK_OPEDATA = 1
Public Const DAQDA100_TALK_CHINFODATA = 2
Public Const DAQDA100_TALK_REPORTDATA = 4
Public Const DAQDA100_TALK_SYSINFODATA = 5
Public Const DAQDA100_TALK_CALIBDATA = 8
Public Const DAQDA100_TALK_SETUPDATA = 9

' status byte
Public Const DAQDA100_STATUS_OFF = &H0000
Public Const DAQDA100_STATUS_ADCONV = &H0001
Public Const DAQDA100_STATUS_SYNTAX = &H0002
Public Const DAQDA100_STATUS_TIMER = &H0004
Public Const DAQDA100_STATUS_MEDIA = &H0008
Public Const DAQDA100_STATUS_RELEASE = &H0020
Public Const DAQDA100_STATUS_SRQ = &H0040
Public Const DAQDA100_STATUS_ALL = &H00FF

' establish
Public Const DAQDA100_SETUP_ABORT = DAQDA100_VALID_OFF
Public Const DAQDA100_SETUP_STORE = DAQDA100_VALID_ON

' unit number
Public Const DAQDA100_UNITNO_MAINUNIT = DAQDA100_CHTYPE_MAINUNIT
Public Const DAQDA100_UNITNO_STANDALONE = DAQDA100_CHTYPE_STANDALONE

' computation
Public Const DAQDA100_COMPUTE_START = 0
Public Const DAQDA100_COMPUTE_STOP = 1
Public Const DAQDA100_COMPUTE_RESTART = 2
Public Const DAQDA100_COMPUTE_CLEAR = 3
Public Const DAQDA100_COMPUTE_RELEASE = 4

' reporting
Public Const DAQDA100_REPORT_RUN_START = 0
Public Const DAQDA100_REPORT_RUN_STOP = 1

' report type
Public Const DAQDA100_REPORT_HOURLY = 0
Public Const DAQDA100_REPORT_DAILY = 1
Public Const DAQDA100_REPORT_MONTHLY = 2
Public Const DAQDA100_REPORT_STATUS = 3

' report status
Public Const DAQDA100_REPSTATUS_NONE = &H0000
Public Const DAQDA100_REPSTATUS_HOURLY_NEW = &H0001
Public Const DAQDA100_REPSTATUS_HOURLY_VALID = &H0002
Public Const DAQDA100_REPSTATUS_DAILY_NEW = &H0004
Public Const DAQDA100_REPSTATUS_DAILY_VALID = &H0008
Public Const DAQDA100_REPSTATUS_MONTHLY_NEW = &H0010
Public Const DAQDA100_REPSTATUS_MONTHLY_VALID = &H0020

' wiring method
Public Const DAQDA100_WIRE_1PH2W = 1
Public Const DAQDA100_WIRE_1PH3W = 2
Public Const DAQDA100_WIRE_3PH3W2I = 3
Public Const DAQDA100_WIRE_3PH3W3I = 4
Public Const DAQDA100_WIRE_3PH4W = 5

' mesurement item
Public Const DAQDA100_POWERITEM_I0 = &H0000
Public Const DAQDA100_POWERITEM_I1 = &H0001
Public Const DAQDA100_POWERITEM_I2 = &H0002
Public Const DAQDA100_POWERITEM_I3 = &H0003
Public Const DAQDA100_POWERITEM_I13 = &H000D
Public Const DAQDA100_POWERITEM_P0 = &H0010
Public Const DAQDA100_POWERITEM_P1 = &H0011
Public Const DAQDA100_POWERITEM_P2 = &H0012
Public Const DAQDA100_POWERITEM_P3 = &H0013
Public Const DAQDA100_POWERITEM_P13 = &H001D
Public Const DAQDA100_POWERITEM_PF0 = &H0020
Public Const DAQDA100_POWERITEM_PF1 = &H0021
Public Const DAQDA100_POWERITEM_PF2 = &H0022
Public Const DAQDA100_POWERITEM_PF3 = &H0023
Public Const DAQDA100_POWERITEM_PF13 = &H002D
Public Const DAQDA100_POWERITEM_PH0 = &H0030
Public Const DAQDA100_POWERITEM_PH1 = &H0031
Public Const DAQDA100_POWERITEM_PH2 = &H0032
Public Const DAQDA100_POWERITEM_PH3 = &H0033
Public Const DAQDA100_POWERITEM_PH13 = &H003D
Public Const DAQDA100_POWERITEM_V0 = &H0040
Public Const DAQDA100_POWERITEM_V1 = &H0041
Public Const DAQDA100_POWERITEM_V2 = &H0042
Public Const DAQDA100_POWERITEM_V3 = &H0043
Public Const DAQDA100_POWERITEM_V13 = &H004D
Public Const DAQDA100_POWERITEM_VA0 = &H0050
Public Const DAQDA100_POWERITEM_VA1 = &H0051
Public Const DAQDA100_POWERITEM_VA2 = &H0052
Public Const DAQDA100_POWERITEM_VA3 = &H0053
Public Const DAQDA100_POWERITEM_VA13 = &H005D
Public Const DAQDA100_POWERITEM_VAR0 = &H0060
Public Const DAQDA100_POWERITEM_VAR1 = &H0061
Public Const DAQDA100_POWERITEM_VAR2 = &H0062
Public Const DAQDA100_POWERITEM_VAR3 = &H0063
Public Const DAQDA100_POWERITEM_VAR13 = &H006D
Public Const DAQDA100_POWERITEM_FREQ = &H007F

' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
' DA100 specifications
' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --

' number
Public Const DAQDA100_NUMCH_BYUNIT = DAQDA100_NUMSLOT * DAQDA100_NUMTERM

' all
Public Const DAQDA100_CHTYPE_MEASALL = &H0F
Public Const DAQDA100_CHNO_ALL = -1
Public Const DAQDA100_LEVELNO_ALL = -1

' code
Public Const DAQDA100_CODE_BINARY = 0
Public Const DAQDA100_CODE_ASCII = 1

' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
' range
' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --

' range type
Public Const DAQDA100_RANGETYPE_VOLT = &H00000000
Public Const DAQDA100_RANGETYPE_DI = &H00010000
Public Const DAQDA100_RANGETYPE_TC = &H00020000
Public Const DAQDA100_RANGETYPE_RTD = &H00040000
Public Const DAQDA100_RANGETYPE_SKIP = &H00080000
Public Const DAQDA100_RANGETYPE_MA = &H00100000
Public Const DAQDA100_RANGETYPE_POWER = &H00200000
Public Const DAQDA100_RANGETYPE_STRAIN = &H00400000
Public Const DAQDA100_RANGETYPE_PULSE = &H00800000

' SKIP
Public Const DAQDA100_RANGE_SKIP = DAQDA100_RANGETYPE_SKIP

' VOLT
Public Const DAQDA100_RANGE_VOLT_20MV = DAQDA100_RANGETYPE_VOLT + 1
Public Const DAQDA100_RANGE_VOLT_60MV = DAQDA100_RANGETYPE_VOLT + 2
Public Const DAQDA100_RANGE_VOLT_200MV = DAQDA100_RANGETYPE_VOLT + 3
Public Const DAQDA100_RANGE_VOLT_2V = DAQDA100_RANGETYPE_VOLT + 4
Public Const DAQDA100_RANGE_VOLT_6V = DAQDA100_RANGETYPE_VOLT + 5
Public Const DAQDA100_RANGE_VOLT_20V = DAQDA100_RANGETYPE_VOLT + 6
Public Const DAQDA100_RANGE_VOLT_50V = DAQDA100_RANGETYPE_VOLT + 7

' TC
Public Const DAQDA100_RANGE_TC_R = DAQDA100_RANGETYPE_TC + 1
Public Const DAQDA100_RANGE_TC_S = DAQDA100_RANGETYPE_TC + 2
Public Const DAQDA100_RANGE_TC_B = DAQDA100_RANGETYPE_TC + 3
Public Const DAQDA100_RANGE_TC_K = DAQDA100_RANGETYPE_TC + 4
Public Const DAQDA100_RANGE_TC_E = DAQDA100_RANGETYPE_TC + 5
Public Const DAQDA100_RANGE_TC_J = DAQDA100_RANGETYPE_TC + 6
Public Const DAQDA100_RANGE_TC_T = DAQDA100_RANGETYPE_TC + 7
Public Const DAQDA100_RANGE_TC_N = DAQDA100_RANGETYPE_TC + 8
Public Const DAQDA100_RANGE_TC_W = DAQDA100_RANGETYPE_TC + 9
Public Const DAQDA100_RANGE_TC_L = DAQDA100_RANGETYPE_TC + 10
Public Const DAQDA100_RANGE_TC_U = DAQDA100_RANGETYPE_TC + 11
Public Const DAQDA100_RANGE_TC_KP = DAQDA100_RANGETYPE_TC + 12

' RTD
Public Const DAQDA100_RANGE_RTD_1MAPT = DAQDA100_RANGETYPE_RTD + 1
Public Const DAQDA100_RANGE_RTD_2MAPT = DAQDA100_RANGETYPE_RTD + 2
Public Const DAQDA100_RANGE_RTD_1MAJPT = DAQDA100_RANGETYPE_RTD + 3
Public Const DAQDA100_RANGE_RTD_2MAJPT = DAQDA100_RANGETYPE_RTD + 4
Public Const DAQDA100_RANGE_RTD_2MAPT50 = DAQDA100_RANGETYPE_RTD + 5
Public Const DAQDA100_RANGE_RTD_1MAPTH = DAQDA100_RANGETYPE_RTD + 6
Public Const DAQDA100_RANGE_RTD_2MAPTH = DAQDA100_RANGETYPE_RTD + 7
Public Const DAQDA100_RANGE_RTD_1MAJPTH = DAQDA100_RANGETYPE_RTD + 8
Public Const DAQDA100_RANGE_RTD_2MAJPTH = DAQDA100_RANGETYPE_RTD + 9
Public Const DAQDA100_RANGE_RTD_1MANIS = DAQDA100_RANGETYPE_RTD + 10
Public Const DAQDA100_RANGE_RTD_1MANID = DAQDA100_RANGETYPE_RTD + 11
Public Const DAQDA100_RANGE_RTD_1MANI120 = DAQDA100_RANGETYPE_RTD + 12
Public Const DAQDA100_RANGE_RTD_CU10GE = DAQDA100_RANGETYPE_RTD + 13
Public Const DAQDA100_RANGE_RTD_CU10LN = DAQDA100_RANGETYPE_RTD + 14
Public Const DAQDA100_RANGE_RTD_CU10WEED = DAQDA100_RANGETYPE_RTD + 15
Public Const DAQDA100_RANGE_RTD_CU10BAILEY = DAQDA100_RANGETYPE_RTD + 16
Public Const DAQDA100_RANGE_RTD_J263B = DAQDA100_RANGETYPE_RTD + 17

' DI
Public Const DAQDA100_RANGE_DI_LEVEL = DAQDA100_RANGETYPE_DI + 1
Public Const DAQDA100_RANGE_DI_CONTACT = DAQDA100_RANGETYPE_DI + 2

' mA
Public Const DAQDA100_RANGE_MA_20MA = DAQDA100_RANGETYPE_MA + 1

' POWER
Public Const DAQDA100_RANGE_POWER_25V05A = DAQDA100_RANGETYPE_POWER + 1
Public Const DAQDA100_RANGE_POWER_25V5A = DAQDA100_RANGETYPE_POWER + 2
Public Const DAQDA100_RANGE_POWER_250V05A = DAQDA100_RANGETYPE_POWER + 3
Public Const DAQDA100_RANGE_POWER_250V5A = DAQDA100_RANGETYPE_POWER + 4

' STRAIN
Public Const DAQDA100_RANGE_STRAIN_2K = DAQDA100_RANGETYPE_STRAIN + 1
Public Const DAQDA100_RANGE_STRAIN_20K = DAQDA100_RANGETYPE_STRAIN + 2
Public Const DAQDA100_RANGE_STRAIN_200K = DAQDA100_RANGETYPE_STRAIN + 3

' PULS
Public Const DAQDA100_RANGE_PULSE_RATE = DAQDA100_RANGETYPE_PULSE + 1
Public Const DAQDA100_RANGE_PULSE_GATE = DAQDA100_RANGETYPE_PULSE + 2

' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
' Connection
' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --

Public Declare Function openDA100 Lib "DAQDA100" (ByVal strAddress As String, ByRef errorCode As Long) As Long

Public Declare Function closeDA100 Lib "DAQDA100" (ByVal daqda100 As Long) As Long

Public Declare Function sendLineDA100 Lib "DAQDA100" (ByVal daqda100 As Long, ByVal strLine As String) As Long

Public Declare Function receiveLineDA100 Lib "DAQDA100" (ByVal daqda100 As Long, ByVal strLine As String, ByVal maxLine As Long, ByRef lenLine As Long) As Long

Public Declare Function receiveByteDA100 Lib "DAQDA100" (ByVal daqda100 As Long, ByRef byteData As Byte, ByVal maxData As Long, ByRef lenData As Long) As Long

Public Declare Function sendTriggerDA100 Lib "DAQDA100" (ByVal daqda100 As Long) As Long

Public Declare Function updateStatusDA100 Lib "DAQDA100" (ByVal daqda100 As Long) As Long

Public Declare Function runCommandDA100 Lib "DAQDA100" (ByVal daqda100 As Long, ByVal strCmd As String) As Long

' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
' Control
' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --

Public Declare Function switchModeDA100 Lib "DAQDA100" (ByVal daqda100 As Long, ByVal iMode As Long) As Long

Public Declare Function switchCodeDA100 Lib "DAQDA100" (ByVal daqda100 As Long, ByVal iCode As Long) As Long

Public Declare Function reconstructDA100 Lib "DAQDA100" (ByVal daqda100 As Long) As Long

Public Declare Function initSetValueDA100 Lib "DAQDA100" (ByVal daqda100 As Long) As Long

Public Declare Function ackAlarmDA100 Lib "DAQDA100" (ByVal daqda100 As Long) As Long

Public Declare Function setDateTimeNowDA100 Lib "DAQDA100" (ByVal daqda100 As Long) As Long

Public Declare Function switchComputeDA100 Lib "DAQDA100" (ByVal daqda100 As Long, ByVal iCode As Long) As Long

Public Declare Function switchReportDA100 Lib "DAQDA100" (ByVal daqda100 As Long, ByVal iReportRun As Long) As Long

Public Declare Function establishDA100 Lib "DAQDA100" (ByVal daqda100 As Long, ByVal iSetup As Long) As Long

' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
' Set on Operation Mode
' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --

Public Declare Function setRangeDA100 Lib "DAQDA100" (ByVal daqda100 As Long, ByVal chType As Long, ByVal chNo As Long, ByVal iRange As Long, ByVal spanMin As Long, ByVal spanMax As Long, ByVal scaleMin As Long, ByVal scaleMax As Long, ByVal scalePoint As Long, ByVal bFilter As Long, ByVal iItem As Long, ByVal iWire As Long) As Long

Public Declare Function setChDELTADA100 Lib "DAQDA100" (ByVal daqda100 As Long, ByVal chType As Long, ByVal chNo As Long, ByVal refChNo As Long, ByVal spanMin As Long, ByVal spanMax As Long) As Long

Public Declare Function setChRRJCDA100 Lib "DAQDA100" (ByVal daqda100 As Long, ByVal chType As Long, ByVal chNo As Long, ByVal refChNo As Long, ByVal spanMin As Long, ByVal spanMax As Long) As Long

Public Declare Function setChUnitDA100 Lib "DAQDA100" (ByVal daqda100 As Long, ByVal chType As Long, ByVal chNo As Long, ByVal strUnit As String) As Long

Public Declare Function setChAlarmDA100 Lib "DAQDA100" (ByVal daqda100 As Long, ByVal chType As Long, ByVal chNo As Long, ByVal levelNo As Long, ByVal iAlarmType As Long, ByVal value As Long, ByVal relayType As Long, ByVal relayNo As Long) As Long

' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
' Measurement
' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --

Public Declare Function measInstChDA100 Lib "DAQDA100" (ByVal daqda100 As Long, ByVal chType As Long, ByVal chNo As Long) As Long

Public Declare Function mathInstChDA100 Lib "DAQDA100" (ByVal daqda100 As Long, ByVal chNo As Long) As Long

Public Declare Function measInfoChDA100 Lib "DAQDA100" (ByVal daqda100 As Long, ByVal chType As Long, ByVal chNo As Long) As Long

Public Declare Function mathInfoChDA100 Lib "DAQDA100" (ByVal daqda100 As Long, ByVal chNo As Long) As Long

Public Declare Function updateSystemConfigDA100 Lib "DAQDA100" (ByVal daqda100 As Long) As Long

Public Declare Function updateReportStatusDA100 Lib "DAQDA100" (ByVal daqda100 As Long) As Long

' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
' Talker
' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --

Public Declare Function talkOperationChDataDA100 Lib "DAQDA100" (ByVal daqda100 As Long, ByVal chType As Long, ByVal chNo As Long) As Long

Public Declare Function talkOperationDataDA100 Lib "DAQDA100" (ByVal daqda100 As Long, ByVal startChType As Long, ByVal startChNo As Long, ByVal endChType As Long, ByVal endChNo As Long) As Long

Public Declare Function talkSetupChDataDA100 Lib "DAQDA100" (ByVal daqda100 As Long, ByVal chType As Long, ByVal chNo As Long) As Long

Public Declare Function talkSetupDataDA100 Lib "DAQDA100" (ByVal daqda100 As Long, ByVal startChType As Long, ByVal startChNo As Long, ByVal endChType As Long, ByVal endChNo As Long) As Long

Public Declare Function talkCalibrationChDataDA100 Lib "DAQDA100" (ByVal daqda100 As Long, ByVal chType As Long, ByVal chNo As Long) As Long

Public Declare Function talkCalibrationDataDA100 Lib "DAQDA100" (ByVal daqda100 As Long, ByVal startChType As Long, ByVal startChNo As Long, ByVal endChType As Long, ByVal endChNo As Long) As Long

Public Declare Function getSetDataByLineDA100 Lib "DAQDA100" (ByVal daqda100 As Long, ByVal strLine As String, ByVal maxLine As Long, ByRef lenLine As Long, ByRef pFlag As Long) As Long

' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
' Get Data
' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --

Public Declare Function dataValueDA100 Lib "DAQDA100" (ByVal daqda100 As Long, ByVal chType As Long, ByVal chNo As Long) As Long

Public Declare Function dataStatusDA100 Lib "DAQDA100" (ByVal daqda100 As Long, ByVal chType As Long, ByVal chNo As Long) As Long

Public Declare Function dataAlarmDA100 Lib "DAQDA100" (ByVal daqda100 As Long, ByVal chType As Long, ByVal chNo As Long, ByVal levelNo As Long) As Long

Public Declare Function dataDoubleValueDA100 Lib "DAQDA100" (ByVal daqda100 As Long, ByVal chType As Long, ByVal chNo As Long) As Double

Public Declare Function dataStringValueDA100 Lib "DAQDA100" (ByVal daqda100 As Long, ByVal chType As Long, ByVal chNo As Long, ByVal strValue As String, ByVal lenValue As Long) As Long

Public Declare Function dataYearDA100 Lib "DAQDA100" (ByVal daqda100 As Long, ByVal chType As Long, ByVal chNo As Long) As Long

Public Declare Function dataMonthDA100 Lib "DAQDA100" (ByVal daqda100 As Long, ByVal chType As Long, ByVal chNo As Long) As Long

Public Declare Function dataDayDA100 Lib "DAQDA100" (ByVal daqda100 As Long, ByVal chType As Long, ByVal chNo As Long) As Long

Public Declare Function dataHourDA100 Lib "DAQDA100" (ByVal daqda100 As Long, ByVal chType As Long, ByVal chNo As Long) As Long

Public Declare Function dataMinuteDA100 Lib "DAQDA100" (ByVal daqda100 As Long, ByVal chType As Long, ByVal chNo As Long) As Long

Public Declare Function dataSecondDA100 Lib "DAQDA100" (ByVal daqda100 As Long, ByVal chType As Long, ByVal chNo As Long) As Long

Public Declare Function alarmTypeDA100 Lib "DAQDA100" (ByVal daqda100 As Long, ByVal chType As Long, ByVal chNo As Long, ByVal levelNo As Long) As Long

' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
' Get Channel Information
' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --

Public Declare Function channelPointDA100 Lib "DAQDA100" (ByVal daqda100 As Long, ByVal chType As Long, ByVal chNo As Long) As Long

Public Declare Function channelStatusDA100 Lib "DAQDA100" (ByVal daqda100 As Long, ByVal chType As Long, ByVal chNo As Long) As Long

Public Declare Function toChannelUnitDA100 Lib "DAQDA100" (ByVal daqda100 As Long, ByVal chType As Long, ByVal chNo As Long, ByVal strUnit As String, ByVal lenUnit As Long) As Long

' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
' Get System Information
' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --

Public Declare Function unitIntervalDA100 Lib "DAQDA100" (ByVal daqda100 As Long) As Double

Public Declare Function unitValidDA100 Lib "DAQDA100" (ByVal daqda100 As Long, ByVal unitNo As Long) As Long

Public Declare Function moduleCodeDA100 Lib "DAQDA100" (ByVal daqda100 As Long, ByVal unitNo As Long, ByVal slotNo As Long) As Long

Public Declare Function toModuleNameDA100 Lib "DAQDA100" (ByVal daqda100 As Long, ByVal unitNo As Long, ByVal slotNo As Long, ByVal strName As String, ByVal lenName As Long) As Long

' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
' Get Status
' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --

Public Declare Function statusByteDA100 Lib "DAQDA100" (ByVal daqda100 As Long) As Long

Public Declare Function statusCodeDA100 Lib "DAQDA100" (ByVal daqda100 As Long) As Long

Public Declare Function statusReportDA100 Lib "DAQDA100" (ByVal daqda100 As Long) As Long

' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
' Utility
' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --

Public Declare Function toDoubleValueDA100 Lib "DAQDA100" (ByVal dataValue As Long, ByVal point As Long) As Double

Public Declare Function toStringValueDA100 Lib "DAQDA100" (ByVal dataValue As Long, ByVal point As Long, ByVal strValue As String, ByVal lenValue As Long) As Long

Public Declare Function toAlarmNameDA100 Lib "DAQDA100" (ByVal iAlarmType As Long, ByVal strAlarm As String, ByVal lenAlarm As Long) As Long

Public Declare Function alarmMaxLengthDA100 Lib "DAQDA100" () As Long

Public Declare Function versionAPIDA100 Lib "DAQDA100" () As Long

Public Declare Function revisionAPIDA100 Lib "DAQDA100" () As Long

Public Declare Function toErrorMessageDA100 Lib "DAQDA100" (ByVal errCode As Long, ByVal errStr As String, ByVal errLen As Long) As Long

Public Declare Function errorMaxLengthDA100 Lib "DAQDA100" () As Long

' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
