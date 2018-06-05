using System;
using System.Runtime.InteropServices;

// DAQDA100.cs
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

public class DAQDA100
{
	// Construct
	public DAQDA100()
	{
	}
	
	// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
	// @see DAQDARWIN
	// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
	
	// communication
	public const int DAQDA100_COMMPORT = 34150;
	
	// total number
	public const int DAQDA100_NUMCHANNEL = 360;
	public const int DAQDA100_NUMALARM = 4;
	public const int DAQDA100_NUMUNIT = 6;
	public const int DAQDA100_NUMSLOT = 6;
	public const int DAQDA100_NUMTERM = 10;
	
	// string length without NULL
	public const int DAQDA100_MAXCHNAMELEN = 3;
	public const int DAQDA100_MAXCHRANGLEN = 6;
	public const int DAQDA100_MAXUNITLEN = 6;
	public const int DAQDA100_MAXMODULELEN = 6;
	public const int DAQDA100_MAXRELAYLEN = DAQDA100_MAXCHNAMELEN;
	
	// maximum value
	public const int DAQDA100_MAXDECIMALPOINT = 4;
	
	// valid
	public const int DAQDA100_VALID_OFF = 0;
	public const int DAQDA100_VALID_ON = 1;
	
	// flag
	public const int DAQDA100_FLAG_OFF = 0x0000;
	public const int DAQDA100_FLAG_ENDDATA = 0x0001;
	
	// data status
	public const int DAQDA100_DATA_UNKNWON = 0x00000000;
	public const int DAQDA100_DATA_NORMAL = 0x00000001;
	public const int DAQDA100_DATA_DIFFINPUT = 0x00000002;
	public const int DAQDA100_DATA_READER = 0x00000003;
	public const int DAQDA100_DATA_PLUSOVER = 0x00007FFF;
	public const int DAQDA100_DATA_MINUSOVER = 0x00008001;
	public const int DAQDA100_DATA_SKIP = 0x00008002;
	public const int DAQDA100_DATA_ILLEGAL = 0x00008003;
	public const int DAQDA100_DATA_ABNORMAL = 0x00008004;
	public const int DAQDA100_DATA_NODATA = 0x00008005;
	
	// alarm type
	public const int DAQDA100_ALARM_NONE = 0;
	public const int DAQDA100_ALARM_UPPER = 1;
	public const int DAQDA100_ALARM_LOWER = 2;
	public const int DAQDA100_ALARM_UPDIFF = 3;
	public const int DAQDA100_ALARM_LOWDIFF = 4;
	public const int DAQDA100_ALARM_INCRATE = 5;
	public const int DAQDA100_ALARM_DECRATE = 6;
	
	// channel/relay type
	public const int DAQDA100_CHTYPE_MAINUNIT = -1;
	public const int DAQDA100_CHTYPE_STANDALONE = 0;
	public const int DAQDA100_CHTYPE_MATHTYPE = 0x0080;
	public const int DAQDA100_CHTYPE_SWITCH = 0x0040;
	public const int DAQDA100_CHTYPE_COMMDATA = 0x0020;
	public const int DAQDA100_CHTYPE_CONSTANT = 0x0010;
	public const int DAQDA100_CHTYPE_REPORT = 0x0100;
	
	// mode
	public const int DAQDA100_MODE_OPE = 0;
	public const int DAQDA100_MODE_SETUP = 1;
	public const int DAQDA100_MODE_CALIB = 2;
	
	// talker output type
	public const int DAQDA100_TALK_MEASUREDDATA = 0;
	public const int DAQDA100_TALK_OPEDATA = 1;
	public const int DAQDA100_TALK_CHINFODATA = 2;
	public const int DAQDA100_TALK_REPORTDATA = 4;
	public const int DAQDA100_TALK_SYSINFODATA = 5;
	public const int DAQDA100_TALK_CALIBDATA = 8;
	public const int DAQDA100_TALK_SETUPDATA = 9;
	
