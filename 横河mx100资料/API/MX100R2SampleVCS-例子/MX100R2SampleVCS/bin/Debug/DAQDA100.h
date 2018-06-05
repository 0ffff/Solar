// DAQDA100.h
/* -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- */
/*
 * Copyright (c) 2004 Yokogawa Electric Corporation. All rights reserved.
 *
 * This is defined export DAQDA100.dll.
 */
/* -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- */
// 2004/11/01 Ver.2 Rev.1
///////////////////////////////////////////////////////////////////////
#ifndef _DAQDA100_H_
#define _DAQDA100_H_
///////////////////////////////////////////////////////////////////////
#if defined(WIN32) || defined(_WIN32_WCE)
// system
#include <windows.h>
// calling
#ifdef DAQDA100_EXPORTS
#define DAQDARWIN_EXPORTS
#define DAQDA100_API __declspec(dllexport)
#else
#define DAQDA100_API __declspec(dllimport)
#endif
#else  //WIN32,WCE
#define DAQDA100_API
#ifndef APIENTRY
#define APIENTRY
#endif
#endif //WIN32,WCE
///////////////////////////////////////////////////////////////////////
#include "DAQDARWIN.h"
///////////////////////////////////////////////////////////////////////
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// value
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// total number
#define DAQDA100_NUMCH_BYUNIT  (DAQDARWIN_NUMSLOT * DAQDARWIN_NUMTERM)
// all
#define DAQDA100_CHTYPE_MEASALL        (0x0F) /* measued channel : 0 - 5 */
#define DAQDA100_CHNO_ALL              (-1)
#define DAQDA100_LEVELNO_ALL           (-1)
// code
#define DAQDA100_CODE_BINARY    (0)
#define DAQDA100_CODE_ASCII     (1)
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// range type
#define DAQDA100_RANGETYPE_VOLT         (0x00000000)
#define DAQDA100_RANGETYPE_DI           (0x00010000)
#define DAQDA100_RANGETYPE_TC           (0x00020000)
#define DAQDA100_RANGETYPE_RTD          (0x00040000)
#define DAQDA100_RANGETYPE_SKIP         (0x00080000)
#define DAQDA100_RANGETYPE_MA           (0x00100000)
#define DAQDA100_RANGETYPE_POWER        (0x00200000)
#define DAQDA100_RANGETYPE_STRAIN       (0x00400000)
#define DAQDA100_RANGETYPE_PULSE        (0x00800000)
// SKIP
#define DAQDA100_RANGE_SKIP             (DAQDA100_RANGETYPE_SKIP)
// VOLT
#define DAQDA100_RANGE_VOLT_20MV        (DAQDA100_RANGETYPE_VOLT | DAQDARWIN_RANGE_VOLT_20MV)
#define DAQDA100_RANGE_VOLT_60MV        (DAQDA100_RANGETYPE_VOLT | DAQDARWIN_RANGE_VOLT_60MV)
#define DAQDA100_RANGE_VOLT_200MV       (DAQDA100_RANGETYPE_VOLT | DAQDARWIN_RANGE_VOLT_200MV)
#define DAQDA100_RANGE_VOLT_2V          (DAQDA100_RANGETYPE_VOLT | DAQDARWIN_RANGE_VOLT_2V)
#define DAQDA100_RANGE_VOLT_6V          (DAQDA100_RANGETYPE_VOLT | DAQDARWIN_RANGE_VOLT_6V)
#define DAQDA100_RANGE_VOLT_20V         (DAQDA100_RANGETYPE_VOLT | DAQDARWIN_RANGE_VOLT_20V)
#define DAQDA100_RANGE_VOLT_50V         (DAQDA100_RANGETYPE_VOLT | DAQDARWIN_RANGE_VOLT_50V)
// TC
#define DAQDA100_RANGE_TC_R     (DAQDA100_RANGETYPE_TC | DAQDARWIN_RANGE_TC_R)
#define DAQDA100_RANGE_TC_S     (DAQDA100_RANGETYPE_TC | DAQDARWIN_RANGE_TC_S)
#define DAQDA100_RANGE_TC_B     (DAQDA100_RANGETYPE_TC | DAQDARWIN_RANGE_TC_B)
#define DAQDA100_RANGE_TC_K     (DAQDA100_RANGETYPE_TC | DAQDARWIN_RANGE_TC_K)
#define DAQDA100_RANGE_TC_E     (DAQDA100_RANGETYPE_TC | DAQDARWIN_RANGE_TC_E)
#define DAQDA100_RANGE_TC_J     (DAQDA100_RANGETYPE_TC | DAQDARWIN_RANGE_TC_J)
#define DAQDA100_RANGE_TC_T     (DAQDA100_RANGETYPE_TC | DAQDARWIN_RANGE_TC_T)
#define DAQDA100_RANGE_TC_N     (DAQDA100_RANGETYPE_TC | DAQDARWIN_RANGE_TC_N)
#define DAQDA100_RANGE_TC_W     (DAQDA100_RANGETYPE_TC | DAQDARWIN_RANGE_TC_W)
#define DAQDA100_RANGE_TC_L     (DAQDA100_RANGETYPE_TC | DAQDARWIN_RANGE_TC_L)
#define DAQDA100_RANGE_TC_U     (DAQDA100_RANGETYPE_TC | DAQDARWIN_RANGE_TC_U)
#define DAQDA100_RANGE_TC_KP    (DAQDA100_RANGETYPE_TC | DAQDARWIN_RANGE_TC_KP)
// RTD
#define DAQDA100_RANGE_RTD_1MAPT        (DAQDA100_RANGETYPE_RTD | DAQDARWIN_RANGE_RTD_1MAPT)
#define DAQDA100_RANGE_RTD_2MAPT        (DAQDA100_RANGETYPE_RTD | DAQDARWIN_RANGE_RTD_2MAPT)
#define DAQDA100_RANGE_RTD_1MAJPT       (DAQDA100_RANGETYPE_RTD | DAQDARWIN_RANGE_RTD_1MAJPT)
#define DAQDA100_RANGE_RTD_2MAJPT       (DAQDA100_RANGETYPE_RTD | DAQDARWIN_RANGE_RTD_2MAJPT)
#define DAQDA100_RANGE_RTD_2MAPT50      (DAQDA100_RANGETYPE_RTD | DAQDARWIN_RANGE_RTD_2MAPT50)
#define DAQDA100_RANGE_RTD_1MAPTH       (DAQDA100_RANGETYPE_RTD | DAQDARWIN_RANGE_RTD_1MAPTH)
#define DAQDA100_RANGE_RTD_2MAPTH       (DAQDA100_RANGETYPE_RTD | DAQDARWIN_RANGE_RTD_2MAPTH)
#define DAQDA100_RANGE_RTD_1MAJPTH      (DAQDA100_RANGETYPE_RTD | DAQDARWIN_RANGE_RTD_1MAJPTH)
#define DAQDA100_RANGE_RTD_2MAJPTH      (DAQDA100_RANGETYPE_RTD | DAQDARWIN_RANGE_RTD_2MAJPTH)
#define DAQDA100_RANGE_RTD_1MANIS       (DAQDA100_RANGETYPE_RTD | DAQDARWIN_RANGE_RTD_1MANIS)
#define DAQDA100_RANGE_RTD_1MANID       (DAQDA100_RANGETYPE_RTD | DAQDARWIN_RANGE_RTD_1MANID)
#define DAQDA100_RANGE_RTD_1MANI120     (DAQDA100_RANGETYPE_RTD | DAQDARWIN_RANGE_RTD_1MANI120)
#define DAQDA100_RANGE_RTD_CU10GE       (DAQDA100_RANGETYPE_RTD | DAQDARWIN_RANGE_RTD_CU10GE)
#define DAQDA100_RANGE_RTD_CU10LN       (DAQDA100_RANGETYPE_RTD | DAQDARWIN_RANGE_RTD_CU10LN)
#define DAQDA100_RANGE_RTD_CU10WEED     (DAQDA100_RANGETYPE_RTD | DAQDARWIN_RANGE_RTD_CU10WEED)
#define DAQDA100_RANGE_RTD_CU10BAILEY   (DAQDA100_RANGETYPE_RTD | DAQDARWIN_RANGE_RTD_CU10BAILEY)
#define DAQDA100_RANGE_RTD_J263B        (DAQDA100_RANGETYPE_RTD | DAQDARWIN_RANGE_RTD_J263B)
// DI
#define DAQDA100_RANGE_DI_LEVEL         (DAQDA100_RANGETYPE_DI | DAQDARWIN_RANGE_DI_LEVEL)
#define DAQDA100_RANGE_DI_CONTACT       (DAQDA100_RANGETYPE_DI | DAQDARWIN_RANGE_DI_CONTACT)
// mA
#define DAQDA100_RANGE_MA_20MA          (DAQDA100_RANGETYPE_MA | DAQDARWIN_RANGE_MA_20MA)
// POWER
#define DAQDA100_RANGE_POWER_25V05A     (DAQDA100_RANGETYPE_POWER | DAQDARWIN_RANGE_POWER_25V05A)
#define DAQDA100_RANGE_POWER_25V5A      (DAQDA100_RANGETYPE_POWER | DAQDARWIN_RANGE_POWER_25V5A)
#define DAQDA100_RANGE_POWER_250V05A    (DAQDA100_RANGETYPE_POWER | DAQDARWIN_RANGE_POWER_250V05A)
#define DAQDA100_RANGE_POWER_250V5A     (DAQDA100_RANGETYPE_POWER | DAQDARWIN_RANGE_POWER_250V5A)
// STRAIN
#define DAQDA100_RANGE_STRAIN_2K        (DAQDA100_RANGETYPE_STRAIN | DAQDARWIN_RANGE_STRAIN_2K)
#define DAQDA100_RANGE_STRAIN_20K       (DAQDA100_RANGETYPE_STRAIN | DAQDARWIN_RANGE_STRAIN_20K)
#define DAQDA100_RANGE_STRAIN_200K      (DAQDA100_RANGETYPE_STRAIN | DAQDARWIN_RANGE_STRAIN_200K)
// PULS
#define DAQDA100_RANGE_PULSE_RATE       (DAQDA100_RANGETYPE_PULSE | DAQDARWIN_RANGE_PULS_RATE)
#define DAQDA100_RANGE_PULSE_GATE       (DAQDA100_RANGETYPE_PULSE | DAQDARWIN_RANGE_PULS_GATE)
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// type
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// DAQ descriptor
// If Visual Basic, type as Long.
typedef int DAQDA100;
///////////////////////////////////////////////////////////////////////
#ifdef __cplusplus
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// Channel
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
class DAQDA100_API CDAQDARWINDataBuffer
{
    //-- ---- ---- ---- ---- ---- Constructs
public:
    CDAQDARWINDataBuffer(void);
    virtual ~CDAQDARWINDataBuffer(void);

