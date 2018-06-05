// DAQDA100Reader.h
/* -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- */
/*
 * Copyright (c) 2004 Yokogawa Electric Corporation. All rights reserved.
 *
 * This is defined export DAQDA100.dll.
 */
/* -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- */
// 2004/11/01 Ver.2 Rev.1
///////////////////////////////////////////////////////////////////////
#ifndef _DAQDA100READER_H_
#define _DAQDA100READER_H_
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
#include "DAQDA100.h"
///////////////////////////////////////////////////////////////////////
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// value
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// communication
#define DAQDA100READER_DATAPORT (34151)
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// type
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// DAQ descriptor
// If Visual Basic, type as Long.
typedef int DAQDA100READER;
///////////////////////////////////////////////////////////////////////
#ifdef __cplusplus
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// Handler
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
class DAQDA100_API CDAQDA100Reader : public CDAQDA100
{
    //-- ---- ---- ---- ---- ---- Constructs
public:
    CDAQDA100Reader(void);
    CDAQDA100Reader(const char * strAddress,
                    unsigned int uiPort = DAQDA100READER_DATAPORT,
                    int *        errCode = NULL);
    virtual ~CDAQDA100Reader(void);

    //-- ---- ---- ---- ---- ---- Attributes

    //-- ---- ---- ---- ---- ---- Overrides
    // chType : channel type as DAQDARWIN_CHTYPE_xxx
    // chNo : channel number is 1 origin
public:
    virtual int open(const char * strAddress,
                     unsigned int uiPort = DAQDA100READER_DATAPORT);
    // measurement
    virtual int measInstCh(int chType = DAQDA100_CHTYPE_MEASALL,
                           int chNo = DAQDA100_CHNO_ALL);
  
    // channel info
    virtual int measInfoCh(int chType = DAQDA100_CHTYPE_MEASALL,
                           int chNo = DAQDA100_CHNO_ALL);
    // utility
    virtual int isObject(const char * classname = "CDAQDA100Reader");

protected:
    virtual int getInfoCh(int sChType,
                          int sChNo,
                          int eChType,
                          int eChNo);

