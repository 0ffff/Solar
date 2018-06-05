// DAQMX.h
/* -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- */
/*
 * Copyright (c) 2003-2007 Yokogawa Electric Corporation. All rights reserved.
 *
 * This is defined export DAQMX.dll.
 */
/* -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- */
// 2007/09/30 Ver.3 Rev.1
// 2007/05/30 Ver.3 Rev.0
// 2004/11/01 Ver.2 Rev.1
// 2003/05/30 Ver.1 Rev.1
///////////////////////////////////////////////////////////////////////
#ifndef _DAQMX_H_
#define _DAQMX_H_
///////////////////////////////////////////////////////////////////////
#if defined(WIN32) || defined(_WIN32_WCE)
#pragma pack(push, oldpack, 8) /* R2.01 */
// system
#include <windows.h>
// calling
#ifdef DAQMX_EXPORTS
#define DAQHANDLER_EXPORTS
#define DAQMX_API __declspec(dllexport)
#else
#define DAQMX_API __declspec(dllimport)
#endif
#else  //WIN32,WCE
#define DAQMX_API
#ifndef APIENTRY
#define APIENTRY
#endif
#endif //WIN32,WCE
///////////////////////////////////////////////////////////////////////
#include "DAQHandler.h"
///////////////////////////////////////////////////////////////////////
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// Value
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// communication
#define DAQMX_COMMPORT          (34316)
// total number
#define DAQMX_NUMMODULE         (6)
#define DAQMX_NUMCHANNEL        (60)
#define DAQMX_NUMDO             (DAQMX_NUMCHANNEL)
#define DAQMX_NUMFIFO           (3)
#define DAQMX_NUMALARM          (4) /* R03.00 */
#define DAQMX_NUMSEGMENT        (2)
#define DAQMX_NUMMACADDR        (6)
#define DAQMX_NUMAOPWM          (DAQMX_NUMCHANNEL) /* R2.01 */
#define DAQMX_NUMBALANCE        (DAQMX_NUMCHANNEL) /* R2.01 */
#define DAQMX_NUMOUTPUT         (DAQMX_NUMCHANNEL) /* R2.01 */
// string length without NULL
#define DAQMX_MAXHOSTNAMELEN    (64)
#define DAQMX_MAXUNITLEN        (6)
#define DAQMX_MAXTAGLEN         (15)
#define DAQMX_MAXCOMMENTLEN     (30)
#define DAQMX_MAXSERIALLEN      (9)
#define DAQMX_MAXPARTNOLEN      (7)
// maximum value
#define DAQMX_MAXDECIMALPOINT   (4)
#define DAQMX_MAXDISPTIME       (120000) /* msec */
#define DAQMX_MAXPULSETIME      (30000) /* R2.01 */
// specified datano
#define DAQMX_INSTANTANEOUS     (-1)
// specified reference channel number
#define DAQMX_REFCHNO_NONE      (0) /* R2.01 */
// specified
#define DAQMX_REFCHNO_ALL       (-1)
#define DAQMX_LEVELNO_ALL       (-1)
#define DAQMX_DONO_ALL          (-1)
#define DAQMX_SEGMENTNO_ALL     (-1)
#define DAQMX_CHNO_ALL          (-1) /* R2.01 */
#define DAQMX_MODULENO_ALL      (-1) /* R2.01 */
#define DAQMX_FIFONO_ALL        (-1) /* R2.01 */
#define DAQMX_AOPWMNO_ALL       (-1) /* R2.01 */
#define DAQMX_BALANCENO_ALL     (-1) /* R2.01 */
#define DAQMX_OUTPUTNO_ALL      (-1) /* R2.01 */
//-- ---- ---- ---- ---- ---- field values
// valid
#define DAQMX_VALID_OFF         (0) /* FALSE */
#define DAQMX_VALID_ON          (1) /* TRUE */
// flag : logical OR : @see DAQDARWIN_FLAG_xxx
#define DAQMX_FLAG_OFF          (0x00000000)
#define DAQMX_FLAG_ENDDATA      (0x00000001)
// data status : @see DAQDARWIN_DATA_xxx
#define DAQMX_DATA_UNKNOWN      (0x00000000) /* correct R2.01 */
#define DAQMX_DATA_NORMAL       (0x00000001)
#define DAQMX_DATA_PLUSOVER     (0x00007FFF) /* +OVER */
#define DAQMX_DATA_MINUSOVER    (0x00008001) /* -OVER */
#define DAQMX_DATA_SKIP         (0x00008002) /* SKIP */
#define DAQMX_DATA_ILLEGAL      (0x00008003) /* ILLEGAL */
#define DAQMX_DATA_NODATA       (0x00008005) /* NoData */
#define DAQMX_DATA_LACK         (0x00008400) /* MX : Lack */
#define DAQMX_DATA_INVALID      (0x00008700) /* MX : Invalid */
// alarm type : @see DAQDARWIN_ALARM_xxx
#define DAQMX_ALARM_NONE        (0)
#define DAQMX_ALARM_UPPER       (1)
#define DAQMX_ALARM_LOWER       (2)
#define DAQMX_ALARM_UPDIFF      (3) /* Upper Differential Alarm */
#define DAQMX_ALARM_LOWDIFF     (4) /* Lower Differential Alarm */
// system control : @see DAQDARWIN_SYSTEM_xxx
#define DAQMX_SYSTEM_RECONSTRUCT        (1) /* reconstruct */
#define DAQMX_SYSTEM_INITOPE            (2) /* initialize */
#define DAQMX_SYSTEM_RESETALARM         (3) /* alarm ack */
// channel kind : *Not* channel type
#define DAQMX_CHKIND_NONE       (0x0000) /* Unknown */
#define DAQMX_CHKIND_AI         (0x0010)
#define DAQMX_CHKIND_AIDIFF     (0x0011) /* AI DELTA */
#define DAQMX_CHKIND_AIRJC      (0x0012) /* AI RRJC */
#define DAQMX_CHKIND_DI         (0x0030)
#define DAQMX_CHKIND_DIDIFF     (0x0031) /* DI DELTA */
#define DAQMX_CHKIND_DO         (0x0040)
#define DAQMX_CHKIND_DOCOM      (0x0041) /* DO Command */
#define DAQMX_CHKIND_DOFAIL     (0x0042) /* DO System Fail */
#define DAQMX_CHKIND_DOERR      (0x0043) /* DO Syetem Error */
// channel kind since R2.01
#define DAQMX_CHKIND_AO         (0x0020)
#define DAQMX_CHKIND_AOCOM      (0x0021) /* AO Command */
#define DAQMX_CHKIND_PWM        (0x0060)
#define DAQMX_CHKIND_PWMCOM     (0x0061) /* PWM Command */
// channel kind since R3.01
#define DAQMX_CHKIND_PI         (0x0050)
#define DAQMX_CHKIND_PIDIFF     (0x0051) /* PI DELTA */
#define DAQMX_CHKIND_CI         (0x0070)
#define DAQMX_CHKIND_CIDIFF     (0x0071) /* CI DELTA */
// scale type
#define DAQMX_SCALE_NONE        (0)
#define DAQMX_SCALE_LINER       (1)
// module type
#define DAQMX_MODULE_NONE         (0x00000000)
#define DAQMX_MODULE_MX110UNVH04  (0xF0000010) /* AI  4ch  10msec universal */
#define DAQMX_MODULE_MX110UNVM10  (0xF0001C10) /* AI 10ch 100msec universal */
#define DAQMX_MODULE_MX115D05H10  (0x10003010) /* DI 10ch DC5V */
#define DAQMX_MODULE_MX125MKCM10  (0x00402010) /* DO 10ch */
// module type since R2.01
#define DAQMX_MODULE_MX110V4RM06  (0xB0101F10) /* AI  6ch 100msec 4W-RTD */
#define DAQMX_MODULE_MX112NDIM04  (0x01004010) /* Strain 4ch NDIS */
#define DAQMX_MODULE_MX112B35M04  (0x01004110) /* Strain 4ch 350ohm */
#define DAQMX_MODULE_MX112B12M04  (0x01004210) /* Strain 4ch 120ohm */
#define DAQMX_MODULE_MX115D24H10  (0x10003210) /* DI 10ch DC24V */
#define DAQMX_MODULE_MX120VAOM08  (0x0080C010) /* AO  8ch */
#define DAQMX_MODULE_MX120PWMM08  (0x0020C810) /* PWM 8ch */
// module type since R3.01
#define DAQMX_MODULE_HIDDEN       (0x0000FF00)
#define DAQMX_MODULE_MX114PLSM10  (0x0400B010) /* Pulse 10ch */
#define DAQMX_MODULE_MX110VTDL30  (0xD0001130) /* AI 30ch */
#define DAQMX_MODULE_MX118CANM10  (0x00085110) /* CAN 10ch */
#define DAQMX_MODULE_MX118CANM20  (0x00085220) /* CAN 20ch */
#define DAQMX_MODULE_MX118CANM30  (0x00085330) /* CAN 30ch */
#define DAQMX_MODULE_MX118CANSUB  (0x00085000) /* CAN Hidden */
#define DAQMX_MODULE_MX118CANMERR (0x00005A10) /* CAN Position Error */
#define DAQMX_MODULE_MX118CANSERR (0x00005B10) /* CAN Hidden Error */
// how many channels by each module
#define DAQMX_CHNUM_0           (0)
#define DAQMX_CHNUM_4           (4)
#define DAQMX_CHNUM_6           (6)  /* since R2.01 */
#define DAQMX_CHNUM_8           (8)  /* since R2.01 */
#define DAQMX_CHNUM_10          (10)
#define DAQMX_CHNUM_30          (30) /* since R3.01 */
// interval (msec)
#define DAQMX_INTERVAL_10       (10)  /* high speed only */
#define DAQMX_INTERVAL_50       (50)  /* high speed only */
#define DAQMX_INTERVAL_100      (100)
#define DAQMX_INTERVAL_200      (200)
#define DAQMX_INTERVAL_500      (500)
#define DAQMX_INTERVAL_1000     (1000)
#define DAQMX_INTERVAL_2000     (2000)
#define DAQMX_INTERVAL_5000     (5000)
#define DAQMX_INTERVAL_10000    (10000)
#define DAQMX_INTERVAL_20000    (20000)
#define DAQMX_INTERVAL_30000    (30000)
#define DAQMX_INTERVAL_60000    (60000)
// filter
#define DAQMX_FILTER_0          (0)
#define DAQMX_FILTER_5          (1)
#define DAQMX_FILTER_10         (2)
#define DAQMX_FILTER_20         (3)
#define DAQMX_FILTER_25         (4)
#define DAQMX_FILTER_40         (5)
#define DAQMX_FILTER_50         (6)
#define DAQMX_FILTER_100        (7)
// RJC type
#define DAQMX_RJC_INTERNAL      (0)
#define DAQMX_RJC_EXTERNAL      (1)
// burnout
#define DAQMX_BURNOUT_OFF       (0)
#define DAQMX_BURNOUT_UP        (1)
#define DAQMX_BURNOUT_DOWN      (2)
// unit type
#define DAQMX_UNITTYPE_NONE     (0x00000000)
#define DAQMX_UNITTYPE_MX100    (0x00010000)
// terminal type
#define DAQMX_TERMINAL_SCREW    (0)
#define DAQMX_TERMINAL_CLAMP    (1)
#define DAQMX_TERMINAL_NDIS     (2) /* R2.01 */
#define DAQMX_TERMINAL_DSUB     (3) /* R3.01 */
// AD
#define DAQMX_INTEGRAL_AUTO     (0)
#define DAQMX_INTEGRAL_50HZ     (1)
#define DAQMX_INTEGRAL_60HZ     (2)
// temperature unit
#define DAQMX_TEMPUNIT_C        (0)
#define DAQMX_TEMPUNIT_F        (1)
// CF write mode
#define DAQMX_CFWRITEMODE_ONCE  (0)
#define DAQMX_CFWRITEMODE_FIFO  (1)
// CF status : logical OR
#define DAQMX_CFSTATUS_NONE     (0x0000)
#define DAQMX_CFSTATUS_EXIST    (0x0001)
#define DAQMX_CFSTATUS_USE      (0x0002)
#define DAQMX_CFSTATUS_FORMAT   (0x0004)
// UNIT status
#define DAQMX_UNITSTAT_NONE     (0x0000)
#define DAQMX_UNITSTAT_INIT     (0x0001)
#define DAQMX_UNITSTAT_STOP     (0x0002)
#define DAQMX_UNITSTAT_RUN      (0x0003)
#define DAQMX_UNITSTAT_BACKUP   (0x0004)
// FIFO status : @see DAQMX_UNITSTAT_xxx
#define DAQMX_FIFOSTAT_NONE     (DAQMX_UNITSTAT_NONE)
#define DAQMX_FIFOSTAT_INIT     (DAQMX_UNITSTAT_INIT)
#define DAQMX_FIFOSTAT_STOP     (DAQMX_UNITSTAT_STOP) /* correct R2.01 */
#define DAQMX_FIFOSTAT_RUN      (DAQMX_UNITSTAT_RUN)
#define DAQMX_FIFOSTAT_BACKUP   (DAQMX_UNITSTAT_BACKUP)
// segment display type
#define DAQMX_DISPTYPE_NONE     (0)
#define DAQMX_DISPTYPE_ON       (1)
#define DAQMX_DISPTYPE_BLINK    (2)
// Choice : since R2.01
#define DAQMX_CHOICE_PREV       (0)
#define DAQMX_CHOICE_PRESET     (1)
// Transmit : since R2.01
#define DAQMX_TRANSMIT_NONE     (0)
#define DAQMX_TRANSMIT_RUN      (1) /* start */
#define DAQMX_TRANSMIT_STOP     (2)
// balance : since R2.01
#define DAQMX_BALANCE_NONE      (0)
#define DAQMX_BALANCE_DONE      (1)
#define DAQMX_BALANCE_NG        (2)
#define DAQMX_BALANCE_ERROR     (3)
// option : since R2.01
#define DAQMX_OPTION_NONE       (0x0000)
#define DAQMX_OPTION_DS         (0x0001)
//-- ---- ---- ---- ---- ---- Range
// NOTE: *not* same as DAQDARWIN_RANGE_xxx
// Reference
#define DAQMX_RANGE_REFERENCE   (-1)
// Volt (DC Voltage)
#define DAQMX_RANGE_VOLT_20MV   (0x0000) /* default */
#define DAQMX_RANGE_VOLT_60MV   (0x0001)
#define DAQMX_RANGE_VOLT_200MV  (0x0002)
#define DAQMX_RANGE_VOLT_2V     (0x0003)
#define DAQMX_RANGE_VOLT_6V     (0x0004)
#define DAQMX_RANGE_VOLT_20V    (0x0005)
#define DAQMX_RANGE_VOLT_100V   (0x0006)
#define DAQMX_RANGE_VOLT_60MVH  (0x0007) /* High Analyze */
#define DAQMX_RANGE_VOLT_1V     (0x0008)
#define DAQMX_RANGE_VOLT_6VH    (0x0009) /* High Analyze */
// TC (Thermocuple)
#define DAQMX_RANGE_TC_R        (0x0200)
#define DAQMX_RANGE_TC_S        (0x0201)
#define DAQMX_RANGE_TC_B        (0x0202)
#define DAQMX_RANGE_TC_K        (0x0203)
#define DAQMX_RANGE_TC_E        (0x0204)
#define DAQMX_RANGE_TC_J        (0x0205)
#define DAQMX_RANGE_TC_T        (0x0206)
#define DAQMX_RANGE_TC_N        (0x0207)
#define DAQMX_RANGE_TC_W        (0x0208)
#define DAQMX_RANGE_TC_L        (0x0209)
#define DAQMX_RANGE_TC_U        (0x020A)
#define DAQMX_RANGE_TC_KP       (0x020B) /* KpvsAu7Fe */
#define DAQMX_RANGE_TC_PL       (0x020C) /* PLATINEL */
#define DAQMX_RANGE_TC_PR       (0x020D) /* PR40-20 */
#define DAQMX_RANGE_TC_NNM      (0x020E) /* NiNiMo */
#define DAQMX_RANGE_TC_WR       (0x020F) /* WRe3-25 */
#define DAQMX_RANGE_TC_WWR      (0x0210) /* W/WRe26 */
#define DAQMX_RANGE_TC_AWG      (0x0211) /* N(AWG14) */
#define DAQMX_RANGE_TC_XK       (0x0212) /* since R3.01 */
// RTD (Resistance Temperature Detector) 1mA
#define DAQMX_RANGE_RTD_1MAPT           (0x0300) /* PT100 */
#define DAQMX_RANGE_RTD_1MAJPT          (0x0301) /* JPT100 */
#define DAQMX_RANGE_RTD_1MAPTH          (0x0302) /* PT100 H */
#define DAQMX_RANGE_RTD_1MAJPTH         (0x0303) /* JPT100 H */
#define DAQMX_RANGE_RTD_1MANIS          (0x0304) /* Ni100 S */
#define DAQMX_RANGE_RTD_1MANID          (0x0305) /* Ni100 D */
#define DAQMX_RANGE_RTD_1MANI120        (0x0306) /* Ni120 */
#define DAQMX_RANGE_RTD_1MAPT50         (0x0307) /* PT50 */
#define DAQMX_RANGE_RTD_1MACU10GE       (0x0308) /* CU10 GE */
#define DAQMX_RANGE_RTD_1MACU10LN       (0x0309) /* CU10 L&N */
#define DAQMX_RANGE_RTD_1MACU10WEED     (0x030A) /* CU10 WEED */
#define DAQMX_RANGE_RTD_1MACU10BAILEY   (0x030B) /* CU10 BAILEY */
#define DAQMX_RANGE_RTD_1MAJ263B        (0x030C) /* J263B */
#define DAQMX_RANGE_RTD_1MACU10A392     (0x030D) /* CU10  a=0.00392 */
#define DAQMX_RANGE_RTD_1MACU10A393     (0x030E) /* CU10  a=0.00393 */
#define DAQMX_RANGE_RTD_1MACU25         (0x030F) /* CU25  a=0.00425 */
#define DAQMX_RANGE_RTD_1MACU53         (0x0310) /* CU53  a=0.00426035 */
#define DAQMX_RANGE_RTD_1MACU100        (0x0311) /* CU100 a=0.00425 */
#define DAQMX_RANGE_RTD_1MAPT25         (0x0312) /* PT25 */
#define DAQMX_RANGE_RTD_1MACU10GEH      (0x0313) /* CU10 GE H */
#define DAQMX_RANGE_RTD_1MACU10LNH      (0x0314) /* CU10 L&N H */
#define DAQMX_RANGE_RTD_1MACU10WEEDH    (0x0315) /* CU10 WEED H */
#define DAQMX_RANGE_RTD_1MACU10BAILEYH  (0x0316) /* CU10 BAILEY H */
#define DAQMX_RANGE_RTD_1MAPTN          (0x0317) /* PT100 N */
#define DAQMX_RANGE_RTD_1MAJPTN         (0x0318) /* JPT100 N */
#define DAQMX_RANGE_RTD_1MAPTG          (0x0319) /* PT100 G : since R3.01 */
#define DAQMX_RANGE_RTD_1MACU100G       (0x031A) /* CU100 G : since R3.01 */
#define DAQMX_RANGE_RTD_1MACU50G        (0x031B) /* CU50 G : since R3.01 */
#define DAQMX_RANGE_RTD_1MACU10G        (0x031C) /* CU10 G : since R3.01 */
// RTD (Resistance Temperature Detector) 2mA
#define DAQMX_RANGE_RTD_2MAPT           (0x0400) /* PT100 */
#define DAQMX_RANGE_RTD_2MAJPT          (0x0401) /* JPT100 */
#define DAQMX_RANGE_RTD_2MAPTH          (0x0402) /* PT100 H */
#define DAQMX_RANGE_RTD_2MAJPTH         (0x0403) /* JPT100 H */
#define DAQMX_RANGE_RTD_2MAPT50         (0x0404) /* PT50 */
#define DAQMX_RANGE_RTD_2MACU10GE       (0x0405) /* CU10 GE */
#define DAQMX_RANGE_RTD_2MACU10LN       (0x0406) /* CU10 L&N */
#define DAQMX_RANGE_RTD_2MACU10WEED     (0x0407) /* CU10 WEED */
#define DAQMX_RANGE_RTD_2MACU10BAILEY   (0x0408) /* CU10 BAILEY */
#define DAQMX_RANGE_RTD_2MAJ263B        (0x0409) /* J263*B */
#define DAQMX_RANGE_RTD_2MACU10A392     (0x040A) /* CU10  a=0.00392 */
#define DAQMX_RANGE_RTD_2MACU10A393     (0x040B) /* CU10  a=0.00393 */
#define DAQMX_RANGE_RTD_2MACU25         (0x040C) /* CU25  a=0.00425 */
#define DAQMX_RANGE_RTD_2MACU53         (0x040D) /* CU53  a=0.00426035 */
#define DAQMX_RANGE_RTD_2MACU100        (0x040E) /* CU100 a=0.00425 */
#define DAQMX_RANGE_RTD_2MAPT25         (0x040F) /* PT25 */
#define DAQMX_RANGE_RTD_2MACU10GEH      (0x0410) /* CU10 GE H */
#define DAQMX_RANGE_RTD_2MACU10LNH      (0x0411) /* CU10 L&N H */
#define DAQMX_RANGE_RTD_2MACU10WEEDH    (0x0412) /* CU10 WEED H */
#define DAQMX_RANGE_RTD_2MACU10BAILEYH  (0x0413) /* CU10 BAILEY H */
#define DAQMX_RANGE_RTD_2MAPTN          (0x0414) /* PT100 N */
#define DAQMX_RANGE_RTD_2MAJPTN         (0x0415) /* JPT100 N */
#define DAQMX_RANGE_RTD_2MACU100G       (0x0416) /* CU100 G : since R3.01 */
#define DAQMX_RANGE_RTD_2MACU50G        (0x0417) /* CU50 G : since R3.01 */
#define DAQMX_RANGE_RTD_2MACU10G        (0x0418) /* CU10 G : since R3.01 */
// DI (Contact)
#define DAQMX_RANGE_DI_LEVEL    (1)
#define DAQMX_RANGE_DI_CONTACT  (2)
// DI : detail
#define DAQMX_RANGE_DI_LEVEL_AI         (0x0100) /* AI */
#define DAQMX_RANGE_DI_CONTACT_AI4      (0x0101) /* AI  4ch */
#define DAQMX_RANGE_DI_CONTACT_AI10     (0x0102) /* AI 10ch */
#define DAQMX_RANGE_DI_LEVEL_DI         (0x0103) /* DI */
#define DAQMX_RANGE_DI_CONTACT_DI       (0x0104) /* DI */
// DI : detail since R2.01
#define DAQMX_RANGE_DI_LEVEL_DI5V       (DAQMX_RANGE_DI_LEVEL_DI) /* DI DC5V */
#define DAQMX_RANGE_DI_LEVEL_DI24V      (0x0105)                  /* DI DC24V */
// DI : detail since R3.01
#define DAQMX_RANGE_DI_CONTACT_AI30     (DAQMX_RANGE_DI_CONTACT_AI10) /* AI 30ch */
// RTD : 0.25mA since R2.01
#define DAQMX_RANGE_RTD_025MAPT500      (0x0500) /* PT500 */
#define DAQMX_RANGE_RTD_025MAPT1K       (0x0501) /* PT1000 */
// RES : since R2.01
#define DAQMX_RANGE_RES_20              (0x0600) /*   20 ohm */
#define DAQMX_RANGE_RES_200             (0x0601) /*  200 ohm */
#define DAQMX_RANGE_RES_2K              (0x0602) /* 2000 ohm */
// STR : Strain since R2.01
#define DAQMX_RANGE_STRAIN_2K           (0x0700) /*   2000 uSTR */
#define DAQMX_RANGE_STRAIN_20K          (0x0701) /*  20000 uSTR */
#define DAQMX_RANGE_STRAIN_200K         (0x0702) /* 200000 uSTR */
// AO : since R2.01
#define DAQMX_RANGE_AO_10V              (0x1000) /* -10 -- 10 V */
#define DAQMX_RANGE_AO_20MA             (0x1001) /*   4 -- 20 mA */
// PWM (Pulse Width Modulation) : since R2.01
#define DAQMX_RANGE_PWM_1MS             (0x1100) /*  1 msec */
#define DAQMX_RANGE_PWM_10MS            (0x1101) /* 10 msec */
// COM : since R3.01
#define DAQMX_RANGE_COM_CAN             (0x0800)
// PI : since R3.01
#define DAQMX_RANGE_PI_LEVEL            (0x0900)
#define DAQMX_RANGE_PI_CONTACT          (0x0901)
//-- ---- ---- ---- ---- ---- Output
#define DAQMX_OUTPUT_NONE               (0) /* not output */
#define DAQMX_OUTPUT_AO_10V             (DAQMX_RANGE_AO_10V)
#define DAQMX_OUTPUT_AO_20MA            (DAQMX_RANGE_AO_20MA)
#define DAQMX_OUTPUT_PWM_1MS            (DAQMX_RANGE_PWM_1MS)
#define DAQMX_OUTPUT_PWM_10MS           (DAQMX_RANGE_PWM_10MS)
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// Type
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// DAQ descriptor
// If Visual Basic, type as Long.
// since R3.00 : int -> void *
typedef void * DAQMX;
// 64 bit data
#ifndef DAQINT64
#if defined(WIN32) || defined(_WIN32_WCE)
typedef __int64   DAQINT64;
#else
typedef long long DAQINT64; //gcc
#endif
#endif //DAQINT64
// 64 bit data for VB
typedef union {
    struct {
        unsigned int aLow;
        unsigned int aHigh;
    } aVB;
    DAQINT64   aC;
} MXINT64;
typedef DAQINT64 MXDataNo;
typedef DAQINT64 MXUserTime;
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// Structure (8 bytes align)
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
//-- ---- ---- ---- ---- ---- Date Time
typedef struct {
    time_t aTime;        //seconds
    int    aMilliSecond; //msec
} MXDateTime; //8 bytes
//-- ---- ---- ---- ---- ---- Alarm
typedef struct {
    int aType;    //DAQMX_ALARM_xxx
    int aReserve;
    int aON;      //value without decimal point position when ON
    int aOFF;     //value without decimal point position when OFF
} MXAlarm; //16 bytes
//-- ---- ---- ---- ---- ---- Measured Data
typedef struct {
    int aValue;                 //value without decimal point position
    int aStatus;                //DAQMX_DATA_xxx
    int aAlarm[DAQMX_NUMALARM]; //DAQMX_VALID_xxx
    //R3.00 alarm 2 -> 4
    //int align[2];
} MXDataInfo; //24 bytes
//-- ---- ---- ---- ---- ---- Channel Information
typedef struct {
    int aSpanMin;  //value without decimal point position
    int aSpanMax;
    int aScaleMin;
    int aScaleMax;
    int aRefChNo;  //1 origin
    int aChatFilter; //changed R3.01
} MXChConfigAIDI; //24 bytes
typedef struct {
    int aFilter;  //DAQMX_FILTER_xxx
    int aRJCType; //DAQMX_RJC_xxx
    int aRJCVolt; //if external, voltage value.
    int aBurnout; //DAQMX_BURNOUT_xxx
} MXChConfigAI; //16 bytes
typedef struct {
    int aDeenergize; //DAQMX_VALID_xxx
    int aHold;       //DAQMX_VALID_xxx
    //R3.00 alarm 2-> 4
    unsigned char aRefAlarm[DAQMX_NUMCHANNEL][DAQMX_NUMALARM]; //DAQMX_VALID_xxx
} MXChConfigDO; //128 -> 248 bytes
typedef struct {
    int  aChNo;      //1-60
    int  aPoint;     //0-4
    int  aValid;     //DAQMX_VALID_xxx
    int  aKind;      //DAQMX_CHKIND_xxx
    int  aRange;     //DAQMX_RANGE_xxx
    int  aScaleType; //DAQMX_SCALE_xxx
    char aUnit[(DAQMX_MAXUNITLEN + 1)];
    char align1;
    char aTag[(DAQMX_MAXTAGLEN + 1)];
    char aComment[(DAQMX_MAXCOMMENTLEN + 1)];
    char align2;
    //R3.00 alarm 2 -> 4
    MXAlarm aAlarm[DAQMX_NUMALARM];
} MXChID; //112 -> 144 bytes
typedef struct {
    MXChID         aChID;
    MXChConfigAIDI aAIDI;
    MXChConfigAI   aAI;
    MXChConfigDO   aDO;
} MXChConfig; //280 -> 432 bytes
typedef struct {
    MXChConfig aChConfig[DAQMX_NUMCHANNEL];
} MXChConfigData; //16800 -> 25920 bytes
typedef struct {
    MXChID aChID;
    int    aFIFONo;    //0 origin
    int    aFIFOIndex; //0 origin
    double aOrigMin;
    double aOrigMax;
    double aDispMin;
    double aDispMax;
    double aRealMin;
    double aRealMax;
} MXChInfo; //168 -> 200 bytes
//-- ---- ---- ---- ---- ---- System
typedef struct {
    int           aOption;
    int           aCheck;  //no use
    char          aSerial[(DAQMX_MAXSERIALLEN + 1)];
    unsigned char aMAC[DAQMX_NUMMACADDR];
} MXProductInfo; //24 bytes
typedef struct {
    int           aType;        //DAQMX_UNITTYPE_xxx
    int           aStyle;       //firmware
    int           aNo;          //user defined
    int           aTempUnit;    //DAQMX_TEMPUNIT_xxx
    int           aCFTimeout;   //for writing CF (second)
    int           aCFWriteMode; //DAQMX_CFWRITEMODE_xxx
    int           aFrequency;   //power source
    int           aReserve;
    char          aPartNo[(DAQMX_MAXPARTNOLEN + 1)];
    MXProductInfo aProduct;
} MXUnitData; //64 bytes
typedef struct {
    int  aType;         //DAQMX_MODULE_xxx
    int  aChNum;        //DAQMX_CHNUM_xxx
    int  aInterval;     //DAQMX_INTERVAL_xxx
    int  aIntegralTime; //DAQMX_INTEGRAL_xxx
    int  aStandbyType;  //DAQMX_MODULE_xxx
    int  aRealType;     //DAQMX_MODULE_xxx
    int  aStatus;       //DAQMX_VALID_xxx
    int  aVersion;      //hardware
    int  aTerminalType; //DAQMX_TERMINAL_xxx
    int  aFIFONo;       //0 origin
    MXProductInfo aProduct;
} MXModuleData; //64 bytes
typedef struct {
    MXUnitData   aUnit;
    MXModuleData aModule[DAQMX_NUMMODULE];
} MXSystemInfo; //448 bytes
//-- ---- ---- ---- ---- ---- Status
typedef struct {
    int aStatus;    //DAQMX_CFSTATUS_xxx
    int aSize;      //KB
    int aRemain;    //KB
    int aReserve;
} MXCFInfo; //16 bytes
typedef struct {
    int      aNo;       //0 origin
    int      aStatus;   //DAQMX_FIFOSTAT_xxx
    int      aInterval; //msec
    int      aReserve;
    MXDataNo aOldNo;
    MXDataNo aNewNo;
} MXFIFOInfo; //32 bytes
typedef struct {
    int        aUnitStatus; //DAQMX_UNITSTAT_xxx
    int        aConfigCnt;
    int        aTimeCnt;
    int        aFIFONum;
    int        aBackup;     //DAQMX_VALID_xxx
    int        aReserve;
    MXCFInfo   aCFInfo;
    MXFIFOInfo aFIFOInfo[DAQMX_NUMFIFO];
    MXDateTime aDateTime; //R2.01
} MXStatus; //144 bytes
//-- ---- ---- ---- ---- ---- Network
typedef struct {
    unsigned int aAddress;
    unsigned int aPort;
    unsigned int aSubMask;
    unsigned int aGateway;
    char         aHost[(DAQMX_MAXHOSTNAMELEN + 1)];
    char         align[7];
} MXNetInfo; //88 bytes
//-- ---- ---- ---- ---- ---- strain : since R2.01
typedef struct {
    int aValid; //DAQMX_VALID_xxx
    int aValue;
} MXBalance; //8 bytes
typedef struct {
    MXBalance aBalance[DAQMX_NUMBALANCE];
} MXBalanceData; //480 bytes
typedef struct {
    int aResult[DAQMX_NUMBALANCE]; //DAQMX_BALANCE_xxx
} MXBalanceResult; //240 bytes
//-- ---- ---- ---- ---- ---- Output : since R2.01
typedef struct {
    int aType;        //DAQMX_OUTPUT_xxx
    int aIdleChoice;  //DAQMX_CHOICE_xxx
    int aErrorChoice; //DAQMX_CHOICE_xxx
    int aPresetValue;
    int aPulseTime;   //1-30000 for PWM
    int aReserve;
} MXOutput; //24 bytes
typedef struct {
    MXOutput aOutput[DAQMX_NUMOUTPUT];
} MXOutputData; //1440 bytes
//-- ---- ---- ---- ---- ---- All Configure
typedef struct {
    MXSystemInfo   aSystemInfo;
    MXStatus       aStatus;
    MXNetInfo      aNetInfo;
    MXChConfigData aChConfigData;
    MXBalanceData  aBalanceData; //R2.01
    MXOutputData   aOutputData;  //R2.01
} MXConfigData; //19400 -> 28520 bytes
//-- ---- ---- ---- ---- ---- DO
typedef struct {
    int aValid; //DAQMX_VALID_xxx
    int aONOFF; //DAQMX_VALID_xxx
} MXDO; //8 bytes
typedef struct {
    MXDO aDO[DAQMX_NUMDO];
} MXDOData; //480 bytes
//-- ---- ---- ---- ---- ---- Segment
typedef struct {
    int aPattern[DAQMX_NUMSEGMENT];
} MXSegment; //8 bytes
//-- ---- ---- ---- ---- ---- AO/PWM : since R2.01
typedef struct {
    int aValid; //DAQMX_VALID_xxx
    int aValue;
} MXAOPWM; //8 bytes
typedef struct {
    MXAOPWM aAOPWM[DAQMX_NUMAOPWM];
} MXAOPWMData; //480 bytes
typedef struct {
    int aTransmit[DAQMX_NUMAOPWM]; //DAQMX_TRANSMIT_xxx
} MXTransmit; //240 bytes
///////////////////////////////////////////////////////////////////////
#ifdef __cplusplus
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// Date and Time
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
class DAQMX_API CDAQMXDateTime : public CDAQDateTime
{
    //-- ---- ---- ---- ---- ---- Constructs
public:
    CDAQMXDateTime(time_t time = 0,
                   int    milliSecond = 0);
    CDAQMXDateTime(MXDateTime * pMXDateTime);
    virtual ~CDAQMXDateTime(void);