	// status byte
	public const int DAQDA100_STATUS_OFF = 0x0000;
	public const int DAQDA100_STATUS_ADCONV = 0x0001;
	public const int DAQDA100_STATUS_SYNTAX = 0x0002;
	public const int DAQDA100_STATUS_TIMER = 0x0004;
	public const int DAQDA100_STATUS_MEDIA = 0x0008;
	public const int DAQDA100_STATUS_RELEASE = 0x0020;
	public const int DAQDA100_STATUS_SRQ = 0x0040;
	public const int DAQDA100_STATUS_ALL = 0x00FF;
	
	// establish
	public const int DAQDA100_SETUP_ABORT = DAQDA100_VALID_OFF;
	public const int DAQDA100_SETUP_STORE = DAQDA100_VALID_ON;
	
	// unit number
	public const int DAQDA100_UNITNO_MAINUNIT = DAQDA100_CHTYPE_MAINUNIT;
	public const int DAQDA100_UNITNO_STANDALONE = DAQDA100_CHTYPE_STANDALONE;
	
	// computation
	public const int DAQDA100_COMPUTE_START = 0;
	public const int DAQDA100_COMPUTE_STOP = 1;
	public const int DAQDA100_COMPUTE_RESTART = 2;
	public const int DAQDA100_COMPUTE_CLEAR = 3;
	public const int DAQDA100_COMPUTE_RELEASE = 4;
	
	// reporting
	public const int DAQDA100_REPORT_RUN_START = 0;
	public const int DAQDA100_REPORT_RUN_STOP = 1;
	
	// report type
	public const int DAQDA100_REPORT_HOURLY = 0;
	public const int DAQDA100_REPORT_DAILY = 1;
	public const int DAQDA100_REPORT_MONTHLY = 2;
	public const int DAQDA100_REPORT_STATUS = 3;
	
	// report status
	public const int DAQDA100_REPSTATUS_NONE = 0x0000;
	public const int DAQDA100_REPSTATUS_HOURLY_NEW = 0x0001;
	public const int DAQDA100_REPSTATUS_HOURLY_VALID = 0x0002;
	public const int DAQDA100_REPSTATUS_DAILY_NEW = 0x0004;
	public const int DAQDA100_REPSTATUS_DAILY_VALID = 0x0008;
	public const int DAQDA100_REPSTATUS_MONTHLY_NEW = 0x0010;
	public const int DAQDA100_REPSTATUS_MONTHLY_VALID = 0x0020;
	
	// wiring method
	public const int DAQDA100_WIRE_1PH2W = 1;
	public const int DAQDA100_WIRE_1PH3W = 2;
	public const int DAQDA100_WIRE_3PH3W2I = 3;
	public const int DAQDA100_WIRE_3PH3W3I = 4;
	public const int DAQDA100_WIRE_3PH4W = 5;
	
