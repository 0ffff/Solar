Attribute VB_Name = "DAQDA100Reader"
' DAQDA100Reader.bas
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

' communication
Public Const DAQDA100READER_DATAPORT = 34151

' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
' @see DAQDA100
' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --

' total number
Public Const DAQDA100READER_NUMCHANNEL = 360
Public Const DAQDA100READER_NUMALARM = 4
Public Const DAQDA100READER_NUMUNIT = 6
Public Const DAQDA100READER_NUMSLOT = 6
Public Const DAQDA100READER_NUMTERM = 10

' all
Public Const DAQDA100READER_CHTYPE_MEASALL = &H000F
Public Const DAQDA100READER_CHNO_ALL = -1

' string length without NULL
Public Const DAQDA100READER_MAXUNITLEN = 6

' valid
Public Const DAQDA100READER_VALID_OFF = 0
Public Const DAQDA100READER_VALID_ON = 1

' data status
Public Const DAQDA100READER_DATA_UNKNWON = &H00000000
Public Const DAQDA100READER_DATA_NORMAL = &H00000001
Public Const DAQDA100READER_DATA_DIFFINPUT = &H00000002
Public Const DAQDA100READER_DATA_READER = &H00000003
Public Const DAQDA100READER_DATA_PLUSOVER = &H00007FFF
Public Const DAQDA100READER_DATA_MINUSOVER = &H00008001
Public Const DAQDA100READER_DATA_SKIP = &H00008002
Public Const DAQDA100READER_DATA_ILLEGAL = &H00008003
Public Const DAQDA100READER_DATA_ABNORMAL = &H00008004
Public Const DAQDA100READER_DATA_NODATA = &H00008005

' alarm type
Public Const DAQDA100READER_ALARM_NONE = 0
Public Const DAQDA100READER_ALARM_UPPER = 1
Public Const DAQDA100READER_ALARM_LOWER = 2
Public Const DAQDA100READER_ALARM_UPDIFF = 3
Public Const DAQDA100READER_ALARM_LOWDIFF = 4
Public Const DAQDA100READER_ALARM_INCRATE = 5
Public Const DAQDA100READER_ALARM_DECRATE = 6

' channel/relay type
' 0 - 5 if subunit
Public Const DAQDA100READER_CHTYPE_MAINUNIT = -1
Public Const DAQDA100READER_CHTYPE_STANDALONE = 0
Public Const DAQDA100READER_CHTYPE_MATHTYPE = &H0080

' unit number
' 0 - 5 if subunit
Public Const DAQDA100READER_UNITNO_MAINUNIT = DAQDA100READER_CHTYPE_MAINUNIT
Public Const DAQDA100READER_UNITNO_STANDALONE = DAQDA100READER_CHTYPE_STANDALONE

' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
' Connection
' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --

Public Declare Function openDA100Reader Lib "DAQDA100" (ByVal strAddress As String, ByRef errorCode As Long) As Long

Public Declare Function closeDA100Reader Lib "DAQDA100" (ByVal daqda100reader As Long) As Long

' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
' Measurement
' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --

Public Declare Function measInstChDA100Reader Lib "DAQDA100" (ByVal daqda100reader As Long, ByVal chType As Long, ByVal chNo As Long) As Long

Public Declare Function mathInstChDA100Reader Lib "DAQDA100" (ByVal daqda100reader As Long, ByVal chNo As Long) As Long

Public Declare Function measInfoChDA100Reader Lib "DAQDA100" (ByVal daqda100reader As Long, ByVal chType As Long, ByVal chNo As Long) As Long

Public Declare Function mathInfoChDA100Reader Lib "DAQDA100" (ByVal daqda100reader As Long, ByVal chNo As Long) As Long

' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
' Get Data
' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --

Public Declare Function dataValueDA100Reader Lib "DAQDA100" (ByVal daqda100reader As Long, ByVal chType As Long, ByVal chNo As Long) As Long