    //-- ---- ---- ---- ---- ---- Attributes
protected:
    CDAQDARWINChInfo m_cChInfo;
    CDAQDARWINDateTime m_cTimeBuf;
    CDAQDARWINDataInfo m_cDataBuf;

    //-- ---- ---- ---- ---- ---- Overrides

    //-- ---- ---- ---- ---- ---- Implements
public:
    virtual void initialize(void);
    // access attributes
    CDAQDARWINChInfo & getClassDARWINChInfo(void);
    CDAQDARWINDateTime & getClassDARWINDateTime(void);
    CDAQDARWINDataInfo & getClassDARWINDataInfo(void);
    // set Data
    void setChInfo(CDAQDARWINChInfo & cDARWINChInfo);
    void setDateTime(CDAQDARWINDateTime & cDARWINDateTime);
    void setDataInfo(CDAQDARWINDataInfo & cDARWINDataInfo);
    // alarm
    int isAlarm(int levelNo);

    //-- ---- ---- ---- ---- ---- Operations

};
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// Handler
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
class DAQDA100_API CDAQDA100 : public CDAQDARWIN
{
    //-- ---- ---- ---- ---- ---- Constructs
public:
    CDAQDA100(void);
    CDAQDA100(const char * strAddress,
              unsigned int uiPort = DAQDARWIN_COMMPORT,
              int *        errCode = NULL);
    virtual ~CDAQDA100(void);

    //-- ---- ---- ---- ---- ---- Attributes
protected:
    int m_code;       //DAQDA100_CODE_xxx
    int m_statusByte; //DAQDARWIN_STATUS_xxx
    int m_reportStatus; //DAQDARWIN_REPSTATUS_xxx
    //system
    CDAQDARWINSysInfo m_cSysInfo;
    //channel
    CDAQDARWINDataBuffer m_cMeasData[DAQDARWIN_NUMUNIT][DAQDA100_NUMCH_BYUNIT];
    CDAQDARWINDataBuffer m_cMathData[DAQDA100_NUMCH_BYUNIT];