	// mesurement item
	public const int DAQDA100_POWERITEM_I0 = 0x0000;
	public const int DAQDA100_POWERITEM_I1 = 0x0001;
	public const int DAQDA100_POWERITEM_I2 = 0x0002;
	public const int DAQDA100_POWERITEM_I3 = 0x0003;
	public const int DAQDA100_POWERITEM_I13 = 0x000D;
	public const int DAQDA100_POWERITEM_P0 = 0x0010;
	public const int DAQDA100_POWERITEM_P1 = 0x0011;
	public const int DAQDA100_POWERITEM_P2 = 0x0012;
	public const int DAQDA100_POWERITEM_P3 = 0x0013;
	public const int DAQDA100_POWERITEM_P13 = 0x001D;
	public const int DAQDA100_POWERITEM_PF0 = 0x0020;
	public const int DAQDA100_POWERITEM_PF1 = 0x0021;
	public const int DAQDA100_POWERITEM_PF2 = 0x0022;
	public const int DAQDA100_POWERITEM_PF3 = 0x0023;
	public const int DAQDA100_POWERITEM_PF13 = 0x002D;
	public const int DAQDA100_POWERITEM_PH0 = 0x0030;
	public const int DAQDA100_POWERITEM_PH1 = 0x0031;
	public const int DAQDA100_POWERITEM_PH2 = 0x0032;
	public const int DAQDA100_POWERITEM_PH3 = 0x0033;
	public const int DAQDA100_POWERITEM_PH13 = 0x003D;
	public const int DAQDA100_POWERITEM_V0 = 0x0040;
	public const int DAQDA100_POWERITEM_V1 = 0x0041;
	public const int DAQDA100_POWERITEM_V2 = 0x0042;
	public const int DAQDA100_POWERITEM_V3 = 0x0043;
	public const int DAQDA100_POWERITEM_V13 = 0x004D;
	public const int DAQDA100_POWERITEM_VA0 = 0x0050;
	public const int DAQDA100_POWERITEM_VA1 = 0x0051;
	public const int DAQDA100_POWERITEM_VA2 = 0x0052;
	public const int DAQDA100_POWERITEM_VA3 = 0x0053;
	public const int DAQDA100_POWERITEM_VA13 = 0x005D;
	public const int DAQDA100_POWERITEM_VAR0 = 0x0060;
	public const int DAQDA100_POWERITEM_VAR1 = 0x0061;
	public const int DAQDA100_POWERITEM_VAR2 = 0x0062;
	public const int DAQDA100_POWERITEM_VAR3 = 0x0063;
	public const int DAQDA100_POWERITEM_VAR13 = 0x006D;
	public const int DAQDA100_POWERITEM_FREQ = 0x007F;
	
	// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
	// DA100 specifications
	// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
	
	// number
	public const int DAQDA100_NUMCH_BYUNIT = DAQDA100_NUMSLOT * DAQDA100_NUMTERM;
	
	// all
	public const int DAQDA100_CHTYPE_MEASALL = 0x0F;
	public const int DAQDA100_CHNO_ALL = -1;
	public const int DAQDA100_LEVELNO_ALL = -1;
	
	// code
	public const int DAQDA100_CODE_BINARY = 0;
	public const int DAQDA100_CODE_ASCII = 1;
	
	// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
	// range
	// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
	
	// range type
	public const int DAQDA100_RANGETYPE_VOLT = 0x00000000;
	public const int DAQDA100_RANGETYPE_DI = 0x00010000;
	public const int DAQDA100_RANGETYPE_TC = 0x00020000;
	public const int DAQDA100_RANGETYPE_RTD = 0x00040000;
	public const int DAQDA100_RANGETYPE_SKIP = 0x00080000;
	public const int DAQDA100_RANGETYPE_MA = 0x00100000;
	public const int DAQDA100_RANGETYPE_POWER = 0x00200000;
	public const int DAQDA100_RANGETYPE_STRAIN = 0x00400000;
	public const int DAQDA100_RANGETYPE_PULSE = 0x00800000;
	
	// SKIP
	public const int DAQDA100_RANGE_SKIP = DAQDA100_RANGETYPE_SKIP;
	
	// VOLT
	public const int DAQDA100_RANGE_VOLT_20MV = DAQDA100_RANGETYPE_VOLT + 1;
	public const int DAQDA100_RANGE_VOLT_60MV = DAQDA100_RANGETYPE_VOLT + 2;
	public const int DAQDA100_RANGE_VOLT_200MV = DAQDA100_RANGETYPE_VOLT + 3;
	public const int DAQDA100_RANGE_VOLT_2V = DAQDA100_RANGETYPE_VOLT + 4;
	public const int DAQDA100_RANGE_VOLT_6V = DAQDA100_RANGETYPE_VOLT + 5;
	public const int DAQDA100_RANGE_VOLT_20V = DAQDA100_RANGETYPE_VOLT + 6;
	public const int DAQDA100_RANGE_VOLT_50V = DAQDA100_RANGETYPE_VOLT + 7;
	