    //-- ---- ---- ---- ---- ---- Attributes

    //-- ---- ---- ---- ---- ---- Overrides
public:
    // since R2.01
    virtual int isObject(const char * classname = "CDAQMXDateTime");

    //-- ---- ---- ---- ---- ---- Implements
public:
    // copy structure
    void getMXDateTime(MXDateTime * pMXDateTime);
    void setMXDateTime(MXDateTime * pMXDateTime);
    static void initMXDateTime(MXDateTime * pMXDateTime);

    //-- ---- ---- ---- ---- ---- Operations
public:
    CDAQMXDateTime & operator=(CDAQMXDateTime & cMXDateTime);

};
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// Channel Information
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
class DAQMX_API CDAQMXChID : public CDAQChInfo
{
    //-- ---- ---- ---- ---- ---- Constructs
public:
    CDAQMXChID(MXChID * pMXChID = NULL);
    virtual ~CDAQMXChID(void);

    //-- ---- ---- ---- ---- ---- Attributes
protected:
    int  m_valid;     //DAQMX_VALID_xxx
    int  m_kind;      //DAQMX_CHKIND_xxx
    int  m_range;     //DAQMX_RANGE_xxx
    int  m_scaleType; //DAQMX_SCALE_xxx
    char m_unit[(DAQMX_MAXUNITLEN + 1)];
    char m_tag[(DAQMX_MAXTAGLEN + 1)];
    char m_comment[(DAQMX_MAXCOMMENTLEN + 1)];
    MXAlarm m_alarm[DAQMX_NUMALARM];

