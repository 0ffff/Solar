using System;
using System.Runtime.InteropServices;

// DAQMXItems.cs
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
//
// Copyright(c) 2004-2007 Yokogawa Electric Corporation. All rights reserved.
//
// This is defined item number in configure.
//
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
// 2007/05/30 Ver.3 Rev.0
// 2004/11/01 Ver.2 Rev.1
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --

public class DAQMXItems
{
	// Construct
	public DAQMXItems()
	{
	}
	
	// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
	// item
	// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
	public const int DAQMX_ITEM_NONE              = -1    ; // 0xFFFFFFFF;
	// MXNetInfo
	public const int DAQMX_ITEM_NETHOST           = 0x0000; // 00000000 00000000;
	public const int DAQMX_ITEM_NETADDRESS        = 0x0001;
	public const int DAQMX_ITEM_NETPORT           = 0x0002;
	public const int DAQMX_ITEM_NETSUBMASK        = 0x0003;
	public const int DAQMX_ITEM_NETGATEWAY        = 0x0004;
	// MXUnitData
	public const int DAQMX_ITEM_UNITTYPE          = 0x0005; // 00000000 00000101;
	public const int DAQMX_ITEM_UNITSTYLE         = 0x0006;
	public const int DAQMX_ITEM_UNITNO            = 0x0007;
	public const int DAQMX_ITEM_UNITTEMP          = 0x0008;
	public const int DAQMX_ITEM_UNITCFTIMEOUT     = 0x0009;
	public const int DAQMX_ITEM_UNITCFWRITEMODE   = 0x000A;
	public const int DAQMX_ITEM_UNITFREQUENCY     = 0x000B;
	public const int DAQMX_ITEM_UNITPARTNO        = 0x000C;
	public const int DAQMX_ITEM_UNITOPTION        = 0x000D;
	public const int DAQMX_ITEM_UNITSERIAL        = 0x000E;
	public const int DAQMX_ITEM_UNITMAC           = 0x000F; // 00000000 00001111;
	// MXStatus
	public const int DAQMX_ITEM_UNITSTATUS        = 0x0010; // 00000000 00010000;
	public const int DAQMX_ITEM_CNTCONFIG         = 0x0011;
	public const int DAQMX_ITEM_CNTTIME           = 0x0012;
	public const int DAQMX_ITEM_FIFONUM           = 0x0013;
	public const int DAQMX_ITEM_BACKUP            = 0x0014;
	// MXCFInfo
	public const int DAQMX_ITEM_CFSTATUS          = 0x0015; // 00000000 00010101;
	public const int DAQMX_ITEM_CFSIZE            = 0x0016;
	public const int DAQMX_ITEM_CFREMAIN          = 0x0017;
	// MXFIFOInfo by FIFO
	public const int DAQMX_ITEM_FIFOSTATUS        = 0x0018; // 00000000 000110xx;
	public const int DAQMX_ITEM_FIFOINTERVAL      = 0x001C; // 00000000 000111xx;
	public const int DAQMX_ITEM_FIFOOLDNO         = 0x0020; // 00000000 001000xx;
	public const int DAQMX_ITEM_FIFONEWNO         = 0x0024; // 00000000 001001xx;
	// MXModuleData by Module
	public const int DAQMX_ITEM_MODULETYPE        = 0x0028; // 00000000 00101xxx;
	public const int DAQMX_ITEM_MODULECHNUM       = 0x0030; // 00000000 00110xxx;
	public const int DAQMX_ITEM_MODULEINTERVAL    = 0x0038; // 00000000 00111xxx;
	public const int DAQMX_ITEM_MODULEINTEGRAL    = 0x0040; // 00000000 01000xxx;
	public const int DAQMX_ITEM_MODULESTANDBY     = 0x0048; // 00000000 01001xxx;
	public const int DAQMX_ITEM_MODULEREALTYPE    = 0x0050; // 00000000 01010xxx;
	public const int DAQMX_ITEM_MODULESTATUS      = 0x0058; // 00000000 01011xxx;
	public const int DAQMX_ITEM_MODULEVERSION     = 0x0060; // 00000000 01100xxx;
	public const int DAQMX_ITEM_MODULETERMINAL    = 0x0068; // 00000000 01101xxx;
	public const int DAQMX_ITEM_MODULEFIFONO      = 0x0070; // 00000000 01110xxx;
	public const int DAQMX_ITEM_MODULESERIAL      = 0x0078; // 00000000 01111xxx;
	// MXChID by Channel
	public const int DAQMX_ITEM_CHVALID           = 0x0080; // 00000000 10xxxxxx;
	public const int DAQMX_ITEM_CHPOINT           = 0x00C0; // 00000000 11xxxxxx;
	public const int DAQMX_ITEM_CHKIND            = 0x0100; // 00000001 00xxxxxx;
	public const int DAQMX_ITEM_CHRANGE           = 0x0140; // 00000001 01xxxxxx;
	public const int DAQMX_ITEM_CHSCALE           = 0x0180; // 00000001 10xxxxxx;
	public const int DAQMX_ITEM_CHUNIT            = 0x01C0; // 00000001 11xxxxxx;
	public const int DAQMX_ITEM_CHTAG             = 0x0200; // 00000010 00xxxxxx;
	public const int DAQMX_ITEM_CHCOMMENT         = 0x0240; // 00000010 01xxxxxx;
	// MXAlarm by Channel
	public const int DAQMX_ITEM_ALARMTYPE         = 0x0280; // 00000010 1yxxxxxx;
	public const int DAQMX_ITEM_ALARMON           = 0x0300; // 00000011 0yxxxxxx;
	public const int DAQMX_ITEM_ALARMOFF          = 0x0380; // 00000011 1yxxxxxx;
	// MXChConfigAIDI by Channel
	public const int DAQMX_ITEM_CHSPANMIN         = 0x0400; // 00000100 00xxxxxx;
	public const int DAQMX_ITEM_CHSPANMAX         = 0x0440; // 00000100 01xxxxxx;
	public const int DAQMX_ITEM_CHSCALEMIN        = 0x0480; // 00000100 10xxxxxx;
	public const int DAQMX_ITEM_CHSCALEMAX        = 0x04C0; // 00000100 11xxxxxx;
	public const int DAQMX_ITEM_CHREFCHNO         = 0x0500; // 00000101 00xxxxxx;
	// MXChConfigAI by Channel
	public const int DAQMX_ITEM_CHFILTER          = 0x0540; // 00000101 01xxxxxx;
	public const int DAQMX_ITEM_CHRJCTYPE         = 0x0580; // 00000101 10xxxxxx;
	public const int DAQMX_ITEM_CHRJCVOLT         = 0x05C0; // 00000101 11xxxxxx;
	public const int DAQMX_ITEM_CHBURNOUT         = 0x0600; // 00000110 00xxxxxx;
	// MXChConfigDO by Channel
	public const int DAQMX_ITEM_CHDEENERGIZE      = 0x0640; // 00000110 01xxxxxx;
	public const int DAQMX_ITEM_CHREFALARM        = 0x0680; // 00000110 1yxxxxxx;
	public const int DAQMX_ITEM_CHHOLD            = 0x0700; // 00000111 00xxxxxx;
	// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
	// since R2.01
	// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
	// MXChConfigAOPWM by Channel
	public const int DAQMX_ITEM_CHOUTPUTTYPE      = 0x0740; // 00000111 01xxxxxx;
	public const int DAQMX_ITEM_CHIDLECHOICE      = 0x0780; // 00000111 10xxxxxx;
	public const int DAQMX_ITEM_CHERRCHOICE       = 0x07C0; // 00000111 11xxxxxx;
	public const int DAQMX_ITEM_CHPRESETVALUE     = 0x0800; // 00001000 00xxxxxx;
	public const int DAQMX_ITEM_CHPULSETIME       = 0x0840; // 00001000 01xxxxxx;
	// MXBalance by Channel
	public const int DAQMX_ITEM_CHBALANCEVALID    = 0x0880; // 00001000 10xxxxxx;
	public const int DAQMX_ITEM_CHBALANCEVALUE    = 0x08C0; // 00001000 11xxxxxx;
	// MXStatus
	public const int DAQMX_ITEM_STATUSTIME        = 0x0900; // 00001001 00000000;
	public const int DAQMX_ITEM_STATUSMSEC        = 0x0901;
	// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
	// since R3.01
	// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
	// MXChConfigPI by channel
	public const int DAQMX_ITEM_CHCHATFILTER      = 0x0940; // 00001001 01xxxxxx;
	// Alarm : for No. 3,4
	public const int DAQMX_ITEM_ALARMTYPE2        = 0x0980; // 00001001 0yxxxxxx;
	public const int DAQMX_ITEM_ALARMON2          = 0x0A00; // 00001010 0yxxxxxx;
	public const int DAQMX_ITEM_ALARMOFF2         = 0x0A80; // 00001010 1yxxxxxx;
	public const int DAQMX_ITEM_CHREFALARM2       = 0x0B00; // 00001011 0yxxxxxx;
	// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
	// All and Mask
	// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
	// max
	public const int DAQMX_MAX_INDEX_FIFO         = 2;
	public const int DAQMX_MAX_INDEX_MODULE       = 5;
	public const int DAQMX_MAX_INDEX_CHANNEL      = 59;
	// All
	public const int DAQMX_ITEM_ALL_START         = 0;
	public const int DAQMX_ITEM_ALL_END_R1        = DAQMX_ITEM_CHHOLD + DAQMX_MAX_INDEX_CHANNEL; // R1.01;
	public const int DAQMX_ITEM_ALL_END_R2        = DAQMX_ITEM_STATUSMSEC; // R2.01;
	public const int DAQMX_ITEM_ALL_END_R3        = DAQMX_ITEM_CHREFALARM2 + 0x0040 + DAQMX_MAX_INDEX_CHANNEL; // R3.01;
	public const int DAQMX_ITEM_ALL_END           = DAQMX_ITEM_ALL_END_R3; // newest;
	// mask
	public const int DAQMX_MASK_BYMODULE          = 0x0007;
	public const int DAQMX_MASK_BYCHANNEL         = 0x003F;
	public const int DAQMX_MASK_BYFIFO            = 0x0003;
	public const int DAQMX_MASK_BYALARM           = 0x0040; // NOTE: position;
	// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
}