	// TC
	public const int DAQDA100_RANGE_TC_R = DAQDA100_RANGETYPE_TC + 1;
	public const int DAQDA100_RANGE_TC_S = DAQDA100_RANGETYPE_TC + 2;
	public const int DAQDA100_RANGE_TC_B = DAQDA100_RANGETYPE_TC + 3;
	public const int DAQDA100_RANGE_TC_K = DAQDA100_RANGETYPE_TC + 4;
	public const int DAQDA100_RANGE_TC_E = DAQDA100_RANGETYPE_TC + 5;
	public const int DAQDA100_RANGE_TC_J = DAQDA100_RANGETYPE_TC + 6;
	public const int DAQDA100_RANGE_TC_T = DAQDA100_RANGETYPE_TC + 7;
	public const int DAQDA100_RANGE_TC_N = DAQDA100_RANGETYPE_TC + 8;
	public const int DAQDA100_RANGE_TC_W = DAQDA100_RANGETYPE_TC + 9;
	public const int DAQDA100_RANGE_TC_L = DAQDA100_RANGETYPE_TC + 10;
	public const int DAQDA100_RANGE_TC_U = DAQDA100_RANGETYPE_TC + 11;
	public const int DAQDA100_RANGE_TC_KP = DAQDA100_RANGETYPE_TC + 12;
	
	// RTD
	public const int DAQDA100_RANGE_RTD_1MAPT = DAQDA100_RANGETYPE_RTD + 1;
	public const int DAQDA100_RANGE_RTD_2MAPT = DAQDA100_RANGETYPE_RTD + 2;
	public const int DAQDA100_RANGE_RTD_1MAJPT = DAQDA100_RANGETYPE_RTD + 3;
	public const int DAQDA100_RANGE_RTD_2MAJPT = DAQDA100_RANGETYPE_RTD + 4;
	public const int DAQDA100_RANGE_RTD_2MAPT50 = DAQDA100_RANGETYPE_RTD + 5;
	public const int DAQDA100_RANGE_RTD_1MAPTH = DAQDA100_RANGETYPE_RTD + 6;
	public const int DAQDA100_RANGE_RTD_2MAPTH = DAQDA100_RANGETYPE_RTD + 7;
	public const int DAQDA100_RANGE_RTD_1MAJPTH = DAQDA100_RANGETYPE_RTD + 8;
	public const int DAQDA100_RANGE_RTD_2MAJPTH = DAQDA100_RANGETYPE_RTD + 9;
	public const int DAQDA100_RANGE_RTD_1MANIS = DAQDA100_RANGETYPE_RTD + 10;
	public const int DAQDA100_RANGE_RTD_1MANID = DAQDA100_RANGETYPE_RTD + 11;
	public const int DAQDA100_RANGE_RTD_1MANI120 = DAQDA100_RANGETYPE_RTD + 12;
	public const int DAQDA100_RANGE_RTD_CU10GE = DAQDA100_RANGETYPE_RTD + 13;
	public const int DAQDA100_RANGE_RTD_CU10LN = DAQDA100_RANGETYPE_RTD + 14;
	public const int DAQDA100_RANGE_RTD_CU10WEED = DAQDA100_RANGETYPE_RTD + 15;
	public const int DAQDA100_RANGE_RTD_CU10BAILEY = DAQDA100_RANGETYPE_RTD + 16;
	public const int DAQDA100_RANGE_RTD_J263B = DAQDA100_RANGETYPE_RTD + 17;
	
	// DI
	public const int DAQDA100_RANGE_DI_LEVEL = DAQDA100_RANGETYPE_DI + 1;
	public const int DAQDA100_RANGE_DI_CONTACT = DAQDA100_RANGETYPE_DI + 2;
	