    //-- ---- ---- ---- ---- ---- Overrides
    // chType : channel type is 0 (fixed)
public:
    virtual void initialize(void);
    // get
    virtual int getChType(void);
    // set
    virtual void setChType(int chType);
    // since R2.01
    virtual int isObject(const char * classname = "CDAQMXChID");

    //-- ---- ---- ---- ---- ---- Implements
    // levelNo : level number is 1 origin
public:
    // copy structure
    void getMXChID(MXChID * pMXChID);
    void setMXChID(MXChID * pMXChID);
    static void initMXChID(MXChID * pMXChID);
    // get
    int isValid(void);  //DAQMX_VALID_xxx
    int getKind(void);  //DAQMX_CHKIND_xxx
    int getRange(void); //DAQMX_RANGE_xxx
    int getScale(void); //DAQMX_SCALE_xxx
    const char * getUnit(void);
    const char * getTag(void);
    const char * getComment(void);
    int getAlarmType(int levelNo); //DAQMX_ALARM_xxx
    int getAlarmValueON(int levelNo);
    int getAlarmValueOFF(int levelNo);
    // set
    void setValid(int bValid);
    void setType(int iKind,  //DAQMX_CHKIND_xxx
                 int iRange, //DAQMX_RANGE_xxx
                 int iScale = DAQMX_SCALE_NONE);
    void setUnit(const char * strUnit);
    void setTag(const char * strTag);
    void setComment(const char * strComment);
    void setAlarmValue(int levelNo,
                       int iAlarmType = DAQMX_ALARM_NONE,
                       int valueON = 0,
                       int valueOFF = 0);
    // since R2.01
    // chname : channel name is unit and channel number as integer.
    int getChName(int unitno = 0);
    static int toChName(int chno,
                        int unitno = 0);
    static int toChNo(int chname);
    static int toUnitNo(int chname);

protected:
    // access structure directly
    MXAlarm * getMXAlarm(int levelNo);

    //-- ---- ---- ---- ---- ---- Operations
public:
    CDAQMXChID & operator=(CDAQMXChID & cMXChID);

};
class DAQMX_API CDAQMXChInfo : public CDAQMXChID
{
    //-- ---- ---- ---- ---- ---- Constructs
public:
    CDAQMXChInfo(MXChInfo * pMXChInfo = NULL);
    virtual ~CDAQMXChInfo(void);

    //-- ---- ---- ---- ---- ---- Attributes
protected:
    int m_FIFONo;
    int m_FIFOIndex;
    double m_origMin;
    double m_origMax;
    double m_dispMin;
    double m_dispMax;
    double m_realMin;
    double m_realMax;

    //-- ---- ---- ---- ---- ---- Overrides
public:
    virtual void initialize(void);
    // since R2.01
    virtual int isObject(const char * classname = "CDAQMXChInfo");

    //-- ---- ---- ---- ---- ---- Implements
    // fifoNo : FIFO number is 0 origin
    // fifoIndex : index in FIFO is 0 origin
public:
    // copy structure
    void getMXChInfo(MXChInfo * pMXChInfo);
    void setMXChInfo(MXChInfo * pMXChInfo);
    static void initMXChInfo(MXChInfo * pMXChInfo);
    // get FIFO
    int getFIFONo(void);
    int getFIFOIndex(void);
    // get values
    double getOriginalMin(void);
    double getOriginalMax(void);
    double getDisplayMin(void);
    double getDisplayMax(void);
    double getRealMin(void);
    double getRealMax(void);
    // set FIFO
    void setFIFONo(int fifoNo);
    void setFIFOIndex(int fifoIndex);
    // block
    //void setFromBlockChInfo(unsigned char * pBlock);

    //-- ---- ---- ---- ---- ---- Operations
public:
    CDAQMXChInfo & operator=(CDAQMXChInfo & cMXChInfo);

};
class DAQMX_API CDAQMXChConfig : public CDAQMXChID
{
    //-- ---- ---- ---- ---- ---- Constructs
public:
    CDAQMXChConfig(MXChConfig * pMXChConfig = NULL);
    virtual ~CDAQMXChConfig(void);

    //-- ---- ---- ---- ---- ---- Attributes
protected:
    MXChConfigAIDI m_MXChConfigAIDI;
    MXChConfigAI m_MXChConfigAI;
    MXChConfigDO m_MXChConfigDO;
    int m_nItemError; //R2.01

    //-- ---- ---- ---- ---- ---- Overrides
public:
    virtual void initialize(void);
    // since R2.01
    virtual int isObject(const char * classname = "CDAQMXChConfig");

    //-- ---- ---- ---- ---- ---- Implements
    // refChNo : reference channel number is 1 origin as channel number 
    // levelNo : level number is 1 origin @see DAQDARWIN
    // scalePoint : If scalling, decimal point position.
public:
    // copy structure
    void getMXChConfig(MXChConfig * pMXChConfig);
    void setMXChConfig(MXChConfig * pMXChConfig);
    static void initMXChConfig(MXChConfig * pMXChConfig);
    // get AIDI
    int getSpanMin(void);
    int getSpanMax(void);
    int getScaleMin(void);
    int getScaleMax(void);
    int getRefChNo(void);
    // get AI
    int getFilter(void);
    int getRJCType(void);
    int getRJCVolt(void);
    int getBurnout(void);
    // get DO
    int isDeenergize(void);
    int isHold(void);
    unsigned char isRefAlarm(int refChNo,
                             int levelNo);
    // set
    void setRefChNo(int refChNo);
    void setFilter(int iFilter); //DAQMX_FILTER_xxx
    void setBurnout(int iBurnout); //DAQMX_BURNOUT_xxx
    void setRJCType(int iRJCType, //DAQMX_RJC_xxx
                    int volt = 0);
    void setAlarm(int levelNo,
                  int iAlarmType,
                  int value,
                  int histerisys = 0);
    // set range
    void setSKIP(void);
    void setVOLT(int iRangeVOLT); //DAQMX_RANGE_VOLT_xxx
    void setTC(int iRangeTC, //DAQMX_RANGE_TC_xxx
               int iTempUnit = DAQMX_TEMPUNIT_C);
    void setRTD(int iRangeRTD, //DAQMX_RANGE_RTD_xxx
                int iTempUnit = DAQMX_TEMPUNIT_C);
    void setDI(int iRangeDI); //DAQMX_RANGE_DI_xxx : detail only
    void setDELTA(int refChNo,
                  int iRange, //DAQMX_RANGE_xxx : if DI, detail
                  int iTempUnit = DAQMX_TEMPUNIT_C);
    // set option
    void setSpan(int spanMin,
                 int spanMax,
                 int iTempUnit = DAQMX_TEMPUNIT_C);
    void setScalling(int scaleMin,
                     int scaleMax,
                     int scalePoint,
                     int iTempUnit = DAQMX_TEMPUNIT_C);
    // set DO
    void setDeenergize(int bDeenergize);
    void setHold(int bHold);
    void setRefAlarm(int refChNo, //if all, DAQMX_REFCHNO_ALL
                     int levelNo, //if all, DAQMX_LEVELNO_ALL
                     int bValid);
    // degF <--> degC
    void changeRange(int iTempUnit); //DAQMX_TEMPUNIT_xxx
    // block
    //void setFromBlockChConfig(unsigned char * pBlock);
    // check
    int isCorrect(int iTempUnit = DAQMX_TEMPUNIT_C);
    //-- ---- ---- ---- ---- ---- since R2.01
    int getItemError(void);
    static int getRangePoint(int iRange,  //DAQMX_RANGE_xxx : if DI, needs detail
                             int iTempUnit = DAQMX_TEMPUNIT_C);
    static int getRangeMin(int iRange,  //DAQMX_RANGE_xxx : if DI, needs detail
                           int iTempUnit = DAQMX_TEMPUNIT_C);
    static int getRangeMax(int iRange,  //DAQMX_RANGE_xxx : if DI, needs detail
                           int iTempUnit = DAQMX_TEMPUNIT_C);
    // range
    void setRES(int iRangeRES); //DAQMX_RANGE_RES_xxx
    void setSTRAIN(int iRangeSTR); //DAQMX_RANGE_STR_xxx
    void setAO(int iRangeAO); //DAQMX_RANGE_AO_xxx
    void setPWM(int iRangePWM); //DAQMX_RANGE_PWM_xxx
    //-- ---- ---- ---- ---- ---- since R3.01
    // range
    void setCOM(int iRangeCOM); //DAQMX_RANGE_COM_xxx
    void setPULSE(int iRangePULSE); //DAQMX_RANGE_PI_xxx
    // setup Pulse
    int isChatFilter(void);
    void setChatFilter(int bChatFilter);

    //-- ---- ---- ---- ---- ---- Operations
public:
    CDAQMXChConfig & operator=(CDAQMXChConfig & cMXChConfig);

};
class DAQMX_API CDAQMXChConfigData
{
    //-- ---- ---- ---- ---- ---- Constructs
public:
    CDAQMXChConfigData(MXChConfigData * pMXChConfigData = NULL);
    virtual ~CDAQMXChConfigData();

    //-- ---- ---- ---- ---- ---- Attributes
protected:
    CDAQMXChConfig m_cMXChConfig[DAQMX_NUMCHANNEL];
    CDAQMXChConfig * m_pcMXChConfig; //deprecated
    int m_nItemError; //R2.01

    //-- ---- ---- ---- ---- ---- Overrides

    //-- ---- ---- ---- ---- ---- Implements
    // chNo : channel number is 1 origin
public:
    virtual void initialize(void);
    // copy strcture
    void getMXChConfigData(MXChConfigData * pMXChConfigData);
    void setMXChConfigData(MXChConfigData * pMXChConfigData);
    static void initMXChConfigData(MXChConfigData * pMXChConfigData);
    void setMXChConfig(MXChConfig * pMXChConfig); //by each channel
    // access class
    CDAQMXChConfig * getClassMXChConfig(int chNo);
    // set
    void setRRJC(int chNo,
                 int refChNo);
    void changeRange(int iTempUnit); //DAQMX_TEMPUNIT_xxx
    // packet
    //void setFromAckGetConfigPacket(unsigned char * pPacket);
    // check
    int isCorrect(int iTempUnit = DAQMX_TEMPUNIT_C);
    // since R2.01
    int getItemError(void);
    virtual int isObject(const char * classname = "CDAQMXChConfigData");

    //-- ---- ---- ---- ---- ---- Operations
public:
    CDAQMXChConfigData & operator=(CDAQMXChConfigData & cMXChConfigData);

};
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// Measured Data
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
class DAQMX_API CDAQMXDataInfo : public CDAQDataInfo
{
    //-- ---- ---- ---- ---- ---- Constructs
public:
    CDAQMXDataInfo(MXDataInfo * pMXDataInfo = NULL,
                   CDAQMXChInfo * pcMXChInfo = NULL);
    virtual ~CDAQMXDataInfo();

    //-- ---- ---- ---- ---- ---- Attributes
protected:
    //MXDataInfo m_MXDataInfo;
    int m_dataStatus;            //DAQMX_DATA_xxx
    int m_alarm[DAQMX_NUMALARM]; //DAQMX_VALID_xxx

    //-- ---- ---- ---- ---- ---- Overrides
public:
    virtual void initialize(void);
    // since R2.01
    virtual int isObject(const char * classname = "CDAQMXDataInfo");

    //-- ---- ---- ---- ---- ---- Implements
    // levelNo : level number is 1 origin
public:
    // copy structure
    void getMXDataInfo(MXDataInfo * pMXDataInfo);
    void setMXDataInfo(MXDataInfo * pMXDataInfo);
    static void initMXDataInfo(MXDataInfo * pMXDataInfo);
    // get
    int getStatus(void);
    int isAlarm(int levelNo);
    // set
    void setStatus(int iDataStatus); //DAQMX_DATA_xxx
    void setAlarm(int levelNo,
                  int bValid);
    // block
    //void setFromBlockData(unsigned char * pBlock,
    //                      int             dataType);
    // class
    CDAQMXChInfo * getClassMXChInfo(void);
    void setClassMXChInfo(CDAQMXChInfo * pcMXChInfo);
    // utility
    static const char * getAlarmName(int iAlarmType);
    static int getMaxLenAlarmName(void);

    //-- ---- ---- ---- ---- ---- Operations
public:
    CDAQMXDataInfo & operator=(CDAQMXDataInfo & cMXDataInfo);

};
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// System Configuration
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
class DAQMX_API CDAQMXSysInfo
{
    //-- ---- ---- ---- ---- ---- Constructs
public:
    CDAQMXSysInfo(MXSystemInfo * pMXSystemInfo = NULL);
    virtual ~CDAQMXSysInfo(void);

    //-- ---- ---- ---- ---- ---- Attributes
protected:
    MXSystemInfo m_MXSystemInfo;
    int m_nItemError; //R2.01

    //-- ---- ---- ---- ---- ---- Overrides

    //-- ---- ---- ---- ---- ---- Implements
    // moduleNo : module number is 0 origin
    // unitNo : unit number is user define
public:
    virtual void initialize(void);
    // copy structure
    void getMXSystemInfo(MXSystemInfo * pMXSystemInfo);
    void setMXSystemInfo(MXSystemInfo * pMXSystemInfo);
    static void initMXSystemInfo(MXSystemInfo * pMXSystemInfo);
    // get unit
    int getUnitType(void); //DAQMX_UNITTYPE_xxx
    int getStyle(void);
    int getUnitNo(void);
    int getTempUnit(void); //DAQMX_TEMPUNIT_xxx
    int getCFTimeout(void);  //sec
    int getCFWriteMode(void); //DAQMX_CFWRITEMODE_xxx
    int getFrequency(void);
    const char * getPartNo(void);
    int getOption(void);
    const char * getUnitSerial(void);
    unsigned char getMAC(int index);
    // get module
    int getModuleType(int moduleNo);    //DAQMX_MODULE_xxx
    int getChNum(int moduleNo);         //DAQMX_CHNUM_xxx
    int getInterval(int moduleNo);      //DAQMX_INTERVAL_xxx
    int getIntegral(int moduleNo);      //DAQMX_INTEGRAL_xxx
    int getStandbyType(int moduleNo);   //DAQMX_MODULE_xxx
    int getRealType(int moduleNo);      //DAQMX_MODULE_xxx
    int isModuleValid(int moduleNo);    //DAQMX_VALID_xxx
    int getModuleVersion(int moduleNo);
    int getTerminalType(int moduleNo);  //DAQMX_TERMINAL_xxx
    int getFIFONo(int moduleNo);
    const char * getModuleSerial(int moduleNo);
    // set for configuration
    void setUnitNo(int unitNo);
    void setTempUnit(int iTempUnit); //DAQMX_TEMPUNIT_xxx
    void setCFTimeout(int timeout = 60); //sec
    void setCFWriteMode(int iCFWriteMode); //DAQMX_CFWRITEMODE_xxx
    void setModule(int moduleNo,
                   int iModuleType, //DAQMX_MODULE_xxx
                   int iChNum,      //DAQMX_CHNUM_xxx
                   int iInterval,   //DAQMX_INTERVAL_xxx
                   int iHz = DAQMX_INTEGRAL_AUTO);
    int setRealModule(int moduleNo);
    // packet
    //void setFromAckGetUnitInfoPacket(unsigned char * pPacket);
    //void setFromAckGetConfigPacket(unsigned char * pPacket);
    // check
    int isCorrect(void);
    int getItemError(void);
    virtual int isObject(const char * classname = "CDAQMXSysInfo");
    // utility
    static int toStyleVersion(int style);

protected:
    // access structure directly
    MXModuleData * getMXModuleData(int moduleNo);

