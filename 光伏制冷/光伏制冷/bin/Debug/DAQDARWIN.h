// DAQDARWIN.h
/* -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- */
/*
 * Copyright (c) 2003-2004 Yokogawa Electric Corporation. All rights reserved.
 *
 * This is defined export DAQDARWIN.dll.
 */
/* -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- */
// 2004/11/01 Ver.2 Rev.1
// 2003/05/30 Ver.1 Rev.1
///////////////////////////////////////////////////////////////////////
#ifndef _DAQDARWIN_H_
#define _DAQDARWIN_H_
///////////////////////////////////////////////////////////////////////
#if defined(WIN32) || defined(_WIN32_WCE)
#pragma pack(push, oldpack, 8) /* R2.01 */
// system
#include <windows.h>
// calling
#ifdef DAQDARWIN_EXPORTS
#define DAQHANDLER_EXPORTS
#define DAQDARWIN_API __declspec(dllexport)
#else
#define DAQDARWIN_API __declspec(dllimport)
#endif
#else  //WIN32,WCE
#define DAQDARWIN_API
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
#define DAQDARWIN_COMMPORT      (34150)
// total number
#define DAQDARWIN_NUMCHANNEL    (360)
#define DAQDARWIN_NUMALARM      (4)
#define DAQDARWIN_NUMUNIT       (6)
#define DAQDARWIN_NUMSLOT       (6)
#define DAQDARWIN_NUMTERM       (10)
// string length without NULL
#define DAQDARWIN_MAXCHNAMELEN  (3) /* ex. "001" */
#define DAQDARWIN_MAXCHRANGLEN  (6) /* ex. "001-60" */
#define DAQDARWIN_MAXUNITLEN    (6)
#define DAQDARWIN_MAXMODULELEN  (6)
#define DAQDARWIN_MAXRELAYLEN   (DAQDARWIN_MAXCHNAMELEN)
// maximum value
#define DAQDARWIN_MAXDECIMALPOINT       (4)
// string
#define DAQDARWIN_TERMINATE     "\r\n"
//-- ---- ---- ---- ---- ---- field values
// valid
#define DAQDARWIN_VALID_OFF     (0) /* FALSE */
#define DAQDARWIN_VALID_ON      (1) /* TRUE */
// flag : logical OR
#define DAQDARWIN_FLAG_OFF      (0x00000000)
#define DAQDARWIN_FLAG_ENDDATA  (0x00000001) /* E(N) */
// data status
#define DAQDARWIN_DATA_UNKNOWN          (0x00000000)
#define DAQDARWIN_DATA_NORMAL           (0x00000001) /* N */
#define DAQDARWIN_DATA_DIFFINPUT        (0x00000002) /* D */
#define DAQDARWIN_DATA_READER           (0x00000003) /* R2.01 (space) */
#define DAQDARWIN_DATA_PLUSOVER         (0x00007FFF) /* O : > 0*/
#define DAQDARWIN_DATA_MINUSOVER        (0x00008001) /* O : < 0*/
#define DAQDARWIN_DATA_SKIP             (0x00008002) /* S */
#define DAQDARWIN_DATA_ILLEGAL          (0x00008003)
#define DAQDARWIN_DATA_ABNORMAL         (0x00008004) /* E */
#define DAQDARWIN_DATA_NODATA           (0x00008005)
// alarm type
#define DAQDARWIN_ALARM_NONE    (0) /* OFF */
#define DAQDARWIN_ALARM_UPPER   (1)
#define DAQDARWIN_ALARM_LOWER   (2)
#define DAQDARWIN_ALARM_UPDIFF  (3) /* Upper Differential Alarm */
#define DAQDARWIN_ALARM_LOWDIFF (4) /* Lower Differential Alarm */
#define DAQDARWIN_ALARM_INCRATE (5) /* Increasing Rate Of Change Alarm */
#define DAQDARWIN_ALARM_DECRATE (6) /* Decreasing Rate Of Change Alarm */
// system control
#define DAQDARWIN_SYSTEM_RECONSTRUCT    (1) /* RS */
#define DAQDARWIN_SYSTEM_INITOPE        (2) /* RC */
#define DAQDARWIN_SYSTEM_RESETALARM     (3) /* AR */
// channel/relay type (reference to module number)
#define DAQDARWIN_CHTYPE_MAINUNIT       (-1)
#define DAQDARWIN_CHTYPE_STANDALONE     (0)
#define DAQDARWIN_CHTYPE_MATHTYPE       (0x0080)
#define DAQDARWIN_CHTYPE_SWITCH         (0x0040)
#define DAQDARWIN_CHTYPE_COMMDATA       (0x0020)
#define DAQDARWIN_CHTYPE_CONSTANT       (0x0010)
#define DAQDARWIN_CHTYPE_REPORT         (0x0100) /* R2.01 */
// specified channel/relay type
#define I                       (DAQDARWIN_CHTYPE_MAINUNIT)
#define A                       (DAQDARWIN_CHTYPE_MATHTYPE)
#define S                       (DAQDARWIN_CHTYPE_SWITCH)
#define C                       (DAQDARWIN_CHTYPE_COMMDATA)
#define K                       (DAQDARWIN_CHTYPE_CONSTANT)
#define R                       (DAQDARWIN_CHTYPE_REPORT) /* R2.01 */
// mode
#define DAQDARWIN_MODE_OPE      (0) /* Operation Mode */
#define DAQDARWIN_MODE_SETUP    (1) /* Setup Mode */
#define DAQDARWIN_MODE_CALIB    (2) /* Calibration Mode */
// talker output type
#define DAQDARWIN_TALK_MEASUREDDATA     (0)
#define DAQDARWIN_TALK_OPEDATA          (1)
#define DAQDARWIN_TALK_CHINFODATA       (2)
#define DAQDARWIN_TALK_REPORTDATA       (4) /* R2.01 */
#define DAQDARWIN_TALK_SYSINFODATA      (5)
#define DAQDARWIN_TALK_CALIBDATA        (8) /* On Calibration Mode */
#define DAQDARWIN_TALK_SETUPDATA        (9) /* On Setup Mode */
// status byte : logical OR
#define DAQDARWIN_STATUS_OFF            (0x00)
#define DAQDARWIN_STATUS_ADCONV         (0x01) /* A/D conversion end */
#define DAQDARWIN_STATUS_SYNTAX         (0x02) /* syntax error */
#define DAQDARWIN_STATUS_TIMER          (0x04) /* internal timer or report */
#define DAQDARWIN_STATUS_MEDIA          (0x08) /* storing data or reading data end */
#define DAQDARWIN_STATUS_RELEASE        (0x20) /* measurement release */
#define DAQDARWIN_STATUS_SRQ            (0x40) /* R2.01 SRQ */
#define DAQDARWIN_STATUS_ALL            (0xFF)
//-- ---- ---- ---- ---- ---- Param
// establish
#define DAQDARWIN_SETUP_ABORT   (DAQDARWIN_VALID_OFF)
#define DAQDARWIN_SETUP_STORE   (DAQDARWIN_VALID_ON)
// unit number
#define DAQDARWIN_UNITNO_MAINUNIT       (DAQDARWIN_CHTYPE_MAINUNIT)
#define DAQDARWIN_UNITNO_STANDALONE     (DAQDARWIN_CHTYPE_STANDALONE)
// computation
#define DAQDARWIN_COMPUTE_START         (0)
#define DAQDARWIN_COMPUTE_STOP          (1)
#define DAQDARWIN_COMPUTE_RESTART       (2)
#define DAQDARWIN_COMPUTE_CLEAR         (3)
#define DAQDARWIN_COMPUTE_RELEASE       (4)
//-- ---- ---- ---- ---- ---- Report
// reporting
#define DAQDARWIN_REPORT_RUN_START       (0) /* Not ON */
#define DAQDARWIN_REPORT_RUN_STOP        (1) /* Not OFF */
// report type : TS4 + trigger + RFx
#define DAQDARWIN_REPORT_HOURLY  (0)
#define DAQDARWIN_REPORT_DAILY   (1)
#define DAQDARWIN_REPORT_MONTHLY (2)
#define DAQDARWIN_REPORT_STATUS  (3)
// report status : logical or
#define DAQDARWIN_REPSTATUS_NONE                 (0x0000)
#define DAQDARWIN_REPSTATUS_HOURLY_NEW           (0x0001)
#define DAQDARWIN_REPSTATUS_HOURLY_VALID         (0x0002)
#define DAQDARWIN_REPSTATUS_DAILY_NEW            (0x0004)
#define DAQDARWIN_REPSTATUS_DAILY_VALID          (0x0008)
#define DAQDARWIN_REPSTATUS_MONTHLY_NEW          (0x0010)
#define DAQDARWIN_REPSTATUS_MONTHLY_VALID        (0x0020)
//-- ---- ---- ---- ---- ---- Range
// VOLT (DC Voltage)
#define DAQDARWIN_RANGE_VOLT_20MV       (1)  /*  -20.000  -  20.000  mV */
#define DAQDARWIN_RANGE_VOLT_60MV       (2)  /*  -60.00   -  60.00   mV */
#define DAQDARWIN_RANGE_VOLT_200MV      (3)  /* -200.00   - 200.00   mV */
#define DAQDARWIN_RANGE_VOLT_2V         (4)  /*   -2.0000 -   2.0000  V */
#define DAQDARWIN_RANGE_VOLT_6V         (5)  /*   -6.000  -   6.000   V */
#define DAQDARWIN_RANGE_VOLT_20V        (6)  /*  -20.000  -  20.000   V */
#define DAQDARWIN_RANGE_VOLT_50V        (7)  /*  -50.00   -  50.00    V */
// TC (Thermocuple)
#define DAQDARWIN_RANGE_TC_R    (1)  /*    0.0 - 1760.0 C */
#define DAQDARWIN_RANGE_TC_S    (2)  /*    0.0 - 1760.0 C */
#define DAQDARWIN_RANGE_TC_B    (3)  /*    0.0 - 1820.0 C */
#define DAQDARWIN_RANGE_TC_K    (4)  /* -200.0 - 1370.0 C */
#define DAQDARWIN_RANGE_TC_E    (5)  /* -200.0 -  800.0 C */
#define DAQDARWIN_RANGE_TC_J    (6)  /* -200.0 - 1100.0 C */
#define DAQDARWIN_RANGE_TC_T    (7)  /* -200.0 -  400.0 C */
#define DAQDARWIN_RANGE_TC_N    (8)  /*    0.0 - 1300.0 C */
#define DAQDARWIN_RANGE_TC_W    (9)  /*    0.0 - 2315.0 C */
#define DAQDARWIN_RANGE_TC_L    (10) /* -200.0 -  900.0 C */
#define DAQDARWIN_RANGE_TC_U    (11) /* -200.0 -  400.0 C */
#define DAQDARWIN_RANGE_TC_KP   (12) /*    0.0 -  300.0 K */
// RTD (Resistance Temperature Detector)
#define DAQDARWIN_RANGE_RTD_1MAPT       (1)  /* PT100    -200.0  - 600.0  C */
#define DAQDARWIN_RANGE_RTD_2MAPT       (2)  /* PT100    -200.0  - 250.0  C */
#define DAQDARWIN_RANGE_RTD_1MAJPT      (3)  /* JPT100   -200.0  - 550.0  C */
#define DAQDARWIN_RANGE_RTD_2MAJPT      (4)  /* JPT100   -200.0  - 250.0  C */
#define DAQDARWIN_RANGE_RTD_2MAPT50     (5)  /* PT50     -200.0  - 550.0  C */
#define DAQDARWIN_RANGE_RTD_1MAPTH      (6)  /* PT100 H  -140.00 - 150.00 C */
#define DAQDARWIN_RANGE_RTD_2MAPTH      (7)  /* PT100 H   -70.00 -  70.00 C */
#define DAQDARWIN_RANGE_RTD_1MAJPTH     (8)  /* JPT100 H -140.00 - 150.00 C */
#define DAQDARWIN_RANGE_RTD_2MAJPTH     (9)  /* JPT100 H  -70.00 -  70.00 C */
#define DAQDARWIN_RANGE_RTD_1MANIS      (10) /* Ni100 S  -200.0  - 250.0  C */
#define DAQDARWIN_RANGE_RTD_1MANID      (11) /* Ni100 D   -60.0  - 180.0  C */
#define DAQDARWIN_RANGE_RTD_1MANI120    (12) /* Ni120     -70.0  - 200.0  C */
#define DAQDARWIN_RANGE_RTD_CU10GE      (13) /* GE       -200.0  - 300.0  C */
#define DAQDARWIN_RANGE_RTD_CU10LN      (14) /* L&N      -200.0  - 300.0  C */
#define DAQDARWIN_RANGE_RTD_CU10WEED    (15) /* WEED     -200.0  - 300.0  C */
#define DAQDARWIN_RANGE_RTD_CU10BAILEY  (16) /* BAILEY   -200.0  - 300.0  C */
#define DAQDARWIN_RANGE_RTD_J263B       (17) /* J263*B     -0.0  - 300.0  K */
// DI (Contact)
#define DAQDARWIN_RANGE_DI_LEVEL        (1) /* 0 < 2.4V <= 1 */
#define DAQDARWIN_RANGE_DI_CONTACT      (2) /* 0 = Off, 1 = On */
// since R2.01
// mA
#define DAQDARWIN_RANGE_MA_20MA (1)
// STRAIN
#define DAQDARWIN_RANGE_STRAIN_2K        (1)
#define DAQDARWIN_RANGE_STRAIN_20K       (2)
#define DAQDARWIN_RANGE_STRAIN_200K      (3)
// PULSE
#define DAQDARWIN_RANGE_PULSE_RATE       (1)
#define DAQDARWIN_RANGE_PULSE_GATE       (2)
// -- ---- ---- ---- ---- ---- power module
// POWER
#define DAQDARWIN_RANGE_POWER_25V05A     (1)
#define DAQDARWIN_RANGE_POWER_25V5A      (2)
#define DAQDARWIN_RANGE_POWER_250V05A    (3)
#define DAQDARWIN_RANGE_POWER_250V5A     (4)
// wiring method
#define DAQDARWIN_WIRE_1PH2W    (1)
#define DAQDARWIN_WIRE_1PH3W    (2)
#define DAQDARWIN_WIRE_3PH3W2I  (3)
#define DAQDARWIN_WIRE_3PH3W3I  (4)
#define DAQDARWIN_WIRE_3PH4W    (5)
// mesurement item
#define DAQDARWIN_POWERITEM_I0    (0x0000)
#define DAQDARWIN_POWERITEM_I1    (0x0001)
#define DAQDARWIN_POWERITEM_I2    (0x0002)
#define DAQDARWIN_POWERITEM_I3    (0x0003)
#define DAQDARWIN_POWERITEM_I13   (0x000D)
#define DAQDARWIN_POWERITEM_P0    (0x0010)
#define DAQDARWIN_POWERITEM_P1    (0x0011)
#define DAQDARWIN_POWERITEM_P2    (0x0012)
#define DAQDARWIN_POWERITEM_P3    (0x0013)
#define DAQDARWIN_POWERITEM_P13   (0x001D)
#define DAQDARWIN_POWERITEM_PF0   (0x0020)
#define DAQDARWIN_POWERITEM_PF1   (0x0021)
#define DAQDARWIN_POWERITEM_PF2   (0x0022)
#define DAQDARWIN_POWERITEM_PF3   (0x0023)
#define DAQDARWIN_POWERITEM_PF13  (0x002D)
#define DAQDARWIN_POWERITEM_PH0   (0x0030)
#define DAQDARWIN_POWERITEM_PH1   (0x0031)
#define DAQDARWIN_POWERITEM_PH2   (0x0032)
#define DAQDARWIN_POWERITEM_PH3   (0x0033)
#define DAQDARWIN_POWERITEM_PH13  (0x003D)
#define DAQDARWIN_POWERITEM_V0    (0x0040)
#define DAQDARWIN_POWERITEM_V1    (0x0041)
#define DAQDARWIN_POWERITEM_V2    (0x0042)
#define DAQDARWIN_POWERITEM_V3    (0x0043)
#define DAQDARWIN_POWERITEM_V13   (0x004D)
#define DAQDARWIN_POWERITEM_VA0   (0x0050)
#define DAQDARWIN_POWERITEM_VA1   (0x0051)
#define DAQDARWIN_POWERITEM_VA2   (0x0052)
#define DAQDARWIN_POWERITEM_VA3   (0x0053)
#define DAQDARWIN_POWERITEM_VA13  (0x005D)
#define DAQDARWIN_POWERITEM_VAR0  (0x0060)
#define DAQDARWIN_POWERITEM_VAR1  (0x0061)
#define DAQDARWIN_POWERITEM_VAR2  (0x0062)
#define DAQDARWIN_POWERITEM_VAR3  (0x0063)
#define DAQDARWIN_POWERITEM_VAR13 (0x006D)
#define DAQDARWIN_POWERITEM_FREQ  (0x007F)
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// Type
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// DAQ descriptor
// If Visual Basic, type as Long.
typedef int DAQDARWIN;
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// Structure (8 bytes align)
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
//-- ---- ---- ---- ---- ---- Date Time
typedef struct {
    char  aYear;   //19xx:70-99, 20xx:00-34
    char  aMonth;  //1-12
    char  aDay;    //1-31
    char  aHour;   //0-23
    char  aMinute; //0-59
    char  aSecond; //0-59
    short aMilliSecond; //Not Use
} DarwinDateTime; //8 bytes
//-- ---- ---- ---- ---- ---- Measured Data
typedef struct {
    int aValue;                     //value without decimal point position
    int aStatus;                    //DAQDARWN_DATA_xxx
    int aAlarm[DAQDARWIN_NUMALARM]; //DAQDARWN_ALARM_xxx
} DarwinDataInfo; //24 bytes
//-- ---- ---- ---- ---- ---- Channel Information
typedef struct {
    int  aChNo;   //1-60
    int  aPoint;  //0-4
    int  aStatus;
    int  aChType;
    char aUnit[(DAQDARWIN_MAXUNITLEN + 1)];
    char align;
} DarwinChInfo; //32 bytes
//-- ---- ---- ---- ---- ---- System
typedef struct {
    int  aSlotNo; //0-5
    int  aInternalCode;
    char aName[(DAQDARWIN_MAXMODULELEN + 1)];
    char align;
} DarwinModuleInfo; //16 bytes
typedef struct {
    int aExist;  //DAQDARWIN_VALID_xxx
    int aUnitNo; //DAQDARWIN_UNITNO_MAINUNIT, or 0-5
    DarwinModuleInfo aModule[DAQDARWIN_NUMSLOT];
} DarwinUnitInfo; //104 bytes
typedef struct {
    DarwinUnitInfo aMainUnit;
    DarwinUnitInfo aSubUnit[DAQDARWIN_NUMUNIT];
} DarwinSystemInfo; //728 bytes
///////////////////////////////////////////////////////////////////////
#ifdef __cplusplus
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// Date and Time from Measured Data
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
class DAQDARWIN_API CDAQDARWINDateTime : public CDAQDateTime
{
    //-- ---- ---- ---- ---- ---- Constructs
public:
    CDAQDARWINDateTime(DarwinDateTime * pDarwinDateTime = NULL);
    CDAQDARWINDateTime(int iYaer,
                       int iMonth,
                       int iDay,
                       int iHour = 0,
                       int iMinute = 0,
                       int iSecond = 0);
    virtual ~CDAQDARWINDateTime(void);

