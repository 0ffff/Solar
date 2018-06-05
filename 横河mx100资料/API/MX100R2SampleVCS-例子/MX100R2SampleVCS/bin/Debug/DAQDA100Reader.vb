Imports System.Runtime.InteropServices

' DAQDA100Reader.vb
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

Module DAQDA100Reader
    
    ' communication
    Public Const DAQDA100READER_DATAPORT As Integer = 34151
    
    ' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
    ' @see DAQDA100
    ' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
    
    ' total number
    Public Const DAQDA100READER_NUMCHANNEL As Integer = 360
    Public Const DAQDA100READER_NUMALARM As Integer = 4
    Public Const DAQDA100READER_NUMUNIT As Integer = 6
    Public Const DAQDA100READER_NUMSLOT As Integer = 6
    Public Const DAQDA100READER_NUMTERM As Integer = 10
    
    ' all
    Public Const DAQDA100READER_CHTYPE_MEASALL As Integer = &H000F
    Public Const DAQDA100READER_CHNO_ALL As Integer = -1
    
    ' string length without NULL
    Public Const DAQDA100READER_MAXUNITLEN As Integer = 6
    
    ' valid
    Public Const DAQDA100READER_VALID_OFF As Integer = 0
    Public Const DAQDA100READER_VALID_ON As Integer = 1
    
    ' data status
    Public Const DAQDA100READER_DATA_UNKNWON As Integer = &H00000000
    Public Const DAQDA100READER_DATA_NORMAL As Integer = &H00000001
    Public Const DAQDA100READER_DATA_DIFFINPUT As Integer = &H00000002
    Public Const DAQDA100READER_DATA_READER As Integer = &H00000003
    Public Const DAQDA100READER_DATA_PLUSOVER As Integer = &H00007FFF
    Public Const DAQDA100READER_DATA_MINUSOVER As Integer = &H00008001
    Public Const DAQDA100READER_DATA_SKIP As Integer = &H00008002
    Public Const DAQDA100READER_DATA_ILLEGAL As Integer = &H00008003
    Public Const DAQDA100READER_DATA_ABNORMAL As Integer = &H00008004
    Public Const DAQDA100READER_DATA_NODATA As Integer = &H00008005
    
    ' alarm type
    Public Const DAQDA100READER_ALARM_NONE As Integer = 0
    Public Const DAQDA100READER_ALARM_UPPER As Integer = 1
    Public Const DAQDA100READER_ALARM_LOWER As Integer = 2
    Public Const DAQDA100READER_ALARM_UPDIFF As Integer = 3
    Public Const DAQDA100READER_ALARM_LOWDIFF As Integer = 4
    Public Const DAQDA100READER_ALARM_INCRATE As Integer = 5
    Public Const DAQDA100READER_ALARM_DECRATE As Integer = 6
    
    ' channel/relay type
    ' 0 - 5 if subunit
    Public Const DAQDA100READER_CHTYPE_MAINUNIT As Integer = -1
    Public Const DAQDA100READER_CHTYPE_STANDALONE As Integer = 0
    Public Const DAQDA100READER_CHTYPE_MATHTYPE As Integer = &H0080
    
    ' unit number
    ' 0 - 5 if subunit
    Public Const DAQDA100READER_UNITNO_MAINUNIT As Integer = DAQDA100READER_CHTYPE_MAINUNIT
    Public Const DAQDA100READER_UNITNO_STANDALONE As Integer = DAQDA100READER_CHTYPE_STANDALONE
    
    ' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
    ' Connection
    ' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
    
    Public Declare Ansi Function openDA100Reader Lib "DAQDA100" (ByVal strAddress As String, ByRef errorCode As Integer) As Integer
    
    Public Declare Ansi Function closeDA100Reader Lib "DAQDA100" (ByVal daqda100reader As Integer) As Integer
    
    ' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
    ' Measurement
    ' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
    
    Public Declare Ansi Function measInstChDA100Reader Lib "DAQDA100" (ByVal daqda100reader As Integer, ByVal chType As Integer, ByVal chNo As Integer) As Integer
    
    Public Declare Ansi Function mathInstChDA100Reader Lib "DAQDA100" (ByVal daqda100reader As Integer, ByVal chNo As Integer) As Integer
    
    Public Declare Ansi Function measInfoChDA100Reader Lib "DAQDA100" (ByVal daqda100reader As Integer, ByVal chType As Integer, ByVal chNo As Integer) As Integer
    
    Public Declare Ansi Function mathInfoChDA100Reader Lib "DAQDA100" (ByVal daqda100reader As Integer, ByVal chNo As Integer) As Integer
    
    ' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
    ' Get Data
    ' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
    
    Public Declare Ansi Function dataValueDA100Reader Lib "DAQDA100" (ByVal daqda100reader As Integer, ByVal chType As Integer, ByVal chNo As Integer) As Integer
    
    Public Declare Ansi Function dataStatusDA100Reader Lib "DAQDA100" (ByVal daqda100reader As Integer, ByVal chType As Integer, ByVal chNo As Integer) As Integer
    
    Public Declare Ansi Function alarmTypeDA100Reader Lib "DAQDA100" (ByVal daqda100reader As Integer, ByVal chType As Integer, ByVal chNo As Integer, ByVal levelNo As Integer) As Integer
    
    Public Declare Ansi Function dataAlarmDA100Reader Lib "DAQDA100" (ByVal daqda100reader As Integer, ByVal chType As Integer, ByVal chNo As Integer, ByVal levelNo As Integer) As Integer
    
    Public Declare Ansi Function dataDoubleValueDA100Reader Lib "DAQDA100" (ByVal daqda100reader As Integer, ByVal chType As Integer, ByVal chNo As Integer) As Double
    
    Public Declare Ansi Function dataStringValueDA100Reader Lib "DAQDA100" (ByVal daqda100reader As Integer, ByVal chType As Integer, ByVal chNo As Integer, ByVal strValue As String, ByVal lenValue As Integer) As Integer
    
    Public Declare Ansi Function dataYearDA100Reader Lib "DAQDA100" (ByVal daqda100reader As Integer, ByVal chType As Integer, ByVal chNo As Integer) As Integer
    
    Public Declare Ansi Function dataMonthDA100Reader Lib "DAQDA100" (ByVal daqda100reader As Integer, ByVal chType As Integer, ByVal chNo As Integer) As Integer
    
    Public Declare Ansi Function dataDayDA100Reader Lib "DAQDA100" (ByVal daqda100reader As Integer, ByVal chType As Integer, ByVal chNo As Integer) As Integer
    
    Public Declare Ansi Function dataHourDA100Reader Lib "DAQDA100" (ByVal daqda100reader As Integer, ByVal chType As Integer, ByVal chNo As Integer) As Integer
    
    Public Declare Ansi Function dataMinuteDA100Reader Lib "DAQDA100" (ByVal daqda100reader As Integer, ByVal chType As Integer, ByVal chNo As Integer) As Integer
    
    Public Declare Ansi Function dataSecondDA100Reader Lib "DAQDA100" (ByVal daqda100reader As Integer, ByVal chType As Integer, ByVal chNo As Integer) As Integer
    
    Public Declare Ansi Function dataMilliSecDA100Reader Lib "DAQDA100" (ByVal daqda100reader As Integer, ByVal chType As Integer, ByVal chNo As Integer) As Integer
    
    ' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
    ' Get Channel Information
    ' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
    
    Public Declare Ansi Function channelPointDA100Reader Lib "DAQDA100" (ByVal daqda100reader As Integer, ByVal chType As Integer, ByVal chNo As Integer) As Integer
    
    Public Declare Ansi Function channelStatusDA100Reader Lib "DAQDA100" (ByVal daqda100reader As Integer, ByVal chType As Integer, ByVal chNo As Integer) As Integer
    
    Public Declare Ansi Function toChannelUnitDA100Reader Lib "DAQDA100" (ByVal daqda100reader As Integer, ByVal chType As Integer, ByVal chNo As Integer, ByVal strUnit As String, ByVal lenUnit As Integer) As Integer
    
    ' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
    ' Utility
    ' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
    
    Public Declare Ansi Function toDoubleValueDA100Reader Lib "DAQDA100" (ByVal dataValue As Integer, ByVal point As Integer) As Double
    
    Public Declare Ansi Function toStringValueDA100Reader Lib "DAQDA100" (ByVal dataValue As Integer, ByVal point As Integer, ByVal strValue As String, ByVal lenValue As Integer) As Integer
    
    Public Declare Ansi Function toAlarmNameDA100Reader Lib "DAQDA100" (ByVal iAlarmType As Integer, ByVal strAlarm As String, ByVal lenAlarm As Integer) As Integer
    
    Public Declare Ansi Function alarmMaxLengthDA100Reader Lib "DAQDA100" () As Integer
    
    Public Declare Ansi Function versionAPIDA100Reader Lib "DAQDA100" () As Integer
    
    Public Declare Ansi Function revisionAPIDA100Reader Lib "DAQDA100" () As Integer
    
    Public Declare Ansi Function toErrorMessageDA100Reader Lib "DAQDA100" (ByVal errCode As Integer, ByVal errStr As String, ByVal errLen As Integer) As Integer
    
    Public Declare Ansi Function errorMaxLengthDA100Reader Lib "DAQDA100" () As Integer
    
    ' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
End Module