    //-- ---- ---- ---- ---- ---- Operations
public:
    CDAQMXSysInfo & operator=(CDAQMXSysInfo & cMXSysInfo);

};
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// Status
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
class DAQMX_API CDAQMXStatus
{
    //-- ---- ---- ---- ---- ---- Constructs
public:
    CDAQMXStatus(MXStatus * pMXStatus = NULL);
    virtual ~CDAQMXStatus(void);

    //-- ---- ---- ---- ---- ---- Attributes
protected:
    MXStatus m_MXStatus;

    //-- ---- ---- ---- ---- ---- Overrides

    //-- ---- ---- ---- ---- ---- Implements
    // fifoNo : FIFO number is 0 origin
public:
    virtual void initialize(void);
    // copy structure
    void getMXStatus(MXStatus * pMXStatus);
    void setMXStatus(MXStatus * pMXStatus);
    static void initMXStatus(MXStatus * pMXStatus);
    // FIFO
    int getFIFONum(void);
    int getFIFOStatus(int fifoNo); //DAQMX_FIFOSTAT_xxx
    int getInterval(int fifoNo);   //msec
    MXDataNo getOldDataNo(int fifoNo);
    MXDataNo getNewDataNo(int fifoNo);
    // CF
    int getCFStatus(void);    //DAQMX_CFSTATUS_xxx
    int getCFSize(void);      //KB
    int getCFRemain(void);    //KB
    // unit
    int getUnitStatus(void); //DAQMX_UNITSTAT_xxx
    int getConfigCnt(void);
    int getTimeCnt(void);
    // packet
    //void setFromAckGetStatusPacket(unsigned char * pPacket);
    //void setFromAckGetConfigPacket(unsigned char * pPacket);
    // since R2.01
    int isBackup(void); //DAQMX_VALID_xxx
    time_t getTime(void);
    int getMilliSecond(void);
    void getDateTime(CDAQDateTime & cDateTime);
    static int isDataNo(MXDataNo dataNo);
    virtual int isObject(const char * classname = "CDAQMXStatus");

protected:
    // access structure directly
    MXFIFOInfo * getMXFIFOInfo(int fifoNo);

    //-- ---- ---- ---- ---- ---- Operations
public:
    CDAQMXStatus & operator=(CDAQMXStatus & cMXStatus);

};
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// Network
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
class DAQMX_API CDAQMXNetInfo
{
    //-- ---- ---- ---- ---- ---- Constructs
public:
    CDAQMXNetInfo(MXNetInfo * pMXNetInfo = NULL);
    virtual ~CDAQMXNetInfo();

    //-- ---- ---- ---- ---- ---- Attributes
protected:
    MXNetInfo m_MXNetInfo;

    //-- ---- ---- ---- ---- ---- Overrides

    //-- ---- ---- ---- ---- ---- Impelments
public:
    virtual void initialize(void);
    // copy structure
    void getMXNetInfo(MXNetInfo * pMXNetInfo);
    void setMXNetInfo(MXNetInfo * pMXNetInfo);
    static void initMXNetInfo(MXNetInfo * pMXNetInfo);
    // get
    unsigned int getAddress(void);
    unsigned int getPort(void);
    unsigned int getSubMask(void);
    unsigned int getGateway(void);
    const char * getHost(void);
    // packet
    //void setFromAckGetConfigPacket(unsigned char * pPacket);
    // since R2.01
    virtual int isObject(const char * classname = "CDAQMXNetInfo");
    static int getPart(unsigned int address,
                       int          index);
  
    //-- ---- ---- ---- ---- ---- Operators
public:
    CDAQMXNetInfo & operator=(CDAQMXNetInfo & cMXNetInfo);

};
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// Balance : since R2.01
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
class DAQMX_API CDAQMXBalanceData
{
    //-- ---- ---- ---- ---- ---- Constructs
public:
    CDAQMXBalanceData(MXBalanceData * pMXBalanceData = NULL);
    virtual ~CDAQMXBalanceData(void);

    //-- ---- ---- ---- ---- ---- Attributes
protected:
    MXBalanceData m_MXBalanceData;

    //-- ---- ---- ---- ---- ---- Overrides

    //-- ---- ---- ---- ---- ---- Implements
    // balanceNo : Balance number is same as channel (1 origin)
public:
    virtual void initialize(void);
    // copy structure
    void getMXBalanceData(MXBalanceData * pMXBalanceData);
    void setMXBalanceData(MXBalanceData * pMXBalanceData);
    static void initMXBalanceData(MXBalanceData * pMXBalanceData);
    // get
    int getBalanceValid(int balanceNo);
    int getBalanceValue(int balanceNo);
    // set
    void setBalance(int balanceNo,
                    int bValid,
                    int iValue = 0);
    // check
    virtual int isObject(const char * classname = "CDAQMXBalanceData");

protected:
    // access structure directly
    MXBalance * getMXBalance(int balanceNo);

    //-- ---- ---- ---- ---- ---- Operations
public:
    CDAQMXBalanceData & operator=(CDAQMXBalanceData & cMXBalanceData);

};
class DAQMX_API CDAQMXBalanceResult : public CDAQMXBalanceData
{
    //-- ---- ---- ---- ---- ---- Constructs
public:
    CDAQMXBalanceResult(MXBalanceData *   pMXBalanceData = NULL,
                        MXBalanceResult * pMXBalanceResult = NULL);
    virtual ~CDAQMXBalanceResult(void);

    //-- ---- ---- ---- ---- ---- Attributes
protected:
    MXBalanceResult m_MXBalanceResult;

    //-- ---- ---- ---- ---- ---- Overrides
public:
    virtual void initialize(void);
    virtual int isObject(const char * classname = "CDAQMXBalanceResult");

    //-- ---- ---- ---- ---- ---- Implements
    // balanceNo : Balance number is same as channel (1 origin)
public:
    // copy structure
    void getMXBalanceResult(MXBalanceResult * pMXBalanceResult);
    void setMXBalanceResult(MXBalanceResult * pMXBalanceResult);
    static void initMXBalanceResult(MXBalanceResult * pMXBalanceResult);
    // get
    int getResult(int balanceNo);
    // set
    void setResult(int balanceNo,
                   int iResult);

    //-- ---- ---- ---- ---- ---- Operations
public:
    CDAQMXBalanceResult & operator=(CDAQMXBalanceResult & cMXBalanceResult);

};
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// Output : since R2.01
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
class DAQMX_API CDAQMXOutputData
{
    //-- ---- ---- ---- ---- ---- Constructs
public:
    CDAQMXOutputData(MXOutputData * pMXOutputData = NULL);
    virtual ~CDAQMXOutputData(void);

    //-- ---- ---- ---- ---- ---- Attributes
protected:
    MXOutputData m_MXOutputData;

    //-- ---- ---- ---- ---- ---- Overrides

    //-- ---- ---- ---- ---- ---- Implements
    // outputNo : Output number is same as channel (1 origin)
public:
    virtual void initialize(void);
    // copy structure
    void getMXOutputData(MXOutputData * pMXOutputData);
    void setMXOutputData(MXOutputData * pMXOutputData);
    static void initMXOutputData(MXOutputData * pMXOutputData);
    // get
    int getOutputType(int outputNo);
    int getIdleChoice(int outputNo);
    int getErrorChoice(int outputNo);
    int getPresetValue(int outputNo);
    int getPulseTime(int outputNo);
    // set
    void setOutputType(int outputNo,
                       int iOutput); //DAQMX_OUTPUT_xxx
    void setChoice(int outputNo,
                   int idleChoice,  //DAQMX_CHOICE_xxx
                   int errorChoice, //DAQMX_CHOICE_xxx
                   int presetValue = 0);
    void setPulseTime(int outputNo,
                      int pulseTime);
    // check
    virtual int isObject(const char * classname = "CDAQMXOutputData");

protected:
    // access structure directly
    MXOutput * getMXOutput(int outputNo);

    //-- ---- ---- ---- ---- ---- Operations
public:
    CDAQMXOutputData & operator=(CDAQMXOutputData & cMXOutputData);

};
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// Configure
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
class DAQMX_API CDAQMXConfig
{
    //-- ---- ---- ---- ---- ---- Constructs
public:
    CDAQMXConfig(MXConfigData * pMXConfigData = NULL);
    virtual ~CDAQMXConfig();

    //-- ---- ---- ---- ---- ---- Attributes
protected:
    CDAQMXSysInfo m_cMXSysInfo;
    CDAQMXStatus m_cMXStatus;
    CDAQMXNetInfo m_cMXNetInfo;
    CDAQMXChConfigData m_cMXChConfigData;
    CDAQMXBalanceData m_cMXBalanceData; //R2.01
    CDAQMXOutputData m_cMXOutputData; //R2.01
    int m_nItemError; //R2.01

    //-- ---- ---- ---- ---- ---- Overrides

    //-- ---- ---- ---- ---- ---- Impelments
    // chNo : channel number is 1 origin
    // moduleNo : module number is 0 origin
    // xxxMin/Max : If min equals max, span and scale are omitted.
    // scalePoint : If scalling, decimal point position.
public:
    virtual void initialize(void);
    // get class
    CDAQMXSysInfo & getClassMXSysInfo(void);
    CDAQMXStatus & getClassMXStatus(void);
    CDAQMXNetInfo & getClassMXNetInfo(void);
    CDAQMXChConfigData & getClassMXChConfigData(void);
    CDAQMXBalanceData & getClassMXBalanceData(void); //R2.01
    CDAQMXOutputData & getClassMXOutputData(void); //R2.01
    CDAQMXChConfig * getClassMXChConfig(int chNo); //R2.01
    // copy structure
    void getMXConfigData(MXConfigData * pMXConfigData);
    void setMXConfigData(MXConfigData * pMXConfigData);
    static void initMXConfigData(MXConfigData * pMXConfigData);
    // utility
    void reconstruct(int bRealType);
    // set
    void setTempUnit(int iTempUnit); //DAQMX_TEMPUNIT_xxx
    void setDOType(int doNo,
                   int iKind, //DAQMX_CHKIND_DOxxx
                   int bDeenergize = DAQMX_VALID_OFF,
                   int bHold = DAQMX_VALID_OFF);
    void setInterval(int moduleNo,
                     int iInterval,   //DAQMX_INTERVAL_xxx
                     int iHz = DAQMX_INTEGRAL_AUTO);
    // range
    void setSKIP(int chNo);
    void setVOLT(int chNo,
                 int iRangeVOLT, //DAQMX_RANEG_VOLT_xxx
                 int spanMin = 0,
                 int spanMax = 0);
    void setTC(int chNo,
               int iRangeTC, //DAQMX_RANGE_TC_xxx
               int spanMin = 0,
               int spanMax = 0);
    void setRTD(int chNo,
                int iRangeRTD, //DAQMX_RANGE_RTD_xxx
                int spanMin = 0,
                int spanMax = 0);
    void setDI(int chNo,
               int iRangeDI, //DAQMX_RANGE_DI_xxx
               int spanMin = 0,
               int spanMax = 0);
    void setDELTA(int chNo,
                  int refChNo,
                  int spanMin = 0,
                  int spanMax = 0,
                  int iRange = DAQMX_RANGE_REFERENCE); //if DI, detail
    void setRRJC(int chNo,
                 int refChNo,
                 int spanMin = 0,
                 int spanMax = 0);
    void setScalling(int chNo,
                     int scaleMin = 0,
                     int scaleMax = 0,
                     int scalePoint = 0);
    // range : since R2.01
    void setRES(int chNo,
                int iRangeRES, //DAQMX_RANGE_RES_xxx
                int spanMin = 0,
                int spanMax = 0);
    void setSTRAIN(int chNo,
                   int iRangeSTRAIN, //DAQMX_RANGE_STR_xxx
                   int spanMin = 0,
                   int spanMax = 0);
    void setAO(int chNo,
               int iRangeAO, //DAQMX_RANGE_AO_xxx
               int spanMin = 0,
               int spanMax = 0);
    void setPWM(int chNo,
                int iRangePWM, //DAQMX_RANGE_PWM_xxx
                int spanMin = 0,
                int spanMax = 0);
    // range : since R3.01
    void setCOM(int chNo,
                int iRangeCOM, //DAQMX_RANGE_COM_xxx
                int spanMin = 0,
                int spanMax = 0);
    void setPULSE(int chNo,
                  int iRangePULSE, //DAQMX_RANGE_PI_xxx
                  int spanMin = 0,
                  int spanMax = 0);
    // packet
    //void setFromAckGetConfigPacket(unsigned char * pPacket);
    // check
    int isCorrect(void);
    //-- ---- ---- ---- ---- ---- since R2.01
    int getItemError(void);
    // decimal point position of range
    int getSpanPoint(int chNo);
    int getRangePoint(int iRange); //DAQMX_RANGE_xxx : if DI, detail
    // chname : channel name is unit and channel number as integer.
    int getChName(int chNo);
    // AO/PWM
    void setAOType(int aoNo,
                   int iKind, //DAQMX_CHKIND_AOxxx
                   int refChNo = DAQMX_REFCHNO_NONE);
    void setPWMType(int pwmNo,
                    int iKind, //DAQMX_CHKIND_PWMxxx
                    int refChNo = DAQMX_REFCHNO_NONE);
    // utility
    void setChKind(int chNo,
                   int iKind,
                   int refChNo = DAQMX_REFCHNO_NONE);
    virtual int isObject(const char * classname = "CDAQMXConfig");

    //-- ---- ---- ---- ---- ---- Operators
public:
    CDAQMXConfig & operator=(CDAQMXConfig & cMXConfig);

};
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// DO
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
class DAQMX_API CDAQMXDOData
{
    //-- ---- ---- ---- ---- ---- Constructs
public:
    CDAQMXDOData(MXDOData * pMXDOData = NULL);
    virtual ~CDAQMXDOData(void);

    //-- ---- ---- ---- ---- ---- Attributes
protected:
    MXDOData m_MXDOData;

    //-- ---- ---- ---- ---- ---- Overrides

    //-- ---- ---- ---- ---- ---- Implements
    // doNo : do number is same as channel (1 origin)
public:
    virtual void initialize(void);
    // copy structure
    void getMXDOData(MXDOData * pMXDOData);
    void setMXDOData(MXDOData * pMXDOData);
    static void initMXDOData(MXDOData * pMXDOData);
    // get
    int getDOValid(int doNo);
    int getDOONOFF(int doNo);
    // set
    void setDO(int doNo,
               int bValid,
               int bONOFF = DAQMX_VALID_OFF);
    //-- ---- ---- ---- ---- since R2.01
    void setDOONOFF(int bONOFF);
    virtual int isObject(const char * classname = "CDAQMXDOData");

protected:
    // access structure directly
    MXDO * getMXDO(int doNo);

    //-- ---- ---- ---- ---- ---- Operations
public:
    CDAQMXDOData & operator=(CDAQMXDOData & cMXDOData);

};
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// Segment
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
class DAQMX_API CDAQMXSegment
{
    //-- ---- ---- ---- ---- ---- Constructs
public:
    CDAQMXSegment(MXSegment * pMXSegment = NULL);
    CDAQMXSegment(int pattern0,
                  int pattern1);
    virtual ~CDAQMXSegment(void);

    //-- ---- ---- ---- ---- ---- Attributes
protected:
    MXSegment m_MXSegment;

    //-- ---- ---- ---- ---- ---- Overrides

    //-- ---- ---- ---- ---- ---- Implements
    // segmentNo : index of segment is 0 origin
    // pattern : hexdecimal 0-9a-f, if negative, none
public:
    virtual void initialize(void);
    // copy structure
    void getMXSegment(MXSegment * pMXSegment);
    void setMXSegment(MXSegment * pMXSegment);
    static void initMXSegment(MXSegment * pMXSegment);
    // get
    int getPattern(int segmentNo);
    // set
    void setPattern(int segmentNo,
                    int pattern);
    // since R2.01
    virtual int isObject(const char * classname = "CDAQMXSegment");

    //-- ---- ---- ---- ---- ---- Operations
public:
    CDAQMXSegment & operator=(CDAQMXSegment & cMXSegment);

};
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// AO/PWM : since R2.01
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
class DAQMX_API CDAQMXAOPWMData
{
    //-- ---- ---- ---- ---- ---- Constructs
public:
    CDAQMXAOPWMData(MXAOPWMData * pMXAOPWMData = NULL);
    virtual ~CDAQMXAOPWMData(void);

    //-- ---- ---- ---- ---- ---- Attributes
protected:
    MXAOPWMData m_MXAOPWMData;

    //-- ---- ---- ---- ---- ---- Overrides

    //-- ---- ---- ---- ---- ---- Implements
    // aopwmNo : AO/PWM number is same as channel (1 origin)
public:
    virtual void initialize(void);
    // copy structure
    void getMXAOPWMData(MXAOPWMData * pMXAOPWMData);
    void setMXAOPWMData(MXAOPWMData * pMXAOPWMData);
    static void initMXAOPWMData(MXAOPWMData * pMXAOPWMData);
    // get
    int getAOPWMValid(int aopwmNo);
    int getAOPWMValue(int aopwmNo);
    // set
    void setAOPWM(int aopwmNo,
                  int bValid,
                  int iAOPWMValue);
    // converter
    static int toAOPWMValue(double realValue,
                            int    iRangeAOPWM); //DAQMX_RANGE_AO_xxx, PWM_xxx
    static double toRealValue(int iAOPWMValue,
                              int iRangeAOPWM);
    // check
    virtual int isObject(const char * classname = "CDAQMXAOPWMData");

protected:
    // access structure directly
    MXAOPWM * getMXAOPWM(int aopwmNo);