    //-- ---- ---- ---- ---- ---- Overrides
public:
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
    // utility
    virtual int isObject(const char * classname = "CDAQDA100");
    // control with update
    virtual int setDateTime(CDAQDARWINDateTime * pcDARWINDateTime = NULL);

    //-- ---- ---- ---- ---- ---- Implements
    // chType : channel type as DAQDARWIN_CHTYPE_xxx
    // chNo : channel number is 1 origin
public:
    // access attributes
    CDAQDARWINSysInfo & getClassSysInfo(void);
    CDAQDARWINDataBuffer * getClassDataBuffer(int chType,
                                              int chNo);
    // control
    int switchMode(int iMode); //DAQDARWIN_MODE_xxx
    int switchCode(int iCode); //DAQDA100_CODE_xxx
    int reconstruct(void);
    int initSetValue(void);
    int ackAlarm(void); //reset alarm
    int updateStatus(void);
    int switchCompute(int iCompute); //DAQDARWIN_COMPUTE_xxx
    int switchReport(int iReportRun); //DAQDARWIN_REPORT_RUN_xxx
    // measurement
    virtual int measInstCh(int chType = DAQDA100_CHTYPE_MEASALL,
                           int chNo = DAQDA100_CHNO_ALL);
    virtual int mathInstCh(int chNo = DAQDA100_CHNO_ALL);
    // channel info
    virtual int measInfoCh(int chType = DAQDA100_CHTYPE_MEASALL,
                           int chNo = DAQDA100_CHNO_ALL);
    virtual int mathInfoCh(int chNo = DAQDA100_CHNO_ALL);
    // system
    int updateSystemConfig(void);
    int updateReportStatus(void);
    // talker
    // @see CDAQDARWIN::getSetDataByLine
    int talkOperationChData(int chType = DAQDA100_CHTYPE_MEASALL,
                            int chNo = DAQDA100_CHNO_ALL);
    int talkSetupChData(int chType = DAQDA100_CHTYPE_MEASALL,
                        int chNo = DAQDA100_CHNO_ALL);
    int talkCalibrationChData(int chType = DAQDA100_CHTYPE_MEASALL,
                              int chNo = DAQDA100_CHNO_ALL);
    // operation mode
    int setRange(int chType,
                 int chNo,
                 int iRange, //DAQDA100_RANGE_xxx
                 int spanMin = 0,
                 int spanMax = 0,
                 int scaleMin = 0,
                 int scaleMax = 0,
                 int scalePoint = 0,
                 int bFilter = DAQDARWIN_VALID_OFF,
                 int iItem = DAQDARWIN_POWERITEM_P1,
                 int iWire = DAQDARWIN_WIRE_1PH2W);
    int setChDELTA(int chType,
                   int chNo,
                   int refChNo,
                   int spanMin = 0,
                   int spanMax = 0);
    int setChRRJC(int chType,
                  int chNo,
                  int refChNo,
                  int spanMin = 0,
                  int spanMax = 0);
    int setChUnit(int chType,
                  int chNo,
                  const char * strUnit);
    int setChAlarm(int chType,
                   int chNo,
                   int levelNo,
                   int iAlarmType = DAQDARWIN_ALARM_NONE,
                   int value = 0,
                   int relayType = 0,
                   int relayNo = 0);
    // get
    int getCode(void); //DAQDA100_CODE_xxx
    int getByte(void); //DAQDARWIN_STATUS_xxx
    int getReport(void); //DAQDARWIN_REPSTATUS_xxx

protected:
    // initialize
    void measClear(void);
    // update attributes
    int updateAll(void);
    int updateRenew(void);
    int updateChInfo(void); //meas and math
    // measurement
    int getInstChBINARY(int sChType,
                        int sChNo,
                        int eChType,
                        int eChNo);
    int getInstChASCII(int sChType,
                       int sChNo,
                       int eChType,
                       int eChNo);
    virtual int getInfoCh(int sChType,
                          int sChNo,
                          int eChType,
                          int eChNo);
    // utility
    int chNumMax(int chType);
    virtual int chNumMaxReport(void); // NOTE: if DR130, 30. but this returns 60
    // DLL version
    static const int getVersionDA100DLL(void);
    static const int getRevisionDA100DLL(void);
  