    //-- ---- ---- ---- ---- ---- Attributes
protected:
    DarwinDateTime m_DarwinDateTime;

  //-- ---- ---- ---- ---- ---- Overrides
public:
    virtual void initialize(void);
    virtual void setNow(void);
    // since R2.01
    virtual int isObject(const char * classname = "CDAQDARWINDateTime");

    //-- ---- ---- ---- ---- ---- Implements
public:
    // copy structure
    void getDarwinDateTime(DarwinDateTime * pDarwinDateTime);
    void setDarwinDateTime(DarwinDateTime * pDarwinDateTime);
    static void initDarwinDateTime(DarwinDateTime * pDarwinDateTime);
    // get
    int getYear(void);
    int getMonth(void);
    int getDay(void);
    int getHour(void);
    int getMinute(void);
    int getSecond(void);
    // block
    int setLine(const char * strLine,
                int          lenLine);
    int setByte(const unsigned char pByte[],
                int                 numByte);
    // YY/MM/DD,hh:mm:ss
    int toString(char * strDateTime,
                 int    lenDateTime);
    //R2.01
    int getFullYear(void); //CCYY

protected:
    // structure -> time_t : @see CDAQDateTime
    void toDateTime(void);

    //-- ---- ---- ---- ---- ---- Operations
public:
    CDAQDARWINDateTime & operator=(CDAQDARWINDateTime & cDARWINDateTime);

};
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// Channel Information
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
class DAQDARWIN_API CDAQDARWINChInfo : public CDAQChInfo
{
    //-- ---- ---- ---- ---- ---- Constructs
public:
    CDAQDARWINChInfo(DarwinChInfo * pDarwinChInfo = NULL);
    CDAQDARWINChInfo(int          chType,
                     int          chNo,
                     int          point,
                     const char * strUnit,
                     int          iStatus = DAQDARWIN_DATA_UNKNOWN);
    virtual ~CDAQDARWINChInfo(void);