	// mA
	public const int DAQDA100_RANGE_MA_20MA = DAQDA100_RANGETYPE_MA + 1;
	
	// POWER
	public const int DAQDA100_RANGE_POWER_25V05A = DAQDA100_RANGETYPE_POWER + 1;
	public const int DAQDA100_RANGE_POWER_25V5A = DAQDA100_RANGETYPE_POWER + 2;
	public const int DAQDA100_RANGE_POWER_250V05A = DAQDA100_RANGETYPE_POWER + 3;
	public const int DAQDA100_RANGE_POWER_250V5A = DAQDA100_RANGETYPE_POWER + 4;
	
	// STRAIN
	public const int DAQDA100_RANGE_STRAIN_2K = DAQDA100_RANGETYPE_STRAIN + 1;
	public const int DAQDA100_RANGE_STRAIN_20K = DAQDA100_RANGETYPE_STRAIN + 2;
	public const int DAQDA100_RANGE_STRAIN_200K = DAQDA100_RANGETYPE_STRAIN + 3;
	
	// PULS
	public const int DAQDA100_RANGE_PULSE_RATE = DAQDA100_RANGETYPE_PULSE + 1;
	public const int DAQDA100_RANGE_PULSE_GATE = DAQDA100_RANGETYPE_PULSE + 2;
	
	// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
	// Connection
	// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
	
	[DllImport("DAQDA100.dll", CharSet=CharSet.Auto, EntryPoint="openDA100")]
	public static extern int openDA100(byte[] strAddress, out int errorCode);
	
	[DllImport("DAQDA100.dll", CharSet=CharSet.Auto, EntryPoint="closeDA100")]
	public static extern int closeDA100(int daqda100);
	
	[DllImport("DAQDA100.dll", CharSet=CharSet.Auto, EntryPoint="sendLineDA100")]
	public static extern int sendLineDA100(int daqda100, byte[] strLine);
	
	[DllImport("DAQDA100.dll", CharSet=CharSet.Auto, EntryPoint="receiveLineDA100")]
	public static extern int receiveLineDA100(int daqda100, byte[] strLine, int maxLine, out int lenLine);
	
	[DllImport("DAQDA100.dll", CharSet=CharSet.Auto, EntryPoint="receiveByteDA100")]
	public static extern int receiveByteDA100(int daqda100, byte[] byteData, int maxData, out int lenData);
	
	[DllImport("DAQDA100.dll", CharSet=CharSet.Auto, EntryPoint="sendTriggerDA100")]
	public static extern int sendTriggerDA100(int daqda100);
	
	[DllImport("DAQDA100.dll", CharSet=CharSet.Auto, EntryPoint="updateStatusDA100")]
	public static extern int updateStatusDA100(int daqda100);
	
	[DllImport("DAQDA100.dll", CharSet=CharSet.Auto, EntryPoint="runCommandDA100")]
	public static extern int runCommandDA100(int daqda100, byte[] strCmd);
	
	// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
	// Control
	// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
	
	[DllImport("DAQDA100.dll", CharSet=CharSet.Auto, EntryPoint="switchModeDA100")]
	public static extern int switchModeDA100(int daqda100, int iMode);
	
	[DllImport("DAQDA100.dll", CharSet=CharSet.Auto, EntryPoint="switchCodeDA100")]
	public static extern int switchCodeDA100(int daqda100, int iCode);
	
	[DllImport("DAQDA100.dll", CharSet=CharSet.Auto, EntryPoint="reconstructDA100")]
	public static extern int reconstructDA100(int daqda100);
	
	[DllImport("DAQDA100.dll", CharSet=CharSet.Auto, EntryPoint="initSetValueDA100")]
	public static extern int initSetValueDA100(int daqda100);
	
	[DllImport("DAQDA100.dll", CharSet=CharSet.Auto, EntryPoint="ackAlarmDA100")]
	public static extern int ackAlarmDA100(int daqda100);
	
