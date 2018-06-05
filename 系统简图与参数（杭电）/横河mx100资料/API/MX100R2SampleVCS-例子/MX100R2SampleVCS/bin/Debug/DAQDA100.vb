Imports System.Runtime.InteropServices

' DAQDA100.vb
' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
'
' Copyright (c) 2004 Yokogawa Electric Corporation. All rights reserved.
'
' This is defined export DAQDA100.dll.
' Declare Functions for Visual Basic .NET
'
' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
' 2004/11/01 Ver.2 Rev.1
' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --

Module DAQDA100
    
    ' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
    ' @see DAQDARWIN
    ' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
    
    ' communication
    Public Const DAQDA100_COMMPORT As Integer = 34150
    
    ' total number
    Public Const DAQDA100_NUMCHANNEL As Integer = 360
    Public Const DAQDA100_NUMALARM As Integer = 4
    Public Const DAQDA100_NUMUNIT As Integer = 6
    Public Const DAQDA100_NUMSLOT As Integer = 6
    Public Const DAQDA100_NUMTERM As Integer = 10
    
    ' string length without NULL
    Public Const DAQDA100_MAXCHNAMELEN As Integer = 3
    Public Const DAQDA100_MAXCHRANGLEN As Integer = 6
    Public Const DAQDA100_MAXUNITLEN As Integer = 6
    Public Const DAQDA100_MAXMODULELEN As Integer = 6
    Public Const DAQDA100_MAXRELAYLEN As Integer = DAQDA100_MAXCHNAMELEN
    
    ' maximum value
    Public Const DAQDA100_MAXDECIMALPOINT As Integer = 4
    
    ' valid
    Public Const DAQDA100_VALID_OFF As Integer = 0
    Public Const DAQDA100_VALID_ON As Integer = 1
    
    ' flag
    Public Const DAQDA100_FLAG_OFF As Integer = &H0000
    Public Const DAQDA100_FLAG_ENDDATA As Integer = &H0001
    
    ' data status
    Public Const DAQDA100_DATA_UNKNWON As Integer = &H00000000
    Public Const DAQDA100_DATA_NORMAL As Integer = &H00000001
    Public Const DAQDA100_DATA_DIFFINPUT As Integer = &H00000002
    Public Const DAQDA100_DATA_READER As Integer = &H00000003
    Public Const DAQDA100_DATA_PLUSOVER As Integer = &H00007FFF
    Public Const DAQDA100_DATA_MINUSOVER As Integer = &H00008001
    Public Const DAQDA100_DATA_SKIP As Integer = &H00008002
    Public Const DAQDA100_DATA_ILLEGAL As Integer = &H00008003
    Public Const DAQDA100_DATA_ABNORMAL As Integer = &H00008004
    Public Const DAQDA100_DATA_NODATA As Integer = &H00008005
    
    ' alarm type
    Public Const DAQDA100_ALARM_NONE As Integer = 0
    Public Const DAQDA100_ALARM_UPPER As Integer = 1
    Public Const DAQDA100_ALARM_LOWER As Integer = 2
    Public Const DAQDA100_ALARM_UPDIFF As Integer = 3
    Public Const DAQDA100_ALARM_LOWDIFF As Integer = 4
    Public Const DAQDA100_ALARM_INCRATE As Integer = 5
    Public Const DAQDA100_ALARM_DECRATE As Integer = 6
    
    ' channel/relay type
    Public Const DAQDA100_CHTYPE_MAINUNIT As Integer = -1
    Public Const DAQDA100_CHTYPE_STANDALONE As Integer = 0
    Public Const DAQDA100_CHTYPE_MATHTYPE As Integer = &H0080
    Public Const DAQDA100_CHTYPE_SWITCH As Integer = &H0040
    Public Const DAQDA100_CHTYPE_COMMDATA As Integer = &H0020
    Public Const DAQDA100_CHTYPE_CONSTANT As Integer = &H0010
    Public Const DAQDA100_CHTYPE_REPORT As Integer = &H0100
    
    ' mode
    Public Const DAQDA100_MODE_OPE As Integer = 0
    Public Const DAQDA100_MODE_SETUP As Integer = 1
    Public Const DAQDA100_MODE_CALIB As Integer = 2
    
    ' talker output type
    Public Const DAQDA100_TALK_MEASUREDDATA As Integer = 0
    Public Const DAQDA100_TALK_OPEDATA As Integer = 1
    Public Const DAQDA100_TALK_CHINFODATA As Integer = 2
    Public Const DAQDA100_TALK_REPORTDATA As Integer = 4
    Public Const DAQDA100_TALK_SYSINFODATA As Integer = 5
    Public Const DAQDA100_TALK_CALIBDATA As Integer = 8
    Public Const DAQDA100_TALK_SETUPDATA As Integer = 9
    
    ' status byte
    Public Const DAQDA100_STATUS_OFF As Integer = &H0000
    Public Const DAQDA100_STATUS_ADCONV As Integer = &H0001
    Public Const DAQDA100_STATUS_SYNTAX As Integer = &H0002
    Public Const DAQDA100_STATUS_TIMER As Integer = &H0004
    Public Const DAQDA100_STATUS_MEDIA As Integer = &H0008
    Public Const DAQDA100_STATUS_RELEASE As Integer = &H0020
    Public Const DAQDA100_STATUS_SRQ As Integer = &H0040
    Public Const DAQDA100_STATUS_ALL As Integer = &H00FF
    
    ' establish
    Public Const DAQDA100_SETUP_ABORT As Integer = DAQDA100_VALID_OFF
    Public Const DAQDA100_SETUP_STORE As Integer = DAQDA100_VALID_ON
    
    ' unit number
    Public Const DAQDA100_UNITNO_MAINUNIT As Integer = DAQDA100_CHTYPE_MAINUNIT
    Public Const DAQDA100_UNITNO_STANDALONE As Integer = DAQDA100_CHTYPE_STANDALONE
    
    ' computation
    Public Const DAQDA100_COMPUTE_START As Integer = 0
    Public Const DAQDA100_COMPUTE_STOP As Integer = 1
    Public Const DAQDA100_COMPUTE_RESTART As Integer = 2
    Public Const DAQDA100_COMPUTE_CLEAR As Integer = 3
    Public Const DAQDA100_COMPUTE_RELEASE As Integer = 4
    
    ' reporting
    Public Const DAQDA100_REPORT_RUN_START As Integer = 0
    Public Const DAQDA100_REPORT_RUN_STOP As Integer = 1
    
    ' report type
    Public Const DAQDA100_REPORT_HOURLY As Integer = 0
    Public Const DAQDA100_REPORT_DAILY As Integer = 1
    Public Const DAQDA100_REPORT_MONTHLY As Integer = 2
    Public Const DAQDA100_REPORT_STATUS As Integer = 3
    
    ' report status
    Public Const DAQDA100_REPSTATUS_NONE As Integer = &H0000
    Public Const DAQDA100_REPSTATUS_HOURLY_NEW As Integer = &H0001
    Public Const DAQDA100_REPSTATUS_HOURLY_VALID As Integer = &H0002
    Public Const DAQDA100_REPSTATUS_DAILY_NEW As Integer = &H0004
    Public Const DAQDA100_REPSTATUS_DAILY_VALID As Integer = &H0008
    Public Const DAQDA100_REPSTATUS_MONTHLY_NEW As Integer = &H0010
    Public Const DAQDA100_REPSTATUS_MONTHLY_VALID As Integer = &H0020
    
    ' wiring method
    Public Const DAQDA100_WIRE_1PH2W As Integer = 1
    Public Const DAQDA100_WIRE_1PH3W As Integer = 2
    Public Const DAQDA100_WIRE_3PH3W2I As Integer = 3
    Public Const DAQDA100_WIRE_3PH3W3I As Integer = 4
    Public Const DAQDA100_WIRE_3PH4W As Integer = 5
    
    ' mesurement item
    Public Const DAQDA100_POWERITEM_I0 As Integer = &H0000
    Public Const DAQDA100_POWERITEM_I1 As Integer = &H0001
    Public Const DAQDA100_POWERITEM_I2 As Integer = &H0002
    Public Const DAQDA100_POWERITEM_I3 As Integer = &H0003
    Public Const DAQDA100_POWERITEM_I13 As Integer = &H000D
    Public Const DAQDA100_POWERITEM_P0 As Integer = &H0010
    Public Const DAQDA100_POWERITEM_P1 As Integer = &H0011
    Public Const DAQDA100_POWERITEM_P2 As Integer = &H0012
    Public Const DAQDA100_POWERITEM_P3 As Integer = &H0013
    Public Const DAQDA100_POWERITEM_P13 As Integer = &H001D
    Public Const DAQDA100_POWERITEM_PF0 As Integer = &H0020
    Public Const DAQDA100_POWERITEM_PF1 As Integer = &H0021
    Public Const DAQDA100_POWERITEM_PF2 As Integer = &H0022
    Public Const DAQDA100_POWERITEM_PF3 As Integer = &H0023
    Public Const DAQDA100_POWERITEM_PF13 As Integer = &H002D
    Public Const DAQDA100_POWERITEM_PH0 As Integer = &H0030
    Public Const DAQDA100_POWERITEM_PH1 As Integer = &H0031
    Public Const DAQDA100_POWERITEM_PH2 As Integer = &H0032
    Public Const DAQDA100_POWERITEM_PH3 As Integer = &H0033
    Public Const DAQDA100_POWERITEM_PH13 As Integer = &H003D
    Public Const DAQDA100_POWERITEM_V0 As Integer = &H0040
    Public Const DAQDA100_POWERITEM_V1 As Integer = &H0041
    Public Const DAQDA100_POWERITEM_V2 As Integer = &H0042
    Public Const DAQDA100_POWERITEM_V3 As Integer = &H0043
    Public Const DAQDA100_POWERITEM_V13 As Integer = &H004D
    Public Const DAQDA100_POWERITEM_VA0 As Integer = &H0050
    Public Const DAQDA100_POWERITEM_VA1 As Integer = &H0051
    Public Const DAQDA100_POWERITEM_VA2 As Integer = &H0052
    Public Const DAQDA100_POWERITEM_VA3 As Integer = &H0053
    Public Const DAQDA100_POWERITEM_VA13 As Integer = &H005D
    Public Const DAQDA100_POWERITEM_VAR0 As Integer = &H0060
    Public Const DAQDA100_POWERITEM_VAR1 As Integer = &H0061
    Public Const DAQDA100_POWERITEM_VAR2 As Integer = &H0062
    Public Const DAQDA100_POWERITEM_VAR3 As Integer = &H0063
    Public Const DAQDA100_POWERITEM_VAR13 As Integer = &H006D
    Public Const DAQDA100_POWERITEM_FREQ As Integer = &H007F
    
    ' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
    ' DA100 specifications
    ' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
    
    ' number
    Public Const DAQDA100_NUMCH_BYUNIT As Integer = DAQDA100_NUMSLOT * DAQDA100_NUMTERM
    
    ' all
    Public Const DAQDA100_CHTYPE_MEASALL As Integer = &H0F
    Public Const DAQDA100_CHNO_ALL As Integer = -1
    Public Const DAQDA100_LEVELNO_ALL As Integer = -1
    
    ' code
    Public Const DAQDA100_CODE_BINARY As Integer = 0
    Public Const DAQDA100_CODE_ASCII As Integer = 1
    
    ' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
    ' range
    ' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
    
    ' range type
    Public Const DAQDA100_RANGETYPE_VOLT As Integer = &H00000000
    Public Const DAQDA100_RANGETYPE_DI As Integer = &H00010000
    Public Const DAQDA100_RANGETYPE_TC As Integer = &H00020000
    Public Const DAQDA100_RANGETYPE_RTD As Integer = &H00040000
    Public Const DAQDA100_RANGETYPE_SKIP As Integer = &H00080000
    Public Const DAQDA100_RANGETYPE_MA As Integer = &H00100000
    Public Const DAQDA100_RANGETYPE_POWER As Integer = &H00200000
    Public Const DAQDA100_RANGETYPE_STRAIN As Integer = &H00400000
    Public Const DAQDA100_RANGETYPE_PULSE As Integer = &H00800000
    
    ' SKIP
    Public Const DAQDA100_RANGE_SKIP As Integer = DAQDA100_RANGETYPE_SKIP
    
    ' VOLT
    Public Const DAQDA100_RANGE_VOLT_20MV As Integer = DAQDA100_RANGETYPE_VOLT + 1
    Public Const DAQDA100_RANGE_VOLT_60MV As Integer = DAQDA100_RANGETYPE_VOLT + 2
    Public Const DAQDA100_RANGE_VOLT_200MV As Integer = DAQDA100_RANGETYPE_VOLT + 3
    Public Const DAQDA100_RANGE_VOLT_2V As Integer = DAQDA100_RANGETYPE_VOLT + 4
    Public Const DAQDA100_RANGE_VOLT_6V As Integer = DAQDA100_RANGETYPE_VOLT + 5
    Public Const DAQDA100_RANGE_VOLT_20V As Integer = DAQDA100_RANGETYPE_VOLT + 6
    Public Const DAQDA100_RANGE_VOLT_50V As Integer = DAQDA100_RANGETYPE_VOLT + 7
    
    ' TC
    Public Const DAQDA100_RANGE_TC_R As Integer = DAQDA100_RANGETYPE_TC + 1
    Public Const DAQDA100_RANGE_TC_S As Integer = DAQDA100_RANGETYPE_TC + 2
    Public Const DAQDA100_RANGE_TC_B As Integer = DAQDA100_RANGETYPE_TC + 3
    Public Const DAQDA100_RANGE_TC_K As Integer = DAQDA100_RANGETYPE_TC + 4
    Public Const DAQDA100_RANGE_TC_E As Integer = DAQDA100_RANGETYPE_TC + 5
    Public Const DAQDA100_RANGE_TC_J As Integer = DAQDA100_RANGETYPE_TC + 6
    Public Const DAQDA100_RANGE_TC_T As Integer = DAQDA100_RANGETYPE_TC + 7
    Public Const DAQDA100_RANGE_TC_N As Integer = DAQDA100_RANGETYPE_TC + 8
    Public Const DAQDA100_RANGE_TC_W As Integer = DAQDA100_RANGETYPE_TC + 9
    Public Const DAQDA100_RANGE_TC_L As Integer = DAQDA100_RANGETYPE_TC + 10
    Public Const DAQDA100_RANGE_TC_U As Integer = DAQDA100_RANGETYPE_TC + 11
    Public Const DAQDA100_RANGE_TC_KP As Integer = DAQDA100_RANGETYPE_TC + 12
    
    ' RTD
    Public Const DAQDA100_RANGE_RTD_1MAPT As Integer = DAQDA100_RANGETYPE_RTD + 1
    Public Const DAQDA100_RANGE_RTD_2MAPT As Integer = DAQDA100_RANGETYPE_RTD + 2
    Public Const DAQDA100_RANGE_RTD_1MAJPT As Integer = DAQDA100_RANGETYPE_RTD + 3
    Public Const DAQDA100_RANGE_RTD_2MAJPT As Integer = DAQDA100_RANGETYPE_RTD + 4
    Public Const DAQDA100_RANGE_RTD_2MAPT50 As Integer = DAQDA100_RANGETYPE_RTD + 5
    Public Const DAQDA100_RANGE_RTD_1MAPTH As Integer = DAQDA100_RANGETYPE_RTD + 6
    Public Const DAQDA100_RANGE_RTD_2MAPTH As Integer = DAQDA100_RANGETYPE_RTD + 7
    Public Const DAQDA100_RANGE_RTD_1MAJPTH As Integer = DAQDA100_RANGETYPE_RTD + 8
    Public Const DAQDA100_RANGE_RTD_2MAJPTH As Integer = DAQDA100_RANGETYPE_RTD + 9
    Public Const DAQDA100_RANGE_RTD_1MANIS As Integer = DAQDA100_RANGETYPE_RTD + 10
    Public Const DAQDA100_RANGE_RTD_1MANID As Integer = DAQDA100_RANGETYPE_RTD + 11
    Public Const DAQDA100_RANGE_RTD_1MANI120 As Integer = DAQDA100_RANGETYPE_RTD + 12
    Public Const DAQDA100_RANGE_RTD_CU10GE As Integer = DAQDA100_RANGETYPE_RTD + 13
    Public Const DAQDA100_RANGE_RTD_CU10LN As Integer = DAQDA100_RANGETYPE_RTD + 14
    Public Const DAQDA100_RANGE_RTD_CU10WEED As Integer = DAQDA100_RANGETYPE_RTD + 15
    Public Const DAQDA100_RANGE_RTD_CU10BAILEY As Integer = DAQDA100_RANGETYPE_RTD + 16
    Public Const DAQDA100_RANGE_RTD_J263B As Integer = DAQDA100_RANGETYPE_RTD + 17
    
    ' DI
    Public Const DAQDA100_RANGE_DI_LEVEL As Integer = DAQDA100_RANGETYPE_DI + 1
    Public Const DAQDA100_RANGE_DI_CONTACT As Integer = DAQDA100_RANGETYPE_DI + 2
    
    ' mA
    Public Const DAQDA100_RANGE_MA_20MA As Integer = DAQDA100_RANGETYPE_MA + 1
    
    ' POWER
    Public Const DAQDA100_RANGE_POWER_25V05A As Integer = DAQDA100_RANGETYPE_POWER + 1
    Public Const DAQDA100_RANGE_POWER_25V5A As Integer = DAQDA100_RANGETYPE_POWER + 2
    Public Const DAQDA100_RANGE_POWER_250V05A As Integer = DAQDA100_RANGETYPE_POWER + 3
    Public Const DAQDA100_RANGE_POWER_250V5A As Integer = DAQDA100_RANGETYPE_POWER + 4
    
    ' STRAIN
    Public Const DAQDA100_RANGE_STRAIN_2K As Integer = DAQDA100_RANGETYPE_STRAIN + 1
    Public Const DAQDA100_RANGE_STRAIN_20K As Integer = DAQDA100_RANGETYPE_STRAIN + 2
    Public Const DAQDA100_RANGE_STRAIN_200K As Integer = DAQDA100_RANGETYPE_STRAIN + 3
    
    ' PULS
    Public Const DAQDA100_RANGE_PULSE_RATE As Integer = DAQDA100_RANGETYPE_PULSE + 1
    Public Const DAQDA100_RANGE_PULSE_GATE As Integer = DAQDA100_RANGETYPE_PULSE + 2
    
    ' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
    ' Connection
    ' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
    
    Public Declare Ansi Function openDA100 Lib "DAQDA100" (ByVal strAddress As String, ByRef errorCode As Integer) As Integer
    
    Public Declare Ansi Function closeDA100 Lib "DAQDA100" (ByVal daqda100 As Integer) As Integer
    
    Public Declare Ansi Function sendLineDA100 Lib "DAQDA100" (ByVal daqda100 As Integer, ByVal strLine As String) As Integer
    
    Public Declare Ansi Function receiveLineDA100 Lib "DAQDA100" (ByVal daqda100 As Integer, ByVal strLine As String, ByVal maxLine As Integer, ByRef lenLine As Integer) As Integer
    
    Public Declare Ansi Function receiveByteDA100 Lib "DAQDA100" (ByVal daqda100 As Integer, ByRef byteData As Byte, ByVal maxData As Integer, ByRef lenData As Integer) As Integer
    
    Public Declare Ansi Function sendTriggerDA100 Lib "DAQDA100" (ByVal daqda100 As Integer) As Integer
    
    Public Declare Ansi Function updateStatusDA100 Lib "DAQDA100" (ByVal daqda100 As Integer) As Integer
    
    Public Declare Ansi Function runCommandDA100 Lib "DAQDA100" (ByVal daqda100 As Integer, ByVal strCmd As String) As Integer
    
    ' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
    ' Control
    ' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
    
    Public Declare Ansi Function switchModeDA100 Lib "DAQDA100" (ByVal daqda100 As Integer, ByVal iMode As Integer) As Integer
    
    Public Declare Ansi Function switchCodeDA100 Lib "DAQDA100" (ByVal daqda100 As Integer, ByVal iCode As Integer) As Integer
    
    Public Declare Ansi Function reconstructDA100 Lib "DAQDA100" (ByVal daqda100 As Integer) As Integer
    
    Public Declare Ansi Function initSetValueDA100 Lib "DAQDA100" (ByVal daqda100 As Integer) As Integer
    
    Public Declare Ansi Function ackAlarmDA100 Lib "DAQDA100" (ByVal daqda100 As Integer) As Integer
    
    Public Declare Ansi Function setDateTimeNowDA100 Lib "DAQDA100" (ByVal daqda100 As Integer) As Integer
    
    Public Declare Ansi Function switchComputeDA100 Lib "DAQDA100" (ByVal daqda100 As Integer, ByVal iCode As Integer) As Integer
    
    Public Declare Ansi Function switchReportDA100 Lib "DAQDA100" (ByVal daqda100 As Integer, ByVal iReportRun As Integer) As Integer
    
    Public Declare Ansi Function establishDA100 Lib "DAQDA100" (ByVal daqda100 As Integer, ByVal iSetup As Integer) As Integer
    
    ' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
    ' Set on Operation Mode
    ' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
    
    Public Declare Ansi Function setRangeDA100 Lib "DAQDA100" (ByVal daqda100 As Integer, ByVal chType As Integer, ByVal chNo As Integer, ByVal iRange As Integer, ByVal spanMin As Integer, ByVal spanMax As Integer, ByVal scaleMin As Integer, ByVal scaleMax As Integer, ByVal scalePoint As Integer, ByVal bFilter As Integer, ByVal iItem As Integer, ByVal iWire As Integer) As Integer
    
    Public Declare Ansi Function setChDELTADA100 Lib "DAQDA100" (ByVal daqda100 As Integer, ByVal chType As Integer, ByVal chNo As Integer, ByVal refChNo As Integer, ByVal spanMin As Integer, ByVal spanMax As Integer) As Integer
    
    Public Declare Ansi Function setChRRJCDA100 Lib "DAQDA100" (ByVal daqda100 As Integer, ByVal chType As Integer, ByVal chNo As Integer, ByVal refChNo As Integer, ByVal spanMin As Integer, ByVal spanMax As Integer) As Integer
    
    Public Declare Ansi Function setChUnitDA100 Lib "DAQDA100" (ByVal daqda100 As Integer, ByVal chType As Integer, ByVal chNo As Integer, ByVal strUnit As String) As Integer
    
    Public Declare Ansi Function setChAlarmDA100 Lib "DAQDA100" (ByVal daqda100 As Integer, ByVal chType As Integer, ByVal chNo As Integer, ByVal levelNo As Integer, ByVal iAlarmType As Integer, ByVal value As Integer, ByVal relayType As Integer, ByVal relayNo As Integer) As Integer
    
    ' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
    ' Measurement
    ' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
    
    Public Declare Ansi Function measInstChDA100 Lib "DAQDA100" (ByVal daqda100 As Integer, ByVal chType As Integer, ByVal chNo As Integer) As Integer
    
    Public Declare Ansi Function mathInstChDA100 Lib "DAQDA100" (ByVal daqda100 As Integer, ByVal chNo As Integer) As Integer
    
    Public Declare Ansi Function measInfoChDA100 Lib "DAQDA100" (ByVal daqda100 As Integer, ByVal chType As Integer, ByVal chNo As Integer) As Integer
    
    Public Declare Ansi Function mathInfoChDA100 Lib "DAQDA100" (ByVal daqda100 As Integer, ByVal chNo As Integer) As Integer
    
    Public Declare Ansi Function updateSystemConfigDA100 Lib "DAQDA100" (ByVal daqda100 As Integer) As Integer
    
    Public Declare Ansi Function updateReportStatusDA100 Lib "DAQDA100" (ByVal daqda100 As Integer) As Integer
    
    ' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
    ' Talker
    ' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
    
    Public Declare Ansi Function talkOperationChDataDA100 Lib "DAQDA100" (ByVal daqda100 As Integer, ByVal chType As Integer, ByVal chNo As Integer) As Integer
    
    Public Declare Ansi Function talkOperationDataDA100 Lib "DAQDA100" (ByVal daqda100 As Integer, ByVal startChType As Integer, ByVal startChNo As Integer, ByVal endChType As Integer, ByVal endChNo As Integer) As Integer
    
    Public Declare Ansi Function talkSetupChDataDA100 Lib "DAQDA100" (ByVal daqda100 As Integer, ByVal chType As Integer, ByVal chNo As Integer) As Integer
    
    Public Declare Ansi Function talkSetupDataDA100 Lib "DAQDA100" (ByVal daqda100 As Integer, ByVal startChType As Integer, ByVal startChNo As Integer, ByVal endChType As Integer, ByVal endChNo As Integer) As Integer
    
    Public Declare Ansi Function talkCalibrationChDataDA100 Lib "DAQDA100" (ByVal daqda100 As Integer, ByVal chType As Integer, ByVal chNo As Integer) As Integer
    
    Public Declare Ansi Function talkCalibrationDataDA100 Lib "DAQDA100" (ByVal daqda100 As Integer, ByVal startChType As Integer, ByVal startChNo As Integer, ByVal endChType As Integer, ByVal endChNo As Integer) As Integer
    
    Public Declare Ansi Function getSetDataByLineDA100 Lib "DAQDA100" (ByVal daqda100 As Integer, ByVal strLine As String, ByVal maxLine As Integer, ByRef lenLine As Integer, ByRef pFlag As Integer) As Integer
    
    ' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
    ' Get Data
    ' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
    
    Public Declare Ansi Function dataValueDA100 Lib "DAQDA100" (ByVal daqda100 As Integer, ByVal chType As Integer, ByVal chNo As Integer) As Integer
    
    Public Declare Ansi Function dataStatusDA100 Lib "DAQDA100" (ByVal daqda100 As Integer, ByVal chType As Integer, ByVal chNo As Integer) As Integer
    
    Public Declare Ansi Function dataAlarmDA100 Lib "DAQDA100" (ByVal daqda100 As Integer, ByVal chType As Integer, ByVal chNo As Integer, ByVal levelNo As Integer) As Integer
    
    Public Declare Ansi Function dataDoubleValueDA100 Lib "DAQDA100" (ByVal daqda100 As Integer, ByVal chType As Integer, ByVal chNo As Integer) As Double
    
    Public Declare Ansi Function dataStringValueDA100 Lib "DAQDA100" (ByVal daqda100 As Integer, ByVal chType As Integer, ByVal chNo As Integer, ByVal strValue As String, ByVal lenValue As Integer) As Integer
    
    Public Declare Ansi Function dataYearDA100 Lib "DAQDA100" (ByVal daqda100 As Integer, ByVal chType As Integer, ByVal chNo As Integer) As Integer
    
    Public Declare Ansi Function dataMonthDA100 Lib "DAQDA100" (ByVal daqda100 As Integer, ByVal chType As Integer, ByVal chNo As Integer) As Integer
    
    Public Declare Ansi Function dataDayDA100 Lib "DAQDA100" (ByVal daqda100 As Integer, ByVal chType As Integer, ByVal chNo As Integer) As Integer
    
    Public Declare Ansi Function dataHourDA100 Lib "DAQDA100" (ByVal daqda100 As Integer, ByVal chType As Integer, ByVal chNo As Integer) As Integer
    
    Public Declare Ansi Function dataMinuteDA100 Lib "DAQDA100" (ByVal daqda100 As Integer, ByVal chType As Integer, ByVal chNo As Integer) As Integer
    
    Public Declare Ansi Function dataSecondDA100 Lib "DAQDA100" (ByVal daqda100 As Integer, ByVal chType As Integer, ByVal chNo As Integer) As Integer
    
    Public Declare Ansi Function alarmTypeDA100 Lib "DAQDA100" (ByVal daqda100 As Integer, ByVal chType As Integer, ByVal chNo As Integer, ByVal levelNo As Integer) As Integer
    
    ' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
    ' Get Channel Information
    ' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
    
    Public Declare Ansi Function channelPointDA100 Lib "DAQDA100" (ByVal daqda100 As Integer, ByVal chType As Integer, ByVal chNo As Integer) As Integer
    
    Public Declare Ansi Function channelStatusDA100 Lib "DAQDA100" (ByVal daqda100 As Integer, ByVal chType As Integer, ByVal chNo As Integer) As Integer
    
    Public Declare Ansi Function toChannelUnitDA100 Lib "DAQDA100" (ByVal daqda100 As Integer, ByVal chType As Integer, ByVal chNo As Integer, ByVal strUnit As String, ByVal lenUnit As Integer) As Integer
    
    ' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
    ' Get System Information
    ' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
    
    Public Declare Ansi Function unitIntervalDA100 Lib "DAQDA100" (ByVal daqda100 As Integer) As Double
    
    Public Declare Ansi Function unitValidDA100 Lib "DAQDA100" (ByVal daqda100 As Integer, ByVal unitNo As Integer) As Integer
    
    Public Declare Ansi Function moduleCodeDA100 Lib "DAQDA100" (ByVal daqda100 As Integer, ByVal unitNo As Integer, ByVal slotNo As Integer) As Integer
    
    Public Declare Ansi Function toModuleNameDA100 Lib "DAQDA100" (ByVal daqda100 As Integer, ByVal unitNo As Integer, ByVal slotNo As Integer, ByVal strName As String, ByVal lenName As Integer) As Integer
    
    ' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
    ' Get Status
    ' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
    
    Public Declare Ansi Function statusByteDA100 Lib "DAQDA100" (ByVal daqda100 As Integer) As Integer
    
    Public Declare Ansi Function statusCodeDA100 Lib "DAQDA100" (ByVal daqda100 As Integer) As Integer
    
    Public Declare Ansi Function statusReportDA100 Lib "DAQDA100" (ByVal daqda100 As Integer) As Integer
    
    ' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
    ' Utility
    ' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
    
    Public Declare Ansi Function toDoubleValueDA100 Lib "DAQDA100" (ByVal dataValue As Integer, ByVal point As Integer) As Double
    
    Public Declare Ansi Function toStringValueDA100 Lib "DAQDA100" (ByVal dataValue As Integer, ByVal point As Integer, ByVal strValue As String, ByVal lenValue As Integer) As Integer
    
    Public Declare Ansi Function toAlarmNameDA100 Lib "DAQDA100" (ByVal iAlarmType As Integer, ByVal strAlarm As String, ByVal lenAlarm As Integer) As Integer
    
    Public Declare Ansi Function alarmMaxLengthDA100 Lib "DAQDA100" () As Integer
    
    Public Declare Ansi Function versionAPIDA100 Lib "DAQDA100" () As Integer
    
    Public Declare Ansi Function revisionAPIDA100 Lib "DAQDA100" () As Integer
    
    Public Declare Ansi Function toErrorMessageDA100 Lib "DAQDA100" (ByVal errCode As Integer, ByVal errStr As String, ByVal errLen As Integer) As Integer
    
    Public Declare Ansi Function errorMaxLengthDA100 Lib "DAQDA100" () As Integer
    
    ' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
End Module
