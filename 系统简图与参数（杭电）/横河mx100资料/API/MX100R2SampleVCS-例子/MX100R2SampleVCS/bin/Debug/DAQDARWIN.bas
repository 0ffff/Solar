Attribute VB_Name = "DAQDARWIN"
' DAQDARWIN.bas
' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
'
' Copyright (c) 2003-2004 Yokogawa Electric Corporation. All rights reserved.
'
' This is defined export DAQDARWIN.dll.
' Declare Functions for Visual Basic
'
' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
' 2004/11/01 Ver.2 rev.1
' 2003/05/30 Ver.1 rev.1
' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --

' communication
Public Const DAQDARWIN_COMMPORT = 34150

' total number
Public Const DAQDARWIN_NUMCHANNEL = 360
Public Const DAQDARWIN_NUMALARM = 4
Public Const DAQDARWIN_NUMUNIT = 6
Public Const DAQDARWIN_NUMSLOT = 6
Public Const DAQDARWIN_NUMTERM = 10

' string length without NULL
Public Const DAQDARWIN_MAXCHNAMELEN = 3
Public Const DAQDARWIN_MAXCHRANGLEN = 6
Public Const DAQDARWIN_MAXUNITLEN = 6
Public Const DAQDARWIN_MAXMODULELEN = 6
Public Const DAQDARWIN_MAXRELAYLEN = DAQDARWIN_MAXCHNAMELEN

' maximum value
Public Const DAQDARWIN_MAXDECIMALPOINT = 4

' string
Public Const DAQDARWIN_TERMINATE = "\r\n"

' valid
Public Const DAQDARWIN_VALID_OFF = 0
Public Const DAQDARWIN_VALID_ON = 1

' flag
Public Const DAQDARWIN_FLAG_OFF = &H0000
Public Const DAQDARWIN_FLAG_ENDDATA = &H0001

' data status
Public Const DAQDARWIN_DATA_UNKNWON = &H00000000
Public Const DAQDARWIN_DATA_NORMAL = &H00000001
Public Const DAQDARWIN_DATA_DIFFINPUT = &H00000002
Public Const DAQDARWIN_DATA_READER = &H00000003
Public Const DAQDARWIN_DATA_PLUSOVER = &H00007FFF
Public Const DAQDARWIN_DATA_MINUSOVER = &H00008001
Public Const DAQDARWIN_DATA_SKIP = &H00008002
Public Const DAQDARWIN_DATA_ILLEGAL = &H00008003
Public Const DAQDARWIN_DATA_ABNORMAL = &H00008004
Public Const DAQDARWIN_DATA_NODATA = &H00008005

' alarm type
Public Const DAQDARWIN_ALARM_NONE = 0
Public Const DAQDARWIN_ALARM_UPPER = 1
Public Const DAQDARWIN_ALARM_LOWER = 2
Public Const DAQDARWIN_ALARM_UPDIFF = 3
Public Const DAQDARWIN_ALARM_LOWDIFF = 4
Public Const DAQDARWIN_ALARM_INCRATE = 5
Public Const DAQDARWIN_ALARM_DECRATE = 6

' system control
Public Const DAQDARWIN_SYSTEM_RECONSTRUCT = 1
Public Const DAQDARWIN_SYSTEM_INITOPE = 2
Public Const DAQDARWIN_SYSTEM_RESETALARM = 3

' channel/relay type
Public Const DAQDARWIN_CHTYPE_MAINUNIT = -1
Public Const DAQDARWIN_CHTYPE_STANDALONE = 0
Public Const DAQDARWIN_CHTYPE_MATHTYPE = &H0080
Public Const DAQDARWIN_CHTYPE_SWITCH = &H0040
Public Const DAQDARWIN_CHTYPE_COMMDATA = &H0020
Public Const DAQDARWIN_CHTYPE_CONSTANT = &H0010
Public Const DAQDARWIN_CHTYPE_REPORT = &H0100

