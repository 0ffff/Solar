using System;
using System.Runtime.InteropServices;

// DAQMX100.cs
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
//
// Copyright(c) 2004-2007 Yokogawa Electric Corporation. All rights reserved.
//
// This is defined export DAQMX100.dll.
// Declare Functions for C#
//
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
// 2007/09/30 Ver.3 Rev.1
// 2004/11/01 Ver.2 Rev.1
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --

public class DAQMX100
{
	// Construct
	public DAQMX100()
	{
	}
	
	// specified
	public const int DAQMX100_LIST_ALL = -1;
	public const int DAQMX100_LIST_CURRENT = -1;
	
	// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
	// @see DAQMX
	// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
	
	// total number
	public const int DAQMX100_NUMMODULE = 6;//模块数量
	public const int DAQMX100_NUMCHANNEL = 60;//通道数量
	public const int DAQMX100_NUMDO = DAQMX100_NUMCHANNEL;
	public const int DAQMX100_NUMFIFO = 3;//FIFO数量
	public const int DAQMX100_NUMALARM = 4;//警报数量
	public const int DAQMX100_NUMSEGMENT = 2;//数码管数量
	public const int DAQMX100_NUMMACADDR = 6;
	public const int DAQMX100_NUMAOPWM = DAQMX100_NUMCHANNEL;
	public const int DAQMX100_NUMBALANCE = DAQMX100_NUMCHANNEL;
	public const int DAQMX100_NUMOUTPUT = DAQMX100_NUMCHANNEL;
	
	// string length without NULL
	public const int DAQMX100_MAXHOSTNAMELEN = 64;
	public const int DAQMX100_MAXUNITLEN = 6;
	public const int DAQMX100_MAXTAGLEN = 15;
	public const int DAQMX100_MAXCOMMENTLEN = 30;
	public const int DAQMX100_MAXSERIALLEN = 9;
	public const int DAQMX100_MAXPARTNOLEN = 7;
	
	// maximum number
	public const int DAQMX100_MAXDECIMALPOINT = 4;
	public const int DAQMX100_MAXDISPTIME = 120000;
	public const int DAQMX100_MAXPULSETIME = 30000;
	
	// constant value
	public const int DAQMX100_INSTANTANEOUS = -1;
	public const int DAQMX100_REFCHNO_NONE = 0;
	public const int DAQMX100_REFCHNO_ALL = -1;
	public const int DAQMX100_LEVELNO_ALL = -1;
	public const int DAQMX100_DONO_ALL = -1;
	public const int DAQMX100_SEGMENTNO_ALL = -1;
	public const int DAQMX100_CHNO_ALL = -1;
	public const int DAQMX100_MODULENO_ALL = -1;
	public const int DAQMX100_FIFONO_ALL = -1;
	public const int DAQMX100_AOPWMNO_ALL = -1;
	public const int DAQMX100_BALANCENO_ALL = -1;
	public const int DAQMX100_OUTPUTNO_ALL = -1;
	
	// valid
	public const int DAQMX100_VALID_OFF = 0;
	public const int DAQMX100_VALID_ON = 1;
	
	// flag : not use
	
	// data status
	public const int DAQMX100_DATA_UNKNWON = 0x00000000;
	public const int DAQMX100_DATA_NORMAL = 0x00000001;
	public const int DAQMX100_DATA_PLUSOVER = 0x00007FFF;
	public const int DAQMX100_DATA_MINUSOVER = 0x00008001;
	public const int DAQMX100_DATA_SKIP = 0x00008002;
	public const int DAQMX100_DATA_ILLEGAL = 0x00008003;
	public const int DAQMX100_DATA_NODATA = 0x00008005;
	public const int DAQMX100_DATA_LACK = 0x00008400;
	public const int DAQMX100_DATA_INVALID = 0x00008700;
	
	// alarm type
	public const int DAQMX100_ALARM_NONE = 0;
	public const int DAQMX100_ALARM_UPPER = 1;
	public const int DAQMX100_ALARM_LOWER = 2;
	public const int DAQMX100_ALARM_UPDIFF = 3;
	public const int DAQMX100_ALARM_LOWDIFF = 4;
	
	// system control : @see each command function
	
	// channel kind
	public const int DAQMX100_CHKIND_NONE = 0x0000;
	public const int DAQMX100_CHKIND_AI = 0x0010;
	public const int DAQMX100_CHKIND_AIDIFF = 0x0011;
	public const int DAQMX100_CHKIND_AIRJC = 0x0012;
	public const int DAQMX100_CHKIND_DI = 0x0030;
	public const int DAQMX100_CHKIND_DIDIFF = 0x0031;
	public const int DAQMX100_CHKIND_DO = 0x0040;
	public const int DAQMX100_CHKIND_DOCOM = 0x0041;
	public const int DAQMX100_CHKIND_DOFAIL = 0x0042;
	public const int DAQMX100_CHKIND_DOERR = 0x0043;
	public const int DAQMX100_CHKIND_AO = 0x0020;
	public const int DAQMX100_CHKIND_AOCOM = 0x0021;
	public const int DAQMX100_CHKIND_PWM = 0x0060;
	public const int DAQMX100_CHKIND_PWMCOM = 0x0061;
	public const int DAQMX100_CHKIND_PI = 0x0050;
	public const int DAQMX100_CHKIND_PIDIFF = 0x0051;
	public const int DAQMX100_CHKIND_CI = 0x0070;
	public const int DAQMX100_CHKIND_CIDIFF = 0x0071;
	
	// scale type
	public const int DAQMX100_SCALE_NONE = 0;
	public const int DAQMX100_SCALE_LINER = 1;
	