    //-- ---- ---- ---- ---- ---- Operations
public:
    CDAQMXAOPWMData & operator=(CDAQMXAOPWMData & cMXAOPWMData);

};
class DAQMX_API CDAQMXTransmit
{
    //-- ---- ---- ---- ---- ---- Constructs
public:
    CDAQMXTransmit(MXTransmit * pMXTransmit = NULL);
    virtual ~CDAQMXTransmit(void);

    //-- ---- ---- ---- ---- ---- Attributes
protected:
    MXTransmit m_MXTransmit;

    //-- ---- ---- ---- ---- ---- Overrides

    //-- ---- ---- ---- ---- ---- Implements
    // aopwmNo : AO/PWM number is same as channel (1 origin)
public:
    virtual void initialize(void);
    // copy structure
    void getMXTransmit(MXTransmit * pMXTransmit);
    void setMXTransmit(MXTransmit * pMXTransmit);
    static void initMXTransmit(MXTransmit * pMXTransmit);
    // get
    int getTransmit(int aopwmNo);
    // set
    void setTransmit(int aopwmNo,
                     int iTransmit); //DAQMX_TRANSMIT_xxx
    // check
    virtual int isObject(const char * classname = "CDAQMXTransmit");

    //-- ---- ---- ---- ---- ---- Operations
public:
    CDAQMXTransmit & operator=(CDAQMXTransmit & cMXTransmit);

};
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// Handler
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
/**
 * This is a handler class for MX.
 */
class DAQMX_API CDAQMX : public CDAQHandler
{
    //-- ---- ---- ---- ---- ---- Constructs
public:
    CDAQMX(void);
    CDAQMX(const char * strAddress,
           unsigned int uiPort = DAQMX_COMMPORT,
           int *        errCode = NULL);
    virtual ~CDAQMX(void);

    //-- ---- ---- ---- ---- ---- Attributes
protected:
    int m_nNo;
    int m_nLastError;
    int m_bAutoFIFO; //boolean
    MXUserTime m_llUserTime;
    int m_nSessionNo;
    // channel on FIFO
    int m_chFIFONo[DAQMX_NUMCHANNEL];
    int m_chFIFOIndex[DAQMX_NUMCHANNEL];
    int m_chDataType[DAQMX_NUMCHANNEL];
    int m_chDeciPos[DAQMX_NUMCHANNEL];
    // last data number
    MXDataNo m_lastFIFODataNo[DAQMX_NUMFIFO];
    MXDataNo m_lastChDataNo[DAQMX_NUMCHANNEL];
    // channel 1 origin : start >= cur >= end
    int m_startChNo;
    int m_endChNo;
    int m_curChNo;
    // index 0 origin : start >= cur >= end
    int m_startFIFOIdx;
    int m_endFIFOIdx;
    int m_curFIFOIdx;
    // data 0 origin : start >= cur >= end
    MXDataNo m_startDataNo;
    MXDataNo m_endDataNo;
    MXDataNo m_curDataNo;
    // for talker
    int m_nFIFONo;
    int m_nDataNum;
    int m_nChNum; //channels in FIFO
    // since R2.01
    int m_nTimeNum;
    int m_packetVer;
    int m_nItemError;
    int m_bTalkConfig; //on talkConfig
    int m_bTalkChInfo; //on talkChInfo
    int m_bTalkData;   //on talkChData/talkFIFOData

    //-- ---- ---- ---- ---- ---- Overrides
    // chType : don't care, measured channel only
    // chNo : channel number is 1 origin
    // cDateTime : cast CDAQMXDateTime
    // cDataInfo : cast CDAQMXDataInfo
public:
    virtual int open(const char * strAddress,
                     unsigned int uiPort = DAQMX_COMMPORT);
    // acquisition
    virtual int getData(int            chType,
                        int            chNo,
                        CDAQDateTime & cDateTime,
                        CDAQDataInfo & cDataInfo);
    virtual int getChannel(int chType,
                           int chNo,
                           CDAQChInfo & cChInfo);
    // since R2.01
    virtual int isObject(const char * classname = "CDAQMX");

    //-- ---- ---- ---- ---- ---- Implements
    // chNo : channel number is 1 origin
    // fifoNo : FIFO number is 0 origin
    // pFlag : DAQMX_FLAG_xxx
public:
    // FIFO
    int startFIFO(void);
    int stopFIFO(void);
    int autoFIFO(int bAuto);
    // date time : NULL is Now
    virtual int setDateTime(CDAQMXDateTime * pcMXDateTime = NULL);
    // control commands
    int setBackup(int bBackup);
    virtual int formatCF(void);
    int initSystem(int iCtrl); //DAQMX_SYSTEM_xxx
    int setSegment(int             dispType,
                   int             dispTime,
                   CDAQMXSegment & cNewMXSegment,
                   CDAQMXSegment & cOldMXSegment);
    // status
    int getStatusData(CDAQMXStatus & cMXStatus);
    // system
    int getSystemConfig(CDAQMXSysInfo & cMXSysInfo);
    // configure : set range @see CDAQMXConfig
    int getConfig(CDAQMXConfig & cMXConfig);
    int setConfig(CDAQMXConfig & cMXConfig);
    int getMXConfig(CDAQMXConfig & cMXConfig); //setup data only
    int setMXConfig(CDAQMXConfig & cMXConfig);
    // configure for VB by talker
    int talkConfig(CDAQMXSysInfo & cMXSysInfo,
                   CDAQMXStatus &  cMXStatus,
                   CDAQMXNetInfo & cMXNetInfo);
    int getChConfig(CDAQMXChConfig & cMXChConfig,
                    int *            pFlag = NULL);
    // DO
    int getDOData(CDAQMXDOData & cMXDoData);
    int setDOData(CDAQMXDOData & cMXDoData);
    // channel inforamtion
    int talkChInfo(int startChNo = 1,
                   int endChNo   = DAQMX_NUMCHANNEL);
    int getChInfo(CDAQMXChInfo & cMXChInfo,
                  int *          pFlag = NULL);
    // measured data by channel
    int getChDataNo(int        chNo,
                    MXDataNo * startDataNo,
                    MXDataNo * endDataNo);
    int talkChData(int      chNo,
                   MXDataNo startDataNo = DAQMX_INSTANTANEOUS,
                   MXDataNo endDataNo   = DAQMX_INSTANTANEOUS);
    // measured data by FIFO
    int getFIFODataNo(int        fifoNo,
                      MXDataNo * startDataNo,
                      MXDataNo * endDataNo);
    int talkFIFOData(int      fifoNo,
                     MXDataNo startDataNo = DAQMX_INSTANTANEOUS,
                     MXDataNo endDataNo   = DAQMX_INSTANTANEOUS);
    // get measured data
    int getTimeData(MXDataNo *       dataNo,
                    CDAQMXDateTime & cMXDateTime,
                    MXUserTime *     pUserTime = NULL,
                    int *            pFlag = NULL);
    int getChData(MXDataNo *       dataNo,
                  CDAQMXDataInfo & cMXDataInfo,
                  int *            pFlag = NULL);
    // Misc
    void setUserTime(MXUserTime userTime);
    MXUserTime getUserTime(void);
    // MX error code
    int getLastError(void);
    //-- ---- ---- ---- ---- ---- since R2.01
    int getItemError(void);
    // AO/PWM
    int getAOPWMData(CDAQMXAOPWMData & cMXAOPWMData,
                     CDAQMXTransmit & cMXTransmit);
    int setAOPWMData(CDAQMXAOPWMData & cMXAOPWMData);
    int setTransmit(CDAQMXTransmit & cMXTransmit);
    int getOutput(CDAQMXOutputData & cMXOutputData);
    int setOutput(CDAQMXOutputData & cMXOutputData);
    // Balance
    int getBalance(CDAQMXBalanceData & cMXBalanceData);
    int setBalance(CDAQMXBalanceData & cMXBalanceData);
    int runBalance(CDAQMXBalanceResult & cMXBalanceResult);
    int resetBalance(CDAQMXBalanceResult & cMXBalanceResult);

protected:
    // send and receive
    virtual int runCommand(unsigned char * reqBuf,
                           int             lenReq,
                           unsigned char * ackBuf,
                           int             lenAck);
    // with decode/encode
    virtual int sendPacket(unsigned char * reqBuf,
                           int             lenReq);
    virtual int receivePacket(unsigned char * ackBuf,
                              int             lenAck,
                              int *           realLen);
    // get block with calculate remain size, without decode
    int receiveBlock(unsigned char * pBlock,
                     int             lenBlock);
    // special commands
    int nop(void);
    int registry(void);
    // incremental number
    int getNo(void); //m_nNo
    MXDataNo incCurDataNo(void);
    int incCurFIFOIdx(void);
    // get number
    int getDataNo(int        fifoNo,
                  MXDataNo   prevLast,
                  MXDataNo * startDataNo,
                  MXDataNo * endDataNo);
    int searchChNo(int fifoNo,
                   int fifoIndex);
    // clear attributes
    void clearAttr(void);
    void clearData(int sessionNo = -1); //when FIFO start
    // DLL version
    static const int getVersionDLL(void);
    static const int getRevisionDLL(void);
    // since R2.01
    int getPacketVersion(void);
    void clearLastDataNoCh(int chNo = DAQMX_CHNO_ALL);
    void clearLastDataNoFIFO(int fifoNo = DAQMX_FIFONO_ALL);
    // send and receive without decode/encode and no check
    virtual int runPacket(unsigned char * reqBuf,
                          int             lenReq,
                          unsigned char * ackBuf,
                          int             lenAck);
    // get buffer with size field, without decode
    int receiveBuffer(unsigned char * pBuf,
                      int             lenBuf,
                      int *           realLen,
                      int *           sizeBuf = NULL);

