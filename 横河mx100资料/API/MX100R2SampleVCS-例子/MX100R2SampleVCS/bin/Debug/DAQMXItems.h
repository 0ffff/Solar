// DAQMXItems.h
/* -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- */
/*
 * Copyright (c) 2004-2007 Yokogawa Electric Corporation. All rights reserved.
 */
/* -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- */
// 2007/05/30 Ver.3 Rev.0
// 2004/11/01 Ver.2 Rev.1
///////////////////////////////////////////////////////////////////////
#ifndef _DAQMXITEMS_H_
#define _DAQMXITEMS_H_
///////////////////////////////////////////////////////////////////////
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// Items
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
#define DAQMX_ITEM_NONE                  (-1)     // 0xFFFFFFFF
// MXNetInfo
#define DAQMX_ITEM_NETHOST               (0x0000) // 00000000 00000000
#define DAQMX_ITEM_NETADDRESS            (0x0001)
#define DAQMX_ITEM_NETPORT               (0x0002)
#define DAQMX_ITEM_NETSUBMASK            (0x0003)
#define DAQMX_ITEM_NETGATEWAY            (0x0004)
// MXUnitData
#define DAQMX_ITEM_UNITTYPE              (0x0005) // 00000000 00000101
#define DAQMX_ITEM_UNITSTYLE             (0x0006)
#define DAQMX_ITEM_UNITNO                (0x0007)
#define DAQMX_ITEM_UNITTEMP              (0x0008)
#define DAQMX_ITEM_UNITCFTIMEOUT         (0x0009)
#define DAQMX_ITEM_UNITCFWRITEMODE       (0x000A)
#define DAQMX_ITEM_UNITFREQUENCY         (0x000B)
#define DAQMX_ITEM_UNITPARTNO            (0x000C)
#define DAQMX_ITEM_UNITOPTION            (0x000D)
#define DAQMX_ITEM_UNITSERIAL            (0x000E)
#define DAQMX_ITEM_UNITMAC               (0x000F) // 00000000 00001111
// MXStatus
#define DAQMX_ITEM_UNITSTATUS            (0x0010) // 00000000 00010000
#define DAQMX_ITEM_CNTCONFIG             (0x0011)
#define DAQMX_ITEM_CNTTIME               (0x0012)
#define DAQMX_ITEM_FIFONUM               (0x0013)
#define DAQMX_ITEM_BACKUP                (0x0014)
// MXCFInfo
#define DAQMX_ITEM_CFSTATUS              (0x0015) // 00000000 00010101
#define DAQMX_ITEM_CFSIZE                (0x0016)
#define DAQMX_ITEM_CFREMAIN              (0x0017)
// MXFIFOInfo by FIFO
#define DAQMX_ITEM_FIFOSTATUS            (0x0018) // 00000000 000110xx
#define DAQMX_ITEM_FIFOINTERVAL          (0x001C) // 00000000 000111xx
#define DAQMX_ITEM_FIFOOLDNO             (0x0020) // 00000000 001000xx
#define DAQMX_ITEM_FIFONEWNO             (0x0024) // 00000000 001001xx
// MXModuleData by Module
#define DAQMX_ITEM_MODULETYPE            (0x0028) // 00000000 00101xxx
#define DAQMX_ITEM_MODULECHNUM           (0x0030) // 00000000 00110xxx
#define DAQMX_ITEM_MODULEINTERVAL        (0x0038) // 00000000 00111xxx
#define DAQMX_ITEM_MODULEINTEGRAL        (0x0040) // 00000000 01000xxx
#define DAQMX_ITEM_MODULESTANDBY         (0x0048) // 00000000 01001xxx
#define DAQMX_ITEM_MODULEREALTYPE        (0x0050) // 00000000 01010xxx
#define DAQMX_ITEM_MODULESTATUS          (0x0058) // 00000000 01011xxx
#define DAQMX_ITEM_MODULEVERSION         (0x0060) // 00000000 01100xxx
#define DAQMX_ITEM_MODULETERMINAL        (0x0068) // 00000000 01101xxx
#define DAQMX_ITEM_MODULEFIFONO          (0x0070) // 00000000 01110xxx
#define DAQMX_ITEM_MODULESERIAL          (0x0078) // 00000000 01111xxx
// MXChID by Channel
#define DAQMX_ITEM_CHVALID               (0x0080) // 00000000 10xxxxxx
#define DAQMX_ITEM_CHPOINT               (0x00C0) // 00000000 11xxxxxx
#define DAQMX_ITEM_CHKIND                (0x0100) // 00000001 00xxxxxx
#define DAQMX_ITEM_CHRANGE               (0x0140) // 00000001 01xxxxxx
#define DAQMX_ITEM_CHSCALE               (0x0180) // 00000001 10xxxxxx
#define DAQMX_ITEM_CHUNIT                (0x01C0) // 00000001 11xxxxxx
#define DAQMX_ITEM_CHTAG                 (0x0200) // 00000010 00xxxxxx
#define DAQMX_ITEM_CHCOMMENT             (0x0240) // 00000010 01xxxxxx
// MXAlarm by Channel
#define DAQMX_ITEM_ALARMTYPE             (0x0280) // 00000010 1yxxxxxx
#define DAQMX_ITEM_ALARMON               (0x0300) // 00000011 0yxxxxxx
#define DAQMX_ITEM_ALARMOFF              (0x0380) // 00000011 1yxxxxxx
// MXChConfigAIDI by Channel
#define DAQMX_ITEM_CHSPANMIN             (0x0400) // 00000100 00xxxxxx
#define DAQMX_ITEM_CHSPANMAX             (0x0440) // 00000100 01xxxxxx
#define DAQMX_ITEM_CHSCALEMIN            (0x0480) // 00000100 10xxxxxx
#define DAQMX_ITEM_CHSCALEMAX            (0x04C0) // 00000100 11xxxxxx
#define DAQMX_ITEM_CHREFCHNO             (0x0500) // 00000101 00xxxxxx
// MXChConfigAI by Channel
#define DAQMX_ITEM_CHFILTER              (0x0540) // 00000101 01xxxxxx
#define DAQMX_ITEM_CHRJCTYPE             (0x0580) // 00000101 10xxxxxx
#define DAQMX_ITEM_CHRJCVOLT             (0x05C0) // 00000101 11xxxxxx
#define DAQMX_ITEM_CHBURNOUT             (0x0600) // 00000110 00xxxxxx
// MXChConfigDO by Channel
#define DAQMX_ITEM_CHDEENERGIZE          (0x0640) // 00000110 01xxxxxx
#define DAQMX_ITEM_CHREFALARM            (0x0680) // 00000110 1yxxxxxx
#define DAQMX_ITEM_CHHOLD                (0x0700) // 00000111 00xxxxxx
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// since R2.01
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// MXChConfigAOPWM by Channel
#define DAQMX_ITEM_CHOUTPUTTYPE          (0x0740) // 00000111 01xxxxxx
#define DAQMX_ITEM_CHIDLECHOICE          (0x0780) // 00000111 10xxxxxx
#define DAQMX_ITEM_CHERRCHOICE           (0x07C0) // 00000111 11xxxxxx
#define DAQMX_ITEM_CHPRESETVALUE         (0x0800) // 00001000 00xxxxxx
#define DAQMX_ITEM_CHPULSETIME           (0x0840) // 00001000 01xxxxxx
// MXBalance by Channel
#define DAQMX_ITEM_CHBALANCEVALID        (0x0880) // 00001000 10xxxxxx
#define DAQMX_ITEM_CHBALANCEVALUE        (0x08C0) // 00001000 11xxxxxx
// MXStatus
#define DAQMX_ITEM_STATUSTIME            (0x0900) // 00001001 00000000
#define DAQMX_ITEM_STATUSMSEC            (0x0901)
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// since R3.01
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// MXChConfigPI by channel
#define DAQMX_ITEM_CHCHATFILTER          (0x0940) // 00001001 01xxxxxx
// Alarm : for No. 3,4
#define DAQMX_ITEM_ALARMTYPE2            (0x0980) // 00001001 1yxxxxxx
#define DAQMX_ITEM_ALARMON2              (0x0A00) // 00001010 0yxxxxxx
#define DAQMX_ITEM_ALARMOFF2             (0x0A80) // 00001010 1yxxxxxx
#define DAQMX_ITEM_CHREFALARM2           (0x0B00) // 00001011 0yxxxxxx
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// All and Mask
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// max : since R3.01
#define DAQMX_MAX_INDEX_FIFO             (2)  //0 - 2
#define DAQMX_MAX_INDEX_MODULE           (5)  //0 - 5
#define DAQMX_MAX_INDEX_CHANNEL          (59) //0 - 59
// All
#define DAQMX_ITEM_ALL_START             (0)
#define DAQMX_ITEM_ALL_END_R1            (DAQMX_ITEM_CHHOLD + DAQMX_MAX_INDEX_CHANNEL) /* R1.01 */
#define DAQMX_ITEM_ALL_END_R2            (DAQMX_ITEM_STATUSMSEC) /* R2.01 */
#define DAQMX_ITEM_ALL_END_R3            (DAQMX_ITEM_CHREFALARM2 + 0x0040 + DAQMX_MAX_INDEX_CHANNEL) /* R3.01 */
#define DAQMX_ITEM_ALL_END               (DAQMX_ITEM_ALL_END_R3) /* newest */
// mask
#define DAQMX_MASK_BYMODULE              (0x0007)
#define DAQMX_MASK_BYCHANNEL             (0x003F)
#define DAQMX_MASK_BYFIFO                (0x0003)
#define DAQMX_MASK_BYALARM               (0x0040) /* NOTE: position */
///////////////////////////////////////////////////////////////////////
#endif //_DAQMXITEMS_H_