	[DllImport("DAQDA100.dll", CharSet=CharSet.Auto, EntryPoint="setDateTimeNowDA100")]
	public static extern int setDateTimeNowDA100(int daqda100);
	
	[DllImport("DAQDA100.dll", CharSet=CharSet.Auto, EntryPoint="switchComputeDA100")]
	public static extern int switchComputeDA100(int daqda100, int iCode);
	
	[DllImport("DAQDA100.dll", CharSet=CharSet.Auto, EntryPoint="switchReportDA100")]
	public static extern int switchReportDA100(int daqda100, int iReportRun);
	
	[DllImport("DAQDA100.dll", CharSet=CharSet.Auto, EntryPoint="establishDA100")]
	public static extern int establishDA100(int daqda100, int iSetup);
	
	// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
	// Set on Operation Mode
	// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
	
	[DllImport("DAQDA100.dll", CharSet=CharSet.Auto, EntryPoint="setRangeDA100")]
	public static extern int setRangeDA100(int daqda100, int chType, int chNo, int iRange, int spanMin, int spanMax, int scaleMin, int scaleMax, int scalePoint, int bFilter, int iItem, int iWire);
	
	[DllImport("DAQDA100.dll", CharSet=CharSet.Auto, EntryPoint="setChDELTADA100")]
	public static extern int setChDELTADA100(int daqda100, int chType, int chNo, int refChNo, int spanMin, int spanMax);
	
	[DllImport("DAQDA100.dll", CharSet=CharSet.Auto, EntryPoint="setChRRJCDA100")]
	public static extern int setChRRJCDA100(int daqda100, int chType, int chNo, int refChNo, int spanMin, int spanMax);
	
	[DllImport("DAQDA100.dll", CharSet=CharSet.Auto, EntryPoint="setChUnitDA100")]
	public static extern int setChUnitDA100(int daqda100, int chType, int chNo, byte[] strUnit);
	
	[DllImport("DAQDA100.dll", CharSet=CharSet.Auto, EntryPoint="setChAlarmDA100")]
	public static extern int setChAlarmDA100(int daqda100, int chType, int chNo, int levelNo, int iAlarmType, int value, int relayType, int relayNo);
	
	// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
	// Measurement
	// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
	
	[DllImport("DAQDA100.dll", CharSet=CharSet.Auto, EntryPoint="measInstChDA100")]
	public static extern int measInstChDA100(int daqda100, int chType, int chNo);
	
	[DllImport("DAQDA100.dll", CharSet=CharSet.Auto, EntryPoint="mathInstChDA100")]
	public static extern int mathInstChDA100(int daqda100, int chNo);
	
	[DllImport("DAQDA100.dll", CharSet=CharSet.Auto, EntryPoint="measInfoChDA100")]
	public static extern int measInfoChDA100(int daqda100, int chType, int chNo);
	
	[DllImport("DAQDA100.dll", CharSet=CharSet.Auto, EntryPoint="mathInfoChDA100")]
	public static extern int mathInfoChDA100(int daqda100, int chNo);
	
	[DllImport("DAQDA100.dll", CharSet=CharSet.Auto, EntryPoint="updateSystemConfigDA100")]
	public static extern int updateSystemConfigDA100(int daqda100);
	
	[DllImport("DAQDA100.dll", CharSet=CharSet.Auto, EntryPoint="updateReportStatusDA100")]
	public static extern int updateReportStatusDA100(int daqda100);
	
	// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
	// Talker
	// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
	
	[DllImport("DAQDA100.dll", CharSet=CharSet.Auto, EntryPoint="talkOperationChDataDA100")]
	public static extern int talkOperationChDataDA100(int daqda100, int chType, int chNo);
	
	[DllImport("DAQDA100.dll", CharSet=CharSet.Auto, EntryPoint="talkOperationDataDA100")]
	public static extern int talkOperationDataDA100(int daqda100, int startChType, int startChNo, int endChType, int endChNo);
	