    //-- ---- ---- ---- ---- ---- Attributes
protected:
    int m_chStatus; //DAQDARWIN_DATA_xxx
    char m_strUnit[(DAQDARWIN_MAXUNITLEN + 1)];

    //-- ---- ---- ---- ---- ---- Overrides
public:
    virtual void initialize(void);
    // since R2.01
    virtual int isObject(const char * classname = "CDAQDARWINChInfo");

    //-- ---- ---- ---- ---- ---- Implements
    // chType : channel type as DAQDARWIN_CHTYPE_xxx
    // chNo : channel number is 1 origin
    // point : decimal point position
public:
    // copy structure
    void getDarwinChInfo(DarwinChInfo * pDarwinChInfo);
    void setDarwinChInfo(DarwinChInfo * pDarwinChInfo);
    static void initDarwinChInfo(DarwinChInfo * pDarwinChInfo);
    // get
    int getChStatus(void); //DAQDARWIN_DATA_xxx
    const char * getUnit(void);
    // set
    void setChStatus(int iStatus); //DAQDARWIN_DATA_xxx
    void setUnit(const char * strUnit);
    int setLine(const char * strLine,
                int          lenLine,
                int *        pFlag);
    // utility
    int getChName(char * strName,
                  int    lenName);
    static int toChName(int    chType,
                        int    chNo,
                        char * strName,
                        int    lenName);
    static int toChRange(int    chType,
                         int    startChNo,
                         int    endChNo,
                         char * strName,
                         int    lenName);
    static const char * getStatusName(int iStatus);
    // convert
    static int toStatus(char cStatus); //1 character to DAQDARWIN_DATA_xxx
    static int toStatus(int value);    //2 byte value to DAQDARWIN_DATA_xxx
    static int toFlag(char cFlag);     //1 character to DAQDARWIN_FLAG_xxx
    static int toChType(char cType);   //1 character to DAQDARWIN_CHTYPE_xxx

    //-- ---- ---- ---- ---- ---- Operations
public:
    CDAQDARWINChInfo & operator=(CDAQDARWINChInfo & cDARWINChInfo);

};
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// Measured Data
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
class DAQDARWIN_API CDAQDARWINDataInfo : public CDAQDataInfo
{
    //-- ---- ---- ---- ---- ---- Constructs
public:
    CDAQDARWINDataInfo(DarwinDataInfo *   pDarwinDataInfo = NULL,
                       CDAQDARWINChInfo * pcDARWINChInfo = NULL);
    virtual ~CDAQDARWINDataInfo(void);

    //-- ---- ---- ---- ---- ---- Attributes
protected:
    int m_dataStatus;                //DAQDARWN_DATA_xxx
    int m_alarm[DAQDARWIN_NUMALARM]; //DAQDARWN_ALARM_xxx

    //-- ---- ---- ---- ---- ---- Overrides
public:
    virtual void initialize(void);
    // since R2.01
    virtual int isObject(const char * classname = "CDAQDARWINDataInfo");

    //-- ---- ---- ---- ---- ---- Implements
    // levelNo : level number is 1 origin
public:
    // copy structure
    void getDarwinDataInfo(DarwinDataInfo * pDarwinDataInfo);
    void setDarwinDataInfo(DarwinDataInfo * pDarwinDataInfo);
    static void initDarwinDataInfo(DarwinDataInfo * pDarwinDataInfo);
    // get
    int getStatus(void); //DAQDARWIN_DATA_xxx
    int getAlarm(int levelNo); //DAQDARWIN_ALARM_xxx
    // set
    void setStatus(int iDataStatus); //DAQDARWIN_DATA_xxx
    void setAlarm(int levelNo,
                  int iAlarmType); //DAQDARWIN_ALARM_xxx
    int setLine(const char * strLine,
                int          lenLine,
                int *        pFlag);
    int setByte(const unsigned char pByte[],
                int                 numByte);
    // class
    CDAQDARWINChInfo * getClassDARWINChInfo(void);
    void setClassDARWINChInfo(CDAQDARWINChInfo * pcDARWINChInfo);
    // utility
    static const char * getAlarmName(int iAlarmType);
    static int toAlarmType(const char * strAlarm);
    static int getMaxLenAlarmName(void); //R2.01

    //-- ---- ---- ---- ---- ---- Operations
public:
    CDAQDARWINDataInfo & operator=(CDAQDARWINDataInfo & cDARWINDataInfo);

};
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// System Configuration Data
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
class DAQDARWIN_API CDAQDARWINSysInfo
{
    //-- ---- ---- ---- ---- ---- Constructs
public:
    CDAQDARWINSysInfo(double             interval = 0.0,
                      DarwinSystemInfo * pDarwinSystemInfo = NULL);
    virtual ~CDAQDARWINSysInfo(void);