' specified channel/relay type
Public Const I = DAQDARWIN_CHTYPE_MAINUNIT
Public Const A = DAQDARWIN_CHTYPE_MATHTYPE
Public Const S = DAQDARWIN_CHTYPE_SWITCH
Public Const C = DAQDARWIN_CHTYPE_COMMDATA
Public Const K = DAQDARWIN_CHTYPE_CONSTANT
Public Const R = DAQDARWIN_CHTYPE_REPORT

' mode
Public Const DAQDARWIN_MODE_OPE = 0
Public Const DAQDARWIN_MODE_SETUP = 1
Public Const DAQDARWIN_MODE_CALIB = 2

' talker output type
Public Const DAQDARWIN_TALK_MEASUREDDATA = 0
Public Const DAQDARWIN_TALK_OPEDATA = 1
Public Const DAQDARWIN_TALK_CHINFODATA = 2
Public Const DAQDARWIN_TALK_REPORTDATA = 4
Public Const DAQDARWIN_TALK_SYSINFODATA = 5
Public Const DAQDARWIN_TALK_CALIBDATA = 8
Public Const DAQDARWIN_TALK_SETUPDATA = 9

' status byte
Public Const DAQDARWIN_STATUS_OFF = &H0000
Public Const DAQDARWIN_STATUS_ADCONV = &H0001
Public Const DAQDARWIN_STATUS_SYNTAX = &H0002
Public Const DAQDARWIN_STATUS_TIMER = &H0004
Public Const DAQDARWIN_STATUS_MEDIA = &H0008
Public Const DAQDARWIN_STATUS_RELEASE = &H0020
Public Const DAQDARWIN_STATUS_SRQ = &H0040
Public Const DAQDARWIN_STATUS_ALL = &H00FF

' establish
Public Const DAQDARWIN_SETUP_ABORT = DAQDARWIN_VALID_OFF
Public Const DAQDARWIN_SETUP_STORE = DAQDARWIN_VALID_ON

' unit number
Public Const DAQDARWIN_UNITNO_MAINUNIT = DAQDARWIN_CHTYPE_MAINUNIT
Public Const DAQDARWIN_UNITNO_STANDALONE = DAQDARWIN_CHTYPE_STANDALONE

' computation
Public Const DAQDARWIN_COMPUTE_START = 0
Public Const DAQDARWIN_COMPUTE_STOP = 1
Public Const DAQDARWIN_COMPUTE_RESTART = 2
Public Const DAQDARWIN_COMPUTE_CLEAR = 3
Public Const DAQDARWIN_COMPUTE_RELEASE = 4

' reporting
Public Const DAQDARWIN_REPORT_RUN_START = 0
Public Const DAQDARWIN_REPORT_RUN_STOP = 1

' report type
Public Const DAQDARWIN_REPORT_HOURLY = 0
Public Const DAQDARWIN_REPORT_DAILY = 1
Public Const DAQDARWIN_REPORT_MONTHLY = 2
Public Const DAQDARWIN_REPORT_STATUS = 3

' report status
Public Const DAQDARWIN_REPSTATUS_NONE = &H0000
Public Const DAQDARWIN_REPSTATUS_HOURLY_NEW = &H0001
Public Const DAQDARWIN_REPSTATUS_HOURLY_VALID = &H0002
Public Const DAQDARWIN_REPSTATUS_DAILY_NEW = &H0004
Public Const DAQDARWIN_REPSTATUS_DAILY_VALID = &H0008
Public Const DAQDARWIN_REPSTATUS_MONTHLY_NEW = &H0010
Public Const DAQDARWIN_REPSTATUS_MONTHLY_VALID = &H0020

' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
' range
' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --

' VOLT
Public Const DAQDARWIN_RANGE_VOLT_20MV = 1
Public Const DAQDARWIN_RANGE_VOLT_60MV = 2
Public Const DAQDARWIN_RANGE_VOLT_200MV = 3
Public Const DAQDARWIN_RANGE_VOLT_2V = 4
Public Const DAQDARWIN_RANGE_VOLT_6V = 5
Public Const DAQDARWIN_RANGE_VOLT_20V = 6
Public Const DAQDARWIN_RANGE_VOLT_50V = 7

' TC
Public Const DAQDARWIN_RANGE_TC_R = 1
Public Const DAQDARWIN_RANGE_TC_S = 2
Public Const DAQDARWIN_RANGE_TC_B = 3
Public Const DAQDARWIN_RANGE_TC_K = 4
Public Const DAQDARWIN_RANGE_TC_E = 5
Public Const DAQDARWIN_RANGE_TC_J = 6
Public Const DAQDARWIN_RANGE_TC_T = 7
Public Const DAQDARWIN_RANGE_TC_N = 8
Public Const DAQDARWIN_RANGE_TC_W = 9
Public Const DAQDARWIN_RANGE_TC_L = 10
Public Const DAQDARWIN_RANGE_TC_U = 11
Public Const DAQDARWIN_RANGE_TC_KP = 12

' RTD
Public Const DAQDARWIN_RANGE_RTD_1MAPT = 1
Public Const DAQDARWIN_RANGE_RTD_2MAPT = 2
Public Const DAQDARWIN_RANGE_RTD_1MAJPT = 3
Public Const DAQDARWIN_RANGE_RTD_2MAJPT = 4
Public Const DAQDARWIN_RANGE_RTD_2MAPT50 = 5
Public Const DAQDARWIN_RANGE_RTD_1MAPTH = 6
Public Const DAQDARWIN_RANGE_RTD_2MAPTH = 7
Public Const DAQDARWIN_RANGE_RTD_1MAJPTH = 8
Public Const DAQDARWIN_RANGE_RTD_2MAJPTH = 9
Public Const DAQDARWIN_RANGE_RTD_1MANIS = 10
Public Const DAQDARWIN_RANGE_RTD_1MANID = 11
Public Const DAQDARWIN_RANGE_RTD_1MANI120 = 12
Public Const DAQDARWIN_RANGE_RTD_CU10GE = 13
Public Const DAQDARWIN_RANGE_RTD_CU10LN = 14
Public Const DAQDARWIN_RANGE_RTD_CU10WEED = 15
Public Const DAQDARWIN_RANGE_RTD_CU10BAILEY = 16
Public Const DAQDARWIN_RANGE_RTD_J263B = 17

' DI
Public Const DAQDARWIN_RANGE_DI_LEVEL = 1
Public Const DAQDARWIN_RANGE_DI_CONTACT = 2

' mA
Public Const DAQDARWIN_RANGE_MA_20MA = 1

' STRAIN
Public Const DAQDARWIN_RANGE_STRAIN_2K = 1
Public Const DAQDARWIN_RANGE_STRAIN_20K = 2
Public Const DAQDARWIN_RANGE_STRAIN_200K = 3

' PULSE
Public Const DAQDARWIN_RANGE_PULSE_RATE = 1
Public Const DAQDARWIN_RANGE_PULSE_GATE = 2

' POWER
Public Const DAQDARWIN_RANGE_POWER_25V05A = 1
Public Const DAQDARWIN_RANGE_POWER_25V5A = 2
Public Const DAQDARWIN_RANGE_POWER_250V05A = 3
Public Const DAQDARWIN_RANGE_POWER_250V5A = 4

' wiring method
Public Const DAQDARWIN_WIRE_1PH2W = 1
Public Const DAQDARWIN_WIRE_1PH3W = 2
Public Const DAQDARWIN_WIRE_3PH3W2I = 3
Public Const DAQDARWIN_WIRE_3PH3W3I = 4
Public Const DAQDARWIN_WIRE_3PH4W = 5