    //-- ---- ---- ---- ---- ---- Operations

};
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
#endif //__cplusplus
///////////////////////////////////////////////////////////////////////
#ifdef __cplusplus
extern "C" {
#endif
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// Communication
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
/**
 * This opens the instrument with initialize.
 * This returns NULL descriptor, if error occured.
 * @param strAddress specified an address as string.
 * @param errorCode stored an error code if error occured.
 * @return an instrument descriptor.
 */
DAQDA100_API DAQDA100 APIENTRY openDA100(const char * strAddress,
                                         int *        errorCode);
#define OPEN_DA100(address) openDA100(address, NULL);
/**
 * This closes the instrument.
 * @param daqda100 specified an instrument descriptor.
 * @return an error code that is not zero if error ocuured.
 */
DAQDA100_API int APIENTRY closeDA100(DAQDA100 daqda100);
/**
 * This sends data as string at terminate.
 * @param daqda100 specified an instrument descriptor.
 * @param strLine specified a buffer for sending without terminate.
 * @return an error code that is not zero if error ocuured.
 */
DAQDA100_API int APIENTRY sendLineDA100(DAQDA100      daqda100,
                                        const char * strLine);
/**
 * This receives data as string at terminate.
 * @param daqda100 specified an instrument descriptor.
 * @param strLine stored a buffer of the received data.
 * @param maxLine specified a max length of the data (strLine).
 * @param lenLine stored a real length of the received data.
 * @return an error code that is not zero if error ocuured.
 */
DAQDA100_API int APIENTRY receiveLineDA100(DAQDA100 daqda100,
                                           char *   strLine,
                                           int      maxLine,
                                           int *    lenLine);
/**
 * This receives a data as byte.
 * @param daqda100 specified an instrument descriptor.
 * @param byteData stored a data.
 * @param maxData specified a length of the data (byteData).
 * @param lenData stored a length of a recieved data.
 * @return an error code that is not zero if error ocuured.
 */
DAQDA100_API int APIENTRY receiveByteDA100(DAQDA100        daqda100,
                                           unsigned char * byteData,
                                           int             maxData,
                                           int *           lenData);
/**
 * This sends a trigger.
 * @param daqda100 specified an instrument descriptor.
 * @return an error code that is not zero if error ocuured.
 */
DAQDA100_API int APIENTRY sendTriggerDA100(DAQDA100 daqda100);
/**
 * This updates status.
 * @param daqda100 specified an instrument descriptor.
 * @return an error code that is not zero if error ocuured.
 */
DAQDA100_API int APIENTRY updateStatusDA100(DAQDA100 daqda100);
/**
 * This runs a command as string, then receives and checks the ack. 
 * @param daqda100 specified an instrument descriptor.
 * @param strCmd specified a command as string without a terminate.
 * @return an error code that is not zero if error ocuured.
 */
DAQDA100_API int APIENTRY runCommandDA100(DAQDA100     daqda100,
                                          const char * strCmd);
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// Control
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
/**
 * This transfers the setting mode.
 * @param daqdarwin specified an instrument descriptor.
 * @param iMode specified a selection of mode as DAQDARWIN_MODE_xxx.
 * @return an error code that is not zero if error ocuured.
 */
DAQDA100_API int APIENTRY switchModeDA100(DAQDA100 daqda100,
                                          int      iMode);
/**
 * This transfers a code type of measuremnet data.
 * @param daqda100 specified an instrument descriptor.
 * @param iCode specified a code type as DAQDA100_CODE_xxx.
 * @return an error code that is not zero if error ocuured.
 */
DAQDA100_API int APIENTRY switchCodeDA100(DAQDA100 daqda100,
                                          int      iCode);
/**
 * This reconstructs.
 * @param daqda100 specified an instrument descriptor.
 * @return an error code that is not zero if error ocuured.
 */
DAQDA100_API int APIENTRY reconstructDA100(DAQDA100 daqda100);
/**
 * This initialize the set values.
 * @param daqda100 specified an instrument descriptor.
 * @return an error code that is not zero if error ocuured.
 */
DAQDA100_API int APIENTRY initSetValueDA100(DAQDA100 daqda100);
/**
 * This sends acknowledge alarm. (clears the alarm buffer)
 * @param daqda100 specified an instrument descriptor.
 * @return an error code that is not zero if error ocuured.
 */
DAQDA100_API int APIENTRY ackAlarmDA100(DAQDA100 daqda100);
/**
 * This sets date and time, now.
 * @param daqda100 specified an instrument descriptor.
 * @return an error code that is not zero if error ocuured.
 */
DAQDA100_API int APIENTRY setDateTimeNowDA100(DAQDA100 daqda100);
/**
 * This transfers computing.
 * @param daqda100 specified an instrument descriptor.
 * @param iCompute specified start/stop as DAQDARWIN_COMPUTE_xxx.
 * @return an error code that is not zero if error ocuured.
 */
DAQDA100_API int APIENTRY switchComputeDA100(DAQDA100 daqda100,
                                             int      iCompute);
/**
 * This transfers reporting.
 * @param daqda100 specified an instrument descriptor.
 * @param iReportRun specified start/stop as DAQDARWIN_REPORT_RUN_xxx.
 * @return an error code that is not zero if error ocuured.
 */
DAQDA100_API int APIENTRY switchReportDA100(DAQDA100 daqda100,
                                            int      iReportRun);
/**
 * This establishes the contents of the setup mode setting.
 * @param daqda100 specified an instrument descriptor.
 * @param iSetup specified a selection as DAQDARWIN_SETUP_xxx.
 * @return an error code that is not zero if error ocuured.
 */
DAQDA100_API int APIENTRY establishDA100(DAQDA100 daqda100,
                                         int      iSetup);
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// Set on Operation Mode
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
/**
 * This sets the range.
 * @param daqda100 specified an instrument descriptor.
 * @param chType specified a channel type as unit number.
 * @param chNo specified a channel number.
 * @param iRange specified a selection of the range as DAQDA100_RANGE_xxx
 * @param spanMin specified a value of the minimum span.
 * @param spanMax specified a value of the maximum span.
 * @param scaleMin specified a value of the minimum scale.
 * @param scaleMax specified a value of the maximum scale.
 * @param scalePoint specified a value of the decimal point position.
 * @param bFilter specified a filter as boolean if PULSE.
 * @param iItem specified a power item as DAQDARWIN_POWERITEM_xxx if POWER.
 * @param iWire specified a wiring method as DAQDARWIN_WIRE_xxx if POWER.
 * @return an error code that is not zero if error ocuured.
 */
DAQDA100_API int APIENTRY setRangeDA100(DAQDA100 daqda100,
                                        int      chType,
                                        int      chNo,
                                        int      iRange,
                                        int      spanMin,
                                        int      spanMax,
                                        int      scaleMin,
                                        int      scaleMax,
                                        int      scalePoint,
                                        int      bFilter,
                                        int      iItem,
                                        int      iWire);
/**
 * This stes DELTA.
 * @param daqda100 specified an instrument descriptor.
 * @param chType specified a channel type as unit number.
 * @param chNo specified a channel number.
 * @param refChNo specified a reference channel number.
 * @param spanMin specified a value of the minimum span.
 * @param spanMax specified a value of the maximum span.
 * @return an error code that is not zero if error ocuured.
 */
DAQDA100_API int APIENTRY setChDELTADA100(DAQDA100 daqda100,
                                          int      chType,
                                          int      chNo,
                                          int      refChNo,
                                          int      spanMin,
                                          int      spanMax);
/**
 * This sets RRJC.
 * @param daqda100 specified an instrument descriptor.
 * @param chType specified a channel type as unit number.
 * @param chNo specified a channel number.
 * @param refChNo specified a reference channel number.
 * @param spanMin specified a value of the minimum span.
 * @param spanMax specified a value of the maximum span.
 * @return an error code that is not zero if error ocuured.
 */
DAQDA100_API int APIENTRY setChRRJCDA100(DAQDA100 daqda100,
                                         int      chType,
                                         int      chNo,
                                         int      refChNo,
                                         int      spanMin,
                                         int      spanMax);
/**
 * This sets an unit name.
 * @param daqda100 specified an instrument descriptor.
 * @param chType specified a channel type as unit number.
 * @param chNo specified a channel number.
 * @param strUnit specified a string of the unit name.
 * @return an error code that is not zero if error ocuured.
 */
DAQDA100_API int APIENTRY setChUnitDA100(DAQDA100     daqda100,
                                         int          chType,
                                         int          chNo,
                                         const char * strUnit);
/**
 * This stes an alarm.
 * @param daqda100 specified an instrument descriptor.
 * @param chType specified a channel type as unit number.
 * @param chNo specified a channel number.
 * @param levelNo specified a level number.
 * @param iAlarmType specified a selection of the alarm type as DAQDARWIN_ALARM_xxx.
 * @param value specified a value of an alarm ON without decimal point position.
 * @param relayType specified a relay type.
 * @param relayNo specified a relay number.
 * @return an error code that is not zero if error ocuured.
 */
DAQDA100_API int APIENTRY setChAlarmDA100(DAQDA100 daqda100,
                                          int      chType,
                                          int      chNo,
                                          int      levelNo,
                                          int      iAlarmType,
                                          int      value,
                                          int      relayType,
                                          int      relayNo);
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// Measurement
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
/**
 * This measures instantenous values as measured channel.
 * @param daqda100 specified an instrument descriptor.
 * @param chType specified a channel type as unit number.
 * @param chNo specified a channel number.
 * @return an error code that is not zero if error ocuured.
 */
DAQDA100_API int APIENTRY measInstChDA100(DAQDA100 daqda100,
                                          int      chType,
                                          int      chNo);
/**
 * This measures instantenous values as mathmatical channel.
 * @param daqda100 specified an instrument descriptor.
 * @param chNo specified a channel number.
 * @return an error code that is not zero if error ocuured.
 */
DAQDA100_API int APIENTRY mathInstChDA100(DAQDA100 daqda100,
                                          int      chNo);
/**
 * This gets informations of measured channel.
 * @param daqda100 specified an instrument descriptor.
 * @param chType specified a channel type as unit number.
 * @param chNo specified a channel number.
 * @return an error code that is not zero if error ocuured.
 */
DAQDA100_API int APIENTRY measInfoChDA100(DAQDA100 daqda100,
                                          int      chType,
                                          int      chNo);
/**
 * This gets informations of mathmatical channel.
 * @param daqda100 specified an instrument descriptor.
 * @param chNo specified a channel number.
 * @return an error code that is not zero if error ocuured.
 */
DAQDA100_API int APIENTRY mathInfoChDA100(DAQDA100 daqda100,
                                          int      chNo);
/**
 * This gets syatem configure.
 * @param daqda100 specified an instrument descriptor.
 * @return an error code that is not zero if error ocuured.
 */
DAQDA100_API int APIENTRY updateSystemConfigDA100(DAQDA100 daqda100);
/**
 * This gets report status
 * @param daqda100 specified an instrument descriptor.
 * @return an error code that is not zero if error ocuured.
 */
DAQDA100_API int APIENTRY updateReportStatusDA100(DAQDA100 daqda100);
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// Talker
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
/**
 * This sends a talker of getting the setting data on operation mode.
 * @param daqda100 specified an instrument descriptor.
 * @param startChType specified a start channel type.
 * @param startChNo specified a start channel number.
 * @param endChType specified an end channel type.
 * @param endChNo specified an end channel number.
 * @return an error code that is not zero if error ocuured.
 */
DAQDA100_API int APIENTRY talkOperationDataDA100(DAQDA100 daqda100,
                                                 int      startChType,
                                                 int      startChNo,
                                                 int      endChType,
                                                 int      endChNo);
/**
 * This sends a talker of getting the setting data on setup mode.
 * @param daqda100 specified an instrument descriptor.
 * @param startChType specified a start channel type.
 * @param startChNo specified a start channel number.
 * @param endChType specified an end channel type.
 * @param endChNo specified an end channel number.
 * @return an error code that is not zero if error ocuured.
 */
DAQDA100_API int APIENTRY talkSetupDataDA100(DAQDA100 daqda100,
                                             int      startChType,
                                             int      startChNo,
                                             int      endChType,
                                             int      endChNo);
/**
 * This sends a talker of getting the setting data on calibration mode.
 * @param daqda100 specified an instrument descriptor.
 * @param startChType specified a start channel type.
 * @param startChNo specified a start channel number.
 * @param endChType specified an end channel type.
 * @param endChNo specified an end channel number.
 * @return an error code that is not zero if error ocuured.
 */
DAQDA100_API int APIENTRY talkCalibrationDataDA100(DAQDA100 daqda100,
                                                   int      startChType,
                                                   int      startChNo,
                                                   int      endChType,
                                                   int      endChNo);
/**
 * This sends a talker of getting the setting data on operation mode.
 * @param daqda100 specified an instrument descriptor.
 * @param chType specified a channel type as unit number.
 * @param chNo specified a channel number.
 * @return an error code that is not zero if error ocuured.
 */
DAQDA100_API int APIENTRY talkOperationChDataDA100(DAQDA100 daqda100,
                                                   int      chType,
                                                   int      chNo);
/**
 * This sends a talker of getting the setting data on setup mode.
 * @param daqda100 specified an instrument descriptor.
 * @param chType specified a channel type as unit number.
 * @param chNo specified a channel number.
 * @return an error code that is not zero if error ocuured.
 */
DAQDA100_API int APIENTRY talkSetupChDataDA100(DAQDA100 daqda100,
                                               int      chType,
                                               int      chNo);
/**
 * This sends a talker of getting the setting data on calibration mode.
 * @param daqda100 specified an instrument descriptor.
 * @param chType specified a channel type as unit number.
 * @param chNo specified a channel number.
 * @return an error code that is not zero if error ocuured.
 */
DAQDA100_API int APIENTRY talkCalibrationChDataDA100(DAQDA100 daqda100,
                                                     int      chType,
                                                     int      chNo);
/**
 * This gets a setting data by line.
 * @param daqda100 specified an instrument descriptor.
 * @param strLine stored a buffer of the received data.
 * @param maxLine specified a max length of the data (strLine).
 * @param lenLine stored a real length of the received data.
 * @param pFlag stored a flag as DAQDARWIN_FLAG_xxx.
 * @return an error code that is not zero if error ocuured.
 */
DAQDA100_API int APIENTRY getSetDataByLineDA100(DAQDA100 daqda100,
                                                char *   strLine,
                                                int      maxLine,
                                                int *    lenLine,
                                                int *    pFlag);
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// Get Data
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
/**
 * This gets a value of measurement data by channel.
 * @param daqda100 specified an instrument descriptor.
 * @param chType specified a channel type.
 * @param chNo specified a channel number.
 * @return a value of measurement data without decimal point position.
 */
DAQDA100_API int APIENTRY dataValueDA100(DAQDA100 daqda100,
                                         int      chType,
                                         int      chNo);
/**
 * This gets a status of measurement data by channel.
 * @param daqda100 specified an instrument descriptor.
 * @param chType specified a channel type.
 * @param chNo specified a channel number.
 * @return a status as DAQDARWIN_DATA_xxx.
 */
DAQDA100_API int APIENTRY dataStatusDA100(DAQDA100 daqda100,
                                          int      chType,
                                          int      chNo);
/**
 * This gets a alarm of measurement data by channel.
 * @param daqda100 specified an instrument descriptor.
 * @param chType specified a channel type.
 * @param chNo specified a channel number.
 * @param levelNo specified a level number.
 * @return a validation as boolean.
 */
DAQDA100_API int APIENTRY dataAlarmDA100(DAQDA100 daqda100,
                                         int      chType,
                                         int      chNo,
                                         int      levelNo);
/**
 * This gets a value of current measurement data by channel.
 * @param daqda100 specified an instrument descriptor.
 * @param chType specified a channel type.
 * @param chNo specified a channel number.
 * @retrun a value as double.
 */
DAQDA100_API double APIENTRY dataDoubleValueDA100(DAQDA100 daqda100,
                                                  int      chType,
                                                  int      chNo);
/**
 * This gets a value of current measurement data by channel.
 * @param daqda100 specified an instrument descriptor.
 * @param chType specified a channel type.
 * @param chNo specified a channel number.
 * @param strValue stored a string of the value.
 * @param lenValue specified a length of the buffer (strValue).
 * @retrun a length of the string by bytes without NULL.
 */
DAQDA100_API int APIENTRY dataStringValueDA100(DAQDA100 daqda100,
                                               int      chType,
                                               int      chNo,
                                               char *   strValue,
                                               int      lenValue);
/**
 * This gets a year of data.
 * @param daqda100 specified an instrument descriptor.
 * @param chType specified a channel type.
 * @param chNo specified a channel number.
 * @retrun a value of year.
 */
DAQDA100_API int APIENTRY dataYearDA100(DAQDA100 daqda100,
                                        int      chType,
                                        int      chNo);
/**
 * This gets a month of data.
 * @param daqda100 specified an instrument descriptor.
 * @param chType specified a channel type.
 * @param chNo specified a channel number.
 * @retrun a value of month.
 */
DAQDA100_API int APIENTRY dataMonthDA100(DAQDA100 daqda100,
                                         int      chType,
                                         int      chNo);
/**
 * This gets a hour of data.
 * @param daqda100 specified an instrument descriptor.
 * @param chType specified a channel type.
 * @param chNo specified a channel number.
 * @retrun a value of hour.
 */
DAQDA100_API int APIENTRY dataDayDA100(DAQDA100 daqda100,
                                       int      chType,
                                       int      chNo);
/**
 * This gets a minute of data.
 * @param daqda100 specified an instrument descriptor.
 * @param chType specified a channel type.
 * @param chNo specified a channel number.
 * @retrun a value of minute.
 */
DAQDA100_API int APIENTRY dataHourDA100(DAQDA100 daqda100,
                                        int      chType,
                                        int      chNo);
/**
 * This gets a day of data.
 * @param daqda100 specified an instrument descriptor.
 * @param chType specified a channel type.
 * @param chNo specified a channel number.
 * @retrun a value of day.
 */
DAQDA100_API int APIENTRY dataMinuteDA100(DAQDA100 daqda100,
                                          int      chType,
                                          int      chNo);
/**
 * This gets a second of data.
 * @param daqda100 specified an instrument descriptor.
 * @param chType specified a channel type.
 * @param chNo specified a channel number.
 * @retrun a value of second.
 */
DAQDA100_API int APIENTRY dataSecondDA100(DAQDA100 daqda100,
                                          int      chType,
                                          int      chNo);
/**
 * This gets a alarm type of measurement data by channel.
 * @param daqda100 specified an instrument descriptor.
 * @param chType specified a channel type.
 * @param chNo specified a channel number.
 * @param levelNo specified a level number.
 * @return a alarm type as DAQDARWIN_ALARM_xxx
 */
DAQDA100_API int APIENTRY alarmTypeDA100(DAQDA100 daqda100,
                                         int      chType,
                                         int      chNo,
                                         int      levelNo);
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// Get Channel Information
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
/**
 * This gets a decimal point position of the channel.
 * @param daqda100 specified an instrument descriptor.
 * @param chType specified a channel type.
 * @param chNo specified a channel number.
 * @return a value of a decimal point position.
 */
DAQDA100_API int APIENTRY channelPointDA100(DAQDA100 daqda100,
                                            int      chType,
                                            int      chNo);
/**
 * This gets a channel status.
 * @param daqda100 specified an instrument descriptor.
 * @param chType specified a channel type.
 * @param chNo specified a channel number.
 * @return a channel status as DAQDARWIN_DATA_xxx
 */
DAQDA100_API int APIENTRY channelStatusDA100(DAQDA100 daqda100,
                                             int      chType,
                                             int      chNo);
/**
 * This gets an unit of the channel.
 * @param daqda100 specified an instrument descriptor.
 * @param chType specified a channel type.
 * @param chNo specified a channel number.
 * @return a string of the unit.
 */
DAQDA100_API const char * APIENTRY getChannelUnitDA100(DAQDA100 daqda100,
                                                       int      chType,
                                                       int      chNo);
/**
 * This gets an unit of the channel.
 * @param daqda100 specified an instrument descriptor.
 * @param chType specified a channel type.
 * @param chNo specified a channel number.
 * @param strUnit stored a string of the unit.
 * @param lenUnit specified a length of the buffer (strUnit).
 * @return a length of the string by bytes without NULL.
 */
DAQDA100_API int APIENTRY toChannelUnitDA100(DAQDA100 daqda100,
                                             int      chType,
                                             int      chNo,
                                             char *   strUnit,
                                             int      lenUnit);
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// Get System Information
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
/**
 * This gets an interval.
 * @param daqda100 specified an instrument descriptor.
 * @return a value of the interval.
 */
DAQDA100_API double APIENTRY unitIntervalDA100(DAQDA100 daqda100);
/**
 * This gets a validation of (main and sub) unit.
 * @param daqda100 specified an instrument descriptor.
 * @param unitNo specified an unit number.
 * @return a validation as boolean.
 */
DAQDA100_API int APIENTRY unitValidDA100(DAQDA100 daqda100,
                                         int      unitNo);
/**
 * This gets a module internal code.
 * @param daqda100 specified an instrument descriptor.
 * @param unitNo specified an unit number.
 * @param slotNo specified a slot number.
 * @return a code.
 */
DAQDA100_API int APIENTRY moduleCodeDA100(DAQDA100 daqda100,
                                          int      unitNo,
                                          int      slotNo);
/**
 * This gets a module name.
 * @param daqda100 specified an instrument descriptor.
 * @param unitNo specified an unit number.
 * @param slotNo specified a slot number.
 * @return a string of the module name.
 */
DAQDA100_API const char * APIENTRY getModuleNameDA100(DAQDA100 daqda100,
                                                      int      unitNo,
                                                      int      slotNo);
/**
 * This gets a module name.
 * @param daqda100 specified an instrument descriptor.
 * @param unitNo specified an unit number.
 * @param slotNo specified a slot number.
 * @param strName stored a string of the unit.
 * @param lenName specified a length of the buffer (strUnit).
 * @return a string of the module name.
 */
DAQDA100_API int APIENTRY toModuleNameDA100(DAQDA100 daqda100,
                                            int      unitNo,
                                            int      slotNo,
                                            char *   strName,
                                            int      lenName);
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// Get Status
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
/**
 * This gets a status byte.
 * @param daqda100 specified an instrument descriptor.
 * @return a status byte as DAQDARWIN_STATUS_xxx (logical or)
 */
DAQDA100_API int APIENTRY statusByteDA100(DAQDA100 daqda100);
/**
 * This gets a code type of measurement data.
 * @param daqda100 specified an instrument descriptor.
 * @return a code type as DAQDA100_CODE_xxx
 */
DAQDA100_API int APIENTRY statusCodeDA100(DAQDA100 daqda100);
/**
 * This gets a report status.
 * @param daqda100 specified an instrument descriptor.
 * @return a report status as DAQDARWIN_REPSTATUS_xxx
 */
DAQDA100_API int APIENTRY statusReportDA100(DAQDA100 daqda100);
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// Utility
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
/**
 * This changes a measured data and decimal point position to a value as double.
 * @param dataValue specified a measured data as integer (Long).
 * @param point specified a decimal point position.
 * @return a value as double.
 */
DAQDA100_API double APIENTRY toDoubleValueDA100(int dataValue,
                                                int point);
/**
 * This changes a measured data and decimal point position to a value as string.
 * @param dataValue specified a measured data as integer (Long).
 * @param point specified a decimal point position.
 * @param strValue stored a string.
 * @param lenValue specified a length of the buffer (strValue).
 * @return a length of the string by bytes without NULL.
 */
DAQDA100_API int APIENTRY toStringValueDA100(int    dataValue,
                                             int    point,
                                             char * strValue,
                                             int    lenValue);
/**
 * This gets a string of the alarm type.
 * @param iAlarmType specified a selection of the alarm type as DAQDARWIN_ALARM_xxx.
 * @return a string of the alarm type.
 */
DAQDA100_API const char * APIENTRY getAlarmNameDA100(int iAlarmType);
/**
 * This changes an alarm type to a mnemonic as string.
 * @param iAlarmType specified a selection of the alarm type as DAQDARWIN_ALARM_xxx.
 * @param strAlarm stored a string.
 * @param lenAlarm specified a length of the buffer (strAlarm).
 * @return a length of the string by bytes without NULL.
 */
DAQDA100_API int APIENTRY toAlarmNameDA100(int    iAlarmType,
                                           char * strAlarm,
                                           int    lenAlarm);
/**
 * This gets a maximum length of alarm names.
 * @return a length by bytes without NULL.
 */
DAQDA100_API int APIENTRY alarmMaxLengthDA100(void);
/**
 * This gets a version of API.
 * @return a number as integer.
 */
DAQDA100_API const int APIENTRY versionAPIDA100(void);
/**
 * This gets a revision of API.
 * @return a number as integer.
 */
DAQDA100_API const int APIENTRY revisionAPIDA100(void);
/**
 * This gets a string of the error code.
 * @param errCode specified an error code that returned by each function.
 * @return a string of the error message.
 */
DAQDA100_API const char * APIENTRY getErrorMessageDA100(int errCode);
/**
 * This changes an error code to a string.
 * @param errCode specified an error code that returned by each function.
 * @param errStr stored a string of the error message.
 * @param errLen specified a length of the buffer (errStr).
 * @return a length of the error message by bytes without NULL.
 */
DAQDA100_API int APIENTRY toErrorMessageDA100(int    errCode,
                                              char * errStr,
                                              int    errLen);
/**
 * This gets a maximum length of error messages.
 * @return a length by bytes without NULL.
 */
DAQDA100_API int APIENTRY errorMaxLengthDA100(void);
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
#ifdef __cplusplus
}
#endif
///////////////////////////////////////////////////////////////////////
#if defined(WIN32) || defined(_WIN32_WCE)
/**
 * This defines types as load library callback for MS-Windows.
 */