    //-- ---- ---- ---- ---- ---- Attributes
protected:
    // system config
    double m_nInterval; //seconds
    DarwinSystemInfo m_DarwinSystemInfo;

    //-- ---- ---- ---- ---- ---- Overrides

    //-- ---- ---- ---- ---- ---- Implements
    // unitNo : unit number is 0 origin, and DAQDARWIN_UNITNO_MAINUNIT
    // slotNo : slot number is 0 origin
public:
    virtual void initialize(void);
    // copy structure
    void getDarwinSystemInfo(DarwinSystemInfo * pDarwinSystemInfo);
    void setDarwinSystemInfo(DarwinSystemInfo * pDarwinSystemInfo);
    static void initDarwinSystemInfo(DarwinSystemInfo * pDarwinSystemInfo);
    // get
    double getInterval(void);
    int isExist(int unitNo);
    const char * getModuleName(int unitNo,
                               int slotNo);
    // set
    int setLine(const char * strLine,
                int          lenLine,
                int *        pFlag);
    // utility for relay
    static int toRelayName(int    relayType,
                           int    relayNo,
                           char * strName,
                           int    lenName);
    // since R2.01
    int getModuleCode(int unitNo,
                      int slotNo);
    virtual int isObject(const char * classname = "CDAQDARWINSysInfo");

protected:
    // access structure directly
    DarwinUnitInfo * getDarwinUnitInfo(int unitNo);
    DarwinModuleInfo * getDarwinModuleInfo(int unitNo,
                                           int slotNo);

    //-- ---- ---- ---- ---- ---- Operations
public:
    CDAQDARWINSysInfo & operator=(CDAQDARWINSysInfo & cDARWINSysInfo);

};
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// Based Connection Handler
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
class DAQDARWIN_API CDAQDARWIN : public CDAQHandler
{
    //-- ---- ---- ---- ---- ---- Constructs
public:
    CDAQDARWIN(void);
    CDAQDARWIN(const char * strAddress,
               unsigned int uiPort = DAQDARWIN_COMMPORT,
               int *        errCode = NULL);
    virtual ~CDAQDARWIN(void);

    //-- ---- ---- ---- ---- ---- Attributes

    //-- ---- ---- ---- ---- ---- Overrides
public:
    // low level communication
    virtual int open(const char * strAddress,
                     unsigned int uiPort = DAQDARWIN_COMMPORT);
    // acquisition
    virtual int getData(int            chType,
                        int            chNo,
                        CDAQDateTime & cDateTime,
                        CDAQDataInfo & cDataInfo);
    virtual int getChannel(int chType,
                           int chNo,
                           CDAQChInfo & cChInfo);
    // since R2.01
    virtual int isObject(const char * classname = "CDAQDARWIN");