	// module type
	// 0xF0000010 -> -268435440
	// 0xF0001C10 -> -268428272
	// 0xB0101F10 -> -1341120752
	// 0xD0001130 -> -805301968
	// 0x0000FF00 -> 65280
	public const int DAQMX100_MODULE_NONE = 0x0;
	public const int DAQMX100_MODULE_MX110UNVH04 = -268435440;
	public const int DAQMX100_MODULE_MX110UNVM10 = -268428272;
	public const int DAQMX100_MODULE_MX115D05H10 = 0x10003010;
	public const int DAQMX100_MODULE_MX125MKCM10 = 0x00402010;
	public const int DAQMX100_MODULE_MX110V4RM06 = -1341120752;
	public const int DAQMX100_MODULE_MX112NDIM04 = 0x01004010;
	public const int DAQMX100_MODULE_MX112B35M04 = 0x01004110;
	public const int DAQMX100_MODULE_MX112B12M04 = 0x01004210;
	public const int DAQMX100_MODULE_MX115D24H10 = 0x10003210;
	public const int DAQMX100_MODULE_MX120VAOM08 = 0x0080C010;
	public const int DAQMX100_MODULE_MX120PWMM08 = 0x0020C810;
	public const int DAQMX100_MODULE_HIDDEN = 65280;
	public const int DAQMX100_MODULE_MX114PLSM10 = 0x0400B010;
	public const int DAQMX100_MODULE_MX110VTDL30 = -805301968;
	public const int DAQMX100_MODULE_MX118CANM10 = 0x00085110;
	public const int DAQMX100_MODULE_MX118CANM20 = 0x00085220;
	public const int DAQMX100_MODULE_MX118CANM30 = 0x00085330;
	public const int DAQMX100_MODULE_MX118CANSUB = 0x00085000;
	public const int DAQMX100_MODULE_MX118CANMERR = 0x00005A10;
	public const int DAQMX100_MODULE_MX118CANSERR = 0x00005B10;
	
	// how many channels by each module
	public const int DAQMX100_CHNUM_0 = 0;
	public const int DAQMX100_CHNUM_4 = 4;
	public const int DAQMX100_CHNUM_6 = 6;
	public const int DAQMX100_CHNUM_8 = 8;
	public const int DAQMX100_CHNUM_10 = 10;
	public const int DAQMX100_CHNUM_30 = 30;
	
	// interval(msec)
	public const int DAQMX100_INTERVAL_10 = 10;
	public const int DAQMX100_INTERVAL_50 = 50;
	public const int DAQMX100_INTERVAL_100 = 100;
	public const int DAQMX100_INTERVAL_200 = 200;
	public const int DAQMX100_INTERVAL_500 = 500;
	public const int DAQMX100_INTERVAL_1000 = 1000;
	public const int DAQMX100_INTERVAL_2000 = 2000;
	public const int DAQMX100_INTERVAL_5000 = 5000;
	public const int DAQMX100_INTERVAL_10000 = 10000;
	public const int DAQMX100_INTERVAL_20000 = 20000;
	public const int DAQMX100_INTERVAL_30000 = 30000;
	public const int DAQMX100_INTERVAL_60000 = 60000;
	
	// filter
	public const int DAQMX100_FILTER_0 = 0;
	public const int DAQMX100_FILTER_5 = 1;
	public const int DAQMX100_FILTER_10 = 2;
	public const int DAQMX100_FILTER_20 = 3;
	public const int DAQMX100_FILTER_25 = 4;
	public const int DAQMX100_FILTER_40 = 5;
	public const int DAQMX100_FILTER_50 = 6;
	public const int DAQMX100_FILTER_100 = 7;
	
	// RJC Type
	public const int DAQMX100_RJC_INTERNAL = 0;
	public const int DAQMX100_RJC_EXTERNAL = 1;
	
	// burnout
	public const int DAQMX100_BURNOUT_OFF = 0;
	public const int DAQMX100_BURNOUT_UP = 1;
	public const int DAQMX100_BURNOUT_DOWN = 2;
	
	// unit type
	public const int DAQMX100_UNITTYPE_NONE = 0x00000000;
	public const int DAQMX100_UNITTYPE_MX100 = 0x00010000;
	
	// terminal type
	public const int DAQMX100_TERMINAL_SCREW = 0;
	public const int DAQMX100_TERMINAL_CLAMP = 1;
	public const int DAQMX100_TERMINAL_NDIS = 2;
	
	// AD
	public const int DAQMX100_INTEGRAL_AUTO = 0;
	public const int DAQMX100_INTEGRAL_50HZ = 1;
	public const int DAQMX100_INTEGRAL_60HZ = 2;
	
	// temparature unit
	public const int DAQMX100_TEMPUNIT_C = 0;
	public const int DAQMX100_TEMPUNIT_F = 1;
	
	// CF write mode
	public const int DAQMX100_CFWRITEMODE_ONCE = 0;
	public const int DAQMX100_CFWRITEMODE_FIFO = 1;
	
	// CF status
	public const int DAQMX100_CFSTATUS_NONE = 0x0000;
	public const int DAQMX100_CFSTATUS_EXIST = 0x0001;
	public const int DAQMX100_CFSTATUS_USE = 0x0002;
	public const int DAQMX100_CFSTATUS_FORMAT = 0x0004;
	
	// UNIT status
	public const int DAQMX100_UNITSTAT_NONE = 0x0000;
	public const int DAQMX100_UNITSTAT_INIT = 0x0001;
	public const int DAQMX100_UNITSTAT_STOP = 0x0002;
	public const int DAQMX100_UNITSTAT_RUN = 0x0003;
	public const int DAQMX100_UNITSTAT_BACKUP = 0x0004;
	
	// FIFO status
	public const int DAQMX100_FIFOSTAT_NONE = DAQMX100_UNITSTAT_NONE;
	public const int DAQMX100_FIFOSTAT_INIT = DAQMX100_UNITSTAT_INIT;
	public const int DAQMX100_FIFOSTAT_STOP = DAQMX100_UNITSTAT_STOP;
	public const int DAQMX100_FIFOSTAT_RUN = DAQMX100_UNITSTAT_RUN;
	public const int DAQMX100_FIFOSTAT_BACKUP = DAQMX100_UNITSTAT_BACKUP;
	
	// segment display type
	public const int DAQMX100_DISPTYPE_NONE = 0;
	public const int DAQMX100_DISPTYPE_ON = 1;
	public const int DAQMX100_DISPTYPE_BLINK = 2;
	
	// choice
	public const int DAQMX100_CHOICE_PREV = 0;
	public const int DAQMX100_CHOICE_PRESET = 1;
	
	// transmit
	public const int DAQMX100_TRANSMIT_NONE = 0;
	public const int DAQMX100_TRANSMIT_RUN = 1;
	public const int DAQMX100_TRANSMIT_STOP = 2;
	
