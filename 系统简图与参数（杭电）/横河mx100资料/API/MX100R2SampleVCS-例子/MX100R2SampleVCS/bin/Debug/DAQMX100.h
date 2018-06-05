// DAQMX100.h
/* -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- */
/*
 * Copyright (c) 2004-2007 Yokogawa Electric Corporation. All rights reserved.
 *
 * This is defined export DAQMX100.dll.
 */
/* -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- */
// 2007/09/30 Ver.3 Rev.1
// 2004/11/01 Ver.2 Rev.1
///////////////////////////////////////////////////////////////////////
#ifndef _DAQMX100_H_
#define _DAQMX100_H_
///////////////////////////////////////////////////////////////////////
#if defined(WIN32) || defined(_WIN32_WCE)
// system
#include <windows.h>
// calling
#ifdef DAQMX100_EXPORTS
#define DAQMX_EXPORTS
#define DAQMX100_API __declspec(dllexport)
#else
#define DAQMX100_API __declspec(dllimport)
#endif
#else  //WIN32,WCE
#define DAQMX100_API
#ifndef APIENTRY
#define APIENTRY
#endif
#endif //WIN32,WCE
///////////////////////////////////////////////////////////////////////
#include "DAQMX.h"
///////////////////////////////////////////////////////////////////////
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// value : @see DAQMX.h
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// all
#define DAQMX100_LIST_ALL               (-1)
// if copy, identify as source
#define DAQMX100_LIST_CURRENT           (-1)
// range type
#define DAQMX100_RANGETYPE_DI           (0x010000)
#define DAQMX100_RANGETYPE_SKIP         (0x080000)
// range (special) : Do not use DAQMX_RANGE_DI_xxx
#define DAQMX100_RANGE_DI_LEVEL         (DAQMX100_RANGETYPE_DI | DAQMX_RANGE_DI_LEVEL)
#define DAQMX100_RANGE_DI_CONTACT       (DAQMX100_RANGETYPE_DI | DAQMX_RANGE_DI_CONTACT)
#define DAQMX100_RANGE_SKIP             (DAQMX100_RANGETYPE_SKIP)
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// configure item : @see DAQMXItems.h
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// type
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// DAQ descriptor
// If Visual Basic, type as Long.
// since R3.01 : int -> void *
typedef void * DAQMX100;
///////////////////////////////////////////////////////////////////////
#ifdef __cplusplus
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// LIST
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
class DAQMX100_API CDAQMXList
{
    //-- ---- ---- ---- ---- ---- Constructs
public:
    CDAQMXList(void);
    virtual ~CDAQMXList(void);

    //-- ---- ---- ---- ---- ---- Attributes
protected:
    void ** m_list;
    int m_num; //size of list

    //-- ---- ---- ---- ---- ---- Overrides
  
    //-- ---- ---- ---- ---- ---- Implements
    // idxNo : index number in list (0 origin)
public:
    virtual void initialize(void);
    int getNum(void);   //return number of data
    int getMaxNo(void); //return maximum index number
    int isData(int idxNo);
    // access data
    virtual int create(void);
    virtual void del(int idxNo);
    virtual void copy(int idxNo,
                      int idxSrc); //NOTE: no operation

protected:
    // access data
    int addData(void * pData); //return idxNo
    void delData(int idxNo);
    // access data directly
    void * getData(int idxNo);

    //-- ---- ---- ---- ---- ---- Operations
};
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// DO LIST
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
class DAQMX100_API CDAQMXDOList : public CDAQMXList
{
    //-- ---- ---- ---- ---- ---- Constructs
public:
    CDAQMXDOList(void);
    virtual ~CDAQMXDOList(void);

    //-- ---- ---- ---- ---- ---- Attributes
protected:
    CDAQMXDOData m_cCurrent;

    //-- ---- ---- ---- ---- ---- Overrides
public:
    virtual int create(void);
    virtual void copy(int idxNo,
                      int idxSrc);

    //-- ---- ---- ---- ---- ---- Implements
    // idDO : identify of DOData (index)
    // doNo : do number is same as channel (1 origin)
public:
    // access data
    int add(CDAQMXDOData * pcMXDOData);
    void change(int idDO,
                int doNo,
                int bValid, //DAQMX_VALID_xxx
                int bONOFF = DAQMX_VALID_OFF);
    void copyData(int            idDO,
                  CDAQMXDOData * pcMXDOData);
    // access data directly
    CDAQMXDOData * getClassMXDOData(int idDO);
    // current data
    void initCurrent(void);
    CDAQMXDOData & getCurrent(void);

    //-- ---- ---- ---- ---- ---- Operations
};
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// AO/PWM LIST
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
class DAQMX100_API CDAQMXAOPWMList : public CDAQMXList
{
    //-- ---- ---- ---- ---- ---- Constructs
public:
    CDAQMXAOPWMList(void);
    virtual ~CDAQMXAOPWMList(void);

    //-- ---- ---- ---- ---- ---- Attributes
protected:
    CDAQMXAOPWMData m_cCurrent;

    //-- ---- ---- ---- ---- ---- Overrides
public:
    virtual int create(void);
    virtual void copy(int idxNo,
                      int idxSrc);

    //-- ---- ---- ---- ---- ---- Implements
    // idAOPWM : identify of AOPWMData (index)
    // aopwmNo : AO/PWM number is same as channel (1 origin)
public:
    // access data
    int add(CDAQMXAOPWMData * pcMXAOPWMData);
    void change(int idAOPWM,
                int aopwmNo,
                int bValid,
                int iAOPWMValue = 0);
    void copyData(int idAOPWM,
                  CDAQMXAOPWMData * pcMXAOPWMData);
    // access data directly
    CDAQMXAOPWMData * getClassMXAOPWMData(int idAOPWM);
    // current data
    void initCurrent(void);
    CDAQMXAOPWMData & getCurrent(void);

    //-- ---- ---- ---- ---- ---- Operations
};
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// balance
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
class DAQMX100_API CDAQMXBalanceList : public CDAQMXList
{
    //-- ---- ---- ---- ---- ---- Constructs
public:
    CDAQMXBalanceList(void);
    virtual ~CDAQMXBalanceList(void);

    //-- ---- ---- ---- ---- ---- Attributes
protected:
    CDAQMXBalanceResult m_cCurrent;

    //-- ---- ---- ---- ---- ---- Overrides
public:
    virtual int create(void);
    virtual void copy(int idxNo,
                      int idxSrc);

    //-- ---- ---- ---- ---- ---- Implements
    // idBalance : identify of BalanceData (index)
    // balanceNo : balance number is same as channel (1 origin)
public:
    // access data
    int add(CDAQMXBalanceData * pcMXBalanceData);
    void change(int idBalance,
                int balanceNo,
                int bValid,
                int iValue = 0);
    void copyData(int idBalance,
                  CDAQMXBalanceData * pcMXBalanceData);
    // access data directly
    CDAQMXBalanceData * getClassMXBalanceData(int idBalance);
    // current data
    void initCurrent(void);
    CDAQMXBalanceResult & getCurrent(void);

    //-- ---- ---- ---- ---- ---- Operations
};
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// Transmit LIST
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
class DAQMX100_API CDAQMXTransmitList : public CDAQMXList
{
    //-- ---- ---- ---- ---- ---- Constructs
public:
    CDAQMXTransmitList(void);
    virtual ~CDAQMXTransmitList(void);

    //-- ---- ---- ---- ---- ---- Attributes
protected:
    CDAQMXTransmit m_cCurrent;

    //-- ---- ---- ---- ---- ---- Overrides
public:
    virtual int create(void);
    virtual void copy(int idxNo,
                      int idxSrc);

    //-- ---- ---- ---- ---- ---- Implements
    // idTrans : identify of transmit (index)
    // aopwmNo : AO/PWM number is same as channel (1 origin)
public:
    // access data
    int add(CDAQMXTransmit * pcMXTransmit);
    void change(int idTrans,
                int aopwmNo,
                int iTransmit); //DAQMX_TRANSMIT_xxx
    void copyData(int idTrans,
                  CDAQMXTransmit * pcMXTransmit);
    // access data directly
    CDAQMXTransmit * getClassMXTransmit(int idTrans);
    // current data
    void initCurrent(void);
    CDAQMXTransmit & getCurrent(void);

    //-- ---- ---- ---- ---- ---- Operations
};
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// ITEM CONFIG
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
class DAQMX100_API CDAQMXItemConfig : public CDAQMXConfig
{
    //-- ---- ---- ---- ---- ---- Constructs
public:
    CDAQMXItemConfig(MXConfigData * pMXConfigData = NULL);
    virtual ~CDAQMXItemConfig(void);
  
    //-- ---- ---- ---- ---- ---- Attributes

    //-- ---- ---- ---- ---- ---- Overrides
public:
    virtual int isObject(const char * classname = "CDAQMXItemConfig");

    //-- ---- ---- ---- ---- ---- Implements
    // itemNo : number of configure item as DAQMX100_ITEM_xxx
public:
    //contents
    int readItem(int    itemNo,
                 char * strItem,
                 int    lenItem);
    int writeItem(int          itemNo,
                  const char * strItem);
    //values
    // chNo : channel number is 1 origin
    // levelNo : level number is 1 origin
    // outputNo : output channel number is 1 origin
    int getHisterisys(int chNo,
                      int levelNo);
    double getDoubleHisterisys(int chNo,
                               int levelNo);
    double getDoubleAlarmON(int chNo,
                            int levelNo);
    double getDoubleAlarmOFF(int chNo,
                             int levelNo);
    double getDoubleSpanMin(int chNo);
    double getDoubleSpanMax(int chNo);
    double getDoubleScaleMin(int chNo);
    double getDoubleScaleMax(int chNo);
    double getDoublePresetValue(int outputNo);
    //item
    static int toItemName(int    itemNo,
                          char * strItem,
                          int    lenItem);
    static int toItemNo(const char * strItem);
    static int getMaxLenItemName(void);

    //-- ---- ---- ---- ---- ---- Operations
};
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// Data
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
class DAQMX100_API CDAQMXDataBuffer
{
    //-- ---- ---- ---- ---- ---- Constructs
public:
    CDAQMXDataBuffer(void);
    CDAQMXDataBuffer(CDAQMXChInfo & cMXChInfo);
    virtual ~CDAQMXDataBuffer(void);

    //-- ---- ---- ---- ---- ---- Attributes
protected:
    CDAQMXChInfo m_cMXChInfo;
    CDAQMXDataInfo ** m_pDataBuf;
    CDAQMXDateTime ** m_pTimeBuf;
    int m_num;
    int m_cur;
    int m_max; //deffective

    //-- ---- ---- ---- ---- ---- Overrides

    //-- ---- ---- ---- ---- ---- Implements
public:
    virtual void initialize(void);
    // access attribute
    CDAQMXChInfo & getClassMXChInfo(void);
    // set Data
    int create(int num);
    void setChInfo(CDAQMXChInfo & cMXChInfo);
    int setDataInfo(int              index,
                    CDAQMXDataInfo & cMXDataInfo);
    int setDateTime(int              index,
                    CDAQMXDateTime & cMXDateTime);
    int next(void);
    int getDataNum(void);
    // get Data
    CDAQMXDataInfo * currentDataInfo(void);
    CDAQMXDateTime * currentDateTime(void);
    int isCurrent(void);

protected:
    CDAQMXDataInfo * getDataInfo(int index);
    CDAQMXDateTime * getDateTime(int index);

    //-- ---- ---- ---- ---- ---- Operations
};
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// Handler
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
class DAQMX100_API CDAQMX100 : public CDAQMX
{
    //-- ---- ---- ---- ---- ---- Constructs
public:
    CDAQMX100(void);
    CDAQMX100(const char * strAddress,
              unsigned int uiPort = DAQMX_COMMPORT,
              int *        errCode = NULL);
    virtual ~CDAQMX100(void);

    //-- ---- ---- ---- ---- ---- Attributes
protected:
    // for measurement
    CDAQMXItemConfig m_cMXItemConfig;
    CDAQMXDataBuffer m_cMXDataBuffer[DAQMX_NUMCHANNEL];
    // for user
    CDAQMXDOList m_cMXDOList;
    CDAQMXAOPWMList m_cMXAOPWMList;
    CDAQMXTransmitList m_cMXTransmitList;
    CDAQMXBalanceList m_cMXBalanceList;

    //-- ---- ---- ---- ---- ---- Overrides
public:
    virtual int open(const char * strAddress,
                     unsigned int uiPort = DAQMX_COMMPORT);
    virtual int isObject(const char * classname = "CDAQMX100");
    // control with update
    virtual int setDateTime(CDAQMXDateTime * pcMXDateTime = NULL);
    virtual int formatCF(void);