    //-- ---- ---- ---- ---- ---- Operations

};
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
#endif //__cplusplus
///////////////////////////////////////////////////////////////////////
#ifdef __cplusplus
extern "C" {
#endif
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// Low level communications
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
/**
 * This opens the instrument with initialize.
 * This returns NULL descriptor, if error occured.
 * @param strAddress specified an address as string.
 * @param errCode stored an error code if error occured.
 * @return an instrument descriptor.
 */
DAQMX_API DAQMX APIENTRY openMX(const char * strAddress,
                                int *        errCode);
#define OPEN_MX(address) openMX(address, NULL)
/**
 * This closes the instrument.
 * @param daqmx specified an instrument descriptor.
 * @return an error code that is not zero if error ocuured.
 */
DAQMX_API int APIENTRY closeMX(DAQMX daqmx);
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// Middle level communications
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// FIFO control commands
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
/**
 * This starts FIFO.
 * @param daqmx specified an instrument descriptor.
 * @return an error code that is not zero if error ocuured.
 */
DAQMX_API int APIENTRY startFIFOMX(DAQMX daqmx);
/**
 * This stops FIFO.
 * @param daqmx specified an instrument descriptor.
 * @return an error code that is not zero if error ocuured.
 */
DAQMX_API int APIENTRY stopFIFOMX(DAQMX daqmx);
/**
 * This sets automatic starting FIFO.
 * @param daqmx specified an instrument descriptor.
 * @param bAuto specified an automatic mode as boolean.
 * @return an error code that is not zero if error ocuured.
 */
DAQMX_API int APIENTRY autoFIFOMX(DAQMX daqmx,
                                  int   bAuto);
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// Date time commands
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
/**
 * This sets date and time.
 * @param daqmx specified an instrument descriptor.
 * @param pMXDateTime specified a structure of date and tiem. If NULL, now.
 * @return an error code that is not zero if error ocuured.
 */
DAQMX_API int APIENTRY setDateTimeMX(DAQMX        daqmx,
                                     MXDateTime * pMXDateTime);
/**
 * This sets date and time, now.
 * @param daqmx specified an instrument descriptor.
 * @return an error code that is not zero if error ocuured.
 * @see setDateTimeMX
 */
DAQMX_API int APIENTRY setDateTimeNowMX(DAQMX daqmx);
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// Control Commands
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
/**
 * This sets a backup mode.
 * @param daqmx specified an instrument descriptor.
 * @param bBackup specified a backup mode as boolean.
 * @param iCDWriteMode specified a mode of writing CF as DAQMX_CFWRITEMODE_xxx
 * @return an error code that is not zero if error ocuured.
 */
DAQMX_API int APIENTRY setBackupMX(DAQMX daqmx,
                                   int   bBackup,
                                   int   iCFWriteMode);
/**
 * This formats a CF card.
 * @param daqmx specified an instrument descriptor.
 * @return an error code that is not zero if error ocuured.
 */
DAQMX_API int APIENTRY formatCFMX(DAQMX daqmx);
/**
 * This initializes a system.
 * @param daqmx specified an instrument descriptor.
 * @param iCtrl specified a selection of control as DAQMX_SYSTEM_xxx.
 * @return an error code that is not zero if error ocuured.
 */
DAQMX_API int APIENTRY initSystemMX(DAQMX daqmx,
                                    int   iCtrl);
/**
 * This sets a segment.
 * @param daqmx specified an instrument descriptor.
 * @param iDispType specified a dipplay type as DAQMX_DISPTYPE_xxx.
 * @param dispTime specified a display time. Maximaum is DAQMX_MAXDISPTIME.
 * @param newSegment specified display patterns for segments.
 * @param oldSegment stored display patterns before.
 * @return an error code that is not zero if error ocuured.
 */
DAQMX_API int APIENTRY setSegmentMX(DAQMX        daqmx,
                                    int          iDispType,
                                    int          dispTime,
                                    MXSegment *  newSegment,
                                    MXSegment *  oldSegment);
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// Get status
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
/**
 * This gets a status data.
 * @param daqmx specified an instrument descriptor.
 * @param pMXStatus stored a status data.
 * @return an error code that is not zero if error ocuured.
 */
DAQMX_API int APIENTRY getStatusDataMX(DAQMX      daqmx,
                                       MXStatus * pMXStatus);
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// Get system
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
/**
 * This gets a system configuration data.
 * @param daqmx specified an instrument descriptor.
 * @param pMXSystemInfo stored a system configuration data.
 * @return an error code that is not zero if error ocuured.
 */
DAQMX_API int APIENTRY getSystemConfigMX(DAQMX          daqmx,
                                         MXSystemInfo * pMXSystemInfo);
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// Configurature
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
/**
 * This gets a configuration data.
 * @param daqmx specified an instrument descriptor.
 * @param pMXConfigData stored a configuration data.
 * @return an error code that is not zero if error ocuured.
 */
DAQMX_API int APIENTRY getConfigDataMX(DAQMX          daqmx,
                                       MXConfigData * pMXConfigData);
/**
 * This sets a configuration data.
 * @param daqmx specified an instrument descriptor.
 * @param pMXConfigData specified a configuration data.
 * @return an error code that is not zero if error ocuured.
 */
DAQMX_API int APIENTRY setConfigDataMX(DAQMX          daqmx,
                                       MXConfigData * pMXConfigData);
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// DO
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
/**
 * This gets a DO data packet.
 * @param daqmx specified an instrument descriptor.
 * @param pMXDOData stored a DO data.
 * @return a error code that is not zero if error ocuured.
 */
DAQMX_API int APIENTRY getDODataMX(DAQMX      daqmx,
                                   MXDOData * pMXDOData);
/**
 * This sets a DO data packet.
 * @param daqmx specified an instrument descriptor.
 * @param pMXDOData specified a DO data.
 * @return an error code that is not zero if error ocuured.
 */
DAQMX_API int APIENTRY setDODataMX(DAQMX      daqmx,
                                   MXDOData * pMXDOData);
/**
 * This changes a DO in the DO data packet.
 * @param pMXDOData changed a DO data that stored by getDODataMX.
 * @param doNo specified a DO number as channel number.
 * @param bValid specified a effective DO as boolean.
 * @param bONOFF specified a value of ON/OFF as boolean
 * @return an error code that is not zero if error ocuured.
 */
DAQMX_API int APIENTRY changeDODataMX(MXDOData * pMXDOData,
                                      int        doNo,
                                      int        bValid,
                                      int        bONOFF);
/**
 * This sets a DO type on configure.
 * @param daqmx specified an instrument descriptor.
 * @param doNo specified a DO number as channel number.
 * @param iKind specified a DO channel kind as DAQMX_CHKIND_DOxxx
 * @param bDeenergize specified a deenergize as boolean.
 * @param bHold specified a hold as boolean.
 * @return an error code that is not zero if error ocuured.
 */
DAQMX_API int APIENTRY setDOTypeMX(DAQMX daqmx,
                                   int   doNo,
                                   int   iKind,
                                   int   bDeenergize,
                                   int   bHold);
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// Get channel information
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
/**
 * This sends a talker of getting channel informations.
 * @param daqmx specified an instrument descriptor.
 * @param startChNo specified a start channel number.
 * @param endChNo specified an end channel number.
 * @return an error code that is not zero if error ocuured.
 */
DAQMX_API int APIENTRY talkChInfoMX(DAQMX daqmx,
                                    int   startChNo,
                                    int   endChNo);
/**
 * This gets a channel information by each channel.
 * @param daqmx specified an instrument descriptor.
 * @param pMXChInfo stored a channel information.
 * @param pFlag stored a flag as DAQMX_FLAG_xxx.
 * @return an error code that is not zero if error ocuured.
 */
DAQMX_API int APIENTRY getChInfoMX(DAQMX      daqmx,
                                   MXChInfo * pMXChInfo,
                                   int *      pFlag);
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// Talker measured data by each channels
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
/**
 * This gets data numbers by each channel.
 * A start data number is the next of the data number at last.
 * @param daqmx specified an instrument descriptor.
 * @param chNo specified a channel number.
 * @param startDataNo stored a start data number.
 * @param endDataNo stored an end data number.
 * @return an error code that is not zero if error ocuured.
 */
DAQMX_API int APIENTRY getChDataNoMX(DAQMX      daqmx,
                                     int        chNo,
                                     MXDataNo * startDataNo,
                                     MXDataNo * endDataNo);
/**
 * This sends a talker of getting measured data by each channel.
 * @param daqmx specified an instrument descriptor.
 * @param chNo specified a channel number.
 * @param startDataNo specified a start data number.
 * @param endDataNo specified an end data number.
 * @return an error code that is not zero if error ocuured.
 */
DAQMX_API int APIENTRY talkChDataMX(DAQMX    daqmx,
                                    int      chNo,
                                    MXDataNo startDataNo,
                                    MXDataNo endDataNo);
/**
 * This sends a talker of getting instantenous measured data by each channel.
 * @param daqmx specified an instrument descriptor.
 * @param chNo specified a channel number.
 * @return an error code that is not zero if error ocuured.
 * @see talkChDataMX
 */
DAQMX_API int APIENTRY talkChDataInstMX(DAQMX daqmx,
                                        int   chNo);
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// Talker measured data by each FIFO
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
/**
 * This gets data numbers by each FIFO.
 * A start data number is the next of the data number at last.
 * @param daqmx specified an instrument descriptor.
 * @param fifoNo specified a FIFO number.
 * @param startDataNo stored a start data number.
 * @param endDataNo stored an end data number.
 * @return an error code that is not zero if error ocuured.
 */
DAQMX_API int APIENTRY getFIFODataNoMX(DAQMX      daqmx,
                                       int        fifoNo,
                                       MXDataNo * startDataNo,
                                       MXDataNo * endDataNo);
/**
 * This sends a talker of getting measured data by each FIFO.
 * @param daqmx specified an instrument descriptor.
 * @param fifoNo specified a FIFO number.
 * @param startDataNo specified a start data number.
 * @param endDataNo specified an end data number.
 * @return an error code that is not zero if error ocuured.
 */
DAQMX_API int APIENTRY talkFIFODataMX(DAQMX    daqmx,
                                      int      fifoNo,
                                      MXDataNo startDataNo,
                                      MXDataNo endDataNo);
/**
 * This sends a talker of getting instantenous measured data by each FIFO.
 * @param daqmx specified an instrument descriptor.
 * @param fifoNo specified a FIFO number.
 * @return an error code that is not zero if error ocuured.
 * @see talkFIFODataMX
 */
DAQMX_API int APIENTRY talkFIFODataInstMX(DAQMX daqmx,
                                          int   fifoNo);
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// Get measured data after talker
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
/**
 * This gets date and time at the data number by each data.
 * @param daqmx specified an instrument descriptor.
 * @param pMXDataNo stored a data number.
 * @param pMXDateTime stored date and time.
 * @param pMXUserTime stored a user time.
 * @param pFlag stored a flag as DAQMX_FLAG_xxx.
 * @return an error code that is not zero if error ocuured.
 */
DAQMX_API int APIENTRY getTimeDataMX(DAQMX        daqmx,
                                     MXDataNo *   pMXDataNo,
                                     MXDateTime * pMXDateTime,
                                     MXUserTime * pMXUserTime,
                                     int *        pFlag);
/**
 * This gets a measured data by each data.
 * @param daqmx specified an instrument descriptor.
 * @param pMXDataNo stored a data number.
 * @param pMXChInfo stored a channel information.
 * @param pMXDataInfo stored a measured data.
 * @param pFlag stored a flag as DAQMX_FLAG_xxx.
 * @return an error code that is not zero if error ocuured.
 */
DAQMX_API int APIENTRY getChDataMX(DAQMX        daqmx,
                                   MXDataNo *   pMXDataNo,
                                   MXChInfo *   pMXChInfo,
                                   MXDataInfo * pMXDataInfo,
                                   int *        pFlag);
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// Misc
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
/**
 * This sets a user time.
 * @param daqmx specified an instrument descriptor.
 * @param userTime specified a counter that defined by user.
 * @return a error code that is not zero if error ocuured.
 */
DAQMX_API int APIENTRY setUserTimeMX(DAQMX      daqmx,
                                     MXUserTime userTime);
/**
 * This gets an error code that received from the instrument at last.
 * @param daqmx specified an instrument descriptor.
 * @param lastErr stored an error code that is received at last.
 * @return an error code that is not zero if error ocuured.
 */
DAQMX_API int APIENTRY getLastErrorMX(DAQMX daqmx,
                                      int * lastErr);
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// Set range
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
/**
 * This sets the range to skip.
 * @param daqmx specified an instrument descriptor.
 * @param startChNo specified a start channel number.
 * @param endChNo specified an end channel number.
 * @return an error code that is not zero if error ocuured.
 */
DAQMX_API int APIENTRY setSKIPMX(DAQMX daqmx,
                                 int   startChNo,
                                 int   endChNo);
/**
 * This sets voltage.
 * If min equals max, span and scale are omitted.
 * @param daqmx specified an instrument descriptor.
 * @param iRangeVOLT specified a selection of the range as DAQMX_RANGE_VOLT_xxx.
 * @param startChNo specified a start channel number.
 * @param endChNo specified an end channel number.
 * @param spanMin specified a value of the minimum span.
 * @param spanMax specified a value of the maximum span.
 * @param scaleMin specified a value of the minimum scale.
 * @param scaleMax specified a value of the maximum scale.
 * @param scalePoint specified a value of the decimal point positon.
 * @return an error code that is not zero if error ocuured.
 */
DAQMX_API int APIENTRY setVOLTMX(DAQMX daqmx,
                                 int   iRangeVOLT,
                                 int   startChNo,
                                 int   endChNo,
                                 int   spanMin,
                                 int   spanMax,
                                 int   scaleMin,
                                 int   scaleMax,
                                 int   scalePoint);
/**
 * This sets thermocouple.
 * If min equals max, span and scale are omitted.
 * @param daqmx specified an instrument descriptor.
 * @param iRangeTC specified a selection of the range as DAQMX_RANGE_TC_xxx.
 * @param startChNo specified a start channel number.
 * @param endChNo specified an end channel number.
 * @param spanMin specified a value of the minimum span.
 * @param spanMax specified a value of the maximum span.
 * @param scaleMin specified a value of the minimum scale.
 * @param scaleMax specified a value of the maximum scale.
 * @param scalePoint specified a value of the decimal point positon.
 * @return an error code that is not zero if error ocuured.
 */
DAQMX_API int APIENTRY setTCMX(DAQMX daqmx,
                               int   iRangeTC,
                               int   startChNo,
                               int   endChNo,
                               int   spanMin,
                               int   spanMax,
                               int   scaleMin,
                               int   scaleMax,
                               int   scalePoint);
/**
 * This sets RTD (resistance Temperature Detector).
 * If min equals max, span and scale are omitted.
 * @param daqmx specified an instrument descriptor.
 * @param iRangeRTD specified a selection of the range as DAQMX_RANGE_RTD_xxx.
 * @param startChNo specified a start channel number.
 * @param endChNo specified an end channel number.
 * @param spanMin specified a value of the minimum span.
 * @param spanMax specified a value of the maximum span.
 * @param scaleMin specified a value of the minimum scale.
 * @param scaleMax specified a value of the maximum scale.
 * @param scalePoint specified a value of the decimal point positon.
 * @return an error code that is not zero if error ocuured.
 */
DAQMX_API int APIENTRY setRTDMX(DAQMX daqmx,
                                int   iRangeRTD,
                                int   startChNo,
                                int   endChNo,
                                int   spanMin,
                                int   spanMax,
                                int   scaleMin,
                                int   scaleMax,
                                int   scalePoint);
/**
 * This sets DI (contact input).
 * If min equals max, span and scale are omitted.
 * @param daqmx specified an instrument descriptor.
 * @param iRangeDI specified a selection of the range as DAQMX_RANGE_DI_xxx.
 * @param startChNo specified a start channel number.
 * @param endChNo specified an end channel number.
 * @param spanMin specified a value of the minimum span.
 * @param spanMax specified a value of the maximum span.
 * @param scaleMin specified a value of the minimum scale.
 * @param scaleMax specified a value of the maximum scale.
 * @param scalePoint specified a value of the decimal point positon.
 * @return an error code that is not zero if error ocuured.
 */
DAQMX_API int APIENTRY setDIMX(DAQMX daqmx,
                               int   iRangeDI,
                               int   startChNo,
                               int   endChNo,
                               int   spanMin,
                               int   spanMax,
                               int   scaleMin,
                               int   scaleMax,
                               int   scalePoint);
/**
 * This sets DELTA (difference between channels).
 * If min equals max, span and scale are omitted.
 * @param daqmx specified an instrument descriptor.
 * @param refChNo specified a reference channel number.
 * @param startChNo specified a start channel number.
 * @param endChNo specified an end channel number.
 * @param spanMin specified a value of the minimum span.
 * @param spanMax specified a value of the maximum span.
 * @param scaleMin specified a value of the minimum scale.
 * @param scaleMax specified a value of the maximum scale.
 * @param scalePoint specified a value of the decimal point positon.
 * @param iRange specified a selection of the range as DAQMX_RANGE_xxx
 * @return an error code that is not zero if error ocuured.
 */
DAQMX_API int APIENTRY setDELTAMX(DAQMX daqmx,
                                  int   refChNo,
                                  int   startChNo,
                                  int   endChNo,
                                  int   spanMin,
                                  int   spanMax,
                                  int   scaleMin,
                                  int   scaleMax,
                                  int   scalePoint,
                                  int   iRange);
/**
 * This sets RRJC (remote RJC).
 * If min equals max, span and scale are omitted.
 * @param daqmx specified an instrument descriptor.
 * @param refChNo specified a reference channel number.
 * @param startChNo specified a start channel number.
 * @param endChNo specified an end channel number.
 * @param spanMin specified a value of the minimum span.
 * @param spanMax specified a value of the maximum span.
 * @return an error code that is not zero if error ocuured.
 */
DAQMX_API int APIENTRY setRRJCMX(DAQMX daqmx,
                                 int   refChNo,
                                 int   startChNo,
                                 int   endChNo,
                                 int   spanMin,
                                 int   spanMax);
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// Scalling
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
/**
 * This sets an unit name.
 * @param daqmx specified an instrument descriptor.
 * @param strUnit specified a string of the unit name.
 * @param startChNo specified a start channel number.
 * @param endChNo specified an end channel number.
 * @return an error code that is not zero if error ocuured.
 */
DAQMX_API int APIENTRY setScallingUnitMX(DAQMX        daqmx,
                                         const char * strUnit,
                                         int          startChNo,
                                         int          endChNo);
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// Alarm
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
/**
 * This sets the alarm.
 * @param daqmx specified an instrument descriptor.
 * @param levelNo specified a level number.
 * @param startChNo specified a start channel number.
 * @param endChNo specified an end channel number.
 * @param iAlarmType specified a selection of the alarm type as DAQMX_ALARM_xxx.
 * @param value specified a value of an alarm ON without decimal point position.
 * @param histerisys specified an offset of the value.
 * @return an error code that is not zero if error ocuured.
 */
DAQMX_API int APIENTRY setAlarmMX(DAQMX daqmx,
                                  int   levelNo,
                                  int   startChNo,
                                  int   endChNo,
                                  int   iAlarmType,
                                  int   value,
                                  int   histerisys);
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// Channel configure
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
/**
 * This sets a tag name.
 * @param daqmx specified an instrument descriptor.
 * @param strTag specified a string of the tag name.
 * @param startChNo specified a start channel number.
 * @param endChNo specified an end channel number.
 * @return an error code that is not zero if error ocuured.
 */
DAQMX_API int APIENTRY setTagMX(DAQMX        daqmx,
                                const char * strTag,
                                int          startChNo,
                                int          endChNo);
/**
 * This sets a comment.
 * @param daqmx specified an instrument descriptor.
 * @param strComment specified a string of the comment.
 * @param startChNo specified a start channel number.
 * @param endChNo specified an end channel number.
 * @return an error code that is not zero if error ocuured.
 */
DAQMX_API int APIENTRY setCommentMX(DAQMX        daqmx,
                                    const char * strComment,
                                    int          startChNo,
                                    int          endChNo);
/**
 * This sets a RJC on TC channels.
 * @param daqmx specified an instrument descriptor.
 * @param iRJCType specified a selection of the RJC type as DAQMX_RJC_xxx.
 * @param startChNo specified a start channel number.
 * @param endChNo specified an end channel number.
 * @param volt specified a value of the voltage on external RJC.
 * @return an error code that is not zero if error ocuured.
 */
DAQMX_API int APIENTRY setRJCTypeMX(DAQMX daqmx,
                                    int   iRJCType,
                                    int   startChNo,
                                    int   endChNo,
                                    int   volt);
/**
 * This sets a filter.
 * @param daqmx specified an instrument descriptor.
 * @param iFilter specified a selection of the filter as DAQMX_FILTER_xxx.
 * @param startChNo specified a start channel number.
 * @param endChNo specified an end channel number.
 * @return an error code that is not zero if error ocuured.
 */
DAQMX_API int APIENTRY setFilterMX(DAQMX daqmx,
                                   int   iFilter,
                                   int   startChNo,
                                   int   endChNo);
/**
 * This sets a burnout.
 * @param daqmx specified an instrument descriptor.
 * @param iBurnout specified a selection of the burnout as DAQMX_BURNOUT_xxx.
 * @param startChNo specified a start channel number.
 * @param endChNo specified an end channel number.
 * @return an error code that is not zero if error ocuured.
 */
DAQMX_API int APIENTRY setBurnoutMX(DAQMX daqmx,
                                    int   iBurnout,
                                    int   startChNo,
                                    int   endChNo);
/**
 * This sets a reference alarm on DO.
 * @param daqmx specified an instrument descriptor.
 * @param refChNo specified a reference channel number. If all, DAQMX_REFCHNO_ALL.
 * @param startChNo specified a start channel number.
 * @param endChNo specified an end channel number.
 * @param levelNo specified a level number. If all, DAQMX_LEVELNO_ALL.
 * @param bValid specified validation as boolean.
 * @return an error code that is not zero if error ocuured.
 */
DAQMX_API int APIENTRY setRefAlarmMX(DAQMX daqmx,
                                     int   refChNo,
                                     int   startChNo,
                                     int   endChNo,
                                     int   levelNo,
                                     int   bValid);
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// Unit Configure
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
/**
 * This sets an interval for module.
 * @param daqmx specified an instrument descriptor.
 * @param moduleNo specified a module number.
 * @param iInterval specified an interval as DAQMX_INTERVAL_xxx.
 * @param iHz specified an integral time as DAQMX_INTEGRAL_xxx.
 * @return an error code that is not zero if error ocuured.
 */
DAQMX_API int APIENTRY setIntervalMX(DAQMX daqmx,
                                     int   moduleNo,
                                     int   iInterval,
                                     int   iHz);
/**
 * This sets a temperature unit.
 * @param daqmx specified an instrument descriptor.
 * @param iTempUnit specified a selection of the temperature unit as DAQMX_TEMPUNIT_xxx.
 * @return an error code that is not zero if error ocuured.
 */
DAQMX_API int APIENTRY setTempUnitMX(DAQMX daqmx,
                                     int   iTempUnit);
/**
 * This sets an unit number defined by the user.
 * @param daqmx specified an instrument descriptor.
 * @param unitNo specified an unit number as integer.
 * @return an error code that is not zero if error ocuured.
 */
DAQMX_API int APIENTRY setUnitNoMX(DAQMX daqmx,
                                   int   unitNo);
/**
 * This sets a time until it begins preservation on a CF card. 
 * @param daqmx specified an instrument descriptor.
 * @param timeout a time as millisecond.
 * @return an error code that is not zero if error ocuured.
 */
DAQMX_API int APIENTRY setSystemTimeoutMX(DAQMX daqmx,
                                          int   timeout);
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// Utilities
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
/**
 * This changes a measured data and decimal point position to a value as double.
 * @param dataValue specified a measured data as integer (Long).
 * @param point specified a decimal point position.
 * @return a value as double.
 */
DAQMX_API double APIENTRY toDoubleValueMX(int dataValue,
                                          int point);
/**
 * This changes a measured data and decimal point position to a value as string.
 * @param dataValue specified a measured data as integer (Long).
 * @param point specified a decimal point position.
 * @param strValue stored a string.
 * @param lenValue specified a length of the buffer (strValue).
 * @return a length of the string by bytes without NULL.
 */
DAQMX_API int APIENTRY toStringValueMX(int    dataValue,
                                       int    point,
                                       char * strValue,
                                       int    lenValue);
/**
 * This changes an alarm type to a mnemonic as string.
 * @param iAlarmType specified a selection of the alarm type as DAQMX_ALARM_xxx.
 * @param strAlarm stored a string.
 * @param lenAlarm specified a length of the buffer (strAlarm).
 * @return a length of the string by bytes without NULL.
 */
DAQMX_API int APIENTRY toAlarmNameMX(int    iAlarmType,
                                     char * strAlarm,
                                     int    lenAlarm);
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// Messages
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
/**
 * This gets a version of API.
 * @return a number as integer.
 */
DAQMX_API const int APIENTRY getVersionAPIMX(void);
/**
 * This gets a revision of API.
 * @return a number as integer.
 */
DAQMX_API const int APIENTRY getRevisionAPIMX(void);
/**
 * This changes an error code to a string.
 * @param errCode specified an error code that returned by each function.
 * @param errStr stored a string of the error message.
 * @param errLen specified a length of the buffer (errStr).
 * @return a length of the error message by bytes without NULL.
 */
DAQMX_API int APIENTRY toErrorMessageMX(int    errCode,
                                        char * errStr,
                                        int    errLen);
/**
 * This gets a maximum length of error messages.
 * @return a length by bytes without NULL.
 */
DAQMX_API int APIENTRY getMaxLenErrorMessageMX(void);
/**
 * This gets a string of the error code.
 * If Visual Basic, Use toErrorMessageMX.
 * @param errCode specified an error code that returned by each function.
 * @return a string of the error message.
 */
DAQMX_API const char * APIENTRY getErrorMessageMX(int errCode);
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// Deprecated command
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
/**
 * This sets timeout.
 * If seconds is minus, timeout off.
 * @param daqmx specified an instrument descriptor.
 * @param seconds specified a value of time out by second.
 * @return an error code that is not zero if error ocuured.
 * @deprecated since Ver.1 Rev.0
 */
DAQMX_API int APIENTRY setTimeOutMX(DAQMX daqmx,
                                    int   seconds);
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// for Visual Basic
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
/**
 * This sends talker of getting a configuration data.
 * @param daqmx specified an instrument descriptor.
 * @param pMXSystemInfo stored a system configuration data.
 * @param pMXStatus stored a status data.
 * @param pMXNetInfo stored a network infomation data.
 * @return an error code that is not zero if error ocuured.
 */
DAQMX_API int APIENTRY talkConfigMX(DAQMX          daqmx,
                                    MXSystemInfo * pMXSystemInfo,
                                    MXStatus *     pMXStatus,
                                    MXNetInfo *    pMXNetInfo);
/**
 * This gets a channel configuration data by each channel.
 * This is called after talkConfigMX.
 * @param daqmx specified an instrument descriptor.
 * @param pMXChConfig stored a channel configuration data.
 * @param pFlag stored a flag as DAQMX_FLAG_xxx
 * @return an error code that is not zero if error ocuured.
 */
DAQMX_API int APIENTRY getChConfigMX(DAQMX        daqmx,
                                     MXChConfig * pMXChConfig,
                                     int *        pFlag);
/**
 * This sets a system configuration data in a configuration data.
 * @param daqmx specified an instrument descriptor.
 * @param pMXSystemInfo specified a system configuration data.
 * @return an error code that is not zero if error ocuured.
 */
DAQMX_API int APIENTRY setSystemConfigMX(DAQMX          daqmx,
                                         MXSystemInfo * pMXSystemInfo);
/**
 * This sets a channel configuration data in a configuration data by each channels.
 * @param daqmx a descriptor of the instrument.
 * @param pMXChConfig specified a channel configuration data.
 * @return an error code that is not zero if error ocuured.
 */
DAQMX_API int APIENTRY setChConfigMX(DAQMX        daqmx,
                                     MXChConfig * pMXChConfig);
/**
 * This sends talker of getting measured data by each channels.
 * @param daqmx specified an instrument descriptor.
 * @param chNo specified a channel number.
 * @param startDataNo specified a start data number.
 * @param endDataNo specified an end data number.
 * @return an error code that is not zero if error ocuured.
 * @see talkChDataMX
 */
DAQMX_API int APIENTRY talkChDataVBMX(DAQMX      daqmx,
                                      int        chNo,
                                      MXDataNo * startDataNo,
                                      MXDataNo * endDataNo);
/**
 * This sends talker of getting measured data by each FIFO.
 * @param daqmx specified an instrument descriptor.
 * @param fifoNo specified a FIFO number.
 * @param startDataNo specified a start data number.
 * @param endDataNo specified an end data number.
 * @return an error code that is not zero if error ocuured.
 * @see talkFIFODataMX
 */
DAQMX_API int APIENTRY talkFIFODataVBMX(DAQMX      daqmx,
                                        int        fifoNo,
                                        MXDataNo * startDataNo,
                                        MXDataNo * endDataNo);
/**
 * This sets a user time.
 * @param daqmx specified an instrument descriptor.
 * @param userTime specified a counter that defined by user.
 * @return an error code that is not zero if error ocuured.
 * @see setUserTimeMX
 */
DAQMX_API int APIENTRY setUserTimeVBMX(DAQMX        daqmx,
                                       MXUserTime * userTime);
/**
 * This increments a data number.
 * @param dataNo specified a data number.
 * @param increment a incremental value as integer.
 */
DAQMX_API void APIENTRY incrementDataNoMX(MXDataNo * dataNo,
                                          int        increment);
/**
 * This decrements a data number.
 * @param dataNo specified a data number.
 * @param increment a decremental value as integer.
 */
DAQMX_API void APIENTRY decrementDataNoMX(MXDataNo * dataNo,
                                          int        decrement);
/**
 * This compares two data numbers.
 * @param prevMXDataNo specified a data number.
 * @param nextMXDataNo specified a another data number.
 * @return If equals, 0. If prev less than next, positive. Others, negative.
 */
DAQMX_API int APIENTRY compareDataNoMX(MXDataNo * prevMXDataNo,
                                       MXDataNo * nextMXDataNo);
/**
 * This converts time.
 * @param pMXDateTime specified date and time from getTimeDataMX.
 * @param pYear stored a year of 4 figures as integer.
 * @param pMonth stored a month (1 - 12).
 * @param pDay stored a day (1 - 31).
 * @param pHour stored a hour (0 - 23).
 * @param pMinute stored a minute (0 - 59).
 * @param pSecond stored a second (0 - 59).
 */
DAQMX_API void APIENTRY toDateTimeMX(MXDateTime * pMXDateTime,
                                     int *        pYear,
                                     int *        pMonth,
                                     int *        pDay,
                                     int *        pHour,
                                     int *        pMinute,
                                     int *        pSecond);
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// since R2.01
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
/**
 * This sets a AO/PWM data packet.
 * @param daqmx specified an instrument descriptor.
 * @param pMXAOPWMData specified a AO/PWM data.
 * @return an error code that is not zero if error ocuured.
 * @since R2.01
 */
DAQMX_API int APIENTRY setAOPWMDataMX(DAQMX         daqmx,
                                      MXAOPWMData * pMXAOPWMData);
/**
 * This sets a transmit data packet.
 * @param daqmx specified an instrument descriptor.
 * @param pMXTransmit specified a transmit data.
 * @return an error code that is not zero if error ocuured.
 * @since R2.01
 */
DAQMX_API int APIENTRY setTransmitMX(DAQMX        daqmx,
                                     MXTransmit * pMXTransmit);
/**
 * This runs the initial balance.
 * @param daqmx specified an instrument descriptor.
 * @param pMXBalanceData specified validation, stored values.
 * @param pMXBalanceResult stored results.
 * @return an error code that is not zero if error ocuured.
 * @since R2.01
 */
DAQMX_API int APIENTRY runBalanceMX(DAQMX             daqmx,
                                    MXBalanceData *   pMXBalanceData,
                                    MXBalanceResult * pMXBalanceResult);
/**
 * This resets the initial balance.
 * @param daqmx specified an instrument descriptor.
 * @param pMXBalanceData specified validation, stored values.
 * @param pMXBalanceResult stored results.
 * @return an error code that is not zero if error ocuured.
 * @since R2.01
 */
DAQMX_API int APIENTRY resetBalanceMX(DAQMX             daqmx,
                                      MXBalanceData *   pMXBalanceData,
                                      MXBalanceResult * pMXBalanceResult);
/**
 * This sets an initial balance data packet.
 * @param daqmx specified an instrument descriptor.
 * @param pMXBalanceData specified an initial balance data.
 * @return an error code that is not zero if error ocuured.
 * @since R2.01
 */
DAQMX_API int APIENTRY setBalanceMX(DAQMX           daqmx,
                                    MXBalanceData * pMXBalanceData);
/**
 * This gets an initial balance data packet.
 * @param daqmx specified an instrument descriptor.
 * @param pMXBalanceData stored an initial balance data.
 * @return an error code that is not zero if error ocuured.
 * @since R2.01
 */
DAQMX_API int APIENTRY getBalanceMX(DAQMX           daqmx,
                                    MXBalanceData * pMXBalanceData);
/**
 * This sets a AO/PWM and transmit data packet.
 * @param daqmx specified an instrument descriptor.
 * @param pMXAOPWMData stord a AO/PWM data.
 * @param pMXTransmit stord a transmit data.
 * @return an error code that is not zero if error ocuured.
 * @since R2.01
 */
DAQMX_API int APIENTRY getAOPWMDataMX(DAQMX         daqmx,
                                      MXAOPWMData * pMXAOPWMData,
                                      MXTransmit *  pMXTransmit);
/**
 * This sets resister range.
 * If min equals max, span and scale are omitted.
 * @param daqmx specified an instrument descriptor.
 * @param iRangeRES specified a selection of the range as DAQMX_RANGE_RES_xxx.
 * @param startChNo specified a start channel number.
 * @param endChNo specified an end channel number.
 * @param spanMin specified a value of the minimum span.
 * @param spanMax specified a value of the maximum span.
 * @param scaleMin specified a value of the minimum scale.
 * @param scaleMax specified a value of the maximum scale.
 * @param scalePoint specified a value of the decimal point positon.
 * @return an error code that is not zero if error ocuured.
 * @since R2.01
 */
DAQMX_API int APIENTRY setRESMX(DAQMX daqmx,
                                int   iRangeRES,
                                int   startChNo,
                                int   endChNo,
                                int   spanMin,
                                int   spanMax,
                                int   scaleMin,
                                int   scaleMax,
                                int   scalePoint);
/**
 * This sets strain range.
 * If min equals max, span and scale are omitted.
 * @param daqmx specified an instrument descriptor.
 * @param iRangeSTRAIN specified a selection of the range as DAQMX_RANGE_STR_xxx.
 * @param startChNo specified a start channel number.
 * @param endChNo specified an end channel number.
 * @param spanMin specified a value of the minimum span.
 * @param spanMax specified a value of the maximum span.
 * @param scaleMin specified a value of the minimum scale.
 * @param scaleMax specified a value of the maximum scale.
 * @param scalePoint specified a value of the decimal point positon.
 * @return an error code that is not zero if error ocuured.
 * @since R2.01
 */
DAQMX_API int APIENTRY setSTRAINMX(DAQMX daqmx,
                                   int   iRangeSTRAIN,
                                   int   startChNo,
                                   int   endChNo,
                                   int   spanMin,
                                   int   spanMax,
                                   int   scaleMin,
                                   int   scaleMax,
                                   int   scalePoint);
/**
 * This sets AO range.
 * If min equals max, span and scale are omitted.
 * @param daqmx specified an instrument descriptor.
 * @param iRangeAO specified a selection of the range as DAQMX_RANGE_AO_xxx.
 * @param startChNo specified a start channel number.
 * @param endChNo specified an end channel number.
 * @param spanMin specified a value of the minimum span.
 * @param spanMax specified a value of the maximum span.
 * @return an error code that is not zero if error ocuured.
 * @since R2.01
 */
DAQMX_API int APIENTRY setAOMX(DAQMX daqmx,
                               int   iRangeAO,
                               int   startChNo,
                               int   endChNo,
                               int   spanMin,
                               int   spanMax);
/**
 * This sets PWM range.
 * If min equals max, span and scale are omitted.
 * @param daqmx specified an instrument descriptor.
 * @param iRangePWM specified a selection of the range as DAQMX_RANGE_PWM_xxx.
 * @param startChNo specified a start channel number.
 * @param endChNo specified an end channel number.
 * @param spanMin specified a value of the minimum span.
 * @param spanMax specified a value of the maximum span.
 * @return an error code that is not zero if error ocuured.
 * @since R2.01
 */
DAQMX_API int APIENTRY setPWMMX(DAQMX daqmx,
                                int   iRangePWM,
                                int   startChNo,
                                int   endChNo,
                                int   spanMin,
                                int   spanMax);
/**
 * This sets an output type for AO/PWM.
 * @param daqmx specified an instrument descriptor.
 * @param iOutput specified an output type as DAQMX_OUTPUT_xxx.
 * @param startChNo specified a start channel number.
 * @param endChNo specified an end channel number.
 * @return an error code that is not zero if error ocuured.
 * @since R2.01
 */
DAQMX_API int APIENTRY setOutputTypeMX(DAQMX daqmx,
                                       int   iOutput,
                                       int   startChNo,
                                       int   endChNo);
/**
 * This sets a choices of AO/PWM.
 * @param daqmx specified an instrument descriptor.
 * @param startChNo specified a start channel number.
 * @param endChNo specified an end channel number.
 * @param idleChoice specified a choice when an idle, as DAQMX_CHOICE_xxx.
 * @param errorChoice specified a choice when  an error, as DAQMX_CHOICE_xxx.
 * @param presetValue specified a value if DAQMX_CHOICE_PRESET.
 * @return an error code that is not zero if error ocuured.
 * @since R2.01
 */
DAQMX_API int APIENTRY setChoiceMX(DAQMX daqmx,
                                   int   startChNo,
                                   int   endChNo,
                                   int   idleChoice,
                                   int   errorChoice,
                                   int   presetValue);
/**
 * This sets pulse times of PWM.
 * @param daqmx specified an instrument descriptor.
 * @param pulseTime specified times by range.
 * @param startChNo specified a start channel number.
 * @param endChNo specified an end channel number.
 * @return an error code that is not zero if error ocuured.
 * @since R2.01
 */
DAQMX_API int APIENTRY setPulseTimeMX(DAQMX daqmx,
                                      int   pulseTime,
                                      int   startChNo,
                                      int   endChNo);
/**
 * This sets a AO type on configure.
 * @param daqmx specified an instrument descriptor.
 * @param aoNo specified a AO number as channel number.
 * @param iKind specified a AO channel kind as DAQMX_CHKIND_AOxxx
 * @param refChNo specified a refernece chaneel number as Input.
 * @return an error code that is not zero if error ocuured.
 * @since R2.01
 */
DAQMX_API int APIENTRY setAOTypeMX(DAQMX daqmx,
                                   int   aoNo,
                                   int   iKind,
                                   int   refChNo);
/**
 * This sets a PWM type on configure.
 * @param daqmx specified an instrument descriptor.
 * @param pwmNo specified a PWM number as channel number.
 * @param iKind specified a PWM channel kind as DAQMX_CHKIND_PWMxxx
 * @param refChNo specified a refernece chaneel number as Input.
 * @return an error code that is not zero if error ocuured.
 * @since R2.01
 */
DAQMX_API int APIENTRY setPWMTypeMX(DAQMX daqmx,
                                    int   pwmNo,
                                    int   iKind,
                                    int   refChNo);
/**
 * This gets an output choice data.
 * @param daqmx specified an instrument descriptor.
 * @param pMXOutputData stored an output choice data.
 * @return an error code that is not zero if error ocuured.
 * @since R2.01
 */
DAQMX_API int APIENTRY getOutputMX(DAQMX          daqmx,
                                   MXOutputData * pMXOutputData);
/**
 * This sets an output choice data.
 * @param daqmx specified an instrument descriptor.
 * @param pMXOutputData specified an output choice data.
 * @return an error code that is not zero if error ocuured.
 * @since R2.01
 */
DAQMX_API int APIENTRY setOutputMX(DAQMX          daqmx,
                                   MXOutputData * pMXOutputData);
/**
 * This changes a AO/PWM in the AO/PWM data packet.
 * @param pMXAOPWMData changed a AO/PWM data that stored by getAOPWMDataMX.
 * @param aopwmNo specified a AO/PWM number as channel number.
 * @param bValid specified a effective AO/PWM as boolean.
 * @param iAOPWMValue specified a value that changes by toAOPWMValueMX.
 * @return an error code that is not zero if error ocuured.
 * @since R2.01
 */
DAQMX_API int APIENTRY changeAOPWMDataMX(MXAOPWMData * pMXAOPWMData,
                                         int           aopwmNo,
                                         int           bValid,
                                         int           iAOPWMValue);
/**
 * This changes an initial balance in the Balance data packet.
 * @param pMXBalanceData changed a Balance data that stored by getBalanceDataMX.
 * @param balanceNo specified a Balance number as channel number.
 * @param bValid specified a effective Balance as boolean.
 * @param iValue specified a value.
 * @return an error code that is not zero if error ocuured.
 * @since R2.01
 */
DAQMX_API int APIENTRY changeBalanceMX(MXBalanceData * pMXBalanceData,
                                       int             balanceNo,
                                       int             bValid,
                                       int             iValue);
/**
 * This changes a transmit status in the transmit data packet.
 * @param pMXTransmit changed a transmit data that stored by getAOPWMDataMX.
 * @param aopwmNo specified a AO/PWM number as channel number.
 * @param iTrans specified a status as DAQMX_TRANSMIT_xxx
 * @return an error code that is not zero if error ocuured.
 * @since R2.01
 */
DAQMX_API int APIENTRY changeTransmitMX(MXTransmit * pMXTransmit,
                                        int          aopwmNo,
                                        int          iTrans);
/**
 * This gets a string of the alarm type.
 * If Visual Basic, Use toAlarmNameMX.
 * @param iAlarmType specified a selection of the alarm type as DAQMX_ALARM_xxx.
 * @return a string of the alarm type.
 * @since R2.01
 */
DAQMX_API const char * APIENTRY getAlarmNameMX(int iAlarmType);
/**
 * This gets a maximum length of alarm name.
 * @return a length by bytes without NULL.
 * @since R2.01
 */
DAQMX_API int APIENTRY getMaxLenAlarmNameMX(void);
/**
 * This changes from an output value to a specified value with range.
 * @param realValue specified an output value.
 * @param iRangeAOPWM specified a range as DAQMX_RANGE_AO_xxx or DAQMX_RANEG_PWM_xxx
 * @return a value specified on setAOPWMData.
 * @since R2.01
 */
DAQMX_API int APIENTRY toAOPWMValueMX(double realValue,
                                      int    iRangeAOPWM);
/**
 * This changes from a specified value to an output value with range.
 * @param realValue specified a value specified on setAOPWMData.
 * @param iRangeAOPWM specified a range as DAQMX_RANGE_AO_xxx or DAQMX_RANEG_PWM_xxx
 * @return an output value.
 * @since R2.01
 */
DAQMX_API double APIENTRY toRealValueMX(int iAOPWMValue,
                                        int iRangeAOPWM);
/**
 * This gets an item number if an error occured when it set configure.
 * @param daqmx specified an instrument descriptor.
 * @param itemErr stored an item number.
 * @return an error code that is not zero if error ocuured.
 * @since R2.01
 */
DAQMX_API int APIENTRY getItemErrorMX(DAQMX daqmx,
                                      int * itemErr);
/**
 * This checks a data number effective.
 * @param dataNo specified a data number.
 * @return If effective, TRUE(1). Others, FALSE(0).
 * @since R2.01
 */
DAQMX_API int APIENTRY isDataNoMX(MXDataNo dataNo);
/**
 * This checks a data number effective for VB.
 * @see isDataNoMX
 * @since R2.01
 */
DAQMX_API int APIENTRY isDataNoVBMX(MXDataNo * dataNo);
/**
 * This converts a style to a version as firmware.
 * @param style specified a style number from system information.
 * @return a version
 * @since R2.01
 */
DAQMX_API int APIENTRY toStyleVersionMX(int style);
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// since R3.01
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
/**
 * This sets a range for communication.
 * If min equals max, span and scale are omitted.
 * @param daqmx specified an instrument descriptor.
 * @param iRangeCOM specified a selection of the range as DAQMX_RANGE_COM_xxx.
 * @param startChNo specified a start channel number.
 * @param endChNo specified an end channel number.
 * @param spanMin specified a value of the minimum span.
 * @param spanMax specified a value of the maximum span.
 * @param scaleMin specified a value of the minimum scale.
 * @param scaleMax specified a value of the maximum scale.
 * @param scalePoint specified a value of the decimal point positon.
 * @return an error code that is not zero if error ocuured.
 * @since R3.01
 */
DAQMX_API int APIENTRY setCOMMX(DAQMX daqmx,
                                int   iRangeCOM,
                                int   startChNo,
                                int   endChNo,
                                int   spanMin,
                                int   spanMax,
                                int   scaleMin,
                                int   scaleMax,
                                int   scalePoint);
/**
 * This sets a range for Pulse.
 * If min equals max, span and scale are omitted.
 * @param daqmx specified an instrument descriptor.
 * @param iRangePULSE specified a selection of the range as DAQMX_RANGE_PI_xxx.
 * @param startChNo specified a start channel number.
 * @param endChNo specified an end channel number.
 * @param spanMin specified a value of the minimum span.
 * @param spanMax specified a value of the maximum span.
 * @param scaleMin specified a value of the minimum scale.
 * @param scaleMax specified a value of the maximum scale.
 * @param scalePoint specified a value of the decimal point positon.
 * @return an error code that is not zero if error ocuured.
 * @since R3.01
 */
DAQMX_API int APIENTRY setPULSEMX(DAQMX daqmx,
                                  int   iRangePULSE,
                                  int   startChNo,
                                  int   endChNo,
                                  int   spanMin,
                                  int   spanMax,
                                  int   scaleMin,
                                  int   scaleMax,
                                  int   scalePoint);
/**
 * This sets a chattaring filter.
 * @param daqmx specified an instrument descriptor.
 * @param bChatFilter specified a chattaring filter as boolean.
 * @param startChNo specified a start channel number.
 * @param endChNo specified an end channel number.
 * @since R3.01
 */
DAQMX_API int APIENTRY setChatFilterMX(DAQMX daqmx,
                                       int   bChatFilter,
                                       int   startChNo,
                                       int   endChNo);
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
#ifdef __cplusplus
}
#endif
///////////////////////////////////////////////////////////////////////
#if defined(WIN32) || defined(_WIN32_WCE)
/**
 * This defines types as load library callback for MS-Windows.
 * The sample is as follows :
 * <pre>
 * <code>
 * HMODULE pDll = LoadLibrary("DAQMX");
 * DLLOPENMX openMX = (DLLOPENMX)GetProcAddress(pDll, "openMX");
 * </code>
 * </pre>
 */