	// balance
	public const int DAQMX100_BALANCE_NONE = 0;
	public const int DAQMX100_BALANCE_DONE = 1;
	public const int DAQMX100_BALANCE_NG = 2;
	public const int DAQMX100_BALANCE_ERROR = 3;
	
	// option
	public const int DAQMX100_OPTION_NONE = 0x0000;
	public const int DAQMX100_OPTION_DS = 0x0001;
	
	// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
	// range
	// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
	
	// range type for special
	public const int DAQMX100_RANGETYPE_DI = 0x00010000;
	public const int DAQMX100_RANGETYPE_SKIP = 0x00080000;
	
	// DI : special
	public const int DAQMX100_RANGE_DI_LEVEL = DAQMX100_RANGETYPE_DI + 0x0001;
	public const int DAQMX100_RANGE_DI_CONTACT = DAQMX100_RANGETYPE_DI + 0x0002;
	
	// Skip
	public const int DAQMX100_RANGE_SKIP = DAQMX100_RANGETYPE_SKIP;
	
	// Reference
	public const int DAQMX100_RANGE_REFERENCE = -1;
	
	// Volt
	public const int DAQMX100_RANGE_VOLT_20MV = 0x0000;
	public const int DAQMX100_RANGE_VOLT_60MV = 0x0001;
	public const int DAQMX100_RANGE_VOLT_200MV = 0x0002;
	public const int DAQMX100_RANGE_VOLT_2V = 0x0003;
	public const int DAQMX100_RANGE_VOLT_6V = 0x0004;
	public const int DAQMX100_RANGE_VOLT_20V = 0x0005;
	public const int DAQMX100_RANGE_VOLT_100V = 0x0006;
	public const int DAQMX100_RANGE_VOLT_60MVH = 0x0007;
	public const int DAQMX100_RANGE_VOLT_1V = 0x0008;
	public const int DAQMX100_RANGE_VOLT_6VH = 0x0009;
	
	// TC
	public const int DAQMX100_RANGE_TC_R = 0x0200;
	public const int DAQMX100_RANGE_TC_S = 0x0201;
	public const int DAQMX100_RANGE_TC_B = 0x0202;
	public const int DAQMX100_RANGE_TC_K = 0x0203;
	public const int DAQMX100_RANGE_TC_E = 0x0204;
	public const int DAQMX100_RANGE_TC_J = 0x0205;
	public const int DAQMX100_RANGE_TC_T = 0x0206;
	public const int DAQMX100_RANGE_TC_N = 0x0207;
	public const int DAQMX100_RANGE_TC_W = 0x0208;
	public const int DAQMX100_RANGE_TC_L = 0x0209;
	public const int DAQMX100_RANGE_TC_U = 0x020A;
	public const int DAQMX100_RANGE_TC_KP = 0x020B;
	public const int DAQMX100_RANGE_TC_PL = 0x020C;
	public const int DAQMX100_RANGE_TC_PR = 0x020D;
	public const int DAQMX100_RANGE_TC_NNM = 0x020E;
	public const int DAQMX100_RANGE_TC_WR = 0x020F;
	public const int DAQMX100_RANGE_TC_WWR = 0x0210;
	public const int DAQMX100_RANGE_TC_AWG = 0x0211;
	public const int DAQMX100_RANGE_TC_XK = 0x0212;
	
	// RTD 1mA
	public const int DAQMX100_RANGE_RTD_1MAPT = 0x0300;
	public const int DAQMX100_RANGE_RTD_1MAJPT = 0x0301;
	public const int DAQMX100_RANGE_RTD_1MAPTH = 0x0302;
	public const int DAQMX100_RANGE_RTD_1MAJPTH = 0x0303;
	public const int DAQMX100_RANGE_RTD_1MANIS = 0x0304;
	public const int DAQMX100_RANGE_RTD_1MANID = 0x0305;
	public const int DAQMX100_RANGE_RTD_1MANI120 = 0x0306;
	public const int DAQMX100_RANGE_RTD_1MAPT50 = 0x0307;
	public const int DAQMX100_RANGE_RTD_1MACU10GE = 0x0308;
	public const int DAQMX100_RANGE_RTD_1MACU10LN = 0x0309;
	public const int DAQMX100_RANGE_RTD_1MACU10WEED = 0x030A;
	public const int DAQMX100_RANGE_RTD_1MACU10BAILEY = 0x030B;
	public const int DAQMX100_RANGE_RTD_1MAJ263B = 0x030C;
	public const int DAQMX100_RANGE_RTD_1MACU10A392 = 0x030D;
	public const int DAQMX100_RANGE_RTD_1MACU10A393 = 0x030E;
	public const int DAQMX100_RANGE_RTD_1MACU25 = 0x030F;
	public const int DAQMX100_RANGE_RTD_1MACU53 = 0x0310;
	public const int DAQMX100_RANGE_RTD_1MACU100 = 0x0311;
	public const int DAQMX100_RANGE_RTD_1MAPT25 = 0x0312;
	public const int DAQMX100_RANGE_RTD_1MACU10GEH = 0x0313;
	public const int DAQMX100_RANGE_RTD_1MACU10LNH = 0x0314;
	public const int DAQMX100_RANGE_RTD_1MACU10WEEDH = 0x0315;
	public const int DAQMX100_RANGE_RTD_1MACU10BAILEYH = 0x0316;
	public const int DAQMX100_RANGE_RTD_1MAPTN = 0x0317;
	public const int DAQMX100_RANGE_RTD_1MAJPTN = 0x0318;
	public const int DAQMX100_RANGE_RTD_1MAPTG = 0x0319;
	public const int DAQMX100_RANGE_RTD_1MACU100G = 0x031A;
	public const int DAQMX100_RANGE_RTD_1MACU50G = 0x031B;
	public const int DAQMX100_RANGE_RTD_1MACU10G = 0x031C;
	
