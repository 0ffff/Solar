// DAQHandler.h
/* -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- */
/*
 * Copyright (c) 2003-2004 Yokogawa Electric Corporation. All rights reserved.
 *
 * This is defined export DAQHandler.dll.
 */
/* -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- */
// 2004/11/01 Ver.2 Rev.1
// 2003/05/30 Ver.1 Rev.1
///////////////////////////////////////////////////////////////////////
#ifndef _DAQHANDLER_H_
#define _DAQHANDLER_H_
///////////////////////////////////////////////////////////////////////
#if defined(WIN32) || defined(_WIN32_WCE)
// system
#include <windows.h>
// calling
#ifdef DAQHANDLER_EXPORTS
#define DAQHANDLER_API __declspec(dllexport)
#else
#define DAQHANDLER_API __declspec(dllimport)
#endif
#else //WIN32,WCE
// calling
#define DAQHANDLER_API
#endif //WIN32,WCE
///////////////////////////////////////////////////////////////////////
#include <stdio.h>
// time_t in time.h
#ifdef _WIN32_WCE
#ifndef _TIME_T_DEFINED
typedef long time_t;
#define _TIME_T_DEFINED
#endif
#else
#include <time.h>
#endif
///////////////////////////////////////////////////////////////////////
#ifdef __cplusplus
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// Date and Time
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
class DAQHANDLER_API CDAQDateTime
{
    //-- ---- ---- ---- ---- ---- Constructs
public:
    CDAQDateTime(time_t time = 0,
                 int    milliSecond = 0);
    virtual ~CDAQDateTime(void);

    //-- ---- ---- ---- ---- ---- Attributes
protected:
    time_t m_time;        //sec
    int    m_milliSecond; //msec

    //-- ---- ---- ---- ---- ---- Overrides

    //-- ---- ---- ---- ---- ---- Implements
public:
    virtual void initialize(void);
    // get
    virtual time_t getTime(void);
    virtual int getMilliSecond(void);
    // set
    virtual void setTime(time_t time);
    virtual void setMilliSecond(int milliSecond);
    virtual void setNow(void);
    // since R2.01
    virtual int isObject(const char * classname = "CDAQDateTime");
    void toLocalDateTime(int * pYear,
                         int * pMonth,
                         int * pDay,
                         int * pHour,
                         int * pMinute,
                         int * pSecond);
    static void toLocalDateTime(time_t sectime,
                                int *  pYear,
                                int *  pMonth,
                                int *  pDay,
                                int *  pHour,
                                int *  pMinute,
                                int *  pSecond);

    //-- ---- ---- ---- ---- ---- Operations
public:
    CDAQDateTime & operator=(CDAQDateTime & cDateTime);

};
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// Channel Information
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
class DAQHANDLER_API CDAQChInfo
{
    //-- ---- ---- ---- ---- ---- Constructs
public:
    CDAQChInfo(int chType = 0,
               int chNo = 0, //0 is unknown
               int point= 0);
    virtual ~CDAQChInfo(void);

    //-- ---- ---- ---- ---- ---- Attributes
protected:
    int m_chType; //channel type
    int m_chNo;   //channel number
    int m_point;  //deciaml point position

    //-- ---- ---- ---- ---- ---- Overrides

    //-- ---- ---- ---- ---- ---- Implements
    // chType : channel type
    // chNo : channel number is 1 origin
    // point : decimal point position
public:
    virtual void initialize(void);
    // get
    virtual int getChType(void);
    virtual int getChNo(void);
    virtual int getPoint(void);
    // set
    virtual void setChType(int chType);
    virtual void setChNo(int chNo);
    virtual void setPoint(int point);
    // since R2.01
    virtual int isObject(const char * classname = "CDAQChInfo");

    //-- ---- ---- ---- ---- ---- Operations
public:
    CDAQChInfo & operator=(CDAQChInfo & cChInfo);

};
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// Data
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
class DAQHANDLER_API CDAQDataInfo
{
    //-- ---- ---- ---- ---- ---- Constructs
public:
    CDAQDataInfo(int          value = 0,
                 CDAQChInfo * pcChInfo = NULL);
    virtual ~CDAQDataInfo(void);

    //-- ---- ---- ---- ---- ---- Attributes
protected:
    int m_value;            //measured data
    CDAQChInfo * m_pChInfo; //reference for decimal point position

    //-- ---- ---- ---- ---- ---- Overrides

    //-- ---- ---- ---- ---- ---- Implements
    // point : decimal point position
public:
    virtual void initialize(void);
    // get
    virtual int getValue(void);
    // set
    virtual void setValue(int value);
    // class
    CDAQChInfo * getClassChInfo(void);
    void setClassChInfo(CDAQChInfo * pcChInfo);
    // formating data
    double getDoubleValue(void);
    int getStringValue(char * strValue,
                       int    lenValue);
    // utility
    static double toDoubleValue(int value,
                                int point);
    static int toStringValue(int    value,
                             int    point,
                             char * strValue,
                             int    lenValue);
    // since R2.01
    virtual int isObject(const char * classname = "CDAQDataInfo");

    //-- ---- ---- ---- ---- ---- Operations
public:
    CDAQDataInfo & operator=(CDAQDataInfo & cDataInfo);

};
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// Handler
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
class DAQHANDLER_API CDAQHandler
{
    //-- ---- ---- ---- ---- ---- Constructs
public:
    CDAQHandler(void);
    CDAQHandler(const char * strAddress,
                unsigned int uiPort,
                int *        errCode = NULL);
    virtual ~CDAQHandler(void);

    //-- ---- ---- ---- ---- ---- Attributes
protected:
    void * m_comm;     // communication descriptor
    int m_nRemainSize; // remain size of data by binary on talker

    //-- ---- ---- ---- ---- ---- Overrides

    //-- ---- ---- ---- ---- ---- Implements
public:
    // low level communications
    virtual int open(const char * strAddress,
                     unsigned int uiPort);
    virtual int close(void);
    // communication by ASCII
    virtual int sendLine(const char * strLine);
    virtual int receiveLine(char * strLine,
                            int    maxLine,
                            int *  lenLine);
    // deprecated
    virtual int setTimeOut(int seconds);
    // acquisition
    virtual int getData(int            chType,
                        int            chNo,
                        CDAQDateTime & cDateTime,
                        CDAQDataInfo & cDataInfo);
    virtual int getChannel(int          chType,
                           int          chNo,
                           CDAQChInfo & cChInfo);
    // error
    static const char * getErrorMessage(int errCode);
    static const int getMaxLenErrorMessage(void);
    // API version
    static const int getVersionAPI(void);
    static const int getRevisionAPI(void);
    // since R2.01
    virtual int isObject(const char * classname = "CDAQHandler");

protected:
    // directly communications
    virtual int send(const unsigned char * bufData,
                     int                   lenData);
    virtual int receive(unsigned char * bufData,
                        int             maxData,
                        int *           lenData);
    // receive remain data
    int receiveRemain(void);
    // DLL version
    static const int getVersionDLL(void);
    static const int getRevisionDLL(void);

    //-- ---- ---- ---- ---- ---- Operations

};
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
#endif //__cplusplus
///////////////////////////////////////////////////////////////////////
#endif //_DAQHANDLER_H_