typedef DAQDA100 (CALLBACK* DLLOPENDA100)(const char *, int *);
typedef int      (CALLBACK* DLLCLOSEDA100)(DAQDA100);
typedef int      (CALLBACK* DLLSENDLINEDA100)(DAQDA100, const char * );
typedef int      (CALLBACK* DLLRECEIVELINEDA100)(DAQDA100, char *, int, int *);
typedef int      (CALLBACK* DLLRECEIVEBYTEDA100)(DAQDA100, unsigned char *, int, int *);
typedef int      (CALLBACK* DLLSENDTRIGGERDA100)(DAQDA100);
typedef int      (CALLBACK* DLLUPDATESTATUSDA100)(DAQDA100);
typedef int      (CALLBACK* DLLRUNCOMMANDDA100)(DAQDA100, const char *);
// Control
typedef int      (CALLBACK* DLLSWITCHMODEDA100)(DAQDA100, int);
typedef int      (CALLBACK* DLLSWITCHCODEDA100)(DAQDA100, int);
typedef int      (CALLBACK* DLLRECONSTRUCTDA100)(DAQDA100);
typedef int      (CALLBACK* DLLINITSETVALUEDA100)(DAQDA100);
typedef int      (CALLBACK* DLLACKALARMDA100)(DAQDA100);
typedef int      (CALLBACK* DLLSETDATETIMENOWDA100)(DAQDA100);
typedef int      (CALLBACK* DLLSWITCHCOMPUTEDA100)(DAQDA100, int);
typedef int      (CALLBACK* DLLSWITCHREPORTDA100)(DAQDA100, int);
typedef int      (CALLBACK* DLLESTABLISHDA100)(DAQDA100, int);
// Set on Operation Mode
typedef int      (CALLBACK* DLLSETRANGEDA100)(DAQDA100, int, int, int, int, int, int, int, int, int, int, int);
typedef int      (CALLBACK* DLLSETCHDELTADA100)(DAQDA100, int, int, int, int, int);
typedef int      (CALLBACK* DLLSETCHRRJCDA100)(DAQDA100, int, int, int, int, int);
typedef int      (CALLBACK* DLLSETCHUNITDA100)(DAQDA100, int, int, const char *);
typedef int      (CALLBACK* DLLSETCHALARMDA100)(DAQDA100, int, int, int, int, int, int, int);
// Measurement
typedef int      (CALLBACK* DLLMEASINSTCHDA100)(DAQDA100, int, int);
typedef int      (CALLBACK* DLLMATHINSTCHDA100)(DAQDA100, int);
typedef int      (CALLBACK* DLLMEASINFOCHDA100)(DAQDA100, int, int);
typedef int      (CALLBACK* DLLMATHINFOCHDA100)(DAQDA100, int);
typedef int      (CALLBACK* DLLUPDATESYSTEMCONFIGDA100)(DAQDA100);
typedef int      (CALLBACK* DLLUPDATEREPORTSTATUSDA100)(DAQDA100);
// Talker
typedef int      (CALLBACK* DLLTALKOPERATIONDATADA100)(DAQDA100, int, int, int, int);
typedef int      (CALLBACK* DLLTALKSETUPDATADA100)(DAQDA100, int, int, int, int);
typedef int      (CALLBACK* DLLTALKCALIBRATIONDATADA100)(DAQDA100, int, int, int, int);
typedef int      (CALLBACK* DLLTALKOPERATIONCHDATADA100)(DAQDA100, int, int);
typedef int      (CALLBACK* DLLTALKSETUPCHDATADA100)(DAQDA100, int, int);
typedef int      (CALLBACK* DLLTALKCALIBRATIONCHDATADA100)(DAQDA100, int, int);
typedef int      (CALLBACK* DLLGETSETDATABYLINEDA100)(DAQDA100, char *, int, int *, int *);
// Get Data
typedef int      (CALLBACK* DLLDATAVALUEDA100)(DAQDA100, int, int);
typedef int      (CALLBACK* DLLDATASTATUSDA100)(DAQDA100, int, int);
typedef int      (CALLBACK* DLLDATAALARMDA100)(DAQDA100, int, int, int);
typedef double   (CALLBACK* DLLDATADOUBLEVALUEDA100)(DAQDA100, int, int);
typedef int      (CALLBACK* DLLDATASTRINGVALUEDA100)(DAQDA100, int, int, char *, int);
typedef int      (CALLBACK* DLLDATAYEARDA100)(DAQDA100, int, int);
typedef int      (CALLBACK* DLLDATAMONTHDA100)(DAQDA100, int, int);
typedef int      (CALLBACK* DLLDATADAYDA100)(DAQDA100, int, int);
typedef int      (CALLBACK* DLLDATAHOURDA100)(DAQDA100, int, int);
typedef int      (CALLBACK* DLLDATAMINUTEDA100)(DAQDA100, int, int);
typedef int      (CALLBACK* DLLDATASECONDDA100)(DAQDA100, int, int);
typedef int      (CALLBACK* DLLALARMTYPEDA100)(DAQDA100, int, int, int);
// Get Channel Information
typedef int      (CALLBACK* DLLCHANNELPOINTDA100)(DAQDA100, int, int);
typedef int      (CALLBACK* DLLCHANNELSTATUSDA100)(DAQDA100, int, int);
typedef LPCSTR   (CALLBACK* DLLGETCHANNELUNITDA100)(DAQDA100, int, int);
typedef int      (CALLBACK* DLLTOCHANNELUNITDA100)(DAQDA100, int, int, char *, int);
// Get System Information
typedef double   (CALLBACK* DLLUNITINTERVALDA100)(DAQDA100);
typedef int      (CALLBACK* DLLUNITVALIDDA100)(DAQDA100);
typedef int      (CALLBACK* DLLMODULECODEDA100)(DAQDA100, int, int);
typedef LPCSTR   (CALLBACK* DLLGETMODULENAMEDA100)(DAQDA100, int, int);
typedef int      (CALLBACK* DLLTOMODULENAMEDA100)(DAQDA100, int, int, char *, int);
// Get Status
typedef int      (CALLBACK* DLLSTATUSBYTEDA100)(DAQDA100);
typedef int      (CALLBACK* DLLSTATUSCODEDA100)(DAQDA100);
typedef int      (CALLBACK* DLLSTATUSREPORTDA100)(DAQDA100);
// Utility
typedef double   (CALLBACK* DLLTODOUBLEVALUEDA100)(int, int);
typedef int      (CALLBACK* DLLTOSTRINGVALUEDA100)(int, int, char *, int);
typedef LPCSTR   (CALLBACK* DLLGETALARMNAMEDA100)(int);
typedef int      (CALLBACK* DLLTOALARMNAMEDA100)(int, char *, int);
typedef int      (CALLBACK* DLLALARMMAXLENGTHDA100)(void);
typedef int      (CALLBACK* DLLVERSIONAPIDA100)(void);
typedef int      (CALLBACK* DLLREVISIONAPIDA100)(void);
typedef LPCSTR   (CALLBACK* DLLGETERRORMESSAGEDA100)(int);
typedef int      (CALLBACK* DLLTOERRORMESSAGEDA100)(int, char *, int);
typedef int      (CALLBACK* DLLERRORMAXLENGTHDA100)(void);
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
#endif //WIN32,WCE
///////////////////////////////////////////////////////////////////////
#endif //_DAQDA100_H_