	// RTD 2mA
	public const int DAQMX100_RANGE_RTD_2MAPT = 0x0400;
	public const int DAQMX100_RANGE_RTD_2MAJPT = 0x0401;
	public const int DAQMX100_RANGE_RTD_2MAPTH = 0x0402;
	public const int DAQMX100_RANGE_RTD_2MAJPTH = 0x0403;
	public const int DAQMX100_RANGE_RTD_2MAPT50 = 0x0404;
	public const int DAQMX100_RANGE_RTD_2MACU10GE = 0x0405;
	public const int DAQMX100_RANGE_RTD_2MACU10LN = 0x0406;
	public const int DAQMX100_RANGE_RTD_2MACU10WEED = 0x0407;
	public const int DAQMX100_RANGE_RTD_2MACU10BAILEY = 0x0408;
	public const int DAQMX100_RANGE_RTD_2MAJ263B = 0x0409;
	public const int DAQMX100_RANGE_RTD_2MACU10A392 = 0x040A;
	public const int DAQMX100_RANGE_RTD_2MACU10A393 = 0x040B;
	public const int DAQMX100_RANGE_RTD_2MACU25 = 0x040C;
	public const int DAQMX100_RANGE_RTD_2MACU53 = 0x040D;
	public const int DAQMX100_RANGE_RTD_2MACU100 = 0x040E;
	public const int DAQMX100_RANGE_RTD_2MAPT25 = 0x040F;
	public const int DAQMX100_RANGE_RTD_2MACU10GEH = 0x0410;
	public const int DAQMX100_RANGE_RTD_2MACU10LNH = 0x0411;
	public const int DAQMX100_RANGE_RTD_2MACU10WEEDH = 0x0412;
	public const int DAQMX100_RANGE_RTD_2MACU10BAILEYH = 0x0413;
	public const int DAQMX100_RANGE_RTD_2MAPTN = 0x0414;
	public const int DAQMX100_RANGE_RTD_2MAJPTN = 0x0415;
	public const int DAQMX100_RANGE_RTD_2MACU100G = 0x0416;
	public const int DAQMX100_RANGE_RTD_2MACU50G = 0x0417;
	public const int DAQMX100_RANGE_RTD_2MACU10G = 0x0418;
	
	// DI : detail
	public const int DAQMX100_RANGE_DI_LEVEL_AI = 0x0100;
	public const int DAQMX100_RANGE_DI_CONTACT_AI4 = 0x0101;
	public const int DAQMX100_RANGE_DI_CONTACT_AI10 = 0x0102;
	public const int DAQMX100_RANGE_DI_LEVEL_DI = 0x0103;
	public const int DAQMX100_RANGE_DI_CONTACT_DI = 0x0104;
	public const int DAQMX100_RANGE_DI_LEVEL_DI5V = DAQMX100_RANGE_DI_LEVEL_DI;
	public const int DAQMX100_RANGE_DI_LEVEL_DI24V = 0x0105;
	public const int DAQMX100_RANGE_DI_CONTACT_AI30 = DAQMX100_RANGE_DI_CONTACT_AI10;
	
	// RTD 0.25mA
	public const int DAQMX100_RANGE_RTD_025MAPT500 = 0x0500;
	public const int DAQMX100_RANGE_RTD_025MAPT1K = 0x0501;
	
	// RES
	public const int DAQMX100_RANGE_RES_20 = 0x0600;
	public const int DAQMX100_RANGE_RES_200 = 0x0601;
	public const int DAQMX100_RANGE_RES_2K = 0x0602;
	
	// STR
	public const int DAQMX100_RANGE_STRAIN_2K = 0x0700;
	public const int DAQMX100_RANGE_STRAIN_20K = 0x0701;
	public const int DAQMX100_RANGE_STRAIN_200K = 0x0702;
	
	// AO
	public const int DAQMX100_RANGE_AO_10V = 0x1000;
	public const int DAQMX100_RANGE_AO_20MA = 0x1001;
	
	// PWM
	public const int DAQMX100_RANGE_PWM_1MS = 0x1100;
	public const int DAQMX100_RANGE_PWM_10MS = 0x1101;
	
	// CAN
	public const int DAQMX100_RANGE_COM_CAN = 0x0800;
	
	// PI
	public const int DAQMX100_RANGE_PI_LEVEL = 0x0900;
	public const int DAQMX100_RANGE_PI_CONTACT = 0x0901;
	
	// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
	// Output
	public const int DAQMX100_OUTPUT_NONE = 0;
	public const int DAQMX100_OUTPUT_AO_10V = DAQMX100_RANGE_AO_10V;
	public const int DAQMX100_OUTPUT_AO_20MA = DAQMX100_RANGE_AO_20MA;
	public const int DAQMX100_OUTPUT_PWM_1MS = DAQMX100_RANGE_PWM_1MS;
	public const int DAQMX100_OUTPUT_PWM_10MS = DAQMX100_RANGE_PWM_10MS;
	
	// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
	// configure item @see DAQMXItems.bas
	// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
	
	// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
	// Connection 连接MX100
	// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="openMX100")]
	public static extern int openMX100(byte[] strAddress, out int errCode);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="closeMX100")]
	public static extern int closeMX100(int daqmx100);
	
	// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
	// FIFO 开始测量
	// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="measStartMX100")]
	public static extern int measStartMX100(int daqmx100);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="measStopMX100")]
	public static extern int measStopMX100(int daqmx100);
	
	// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
	// Control
	// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="setDateTimeNowMX100")]
	public static extern int setDateTimeNowMX100(int daqmx100);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="switchBackupMX100")]
	public static extern int switchBackupMX100(int daqmx100, int bBackup);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="formatCFMX100")]
	public static extern int formatCFMX100(int daqmx100);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="reconstructMX100")]
	public static extern int reconstructMX100(int daqmx100);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="initSetValueMX100")]
	public static extern int initSetValueMX100(int daqmx100);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="ackAlarmMX100")]
	public static extern int ackAlarmMX100(int daqmx100);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="displaySegmentMX100")]
	public static extern int displaySegmentMX100(int daqmx100, int dispPattern0, int dispPattern1, int dispType, int dispTime);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="initDataChMX100")]
	public static extern int initDataChMX100(int daqmx100, int chNo);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="initDataFIFOMX100")]
	public static extern int initDataFIFOMX100(int daqmx100, int fifoNo);
	
	// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
	// Setup
	// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="sendConfigMX100")]
	public static extern int sendConfigMX100(int daqmx100);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="initBalanceMX100")]
	public static extern int initBalanceMX100(int daqmx100);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="clearBalanceMX100")]
	public static extern int clearBalanceMX100(int daqmx100);
	
	// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
	// Setting
	// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="setRangeMX100")]
	public static extern int setRangeMX100(int daqmx100, int chNo, int iRange);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="setChDELTAMX100")]
	public static extern int setChDELTAMX100(int daqmx100, int chNo, int refChNo, int iRange);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="setChRRJCMX100")]
	public static extern int setChRRJCMX100(int daqmx100, int chNo, int refChNo);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="setChUnitMX100")]
	public static extern int setChUnitMX100(int daqmx100, int chNo, byte[] strUnit);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="setChTagMX100")]
	public static extern int setChTagMX100(int daqmx100, int chNo, byte[] strTag);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="setChCommentMX100")]
	public static extern int setChCommentMX100(int daqmx100, int chNo, byte[] strComment);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="setSpanMX100")]
	public static extern int setSpanMX100(int daqmx100, int chNo, int spanMin, int spanMax);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="setDoubleSpanMX100")]
	public static extern int setDoubleSpanMX100(int daqmx100, int chNo, double spanMin, double spanMax);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="setScaleMX100")]
	public static extern int setScaleMX100(int daqmx100, int chNo, int scaleMin, int scaleMax, int scalePoint);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="setDoubleScaleMX100")]
	public static extern int setDoubleScaleMX100(int daqmx100, int chNo, double scaleMin, double scaleMax, int scalePoint);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="setAlarmMX100")]
	public static extern int setAlarmMX100(int daqmx100, int chNo, int levelNo, int iAlarmType, int value);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="setDoubleAlarmMX100")]
	public static extern int setDoubleAlarmMX100(int daqmx100, int chNo, int levelNo, int iAlarmType, double value);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="setAlarmValueMX100")]
	public static extern int setAlarmValueMX100(int daqmx100, int chNo, int levelNo, int iAlarmType, int valueON, int valueOFF);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="setDoubleAlarmValueMX100")]
	public static extern int setDoubleAlarmValueMX100(int daqmx100, int chNo, int levelNo, int iAlarmType, double valueON, double valueOFF);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="setHisterisysMX100")]
	public static extern int setHisterisysMX100(int daqmx100, int chNo, int levelNo, int histerisys);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="setDoubleHisterisysMX100")]
	public static extern int setDoubleHisterisysMX100(int daqmx100, int chNo, int levelNo, double histerisys);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="setFilterMX100")]
	public static extern int setFilterMX100(int daqmx100, int chNo, int iFilter);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="setRJCTypeMX100")]
	public static extern int setRJCTypeMX100(int daqmx100, int chNo, int iRJCType, int volt);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="setBurnoutMX100")]
	public static extern int setBurnoutMX100(int daqmx100, int chNo, int iBurnout);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="setDeenergizeMX100")]
	public static extern int setDeenergizeMX100(int daqmx100, int doNo, int bDeenergize);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="setHoldMX100")]
	public static extern int setHoldMX100(int daqmx100, int doNo, int bHold);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="setRefAlarmMX100")]
	public static extern int setRefAlarmMX100(int daqmx100, int doNo, int refChNo, int levelNo, int bValid);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="setChKindMX100")]
	public static extern int setChKindMX100(int daqmx100, int chNo, int iKind, int refChNo);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="setChatFilterMX100")]
	public static extern int setChatFilterMX100(int daqmx100, int chNo, int bChatFilter);
	
	// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="setIntervalMX100")]
	public static extern int setIntervalMX100(int daqmx100, int moduleNo, int iInterval);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="setIntegralMX100")]
	public static extern int setIntegralMX100(int daqmx100, int moduleNo, int iIntegral);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="setUnitNoMX100")]
	public static extern int setUnitNoMX100(int daqmx100, int unitNo);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="setUnitTempMX100")]
	public static extern int setUnitTempMX100(int daqmx100, int iTempUnit);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="setCFWriteModeMX100")]
	public static extern int setCFWriteModeMX100(int daqmx100, int iCFWriteMode);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="setOutputTypeMX100")]
	public static extern int setOutputTypeMX100(int daqmx100, int outputNo, int iOutput);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="setChoiceMX100")]
	public static extern int setChoiceMX100(int daqmx100, int outputNo, int idleChoice, int errorChoice, int presetValue);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="setDoubleChoiceMX100")]
	public static extern int setDoubleChoiceMX100(int daqmx100, int outputNo, int idleChoice, int errorChoice, double presetValue);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="setPulseTimeMX100")]
	public static extern int setPulseTimeMX100(int daqmx100, int outputNo, int pulseTime);
	
	// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
	// Data Operation
	// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="createDOMX100")]
	public static extern int createDOMX100(int daqmx100, out int errorCode);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="deleteDOMX100")]
	public static extern int deleteDOMX100(int daqmx100, int idDO);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="changeDOMX100")]
	public static extern int changeDOMX100(int daqmx100, int idDO, int doNo, int bValid, int bONOFF);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="copyDOMX100")]
	public static extern int copyDOMX100(int daqmx100, int idDO, int idDOSrc);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="commandDOMX100")]
	public static extern int commandDOMX100(int daqmx100, int idDO);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="switchDOMX100")]
	public static extern int switchDOMX100(int daqmx100, int idDO, int bONOFF);
	
	// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="createAOPWMMX100")]
	public static extern int createAOPWMMX100(int daqmx100, out int errCode);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="deleteAOPWMMX100")]
	public static extern int deleteAOPWMMX100(int daqmx100, int idAOPWM);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="changeAOPWMMX100")]
	public static extern int changeAOPWMMX100(int daqmx100, int idAOPWM, int aopwmNo, int bValid, int iAOPWMValue);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="changeAOPWMValueMX100")]
	public static extern int changeAOPWMValueMX100(int daqmx100, int idAOPWM, int aopwmNo, int bValid, double realValue);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="copyAOPWMMX100")]
	public static extern int copyAOPWMMX100(int daqmx100, int idAOPWM, int idAOPWMSrc);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="commandAOPWMMX100")]
	public static extern int commandAOPWMMX100(int daqmx100, int idAOPWM);
	
	// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="createBalanceMX100")]
	public static extern int createBalanceMX100(int daqmx100, out int errCode);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="deleteBalanceMX100")]
	public static extern int deleteBalanceMX100(int daqmx100, int idBalance);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="changeBalanceMX100")]
	public static extern int changeBalanceMX100(int daqmx100, int idBalance, int balanceNo, int bValid, int iValue);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="copyBalanceMX100")]
	public static extern int copyBalanceMX100(int daqmx100, int idBalance, int idBalanceSrc);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="commandBalanceMX100")]
	public static extern int commandBalanceMX100(int daqmx100, int idBalance);
	
	// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="createTransmitMX100")]
	public static extern int createTransmitMX100(int daqmx100, out int errCode);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="deleteTransmitMX100")]
	public static extern int deleteTransmitMX100(int daqmx100, int idTrans);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="changeTransmitMX100")]
	public static extern int changeTransmitMX100(int daqmx100, int idTrans, int aopwmNo, int iTransmit);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="copyTransmitMX100")]
	public static extern int copyTransmitMX100(int daqmx100, int idTrans, int idTransSrc);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="commandTransmitMX100")]
	public static extern int commandTransmitMX100(int daqmx100, int idTrans);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="switchTransmitMX100")]
	public static extern int switchTransmitMX100(int daqmx100, int idTrans, int iTransmit);
	
	// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
	// Update
	// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="updateStatusMX100")]
	public static extern int updateStatusMX100(int daqmx100);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="updateSystemMX100")]
	public static extern int updateSystemMX100(int daqmx100);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="updateConfigMX100")]
	public static extern int updateConfigMX100(int daqmx100);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="updateDODataMX100")]
	public static extern int updateDODataMX100(int daqmx100);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="updateAOPWMDataMX100")]
	public static extern int updateAOPWMDataMX100(int daqmx100);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="updateInfoChMX100")]
	public static extern int updateInfoChMX100(int daqmx100, int chNo);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="updateBalanceMX100")]
	public static extern int updateBalanceMX100(int daqmx100);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="updateOutputMX100")]
	public static extern int updateOutputMX100(int daqmx100);
	
	// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
	// Data Aquisition
	// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="measDataChMX100")]
	public static extern int measDataChMX100(int daqmx100, int chNo);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="measInstChMX100")]
	public static extern int measInstChMX100(int daqmx100, int chNo);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="measDataFIFOMX100")]
	public static extern int measDataFIFOMX100(int daqmx100, int fifoNo);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="measInstFIFOMX100")]
	public static extern int measInstFIFOMX100(int daqmx100, int fifoNo);
	
	// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
	// Item
	// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="getItemAllMX100")]
	public static extern int getItemAllMX100(int daqmx100);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="setItemAllMX100")]
	public static extern int setItemAllMX100(int daqmx100);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="readItemMX100")]
	public static extern int readItemMX100(int daqmx100, int itemNo, byte[] strItem, int lenItem, out int realLen);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="writeItemMX100")]
	public static extern int writeItemMX100(int daqmx100, int itemNo, byte[] strItem);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="initItemMX100")]
	public static extern int initItemMX100(int daqmx100);
	
	// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
	// Get Current Measured Data
	// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="dataValueMX100")]
	public static extern int dataValueMX100(int daqmx100, int chNo);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="dataStatusMX100")]
	public static extern int dataStatusMX100(int daqmx100, int chNo);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="dataAlarmMX100")]
	public static extern int dataAlarmMX100(int daqmx100, int chNo, int levelNo);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="dataDoubleValueMX100")]
	public static extern double dataDoubleValueMX100(int daqmx100, int chNo);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="dataStringValueMX100")]
	public static extern int dataStringValueMX100(int daqmx100, int chNo, byte[] strValue, int lenValue);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="dataTimeMX100")]
	public static extern int dataTimeMX100(int daqmx100, int chNo);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="dataMilliSecMX100")]
	public static extern int dataMilliSecMX100(int daqmx100, int chNo);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="dataYearMX100")]
	public static extern int dataYearMX100(int daqmx100, int chNo);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="dataMonthMX100")]
	public static extern int dataMonthMX100(int daqmx100, int chNo);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="dataDayMX100")]
	public static extern int dataDayMX100(int daqmx100, int chNo);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="dataHourMX100")]
	public static extern int dataHourMX100(int daqmx100, int chNo);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="dataMinuteMX100")]
	public static extern int dataMinuteMX100(int daqmx100, int chNo);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="dataSecondMX100")]
	public static extern int dataSecondMX100(int daqmx100, int chNo);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="dataValidMX100")]
	public static extern int dataValidMX100(int daqmx100, int chNo);
	
	// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
	// Get Channel Information
	// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="channelFIFONoMX100")]
	public static extern int channelFIFONoMX100(int daqmx100, int chNo);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="channelFIFOIndexMX100")]
	public static extern int channelFIFOIndexMX100(int daqmx100, int chNo);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="channelDisplayMinMX100")]
	public static extern double channelDisplayMinMX100(int daqmx100, int chNo);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="channelDisplayMaxMX100")]
	public static extern double channelDisplayMaxMX100(int daqmx100, int chNo);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="channelRealMinMX100")]
	public static extern double channelRealMinMX100(int daqmx100, int chNo);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="channelRealMaxMX100")]
	public static extern double channelRealMaxMX100(int daqmx100, int chNo);
	
	// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
	// Get Channel Configure
	// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="channelValidMX100")]
	public static extern int channelValidMX100(int daqmx100, int chNo);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="channelPointMX100")]
	public static extern int channelPointMX100(int daqmx100, int chNo);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="channelKindMX100")]
	public static extern int channelKindMX100(int daqmx100, int chNo);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="channelRangeMX100")]
	public static extern int channelRangeMX100(int daqmx100, int chNo);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="channelScaleTypeMX100")]
	public static extern int channelScaleTypeMX100(int daqmx100, int chNo);
	
	// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="toChannelUnitMX100")]
	public static extern int toChannelUnitMX100(int daqmx100, int chNo, byte[] strUnit, int lenUnit);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="toChannelTagMX100")]
	public static extern int toChannelTagMX100(int daqmx100, int chNo, byte[] strTag, int lenTag);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="toChannelCommentMX100")]
	public static extern int toChannelCommentMX100(int daqmx100, int chNo, byte[] strComment, int lenComment);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="channelSpanMinMX100")]
	public static extern int channelSpanMinMX100(int daqmx100, int chNo);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="channelDoubleSpanMinMX100")]
	public static extern double channelDoubleSpanMinMX100(int daqmx100, int chNo);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="channelSpanMaxMX100")]
	public static extern int channelSpanMaxMX100(int daqmx100, int chNo);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="channelDoubleSpanMaxMX100")]
	public static extern double channelDoubleSpanMaxMX100(int daqmx100, int chNo);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="channelScaleMinMX100")]
	public static extern int channelScaleMinMX100(int daqmx100, int chNo);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="channelDoubleScaleMinMX100")]
	public static extern double channelDoubleScaleMinMX100(int daqmx100, int chNo);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="channelScaleMaxMX100")]
	public static extern int channelScaleMaxMX100(int daqmx100, int chNo);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="channelDoubleScaleMaxMX100")]
	public static extern double channelDoubleScaleMaxMX100(int daqmx100, int chNo);
	
	// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="alarmTypeMX100")]
	public static extern int alarmTypeMX100(int daqmx100, int chNo, int levelNo);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="alarmValueONMX100")]
	public static extern int alarmValueONMX100(int daqmx100, int chNo, int levelNo);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="alarmDoubleValueONMX100")]
	public static extern double alarmDoubleValueONMX100(int daqmx100, int chNo, int levelNo);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="alarmValueOFFMX100")]
	public static extern int alarmValueOFFMX100(int daqmx100, int chNo, int levelNo);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="alarmDoubleValueOFFMX100")]
	public static extern double alarmDoubleValueOFFMX100(int daqmx100, int chNo, int levelNo);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="alarmHisterisysMX100")]
	public static extern int alarmHisterisysMX100(int daqmx100, int chNo, int levelNo);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="alarmDoubleHisterisysMX100")]
	public static extern double alarmDoubleHisterisysMX100(int daqmx100, int chNo, int levelNo);
	
	// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="channelFilterMX100")]
	public static extern int channelFilterMX100(int daqmx100, int chNo);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="channelRJCTypeMX100")]
	public static extern int channelRJCTypeMX100(int daqmx100, int chNo);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="channelRJCVoltMX100")]
	public static extern int channelRJCVoltMX100(int daqmx100, int chNo);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="channelBurnoutMX100")]
	public static extern int channelBurnoutMX100(int daqmx100, int chNo);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="channelDeenergizeMX100")]
	public static extern int channelDeenergizeMX100(int daqmx100, int doNo);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="channelHoldMX100")]
	public static extern int channelHoldMX100(int daqmx100, int doNo);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="channelRefAlarmMX100")]
	public static extern int channelRefAlarmMX100(int daqmx100, int doNo, int refChNo, int levelNo);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="channelRefChNoMX100")]
	public static extern int channelRefChNoMX100(int daqmx100, int chNo);
	
	// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="channelBalanceValidMX100")]
	public static extern int channelBalanceValidMX100(int daqmx100, int balanceNo);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="channelBalanceValueMX100")]
	public static extern int channelBalanceValueMX100(int daqmx100, int balanceNo);
	
	// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="channelOutputTypeMX100")]
	public static extern int channelOutputTypeMX100(int daqmx100, int outputNo);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="channelIdleChoiceMX100")]
	public static extern int channelIdleChoiceMX100(int daqmx100, int outputNo);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="channelErrorChoiceMX100")]
	public static extern int channelErrorChoiceMX100(int daqmx100, int outputNo);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="channelPresetValueMX100")]
	public static extern int channelPresetValueMX100(int daqmx100, int outputNo);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="channelDoublePresetValueMX100")]
	public static extern double channelDoublePresetValueMX100(int daqmx100, int outputNo);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="channelPulseTimeMX100")]
	public static extern int channelPulseTimeMX100(int daqmx100, int outputNo);
	
	// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="channelChatFilterMX100")]
	public static extern int channelChatFilterMX100(int daqmx100, int chNo);
	
	// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
	// Get Network Data
	// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="toNetHostMX100")]
	public static extern int toNetHostMX100(int daqmx100, byte[] strHost, int lenHost);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="netAddressMX100")]
	public static extern int netAddressMX100(int daqmx100);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="netPortMX100")]
	public static extern int netPortMX100(int daqmx100);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="netSubmaskMX100")]
	public static extern int netSubmaskMX100(int daqmx100);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="netGatewayMX100")]
	public static extern int netGatewayMX100(int daqmx100);
	
	// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
	// Get System Data
	// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="moduleTypeMX100")]
	public static extern int moduleTypeMX100(int daqmx100, int moduleNo);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="moduleChNumMX100")]
	public static extern int moduleChNumMX100(int daqmx100, int moduleNo);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="moduleIntervalMX100")]
	public static extern int moduleIntervalMX100(int daqmx100, int moduleNo);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="moduleIntegralMX100")]
	public static extern int moduleIntegralMX100(int daqmx100, int moduleNo);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="moduleValidMX100")]
	public static extern int moduleValidMX100(int daqmx100, int moduleNo);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="moduleStandbyTypeMX100")]
	public static extern int moduleStandbyTypeMX100(int daqmx100, int moduleNo);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="moduleRealTypeMX100")]
	public static extern int moduleRealTypeMX100(int daqmx100, int moduleNo);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="moduleTerminalMX100")]
	public static extern int moduleTerminalMX100(int daqmx100, int moduleNo);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="moduleVersionMX100")]
	public static extern int moduleVersionMX100(int daqmx100, int moduleNo);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="moduleFIFONoMX100")]
	public static extern int moduleFIFONoMX100(int daqmx100, int moduleNo);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="toModuleSerialMX100")]
	public static extern int toModuleSerialMX100(int daqmx100, int moduleNo, byte[] strSerial, int lenSerial);
	
	// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="unitTypeMX100")]
	public static extern int unitTypeMX100(int daqmx100);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="unitStyleMX100")]
	public static extern int unitStyleMX100(int daqmx100);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="unitNoMX100")]
	public static extern int unitNoMX100(int daqmx100);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="unitTempMX100")]
	public static extern int unitTempMX100(int daqmx100);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="unitFrequencyMX100")]
	public static extern int unitFrequencyMX100(int daqmx100);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="toUnitPartNoMX100")]
	public static extern int toUnitPartNoMX100(int daqmx100, byte[] strPartNo, int lenPartNo);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="unitOptionMX100")]
	public static extern int unitOptionMX100(int daqmx100);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="toUnitSerialMX100")]
	public static extern int toUnitSerialMX100(int daqmx100, byte[] strSerial, int lenSerial);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="unitMACMX100")]
	public static extern int unitMACMX100(int daqmx100, int index);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="unitCFWriteModeMX100")]
	public static extern int unitCFWriteModeMX100(int daqmx100);
	
	// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
	// Get Status Data
	// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="statusUnitMX100")]
	public static extern int statusUnitMX100(int daqmx100);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="statusFIFONumMX100")]
	public static extern int statusFIFONumMX100(int daqmx100);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="statusBackupMX100")]
	public static extern int statusBackupMX100(int daqmx100);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="statusFIFOMX100")]
	public static extern int statusFIFOMX100(int daqmx100, int fifoNo);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="statusFIFOIntervalMX100")]
	public static extern int statusFIFOIntervalMX100(int daqmx100, int fifoNo);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="statusCFMX100")]
	public static extern int statusCFMX100(int daqmx100);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="statusCFSizeMX100")]
	public static extern int statusCFSizeMX100(int daqmx100);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="statusCFRemainMX100")]
	public static extern int statusCFRemainMX100(int daqmx100);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="statusTimeMX100")]
	public static extern int statusTimeMX100(int daqmx100);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="statusMilliSecMX100")]
	public static extern int statusMilliSecMX100(int daqmx100);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="statusYearMX100")]
	public static extern int statusYearMX100(int daqmx100);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="statusMonthMX100")]
	public static extern int statusMonthMX100(int daqmx100);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="statusDayMX100")]
	public static extern int statusDayMX100(int daqmx100);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="statusHourMX100")]
	public static extern int statusHourMX100(int daqmx100);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="statusMinuteMX100")]
	public static extern int statusMinuteMX100(int daqmx100);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="statusSecondMX100")]
	public static extern int statusSecondMX100(int daqmx100);
	
	// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
	// Get Current Data
	// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="currentDOValidMX100")]
	public static extern int currentDOValidMX100(int daqmx100, int doNo);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="currentDOValueMX100")]
	public static extern int currentDOValueMX100(int daqmx100, int doNo);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="currentAOPWMValidMX100")]
	public static extern int currentAOPWMValidMX100(int daqmx100, int aopwmNo);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="currentAOPWMValueMX100")]
	public static extern int currentAOPWMValueMX100(int daqmx100, int aopwmNo);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="currentDoubleAOPWMValueMX100")]
	public static extern double currentDoubleAOPWMValueMX100(int daqmx100, int aopwmNo);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="currentBalanceValidMX100")]
	public static extern int currentBalanceValidMX100(int daqmx100, int balanceNo);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="currentBalanceValueMX100")]
	public static extern int currentBalanceValueMX100(int daqmx100, int balanceNo);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="currentBalanceResultMX100")]
	public static extern int currentBalanceResultMX100(int daqmx100, int balanceNo);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="currentTransmitMX100")]
	public static extern int currentTransmitMX100(int daqmx100, int aopwmNo);
	
	// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
	// Get User Data
	// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="userDOValidMX100")]
	public static extern int userDOValidMX100(int daqmx100, int idDO, int doNo);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="userDOValueMX100")]
	public static extern int userDOValueMX100(int daqmx100, int idDO, int doNo);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="userAOPWMValidMX100")]
	public static extern int userAOPWMValidMX100(int daqmx100, int idAOPWM, int aopwmNo);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="userAOPWMValueMX100")]
	public static extern int userAOPWMValueMX100(int daqmx100, int idAOPWM, int aopwmNo);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="userDoubleAOPWMValueMX100")]
	public static extern double userDoubleAOPWMValueMX100(int daqmx100, int idAOPWM, int aopwmNo);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="userBalanceValidMX100")]
	public static extern int userBalanceValidMX100(int daqmx100, int idBalance, int balanceNo);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="userBalanceValueMX100")]
	public static extern int userBalanceValueMX100(int daqmx100, int idBalance, int balanceNo);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="userTransmitMX100")]
	public static extern int userTransmitMX100(int daqmx100, int idTrans, int aopwmNo);
	
	// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
	// Utility
	// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="dataNumChMX100")]
	public static extern int dataNumChMX100(int daqmx100, int chNo);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="dataNumFIFOMX100")]
	public static extern int dataNumFIFOMX100(int daqmx100, int fifoNo);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="lastErrorMX100")]
	public static extern int lastErrorMX100(int daqmx100);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="toErrorMessageMX100")]
	public static extern int toErrorMessageMX100(int errCode, byte[] errStr, int errLen);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="errorMaxLengthMX100")]
	public static extern int errorMaxLengthMX100();
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="itemErrorMX100")]
	public static extern int itemErrorMX100(int daqmx100);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="channelNumberMX100")]
	public static extern int channelNumberMX100(int daqmx100, int fifoNo, int fifioIndex);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="rangePointMX100")]
	public static extern int rangePointMX100(int daqmx100, int iRange);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="toDoubleValueMX100")]
	public static extern double toDoubleValueMX100(int dataValue, int point);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="toStringValueMX100")]
	public static extern int toStringValueMX100(int dataValue, int point, byte[] strValue, int lenValue);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="toAlarmNameMX100")]
	public static extern int toAlarmNameMX100(int iAlarmType, byte[] strAlarm, int lenAlarm);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="alarmMaxLengthMX100")]
	public static extern int alarmMaxLengthMX100();
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="versionAPIMX100")]
	public static extern int versionAPIMX100();
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="revisionAPIMX100")]
	public static extern int revisionAPIMX100();
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="addressPartMX100")]
	public static extern int addressPartMX100(int address, int index);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="toAOPWMValueMX100")]
	public static extern int toAOPWMValueMX100(double realValue, int iRangeAOPWM);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="toRealValueMX100")]
	public static extern double toRealValueMX100(int iAOPWMValue, int iRangeAOPWM);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="toItemNameMX100")]
	public static extern int toItemNameMX100(int itemNo, byte[] strItem, int lenItem);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="toItemNoMX100")]
	public static extern int toItemNoMX100(byte[] strItem);
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="itemMaxLengthMX100")]
	public static extern int itemMaxLengthMX100();
	
	[DllImport("DAQMX100.dll", CharSet=CharSet.Auto, EntryPoint="toStyleVersionMX100")]
	public static extern int toStyleVersionMX100(int style);
	
	// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
}