    //-- ---- ---- ---- ---- ---- Implements
    // chNo : channel number is 1 origin
    // moduleNo : module number is 0 origin
    // doNo : do number is same as channel (1 origin)
    // levelNo : level number is 1 origin
    // dispPattern : hexdecimal 0-9a-f, if negative, none
public:
    // access attributes
    CDAQMXItemConfig & getClassMXItemConfig(void);
    CDAQMXDataBuffer * getClassMXDataBuffer(int chNo);
    CDAQMXDOList & getClassMXDOList(void);
    CDAQMXAOPWMList & getClassMXAOPWMList(void);
    CDAQMXTransmitList & getClassMXTransmitList(void);
    CDAQMXBalanceList & getClassMXBalanceList(void);
    // FIFO
    int measStart(void);
    int measStop(void);
    // control
    int switchBackup(int bBackup);
    int reconstruct(void);
    int initSetValue(void);
    int ackAlarm(void);
    int displaySegment(int dispPattern0,
                       int dispPattern1,
                       int dispType, //DAQMX_DISPTYPE_xxx
                       int dispTime);
    void initDataCh(int chNo = DAQMX_CHNO_ALL);
    void initDataFIFO(int fifoNo = DAQMX_FIFONO_ALL);
    // setup range
    int setRange(int chNo,
                 int iRange); //DAQMX_RANGE_xxx
    int setChDELTA(int chNo,
                   int refChNo,
                   int iRange = DAQMX_RANGE_REFERENCE);
    int setChRRJC(int chNo,
                  int refChNo);
    // setup channel
    int setChUnit(int          chNo,
                  const char * strUnit);
    int setChTag(int          chNo,
                 const char * strTag);
    int setChComment(int          chNo,
                     const char * strComment);
    int setSpan(int    chNo,
                double spanMin,
                double spanMax);
    int setSpan(int chNo,
                int spanMin = 0,
                int spanMax = 0);
    int setScale(int    chNo,
                 double scaleMin,
                 double scaleMax,
                 int    scalePoint);
    int setScale(int chNo,
                 int scaleMin = 0,
                 int scaleMax = 0,
                 int scalePoint = 0);
    int setAlarm(int    chNo,
                 int    levelNo,
                 int    iAlarmType, //DAQMX_ALARM_xxx
                 double valueON,
                 double valueOFF);
    int setAlarm(int chNo,
                 int levelNo,
                 int iAlarmType = DAQMX_ALARM_NONE,
                 int valueON = 0,
                 int valueOFF = 0);
    int setHisterisys(int    chNo,
                      int    levelNo,
                      double histerisys);
    int setHisterisys(int chNo,
                      int levelNo,
                      int histerisys = 0);
    int setFilter(int chNo,
                  int iFilter); //DAQMX_FILTER_xxx
    int setRJCType(int chNo,
                   int iRJCType, //DAQMX_RJC_xxx
                   int volt = 0); //uV
    int setBurnout(int chNo,
                   int iBurnout); //DAQMX_BURNOUT_xxx
    int setDeenergize(int doNo,
                      int bDeenergize);
    int setHold(int doNo,
                int bHold);
    int setRefAlarm(int doNo,
                    int refChNo,
                    int levelNo,
                    int bValid);
    int setChKind(int chNo,
                  int iKind, //DAQMX_CHKIND_xxx
                  int refChNo = DAQMX_REFCHNO_NONE);
    // setup module
    int setInterval(int moduleNo,
                    int iInterval); //DAQMX_INTERVAL_xxx
    int setIntegral(int moduleNo,
                    int iHz); //DAQMX_INTEGRAL_xxx
    // setup unit
    int setUnitNo(int unitNo);
    int setUnitTemp(int iTempUnit); //DAQMX_TEMPUNIT_xxx
    int setCFWriteMode(int iCFWriteMode); //DAQMX_CFWRITEMODE_xxx
    // setup output
    int setOutputType(int outputNo,
                      int iOutput); //DAQMX_OUTPUT_xxx
    int setChoice(int outputNo,
                  int idleChoice,  //DAQMX_CHOICE_xxx
                  int errorChoice, //DAQMX_CHOICE_xxx
                  int presetValue);
    int setChoice(int    outputNo,
                  int    idleChoice,  //DAQMX_CHOICE_xxx
                  int    errorChoice, //DAQMX_CHOICE_xxx
                  double presetValue);
    int setPulseTime(int outputNo,
                     int pulseTime);
    // control data (DO/AO/PWM/transmit)
    int commandDO(int idDO);
    int commandDO(CDAQMXDOData & cMXDOData);
    int switchDO(int idDO,
                 int bONOFF);
    void changeAOPWMValue(int    idAOPWM,
                          int    aopwmNo,
                          int    bValid,
                          double realValue);
    int commandAOPWM(int idAOPWM);
    int commandAOPWM(CDAQMXAOPWMData & cMXAOPWMData);
    int reloadBalance(int idBalance);
    int reloadBalance(CDAQMXBalanceData & cMXBalanceData);
    int commandTransmit(int idTrans);
    int commandTransmit(CDAQMXTransmit & cMXTransmit);
    int switchTransmit(int idTrans,
                       int iTransmit);
    // update attributes getting by manual
    int updateStatus(void);
    int updateSystem(void);
    int updateConfig(void);
    int updateDOData(void);
    int updateAOPWMData(void);
    int updateBalance(void);
    int updateOutput(void);
    int updateInfoCh(int chNo = DAQMX_CHNO_ALL);
    // measurement
    int measDataCh(int chNo = DAQMX_CHNO_ALL);
    int measDataFIFO(int fifoNo = DAQMX_FIFONO_ALL);
    int measInstCh(int chNo = DAQMX_CHNO_ALL);
    int measInstFIFO(int fifoNo = DAQMX_FIFONO_ALL);
    // setup
    int sendConfig(void); //@see m_cMXItemConfig
    int initBalance(void); //run
    int clearBalance(void); //reset
    // item
    int getItemAll(void);
    int setItemAll(void);
    // double values
    double currentDoubleAOPWMValue(int aopwmNo);
    double userDoubleAOPWMValue(int idAOPWM,
                                int aopwmNo);
    // utility
    int getDataNum(int fifoNo);
    int toChNo(int fifoNo,
               int fifoIndex);
    //-- ----- ---- since R3.01
    // setup Pulse
    int setChatFilter(int chNo,
                      int bChatFilter);

protected:
    void measClear(void);
    void userClear(void);
    // update attributes
    int updateAll(void);
    int updateRenew(void);
    // measurement
    int getDataCh(int   chNo,
                  int * bComm);
    int getDataFIFO(int   fifoNo,
                    int * bComm);
    int getInstCh(int chNo);
    int getInstFIFO(int fifoNo);
    int nextFIFO(int fifoNo);
    // DLL version
    static const int getVersionMX100DLL(void);
    static const int getRevisionMX100DLL(void);