' mesurement item
Public Const DAQDARWIN_POWERITEM_I0 = &H0000
Public Const DAQDARWIN_POWERITEM_I1 = &H0001
Public Const DAQDARWIN_POWERITEM_I2 = &H0002
Public Const DAQDARWIN_POWERITEM_I3 = &H0003
Public Const DAQDARWIN_POWERITEM_I13 = &H000D
Public Const DAQDARWIN_POWERITEM_P0 = &H0010
Public Const DAQDARWIN_POWERITEM_P1 = &H0011
Public Const DAQDARWIN_POWERITEM_P2 = &H0012
Public Const DAQDARWIN_POWERITEM_P3 = &H0013
Public Const DAQDARWIN_POWERITEM_P13 = &H001D
Public Const DAQDARWIN_POWERITEM_PF0 = &H0020
Public Const DAQDARWIN_POWERITEM_PF1 = &H0021
Public Const DAQDARWIN_POWERITEM_PF2 = &H0022
Public Const DAQDARWIN_POWERITEM_PF3 = &H0023
Public Const DAQDARWIN_POWERITEM_PF13 = &H002D
Public Const DAQDARWIN_POWERITEM_PH0 = &H0030
Public Const DAQDARWIN_POWERITEM_PH1 = &H0031
Public Const DAQDARWIN_POWERITEM_PH2 = &H0032
Public Const DAQDARWIN_POWERITEM_PH3 = &H0033
Public Const DAQDARWIN_POWERITEM_PH13 = &H003D
Public Const DAQDARWIN_POWERITEM_V0 = &H0040
Public Const DAQDARWIN_POWERITEM_V1 = &H0041
Public Const DAQDARWIN_POWERITEM_V2 = &H0042
Public Const DAQDARWIN_POWERITEM_V3 = &H0043
Public Const DAQDARWIN_POWERITEM_V13 = &H004D
Public Const DAQDARWIN_POWERITEM_VA0 = &H0050
Public Const DAQDARWIN_POWERITEM_VA1 = &H0051
Public Const DAQDARWIN_POWERITEM_VA2 = &H0052
Public Const DAQDARWIN_POWERITEM_VA3 = &H0053
Public Const DAQDARWIN_POWERITEM_VA13 = &H005D
Public Const DAQDARWIN_POWERITEM_VAR0 = &H0060
Public Const DAQDARWIN_POWERITEM_VAR1 = &H0061
Public Const DAQDARWIN_POWERITEM_VAR2 = &H0062
Public Const DAQDARWIN_POWERITEM_VAR3 = &H0063
Public Const DAQDARWIN_POWERITEM_VAR13 = &H006D
Public Const DAQDARWIN_POWERITEM_FREQ = &H007F

' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
' structures
' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --

Public Type DarwinDateTime
        aYear           As Byte
        aMonth          As Byte
        aDay            As Byte
        aHour           As Byte
        aMinute         As Byte
        aSecond         As Byte
        aMilliSecond    As Integer
End Type
Public Type DarwinDataInfo
        aValue          As Long
        aStatus         As Long
        aAlarm(1 To 4)  As Long
End Type
Public Type DarwinChInfo
        aChNo           As Long
        aPoint          As Long
        aStatus         As Long
        aChType         As Long
        aUnit           As String * DAQDARWIN_MAXUNITLEN
        align(0 To 1)   As Byte
End Type
Public Type DarwinModuleInfo
        aSlotNo         As Long
        aInternalCode   As Long
        aName           As String * DAQDARWIN_MAXMODULELEN
        align(0 To 1)   As Byte
End Type
Public Type DarwinUnitInfo
        aExist          As Long
        aUnitNo         As Long
        aModule(0 To 5) As DarwinModuleInfo
End Type
Public Type DarwinSystemInfo
        aMainUnit               As DarwinUnitInfo
        aSubUnit(0 To 5)        As DarwinUnitInfo
End Type

' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
' Low level communications
' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --

Public Declare Function openDARWIN Lib "DAQDARWIN" (ByVal strAddress As String, ByRef errorCode As Long) As Long