    //-- ---- ---- ---- ---- ---- Implements
    // chType : channel type
    // chNo : channel number is 1 origin
    // levelNo : level number is 1 origin
    // relayType : relay type @see channel type
    // relayNo : relay number is 1 origin. If 0, NONE.
public:
    // send and receive
    virtual int runCommand(const char * strCmd);
    // status
    virtual int getStatusByte(int * pStatusByte);
    // action
    virtual int sendTrigger(void);
    // date time : NULL is Now
    virtual int setDateTime(CDAQDARWINDateTime * pcDARWINDateTime = NULL);
    // control commands
    int transMode(int iMode);
    int initSystem(int iCtrl);
    // system
    int getSystemConfig(CDAQDARWINSysInfo & cDARWINSysInfo);
    // channel information
    int talkChInfo(int startChType,
                   int startChNo,
                   int endChType,
                   int endChNo);
    int getChInfo(CDAQDARWINChInfo & cDARWINChInfo,
                  int *              pFlag);
    // measured data as ASCII
    int talkDataByASCII(int                  startChType,
                        int                  startChNo,
                        int                  endChType,
                        int                  endChNo,
                        CDAQDARWINDateTime & cDARWINDateTime);
    int getChDataByASCII(CDAQDARWINDataInfo & cDARWINDataInfo,
                         int *                pFlag);
    // measured data as binary
    int talkDataByBinary(int                  startChType,
                         int                  startChNo,
                         int                  endChType,
                         int                  endChNo,
                         CDAQDARWINDateTime & cDARWINDateTime);
    int getChDataByBinary(CDAQDARWINDataInfo & cDARWINDataInfo,
                          int *                pFlag);
    // configuration by mode
    int talkOperationData(int startChType,
                          int startChNo,
                          int endChType,
                          int endChNo);
    int talkSetupData(int startChType,
                      int startChNo,
                      int endChType,
                      int endChNo);
    int talkCalibrationData(int startChType,
                            int startChNo,
                            int endChType,
                            int endChNo);
    int getSetDataByLine(char * strLine,
                         int    maxLine,
                         int *  lenLine,
                         int *  pFlag);
    // range
    int setSKIP(int chType,
                int startChNo,
                int endChNo = 0);
    int setVOLT(int iRangeVOLT, //DAQDARWIN_RANGE_VOLT_xxx
                int chType,
                int startChNo,
                int endChNo = 0,
                int spanMin = 0,
                int spanMax = 0,
                int scaleMin = 0,
                int scaleMax = 0,
                int scalePoint = 0);
    int setTC(int iRangeTC, //DAQDARWIN_RANGE_TC_xxx
              int chType,
              int startChNo,
              int endChNo = 0,
              int spanMin = 0,
              int spanMax = 0,
              int scaleMin = 0,
              int scaleMax = 0,
              int scalePoint = 0);
    int setRTD(int iRangeRTD, //DAQDARWIN_RANGE_RTD_xxx
               int chType,
               int startChNo,
               int endChNo = 0,
               int spanMin = 0,
               int spanMax = 0,
               int scaleMin = 0,
               int scaleMax = 0,
               int scalePoint = 0);
    int setDI(int iRangeDI, //DAQDARWIN_RANGE_DI_xxx
              int chType,
              int startChNo,
              int endChNo = 0,
              int spanMin = 0,
              int spanMax = 0,
              int scaleMin = 0,
              int scaleMax = 0,
              int scalePoint = 0);
    int setDELTA(int refChNo,
                 int chType,
                 int startChNo,
                 int endChNo = 0,
                 int spanMin = 0,
                 int spanMax = 0);
    int setRRJC(int refChNo,
                int chType,
                int startChNo,
                int endChNo = 0,
                int spanMin = 0,
                int spanMax = 0);
    // scalling
    int setScallingUnit(const char * strUnit,
                        int          chType,
                        int          startChNo,
                        int          endChNo = 0);
    // alarm
    int setAlarm(int levelNo,
                 int chType,
                 int startChNo,
                 int endChNo = 0,
                 int iAlarmType = DAQDARWIN_ALARM_NONE,
                 int value = 0,
                 int relayType = 0,
                 int relayNo = 0);
    // since R2.01
    int setMA(int iRangeMA, //DAQDARWIN_RANGE_MA_xxx
              int chType,
              int startChNo,
              int endChNo = 0,
              int spanMin = 0,
              int spanMax = 0,
              int scaleMin = 0,
              int scaleMax = 0,
              int scalePoint = 0);
    int setSTRAIN(int iRangeSTRAIN, //DAQDARWIN_RANGE_STRAIN_xxx
                  int chType,
                  int startChNo,
                  int endChNo = 0,
                  int spanMin = 0,
                  int spanMax = 0,
                  int scaleMin = 0,
                  int scaleMax = 0,
                  int scalePoint = 0);
    int setPULSE(int iRangePULSE, //DAQDARWIN_RANGE_PULSE_xxx
                 int chType,
                 int startChNo,
                 int endChNo = 0,
                 int spanMin = 0,
                 int spanMax = 0,
                 int scaleMin = 0,
                 int scaleMax = 0,
                 int scalePoint = 0,
                 int bFilter = DAQDARWIN_VALID_OFF);
    int setPOWER(int iRangePOWER, //DAQDARWIN_RANGE_POWER_xxx
                 int chType,
                 int chNo,
                 int iItem = DAQDARWIN_POWERITEM_P1,
                 int iWire = DAQDARWIN_WIRE_1PH2W,
                 int spanMin = 0,
                 int spanMax = 0,
                 int scaleMin = 0,
                 int scaleMax = 0,
                 int scalePoint = 0);
    int establish(int iSetup = DAQDARWIN_SETUP_ABORT);
    int compute(int iCompute); //DAQDARWIN_COMPUTE_xxx
    int reporting(int iReportRun); //DAQDARWIN_REPORT_RUN_xxx
    int getReportStatus(int * pReportStatus);
    virtual int receiveByte(unsigned char * byteData,
                            int             maxData = 1,
                            int *           lenData = NULL);

protected:
    // action
    virtual int startTalker(int iTalk); //DAQDARWIN_TALK_xxx
    // check
    int checkAck(const char * strAck,
                 int          lenAck);
    // DLL version
    static const int getVersionDLL(void);
    static const int getRevisionDLL(void);

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
 * @param errorCode stored an error code if error occured.
 * @return an instrument descriptor.
 */
DAQDARWIN_API DAQDARWIN APIENTRY openDARWIN(const char * strAddress,
                                            int *        errorCode);
#define OPEN_DARWIN(address) openDARWIN(address, NULL)
/**
 * This closes the instrument.
 * @param daqdarwin specified an instrument descriptor.
 * @return an error code that is not zero if error ocuured.
 */
DAQDARWIN_API int APIENTRY closeDARWIN(DAQDARWIN daqdarwin);
/**
 * This sends data as string at terminate.
 * @param daqdarwin specified an instrument descriptor.
 * @param strLine specified a buffer for sending without terminate.
 * @return an error code that is not zero if error ocuured.
 */
DAQDARWIN_API int APIENTRY sendLineDARWIN(DAQDARWIN daqdarwin,
                                          const char * strLine);
/**
 * This receives data as string at terminate.
 * @param daqdarwin specified an instrument descriptor.
 * @param strLine stored a buffer of the received data.
 * @param maxLine specified a max length of the data (strLine).
 * @param lenLine stored a real length of the received data.
 * @return an error code that is not zero if error ocuured.
 */
DAQDARWIN_API int APIENTRY receiveLineDARWIN(DAQDARWIN daqdarwin,
                                             char *    strLine,
                                             int       maxLine,
                                             int *     lenLine);
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// Middle level communications
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
/**
 * This runs a command as string, then receives and checks the ack. 
 * @param daqdarwin specified an instrument descriptor.
 * @param strCmd specified a command as string without a terminate.
 * @return an error code that is not zero if error ocuured.
 */
DAQDARWIN_API int APIENTRY runCommandDARWIN(DAQDARWIN    daqdarwin,
                                            const char * strCmd);
/**
 * This gets a status byte.
 * @param daqdarwin specified an instrument descriptor.
 * @param pStatusByte stroed a value of the status byte.
 * @return an error code that is not zero if error ocuured.
 */
DAQDARWIN_API int APIENTRY getStatusByteDARWIN(DAQDARWIN daqdarwin,
                                               int *     pStatusByte);
/**
 * This sends a trigger.
 * @param daqdarwin specified an instrument descriptor.
 * @return an error code that is not zero if error ocuured.
 */
DAQDARWIN_API int APIENTRY sendTriggerDARWIN(DAQDARWIN daqdarwin);
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// Date time commands
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
/**
 * This sets date and time.
 * @param daqdarwin specified an instrument descriptor.
 * @param pDarwinDateTime specified a structure of date and time. If NULL, now.
 * @return an error code that is not zero if error ocuured.
 * @see SD command.
 */
DAQDARWIN_API int APIENTRY setDateTimeDARWIN(DAQDARWIN daqdarwin,
                                             DarwinDateTime * pDarwinDateTime);
/**
 * This sets date and time, now.
 * @param daqdarwin specified an instrument descriptor.
 * @return an error code that is not zero if error ocuured.
 * @see setDateTimeDARWIN
 */
DAQDARWIN_API int APIENTRY setDateTimeNowDARWIN(DAQDARWIN daqdarwin);
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// Control commands
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
/**
 * This transfers the setting mode.
 * @param daqdarwin specified an instrument descriptor.
 * @param iMode specified a selection of mode as DAQDARWIN_MODE_xxx.
 * @return an error code that is not zero if error ocuured.
 * @see DS command.
 */
DAQDARWIN_API int APIENTRY transModeDARWIN(DAQDARWIN daqdarwin,
                                           int       iMode);
/**
 * This initializes a system.
 * @param daqdarwin specified an instrument descriptor.
 * @param iCtrl specified a selection of control as DAQDARWIN_SYSTEM_xxx.
 * @return an error code that is not zero if error ocuured.
 * @see RS, RC, AR command.
 */
DAQDARWIN_API int APIENTRY initSystemDARWIN(DAQDARWIN daqdarwin,
                                            int       iCtrl);
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// Get system
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
/**
 * This gets a system configuration data.
 * @param daqdarwin specified an instrument descriptor.
 * @param interval stored an interval time by seconds.
 * @param pDarwinSystemInfo stored a system configuration data.
 * @return an error code that is not zero if error ocuured.
 */
DAQDARWIN_API int APIENTRY getSystemConfigDARWIN(DAQDARWIN          daqdarwin,
                                                 double *           interval,
                                                 DarwinSystemInfo * pDarwinSystemInfo);
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// Get channel information
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
/**
 * This sends a talker of getting channel informations.
 * @param daqdarwin specified an instrument descriptor.
 * @param startChType specified a start channel type.
 * @param startChNo specified a start channel number.
 * @param endChType specified an end channel type.
 * @param endChNo specified an end channel number.
 * @return an error code that is not zero if error ocuured.
 */
DAQDARWIN_API int APIENTRY talkChInfoDARWIN(DAQDARWIN daqdarwin,
                                            int       startChType,
                                            int       startChNo,
                                            int       endChType,
                                            int       endChNo);
/**
 * This gets a channel information by line.
 * @param daqdarwin specified an instrument descriptor.
 * @param pDarwinChInfo stored a channel information data.
 * @param pFlag stored a flag as DAQDARWIN_FLAG_xxx.
 * @return an error code that is not zero if error ocuured.
 */
DAQDARWIN_API int APIENTRY getChInfoDARWIN(DAQDARWIN      daqdarwin,
                                           DarwinChInfo * pDarwinChInfo,
                                           int *          pFlag);
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// Get measured data as ASCII
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
/**
 * This sends a talker of getting data as ASCII.
 * @param daqdarwin specified an instrument descriptor.
 * @param startChType specified a start channel type.
 * @param startChNo specified a start channel number.
 * @param endChType specified an end channel type.
 * @param endChNo specified an end channel number.
 * @param pDarwinDateTime stored date and time.
 * @return an error code that is not zero if error ocuured.
 */
DAQDARWIN_API int APIENTRY talkDataByASCIIDARWIN(DAQDARWIN        daqdarwin,
                                                 int              startChType,
                                                 int              startChNo,
                                                 int              endChType,
                                                 int              endChNo,
                                                 DarwinDateTime * pDarwinDateTime);
/**
 * This gets a measured data as ASCII formating by each channel.
 * @param daqdarwin specified an instrument descriptor.
 * @param pDarwinChInfo stored a channel information.
 * @param pDarwinDataInfo stored a measured data.
 * @param pFlag stored a flag as DAQDARWIN_FLAG_xxx.
 * @return an error code that is not zero if error ocuured.
 */
DAQDARWIN_API int APIENTRY getChDataByASCIIDARWIN(DAQDARWIN        daqdarwin,
                                                  DarwinChInfo *   pDarwinChInfo,
                                                  DarwinDataInfo * pDarwinDataInfo,
                                                  int *            pFlag);
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// Get measured data as binary
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
/**
 * This sends a talker of getting data as binary.
 * @param daqdarwin specified an instrument descriptor.
 * @param startChType specified a start channel type.
 * @param startChNo specified a start channel number.
 * @param endChType specified an end channel type.
 * @param endChNo specified an end channel number.
 * @param pDarwinDateTime stored date and time.
 * @return an error code that is not zero if error ocuured.
 */
DAQDARWIN_API int APIENTRY talkDataByBinaryDARWIN(DAQDARWIN        daqdarwin,
                                                  int              startChType,
                                                  int              startChNo,
                                                  int              endChType,
                                                  int              endChNo,
                                                  DarwinDateTime * pDarwinDateTime);
/**
 * This gets a measured data that binary formating by channel.
 * @param daqdarwin specified an instrument descriptor.
 * @param pDarwinChInfo stored a channel information.
 * @param pDarwinDataInfo stored a measured data.
 * @param pFlag stored a flag as DAQDARWIN_FLAG_xxx.
 * @return an error code that is not zero if error ocuured.
 */
DAQDARWIN_API int APIENTRY getChDataByBinaryDARWIN(DAQDARWIN        daqdarwin,
                                                   DarwinChInfo *   pDarwinChInfo,
                                                   DarwinDataInfo * pDarwinDataInfo,
                                                   int *            pFlag);
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// Get configuration data by mode
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
/**
 * This sends a talker of getting the setting data on operation mode.
 * @param daqdarwin specified an instrument descriptor.
 * @param startChType specified a start channel type.
 * @param startChNo specified a start channel number.
 * @param endChType specified an end channel type.
 * @param endChNo specified an end channel number.
 * @return an error code that is not zero if error ocuured.
 */
DAQDARWIN_API int APIENTRY talkOperationDataDARWIN(DAQDARWIN daqdarwin,
                                                   int       startChType,
                                                   int       startChNo,
                                                   int       endChType,
                                                   int       endChNo);
/**
 * This sends a talker of getting the setting data on setup mode.
 * @param daqdarwin specified an instrument descriptor.
 * @param startChType specified a start channel type.
 * @param startChNo specified a start channel number.
 * @param endChType specified an end channel type.
 * @param endChNo specified an end channel number.
 * @return an error code that is not zero if error ocuured.
 */
DAQDARWIN_API int APIENTRY talkSetupDataDARWIN(DAQDARWIN daqdarwin,
                                               int       startChType,
                                               int       startChNo,
                                               int       endChType,
                                               int       endChNo);
/**
 * This sends a talker of getting the setting data on calibration mode.
 * @param daqdarwin specified an instrument descriptor.
 * @param startChType specified a start channel type.
 * @param startChNo specified a start channel number.
 * @param endChType specified an end channel type.
 * @param endChNo specified an end channel number.
 * @return an error code that is not zero if error ocuured.
 */
DAQDARWIN_API int APIENTRY talkCalibrationDataDARWIN(DAQDARWIN daqdarwin,
                                                     int       startChType,
                                                     int       startChNo,
                                                     int       endChType,
                                                     int       endChNo);
/**
 * This gets a setting data by line.
 * @param daqdarwin specified an instrument descriptor.
 * @param strLine stored a buffer of the received data.
 * @param maxLine specified a max length of the data (strLine).
 * @param lenLine stored a real length of the received data.
 * @param pFlag stored a flag as DAQDARWIN_FLAG_xxx.
 * @return an error code that is not zero if error ocuured.
 */
DAQDARWIN_API int APIENTRY getSetDataByLineDARWIN(DAQDARWIN daqdarwin,
                                                  char *    strLine,
                                                  int       maxLine,
                                                  int *     lenLine,
                                                  int *     pFlag);
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// Set range
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
/**
 * This sets the range to skip.
 * @param daqdarwin specified an instrument descriptor.
 * @param chType specified a channel type.
 * @param startChNo specified a start channel number.
 * @param endChNo specified an end channel number.
 * @return an error code that is not zero if error ocuured.
 */
DAQDARWIN_API int APIENTRY setSKIPDARWIN(DAQDARWIN daqdarwin,
                                         int       chType,
                                         int       startChNo,
                                         int       endChNo);
/**
 * This sets voltage.
 * If min equals max, span or scale are omitted.
 * @param daqdarwin specified an instrument descriptor.
 * @param chType specified a channel type.
 * @param iRangeVOLT a selection of the range as DAQDARWIN_RANGE_VOLT_xxx.
 * @param startChNo specified a start channel number.
 * @param endChNo specified an end channel number.
 * @param spanMin specified a value of the minimum span.
 * @param spanMax specified a value of the maximum span.
 * @param scaleMin specified a value of the minimum scale.
 * @param scaleMax specified a value of the maximum scale.
 * @param scalePoint specified a value of the decimal point position.
 * @return an error code that is not zero if error ocuured.
 */
DAQDARWIN_API int APIENTRY setVOLTDARWIN(DAQDARWIN daqdarwin,
                                         int       iRangeVOLT,
                                         int       chType,
                                         int       startChNo,
                                         int       endChNo,
                                         int       spanMin,
                                         int       spanMax,
                                         int       scaleMin,
                                         int       scaleMax,
                                         int       scalePoint);
/**
 * This sets thermocouple.
 * If min equals max, span or scale are omitted.
 * @param daqdarwin specified an instrument descriptor.
 * @param chType specified a channel type.
 * @param iRangeTC a selection of the range as DAQDARWIN_RANGE_TC_xxx.
 * @param startChNo specified a start channel number.
 * @param endChNo specified an end channel number.
 * @param spanMin specified a value of the minimum span.
 * @param spanMax specified a value of the maximum span.
 * @param scaleMin specified a value of the minimum scale.
 * @param scaleMax specified a value of the maximum scale.
 * @param scalePoint specified a value of the decimal point position.
 * @return an error code that is not zero if error ocuured.
 */
DAQDARWIN_API int APIENTRY setTCDARWIN(DAQDARWIN daqdarwin,
                                       int       iRangeTC,
                                       int       chType,
                                       int       startChNo,
                                       int       endChNo,
                                       int       spanMin,
                                       int       spanMax,
                                       int       scaleMin,
                                       int       scaleMax,
                                       int       scalePoint);
/**
 * This sets RTD (resistance Temperature Detector).
 * If min equals max, span or scale are omitted.
 * @param daqdarwin specified an instrument descriptor.
 * @param chType specified a channel type.
 * @param iRangeRTD a selection of the range as DAQDARWIN_RANGE_RTD_xxx.
 * @param startChNo specified a start channel number.
 * @param endChNo specified an end channel number.
 * @param spanMin specified a value of the minimum span.
 * @param spanMax specified a value of the maximum span.
 * @param scaleMin specified a value of the minimum scale.
 * @param scaleMax specified a value of the maximum scale.
 * @param scalePoint specified a value of the decimal point position.
 * @return an error code that is not zero if error ocuured.
 */
DAQDARWIN_API int APIENTRY setRTDDARWIN(DAQDARWIN daqdarwin,
                                        int       iRangeRTD,
                                        int       chType,
                                        int       startChNo,
                                        int       endChNo,
                                        int       spanMin,
                                        int       spanMax,
                                        int       scaleMin,
                                        int       scaleMax,
                                        int       scalePoint);
/**
 * This sets DI (contact input).
 * If min equals max, span or scale are omitted.
 * @param daqdarwin specified an instrument descriptor.
 * @param chType specified a channel type.
 * @param iRangeDI a selection of the range as DAQDARWIN_RANGE_DI_xxx.
 * @param startChNo specified a start channel number.
 * @param endChNo specified an end channel number.
 * @param spanMin specified a value of the minimum span.
 * @param spanMax specified a value of the maximum span.
 * @param scaleMin specified a value of the minimum scale.
 * @param scaleMax specified a value of the maximum scale.
 * @param scalePoint specified a value of the decimal point position.
 * @return an error code that is not zero if error ocuured.
 */
DAQDARWIN_API int APIENTRY setDIDARWIN(DAQDARWIN daqdarwin,
                                       int       iRangeDI,
                                       int       chType,
                                       int       startChNo,
                                       int       endChNo,
                                       int       spanMin,
                                       int       spanMax,
                                       int       scaleMin,
                                       int       scaleMax,
                                       int       scalePoint);
/**
 * This sets DELTA (difference between channels).
 * If min equals max, span is omitted.
 * @param daqdarwin specified an instrument descriptor.
 * @param refChNo specified a reference channel number.
 * @param chType specified a channel type.
 * @param startChNo specified a start channel number.
 * @param endChNo specified an end channel number.
 * @param spanMin specified a value of the minimum span.
 * @param spanMax specified a value of the maximum span.
 * @return an error code that is not zero if error ocuured.
 */
DAQDARWIN_API int APIENTRY setDELTADARWIN(DAQDARWIN daqdarwin,
                                          int       refChNo,
                                          int       chType,
                                          int       startChNo,
                                          int       endChNo,
                                          int       spanMin,
                                          int       spanMax);
/**
 * This sets RRJC (remote RJC).
 * If min equals max, span is omitted.
 * @param daqdarwin specified an instrument descriptor.
 * @param refChNo specified a reference channel number.
 * @param chType specified a channel type.
 * @param startChNo specified a start channel number.
 * @param endChNo specified an end channel number.
 * @param spanMin specified a value of the minimum span.
 * @param spanMax specified a value of the maximum span.
 * @return an error code that is not zero if error ocuured.
 */
DAQDARWIN_API int APIENTRY setRRJCDARWIN(DAQDARWIN daqdarwin,
                                         int       refChNo,
                                         int       chType,
                                         int       startChNo,
                                         int       endChNo,
                                         int       spanMin,
                                         int       spanMax);
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// Scalling
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
/**
 * This sets an unit name.
 * @param daqdarwin specified an instrument descriptor.
 * @param strUnit specified a string of the unit name.
 * @param chType specified a channel type.
 * @param startChNo specified a start channel number.
 * @param endChNo specified an end channel number.
 * @return an error code that is not zero if error ocuured.
 * @see SN commnd.
 */
DAQDARWIN_API int APIENTRY setScallingUnitDARWIN(DAQDARWIN    daqdarwin,
                                                 const char * strUnit,
                                                 int          chType,
                                                 int          startChNo,
                                                 int          endChNo);
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// Alarm
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
/**
 * This sets the alarm.
 * @param daqdarwin specified an instrument descriptor.
 * @param levelNo specified a level number.
 * @param chType specified a channel type.
 * @param startChNo specified a start channel number.
 * @param endChNo specified an end channel number.
 * @param iAlarmType specified a selection of the alarm type as DAQDARWIN_ALARM_xxx.
 * @param value specified a value of an alarm ON without decimal point position.
 * @param relayType specified a relay type.
 * @param relayNo specified a relay number.
 * @return an error code that is not zero if error ocuured.
 * @see AS command.
 */
DAQDARWIN_API int APIENTRY setAlarmDARWIN(DAQDARWIN daqdarwin,
                                          int       levelNo,
                                          int       chType,
                                          int       startChNo,
                                          int       endChNo,
                                          int       iAlarmType,
                                          int       value,
                                          int       relayType,
                                          int       relayNo);
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// Utilities
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
/**
 * This changes a measured data and decimal point position to a value as double.
 * @param dataValue specified a measured data as integer (Long).
 * @param point specified a decimal point position.
 * @return a value as double.
 */
DAQDARWIN_API double APIENTRY toDoubleValueDARWIN(int dataValue,
                                                  int point);
/**
 * This changes a measured data and decimal point position to a value as string.
 * @param dataValue specified a measured data as integer (Long).
 * @param point specified a decimal point position.
 * @param strValue stored a string.
 * @param lenValue specified  a length of the buffer (strValue).
 * @return a length of the string by bytes without NULL.
 */
DAQDARWIN_API int APIENTRY toStringValueDARWIN(int    dataValue,
                                               int    point,
                                               char * strValue,
                                               int    lenValue);
/**
 * This changes an alarm type to a mnemonic as string.
 * @param iAlarmType specified a selection of the alarm type as DAQDARWIN_ALARM_xxx.
 * @param strAlarm stored a string.
 * @param lenAlarm specified a length of the buffer (strAlarm).
 * @return a length of the string by bytes without NULL.
 */
DAQDARWIN_API int APIENTRY toAlarmNameDARWIN(int    iAlarmType,
                                             char * strAlarm,
                                             int    lenAlarm);
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// Messages
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
/**
 * This gets a version of API.
 * @return a number as integer.
 */
DAQDARWIN_API const int APIENTRY getVersionAPIDARWIN(void);
/**
 * This gets a revision of API.
 * @return a number as integer.
 */
DAQDARWIN_API const int APIENTRY getRevisionAPIDARWIN(void);
/**
 * This changes an error code to a string.
 * @param errCode specified an error code that returned by each function.
 * @param errStr stored a string of the error message.
 * @param errLen specified a length of the buffer (errStr).
 * @return a length of the error message by bytes without NULL.
 */
DAQDARWIN_API int APIENTRY toErrorMessageDARWIN(int    errCode,
                                                char * errStr,
                                                int    errLen);
/**
 * This gets a maximum length of error messages.
 * @return a length by bytes without NULL.
 */
DAQDARWIN_API int APIENTRY getMaxLenErrorMessageDARWIN(void);
/**
 * This gets a string of the error code.
 * If Visual Basic, Use toErrorStringDARWIN.
 * @param errCode specified an error code that returned by each function.
 * @return a string of the error message.
 */
DAQDARWIN_API const char * APIENTRY getErrorMessageDARWIN(int errCode);
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// Deprecated command
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
/**
 * This sets timeout.
 * If seconds is minus, timeout off.
 * @param daqdarwin specified an instrument descriptor.
 * @param seconds specified a value of time out by second.
 * @return an error code that is not zero if error ocuured.
 * @deprecated since Ver.1 Rev.0
 */
DAQDARWIN_API int APIENTRY setTimeOutDARWIN(DAQDARWIN daqdarwin,
                                            int       seconds);
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// Since R2.01
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
/**
 * This sets mA.
 * If min equals max, span or scale are omitted.
 * @param daqdarwin specified an instrument descriptor.
 * @param chType specified a channel type.
 * @param iRangeMA a selection of the range as DAQDARWIN_RANGE_MA_xxx.
 * @param startChNo specified a start channel number.
 * @param endChNo specified an end channel number.
 * @param spanMin specified a value of the minimum span.
 * @param spanMax specified a value of the maximum span.
 * @param scaleMin specified a value of the minimum scale.
 * @param scaleMax specified a value of the maximum scale.
 * @param scalePoint specified a value of the decimal point position.
 * @return an error code that is not zero if error ocuured.
 */
DAQDARWIN_API int APIENTRY setMADARWIN(DAQDARWIN daqdarwin,
                                       int       iRangeMA,
                                       int       chType,
                                       int       startChNo,
                                       int       endChNo,
                                       int       spanMin,
                                       int       spanMax,
                                       int       scaleMin,
                                       int       scaleMax,
                                       int       scalePoint);
/**
 * This sets strain.
 * If min equals max, span or scale are omitted.
 * @param daqdarwin specified an instrument descriptor.
 * @param chType specified a channel type.
 * @param iRangeSTRAIN a selection of the range as DAQDARWIN_RANGE_STRAIN_xxx.
 * @param startChNo specified a start channel number.
 * @param endChNo specified an end channel number.
 * @param spanMin specified a value of the minimum span.
 * @param spanMax specified a value of the maximum span.
 * @param scaleMin specified a value of the minimum scale.
 * @param scaleMax specified a value of the maximum scale.
 * @param scalePoint specified a value of the decimal point position.
 * @return an error code that is not zero if error ocuured.
 */
DAQDARWIN_API int APIENTRY setSTRAINDARWIN(DAQDARWIN daqdarwin,
                                           int       iRangeSTRAIN,
                                           int       chType,
                                           int       startChNo,
                                           int       endChNo,
                                           int       spanMin,
                                           int       spanMax,
                                           int       scaleMin,
                                           int       scaleMax,
                                           int       scalePoint);
/**
 * This sets pulse.
 * If min equals max, span or scale are omitted.
 * @param daqdarwin specified an instrument descriptor.
 * @param iRangePULSE a selection of the range as DAQDARWIN_RANGE_STRAIN_xxx.
 * @param chType specified a channel type.
 * @param startChNo specified a start channel number.
 * @param endChNo specified an end channel number.
 * @param spanMin specified a value of the minimum span.
 * @param spanMax specified a value of the maximum span.
 * @param scaleMin specified a value of the minimum scale.
 * @param scaleMax specified a value of the maximum scale.
 * @param scalePoint specified a value of the decimal point position.
 * @param bFilter specified a filter as boolean.
 * @return an error code that is not zero if error ocuured.
 */
DAQDARWIN_API int APIENTRY setPULSEDARWIN(DAQDARWIN daqdarwin,
                                          int       iRangePULSE,
                                          int       chType,
                                          int       startChNo,
                                          int       endChNo,
                                          int       spanMin,
                                          int       spanMax,
                                          int       scaleMin,
                                          int       scaleMax,
                                          int       scalePoint,
                                          int       bFilter);
/**
 * This sets power.
 * If min equals max, span or scale are omitted.
 * @param daqdarwin specified an instrument descriptor.
 * @param iRangePOWER a selection of the range as DAQDARWIN_RANGE_POWER_xxx.
 * @param chType specified a channel type.
 * @param chNo specified a channel number.
 * @param iItem specified a power item as DAQDARWIN_POWERITEM_xxx.
 * @param iWire specified a wiring method as DAQDARWIN_WIRE_xxx.
 * @param spanMin specified a value of the minimum span.
 * @param spanMax specified a value of the maximum span.
 * @param scaleMin specified a value of the minimum scale.
 * @param scaleMax specified a value of the maximum scale.
 * @param scalePoint specified a value of the decimal point position.
 * @return an error code that is not zero if error ocuured.
 */
DAQDARWIN_API int APIENTRY setPOWERDARWIN(DAQDARWIN daqdarwin,
                                          int       iRangePOWER,
                                          int       chType,
                                          int       chNo,
                                          int       iItem,
                                          int       iWire,
                                          int       spanMin,
                                          int       spanMax,
                                          int       scaleMin,
                                          int       scaleMax,
                                          int       scalePoint);
/**
 * This establishes the contents of the setup mode setting.
 * @param daqdarwin specified an instrument descriptor.
 * @param iSetup specified a selection as DAQDARWIN_SETUP_xxx.
 * @return an error code that is not zero if error ocuured.
 */
DAQDARWIN_API int APIENTRY establishDARWIN(DAQDARWIN daqdarwin,
                                           int       iSetup);
/**
 * This controls computation.
 * @param daqdarwin specified an instrument descriptor.
 * @param iCompute specified a selection as DAQDARWIN_COMPUTE_xxx.
 * @return an error code that is not zero if error ocuured.
 */
DAQDARWIN_API int APIENTRY computeDARWIN(DAQDARWIN daqdarwin,
                                         int       iCompute);
/**
 * This runs a report.
 * @param daqdarwin specified an instrument descriptor.
 * @param iReportRun specified start/stop as DAQDARWIN_REPORT_RUN_xxx.
 * @return an error code that is not zero if error ocuured.
 */
DAQDARWIN_API int APIENTRY reportingDARWIN(DAQDARWIN daqdarwin,
                                           int       iReportRun);
/**
 * This gets a report status.
 * @param daqdarwin specified an instrument descriptor.
 * @param pReportStatus stored a report status as DAQDARWIN_REPSTATUS_xxx.
 * @return an error code that is not zero if error ocuured.
 */
DAQDARWIN_API int APIENTRY getReportStatusDARWIN(DAQDARWIN daqdarwin,
                                                 int *     pReportStatus);
/**
 * This receives a data as byte.
 * @param daqdarwin specified an instrument descriptor.
 * @param byteData stored a data.
 * @param maxData specified a length of the data (byteData).
 * @param lenData stored a length of a recieved data.
 * @return an error code that is not zero if error ocuured.
 */
DAQDARWIN_API int APIENTRY receiveByteDARWIN(DAQDARWIN       daqdarwin,
                                             unsigned char * byteData,
                                             int             maxData,
                                             int *           lenData);
/**
 * This gets an alarm type to a mnemonic as string.
 * @param iAlarmType specified a selection of the alarm type as DAQDARWIN_ALARM_xxx.
 * @return a string of the alarm type.
 */
DAQDARWIN_API const char * APIENTRY getAlarmNameDARWIN(int iAlarmType);
/**
 * This gets a maximum length of alarm names.
 * @return a length by bytes without NULL.
 */
DAQDARWIN_API int APIENTRY getMaxLenAlarmNameDARWIN(void);
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
#ifdef __cplusplus
}
#endif
///////////////////////////////////////////////////////////////////////
#if defined(WIN32) || defined(_WIN32_WCE)
/**
 * This defines types as load library callback for MS-Windows.
 * The sample is as follows :
 * <PRE>
 * <CODE>
 * HMODULE pDll = LoadLibrary("DAQDARWIN");
 * DLLOPENDARWIN openDARWIN = (DLLOPENDARWIN)GetProcAddress(pDll, "openDARWIN");
 * </CODE>
 * </PRE>
 */
