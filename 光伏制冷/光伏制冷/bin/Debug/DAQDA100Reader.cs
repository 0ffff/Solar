using System;
using System.Runtime.InteropServices;

// DAQDA100Reader.cs
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
//
// Copyright(c) 2004 Yokogawa Electric Corporation. All rights reserved.
//
// This is defined export DAQDA100.dll.
// Declare Functions for C#
//
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
// 2004/11/01 Ver.2 Rev.1
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --

public class DAQDA100Reader
{
	// Construct
	public DAQDA100Reader()
	{
	}
	
	// communication
	public const int DAQDA100READER_DATAPORT = 34151;
	
	// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
	// @see DAQDA100
	// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
	
	// total number
	public const int DAQDA100READER_NUMCHANNEL = 360;
	public const int DAQDA100READER_NUMALARM = 4;
	public const int DAQDA100READER_NUMUNIT = 6;
	public const int DAQDA100READER_NUMSLOT = 6;
	public const int DAQDA100READER_NUMTERM = 10;
	
	// all
	public const int DAQDA100READER_CHTYPE_MEASALL = 0x000F;
	public const int DAQDA100READER_CHNO_ALL = -1;
	
	// string length without NULL
	public const int DAQDA100READER_MAXUNITLEN = 6;
	
	// valid
	public const int DAQDA100READER_VALID_OFF = 0;
	public const int DAQDA100READER_VALID_ON = 1;
	
	// data status
	public const int DAQDA100READER_DATA_UNKNWON = 0x00000000;
	public const int DAQDA100READER_DATA_NORMAL = 0x00000001;
	public const int DAQDA100READER_DATA_DIFFINPUT = 0x00000002;
	public const int DAQDA100READER_DATA_READER = 0x00000003;
	public const int DAQDA100READER_DATA_PLUSOVER = 0x00007FFF;
	public const int DAQDA100READER_DATA_MINUSOVER = 0x00008001;
	public const int DAQDA100READER_DATA_SKIP = 0x00008002;
	public const int DAQDA100READER_DATA_ILLEGAL = 0x00008003;
	public const int DAQDA100READER_DATA_ABNORMAL = 0x00008004;
	public const int DAQDA100READER_DATA_NODATA = 0x00008005;
	
	// alarm type
	public const int DAQDA100READER_ALARM_NONE = 0;
	public const int DAQDA100READER_ALARM_UPPER = 1;
	public const int DAQDA100READER_ALARM_LOWER = 2;
	public const int DAQDA100READER_ALARM_UPDIFF = 3;
	public const int DAQDA100READER_ALARM_LOWDIFF = 4;
	public const int DAQDA100READER_ALARM_INCRATE = 5;
	public const int DAQDA100READER_ALARM_DECRATE = 6;
	
	// channel/relay type
	// 0 - 5 if subunit
	public const int DAQDA100READER_CHTYPE_MAINUNIT = -1;
	public const int DAQDA100READER_CHTYPE_STANDALONE = 0;
	public const int DAQDA100READER_CHTYPE_MATHTYPE = 0x0080;
	
	// unit number
	// 0 - 5 if subunit
	public const int DAQDA100READER_UNITNO_MAINUNIT = DAQDA100READER_CHTYPE_MAINUNIT;
	public const int DAQDA100READER_UNITNO_STANDALONE = DAQDA100READER_CHTYPE_STANDALONE;
	
	// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
	// Connection
	// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
	
	[DllImport("DAQDA100.dll", CharSet=CharSet.Auto, EntryPoint="openDA100Reader")]
	public static extern int openDA100Reader(byte[] strAddress, out int errorCode);
	
	[DllImport("DAQDA100.dll", CharSet=CharSet.Auto, EntryPoint="closeDA100Reader")]
	public static extern int closeDA100Reader(int daqda100reader);
	
	// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
	// Measurement
	// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
	
	[DllImport("DAQDA100.dll", CharSet=CharSet.Auto, EntryPoint="measInstChDA100Reader")]
	public static extern int measInstChDA100Reader(int daqda100reader, int chType, int chNo);
	
	[DllImport("DAQDA100.dll", CharSet=CharSet.Auto, EntryPoint="mathInstChDA100Reader")]
	public static extern int mathInstChDA100Reader(int daqda100reader, int chNo);
	
	[DllImport("DAQDA100.dll", CharSet=CharSet.Auto, EntryPoint="measInfoChDA100Reader")]
	public static extern int measInfoChDA100Reader(int daqda100reader, int chType, int chNo);
	
	[DllImport("DAQDA100.dll", CharSet=CharSet.Auto, EntryPoint="mathInfoChDA100Reader")]
	public static extern int mathInfoChDA100Reader(int daqda100reader, int chNo);
	
	// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
	// Get Data
	// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
	
	[DllImport("DAQDA100.dll", CharSet=CharSet.Auto, EntryPoint="dataValueDA100Reader")]
	public static extern int dataValueDA100Reader(int daqda100reader, int chType, int chNo);
	
	[DllImport("DAQDA100.dll", CharSet=CharSet.Auto, EntryPoint="dataStatusDA100Reader")]
	public static extern int dataStatusDA100Reader(int daqda100reader, int chType, int chNo);
	