Public Declare Function closeDARWIN Lib "DAQDARWIN" (ByVal daqdarwin As Long) As Long

Public Declare Function sendLineDARWIN Lib "DAQDARWIN" (ByVal daqdarwin As Long, ByVal strLine As String) As Long

Public Declare Function receiveLineDARWIN Lib "DAQDARWIN" (ByVal daqdarwin As Long, ByVal strLine As String, ByVal maxLine As Long, ByRef lenLine As Long) As Long

' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
' Middle level communications
' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --

Public Declare Function runCommandDARWIN Lib "DAQDARWIN" (ByVal daqdarwin As Long, ByVal strCmd As String) As Long

Public Declare Function getStatusByteDARWIN Lib "DAQDARWIN" (ByVal daqdarwin As Long, ByRef pStatusByte As Long) As Long

Public Declare Function sendTriggerDARWIN Lib "DAQDARWIN" (ByVal daqdarwin As Long) As Long

' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
' Date time commands
' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --

Public Declare Function setDateTimeDARWIN Lib "DAQDARWIN" (ByVal daqdarwin As Long, ByRef pDarwinDateTime As DarwinDateTime) As Long

Public Declare Function setDateTimeNowDARWIN Lib "DAQDARWIN" (ByVal daqdarwin As Long) As Long

' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
' Control commands
' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --

Public Declare Function transModeDARWIN Lib "DAQDARWIN" (ByVal daqdarwin As Long, ByVal iMode As Long) As Long

Public Declare Function initSystemDARWIN Lib "DAQDARWIN" (ByVal daqdarwin As Long, ByVal iCtrl As Long) As Long

' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
' Get system
' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --

Public Declare Function getSystemConfigDARWIN Lib "DAQDARWIN" (ByVal daqdarwin As Long, ByRef interval As Double, ByRef pDarwinSystemInfo As DarwinSystemInfo) As Long

' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
' Get channel information
' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --

Public Declare Function talkChInfoDARWIN Lib "DAQDARWIN" (ByVal daqdarwin As Long, ByVal startChType As Long, ByVal startChNo As Long, ByVal endChType As Long, ByVal endChNo As Long) As Long

Public Declare Function getChInfoDARWIN Lib "DAQDARWIN" (ByVal daqdarwin As Long, ByRef pDarwinChInfo As DarwinChInfo, ByRef pFlag As Long) As Long

' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
' Get measured data as ASCII
' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --

Public Declare Function talkDataByASCIIDARWIN Lib "DAQDARWIN" (ByVal daqdarwin As Long, ByVal startChType As Long, ByVal startChNo As Long, ByVal endChType As Long, ByVal endChNo As Long, ByRef pDarwinDateTime As DarwinDateTime) As Long

Public Declare Function getChDataByASCIIDARWIN Lib "DAQDARWIN" (ByVal daqdarwin As Long, ByRef pDarwinChInfo As DarwinChInfo, ByRef pDarwinDataInfo As DarwinDataInfo, ByRef pFlag As Long) As Long

' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
' Get measured data as binary
' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --

Public Declare Function talkDataByBinaryDARWIN Lib "DAQDARWIN" (ByVal daqdarwin As Long, ByVal startChType As Long, ByVal startChNo As Long, ByVal endChType As Long, ByVal endChNo As Long, ByRef pDarwinDateTime As DarwinDateTime) As Long

Public Declare Function getChDataByBinaryDARWIN Lib "DAQDARWIN" (ByVal daqdarwin As Long, ByRef pDarwinChInfo As DarwinChInfo, ByRef pDarwinDataInfo As DarwinDataInfo, ByRef pFlag As Long) As Long

' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
' Get configuration data by mode
' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --

Public Declare Function talkOperationDataDARWIN Lib "DAQDARWIN" (ByVal daqdarwin As Long, ByVal startChType As Long, ByVal startChNo As Long, ByVal endChType As Long, ByVal endChNo As Long) As Long