    //-- ---- ---- ---- ---- ---- Operations
};
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
#endif //__cplusplus
///////////////////////////////////////////////////////////////////////
#ifdef __cplusplus
extern "C" {
#endif
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
/**
 * The simple algorithm is as follows :
 * <pre>
 * <code>
 * DAQMX100 daqmx100 = openMX100("192.168.1.12", NULL);
 * measStartMX100(daqmx100);
 * measDataChMX100(daqmx100, 1);
 * dataValueMX100(daqmx100, 1);
 * measStopMX100(daqmx100);
 * closeMX100(daqmx100);
 * </code>
 * </pre>
 */
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// Connection
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
/**
 * This opens the instrument with initialize.
 * This returns NULL descriptor, if error occured.
 * @param strAddress specified an address as string.
 * @param errCode stored an error code if error occured.
 * @return an instrument descriptor.
 */
DAQMX100_API DAQMX100 APIENTRY openMX100(const char * strAddress,
                                         int *        errCode);
#define OPEN_MX100(address) openMX100(address, NULL);
/**
 * This closes the instrument.
 * @param daqmx100 specified an instrument descriptor.
 * @return an error code that is not zero if error ocuured.
 */
DAQMX100_API int APIENTRY closeMX100(DAQMX100 daqmx100);
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// FIFO
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
/**
 * This starts measurement.
 * @param daqmx100 specified an instrument descriptor.
 * @return an error code that is not zero if error ocuured.
 */
DAQMX100_API int APIENTRY measStartMX100(DAQMX100 daqmx100);
/**
 * This stops measurement.
 * @param daqmx100 specified an instrument descriptor.
 * @return an error code that is not zero if error ocuured.
 */
DAQMX100_API int APIENTRY measStopMX100(DAQMX100 daqmx100);
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// Control
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
/**
 * This sets date and time, now.
 * @param daqmx100 specified an instrument descriptor.
 * @return an error code that is not zero if error ocuured.
 */
DAQMX100_API int APIENTRY setDateTimeNowMX100(DAQMX100 daqmx100);
/**
 * This switches backup mode.
 * @param daqmx100 specified an instrument descriptor.
 * @param bBackup specified a backup mode as boolean.
 * @return an error code that is not zero if error ocuured.
 */
DAQMX100_API int APIENTRY switchBackupMX100(DAQMX100 daqmx100,
                                            int      bBackup); 
/**
 * This formats a CF card.
 * @param daqmx100 specified an instrument descriptor.
 * @return an error code that is not zero if error ocuured.
 */
DAQMX100_API int APIENTRY formatCFMX100(DAQMX100 daqmx100);
/**
 * This reconstructs the instrument.
 * @param daqmx100 specified an instrument descriptor.
 * @return an error code that is not zero if error ocuured.
 */
DAQMX100_API int APIENTRY reconstructMX100(DAQMX100 daqmx100);
/**
 * This initializes setting values.
 * @param daqmx100 specified an instrument descriptor.
 * @return an error code that is not zero if error ocuured.
 */
DAQMX100_API int APIENTRY initSetValueMX100(DAQMX100 daqmx100);
/**
 * This sends acknowledge alarm.
 * @param daqmx100 specified an instrument descriptor.
 * @return an error code that is not zero if error ocuured.
 */
DAQMX100_API int APIENTRY ackAlarmMX100(DAQMX100 daqmx100);
/**
 * This displays segments.
 * @param daqmx100 specified an instrument descriptor.
 * @param dispPattern0 specified a pattern of segent number 0.
 * @param dispPattern1 specified a pattern of segent number 1.
 * @param dispType specified a dipplay type as DAQMX_DISPTYPE_xxx.
 * @param dispTime specified a display time. Maximaum is DAQMX_MAXDISPTIME.
 * @return an error code that is not zero if error ocuured.
 */
DAQMX100_API int APIENTRY displaySegmentMX100(DAQMX100 daqmx100,
                                              int      dispPattern0,
                                              int      dispPattern1,
                                              int      dispType,
                                              int      dispTime); 
/**
 * This initializes the data buffer as channel number.
 * @param daqmx100 specified an instrument descriptor.
 * @param chNo specified a channel number.
 * @return an error code that is not zero if error ocuured.
 */
DAQMX100_API int APIENTRY initDataChMX100(DAQMX100 daqmx100,
                                          int      chNo);
/**
 * This initializes the data buffer as FIFO number.
 * @param daqmx100 specified an instrument descriptor.
 * @param fifoNo specified a FIFO number.
 * @return an error code that is not zero if error ocuured.
 */
DAQMX100_API int APIENTRY initDataFIFOMX100(DAQMX100 daqmx100,
                                            int      fifoNo);
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// Setup
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
/**
 * This sets configure.
 * @param daqmx100 specified an instrument descriptor.
 * @return an error code that is not zero if error ocuured.
 */
DAQMX100_API int APIENTRY sendConfigMX100(DAQMX100 daqmx100);
/**
 * This runs initial balance.
 * @param daqmx100 specified an instrument descriptor.
 * @return an error code that is not zero if error ocuured.
 */
DAQMX100_API int APIENTRY initBalanceMX100(DAQMX100 daqmx100);
/**
 * This resets balance.
 * @param daqmx100 specified an instrument descriptor.
 * @return an error code that is not zero if error ocuured.
 */
DAQMX100_API int APIENTRY clearBalanceMX100(DAQMX100 daqmx100);
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// Setting
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
/**
 * This sets range.
 * NOTE: DI range is detail.
 * @param daqmx100 specified an instrument descriptor.
 * @param chNo specified a channel number.
 * @param iRange specified a selection of the range as DAQMX_RANGE_xxx
 * @return an error code that is not zero if error ocuured.
 */
DAQMX100_API int APIENTRY setRangeMX100(DAQMX100 daqmx100,
                                        int      chNo,
                                        int      iRange);
/**
 * This sets DELTA.
 * @param daqmx100 specified an instrument descriptor.
 * @param chNo specified a channel number.
 * @param refChNo specified a reference channel number.
 * @param iRange specified a selection of the range as DAQMX_RANGE_xxx
 * @return an error code that is not zero if error ocuured.
 */
DAQMX100_API int APIENTRY setChDELTAMX100(DAQMX100 daqmx100,
                                          int      chNo,
                                          int      refChNo,
                                          int      iRange);
/**
 * Thsi sets RRJC.
 * @param daqmx100 specified an instrument descriptor.
 * @param chNo specified a channel number.
 * @param refChNo specified a reference channel number.
 * @return an error code that is not zero if error ocuured.
 */
DAQMX100_API int APIENTRY setChRRJCMX100(DAQMX100 daqmx100,
                                         int      chNo,
                                         int      refChNo);
/**
 * This sets an unit name.
 * @param daqmx100 specified an instrument descriptor.
 * @param chNo specified a channel number.
 * @param strUnit specified a string of the unit name.
 * @return an error code that is not zero if error ocuured.
 */
DAQMX100_API int APIENTRY setChUnitMX100(DAQMX100     daqmx100,
                                         int          chNo,
                                         const char * strUnit);
/**
 * This sets a tag name.
 * @param daqmx100 specified an instrument descriptor.
 * @param chNo specified a channel number.
 * @param strTag specified a string of the tag name.
 * @return an error code that is not zero if error ocuured.
 */
DAQMX100_API int APIENTRY setChTagMX100(DAQMX100     daqmx100,
                                        int          chNo,
                                        const char * strTag);
/**
 * This sets a comment.
 * @param daqmx100 specified an instrument descriptor.
 * @param chNo specified a channel number.
 * @param strComment specified a string of the comment.
 * @return an error code that is not zero if error ocuured.
 */
DAQMX100_API int APIENTRY setChCommentMX100(DAQMX100     daqmx100,
                                            int          chNo,
                                            const char * strComment);
/**
 * This sets span.
 * @param daqmx100 specified an instrument descriptor.
 * @param chNo specified a channel number.
 * @param spanMin specified a value of the minimum span.
 * @param spanMax specified a value of the maximum span.
 * @return an error code that is not zero if error ocuured.
 */
DAQMX100_API int APIENTRY setSpanMX100(DAQMX100 daqmx100,
                                       int      chNo,
                                       int      spanMin,
                                       int      spanMax);
/**
 * This sets span with double values.
 * @param daqmx100 specified an instrument descriptor.
 * @param chNo specified a channel number.
 * @param spanMin specified a value of the minimum span.
 * @param spanMax specified a value of the maximum span.
 * @return an error code that is not zero if error ocuured.
 */
DAQMX100_API int APIENTRY setDoubleSpanMX100(DAQMX100 daqmx100,
                                             int      chNo,
                                             double   spanMin,
                                             double   spanMax);
/**
 * This sets scalling.
 * @param daqmx100 specified an instrument descriptor.
 * @param chNo specified a channel number.
 * @param scaleMin specified a value of the minimum scale.
 * @param scaleMax specified a value of the maximum scale.
 * @param scalePoint specified a value of the decimal point positon.
 * @return an error code that is not zero if error ocuured.
 */
DAQMX100_API int APIENTRY setScaleMX100(DAQMX100 daqmx100,
                                        int      chNo,
                                        int      scaleMin,
                                        int      scaleMax,
                                        int      scalePoint);
/**
 * This sets scalling with double value.
 * @param daqmx100 specified an instrument descriptor.
 * @param chNo specified a channel number.
 * @param scaleMin specified a value of the minimum scale.
 * @param scaleMax specified a value of the maximum scale.
 * @param scalePoint specified a value of the decimal point positon.
 * @return an error code that is not zero if error ocuured.
 */
DAQMX100_API int APIENTRY setDoubleScaleMX100(DAQMX100 daqmx100,
                                              int      chNo,
                                              double   scaleMin,
                                              double   scaleMax,
                                              int      scalePoint);
/**
 * This sets an alarm.
 * @param daqmx100 specified an instrument descriptor.
 * @param chNo specified a channel number.
 * @param levelNo specified a level number.
 * @param iAlarmType specified a selection of the alarm type as DAQMX_ALARM_xxx.
 * @param value specified a value of an alarm ON without decimal point position.
 * @return an error code that is not zero if error ocuured.
 */
DAQMX100_API int APIENTRY setAlarmMX100(DAQMX100 daqmx100,
                                        int      chNo,
                                        int      levelNo,
                                        int      iAlarmType,
                                        int      value);
/**
 * This sets an alarm with double value.
 * @param daqmx100 specified an instrument descriptor.
 * @param chNo specified a channel number.
 * @param levelNo specified a level number.
 * @param iAlarmType specified a selection of the alarm type as DAQMX_ALARM_xxx.
 * @param value specified a value of an alarm ON without decimal point position.
 * @return an error code that is not zero if error ocuured.
 */
DAQMX100_API int APIENTRY setDoubleAlarmMX100(DAQMX100 daqmx100,
                                              int      chNo,
                                              int      levelNo,
                                              int      iAlarmType,
                                              double   value);
/**
 * This sets an alarm with ON and OFF.
 * @param daqmx100 specified an instrument descriptor.
 * @param chNo specified a channel number.
 * @param levelNo specified a level number.
 * @param iAlarmType specified a selection of the alarm type as DAQMX_ALARM_xxx.
 * @param valueON specified a value of an alarm ON without decimal point position.
 * @param valueOFF specified a value of an alarm OFF without decimal point position.
 * @return an error code that is not zero if error ocuured.
 */
DAQMX100_API int APIENTRY setAlarmValueMX100(DAQMX100 daqmx100,
                                             int      chNo,
                                             int      levelNo,
                                             int      iAlarmType,
                                             int      valueON,
                                             int      valueOFF);
/**
 * This sets an alarm with ON and OFF as double.
 * @param daqmx100 specified an instrument descriptor.
 * @param chNo specified a channel number.
 * @param levelNo specified a level number.
 * @param iAlarmType specified a selection of the alarm type as DAQMX_ALARM_xxx.
 * @param valueON specified a value of an alarm ON without decimal point position.
 * @param valueOFF specified a value of an alarm OFF without decimal point position.
 * @return an error code that is not zero if error ocuured.
 */
DAQMX100_API int APIENTRY setDoubleAlarmValueMX100(DAQMX100 daqmx100,
                                                   int      chNo,
                                                   int      levelNo,
                                                   int      iAlarmType,
                                                   double   valueON,
                                                   double   valueOFF);
/**
 * This sets a histerisys.
 * @param daqmx100 specified an instrument descriptor.
 * @param chNo specified a channel number.
 * @param levelNo specified a level number.
 * @param histerisys specified an offset of the value.
 * @return an error code that is not zero if error ocuured.
 */
DAQMX100_API int APIENTRY setHisterisysMX100(DAQMX100 daqmx100,
                                             int      chNo,
                                             int      levelNo,
                                             int      histerisys);
/**
 * This sets a histerisys as double.
 * @param daqmx100 specified an instrument descriptor.
 * @param chNo specified a channel number.
 * @param levelNo specified a level number.
 * @param histerisys specified an offset of the value.
 * @return an error code that is not zero if error ocuured.
 */
DAQMX100_API int APIENTRY setDoubleHisterisysMX100(DAQMX100 daqmx100,
                                                   int      chNo,
                                                   int      levelNo,
                                                   double   histerisys);
/**
 * This sets a filter.
 * @param daqmx100 specified an instrument descriptor.
 * @param chNo specified a channel number.
 * @param iFilter specified a selection of the filter as DAQMX_FILTER_xxx.
 * @return an error code that is not zero if error ocuured.
 */
DAQMX100_API int APIENTRY setFilterMX100(DAQMX100 daqmx100,
                                         int      chNo,
                                         int      iFilter);
/**
 * This sets a RJC on TC channels.
 * @param daqmx100 specified an instrument descriptor.
 * @param chNo specified a channel number.
 * @param iRJCType specified a selection of the RJC type as DAQMX_RJC_xxx.
 * @param volt specified a value of the voltage (uV) on external RJC.
 * @return an error code that is not zero if error ocuured.
 */
DAQMX100_API int APIENTRY setRJCTypeMX100(DAQMX100 daqmx100,
                                          int      chNo,
                                          int      iRJCType,
                                          int      volt);
/**
 * This sets a burnout.
 * @param daqmx100 specified an instrument descriptor.
 * @param chNo specified a channel number.
 * @param iBurnout specified a selection of the burnout as DAQMX_BURNOUT_xxx.
 * @return an error code that is not zero if error ocuured.
 */
DAQMX100_API int APIENTRY setBurnoutMX100(DAQMX100 daqmx100,
                                          int      chNo,
                                          int      iBurnout);
/**
 * This sets deenergize.
 * @param daqmx100 specified an instrument descriptor.
 * @param doNo specified a DO number as channel number.
 * @param bDeenergize specified a deenergize as boolean.
 * @return an error code that is not zero if error ocuured.
 */
DAQMX100_API int APIENTRY setDeenergizeMX100(DAQMX100 daqmx100,
                                             int      doNo,
                                             int      bDeenergize);
/**
 * This sets hold.
 * @param daqmx100 specified an instrument descriptor.
 * @param doNo specified a DO number as channel number.
 * @param bHold specified a hold as boolean.
 * @return an error code that is not zero if error ocuured.
 */
DAQMX100_API int APIENTRY setHoldMX100(DAQMX100 daqmx100,
                                       int      doNo,
                                       int      bHold);
/**
 * This sets a reference alarm on DO.
 * @param daqmx100 specified an instrument descriptor.
 * @param doNo specified a DO number as channel number.
 * @param refChNo specified a reference channel number. If all, DAQMX_REFCHNO_ALL.
 * @param levelNo specified a level number. If all, DAQMX_LEVELNO_ALL.
 * @param bValid specified validation as boolean.
 * @return an error code that is not zero if error ocuured.
 */
DAQMX100_API int APIENTRY setRefAlarmMX100(DAQMX100 daqmx100,
                                           int      doNo,
                                           int      refChNo,
                                           int      levelNo,
                                           int      bValid);
/**
 * This sets a channel kind on configure.
 * @param daqmx100 specified an instrument descriptor.
 * @param chNo specified a channel number.
 * @param iKind specified a channel kind as DAQMX_CHKIND_xxx
 * @return an error code that is not zero if error ocuured.
 */
DAQMX100_API int APIENTRY setChKindMX100(DAQMX100 daqmx100,
                                         int      chNo,
                                         int      iKind,
                                         int      refChNo);
/**
 * This sets a chatering filter.
 * @param daqmx100 specified an instrument descriptor.
 * @param chNo specified a channel number.
 * @param bChatFilter specified a charering filter as boolean.
 * @return an error code that is not zero if error ocuured.
 * @since R3.01
 */
DAQMX100_API int APIENTRY setChatFilterMX100(DAQMX100 daqmx100,
                                             int      chNo,
                                             int      bChatFilter);
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
/**
 * This sets an interval for module.
 * @param daqmx100 specified an instrument descriptor.
 * @param moduleNo specified a module number. If all, DAQMX_MODULENO_ALL.
 * @param iInterval specified an interval as DAQMX_INTERVAL_xxx.
 * @return an error code that is not zero if error ocuured.
 */
DAQMX100_API int APIENTRY setIntervalMX100(DAQMX100 daqmx100,
                                           int      moduleNo,
                                           int      iInterval);
/**
 * This sets an integral for module.
 * @param daqmx100 specified an instrument descriptor.
 * @param moduleNo specified a module number. If all, DAQMX_MODULENO_ALL.
 * @param iIntegral specified an integral time as DAQMX_INTEGRAL_xxx.
 * @return an error code that is not zero if error ocuured.
 */
DAQMX100_API int APIENTRY setIntegralMX100(DAQMX100 daqmx100,
                                           int      moduleNo,
                                           int      iIntegral);
/**
 * This sets an unit number defined by the user.
 * @param daqmx100 specified an instrument descriptor.
 * @param unitNo specified an unit number as integer.
 * @return an error code that is not zero if error ocuured.
 */
DAQMX100_API int APIENTRY setUnitNoMX100(DAQMX100 daqmx100,
                                         int      unitNo);
/**
 * This sets a temperature unit.
 * @param daqmx100 specified an instrument descriptor.
 * @param iTempUnit specified a selection of the temperature unit as DAQMX_TEMPUNIT_xxx.
 * @return an error code that is not zero if error ocuured.
 */
DAQMX100_API int APIENTRY setUnitTempMX100(DAQMX100 daqmx100,
                                           int      iTempUnit);
/**
 * This sets a writing mode for CF.
 * @param daqmx100 specified an instrument descriptor.
 * @param iCDWriteMode specified a mode of writing CF as DAQMX_CFWRITEMODE_xxx
 * @return an error code that is not zero if error ocuured.
 */
DAQMX100_API int APIENTRY setCFWriteModeMX100(DAQMX100 daqmx100,
                                              int      iCFWriteMode);
/**
 * This sets an output type.
 * @param daqmx100 specified an instrument descriptor.
 * @param outputNo specified an output channel number.
 * @param iOutput specified an output type as DAQMX_OUTPUT_xxx
 * @return an error code that is not zero if error ocuured.
 */
DAQMX100_API int APIENTRY setOutputTypeMX100(DAQMX100 daqmx100,
                                             int      outputNo,
                                             int      iOutput);
/**
 * This sets choices.
 * @param daqmx100 specified an instrument descriptor.
 * @param outputNo specified an output channel number.
 * @param idleChoice specified a choice on idle as DAQMX_CHOICE_xxx
 * @param errorChoice specified a choice on error as DAQMX_CHOICE_xxx
 * @param presetValue specified a value on DAQMX_CHOICE_PRESET.
 * @return an error code that is not zero if error ocuured.
 */
DAQMX100_API int APIENTRY setChoiceMX100(DAQMX100 daqmx100,
                                         int      outputNo,
                                         int      idleChoice,
                                         int      errorChoice,
                                         int      presetValue);
/**
 * This sets choices as double.
 * @param daqmx100 specified an instrument descriptor.
 * @param outputNo specified an output channel number.
 * @param idleChoice specified a choice on idle as DAQMX_CHOICE_xxx
 * @param errorChoice specified a choice on error as DAQMX_CHOICE_xxx
 * @param presetValue specified a value on DAQMX_CHOICE_PRESET.
 * @return an error code that is not zero if error ocuured.
 */
DAQMX100_API int APIENTRY setDoubleChoiceMX100(DAQMX100 daqmx100,
                                               int      outputNo,
                                               int      idleChoice,
                                               int      errorChoice,
                                               double   presetValue);
/**
 * This sets a pulse time.
 * @param daqmx100 specified an instrument descriptor.
 * @param outputNo specified an output channel number.
 * @param pulseTime specified a pulse time.
 * @return an error code that is not zero if error ocuured.
 */
DAQMX100_API int APIENTRY setPulseTimeMX100(DAQMX100 daqmx100,
                                            int      outputNo,
                                            int      pulseTime);
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// Data Operation
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
/**
 * This creates DO data.
 * @param daqmx100 specified an instrument descriptor.
 * @param errCode stored an error code if error occured.
 * @return an identifier of DO data.
 */
DAQMX100_API int APIENTRY createDOMX100(DAQMX100 daqmx100,
                                        int *    errCode);
/**
 * This deletes DO data
 * @param daqmx100 specified an instrument descriptor.
 * @param idDO specified an identifier of DO data.
 * @return an error code that is not zero if error ocuured.
 */
DAQMX100_API int APIENTRY deleteDOMX100(DAQMX100 daqmx100,
                                        int      idDO);
/**
 * This changes DO data.
 * @param daqmx100 specified an instrument descriptor.
 * @param idDO specified an identifier of DO data.
 * @param doNo specified a DO channel number.
 * @param bValid specified a effective as boolean.
 * @param bONOFF specified ON/OFF as boolean.
 * @return an error code that is not zero if error ocuured.
 */
DAQMX100_API int APIENTRY changeDOMX100(DAQMX100 daqmx100,
                                        int      idDO,
                                        int      doNo,
                                        int      bValid,
                                        int      bONOFF);
/**
 * This copies DO data.
 * @param daqmx100 specified an instrument descriptor.
 * @param idDO specified an identifier of DO data.
 * @param idDOSrc specified an identifier of source.
 * @return an error code that is not zero if error ocuured.
 */
DAQMX100_API int APIENTRY copyDOMX100(DAQMX100 daqmx100,
                                      int      idDO,
                                      int      idDOSrc);
/**
 * This runs command DO.
 * @param daqmx100 specified an instrument descriptor.
 * @param idDO specified an identifier of DO data.
 * @return an error code that is not zero if error ocuured.
 */
DAQMX100_API int APIENTRY commandDOMX100(DAQMX100 daqmx100,
                                         int      idDO);
/**
 * This switches ON/OFF with DO Data.
 * @param daqmx100 specified an instrument descriptor.
 * @param idDO specified an identifier of DO data.
 * @param bONOFF specified a ON/OFF.
 * @return an error code that is not zero if error ocuured.
 */
DAQMX100_API int APIENTRY switchDOMX100(DAQMX100 daqmx100,
                                        int      idDO,
                                        int      bONOFF);
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
/**
 * This creates AO/PWM data.
 * @param daqmx100 specified an instrument descriptor.
 * @param errCode stored an error code if error occured.
 * @return an identifier of AO/PWM data.
 */
DAQMX100_API int APIENTRY createAOPWMMX100(DAQMX100 daqmx100,
                                           int *    errCode);
/**
 * This deletes AO/PWM data
 * @param daqmx100 specified an instrument descriptor.
 * @param idAOPWM specified an identifier of AO/PWM data.
 * @return an error code that is not zero if error ocuured.
 */
DAQMX100_API int APIENTRY deleteAOPWMMX100(DAQMX100 daqmx100,
                                           int      idAOPWM);
/**
 * This changes AO/PWM data.
 * @param daqmx100 specified an instrument descriptor.
 * @param idAOPWM specified an identifier of AO/PWM data.
 * @param aopwmNo specified a AO/PWM channel number.
 * @param bValid specified effective as boolean.
 * @param iAOPWMValue specified output as integer.
 * @return an error code that is not zero if error ocuured.
 */
DAQMX100_API int APIENTRY changeAOPWMMX100(DAQMX100 daqmx100,
                                           int      idAOPWM,
                                           int      aopwmNo,
                                           int      bValid,
                                           int      iAOPWMValue);
/**
 * This changes AO/PWM data with real value.
 * @param daqmx100 specified an instrument descriptor.
 * @param idAOPWM specified an identifier of AO/PWM data.
 * @param aopwmNo specified a AO/PWM channel number.
 * @param bValid specified effective as boolean.
 * @param realValue specified real output as double.
 * @return an error code that is not zero if error ocuured.
 */
DAQMX100_API int APIENTRY changeAOPWMValueMX100(DAQMX100 daqmx100,
                                                int      idAOPWM,
                                                int      aopwmNo,
                                                int      bValid,
                                                double   realValue);
/**
 * This copies AO/PWM data.
 * @param daqmx100 specified an instrument descriptor.
 * @param idAOPWM specified an identifier of AO/PWM data.
 * @param idAOPWMSrc specified an identifier of source.
 * @return an error code that is not zero if error ocuured.
 */
DAQMX100_API int APIENTRY copyAOPWMMX100(DAQMX100 daqmx100,
                                         int      idAOPWM,
                                         int      idAOPWMSrc);
/**
 * This runs command AO/PWM.
 * @param daqmx100 specified an instrument descriptor.
 * @param idAOPWM specified an identifier of AO/PWM data.
 * @return an error code that is not zero if error ocuured.
 */
DAQMX100_API int APIENTRY commandAOPWMMX100(DAQMX100 daqmx100,
                                            int      idAOPWM);
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
/**
 * This creates balance data.
 * @param daqmx100 specified an instrument descriptor.
 * @param errCode stored an error code if error occured.
 * @return an identifier of balance data.
 */
DAQMX100_API int APIENTRY createBalanceMX100(DAQMX100 daqmx100,
                                             int *    errCode);
/**
 * This deletes balance data
 * @param daqmx100 specified an instrument descriptor.
 * @param idBalance specified an identifier of balance data.
 * @return an error code that is not zero if error ocuured.
 */
DAQMX100_API int APIENTRY deleteBalanceMX100(DAQMX100 daqmx100,
                                             int      idBalance);
/**
 * This changes balance data.
 * @param daqmx100 specified an instrument descriptor.
 * @param idBalance specified an identifier of balance data.
 * @param balanceNo specified a balance (strain) channel number.
 * @param bValid specified effective as boolean.
 * @param iValue specified an initial balance as integer.
 * @return an error code that is not zero if error ocuured.
 */
DAQMX100_API int APIENTRY changeBalanceMX100(DAQMX100 daqmx100,
                                             int      idBalance,
                                             int      balanceNo,
                                             int      bValid,
                                             int      iValue);
/**
 * This copies balance data.
 * @param daqmx100 specified an instrument descriptor.
 * @param idBalance specified an identifier of balance data.
 * @param idBalanceSrc specified an identifier of source.
 * @return an error code that is not zero if error ocuured.
 */
DAQMX100_API int APIENTRY copyBalanceMX100(DAQMX100 daqmx100,
                                           int      idBalance,
                                           int      idBalanceSrc);
/**
 * This runs reloading balance.
 * @param daqmx100 specified an instrument descriptor.
 * @param idBalance specified an identifier of balance data.
 * @return an error code that is not zero if error ocuured.
 */
DAQMX100_API int APIENTRY commandBalanceMX100(DAQMX100 daqmx100,
                                              int      idBalance);
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
/**
 * This creates transmit data.
 * @param daqmx100 specified an instrument descriptor.
 * @param errCode stored an error code if error occured.
 * @return an identifier of transmit data.
 */
DAQMX100_API int APIENTRY createTransmitMX100(DAQMX100 daqmx100,
                                              int *    errCode);
/**
 * This deletes transmit data
 * @param daqmx100 specified an instrument descriptor.
 * @param idTrans specified an identifier of transmit data.
 * @return an error code that is not zero if error ocuured.
 */
DAQMX100_API int APIENTRY deleteTransmitMX100(DAQMX100 daqmx100,
                                              int      idTrans);
/**
 * This changes transmit data.
 * @param daqmx100 specified an instrument descriptor.
 * @param idTrans specified an identifier of transmit data.
 * @param aopwmNo specified a transmit (AO/PWM) channel number.
 * @param iTransmit specified transmit as DAQMX_TRANSMIT_xxx
 * @return an error code that is not zero if error ocuured.
 */
DAQMX100_API int APIENTRY changeTransmitMX100(DAQMX100 daqmx100,
                                              int      idTrans,
                                              int      aopwmNo,
                                              int      iTransmit);
/**
 * This copies transmit data.
 * @param daqmx100 specified an instrument descriptor.
 * @param idTrans specified an identifier of transmit data.
 * @param idTransSrc specified an identifier of source.
 * @return an error code that is not zero if error ocuured.
 */
DAQMX100_API int APIENTRY copyTransmitMX100(DAQMX100 daqmx100,
                                            int      idTrans,
                                            int      idTransSrc);
/**
 * This runs command transmit.
 * @param daqmx100 specified an instrument descriptor.
 * @param idTrans specified an identifier of transmit data.
 * @return an error code that is not zero if error ocuured.
 */
DAQMX100_API int APIENTRY commandTransmitMX100(DAQMX100 daqmx100,
                                               int      idTrans);
/**
 * This switches transmit with transmit Data.
 * @param daqmx100 specified an instrument descriptor.
 * @param idTrans specified an identifier of transmit data.
 * @param iTransmit specified transmit as DAQMX_TRANSMIT_xxx
 * @return an error code that is not zero if error ocuured.
 */
DAQMX100_API int APIENTRY switchTransmitMX100(DAQMX100 daqmx100,
                                              int      idTrans,
                                              int      iTransmit);
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// Update
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
/**
 * This updates status.
 * @param daqmx100 specified an instrument descriptor.
 * @return an error code that is not zero if error ocuured.
 */
DAQMX100_API int APIENTRY updateStatusMX100(DAQMX100 daqmx100);
/**
 * This updates system configure information.
 * @param daqmx100 specified an instrument descriptor.
 * @return an error code that is not zero if error ocuured.
 */
DAQMX100_API int APIENTRY updateSystemMX100(DAQMX100 daqmx100);
/**
 * This updates configure information.
 * @param daqmx100 specified an instrument descriptor.
 * @return an error code that is not zero if error ocuured.
 */
DAQMX100_API int APIENTRY updateConfigMX100(DAQMX100 daqmx100);
/**
 * This updates DO data information.
 * @param daqmx100 specified an instrument descriptor.
 * @return an error code that is not zero if error ocuured.
 */
DAQMX100_API int APIENTRY updateDODataMX100(DAQMX100 daqmx100);
/**
 * This updates AO/PWM data information.
 * @param daqmx100 specified an instrument descriptor.
 * @return an error code that is not zero if error ocuured.
 */
DAQMX100_API int APIENTRY updateAOPWMDataMX100(DAQMX100 daqmx100);
/**
 * This updates channel information.
 * @param daqmx100 specified an instrument descriptor.
 * @param chNo specified a channel number.
 * @return an error code that is not zero if error ocuured.
 */
DAQMX100_API int APIENTRY updateInfoChMX100(DAQMX100 daqmx100,
                                            int      chNo);
/**
 * This updates balance data information.
 * @param daqmx100 specified an instrument descriptor.
 * @return an error code that is not zero if error ocuured.
 */
DAQMX100_API int APIENTRY updateBalanceMX100(DAQMX100 daqmx100);
/**
 * This updates output and transmit data information.
 * @param daqmx100 specified an instrument descriptor.
 * @return an error code that is not zero if error ocuured.
 */
DAQMX100_API int APIENTRY updateOutputMX100(DAQMX100 daqmx100);
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// Data Aquisition
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
/**
 * This gets measurement data from instrument by channel.
 * @param daqmx100 specified an instrument descriptor.
 * @param chNo specified a channel number.
 * @return an error code that is not zero if error ocuured.
 */
DAQMX100_API int APIENTRY measDataChMX100(DAQMX100 daqmx100,
                                          int      chNo);
/**
 * This gets instanteneous data from instrument by channel.
 * @param daqmx100 specified an instrument descriptor.
 * @param chNo specified a channel number.
 * @return an error code that is not zero if error ocuured.
 */
DAQMX100_API int APIENTRY measInstChMX100(DAQMX100 daqmx100,
                                          int      chNo);
/**
 * This gets measurement data from instrument by FIFO.
 * @param daqmx100 specified an instrument descriptor.
 * @param fifoNo specified a FIFO number.
 * @return an error code that is not zero if error ocuured.
 */
DAQMX100_API int APIENTRY measDataFIFOMX100(DAQMX100 daqmx100,
                                            int      fifoNo);
/**
 * This gets instanteneous data from instrument by FIFO.
 * @param daqmx100 specified an instrument descriptor.
 * @param fifoNo specified a FIFO number.
 * @return an error code that is not zero if error ocuured.
 */
DAQMX100_API int APIENTRY measInstFIFOMX100(DAQMX100 daqmx100,
                                            int      fifoNo);
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// Item
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
/**
 * This gets all configuration from the instrument.
 * @param daqmx100 specified an instrument descriptor.
 * @return an error code that is not zero if error ocuured.
 */
DAQMX100_API int APIENTRY getItemAllMX100(DAQMX100 daqmx100);
/**
 * This sets all configuration to the instrument.
 * @param daqmx100 specified an instrument descriptor.
 * @return an error code that is not zero if error ocuured.
 */
DAQMX100_API int APIENTRY setItemAllMX100(DAQMX100 daqmx100);
/**
 * This reads a content of specified item in configuration.
 * @param daqmx100 specified an instrument descriptor.
 * @param itemNo specified an item number.
 * @param strItem stored a buffer for string.
 * @param lenItem specified a length of the buffer (strItem).
 * @param realLen stored a length of the string by bytes without NULL.
 * @return an error code that is not zero if error ocuured.
 */
DAQMX100_API int APIENTRY readItemMX100(DAQMX100 daqmx100,
                                        int      itemNo,
                                        char *   strItem,
                                        int      lenItem,
                                        int *    realLen);
/**
 * This writes a content of specified item in configuration.
 * @param daqmx100 specified an instrument descriptor.
 * @param itemNo specified an item number.
 * @param strItem specified a string of the content.
 * @return an error code that is not zero if error ocuured.
 */
DAQMX100_API int APIENTRY writeItemMX100(DAQMX100     daqmx100,
                                         int          itemNo,
                                         const char * strItem);
/**
 * This initializes all configuration.
 * @param daqmx100 specified an instrument descriptor.
 * @return an error code that is not zero if error ocuured.
 */
DAQMX100_API int APIENTRY initItemMX100(DAQMX100 daqmx100);
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// Get Current Measured Data
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
/**
 * This gets a value of current measurement data by channel.
 * @param daqmx100 specified an instrument descriptor.
 * @param chNo specified a channel number.
 * @return a value of measurement data without decimal point position.
 */
DAQMX100_API int APIENTRY dataValueMX100(DAQMX100 daqmx100,
                                         int      chNo);
/**
 * This gets a status of current measurement data by channel.
 * @param daqmx100 specified an instrument descriptor.
 * @param chNo specified a channel number.
 * @return a status as DAQMX_DATA_xxx.
 */
DAQMX100_API int APIENTRY dataStatusMX100(DAQMX100 daqmx100,
                                          int      chNo);
/**
 * This gets an alarm of current measurement data by channel.
 * @param daqmx100 specified an instrument descriptor.
 * @param chNo specified a channel number.
 * @param levelNo specified a level number.
 * @return a validation as boolean.
 */
DAQMX100_API int APIENTRY dataAlarmMX100(DAQMX100 daqmx100,
                                         int      chNo,
                                         int      levelNo);
/**
 * This gets a value of current measurement data by channel.
 * @param daqmx100 specified an instrument descriptor.
 * @param chNo specified a channel number.
 * @retrun a value as double.
 */
DAQMX100_API double APIENTRY dataDoubleValueMX100(DAQMX100 daqmx100,
                                                  int      chNo);
/**
 * This gets a value of current measurement data by channel.
 * @param daqmx100 specified an instrument descriptor.
 * @param chNo specified a channel number.
 * @param strValue stored a string of the value.
 * @param lenValue specified a length of the buffer (strValue).
 * @retrun a length of the string by bytes without NULL.
 */
DAQMX100_API int APIENTRY dataStringValueMX100(DAQMX100 daqmx100,
                                               int      chNo,
                                               char *   strValue,
                                               int      lenValue);
/**
 * This gets time of current measurement data by channel.
 * @param daqmx100 specified an instrument descriptor.
 * @param chNo specified a channel number.
 * @return time (seconds)
 */
DAQMX100_API time_t APIENTRY dataTimeMX100(DAQMX100 daqmx100,
                                           int      chNo);
/**
 * This gets additional time of current measurement data by channel.
 * @param daqmx100 specified an instrument descriptor.
 * @param chNo specified a channel number.
 * @return millisecond.
 */
DAQMX100_API int APIENTRY dataMilliSecMX100(DAQMX100 daqmx100,
                                            int      chNo);
/**
 * This gets a year of data.
 * @param daqmx100 specified an instrument descriptor.
 * @param chNo specified a channel number.
 * @retrun a value of year.
 */
DAQMX100_API int APIENTRY dataYearMX100(DAQMX100 daqmx100,
                                        int      chNo);
/**
 * This gets a month of data.
 * @param daqmx100 specified an instrument descriptor.
 * @param chNo specified a channel number.
 * @retrun a value of month.
 */
DAQMX100_API int APIENTRY dataMonthMX100(DAQMX100 daqmx100,
                                         int      chNo);
/**
 * This gets a day of data.
 * @param daqmx100 specified an instrument descriptor.
 * @param chNo specified a channel number.
 * @retrun a value of day.
 */
DAQMX100_API int APIENTRY dataDayMX100(DAQMX100 daqmx100,
                                       int      chNo);
/**
 * This gets a hour of data.
 * @param daqmx100 specified an instrument descriptor.
 * @param chNo specified a channel number.
 * @retrun a value of hour.
 */
DAQMX100_API int APIENTRY dataHourMX100(DAQMX100 daqmx100,
                                        int      chNo);
/**
 * This gets a minute of data.
 * @param daqmx100 specified an instrument descriptor.
 * @param chNo specified a channel number.
 * @retrun a value of minute.
 */
DAQMX100_API int APIENTRY dataMinuteMX100(DAQMX100 daqmx100,
                                          int      chNo);
/**
 * This gets a second of data.
 * @param daqmx100 specified an instrument descriptor.
 * @param chNo specified a channel number.
 * @retrun a value of second.
 */
DAQMX100_API int APIENTRY dataSecondMX100(DAQMX100 daqmx100,
                                          int      chNo);
/**
 * This gets validation that current data is exist.
 * @param daqmx100 specified an instrument descriptor.
 * @param chNo specified a channel number.
 * @retrun a validation as boolean.
 */
DAQMX100_API int APIENTRY dataValidMX100(DAQMX100 daqmx100,
                                         int      chNo);
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// Get Channel Information
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
/**
 * This gets a number of FIFO.
 * @param daqmx100 specified an instrument descriptor.
 * @param chNo specified a channel number.
 * @return a number of FIFO.
 */
DAQMX100_API int APIENTRY channelFIFONoMX100(DAQMX100 daqmx100,
                                             int      chNo);
/**
 * This gets an index of channel in FIFO.
 * @param daqmx100 specified an instrument descriptor.
 * @param chNo specified a channel number.
 * @return an index of channel in FIFO.
 */
DAQMX100_API int APIENTRY channelFIFOIndexMX100(DAQMX100 daqmx100,
                                                int      chNo);
/**
 * This gets a minimum value for displaying.
 * @param daqmx100 specified an instrument descriptor.
 * @param chNo specified a channel number.
 * @return a minimum value as double.
 */
DAQMX100_API double APIENTRY channelDisplayMinMX100(DAQMX100 daqmx100,
                                                    int      chNo);
/**
 * This gets a maximum value for displaying.
 * @param daqmx100 specified an instrument descriptor.
 * @param chNo specified a channel number.
 * @return a maximum value as double.
 */
DAQMX100_API double APIENTRY channelDisplayMaxMX100(DAQMX100 daqmx100,
                                                    int      chNo);
/**
 * This gets a minimum value for real span.
 * @param daqmx100 specified an instrument descriptor.
 * @param chNo specified a channel number.
 * @return a minimum value as double.
 */
DAQMX100_API double APIENTRY channelRealMinMX100(DAQMX100 daqmx100,
                                                 int      chNo);
/**
 * This gets a maximum value for real span.
 * @param daqmx100 specified an instrument descriptor.
 * @param chNo specified a channel number.
 * @return a maximum value as double.
 */
DAQMX100_API double APIENTRY channelRealMaxMX100(DAQMX100 daqmx100,
                                                 int      chNo);
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// Get Channel Configure
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
/**
 * This gets a validation of the channel.
 * @param daqmx100 specified an instrument descriptor.
 * @param chNo specified a channel number.
 * @return a validation as boolean.
 */
DAQMX100_API int APIENTRY channelValidMX100(DAQMX100 daqmx100,
                                            int      chNo);
/**
 * This gets a decimal point position of the channel.
 * @param daqmx100 specified an instrument descriptor.
 * @param chNo specified a channel number.
 * @return a value of a decimal point position.
 */
DAQMX100_API int APIENTRY channelPointMX100(DAQMX100 daqmx100,
                                            int      chNo);
/**
 * This gets a kind of the channel.
 * @param daqmx100 specified an instrument descriptor.
 * @param chNo specified a channel number.
 * @return a value of kind as DAQMX_CHKIND_xxx.
 */
DAQMX100_API int APIENTRY channelKindMX100(DAQMX100 daqmx100,
                                           int      chNo);
/**
 * Thsi gets a range type of the channel.
 * @param daqmx100 specified an instrument descriptor.
 * @param chNo specified a channel number.
 * @return a value of range as DAQMX_RANGE_xxx.
 */
DAQMX100_API int APIENTRY channelRangeMX100(DAQMX100 daqmx100,
                                            int      chNo);
/**
 * This gets a scalling type of the channel.
 * @param daqmx100 specified an instrument descriptor.
 * @param chNo specified a channel number.
 * @return a value of scalling as DAQMX_SCALE_xxx.
 */
DAQMX100_API int APIENTRY channelScaleTypeMX100(DAQMX100 daqmx100,
                                                int      chNo);
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
/**
 * This gets an unit of the channel.
 * @param daqmx100 specified an instrument descriptor.
 * @param chNo specified a channel number.
 * @return a string of the unit.
 */
DAQMX100_API const char * APIENTRY getChannelUnitMX100(DAQMX100 daqmx100,
                                                       int      chNo);
/**
 * This gets an unit of the channel.
 * @param daqmx100 specified an instrument descriptor.
 * @param chNo specified a channel number.
 * @param strUnit stored a string of the unit.
 * @param lenUnit specified a length of the buffer (strUnit).
 * @return a length of the string by bytes without NULL.
 */
DAQMX100_API int APIENTRY toChannelUnitMX100(DAQMX100 daqmx100,
                                             int      chNo,
                                             char *   strUnit,
                                             int      lenUnit);
/**
 * This gets a tag of the channel.
 * @param daqmx100 specified an instrument descriptor.
 * @param chNo specified a channel number.
 * @return a string of the tag.
 */
DAQMX100_API const char * APIENTRY getChannelTagMX100(DAQMX100 daqmx100,
                                                      int      chNo);
/**
 * This gets a tag of the channel.
 * @param daqmx100 specified an instrument descriptor.
 * @param chNo specified a channel number.
 * @param strTag stored a string of the tag.
 * @param lenTag specified a length of the buffer (strTag).
 * @return a length of the string by bytes without NULL.
 */
DAQMX100_API int APIENTRY toChannelTagMX100(DAQMX100 daqmx100,
                                            int      chNo,
                                            char *   strTag,
                                            int      lenTag);
/**
 * This gets a comment of the channel.
 * @param daqmx100 specified an instrument descriptor.
 * @param chNo specified a channel number.
 * @return a string of the comment.
 */
DAQMX100_API const char * APIENTRY getChannelCommentMX100(DAQMX100 daqmx100,
                                                          int      chNo);
/**
 * This gets a comment of the channel.
 * @param daqmx100 specified an instrument descriptor.
 * @param chNo specified a channel number.
 * @param strComment stored a string of the comment.
 * @param lenComment specified a length of the buffer (strComment).
 * @return a length of the string by bytes without NULL.
 */
DAQMX100_API int APIENTRY toChannelCommentMX100(DAQMX100 daqmx100,
                                                int      chNo,
                                                char *   strComment,
                                                int      lenComment);
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
/**
 * This gets a minimum span of the channel.
 * @param daqmx100 specified an instrument descriptor.
 * @param chNo specified a channel number.
 * @return a minimum value of the span.
 */
DAQMX100_API int APIENTRY channelSpanMinMX100(DAQMX100 daqmx100,
                                              int      chNo);
/**
 * This gets a minimum span of the channel.
 * @param daqmx100 specified an instrument descriptor.
 * @param chNo specified a channel number.
 * @return a minimum value of the span as double.
 */
DAQMX100_API double APIENTRY channelDoubleSpanMinMX100(DAQMX100 daqmx100,
                                                       int      chNo);
/**
 * This gets a maximum span of the channel.
 * @param daqmx100 specified an instrument descriptor.
 * @param chNo specified a channel number.
 * @return a maximum value of the span.
 */
DAQMX100_API int APIENTRY channelSpanMaxMX100(DAQMX100 daqmx100,
                                              int      chNo);
/**
 * This gets a maximum span of the channel.
 * @param daqmx100 specified an instrument descriptor.
 * @param chNo specified a channel number.
 * @return a maximum value of the span as double.
 */
DAQMX100_API double APIENTRY channelDoubleSpanMaxMX100(DAQMX100 daqmx100,
                                                       int      chNo);
/**
 * This gets a minimum scalling of the channel.
 * @param daqmx100 specified an instrument descriptor.
 * @param chNo specified a channel number.
 * @return a minimum value of the scalling.
 */
DAQMX100_API int APIENTRY channelScaleMinMX100(DAQMX100 daqmx100,
                                               int      chNo);
/**
 * This gets a minimum scalling of the channel.
 * @param daqmx100 specified an instrument descriptor.
 * @param chNo specified a channel number.
 * @return a minimum value of the scalling as double.
 */
DAQMX100_API double APIENTRY channelDoubleScaleMinMX100(DAQMX100 daqmx100,
                                                        int      chNo);
/**
 * This gets a maximum scalling of the channel.
 * @param daqmx100 specified an instrument descriptor.
 * @param chNo specified a channel number.
 * @return a maximum value of the scalling.
 */
DAQMX100_API int APIENTRY channelScaleMaxMX100(DAQMX100 daqmx100,
                                               int      chNo);
/**
 * This gets a maximum scalling of the channel.
 * @param daqmx100 specified an instrument descriptor.
 * @param chNo specified a channel number.
 * @return a maximum value of the scalling as double.
 */
DAQMX100_API double APIENTRY channelDoubleScaleMaxMX100(DAQMX100 daqmx100,
                                                        int      chNo);
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
/**
 * This gets a type of the alarm.
 * @param daqmx100 specified an instrument descriptor.
 * @param chNo specified a channel number.
 * @param levelNo specified a level number.
 * @return a value of type as DAQMX_ALARM_xxx.
 */
DAQMX100_API int APIENTRY alarmTypeMX100(DAQMX100 daqmx100,
                                         int      chNo,
                                         int      levelNo);
/**
 * This gets a value of the alarm ON.
 * @param daqmx100 specified an instrument descriptor.
 * @param chNo specified a channel number.
 * @param levelNo specified a level number.
 * @return a value of the alarm ON.
 */
DAQMX100_API int APIENTRY alarmValueONMX100(DAQMX100 daqmx100,
                                            int      chNo,
                                            int      levelNo);
/**
 * This gets a value of the alarm ON.
 * @param daqmx100 specified an instrument descriptor.
 * @param chNo specified a channel number.
 * @param levelNo specified a level number.
 * @return a value of the alarm ON as double.
 */
DAQMX100_API double APIENTRY alarmDoubleValueONMX100(DAQMX100 daqmx100,
                                                     int      chNo,
                                                     int      levelNo);
/**
 * This gets a value of the alarm OFF.
 * @param daqmx100 specified an instrument descriptor.
 * @param chNo specified a channel number.
 * @param levelNo specified a level number.
 * @return a value of the alarm OFF.
 */
DAQMX100_API int APIENTRY alarmValueOFFMX100(DAQMX100 daqmx100,
                                             int      chNo,
                                             int      levelNo);
/**
 * This gets a value of the alarm OFF.
 * @param daqmx100 specified an instrument descriptor.
 * @param chNo specified a channel number.
 * @param levelNo specified a level number.
 * @return a value of the alarm OFF as double.
 */
DAQMX100_API double APIENTRY alarmDoubleValueOFFMX100(DAQMX100 daqmx100,
                                                      int      chNo,
                                                      int      levelNo);
/**
 * This gets a histerisys of the alarm.
 * @param daqmx100 specified an instrument descriptor.
 * @param chNo specified a channel number.
 * @param levelNo specified a level number.
 * @return a value of the histerisys.
 */
DAQMX100_API int APIENTRY alarmHisterisysMX100(DAQMX100 daqmx100,
                                               int      chNo,
                                               int      levelNo);
/**
 * This gets a histerisys of the alarm.
 * @param daqmx100 specified an instrument descriptor.
 * @param chNo specified a channel number.
 * @param levelNo specified a level number.
 * @return a value of the histerisys as double.
 */
DAQMX100_API double APIENTRY alarmDoubleHisterisysMX100(DAQMX100 daqmx100,
                                                        int      chNo,
                                                        int      levelNo);
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
/**
 * This gets a filter of the channel.
 * @param daqmx100 specified an instrument descriptor.
 * @param chNo specified a channel number.
 * @return a value of the filter as DAQMX_FILTER_xxx.
 */
DAQMX100_API int APIENTRY channelFilterMX100(DAQMX100 daqmx100,
                                             int      chNo);
/**
 * This gets a RJC type of the channel.
 * @param daqmx100 specified an instrument descriptor.
 * @param chNo specified a channel number.
 * @return a value of type as DAQMX_RJC_xxx.
 */
DAQMX100_API int APIENTRY channelRJCTypeMX100(DAQMX100 daqmx100,
                                              int      chNo);
/**
 * This gets a RJC voltage of the channel.
 * @param daqmx100 specified an instrument descriptor.
 * @param chNo specified a channel number.
 * @return a value of the voltage.
 */
DAQMX100_API int APIENTRY channelRJCVoltMX100(DAQMX100 daqmx100,
                                              int      chNo);
/**
 * This gets a burnout of the channel.
 * @param daqmx100 specified an instrument descriptor.
 * @param chNo specified a channel number.
 * @return a value of the burnout as DAQMX_BURNOUT_xxx.
 */
DAQMX100_API int APIENTRY channelBurnoutMX100(DAQMX100 daqmx100,
                                              int      chNo);
/**
 * This gets a deenergize of the channel.
 * @param daqmx100 specified an instrument descriptor.
 * @param doNo specified a DO number as channel number.
 * @return a deenergize as boolean.
 */
DAQMX100_API int APIENTRY channelDeenergizeMX100(DAQMX100 daqmx100,
                                                 int      doNo);
/**
 * This gets a hold of the channel.
 * @param daqmx100 specified an instrument descriptor.
 * @param doNo specified a DO number as channel number.
 * @return a hold as boolean.
 */
DAQMX100_API int APIENTRY channelHoldMX100(DAQMX100 daqmx100,
                                           int      doNo);
/**
 * This gets a reference alarm of the channel.
 * @param daqmx100 specified an instrument descriptor.
 * @param doNo specified a DO number as channel number.
 * @param refChNo specified a reference channel number.
 * @param levelNo specified a level number.
 * @return a validation as boolean.
 */
DAQMX100_API int APIENTRY channelRefAlarmMX100(DAQMX100 daqmx100,
                                               int      doNo,
                                               int      refChNo,
                                               int      levelNo);
/**
 * This gets a reference channel number.
 * @param daqmx100 specified an instrument descriptor.
 * @param chNo specified a channel number.
 * @return a reference channel number.
 */
DAQMX100_API int APIENTRY channelRefChNoMX100(DAQMX100 daqmx100,
                                              int      chNo);
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
/**
 * This gets validation that balance is effective.
 * @param daqmx100 specified an instrument descriptor.
 * @param balanceNo specified a balance (strain) channel number.
 * @return a validation as boolean.
 */
DAQMX100_API int APIENTRY channelBalanceValidMX100(DAQMX100 daqmx100,
                                                   int      balanceNo);
/**
 * This gets a value of the initial balance.
 * @param daqmx100 specified an instrument descriptor.
 * @param balanceNo specified a balance (strain) channel number.
 * @return a value.
 */
DAQMX100_API int APIENTRY channelBalanceValueMX100(DAQMX100 daqmx100,
                                                   int      balanceNo);
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
/**
 * This gets a type of output.
 * @param daqmx100 specified an instrument descriptor.
 * @param outputNo specified an output channel number.
 * @return a type of output as DAQMX_OUTPUT_xxx
 */
DAQMX100_API int APIENTRY channelOutputTypeMX100(DAQMX100 daqmx100,
                                                 int      outputNo);
/**
 * This gets a choice on idle.
 * @param daqmx100 specified an instrument descriptor.
 * @param outputNo specified an output channel number.
 * @return a choice as DAQMX_CHOICE_xxx
 */
DAQMX100_API int APIENTRY channelIdleChoiceMX100(DAQMX100 daqmx100,
                                                 int      outputNo);
/**
 * This gets a choice on error.
 * @param daqmx100 specified an instrument descriptor.
 * @param outputNo specified an output channel number.
 * @return a choice as DAQMX_CHOICE_xxx
 */
DAQMX100_API int APIENTRY channelErrorChoiceMX100(DAQMX100 daqmx100,
                                                  int      outputNo);
/**
 * This gets a value if DAQMX_CHOICE_PRESET.
 * @param daqmx100 specified an instrument descriptor.
 * @param outputNo specified an output channel number.
 * @return a value without decimal point position.
 */
DAQMX100_API int APIENTRY channelPresetValueMX100(DAQMX100 daqmx100,
                                                  int      outputNo);
/**
 * This gets a value if DAQMX_CHOICE_PRESET.
 * @param daqmx100 specified an instrument descriptor.
 * @param outputNo specified an output channel number.
 * @return a value as double.
 */
DAQMX100_API double APIENTRY channelDoublePresetValueMX100(DAQMX100 daqmx100,
                                                           int      outputNo);
/**
 * This gets a pulse time.
 * @param daqmx100 specified an instrument descriptor.
 * @param outputNo specified an output channel number.
 * @return a value.
 */
DAQMX100_API int APIENTRY channelPulseTimeMX100(DAQMX100 daqmx100,
                                                int      outputNo);
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
/**
 * This gets a chatering filter of the channel.
 * @param daqmx100 specified an instrument descriptor.
 * @param chNo specified a channel number.
 * @return a chatering filter as boolean.
 * @since R3.01
 */
DAQMX100_API int APIENTRY channelChatFilterMX100(DAQMX100 daqmx100,
                                                 int      chNo);
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// Get Network Data
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
/**
 * This gets a host name.
 * @param daqmx100 specified an instrument descriptor.
 * @return a string of the host name.
 */
DAQMX100_API const char * APIENTRY getNetHostMX100(DAQMX100 daqmx100);
/**
 * This gets a host name.
 * @param daqmx100 specified an instrument descriptor.
 * @param strHost stored a host name.
 * @param lenHost specified a length of the buffer (strHost).
 * @return a length of the string by bytes without NULL.
 */
DAQMX100_API int APIENTRY toNetHostMX100(DAQMX100 daqmx100,
                                         char *   strHost,
                                         int      lenHost);
/**
 * This gets an IP address.
 * @param daqmx100 specified an instrument descriptor.
 * @return an IP address.
 */
DAQMX100_API unsigned int APIENTRY netAddressMX100(DAQMX100 daqmx100);
/**
 * This gets a port number.
 * @param daqmx100 specified an instrument descriptor.
 * @return a port number.
 */
DAQMX100_API unsigned int APIENTRY netPortMX100(DAQMX100 daqmx100);
/**
 * This gets a subnet mask address.
 * @param daqmx100 specified an instrument descriptor.
 * @return a subnet mask address.
 */
DAQMX100_API unsigned int APIENTRY netSubmaskMX100(DAQMX100 daqmx100);
/**
 * This gets a gateway address.
 * @param daqmx100 specified an instrument descriptor.
 * @return a gateway address.
 */
DAQMX100_API unsigned int APIENTRY netGatewayMX100(DAQMX100 daqmx100);
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// Get System Data
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
/**
 * This gets a type of the module.
 * @param daqmx100 specified an instrument descriptor.
 * @param moduleNo specified a module number.
 * @return a value of type as DAQMX_MODULE_xxx.
 */
DAQMX100_API int APIENTRY moduleTypeMX100(DAQMX100 daqmx100,
                                          int      moduleNo);
/**
 * This gets a number of channels on the module.
 * @param daqmx100 specified an instrument descriptor.
 * @param moduleNo specified a module number.
 * @return a value of channel number as DAQMX_CHNUM_xxx.
 */
DAQMX100_API int APIENTRY moduleChNumMX100(DAQMX100 daqmx100,
                                           int      moduleNo);
/**
 * This gets an interval of the module.
 * @param daqmx100 specified an instrument descriptor.
 * @param moduleNo specified a module number.
 * @return a value of the interval as DAQMX_INTERVAL_xxx.
 */
DAQMX100_API int APIENTRY moduleIntervalMX100(DAQMX100 daqmx100,
                                              int      moduleNo);
/**
 * This gets an integral time of the module.
 * @param daqmx100 specified an instrument descriptor.
 * @param moduleNo specified a module number.
 * @return a value of the integral time as DAQMX_INTEGRAL_xxx.
 */
DAQMX100_API int APIENTRY moduleIntegralMX100(DAQMX100 daqmx100,
                                              int      moduleNo);
/**
 * This gets a validation of the module.
 * @param daqmx100 specified an instrument descriptor.
 * @param moduleNo specified a module number.
 * @return a validation as boolean.
 */
DAQMX100_API int APIENTRY moduleValidMX100(DAQMX100 daqmx100,
                                           int      moduleNo);
/**
 * This gets a type of the module at standby.
 * @param daqmx100 specified an instrument descriptor.
 * @param moduleNo specified a module number.
 * @return a value of type as DAQMX_MODULE_xxx.
 */
DAQMX100_API int APIENTRY moduleStandbyTypeMX100(DAQMX100 daqmx100,
                                                 int      moduleNo);
/**
 * This gets a real type of the module.
 * @param daqmx100 specified an instrument descriptor.
 * @param moduleNo specified a module number.
 * @return a value of type as DAQMX_MODULE_xxx.
 */
DAQMX100_API int APIENTRY moduleRealTypeMX100(DAQMX100 daqmx100,
                                              int      moduleNo);
/**
 * This gets a terminal of the module.
 * @param daqmx100 specified an instrument descriptor.
 * @param moduleNo specified a module number.
 * @return a terminal as DAQMX_TERMINAL_xxx.
 */
DAQMX100_API int APIENTRY moduleTerminalMX100(DAQMX100 daqmx100,
                                              int      moduleNo);
/**
 * This gets a version of the module.
 * @param daqmx100 specified an instrument descriptor.
 * @param moduleNo specified a module number.
 * @return a value of the version.
 */
DAQMX100_API int APIENTRY moduleVersionMX100(DAQMX100 daqmx100,
                                             int      moduleNo);
/**
 * This gets a FIFO number of the module.
 * @param daqmx100 specified an instrument descriptor.
 * @param moduleNo specified a module number.
 * @return a value of the FIFO number.
 */
DAQMX100_API int APIENTRY moduleFIFONoMX100(DAQMX100 daqmx100,
                                            int      moduleNo);
/**
 * This gets a serial number of the module.
 * @param daqmx100 specified an instrument descriptor.
 * @param moduleNo specified a module number.
 * @return a string of the serial number.
 */
DAQMX100_API const char * APIENTRY getModuleSerialMX100(DAQMX100 daqmx100,
                                                        int      moduleNo);
/**
 * This gets a serial number of the module.
 * @param daqmx100 specified an instrument descriptor.
 * @param moduleNo specified a module number.
 * @param strSerial stored a string of the serial number.
 * @param lenSerial specified a length of the buffer (strSerial).
 * @return a length of the string by bytes without NULL.
 */
DAQMX100_API int APIENTRY toModuleSerialMX100(DAQMX100 daqmx100,
                                              int      moduleNo,
                                              char *   strSerial,
                                              int      lenSerial);
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
/**
 * This gets a type of the unit.
 * @param daqmx100 specified an instrument descriptor.
 * @return a value of the type as DAQMX_UNITTYPE_xxx.
 */
DAQMX100_API int APIENTRY unitTypeMX100(DAQMX100 daqmx100);
/**
 * This gets a style of the unit.
 * @param daqmx100 specified an instrument descriptor.
 * @return a value of the style.
 */
DAQMX100_API int APIENTRY unitStyleMX100(DAQMX100 daqmx100);
/**
 * This gets a number of the unit.
 * @param daqmx100 specified an instrument descriptor.
 * @return a number of the unit.
 */
DAQMX100_API int APIENTRY unitNoMX100(DAQMX100 daqmx100);
/**
 * This gets a temperature unit.
 * @param daqmx100 specified an instrument descriptor.
 * @return a value of temperature unit as DAQMX_TEMPUNIT_xxx.
 */
DAQMX100_API int APIENTRY unitTempMX100(DAQMX100 daqmx100);
/**
 * This gets a frecuency of the unit.
 * @param daqmx100 specified an instrument descriptor.
 * @return a value of the frecuency.
 */
DAQMX100_API int APIENTRY unitFrequencyMX100(DAQMX100 daqmx100);
/**
 * This gets a part number of the unit.
 * @param daqmx100 specified an instrument descriptor.
 * @return a string of the part number.
 */
DAQMX100_API const char * APIENTRY getUnitPartNoMX100(DAQMX100 daqmx100);
/**
 * This gets a part number of the unit.
 * @param daqmx100 specified an instrument descriptor.
 * @param strPartNo stored a string of the part number.
 * @param lenPartNo specified a length of the buffer (strPartNo).
 * @return a length of the string by bytes without NULL.
 */
DAQMX100_API int APIENTRY toUnitPartNoMX100(DAQMX100 daqmx100,
                                            char *   strPartNo,
                                            int      lenPartNo);
/**
 * This gets an option of the unit.
 * @param daqmx100 specified an instrument descriptor.
 * @return a value of the option.
 */
DAQMX100_API int APIENTRY unitOptionMX100(DAQMX100 daqmx100);
/**
 * This gets a serial number of the unit.
 * @param daqmx100 specified an instrument descriptor.
 * @return a string of the serial number.
 */
DAQMX100_API const char * APIENTRY getUnitSerialMX100(DAQMX100 daqmx100);
/**
 * This gets a serial number of the unit.
 * @param daqmx100 specified an instrument descriptor.
 * @param strSerial stored a string of the part number.
 * @param lenSerial specified a length of the buffer (strSerial).
 * @return a length of the string by bytes without NULL.
 */
DAQMX100_API int APIENTRY toUnitSerialMX100(DAQMX100 daqmx100,
                                            char *   strSerial,
                                            int      lenSerial);
/**
 * This gets a MAC address of the unit.
 * @param daqmx100 specified an instrument descriptor.
 * @param index specified an index in part of MAC address.
 * @return a value of the address in part of MAC address.
 */
DAQMX100_API int APIENTRY unitMACMX100(DAQMX100 daqmx100,
                                       int      index);
/**
 * This gets a mode of writing CF.
 * @param daqmx100 specified an instrument descriptor.
 * @return a value of the mode as DAQMX_CFWRITEMODE_xxx.
 */
DAQMX100_API int APIENTRY unitCFWriteModeMX100(DAQMX100 daqmx100);
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// Get Status Data
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
/**
 * This gets a status of the unit.
 * @param daqmx100 specified an instrument descriptor.
 * @return a value of the status as DAQMX_UNITSTAT_xxx.
 */
DAQMX100_API int APIENTRY statusUnitMX100(DAQMX100 daqmx100);
/**
 * This gets a number of FIFO.
 * @param daqmx100 specified an instrument descriptor.
 * @return a number of FIFO.
 */
DAQMX100_API int APIENTRY statusFIFONumMX100(DAQMX100 daqmx100);
/**
 * This gets a validation of the backup mode.
 * @param daqmx100 specified an instrument descriptor.
 * @return a validation as boolean.
 */
DAQMX100_API int APIENTRY statusBackupMX100(DAQMX100 daqmx100);
/**
 * This gets a status of the FIFO.
 * @param daqmx100 specified an instrument descriptor.
 * @param fifoNo specified a FIFO number.
 * @return a value of the statis as DAQMX_FIFOSTAT_xxx.
 */
DAQMX100_API int APIENTRY statusFIFOMX100(DAQMX100 daqmx100,
                                          int      fifoNo);
/**
 * This gets an interval of the FIFO.
 * @param daqmx100 specified an instrument descriptor.
 * @param fifoNo specified a FIFO number.
 * @return a value of the interval as DAQMX_INTERVAL_xxx.
 */
DAQMX100_API int APIENTRY statusFIFOIntervalMX100(DAQMX100 daqmx100,
                                                  int      fifoNo);
/**
 * This gets a status of the CF.
 * @param daqmx100 specified an instrument descriptor.
 * @return a value of the status as DAQMX_CFSTATUS_xxx.
 */
DAQMX100_API int APIENTRY statusCFMX100(DAQMX100 daqmx100);
/**
 * This gets a size of the CF.
 * @param daqmx100 specified an instrument descriptor.
 * @return a value of the size by KB.
 */
DAQMX100_API int APIENTRY statusCFSizeMX100(DAQMX100 daqmx100);
/**
 * This gets a remain sie of the CF.
 * @param daqmx100 specified an instrument descriptor.
 * @return a value of the size by KB.
 */
DAQMX100_API int APIENTRY statusCFRemainMX100(DAQMX100 daqmx100);
/**
 * This gets time of status.
 * @param daqmx100 specified an instrument descriptor.
 * @return time (seconds)
 */
DAQMX100_API int APIENTRY statusTimeMX100(DAQMX100 daqmx100);
/**
 * This gets additional time of status.
 * @param daqmx100 specified an instrument descriptor.
 * @return millisecond.
 */
DAQMX100_API int APIENTRY statusMilliSecMX100(DAQMX100 daqmx100);
/**
 * This gets a year of status.
 * @param daqmx100 specified an instrument descriptor.
 * @retrun a value of year.
 */
DAQMX100_API int APIENTRY statusYearMX100(DAQMX100 daqmx100);
/**
 * This gets a month of status.
 * @param daqmx100 specified an instrument descriptor.
 * @retrun a value of month.
 */
DAQMX100_API int APIENTRY statusMonthMX100(DAQMX100 daqmx100);
/**
 * This gets a day of status.
 * @param daqmx100 specified an instrument descriptor.
 * @retrun a value of day.
 */
DAQMX100_API int APIENTRY statusDayMX100(DAQMX100 daqmx100);
/**
 * This gets a hour of status.
 * @param daqmx100 specified an instrument descriptor.
 * @retrun a value of hour.
 */
DAQMX100_API int APIENTRY statusHourMX100(DAQMX100 daqmx100);
/**
 * This gets a minute of status.
 * @param daqmx100 specified an instrument descriptor.
 * @retrun a value of minute.
 */
DAQMX100_API int APIENTRY statusMinuteMX100(DAQMX100 daqmx100);
/**
 * This gets a second of status.
 * @param daqmx100 specified an instrument descriptor.
 * @retrun a value of second.
 */
DAQMX100_API int APIENTRY statusSecondMX100(DAQMX100 daqmx100);
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// Get Current Data
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
/**
 * This gets a validation of current DO data.
 * @param daqmx100 specified an instrument descriptor.
 * @param doNo specified a DO channel number.
 * @return a validation as boolean.
 */
DAQMX100_API int APIENTRY currentDOValidMX100(DAQMX100 daqmx100,
                                              int      doNo);
/**
 * This gets a status (ON/OFF) of current DO data
 * @param daqmx100 specified an instrument descriptor.
 * @param doNo specified a DO channel number.
 * @return a value of ON/OFF as booelan.
 */
DAQMX100_API int APIENTRY currentDOValueMX100(DAQMX100 daqmx100,
                                              int      doNo);
/**
 * This gets a validation of current AO/PWM data.
 * @param daqmx100 specified an instrument descriptor.
 * @param aopwmNo specified a AO/PWM channel number.
 * @return a validation as boolean.
 */
DAQMX100_API int APIENTRY currentAOPWMValidMX100(DAQMX100 daqmx100,
                                                 int      aopwmNo);
/**
 * This gets a value of current AO/PWM data.
 * @param daqmx100 specified an instrument descriptor.
 * @param aopwmNo specified a AO/PWM channel number.
 * @return a value of output without decimal point position.
 */
DAQMX100_API int APIENTRY currentAOPWMValueMX100(DAQMX100 daqmx100,
                                                 int      aopwmNo);
/**
 * This gets a value of current AO/PWM data.
 * @param daqmx100 specified an instrument descriptor.
 * @param aopwmNo specified a AO/PWM channel number.
 * @return a value of output as double.
 */
DAQMX100_API double APIENTRY currentDoubleAOPWMValueMX100(DAQMX100 daqmx100,
                                                          int      aopwmNo);
/**
 * This gets a validation of current balance data.
 * @param daqmx100 specified an instrument descriptor.
 * @param balanceNo specified a balance (strain) channel number.
 * @return a validation as boolean.
 */
DAQMX100_API int APIENTRY currentBalanceValidMX100(DAQMX100 daqmx100,
                                                   int      balanceNo);
/**
 * This gets a value of current balance data.
 * @param daqmx100 specified an instrument descriptor.
 * @param balanceNo specified a balance (strain) channel number.
 * @return a value.
 */
DAQMX100_API int APIENTRY currentBalanceValueMX100(DAQMX100 daqmx100,
                                                   int      balanceNo);
/**
 * This gets a result of current balance data.
 * @param daqmx100 specified an instrument descriptor.
 * @param balanceNo specified a balance (strain) channel number.
 * @return a result as DAQMX_BALANCE_xxx
 */
DAQMX100_API int APIENTRY currentBalanceResultMX100(DAQMX100 daqmx100,
                                                    int      balanceNo);
/**
 * This gets a transmit of current transmit data.
 * @param daqmx100 specified an instrument descriptor.
 * @param aopwmNo specified a trancemit (AO/PWM) channel number.
 * @return a transmit as DAQMX_TRANSMIT_xxx
 */
DAQMX100_API int APIENTRY currentTransmitMX100(DAQMX100 daqmx100,
                                               int      aopwmNo);
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// Get User Data
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
/**
 * This gets a validation of user DO data.
 * @param daqmx100 specified an instrument descriptor.
 * @param idDO specified an identifier of DO data.
 * @param doNo specified a DO channel number.
 * @return a validation as boolean.
 */
DAQMX100_API int APIENTRY userDOValidMX100(DAQMX100 daqmx100,
                                           int      idDO,
                                           int      doNo);
/**
 * This gets a status (ON/OFF) of user DO data
 * @param daqmx100 specified an instrument descriptor.
 * @param idDO specified an identifier of DO data.
 * @param doNo specified a DO channel number.
 * @return a value of ON/OFF as booelan.
 */
DAQMX100_API int APIENTRY userDOValueMX100(DAQMX100 daqmx100,
                                           int      idDO,
                                           int      doNo);
/**
 * This gets a validation of user AO/PWM data.
 * @param daqmx100 specified an instrument descriptor.
 * @param idAOPWM specified an identifier of AO/PWM data.
 * @param aopwmNo specified a AO/PWM channel number.
 * @return a validation as boolean.
 */
DAQMX100_API int APIENTRY userAOPWMValidMX100(DAQMX100 daqmx100,
                                              int      idAOPWM,
                                              int      aopwmNo);
/**
 * This gets a value of user AO/PWM data.
 * @param daqmx100 specified an instrument descriptor.
 * @param idAOPWM specified an identifier of AO/PWM data.
 * @param aopwmNo specified a AO/PWM channel number.
 * @return a value of output without decimal point position.
 */
DAQMX100_API int APIENTRY userAOPWMValueMX100(DAQMX100 daqmx100,
                                              int      idAOPWM,
                                              int      aopwmNo);
/**
 * This gets a value of user AO/PWM data.
 * @param daqmx100 specified an instrument descriptor.
 * @param idAOPWM specified an identifier of AO/PWM data.
 * @param aopwmNo specified a AO/PWM channel number.
 * @return a value of output as double.
 */
DAQMX100_API double APIENTRY userDoubleAOPWMValueMX100(DAQMX100 daqmx100,
                                                       int      idAOPWM,
                                                       int      aopwmNo);
/**
 * This gets a validation of user balance data.
 * @param daqmx100 specified an instrument descriptor.
 * @param idBalance specified an identifier of balance data.
 * @param balanceNo specified a balance (strain) channel number.
 * @return a validation as boolean.
 */
DAQMX100_API int APIENTRY userBalanceValidMX100(DAQMX100 daqmx100,
                                                int      idBalance,
                                                int      balanceNo);
/**
 * This gets a value of user balance data.
 * @param daqmx100 specified an instrument descriptor.
 * @param idBalance specified an identifier of balance data.
 * @param balanceNo specified a balance (strain) channel number.
 * @return a value.
 */
DAQMX100_API int APIENTRY userBalanceValueMX100(DAQMX100 daqmx100,
                                                int      idBalance,
                                                int      balanceNo);
/**
 * This gets a transmit of user transmit data.
 * @param daqmx100 specified an instrument descriptor.
 * @param idTrans specified an identifier of transmit data.
 * @param aopwmNo specified a trancemit (AO/PWM) channel number.
 * @return a transmit as DAQMX_TRANSMIT_xxx
 */
DAQMX100_API int APIENTRY userTransmitMX100(DAQMX100 daqmx100,
                                            int      idTrans,
                                            int      aopwmNo);
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// Utility
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
/**
 * This gets a number of remain measurement data by channel.
 * @param daqmx100 specified an instrument descriptor.
 * @param chNo specified a channel number.
 * @return a value of the number.
 */
DAQMX100_API int APIENTRY dataNumChMX100(DAQMX100 daqmx100,
                                         int      chNo);
/**
 * This gets a number of remain measurement data by FIFO.
 * @param daqmx100 specified an instrument descriptor.
 * @param fifoNo specified a FIFO number.
 * @return a value of the number.
 */
DAQMX100_API int APIENTRY dataNumFIFOMX100(DAQMX100 daqmx100,
                                           int      fifoNo);
/**
 * This gets an error code that received from the instrument at last.
 * @param daqmx100 specified an instrument descriptor.
 * @return a error code of the instrument.
 */
DAQMX100_API int APIENTRY lastErrorMX100(DAQMX100 daqmx100);
/**
 * This changes an error code to a string.
 * @param errCode specified an error code that returned by each function.
 * @param errStr stored a string of the error message.
 * @param errLen specified a length of the buffer (errStr).
 * @return a length of the error message by bytes without NULL.
 */
DAQMX100_API int APIENTRY toErrorMessageMX100(int    errCode,
                                              char * errStr,
                                              int    errLen);
/**
 * This gets a string of the error code.
 * If Visual Basic, Use toErrorMessageMX100.
 * @param errCode specified an error code that returned by each function.
 * @return a string of the error message.
 */
DAQMX100_API const char * APIENTRY getErrorMessageMX100(int errCode);
/**
 * This gets a maximum length of error messages.
 * @return a length by bytes without NULL.
 */
DAQMX100_API int APIENTRY errorMaxLengthMX100(void);
/**
 * This gets an item number if an error occured when it set configure.
 * @param daqmx100 specified an instrument descriptor.
 * @return an item number.
 */
DAQMX100_API int APIENTRY itemErrorMX100(DAQMX100 daqmx100);
/**
 * This gets a channel number by FIFO number and index.
 * @param daqmx100 specified an instrument descriptor.
 * @param fifoNo specified a FIFO number.
 * @param fifoIndex specified an index in the FIFO.
 * @return a channel number.
 */
DAQMX100_API int APIENTRY channelNumberMX100(DAQMX100 daqmx100,
                                             int      fifoNo,
                                             int      fifoIndex);
/**
 * This gets a decimal point position of range type.
 * @param daqmx100 specified an instrument descriptor.
 * @param iRange specified a selection of the range as DAQMX_RANGE_xxx
 * return a decimal point position.
 */
DAQMX100_API int APIENTRY rangePointMX100(DAQMX100 daqmx100,
                                          int      iRange);
/**
 * This changes a measured data and decimal point position to a value as double.
 * @param dataValue specified a measured data as integer (Long).
 * @param point specified a decimal point position.
 * @return a value as double.
 */
DAQMX100_API double APIENTRY toDoubleValueMX100(int dataValue,
                                                int point);
/**
 * This changes a measured data and decimal point position to a value as string.
 * @param dataValue specified a measured data as integer (Long).
 * @param point specified a decimal point position.
 * @param strValue stored a string.
 * @param lenValue specified a length of the buffer (strValue).
 * @return a length of the string by bytes without NULL.
 */
DAQMX100_API int APIENTRY toStringValueMX100(int    dataValue,
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
DAQMX100_API int APIENTRY toAlarmNameMX100(int    iAlarmType,
                                           char * strAlarm,
                                           int    lenAlarm);
/**
 * This gets a string of the alarm type.
 * If Visual Basic, Use toAlarmNameMX100.
 * @param iAlarmType specified a selection of the alarm type as DAQMX_ALARM_xxx.
 * @return a string of the alarm type.
 */
DAQMX100_API const char * APIENTRY getAlarmNameMX100(int iAlarmType);
/**
 * This gets a maximum length of alarm name.
 * @return a length by bytes without NULL.
 */
DAQMX100_API int APIENTRY alarmMaxLengthMX100(void);
/**
 * This gets a version of API.
 * @return a number as integer.
 */
DAQMX100_API const int APIENTRY versionAPIMX100(void);
/**
 * This gets a revision of API.
 * @return a number as integer.
 */
DAQMX100_API const int APIENTRY revisionAPIMX100(void);
/**
 * This gets a part of the address.
 * @param address specified an address.
 * @param index specified an index in the part of the address.
 * @return a value of the part.
 */
DAQMX100_API int APIENTRY addressPartMX100(unsigned int address,
                                           int          index);
/**
 * This changes from an output value to a specified value with range.
 * @param realValue specified an output value.
 * @param iRangeAOPWM specified a range as DAQMX_RANGE_AO_xxx or DAQMX_RANEG_PWM_xxx
 * @return a value specified on setAOPWMData.
 */
DAQMX100_API int APIENTRY toAOPWMValueMX100(double realValue,
                                            int    iRangeAOPWM);
/**
 * This changes from a specified value to an output value with range.
 * @param realValue specified a value specified on setAOPWMData.
 * @param iRangeAOPWM specified a range as DAQMX_RANGE_AO_xxx or DAQMX_RANEG_PWM_xxx
 * @return an output value.
 */
DAQMX100_API double APIENTRY toRealValueMX100(int iAOPWMValue,
                                              int iRangeAOPWM);
/**
 * This changes an item number to a string.
 * @param itemNo specified an item number.
 * @param strItem stored a string of the item.
 * @param lenItem specified a length of the bufer (strItem).
 * @return a length of the string by bytes without NULL.
 */
DAQMX100_API int APIENTRY toItemNameMX100(int    itemNo,
                                          char * strItem,
                                          int    lenItem);
/**
 * This changes a string to an item number.
 * @param strItem specified a string of the item.
 * @return an item number.
 */
DAQMX100_API int APIENTRY toItemNoMX100(const char * strItem);
/**
 * This gets a maximum length of item name.
 * @return a length by bytes without NULL.
 */
DAQMX100_API int APIENTRY itemMaxLengthMX100(void);
/**
 * This converts a style to a version as firmware.
 * @param style specified a style number from system information.
 * @return a version
 */
DAQMX100_API int APIENTRY toStyleVersionMX100(int style);
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
#ifdef __cplusplus
}
#endif
///////////////////////////////////////////////////////////////////////
#if defined(WIN32) || defined(_WIN32_WCE)
/**
 * This defines types as load library callback for MS-Windows.
 */
// Connection
typedef DAQMX100 (CALLBACK* DLLOPENMX100)(const char *, int *);
typedef int      (CALLBACK* DLLCLOSEMX100)(DAQMX100);
// FIFO
typedef int      (CALLBACK* DLLMEASSTARTMX100)(DAQMX100);
typedef int      (CALLBACK* DLLMEASSTOPMX100)(DAQMX100);
// Control
typedef int      (CALLBACK* DLLSETDATETIMENOWMX100)(DAQMX100);
typedef int      (CALLBACK* DLLSWITCHBACKUPMX100)(DAQMX100, int);
typedef int      (CALLBACK* DLLFORMATCFMX100)(DAQMX100); 
typedef int      (CALLBACK* DLLRECONSTRUCTMX100)(DAQMX100);
typedef int      (CALLBACK* DLLINITSETVALUEMX100)(DAQMX100);
typedef int      (CALLBACK* DLLACKALARMMX100)(DAQMX100);
typedef int      (CALLBACK* DLLDISPLAYSEGMENTMX100)(DAQMX100, int, int, int, int);
typedef int      (CALLBACK* DLLINITDATACHMX100)(DAQMX100, int);
typedef int      (CALLBACK* DLLINITDATAFIFOMX100)(DAQMX100, int);
// Setup
typedef int      (CALLBACK* DLLSENDCONFIGMX100)(DAQMX100);
typedef int      (CALLBACK* DLLINITBALANCEMX100)(DAQMX100);
typedef int      (CALLBACK* DLLCLEARBALANCEMX100)(DAQMX100);
// Setting
typedef int      (CALLBACK* DLLSETRANGEMX100)(DAQMX100, int, int);
typedef int      (CALLBACK* DLLSETCHDELTAMX100)(DAQMX100, int, int, int);
typedef int      (CALLBACK* DLLSETCHRRJCMX100)(DAQMX100, int, int);
typedef int      (CALLBACK* DLLSETCHUNITMX100)(DAQMX100, int, const char *);
typedef int      (CALLBACK* DLLSETCHTAGMX100)(DAQMX100, int, const char *);
typedef int      (CALLBACK* DLLSETCHCOMMENTMX100)(DAQMX100, int, const char *);
typedef int      (CALLBACK* DLLSETSPANMX100)(DAQMX100, int, int, int);
typedef int      (CALLBACK* DLLSETDOUBLESPANMX100)(DAQMX100, int, double, double);
typedef int      (CALLBACK* DLLSETSCALEMX100)(DAQMX100, int, int, int, int);
typedef int      (CALLBACK* DLLSETDOUBLESCALEMX100)(DAQMX100, int, double, double, int);
typedef int      (CALLBACK* DLLSETALARMMX100)(DAQMX100, int, int, int, int);
typedef int      (CALLBACK* DLLSETDOUBLEALARMMX100)(DAQMX100, int, int, int, double);
typedef int      (CALLBACK* DLLSETALARMVALUEMX100)(DAQMX100, int, int, int, int, int);
typedef int      (CALLBACK* DLLSETDOUBLEALARMVALUEMX100)(DAQMX100, int, int, int, double, double);
typedef int      (CALLBACK* DLLSETHISTERISYSMX100)(DAQMX100, int, int, int);
typedef int      (CALLBACK* DLLSETDOUBLEHISTERISYSMX100)(DAQMX100, int, int, double);
typedef int      (CALLBACK* DLLSETFILTERMX100)(DAQMX100, int, int);
typedef int      (CALLBACK* DLLSETRJCTYPEMX100)(DAQMX100, int, int, int);
typedef int      (CALLBACK* DLLSETBURNOUTMX100)(DAQMX100, int, int);
typedef int      (CALLBACK* DLLSETDEENERGIZEMX100)(DAQMX100, int, int);
typedef int      (CALLBACK* DLLSETHOLDMX100)(DAQMX100, int, int);
typedef int      (CALLBACK* DLLSETREFALARMMX100)(DAQMX100, int, int, int, int);
typedef int      (CALLBACK* DLLSETCHKINDMX100)(DAQMX100, int, int, int);
typedef int      (CALLBACK* DLLSETINTERVALMX100)(DAQMX100, int, int);
typedef int      (CALLBACK* DLLSETINTEGRALMX100)(DAQMX100, int, int);
typedef int      (CALLBACK* DLLSETUNITNOMX100)(DAQMX100, int);
typedef int      (CALLBACK* DLLSETUNITTEMPMX100)(DAQMX100, int);
typedef int      (CALLBACK* DLLSETCFWRITEMODEMX100)(DAQMX100, int);
typedef int      (CALLBACK* DLLSETOUTPUTTYPEMX100)(DAQMX100, int, int);
typedef int      (CALLBACK* DLLSETCHOICEMX100)(DAQMX100, int, int, int, int);
typedef int      (CALLBACK* DLLSETDOUBLECHOICEMX100)(DAQMX100, int, int, int, double);
typedef int      (CALLBACK* DLLSETPULSETIMEMX100)(DAQMX100, int, int);
// Data Operation
typedef int      (CALLBACK* DLLCREATEDOMX100)(DAQMX100, int *);
typedef int      (CALLBACK* DLLDELETEDOMX100)(DAQMX100, int);
typedef int      (CALLBACK* DLLCHANGEDOMX100)(DAQMX100, int, int, int, int);
typedef int      (CALLBACK* DLLCOPYDOMX100)(DAQMX100, int, int);
typedef int      (CALLBACK* DLLCOMMANDDOMX100)(DAQMX100, int);
typedef int      (CALLBACK* DLLSWITCHDOMX100)(DAQMX100, int, int);
typedef int      (CALLBACK* DLLCREATEAOPWMMX100)(DAQMX100, int *);
typedef int      (CALLBACK* DLLDELETEAOPWMMX100)(DAQMX100, int);
typedef int      (CALLBACK* DLLCHANGEAOPWMMX100)(DAQMX100, int, int, int, int);
typedef int      (CALLBACK* DLLCHANGEAOPWMVALUEMX100)(DAQMX100, int, int, int, double);
typedef int      (CALLBACK* DLLCOPYAOPWMMX100)(DAQMX100, int, int);
typedef int      (CALLBACK* DLLCOMMANDAOPWMMX100)(DAQMX100, int);
typedef int      (CALLBACK* DLLCREATEBALANCEMX100)(DAQMX100, int *);
typedef int      (CALLBACK* DLLDELETEBALANCEMX100)(DAQMX100, int);
typedef int      (CALLBACK* DLLCHANGEBALANCEMX100)(DAQMX100, int, int, int, int);
typedef int      (CALLBACK* DLLCOPYBALANCEMX100)(DAQMX100, int, int);
typedef int      (CALLBACK* DLLCOMMANDBALANCEMX100)(DAQMX100, int);
typedef int      (CALLBACK* DLLCREATETRANSMITMX100)(DAQMX100, int *);
typedef int      (CALLBACK* DLLDELETETRANSMITMX100)(DAQMX100, int);
typedef int      (CALLBACK* DLLCHANGETRANSMITMX100)(DAQMX100, int, int, int);
typedef int      (CALLBACK* DLLCOPYTRANSMITMX100)(DAQMX100, int, int);
typedef int      (CALLBACK* DLLCOMMANDTRANSMITMX100)(DAQMX100, int);
typedef int      (CALLBACK* DLLSWITCHTRANSMITMX100)(DAQMX100, int, int);
// Update
typedef int      (CALLBACK* DLLUPDATESTATUSMX100)(DAQMX100);
typedef int      (CALLBACK* DLLUPDATESYSTEMMX100)(DAQMX100);
typedef int      (CALLBACK* DLLUPDATECONFIGMX100)(DAQMX100);
typedef int      (CALLBACK* DLLUPDATEDODATAMX100)(DAQMX100);
typedef int      (CALLBACK* DLLUPDATEAOPWMDATAMX100)(DAQMX100);
typedef int      (CALLBACK* DLLUPDATEINFOCHMX100)(DAQMX100, int);
typedef int      (CALLBACK* DLLUPDATEBALANCEMX100)(DAQMX100);
typedef int      (CALLBACK* DLLUPDATEOUTPUTMX100)(DAQMX100);
// Data Aquisition
typedef int      (CALLBACK* DLLMEASDATACHMX100)(DAQMX100, int);
typedef int      (CALLBACK* DLLMEASINSTCHMX100)(DAQMX100, int);
typedef int      (CALLBACK* DLLMEASDATAFIFOMX100)(DAQMX100, int);
typedef int      (CALLBACK* DLLMEASINSTFIFOMX100)(DAQMX100, int);
// Item
typedef int      (CALLBACK* DLLGETITEMALLMX100)(DAQMX100);
typedef int      (CALLBACK* DLLSETITEMALLMX100)(DAQMX100);
typedef int      (CALLBACK* DLLREADITEMMX100)(DAQMX100, int, char *, int, int *);
typedef int      (CALLBACK* DLLWRITEITEMMX100)(DAQMX100, int, const char *);
typedef int      (CALLBACK* DLLINITITEMMX100)(DAQMX100);
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// Get Current Measured Data
typedef int      (CALLBACK* DLLDATAVALUEMX100)(DAQMX100, int);
typedef int      (CALLBACK* DLLDATASTATUSMX100)(DAQMX100, int);
typedef int      (CALLBACK* DLLDATAALARMMX100)(DAQMX100, int, int);
typedef double 	 (CALLBACK* DLLDATADOUBLEVALUEMX100)(DAQMX100, int);
typedef int      (CALLBACK* DLLDATASTRINGVALUEMX100)(DAQMX100, int, char *, int);
typedef time_t   (CALLBACK* DLLDATATIMEMX100)(DAQMX100, int);
typedef int      (CALLBACK* DLLDATAMILLISECMX100)(DAQMX100, int);
typedef int      (CALLBACK* DLLDATAYEARMX100)(DAQMX100, int);
typedef int      (CALLBACK* DLLDATAMONTHMX100)(DAQMX100, int);
typedef int      (CALLBACK* DLLDATADAYMX100)(DAQMX100, int);
typedef int      (CALLBACK* DLLDATAHOURMX100)(DAQMX100, int);
typedef int      (CALLBACK* DLLDATAMINUTEMX100)(DAQMX100, int);
typedef int      (CALLBACK* DLLDATASECONDMX100)(DAQMX100, int);
typedef int      (CALLBACK* DLLDATAVALIDMX100)(DAQMX100, int);
// Get Channel Information
typedef int      (CALLBACK* DLLCHANNELFIFONOMX100)(DAQMX100, int);
typedef int      (CALLBACK* DLLCHANNELFIFOINDEXMX100)(DAQMX100, int);
typedef double   (CALLBACK* DLLCHANNELDISPLAYMINMX100)(DAQMX100, int);
typedef double   (CALLBACK* DLLCHANNELDISPLAYMAXMX100)(DAQMX100, int);
typedef double   (CALLBACK* DLLCHANNELREALMINMX100)(DAQMX100, int);
typedef double   (CALLBACK* DLLCHANNELREALMAXMX100)(DAQMX100, int);
// Get Channel Configure
typedef int      (CALLBACK* DLLCHANNELVALIDMX100)(DAQMX100, int);
typedef int      (CALLBACK* DLLCHANNELPOINTMX100)(DAQMX100, int);
typedef int      (CALLBACK* DLLCHANNELKINDMX100)(DAQMX100, int);
typedef int      (CALLBACK* DLLCHANNELRANGEMX100)(DAQMX100, int);
typedef int      (CALLBACK* DLLCHANNELSCALETYPEMX100)(DAQMX100, int);
typedef LPCSTR   (CALLBACK* DLLGETCHANNELUNITMX100)(DAQMX100, int);
typedef int      (CALLBACK* DLLTOCHANNELUNITMX100)(DAQMX100, int, char *, int);
typedef LPCSTR   (CALLBACK* DLLGETCHANNELTAGMX100)(DAQMX100, int);
typedef int      (CALLBACK* DLLTOCHANNELTAGMX100)(DAQMX100, int, char *, int);
typedef LPCSTR   (CALLBACK* DLLGETCHANNELCOMMENTMX100)(DAQMX100, int);
typedef int      (CALLBACK* DLLTOCHANNELCOMMENTMX100)(DAQMX100, int, char *, int);
typedef int      (CALLBACK* DLLCHANNELSPANMINMX100)(DAQMX100, int);
typedef double   (CALLBACK* DLLCHANNELDOUBLESPANMINMX100)(DAQMX100, int);
typedef int      (CALLBACK* DLLCHANNELSPANMAXMX100)(DAQMX100, int);
typedef double   (CALLBACK* DLLCHANNELDOUBLESPANMAXMX100)(DAQMX100, int);
typedef int      (CALLBACK* DLLCHANNELSCALEMINMX100)(DAQMX100, int);
typedef double   (CALLBACK* DLLCHANNELDOUBLESCALEMINMX100)(DAQMX100, int);
typedef int      (CALLBACK* DLLCHANNELSCALEMAXMX100)(DAQMX100, int);
typedef double   (CALLBACK* DLLCHANNELDOUBLESCALEMAXMX100)(DAQMX100, int);
typedef int      (CALLBACK* DLLALARMTYPEMX100)(DAQMX100, int, int);
typedef int      (CALLBACK* DLLALARMVALUEONMX100)(DAQMX100, int, int);
typedef double   (CALLBACK* DLLALARMDOUBLEVALUEONMX100)(DAQMX100, int, int);
typedef int      (CALLBACK* DLLALARMVALUEOFFMX100)(DAQMX100, int, int);
typedef double   (CALLBACK* DLLALARMDOUBLEVALUEOFFMX100)(DAQMX100, int, int);
typedef int      (CALLBACK* DLLALARMHISTERISYSMX100)(DAQMX100, int , int);
typedef double   (CALLBACK* DLLALARMDOUBLEHISTERISYSMX100)(DAQMX100, int , int);
typedef int      (CALLBACK* DLLCHANNELFILTERMX100)(DAQMX100, int);
typedef int      (CALLBACK* DLLCHANNELRJCTYPEMX100)(DAQMX100, int);
typedef int      (CALLBACK* DLLCHANNELRJCVOLTMX100)(DAQMX100, int);
typedef int      (CALLBACK* DLLCHANNELBURNOUTMX100)(DAQMX100, int);
typedef int      (CALLBACK* DLLCHANNELDEENERGIZEMX100)(DAQMX100, int);
typedef int      (CALLBACK* DLLCHANNELHOLDMX100)(DAQMX100, int);
typedef int      (CALLBACK* DLLCHANNELREFALARMMX100)(DAQMX100, int, int, int);
typedef int      (CALLBACK* DLLCHANNELREFCHNOMX100)(DAQMX100, int);
typedef int      (CALLBACK* DLLCHANNELBALANCEVALIDMX100)(DAQMX100, int);
typedef int      (CALLBACK* DLLCHANNELBALANCEVALUEMX100)(DAQMX100, int);
typedef int      (CALLBACK* DLLCHANNELOUTPUTTYPEMX100)(DAQMX100, int);
typedef int      (CALLBACK* DLLCHANNELIDLECHOICEMX100)(DAQMX100, int);
typedef int      (CALLBACK* DLLCHANNELERRORCHOICEMX100)(DAQMX100, int);
typedef int      (CALLBACK* DLLCHANNELPRESETVALUEMX100)(DAQMX100, int);
typedef double   (CALLBACK* DLLCHANNELDOUBLEPRESETVALUEMX100)(DAQMX100, int);
typedef int      (CALLBACK* DLLCHANNELPULSETIMEMX100)(DAQMX100, int);
// Get Network Data
typedef LPCSTR   (CALLBACK* DLLGETNETHOSTMX100)(DAQMX100);
typedef int      (CALLBACK* DLLTONETHOSTMX100)(DAQMX100, char *, int);
typedef unsigned int (CALLBACK* DLLNETADDRESSMX100)(DAQMX100);
typedef int      (CALLBACK* DLLNETPORTMX100)(DAQMX100);
typedef unsigned int (CALLBACK* DLLNETSUBMASKMX100)(DAQMX100);
typedef unsigned int (CALLBACK* DLLNETGATEWAYMX100)(DAQMX100);
// Get System Data
typedef int      (CALLBACK* DLLMODULETYPEMX100)(DAQMX100, int);
typedef int      (CALLBACK* DLLMODULECHNUMMX100)(DAQMX100, int);
typedef int      (CALLBACK* DLLMODULEINTERVALMX100)(DAQMX100, int);
typedef int      (CALLBACK* DLLMODULEINTEGRALMX100)(DAQMX100, int);
typedef int      (CALLBACK* DLLMODULEVALIDMX100)(DAQMX100, int);
typedef int      (CALLBACK* DLLMODULESTANDBYTYPEMX100)(DAQMX100, int);
typedef int      (CALLBACK* DLLMODULEREALTYPEMX100)(DAQMX100, int);
typedef int      (CALLBACK* DLLMODULETERMINALMX100)(DAQMX100, int);
typedef int      (CALLBACK* DLLMODULEVERSIONMX100)(DAQMX100, int);
typedef int      (CALLBACK* DLLMODULEFIFONOMX100)(DAQMX100, int);
typedef LPCSTR   (CALLBACK* DLLGETMODULESERIALMX100)(DAQMX100, int);
typedef int      (CALLBACK* DLLTOMODULESERIALMX100)(DAQMX100, int, char *, int);
typedef int      (CALLBACK* DLLUNITTYPEMX100)(DAQMX100);
typedef int      (CALLBACK* DLLUNITSTYLEMX100)(DAQMX100);
typedef int      (CALLBACK* DLLUNITNOMX100)(DAQMX100);
typedef int      (CALLBACK* DLLUNITTEMPMX100)(DAQMX100);
typedef int      (CALLBACK* DLLUNITFREQUENCYMX100)(DAQMX100);
typedef LPCSTR   (CALLBACK* DLLGETUNITPARTNOMX100)(DAQMX100);
typedef int      (CALLBACK* DLLTOUNITPARTNOMX100)(DAQMX100, char *, int);
typedef int      (CALLBACK* DLLUNITOPTIONMX100)(DAQMX100);
typedef LPCSTR   (CALLBACK* DLLGETUNITSERIALMX100)(DAQMX100);
typedef int      (CALLBACK* DLLTOUNITSERIALMX100)(DAQMX100, char *, int);
typedef int      (CALLBACK* DLLUNITMACMX100)(DAQMX100, int);
typedef int      (CALLBACK* DLLUNITCFWRITEMODEMX100)(DAQMX100);
// Get Status Data
typedef int      (CALLBACK* DLLSTATUSUNITMX100)(DAQMX100);
typedef int      (CALLBACK* DLLSTATUSFIFONUMMX100)(DAQMX100);
typedef int      (CALLBACK* DLLSTATUSBACKUPMX100)(DAQMX100);
typedef int      (CALLBACK* DLLSTATUSFIFOMX100)(DAQMX100, int);
typedef int      (CALLBACK* DLLSTATUSFIFOINTERVALMX100)(DAQMX100, int);
typedef int      (CALLBACK* DLLSTATUSCFMX100)(DAQMX100);
typedef int      (CALLBACK* DLLSTATUSCFSIZEMX100)(DAQMX100);
typedef int      (CALLBACK* DLLSTATUSCFREMAINMX100)(DAQMX100);
typedef int      (CALLBACK* DLLSTATUSTIMEMX100)(DAQMX100);
typedef int      (CALLBACK* DLLSTATUSMILLISECMX100)(DAQMX100);
typedef int      (CALLBACK* DLLSTATUSYEARMX100)(DAQMX100);
typedef int      (CALLBACK* DLLSTATUSMONTHMX100)(DAQMX100);
typedef int      (CALLBACK* DLLSTATUSDAYMX100)(DAQMX100);
typedef int      (CALLBACK* DLLSTATUSHOURMX100)(DAQMX100);
typedef int      (CALLBACK* DLLSTATUSMINUTEMX100)(DAQMX100);
typedef int      (CALLBACK* DLLSTATUSSECONDMX100)(DAQMX100);
// Get Current Data
typedef int      (CALLBACK* DLLCURRENTDOVALIDMX100)(DAQMX100, int);
typedef int      (CALLBACK* DLLCURRENTDOVALUEMX100)(DAQMX100, int);
typedef int      (CALLBACK* DLLCURRENTAOPWMVALIDMX100)(DAQMX100, int);
typedef int      (CALLBACK* DLLCURRENTAOPWMVALUEMX100)(DAQMX100, int);
typedef double   (CALLBACK* DLLCURRENTDOUBLEAOPWMVALUEMX100)(DAQMX100, int);
typedef int      (CALLBACK* DLLCURRENTBALANCEVALIDMX100)(DAQMX100, int);
typedef int      (CALLBACK* DLLCURRENTBALANCEVALUEMX100)(DAQMX100, int);
typedef int      (CALLBACK* DLLCURRENTBALANCERESULTMX100)(DAQMX100, int);
typedef int      (CALLBACK* DLLCURRENTTRANSMITMX100)(DAQMX100, int);
// Get User Data
typedef int      (CALLBACK* DLLUSERDOVALIDMX100)(DAQMX100, int, int);
typedef int      (CALLBACK* DLLUSERDOVALUEMX100)(DAQMX100, int, int);
typedef int      (CALLBACK* DLLUSERAOPWMVALIDMX100)(DAQMX100, int, int);
typedef int      (CALLBACK* DLLUSERAOPWMVALUEMX100)(DAQMX100, int, int);
typedef double   (CALLBACK* DLLUSERDOUBLEAOPWMVALUEMX100)(DAQMX100, int, int);
typedef int      (CALLBACK* DLLUSERBALANCEVALIDMX100)(DAQMX100, int, int);
typedef int      (CALLBACK* DLLUSERBALANCEVALUEMX100)(DAQMX100, int, int);
typedef int      (CALLBACK* DLLUSERTRANSMITMX100)(DAQMX100, int, int);
// Utility
typedef int      (CALLBACK* DLLDATANUMCHMX100)(DAQMX100, int);
typedef int      (CALLBACK* DLLDATANUMFIFOMX100)(DAQMX100, int);
typedef int      (CALLBACK* DLLLASTERRORMX100)(DAQMX100);
typedef int      (CALLBACK* DLLTOERRORMESSAGEMX100)(int, char *, int);
typedef LPCSTR   (CALLBACK* DLLGETERRORMESSAGEMX100)(int);
typedef int      (CALLBACK* DLLERRORMAXLENGTHMX100)(void);
typedef int      (CALLBACK* DLLITEMERRORMX100)(DAQMX100);
typedef int      (CALLBACK* DLLCHANNELNUMBERMX100)(DAQMX100, int, int);
typedef int      (CALLBACK* DLLRANGEPOINTMX100)(DAQMX100, int);
typedef double   (CALLBACK* DLLTODOUBLEVALUEMX100)(int, int);
typedef int      (CALLBACK* DLLTOSTRINGVALUEMX100)(int, int, char *, int);
typedef int      (CALLBACK* DLLTOALARMNAMEMX100)(int, char *, int);
typedef LPCSTR   (CALLBACK* DLLGETALARMNAMEMX100)(int);
typedef int      (CALLBACK* DLLALARMMAXLENGTHMX100)(void);
typedef int      (CALLBACK* DLLVERSIONAPIMX100)(void);
typedef int      (CALLBACK* DLLREVISIONAPIMX100)(void);
typedef int      (CALLBACK* DLLADDRESSPARTMX100)(unsigned int, int);
typedef int      (CALLBACK* DLLTOAOPWMVALUEMX100)(double, int);
typedef double   (CALLBACK* DLLTOREALVALUEMX100)(int, int);
typedef int      (CALLBACK* DLLTOITEMNAMEMX100)(int, char *, int);
typedef int      (CALLBACK* DLLTOITEMNOMX100)(const char *);
typedef int      (CALLBACK* DLLITEMMAXLENGTHMX100)(void);
typedef int      (CALLBACK* DLLTOSTYLEVERSIONMX100)(int);
// since R3.01
typedef int      (CALLBACK* DLLSETCHATFILTERMX100)(DAQMX100, int, int);
typedef int      (CALLBACK* DLLCHANNELCHATFILTERMX100)(DAQMX100, int);
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
#endif //WIN32,WCE
///////////////////////////////////////////////////////////////////////
#endif //_DAQMX100_H_