	[DllImport("DAQDA100.dll", CharSet=CharSet.Auto, EntryPoint="alarmTypeDA100Reader")]
	public static extern int alarmTypeDA100Reader(int daqda100reader, int chType, int chNo, int levelNo);
	
	[DllImport("DAQDA100.dll", CharSet=CharSet.Auto, EntryPoint="dataAlarmDA100Reader")]
	public static extern int dataAlarmDA100Reader(int daqda100reader, int chType, int chNo, int levelNo);
	
	[DllImport("DAQDA100.dll", CharSet=CharSet.Auto, EntryPoint="dataDoubleValueDA100Reader")]
	public static extern double dataDoubleValueDA100Reader(int daqda100reader, int chType, int chNo);
	
	[DllImport("DAQDA100.dll", CharSet=CharSet.Auto, EntryPoint="dataStringValueDA100Reader")]
	public static extern int dataStringValueDA100Reader(int daqda100reader, int chType, int chNo, byte[] strValue, int lenValue);
	
	[DllImport("DAQDA100.dll", CharSet=CharSet.Auto, EntryPoint="dataYearDA100Reader")]
	public static extern int dataYearDA100Reader(int daqda100reader, int chType, int chNo);
	
	[DllImport("DAQDA100.dll", CharSet=CharSet.Auto, EntryPoint="dataMonthDA100Reader")]
	public static extern int dataMonthDA100Reader(int daqda100reader, int chType, int chNo);
	
	[DllImport("DAQDA100.dll", CharSet=CharSet.Auto, EntryPoint="dataDayDA100Reader")]
	public static extern int dataDayDA100Reader(int daqda100reader, int chType, int chNo);
	
	[DllImport("DAQDA100.dll", CharSet=CharSet.Auto, EntryPoint="dataHourDA100Reader")]
	public static extern int dataHourDA100Reader(int daqda100reader, int chType, int chNo);
	
	[DllImport("DAQDA100.dll", CharSet=CharSet.Auto, EntryPoint="dataMinuteDA100Reader")]
	public static extern int dataMinuteDA100Reader(int daqda100reader, int chType, int chNo);
	
	[DllImport("DAQDA100.dll", CharSet=CharSet.Auto, EntryPoint="dataSecondDA100Reader")]
	public static extern int dataSecondDA100Reader(int daqda100reader, int chType, int chNo);
	
	[DllImport("DAQDA100.dll", CharSet=CharSet.Auto, EntryPoint="dataMilliSecDA100Reader")]
	public static extern int dataMilliSecDA100Reader(int daqda100reader, int chType, int chNo);
	
	// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
	// Get Channel Information
	// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
	
	[DllImport("DAQDA100.dll", CharSet=CharSet.Auto, EntryPoint="channelPointDA100Reader")]
	public static extern int channelPointDA100Reader(int daqda100reader, int chType, int chNo);
	
	[DllImport("DAQDA100.dll", CharSet=CharSet.Auto, EntryPoint="channelStatusDA100Reader")]
	public static extern int channelStatusDA100Reader(int daqda100reader, int chType, int chNo);
	
	[DllImport("DAQDA100.dll", CharSet=CharSet.Auto, EntryPoint="toChannelUnitDA100Reader")]
	public static extern int toChannelUnitDA100Reader(int daqda100reader, int chType, int chNo, byte[] strUnit, int lenUnit);
	
	// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
	// Utility
	// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
	
	[DllImport("DAQDA100.dll", CharSet=CharSet.Auto, EntryPoint="toDoubleValueDA100Reader")]
	public static extern double toDoubleValueDA100Reader(int dataValue, int point);
	
	[DllImport("DAQDA100.dll", CharSet=CharSet.Auto, EntryPoint="toStringValueDA100Reader")]
	public static extern int toStringValueDA100Reader(int dataValue, int point, byte[] strValue, int lenValue);
	
	[DllImport("DAQDA100.dll", CharSet=CharSet.Auto, EntryPoint="toAlarmNameDA100Reader")]
	public static extern int toAlarmNameDA100Reader(int iAlarmType, byte[] strAlarm, int lenAlarm);
	
	[DllImport("DAQDA100.dll", CharSet=CharSet.Auto, EntryPoint="alarmMaxLengthDA100Reader")]
	public static extern int alarmMaxLengthDA100Reader();
	
	[DllImport("DAQDA100.dll", CharSet=CharSet.Auto, EntryPoint="versionAPIDA100Reader")]
	public static extern int versionAPIDA100Reader();
	
	[DllImport("DAQDA100.dll", CharSet=CharSet.Auto, EntryPoint="revisionAPIDA100Reader")]
	public static extern int revisionAPIDA100Reader();
	
	[DllImport("DAQDA100.dll", CharSet=CharSet.Auto, EntryPoint="toErrorMessageDA100Reader")]
	public static extern int toErrorMessageDA100Reader(int errCode, byte[] errStr, int errLen);
	
	[DllImport("DAQDA100.dll", CharSet=CharSet.Auto, EntryPoint="errorMaxLengthDA100Reader")]
	public static extern int errorMaxLengthDA100Reader();
	
	// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
}