	[DllImport("DAQDA100.dll", CharSet=CharSet.Auto, EntryPoint="talkSetupChDataDA100")]
	public static extern int talkSetupChDataDA100(int daqda100, int chType, int chNo);
	
	[DllImport("DAQDA100.dll", CharSet=CharSet.Auto, EntryPoint="talkSetupDataDA100")]
	public static extern int talkSetupDataDA100(int daqda100, int startChType, int startChNo, int endChType, int endChNo);
	
	[DllImport("DAQDA100.dll", CharSet=CharSet.Auto, EntryPoint="talkCalibrationChDataDA100")]
	public static extern int talkCalibrationChDataDA100(int daqda100, int chType, int chNo);
	
	[DllImport("DAQDA100.dll", CharSet=CharSet.Auto, EntryPoint="talkCalibrationDataDA100")]
	public static extern int talkCalibrationDataDA100(int daqda100, int startChType, int startChNo, int endChType, int endChNo);
	
	[DllImport("DAQDA100.dll", CharSet=CharSet.Auto, EntryPoint="getSetDataByLineDA100")]
	public static extern int getSetDataByLineDA100(int daqda100, byte[] strLine, int maxLine, out int lenLine, out int pFlag);
	
	// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
	// Get Data
	// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
	
	[DllImport("DAQDA100.dll", CharSet=CharSet.Auto, EntryPoint="dataValueDA100")]
	public static extern int dataValueDA100(int daqda100, int chType, int chNo);
	
	[DllImport("DAQDA100.dll", CharSet=CharSet.Auto, EntryPoint="dataStatusDA100")]
	public static extern int dataStatusDA100(int daqda100, int chType, int chNo);
	
	[DllImport("DAQDA100.dll", CharSet=CharSet.Auto, EntryPoint="dataAlarmDA100")]
	public static extern int dataAlarmDA100(int daqda100, int chType, int chNo, int levelNo);
	
	[DllImport("DAQDA100.dll", CharSet=CharSet.Auto, EntryPoint="dataDoubleValueDA100")]
	public static extern double dataDoubleValueDA100(int daqda100, int chType, int chNo);
	
	[DllImport("DAQDA100.dll", CharSet=CharSet.Auto, EntryPoint="dataStringValueDA100")]
	public static extern int dataStringValueDA100(int daqda100, int chType, int chNo, byte[] strValue, int lenValue);
	
	[DllImport("DAQDA100.dll", CharSet=CharSet.Auto, EntryPoint="dataYearDA100")]
	public static extern int dataYearDA100(int daqda100, int chType, int chNo);
	
	[DllImport("DAQDA100.dll", CharSet=CharSet.Auto, EntryPoint="dataMonthDA100")]
	public static extern int dataMonthDA100(int daqda100, int chType, int chNo);
	
	[DllImport("DAQDA100.dll", CharSet=CharSet.Auto, EntryPoint="dataDayDA100")]
	public static extern int dataDayDA100(int daqda100, int chType, int chNo);
	
	[DllImport("DAQDA100.dll", CharSet=CharSet.Auto, EntryPoint="dataHourDA100")]
	public static extern int dataHourDA100(int daqda100, int chType, int chNo);
	
	[DllImport("DAQDA100.dll", CharSet=CharSet.Auto, EntryPoint="dataMinuteDA100")]
	public static extern int dataMinuteDA100(int daqda100, int chType, int chNo);
	
	[DllImport("DAQDA100.dll", CharSet=CharSet.Auto, EntryPoint="dataSecondDA100")]
	public static extern int dataSecondDA100(int daqda100, int chType, int chNo);
	
	[DllImport("DAQDA100.dll", CharSet=CharSet.Auto, EntryPoint="alarmTypeDA100")]
	public static extern int alarmTypeDA100(int daqda100, int chType, int chNo, int levelNo);
	
	// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
	// Get Channel Information
	// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
	
	[DllImport("DAQDA100.dll", CharSet=CharSet.Auto, EntryPoint="channelPointDA100")]
	public static extern int channelPointDA100(int daqda100, int chType, int chNo);
	
	[DllImport("DAQDA100.dll", CharSet=CharSet.Auto, EntryPoint="channelStatusDA100")]
	public static extern int channelStatusDA100(int daqda100, int chType, int chNo);
	
	[DllImport("DAQDA100.dll", CharSet=CharSet.Auto, EntryPoint="toChannelUnitDA100")]
	public static extern int toChannelUnitDA100(int daqda100, int chType, int chNo, byte[] strUnit, int lenUnit);
	
	// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
	// Get System Information
	// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
	
	[DllImport("DAQDA100.dll", CharSet=CharSet.Auto, EntryPoint="unitIntervalDA100")]
	public static extern double unitIntervalDA100(int daqda100);
	
	[DllImport("DAQDA100.dll", CharSet=CharSet.Auto, EntryPoint="unitValidDA100")]
	public static extern int unitValidDA100(int daqda100, int unitNo);
	
	[DllImport("DAQDA100.dll", CharSet=CharSet.Auto, EntryPoint="moduleCodeDA100")]
	public static extern int moduleCodeDA100(int daqda100, int unitNo, int slotNo);
	
	[DllImport("DAQDA100.dll", CharSet=CharSet.Auto, EntryPoint="toModuleNameDA100")]
	public static extern int toModuleNameDA100(int daqda100, int unitNo, int slotNo, byte[] strName, int lenName);
	
	// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
	// Get Status
	// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
	
	[DllImport("DAQDA100.dll", CharSet=CharSet.Auto, EntryPoint="statusByteDA100")]
	public static extern int statusByteDA100(int daqda100);
	
	[DllImport("DAQDA100.dll", CharSet=CharSet.Auto, EntryPoint="statusCodeDA100")]
	public static extern int statusCodeDA100(int daqda100);
	
	[DllImport("DAQDA100.dll", CharSet=CharSet.Auto, EntryPoint="statusReportDA100")]
	public static extern int statusReportDA100(int daqda100);
	
	// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
	// Utility
	// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
	
	[DllImport("DAQDA100.dll", CharSet=CharSet.Auto, EntryPoint="toDoubleValueDA100")]
	public static extern double toDoubleValueDA100(int dataValue, int point);
	
	[DllImport("DAQDA100.dll", CharSet=CharSet.Auto, EntryPoint="toStringValueDA100")]
	public static extern int toStringValueDA100(int dataValue, int point, byte[] strValue, int lenValue);
	
	[DllImport("DAQDA100.dll", CharSet=CharSet.Auto, EntryPoint="toAlarmNameDA100")]
	public static extern int toAlarmNameDA100(int iAlarmType, byte[] strAlarm, int lenAlarm);
	
	[DllImport("DAQDA100.dll", CharSet=CharSet.Auto, EntryPoint="alarmMaxLengthDA100")]
	public static extern int alarmMaxLengthDA100();
	
	[DllImport("DAQDA100.dll", CharSet=CharSet.Auto, EntryPoint="versionAPIDA100")]
	public static extern int versionAPIDA100();
	
	[DllImport("DAQDA100.dll", CharSet=CharSet.Auto, EntryPoint="revisionAPIDA100")]
	public static extern int revisionAPIDA100();
	
	[DllImport("DAQDA100.dll", CharSet=CharSet.Auto, EntryPoint="toErrorMessageDA100")]
	public static extern int toErrorMessageDA100(int errCode, byte[] errStr, int errLen);
	
	[DllImport("DAQDA100.dll", CharSet=CharSet.Auto, EntryPoint="errorMaxLengthDA100")]
	public static extern int errorMaxLengthDA100();
	
	// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
}