// Low level communications
typedef DAQDARWIN (CALLBACK* DLLOPENDARWIN)(const char *, int *);
typedef int       (CALLBACK* DLLCLOSEDARWIN)(DAQDARWIN);
typedef int       (CALLBACK* DLLSENDLINEDARWIN)(DAQDARWIN, const char *);
typedef int       (CALLBACK* DLLRECEIVELINEDARWIN)(DAQDARWIN, char *, int, int *);
// Middle level communications
typedef int       (CALLBACK* DLLRUNCOMMANDDARWIN)(DAQDARWIN, const char *);
typedef int       (CALLBACK* DLLGETSTATUSBYTEDARWIN)(DAQDARWIN, int *);
typedef int       (CALLBACK* DLLSENDTRIGGERDARWIN)(DAQDARWIN);
// Date time commands
typedef int       (CALLBACK* DLLSETDATETIMEDARWIN)(DAQDARWIN, DarwinDateTime *);
typedef int       (CALLBACK* DLLSETDATETIMENOWDARWIN)(DAQDARWIN);
// Control commands
typedef int       (CALLBACK* DLLTRANSMODEDARWIN)(DAQDARWIN, int);
typedef int       (CALLBACK* DLLINITSYSTEMDARWIN)(DAQDARWIN, int);
// Get system
typedef int       (CALLBACK* DLLGETSYSTEMCONFIGDARWIN)(DAQDARWIN, double *, DarwinSystemInfo *);
// Get channel information
typedef int       (CALLBACK* DLLTALKCHINFODARWIN)(DAQDARWIN, int, int, int, int);
typedef int       (CALLBACK* DLLGETCHINFODARWIN)(DAQDARWIN, DarwinChInfo *, int *);
// Get measured data as ASCII
typedef int       (CALLBACK* DLLTALKDATABYASCIIDARWIN)(DAQDARWIN, int, int, int, int, DarwinDateTime *);
typedef int       (CALLBACK* DLLGETCHDATABYASCIIDARWIN)(DAQDARWIN, DarwinChInfo *, DarwinDataInfo *, int *);
// Get measured data as binary
typedef int       (CALLBACK* DLLTALKDATABYBINARYDARWIN)(DAQDARWIN, int, int, int, int, DarwinDateTime *);
typedef int       (CALLBACK* DLLGETCHDATABYBINARYDARWIN)(DAQDARWIN, DarwinChInfo *, DarwinDataInfo *, int *);
// Get configuration data by mode
typedef int       (CALLBACK* DLLTALKOPERATIONDATADARWIN)(DAQDARWIN, int, int, int, int);
typedef int       (CALLBACK* DLLTALKSETUPDATADARWIN)(DAQDARWIN, int, int, int, int);
typedef int       (CALLBACK* DLLTALKCALIBRATIONDATADARWIN)(DAQDARWIN, int, int, int, int);
typedef int       (CALLBACK* DLLGETSETDATABYLINEDARWIN)(DAQDARWIN, char *, int, int *, int *);
// Set range
typedef int       (CALLBACK* DLLSETSKIPDARWIN)(DAQDARWIN, int, int, int);
typedef int       (CALLBACK* DLLSETVOLTDARWIN)(DAQDARWIN, int, int, int, int, int, int, int, int, int);
typedef int       (CALLBACK* DLLSETTCDARWIN)(DAQDARWIN, int, int, int, int, int, int, int, int, int);
typedef int       (CALLBACK* DLLSETRTDDARWIN)(DAQDARWIN, int, int, int, int, int, int, int, int, int);
typedef int       (CALLBACK* DLLSETDIDARWIN)(DAQDARWIN, int, int, int, int, int, int, int, int, int);
typedef int       (CALLBACK* DLLSETDELTADARWIN)(DAQDARWIN, int, int, int, int, int, int);
typedef int       (CALLBACK* DLLSETRRJCDARWIN)(DAQDARWIN, int, int, int, int, int, int);
// Scalling
typedef int       (CALLBACK* DLLSETSCALLINGUNITDARWIN)(DAQDARWIN, const char *, int, int, int);
// Alarm
typedef int       (CALLBACK* DLLSETALARMDARWIN)(DAQDARWIN, int, int, int, int, int, int, int, int);
// Utility
typedef double    (CALLBACK* DLLTODOUBLEVALUEDARWIN)(int, int);
typedef int       (CALLBACK* DLLTOSTRINGVALUEDARWIN)(int, int, char *, int);
typedef int       (CALLBACK* DLLTOALARMNAMEDARWIN)(int, char *, int);
// Messages
typedef int       (CALLBACK* DLLGETVERSIONAPIDARWIN)(void);
typedef int       (CALLBACK* DLLGETREVISIONAPIDARWIN)(void);
typedef int       (CALLBACK* DLLTOERRORMESSAGEDARWIN)(int, char *, int);
typedef int       (CALLBACK* DLLGETMAXLENERRORMESSAGEDARWIN)(void);
typedef LPCSTR    (CALLBACK* DLLGETERRORMESSAGEDARWIN)(int);
// Deprecated command
typedef int       (CALLBACK* DLLSETTIMEOUTDARWIN)(DAQDARWIN, int);
// Since R2.01
typedef int       (CALLBACK* DLLSETMADARWIN)(DAQDARWIN, int, int, int, int, int, int, int, int, int);
typedef int       (CALLBACK* DLLSETSTRAINDARWIN)(DAQDARWIN, int, int, int, int, int, int, int, int, int);
typedef int       (CALLBACK* DLLSETPULSEDARWIN)(DAQDARWIN, int, int, int, int, int, int, int, int, int, int);
typedef int       (CALLBACK* DLLSETPOWERDARWIN)(DAQDARWIN, int, int, int, int, int, int, int, int, int, int);
typedef int       (CALLBACK* DLLESTABLISHDARWIN)(DAQDARWIN, int);
typedef int       (CALLBACK* DLLCOMPUTEDARWIN)(DAQDARWIN, int);
typedef int       (CALLBACK* DLLREPORTINGDARWIN)(DAQDARWIN, int);
typedef int       (CALLBACK* DLLGETREPORTSTATUSDARWIN)(DAQDARWIN, int *);
typedef int       (CALLBACK* DLLRECEIVEBYTEDARWIN)(DAQDARWIN, unsigned char *, int, int *);
typedef LPCSTR    (CALLBACK* DLLGETALARMNAMEDARWIN)(int);
typedef int       (CALLBACK* DLLGETMAXLENALARMNAMEDARWIN)(void);
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
#pragma pack(pop, oldpack) /* R2.01 */
#endif //WIN32,WCE
///////////////////////////////////////////////////////////////////////
#endif //_DAQDARWIN_H_