Public Declare Function dataStatusDA100Reader Lib "DAQDA100" (ByVal daqda100reader As Long, ByVal chType As Long, ByVal chNo As Long) As Long

Public Declare Function alarmTypeDA100Reader Lib "DAQDA100" (ByVal daqda100reader As Long, ByVal chType As Long, ByVal chNo As Long, ByVal levelNo As Long) As Long

Public Declare Function dataAlarmDA100Reader Lib "DAQDA100" (ByVal daqda100reader As Long, ByVal chType As Long, ByVal chNo As Long, ByVal levelNo As Long) As Long

Public Declare Function dataDoubleValueDA100Reader Lib "DAQDA100" (ByVal daqda100reader As Long, ByVal chType As Long, ByVal chNo As Long) As Double

Public Declare Function dataStringValueDA100Reader Lib "DAQDA100" (ByVal daqda100reader As Long, ByVal chType As Long, ByVal chNo As Long, ByVal strValue As String, ByVal lenValue As Long) As Long

Public Declare Function dataYearDA100Reader Lib "DAQDA100" (ByVal daqda100reader As Long, ByVal chType As Long, ByVal chNo As Long) As Long

Public Declare Function dataMonthDA100Reader Lib "DAQDA100" (ByVal daqda100reader As Long, ByVal chType As Long, ByVal chNo As Long) As Long

Public Declare Function dataDayDA100Reader Lib "DAQDA100" (ByVal daqda100reader As Long, ByVal chType As Long, ByVal chNo As Long) As Long

Public Declare Function dataHourDA100Reader Lib "DAQDA100" (ByVal daqda100reader As Long, ByVal chType As Long, ByVal chNo As Long) As Long

Public Declare Function dataMinuteDA100Reader Lib "DAQDA100" (ByVal daqda100reader As Long, ByVal chType As Long, ByVal chNo As Long) As Long

Public Declare Function dataSecondDA100Reader Lib "DAQDA100" (ByVal daqda100reader As Long, ByVal chType As Long, ByVal chNo As Long) As Long

Public Declare Function dataMilliSecDA100Reader Lib "DAQDA100" (ByVal daqda100reader As Long, ByVal chType As Long, ByVal chNo As Long) As Long

' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
' Get Channel Information
' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --

Public Declare Function channelPointDA100Reader Lib "DAQDA100" (ByVal daqda100reader As Long, ByVal chType As Long, ByVal chNo As Long) As Long

Public Declare Function channelStatusDA100Reader Lib "DAQDA100" (ByVal daqda100reader As Long, ByVal chType As Long, ByVal chNo As Long) As Long

Public Declare Function toChannelUnitDA100Reader Lib "DAQDA100" (ByVal daqda100reader As Long, ByVal chType As Long, ByVal chNo As Long, ByVal strUnit As String, ByVal lenUnit As Long) As Long

' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
' Utility
' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --

Public Declare Function toDoubleValueDA100Reader Lib "DAQDA100" (ByVal dataValue As Long, ByVal point As Long) As Double

Public Declare Function toStringValueDA100Reader Lib "DAQDA100" (ByVal dataValue As Long, ByVal point As Long, ByVal strValue As String, ByVal lenValue As Long) As Long

Public Declare Function toAlarmNameDA100Reader Lib "DAQDA100" (ByVal iAlarmType As Long, ByVal strAlarm As String, ByVal lenAlarm As Long) As Long

Public Declare Function alarmMaxLengthDA100Reader Lib "DAQDA100" () As Long

Public Declare Function versionAPIDA100Reader Lib "DAQDA100" () As Long

Public Declare Function revisionAPIDA100Reader Lib "DAQDA100" () As Long

Public Declare Function toErrorMessageDA100Reader Lib "DAQDA100" (ByVal errCode As Long, ByVal errStr As String, ByVal errLen As Long) As Long

Public Declare Function errorMaxLengthDA100Reader Lib "DAQDA100" () As Long

' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