// Low level communications
typedef DAQMX (CALLBACK* DLLOPENMX)(const char *, int *);
typedef int   (CALLBACK* DLLCLOSEMX)(DAQMX);
// Middle level communications
// FIFO control commands
typedef int   (CALLBACK* DLLSTARTFIFOMX)(DAQMX);
typedef int   (CALLBACK* DLLSTOPFIFOMX)(DAQMX);
typedef int   (CALLBACK* DLLAUTOFIFOMX)(DAQMX, int);
// Date time commands
typedef int   (CALLBACK* DLLSETDATETIMEMX)(DAQMX, MXDateTime *);
typedef int   (CALLBACK* DLLSETDATETIMENOWMX)(DAQMX);
// Control commands
typedef int   (CALLBACK* DLLSETBACKUPMX)(DAQMX, int, int);
typedef int   (CALLBACK* DLLFORMATCFMX)(DAQMX);
typedef int   (CALLBACK* DLLINITSYSTEMMX)(DAQMX, int);
typedef int   (CALLBACK* DLLSETSEGMENTMX)(DAQMX, int, int, MXSegment *, MXSegment *);
// Get status
typedef int   (CALLBACK* DLLGETSTATUSDATAMX)(DAQMX, MXStatus *);
// Get system
typedef int   (CALLBACK* DLLGETSYSTEMCONFIGMX)(DAQMX, MXSystemInfo *);
// Configurature
typedef int   (CALLBACK* DLLGETCONFIGDATAMX)(DAQMX, MXConfigData *);
typedef int   (CALLBACK* DLLSETCONFIGDATAMX)(DAQMX, MXConfigData *);
// DO
typedef int   (CALLBACK* DLLGETDODATAMX)(DAQMX, MXDOData *);
typedef int   (CALLBACK* DLLSETDODATAMX)(DAQMX, MXDOData *);
typedef int   (CALLBACK* DLLCHANGEDODATAMX)(MXDOData *, int, int, int);
typedef int   (CALLBACK* DLLSETDOTYPEMX)(DAQMX, int, int, int, int);
// Get channel information
typedef int   (CALLBACK* DLLTALKCHINFOMX)(DAQMX, int, int);
typedef int   (CALLBACK* DLLGETCHINFOMX)(DAQMX, MXChInfo *, int *);
// Talker measured data by each channels
typedef int   (CALLBACK* DLLGETCHDATANOMX)(DAQMX, int, MXDataNo *, MXDataNo *);
typedef int   (CALLBACK* DLLTALKCHDATAMX)(DAQMX, int, MXDataNo, MXDataNo);
typedef int   (CALLBACK* DLLTALKCHDATAINSTMX)(DAQMX, int);
// Talker measured data by each FIFO
typedef int   (CALLBACK* DLLGETFIFODATANOMX)(DAQMX, int, MXDataNo *, MXDataNo *);
typedef int   (CALLBACK* DLLTALKFIFODATAMX)(DAQMX, int, MXDataNo, MXDataNo);
typedef int   (CALLBACK* DLLTALKFIFODATAINSTMX)(DAQMX, int);
// Get measured data after talker
typedef int   (CALLBACK* DLLGETTIMEDATAMX)(DAQMX, MXDataNo *, MXDateTime *, MXUserTime *, int *);
typedef int   (CALLBACK* DLLGETCHDATAMX)(DAQMX, MXDataNo *, MXChInfo *, MXDataInfo *, int *);
// Misc
typedef int     (CALLBACK* DLLSETUSERTIMEMX)(DAQMX, MXUserTime);
typedef int     (CALLBACK* DLLGETLASTERRORMX)(DAQMX, int *);
// Set range
typedef int     (CALLBACK* DLLSETSKIPMX)(DAQMX, int, int);
typedef int     (CALLBACK* DLLSETVOLTMX)(DAQMX, int, int, int, int, int, int, int, int);
typedef int     (CALLBACK* DLLSETTCMX)(DAQMX, int, int, int, int, int, int, int, int);
typedef int     (CALLBACK* DLLSETRTDMX)(DAQMX, int, int, int, int, int, int, int, int);
typedef int     (CALLBACK* DLLSETDIMX)(DAQMX, int, int, int, int, int, int, int, int);
typedef int     (CALLBACK* DLLSETDELTAMX)(DAQMX, int, int, int, int, int, int, int, int, int);
typedef int     (CALLBACK* DLLSETRRJCMX)(DAQMX, int, int, int, int, int);
// Scalling
typedef int     (CALLBACK* DLLSETSCALLINGUNITMX)(DAQMX, const char *, int, int);
// Alarm
typedef int     (CALLBACK* DLLSETALARMMX)(DAQMX, int, int, int, int, int, int);
// Channels
typedef int     (CALLBACK* DLLSETTAGMX)(DAQMX, const char *, int, int);
typedef int     (CALLBACK* DLLSETCOMMENTMX)(DAQMX, const char *, int, int);
typedef int     (CALLBACK* DLLSETRJCTYPEMX)(DAQMX, int, int, int, int);
typedef int     (CALLBACK* DLLSETFILTERMX)(DAQMX, int, int, int);
typedef int     (CALLBACK* DLLSETBURNOUTMX)(DAQMX, int, int, int);
typedef int     (CALLBACK* DLLSETREFALARMMX)(DAQMX, int, int, int, int, int);
// Unit
typedef int     (CALLBACK* DLLSETINTERVALMX)(DAQMX, int, int, int);
typedef int     (CALLBACK* DLLSETTEMPUNITMX)(DAQMX, int);
typedef int     (CALLBACK* DLLSETUNITNOMX)(DAQMX, int);
typedef int     (CALLBACK* DLLSETSYSTEMTIMEOUTMX)(DAQMX, int);
// Utilities
typedef double  (CALLBACK* DLLTODOUBLEVALUEMX)(int, int);
typedef int     (CALLBACK* DLLTOSTRINGVALUEMX)(int, int, char *, int);
typedef int     (CALLBACK* DLLTOALARMNAMEMX)(int, char *, int);
// Messages
typedef int     (CALLBACK* DLLGETVERSIONAPIMX)(void);
typedef int     (CALLBACK* DLLGETREVISIONAPIMX)(void);
typedef int     (CALLBACK* DLLTOERRORMESSAGEMX)(int, char *, int);
typedef int     (CALLBACK* DLLGETMAXLENERRORMESSAGEMX)(void);
typedef LPCSTR  (CALLBACK* DLLGETERRORMESSAGEMX)(int);
// Deprecated command
typedef int     (CALLBACK* DLLSETTIMEOUTMX)(DAQMX, int);
// for Visual Basic
typedef int     (CALLBACK* DLLTALKCONFIGMX)(DAQMX, MXSystemInfo *, MXStatus *, MXNetInfo *);
typedef int     (CALLBACK* DLLGETCHCONFIGMX)(DAQMX, MXChConfig *, int *);
typedef int     (CALLBACK* DLLSETSYSTEMCONFIGMX)(DAQMX, MXSystemInfo *);
typedef int     (CALLBACK* DLLSETCHCONFIGMX)(DAQMX, MXChConfig *);
typedef int     (CALLBACK* DLLTALKCHDATAVBMX)(DAQMX, int, MXDataNo *, MXDataNo *);
typedef int     (CALLBACK* DLLTALKFIFODATAVBMX)(DAQMX, int, MXDataNo *, MXDataNo *);
typedef int     (CALLBACK* DLLSETUSERTIMEVBMX)(DAQMX, MXUserTime *);
typedef void    (CALLBACK* DLLINCREMENTDATANOMX)(MXDataNo *, int);
typedef void    (CALLBACK* DLLDECREMENTDATANOMX)(MXDataNo *, int);
typedef int     (CALLBACK* DLLCOMPAREDATANOMX)(MXDataNo *, MXDataNo *);
typedef void    (CALLBACK* DLLTODATETIMEMX)(MXDateTime *, int *, int *, int *, int *, int *, int *);
// since R2.01
typedef int     (CALLBACK* DLLSETAOPWMDATAMX)(DAQMX, MXAOPWMData *);
typedef int     (CALLBACK* DLLSETTRANSMITMX)(DAQMX, MXTransmit *);
typedef int     (CALLBACK* DLLRUNBALANCEMX)(DAQMX, MXBalanceData *, MXBalanceResult *);
typedef int     (CALLBACK* DLLRESETBALANCEMX)(DAQMX, MXBalanceData *, MXBalanceResult *);
typedef int     (CALLBACK* DLLSETBALANCEMX)(DAQMX, MXBalanceData *);
typedef int     (CALLBACK* DLLGETBALANCEMX)(DAQMX, MXBalanceData *);
typedef int     (CALLBACK* DLLGETAOPWMDATAMX)(DAQMX, MXAOPWMData *, MXTransmit *);
typedef int     (CALLBACK* DLLSETRESMX)(DAQMX, int, int, int, int, int, int, int, int);
typedef int     (CALLBACK* DLLSETSTRAINMX)(DAQMX, int, int, int, int, int, int, int, int);
typedef int     (CALLBACK* DLLSETAOMX)(DAQMX, int, int, int, int, int);
typedef int     (CALLBACK* DLLSETPWMMX)(DAQMX, int, int, int, int, int);
typedef int     (CALLBACK* DLLSETOUTPUTTYPEMX)(DAQMX, int, int, int);
typedef int     (CALLBACK* DLLSETCHOICEMX)(DAQMX, int, int, int, int, int);
typedef int     (CALLBACK* DLLSETPULSETIMEMX)(DAQMX, int, int, int);
typedef int     (CALLBACK* DLLSETAOTYPEMX)(DAQMX, int, int, int);
typedef int     (CALLBACK* DLLSETPWMTYPEMX)(DAQMX, int, int, int);
typedef int     (CALLBACK* DLLGETOUTPUTMX)(DAQMX, MXOutputData *);
typedef int     (CALLBACK* DLLSETOUTPUTMX)(DAQMX, MXOutputData *);
typedef int     (CALLBACK* DLLCHANGEAOPWMDATAMX)(MXAOPWMData *, int, int, int);
typedef int     (CALLBACK* DLLCHANGEBALANCEMX)(MXBalanceData*, int, int, int);
typedef int     (CALLBACK* DLLCHANGETRANSMITMX)(MXTransmit *, int, int);
typedef LPCSTR  (CALLBACK* DLLGETALARMNAMEMX)(int);
typedef int     (CALLBACK* DLLGETMAXLENALARMNAMEMX)(void);
typedef int     (CALLBACK* DLLTOAOPWMVALUEMX)(double, int);
typedef double  (CALLBACK* DLLTOREALVALUEMX)(int, int);
typedef int     (CALLBACK* DLLGETITEMERRORMX)(DAQMX, int *);
typedef int     (CALLBACK* DLLISDATANOMX)(MXDataNo);
typedef int     (CALLBACK* DLLISDATANOVBMX)(MXDataNo *);
typedef int     (CALLBACK* DLLTOSTYLEVERSIONMX)(int);
// since R3.01
typedef int     (CALLBACK* DLLSETCOMMX)(DAQMX, int, int, int, int, int, int, int, int);
typedef int     (CALLBACK* DLLSETPULSEMX)(DAQMX, int, int, int, int, int, int, int, int);
typedef int     (CALLBACK* DLLSETCHATFILTERMX)(DAQMX, int, int, int);
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
#pragma pack(pop, oldpack) /* R2.01 */
#endif //WIN32,WCE
///////////////////////////////////////////////////////////////////////
#endif //_DAQMX_H_