Public Declare Function talkSetupDataDARWIN Lib "DAQDARWIN" (ByVal daqdarwin As Long, ByVal startChType As Long, ByVal startChNo As Long, ByVal endChType As Long, ByVal endChNo As Long) As Long

Public Declare Function talkCalibrationDataDARWIN Lib "DAQDARWIN" (ByVal daqdarwin As Long, ByVal startChType As Long, ByVal startChNo As Long, ByVal endChType As Long, ByVal endChNo As Long) As Long

Public Declare Function getSetDataByLineDARWIN Lib "DAQDARWIN" (ByVal daqdarwin As Long, ByVal strLine As String, ByVal maxLine As Long, ByRef lenLine As Long, ByRef pFlag As Long) As Long

' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
' Set range
' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --

Public Declare Function setSKIPDARWIN Lib "DAQDARWIN" (ByVal daqdarwin As Long, ByVal chType As Long, ByVal startChNo As Long, ByVal endChNo As Long) As Long

Public Declare Function setVOLTDARWIN Lib "DAQDARWIN" (ByVal daqdarwin As Long, ByVal iRangeVOLT As Long, ByVal chType As Long, ByVal startChNo As Long, ByVal endChNo As Long, ByVal spanMin As Long, ByVal spanMax As Long, ByVal scaleMin As Long, ByVal scaleMax As Long, ByVal scalePoint As Long) As Long

Public Declare Function setTCDARWIN Lib "DAQDARWIN" (ByVal daqdarwin As Long, ByVal iRangeTC As Long, ByVal chType As Long, ByVal startChNo As Long, ByVal endChNo As Long, ByVal spanMin As Long, ByVal spanMax As Long, ByVal scaleMin As Long, ByVal scaleMax As Long, ByVal scalePoint As Long) As Long

Public Declare Function setRTDDARWIN Lib "DAQDARWIN" (ByVal daqdarwin As Long, ByVal iRangeRTD As Long, ByVal chType As Long, ByVal startChNo As Long, ByVal endChNo As Long, ByVal spanMin As Long, ByVal spanMax As Long, ByVal scaleMin As Long, ByVal scaleMax As Long, ByVal scalePoint As Long) As Long

Public Declare Function setDIDARWIN Lib "DAQDARWIN" (ByVal daqdarwin As Long, ByVal iRangeDI As Long, ByVal chType As Long, ByVal startChNo As Long, ByVal endChNo As Long, ByVal spanMin As Long, ByVal spanMax As Long, ByVal scaleMin As Long, ByVal scaleMax As Long, ByVal scalePoint As Long) As Long

Public Declare Function setDELTADARWIN Lib "DAQDARWIN" (ByVal daqdarwin As Long, ByVal refChNo As Long, ByVal chType As Long, ByVal startChNo As Long, ByVal endChNo As Long, ByVal spanMin As Long, ByVal spanMax As Long) As Long

Public Declare Function setRRJCDARWIN Lib "DAQDARWIN" (ByVal daqdarwin As Long, ByVal refChNo As Long, ByVal chType As Long, ByVal startChNo As Long, ByVal endChNo As Long, ByVal spanMin As Long, ByVal spanMax As Long) As Long

' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
' Scalling
' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --

Public Declare Function setScallingUnitDARWIN Lib "DAQDARWIN" (ByVal daqdarwin As Long, ByVal strUnit As String, ByVal chType As Long, ByVal startChNo As Long, ByVal endChNo As Long) As Long

' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
' Alarm
' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --

Public Declare Function setAlarmDARWIN Lib "DAQDARWIN" (ByVal daqdarwin As Long, ByVal levelNo As Long, ByVal chType As Long, ByVal startChNo As Long, ByVal endChNo As Long, ByVal iAlarmType As Long, ByVal value As Long, ByVal relayType As Long, ByVal relayNo As Long) As Long

' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
' Utilities
' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --

Public Declare Function toDoubleValueDARWIN Lib "DAQDARWIN" (ByVal dataValue As Long, ByVal point As Long) As Double

Public Declare Function toStringValueDARWIN Lib "DAQDARWIN" (ByVal dataValue As Long, ByVal point As Long, ByVal strValue As String, ByVal lenValue As Long) As Long

Public Declare Function toAlarmNameDARWIN Lib "DAQDARWIN" (ByVal iAlarmType As Long, ByVal strAlarm As String, ByVal lenAlarm As Long) As Long

' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
' Messages
' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --

Public Declare Function getVersionAPIDARWIN Lib "DAQDARWIN" () As Long

Public Declare Function getRevisionAPIDARWIN Lib "DAQDARWIN" () As Long

Public Declare Function toErrorMessageDARWIN Lib "DAQDARWIN" (ByVal errCode As Long, ByVal errStr As String, ByVal errLen As Long) As Long

Public Declare Function getMaxLenErrorMessageDARWIN Lib "DAQDARWIN" () As Long

' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
' Deprecated command
' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --

Public Declare Function setTimeOutDARWIN Lib "DAQDARWIN" (ByVal daqdarwin As Long, ByVal seconds As Long) As Long

' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
' Since R2.01
' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --

Public Declare Function setMADARWIN Lib "DAQDARWIN" (ByVal daqdarwin As Long, ByVal iRangeMA As Long, ByVal chType As Long, ByVal startChNo As Long, ByVal endChNo As Long, ByVal spanMin As Long, ByVal spanMax As Long, ByVal scaleMin As Long, ByVal scaleMax As Long, ByVal scalePoint As Long) As Long

Public Declare Function setSTRAINDARWIN Lib "DAQDARWIN" (ByVal daqdarwin As Long, ByVal iRangeSTRAIN As Long, ByVal chType As Long, ByVal startChNo As Long, ByVal endChNo As Long, ByVal spanMin As Long, ByVal spanMax As Long, ByVal scaleMin As Long, ByVal scaleMax As Long, ByVal scalePoint As Long) As Long

Public Declare Function setPULSEDARWIN Lib "DAQDARWIN" (ByVal daqdarwin As Long, ByVal iRangePULSE As Long, ByVal chType As Long, ByVal startChNo As Long, ByVal endChNo As Long, ByVal spanMin As Long, ByVal spanMax As Long, ByVal scaleMin As Long, ByVal scaleMax As Long, ByVal scalePoint As Long, ByVal bFilter As Long) As Long

Public Declare Function setPOWERDARWIN Lib "DAQDARWIN" (ByVal daqdarwin As Long, ByVal iRangePOWER As Long, ByVal chType As Long, ByVal chNo As Long, ByVal iItem As Long, ByVal iWire As Long, ByVal spanMin As Long, ByVal spanMax As Long, ByVal scaleMin As Long, ByVal scaleMax As Long, ByVal scalePoint As Long) As Long

Public Declare Function establishDARWIN Lib "DAQDARWIN" (ByVal daqdarwin As Long, ByVal iSetup As Long) As Long

Public Declare Function computeDARWIN Lib "DAQDARWIN" (ByVal daqdarwin As Long, ByVal iCompute As Long) As Long

Public Declare Function reportingDARWIN Lib "DAQDARWIN" (ByVal daqdarwin As Long, ByVal iReportRun As Long) As Long

Public Declare Function getReportStatusDARWIN Lib "DAQDARWIN" (ByVal daqdarwin As Long, ByRef pReportStatus As Long) As Long

Public Declare Function receiveByteDARWIN Lib "DAQDARWIN" (ByVal daqdarwin As Long, ByRef byteData As Byte, ByVal maxData As Long, ByRef lenData As Long) As Long

Public Declare Function getMaxLenAlarmNameDARWIN Lib "DAQDARWIN" () As Long

' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