    //-- ---- ---- ---- ---- ---- Implements
protected:
    // measurement
    int getInstCh(int sChType,
                  int sChNo,
                  int eChType,
                  int eChNo);

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
DAQDA100_API DAQDA100READER APIENTRY openDA100Reader(const char * strAddress,
                                                     int *        errorCode);
#define OPEN_DA100READER(address) openDA100Reader(address, NULL);
/**
 * This closes the instrument.
 * @param daqda100reader specified an instrument descriptor.
 * @return an error code that is not zero if error ocuured.
 */
DAQDA100_API int APIENTRY closeDA100Reader(DAQDA100READER daqda100reader);
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// Measurement
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
/**
 * This measures instantenous values as measured channel.
 * @param daqda100reader specified an instrument descriptor.
 * @param chType specified a channel type as unit number.
 * @param chNo specified a channel number.
 * @return an error code that is not zero if error ocuured.
 */
DAQDA100_API int APIENTRY measInstChDA100Reader(DAQDA100READER daqda100reader,
                                                int            chType,
                                                int            chNo);
/**
 * This measures instantenous values as mathmatical channel.
 * @param daqda100reader specified an instrument descriptor.
 * @param chNo specified a channel number.
 * @return an error code that is not zero if error ocuured.
 */
DAQDA100_API int APIENTRY mathInstChDA100Reader(DAQDA100READER daqda100reader,
                                                int            chNo);
/**
 * This gets informations of measured channel.
 * @param daqda100reader specified an instrument descriptor.
 * @param chType specified a channel type as unit number.
 * @param chNo specified a channel number.
 * @return an error code that is not zero if error ocuured.
 */
DAQDA100_API int APIENTRY measInfoChDA100Reader(DAQDA100READER daqda100reader,
                                                int            chType,
                                                int            chNo);
/**
 * This gets informations of mathmatical channel.
 * @param daqda100reader specified an instrument descriptor.
 * @param chNo specified a channel number.
 * @return an error code that is not zero if error ocuured.
 */
DAQDA100_API int APIENTRY mathInfoChDA100Reader(DAQDA100READER daqda100reader,
                                                int            chNo);
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// Get Data
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
/**
 * This gets a value of measurement data by channel.
 * @param daqda100reader specified an instrument descriptor.
 * @param chType specified a channel type.
 * @param chNo specified a channel number.
 * @return a value of measurement data without decimal point position.
 */
DAQDA100_API int APIENTRY dataValueDA100Reader(DAQDA100READER daqda100reader,
                                               int            chType,
                                               int            chNo);
/**
 * This gets a status of measurement data by channel.
 * @param daqda100reader specified an instrument descriptor.
 * @param chType specified a channel type.
 * @param chNo specified a channel number.
 * @return a status as DAQDARWIN_DATA_xxx.
 */
DAQDA100_API int APIENTRY dataStatusDA100Reader(DAQDA100READER daqda100reader,
                                                int            chType,
                                                int            chNo);
/**
 * This gets an alarm type of measurement data by channel.
 * @param daqda100reader specified an instrument descriptor.
 * @param chType specified a channel type.
 * @param chNo specified a channel number.
 * @param levelNo specified a level number.
 * @return an alarm type as DAQDARWIN_ALARM_xxx
 */
DAQDA100_API int APIENTRY alarmTypeDA100Reader(DAQDA100READER daqda100reader,
                                               int            chType,
                                               int            chNo,
                                               int            levelNo);
/**
 * This gets a alarm of measurement data by channel.
 * @param daqda100reader specified an instrument descriptor.
 * @param chType specified a channel type.
 * @param chNo specified a channel number.
 * @param levelNo specified a level number.
 * @return a validation as boolean.
 */
DAQDA100_API int APIENTRY dataAlarmDA100Reader(DAQDA100READER daqda100reader,
                                               int            chType,
                                               int            chNo,
                                               int            levelNo);
/**
 * This gets a value of current measurement data by channel.
 * @param daqda100reader specified an instrument descriptor.
 * @param chType specified a channel type.
 * @param chNo specified a channel number.
 * @retrun a value as double.
 */
DAQDA100_API double APIENTRY dataDoubleValueDA100Reader(DAQDA100READER daqda100reader,
                                                        int            chType,
                                                        int            chNo);
/**
 * This gets a value of current measurement data by channel.
 * @param daqda100reader specified an instrument descriptor.
 * @param chType specified a channel type.
 * @param chNo specified a channel number.
 * @param strValue stored a string of the value.
 * @param lenValue specified a length of the buffer (strValue).
 * @retrun a length of the string by bytes without NULL.
 */
DAQDA100_API int APIENTRY dataStringValueDA100Reader(DAQDA100READER daqda100reader,
                                                     int            chType,
                                                     int            chNo,
                                                     char *         strValue,
                                                     int            lenValue);
/**
 * This gets a year of data.
 * @param daqda100reader specified an instrument descriptor.
 * @param chType specified a channel type.
 * @param chNo specified a channel number.
 * @retrun a value of year.
 */
DAQDA100_API int APIENTRY dataYearDA100Reader(DAQDA100READER daqda100reader,
                                              int            chType,
                                              int            chNo);
/**
 * This gets a month of data.
 * @param daqda100reader specified an instrument descriptor.
 * @param chType specified a channel type.
 * @param chNo specified a channel number.
 * @retrun a value of month.
 */
DAQDA100_API int APIENTRY dataMonthDA100Reader(DAQDA100READER daqda100reader,
                                               int            chType,
                                               int            chNo);
/**
 * This gets a hour of data.
 * @param daqda100reader specified an instrument descriptor.
 * @param chType specified a channel type.
 * @param chNo specified a channel number.
 * @retrun a value of hour.
 */
DAQDA100_API int APIENTRY dataDayDA100Reader(DAQDA100READER daqda100reader,
                                             int            chType,
                                             int            chNo);
/**
 * This gets a minute of data.
 * @param daqda100reader specified an instrument descriptor.
 * @param chType specified a channel type.
 * @param chNo specified a channel number.
 * @retrun a value of minute.
 */
DAQDA100_API int APIENTRY dataHourDA100Reader(DAQDA100READER daqda100reader,
                                              int            chType,
                                              int            chNo);
/**
 * This gets a day of data.
 * @param daqda100reader specified an instrument descriptor.
 * @param chType specified a channel type.
 * @param chNo specified a channel number.
 * @retrun a value of day.
 */
DAQDA100_API int APIENTRY dataMinuteDA100Reader(DAQDA100READER daqda100reader,
                                                int            chType,
                                                int            chNo);
/**
 * This gets a second of data.
 * @param daqda100redaer specified an instrument descriptor.
 * @param chType specified a channel type.
 * @param chNo specified a channel number.
 * @retrun a value of second.
 */
DAQDA100_API int APIENTRY dataSecondDA100Reader(DAQDA100READER daqda100redaer,
                                                int            chType,
                                                int            chNo);
/**
 * This gets a millisecond of data.
 * @param daqda100redaer specified an instrument descriptor.
 * @param chType specified a channel type.
 * @param chNo specified a channel number.
 * @retrun a value of millisecond.
 */
DAQDA100_API int APIENTRY dataMilliSecDA100Reader(DAQDA100READER daqda100redaer,
                                                  int            chType,
                                                  int            chNo);
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// Get Channel Information
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
/**
 * This gets a decimal point position of the channel.
 * @param daqda100reader specified an instrument descriptor.
 * @param chType specified a channel type.
 * @param chNo specified a channel number.
 * @return a value of a decimal point position.
 */
DAQDA100_API int APIENTRY channelPointDA100Reader(DAQDA100READER daqda100reader,
                                                  int            chType,
                                                  int            chNo);
/**
 * This gets a channel status.
 * @param daqda100reader specified an instrument descriptor.
 * @param chType specified a channel type.
 * @param chNo specified a channel number.
 * @return a channel status as DAQDARWIN_DATA_xxx
 */
DAQDA100_API int APIENTRY channelStatusDA100Reader(DAQDA100READER daqda100reader,
                                                   int            chType,
                                                   int            chNo);
/**
 * This gets an unit of the channel.
 * @param daqda100reader specified an instrument descriptor.
 * @param chType specified a channel type.
 * @param chNo specified a channel number.
 * @return a string of the unit.
 */
DAQDA100_API const char * APIENTRY getChannelUnitDA100Reader(DAQDA100READER daqda100reader,
                                                             int            chType,
                                                             int            chNo);
/**
 * This gets an unit of the channel.
 * @param daqda100reader specified an instrument descriptor.
 * @param chType specified a channel type.
 * @param chNo specified a channel number.
 * @param strUnit stored a string of the unit.
 * @param lenUnit specified a length of the buffer (strUnit).
 * @return a length of the string by bytes without NULL.
 */
DAQDA100_API int APIENTRY toChannelUnitDA100Reader(DAQDA100READER daqda100reader,
                                                   int            chType,
                                                   int            chNo,
                                                   char *         strUnit,
                                                   int            lenUnit);
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
// Utility
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
/**
 * This changes a measured data and decimal point position to a value as double.
 * @param dataValue specified a measured data as integer (Long).
 * @param point specified a decimal point position.
 * @return a value as double.
 */
DAQDA100_API double APIENTRY toDoubleValueDA100Reader(int dataValue,
                                                      int point);
/**
 * This changes a measured data and decimal point position to a value as string.
 * @param dataValue specified a measured data as integer (Long).
 * @param point specified a decimal point position.
 * @param strValue stored a string.
 * @param lenValue specified a length of the buffer (strValue).
 * @return a length of the string by bytes without NULL.
 */
DAQDA100_API int APIENTRY toStringValueDA100Reader(int    dataValue,
                                                   int    point,
                                                   char * strValue,
                                                   int    lenValue);
/**
 * This gets a string of the alarm type.
 * @param iAlarmType specified a selection of the alarm type as DAQDARWIN_ALARM_xxx.
 * @return a string of the alarm type.
 */
DAQDA100_API const char * APIENTRY getAlarmNameDA100Reader(int iAlarmType);
/**
 * This changes an alarm type to a mnemonic as string.
 * @param iAlarmType specified a selection of the alarm type as DAQDARWIN_ALARM_xxx.
 * @param strAlarm stored a string.
 * @param lenAlarm specified a length of the buffer (strAlarm).
 * @return a length of the string by bytes without NULL.
 */
DAQDA100_API int APIENTRY toAlarmNameDA100Reader(int    iAlarmType,
                                                 char * strAlarm,
                                                 int    lenAlarm);
/**
 * This gets a maximum length of alarm names.
 * @return a length by bytes without NULL.
 */
DAQDA100_API int APIENTRY alarmMaxLengthDA100Reader(void);
/**
 * This gets a version of API.
 * @return a number as integer.
 */
DAQDA100_API const int APIENTRY versionAPIDA100Reader(void);
/**
 * This gets a revision of API.
 * @return a number as integer.
 */
DAQDA100_API const int APIENTRY revisionAPIDA100Reader(void);
/**
 * This gets a string of the error code.
 * @param errCode specified an error code that returned by each function.
 * @return a string of the error message.
 */
DAQDA100_API const char * APIENTRY getErrorMessageDA100Reader(int errCode);
/**
 * This changes an error code to a string.
 * @param errCode specified an error code that returned by each function.
 * @param errStr stored a string of the error message.
 * @param errLen specified a length of the buffer (errStr).
 * @return a length of the error message by bytes without NULL.
 */
DAQDA100_API int APIENTRY toErrorMessageDA100Reader(int    errCode,
                                                    char * errStr,
                                                    int    errLen);
/**
 * This gets a maximum length of error messages.
 * @return a length by bytes without NULL.
 */
DAQDA100_API int APIENTRY errorMaxLengthDA100Reader(void);
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
 * HMODULE pDll = LoadLibrary("DAQDA100");
 * DLLOPENDA100READER openDA100Reader = (DLLOPENDA100READER)GetProcAddress(pDll, "openDA100Reader");
 * </CODE>
 * </PRE>
 */
typedef DAQDA100READER (CALLBACK* DLLOPENDA100READER)(const char *, int *);
typedef int            (CALLBACK* DLLCLOSEDA100READER)(DAQDA100READER);
// Measurement
typedef int            (CALLBACK* DLLMEASINSTCHDA100READER)(DAQDA100READER, int, int);
typedef int            (CALLBACK* DLLMATHINSTCHDA100READER)(DAQDA100READER, int);
typedef int            (CALLBACK* DLLMEASINFOCHDA100READER)(DAQDA100READER, int, int);
typedef int            (CALLBACK* DLLMATHINFOCHDA100READER)(DAQDA100READER, int);
// Get Data
typedef int            (CALLBACK* DLLDATAVALUEDA100READER)(DAQDA100READER, int, int);
typedef int            (CALLBACK* DLLDATASTATUSDA100READER)(DAQDA100READER, int, int);
typedef int            (CALLBACK* DLLALARMTYPEDA100READER)(DAQDA100READER, int, int, int);
typedef int            (CALLBACK* DLLDATAALARMDA100READER)(DAQDA100READER, int, int, int);
typedef double         (CALLBACK* DLLDATADOUBLEVALUEDA100READER)(DAQDA100READER, int, int);
typedef int            (CALLBACK* DLLDATASTRINGVALUEDA100READER)(DAQDA100READER, int, int, char *, int);
typedef int            (CALLBACK* DLLDATAYEARDA100READER)(DAQDA100READER, int, int);
typedef int            (CALLBACK* DLLDATAMONTHDA100READER)(DAQDA100READER, int, int);
typedef int            (CALLBACK* DLLDATADAYDA100READER)(DAQDA100READER, int, int);
typedef int            (CALLBACK* DLLDATAHOURDA100READER)(DAQDA100READER, int, int);
typedef int            (CALLBACK* DLLDATAMINUTEDA100READER)(DAQDA100READER, int, int);
typedef int            (CALLBACK* DLLDATASECONDDA100READER)(DAQDA100READER, int, int);
typedef int            (CALLBACK* DLLDATAMILLISECDA100READER)(DAQDA100READER, int, int);
// Get Channel Information
typedef int            (CALLBACK* DLLCHANNELPOINTDA100READER)(DAQDA100READER, int, int);
typedef int            (CALLBACK* DLLCHANNELSTATUSDA100READER)(DAQDA100READER, int, int);
typedef LPCSTR         (CALLBACK* DLLGETCHANNELUNITDA100READER)(DAQDA100READER, int, int);
typedef int            (CALLBACK* DLLTOCHANNELUNITDA100READER)(DAQDA100READER, int, int, char *, int);
// Utility
typedef double         (CALLBACK* DLLTODOUBLEVALUEDA100READER)(int, int);
typedef int            (CALLBACK* DLLTOSTRINGVALUEDA100READER)(int, int, char *, int);
typedef LPCSTR         (CALLBACK* DLLGETALARMNAMEDA100READER)(int);
typedef int            (CALLBACK* DLLTOALARMNAMEDA100READER)(int, char *, int);
typedef int            (CALLBACK* DLLALARMMAXLENGTHDA100READER)(void);
typedef int            (CALLBACK* DLLVERSIONAPIDA100READER)(void);
typedef int            (CALLBACK* DLLREVISIONAPIDA100READER)(void);
typedef LPCSTR         (CALLBACK* DLLGETERRORMESSAGEDA100READER)(int);
typedef int            (CALLBACK* DLLTOERRORMESSAGEDA100READER)(int, char *, int);
typedef int            (CALLBACK* DLLERRORMAXLENGTHDA100READER)(void);
// -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- -- //
#endif //WIN32,WCE
///////////////////////////////////////////////////////////////////////
#endif //_DAQDA100READER_H_
