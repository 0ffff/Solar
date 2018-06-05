Imports System.Runtime.InteropServices

' DAQMX100.vb
' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
'
' Copyright (c) 2004-2007 Yokogawa Electric Corporation. All rights reserved.
'
' This is defined export DAQMX100.dll.
' Declare Functions for Visual Basic .NET
'
' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
' 2007/09/30 Ver.3 Rev.1
' 2004/11/01 Ver.2 Rev.1
' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --

Module DAQMX100
    
    ' specified
    Public Const DAQMX100_LIST_ALL As Integer = -1
    Public Const DAQMX100_LIST_CURRENT As Integer = -1
    
    ' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
    ' @see DAQMX
    ' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
    
    ' total number
    Public Const DAQMX100_NUMMODULE As Integer = 6
    Public Const DAQMX100_NUMCHANNEL As Integer = 60
    Public Const DAQMX100_NUMDO As Integer = DAQMX100_NUMCHANNEL
    Public Const DAQMX100_NUMFIFO As Integer = 3
    Public Const DAQMX100_NUMALARM As Integer = 4
    Public Const DAQMX100_NUMSEGMENT As Integer = 2
    Public Const DAQMX100_NUMMACADDR As Integer = 6
    Public Const DAQMX100_NUMAOPWM As Integer = DAQMX100_NUMCHANNEL
    Public Const DAQMX100_NUMBALANCE As Integer = DAQMX100_NUMCHANNEL
    Public Const DAQMX100_NUMOUTPUT As Integer = DAQMX100_NUMCHANNEL
    
    ' string length without NULL
    Public Const DAQMX100_MAXHOSTNAMELEN As Integer = 64
    Public Const DAQMX100_MAXUNITLEN As Integer = 6
    Public Const DAQMX100_MAXTAGLEN As Integer = 15
    Public Const DAQMX100_MAXCOMMENTLEN As Integer = 30
    Public Const DAQMX100_MAXSERIALLEN As Integer = 9
    Public Const DAQMX100_MAXPARTNOLEN As Integer = 7
    
    ' maximum number
    Public Const DAQMX100_MAXDECIMALPOINT As Integer = 4
    Public Const DAQMX100_MAXDISPTIME As Integer = 120000
    Public Const DAQMX100_MAXPULSETIME As Integer = 30000
    
    ' constant value
    Public Const DAQMX100_INSTANTANEOUS As Integer = -1
    Public Const DAQMX100_REFCHNO_NONE As Integer = 0
    Public Const DAQMX100_REFCHNO_ALL As Integer = -1
    Public Const DAQMX100_LEVELNO_ALL As Integer = -1
    Public Const DAQMX100_DONO_ALL As Integer = -1
    Public Const DAQMX100_SEGMENTNO_ALL As Integer = -1
    Public Const DAQMX100_CHNO_ALL As Integer = -1
    Public Const DAQMX100_MODULENO_ALL As Integer = -1
    Public Const DAQMX100_FIFONO_ALL As Integer = -1
    Public Const DAQMX100_AOPWMNO_ALL As Integer = -1
    Public Const DAQMX100_BALANCENO_ALL As Integer = -1
    Public Const DAQMX100_OUTPUTNO_ALL As Integer = -1
    
    ' valid
    Public Const DAQMX100_VALID_OFF As Integer = 0
    Public Const DAQMX100_VALID_ON As Integer = 1
    
    ' flag : not use
    
    ' data status
    Public Const DAQMX100_DATA_UNKNWON As Integer = &H00000000
    Public Const DAQMX100_DATA_NORMAL As Integer = &H00000001
    Public Const DAQMX100_DATA_PLUSOVER As Integer = &H00007FFF
    Public Const DAQMX100_DATA_MINUSOVER As Integer = &H00008001
    Public Const DAQMX100_DATA_SKIP As Integer = &H00008002
    Public Const DAQMX100_DATA_ILLEGAL As Integer = &H00008003
    Public Const DAQMX100_DATA_NODATA As Integer = &H00008005
    Public Const DAQMX100_DATA_LACK As Integer = &H00008400
    Public Const DAQMX100_DATA_INVALID As Integer = &H00008700
    
    ' alarm type
    Public Const DAQMX100_ALARM_NONE As Integer = 0
    Public Const DAQMX100_ALARM_UPPER As Integer = 1
    Public Const DAQMX100_ALARM_LOWER As Integer = 2
    Public Const DAQMX100_ALARM_UPDIFF As Integer = 3
    Public Const DAQMX100_ALARM_LOWDIFF As Integer = 4
    
    ' system control : @see each command function
    
    ' channel kind
    Public Const DAQMX100_CHKIND_NONE As Integer = &H0000
    Public Const DAQMX100_CHKIND_AI As Integer = &H0010
    Public Const DAQMX100_CHKIND_AIDIFF As Integer = &H0011
    Public Const DAQMX100_CHKIND_AIRJC As Integer = &H0012
    Public Const DAQMX100_CHKIND_DI As Integer = &H0030
    Public Const DAQMX100_CHKIND_DIDIFF As Integer = &H0031
    Public Const DAQMX100_CHKIND_DO As Integer = &H0040
    Public Const DAQMX100_CHKIND_DOCOM As Integer = &H0041
    Public Const DAQMX100_CHKIND_DOFAIL As Integer = &H0042
    Public Const DAQMX100_CHKIND_DOERR As Integer = &H0043
    Public Const DAQMX100_CHKIND_AO As Integer = &H0020
    Public Const DAQMX100_CHKIND_AOCOM As Integer = &H0021
    Public Const DAQMX100_CHKIND_PWM As Integer = &H0060
    Public Const DAQMX100_CHKIND_PWMCOM As Integer = &H0061
    Public Const DAQMX100_CHKIND_PI As Integer = &H0050
    Public Const DAQMX100_CHKIND_PIDIFF As Integer = &H0051
    Public Const DAQMX100_CHKIND_CI As Integer = &H0070
    Public Const DAQMX100_CHKIND_CIDIFF As Integer = &H0071
    
    ' scale type
    Public Const DAQMX100_SCALE_NONE As Integer = 0
    Public Const DAQMX100_SCALE_LINER As Integer = 1
    
    ' module type
    ' 0xF0000010 -> -268435440
    ' 0xF0001C10 -> -268428272
    ' 0xB0101F10 -> -1341120752
    ' 0xD0001130 -> -805301968
    ' 0x0000FF00 -> 65280
    Public Const DAQMX100_MODULE_NONE As Integer = &H0
    Public Const DAQMX100_MODULE_MX110UNVH04 As Integer = -268435440
    Public Const DAQMX100_MODULE_MX110UNVM10 As Integer = -268428272
    Public Const DAQMX100_MODULE_MX115D05H10 As Integer = &H10003010
    Public Const DAQMX100_MODULE_MX125MKCM10 As Integer = &H00402010
    Public Const DAQMX100_MODULE_MX110V4RM06 As Integer = -1341120752
    Public Const DAQMX100_MODULE_MX112NDIM04 As Integer = &H01004010
    Public Const DAQMX100_MODULE_MX112B35M04 As Integer = &H01004110
    Public Const DAQMX100_MODULE_MX112B12M04 As Integer = &H01004210
    Public Const DAQMX100_MODULE_MX115D24H10 As Integer = &H10003210
    Public Const DAQMX100_MODULE_MX120VAOM08 As Integer = &H0080C010
    Public Const DAQMX100_MODULE_MX120PWMM08 As Integer = &H0020C810
    Public Const DAQMX100_MODULE_HIDDEN As Integer = 65280
    Public Const DAQMX100_MODULE_MX114PLSM10 As Integer = &H0400B010
    Public Const DAQMX100_MODULE_MX110VTDL30 As Integer = -805301968
    Public Const DAQMX100_MODULE_MX118CANM10 As Integer = &H00085110
    Public Const DAQMX100_MODULE_MX118CANM20 As Integer = &H00085220
    Public Const DAQMX100_MODULE_MX118CANM30 As Integer = &H00085330
    Public Const DAQMX100_MODULE_MX118CANSUB As Integer = &H00085000
    Public Const DAQMX100_MODULE_MX118CANMERR As Integer = &H00005A10
    Public Const DAQMX100_MODULE_MX118CANSERR As Integer = &H00005B10
    
    ' how many channels by each module
    Public Const DAQMX100_CHNUM_0 As Integer = 0
    Public Const DAQMX100_CHNUM_4 As Integer = 4
    Public Const DAQMX100_CHNUM_6 As Integer = 6
    Public Const DAQMX100_CHNUM_8 As Integer = 8
    Public Const DAQMX100_CHNUM_10 As Integer = 10
    Public Const DAQMX100_CHNUM_30 As Integer = 30
    
    ' interval (msec)
    Public Const DAQMX100_INTERVAL_10 As Integer = 10
    Public Const DAQMX100_INTERVAL_50 As Integer = 50
    Public Const DAQMX100_INTERVAL_100 As Integer = 100
    Public Const DAQMX100_INTERVAL_200 As Integer = 200
    Public Const DAQMX100_INTERVAL_500 As Integer = 500
    Public Const DAQMX100_INTERVAL_1000 As Integer = 1000
    Public Const DAQMX100_INTERVAL_2000 As Integer = 2000
    Public Const DAQMX100_INTERVAL_5000 As Integer = 5000
    Public Const DAQMX100_INTERVAL_10000 As Integer = 10000
    Public Const DAQMX100_INTERVAL_20000 As Integer = 20000
    Public Const DAQMX100_INTERVAL_30000 As Integer = 30000
    Public Const DAQMX100_INTERVAL_60000 As Integer = 60000
    
    ' filter
    Public Const DAQMX100_FILTER_0 As Integer = 0
    Public Const DAQMX100_FILTER_5 As Integer = 1
    Public Const DAQMX100_FILTER_10 As Integer = 2
    Public Const DAQMX100_FILTER_20 As Integer = 3
    Public Const DAQMX100_FILTER_25 As Integer = 4
    Public Const DAQMX100_FILTER_40 As Integer = 5
    Public Const DAQMX100_FILTER_50 As Integer = 6
    Public Const DAQMX100_FILTER_100 As Integer = 7
    
    ' RJC Type
    Public Const DAQMX100_RJC_INTERNAL As Integer = 0
    Public Const DAQMX100_RJC_EXTERNAL As Integer = 1
    
    ' burnout
    Public Const DAQMX100_BURNOUT_OFF As Integer = 0
    Public Const DAQMX100_BURNOUT_UP As Integer = 1
    Public Const DAQMX100_BURNOUT_DOWN As Integer = 2
    
    ' unit type
    Public Const DAQMX100_UNITTYPE_NONE As Integer = &H00000000
    Public Const DAQMX100_UNITTYPE_MX100 As Integer = &H00010000
    
    ' terminal type
    Public Const DAQMX100_TERMINAL_SCREW As Integer = 0
    Public Const DAQMX100_TERMINAL_CLAMP As Integer = 1
    Public Const DAQMX100_TERMINAL_NDIS As Integer = 2
    
    ' AD
    Public Const DAQMX100_INTEGRAL_AUTO As Integer = 0
    Public Const DAQMX100_INTEGRAL_50HZ As Integer = 1
    Public Const DAQMX100_INTEGRAL_60HZ As Integer = 2
    
    ' temparature unit
    Public Const DAQMX100_TEMPUNIT_C As Integer = 0
    Public Const DAQMX100_TEMPUNIT_F As Integer = 1
    
    ' CF write mode
    Public Const DAQMX100_CFWRITEMODE_ONCE As Integer = 0
    Public Const DAQMX100_CFWRITEMODE_FIFO As Integer = 1
    
    ' CF status
    Public Const DAQMX100_CFSTATUS_NONE As Integer = &H0000
    Public Const DAQMX100_CFSTATUS_EXIST As Integer = &H0001
    Public Const DAQMX100_CFSTATUS_USE As Integer = &H0002
    Public Const DAQMX100_CFSTATUS_FORMAT As Integer = &H0004
    
    ' UNIT status
    Public Const DAQMX100_UNITSTAT_NONE As Integer = &H0000
    Public Const DAQMX100_UNITSTAT_INIT As Integer = &H0001
    Public Const DAQMX100_UNITSTAT_STOP As Integer = &H0002
    Public Const DAQMX100_UNITSTAT_RUN As Integer = &H0003
    Public Const DAQMX100_UNITSTAT_BACKUP As Integer = &H0004
    
    ' FIFO status
    Public Const DAQMX100_FIFOSTAT_NONE As Integer = DAQMX100_UNITSTAT_NONE
    Public Const DAQMX100_FIFOSTAT_INIT As Integer = DAQMX100_UNITSTAT_INIT
    Public Const DAQMX100_FIFOSTAT_STOP As Integer = DAQMX100_UNITSTAT_STOP
    Public Const DAQMX100_FIFOSTAT_RUN As Integer = DAQMX100_UNITSTAT_RUN
    Public Const DAQMX100_FIFOSTAT_BACKUP As Integer = DAQMX100_UNITSTAT_BACKUP
    
    ' segment display type
    Public Const DAQMX100_DISPTYPE_NONE As Integer = 0
    Public Const DAQMX100_DISPTYPE_ON As Integer = 1
    Public Const DAQMX100_DISPTYPE_BLINK As Integer = 2
    
    ' choice
    Public Const DAQMX100_CHOICE_PREV As Integer = 0
    Public Const DAQMX100_CHOICE_PRESET As Integer = 1
    
    ' transmit
    Public Const DAQMX100_TRANSMIT_NONE As Integer = 0
    Public Const DAQMX100_TRANSMIT_RUN As Integer = 1
    Public Const DAQMX100_TRANSMIT_STOP As Integer = 2
    
    ' balance
    Public Const DAQMX100_BALANCE_NONE As Integer = 0
    Public Const DAQMX100_BALANCE_DONE As Integer = 1
    Public Const DAQMX100_BALANCE_NG As Integer = 2
    Public Const DAQMX100_BALANCE_ERROR As Integer = 3
    
    ' option
    Public Const DAQMX100_OPTION_NONE As Integer = &H0000
    Public Const DAQMX100_OPTION_DS As Integer = &H0001
    
    ' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
    ' range
    ' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
    
    ' range type for special
    Public Const DAQMX100_RANGETYPE_DI As Integer = &H00010000
    Public Const DAQMX100_RANGETYPE_SKIP As Integer = &H00080000
    
    ' DI : special
    Public Const DAQMX100_RANGE_DI_LEVEL As Integer = DAQMX100_RANGETYPE_DI + &H0001
    Public Const DAQMX100_RANGE_DI_CONTACT As Integer = DAQMX100_RANGETYPE_DI + &H0002
    
    ' Skip
    Public Const DAQMX100_RANGE_SKIP As Integer = DAQMX100_RANGETYPE_SKIP
    
    ' Reference
    Public Const DAQMX100_RANGE_REFERENCE As Integer = -1
    
    ' Volt
    Public Const DAQMX100_RANGE_VOLT_20MV As Integer = &H0000
    Public Const DAQMX100_RANGE_VOLT_60MV As Integer = &H0001
    Public Const DAQMX100_RANGE_VOLT_200MV As Integer = &H0002
    Public Const DAQMX100_RANGE_VOLT_2V As Integer = &H0003
    Public Const DAQMX100_RANGE_VOLT_6V As Integer = &H0004
    Public Const DAQMX100_RANGE_VOLT_20V As Integer = &H0005
    Public Const DAQMX100_RANGE_VOLT_100V As Integer = &H0006
    Public Const DAQMX100_RANGE_VOLT_60MVH As Integer = &H0007
    Public Const DAQMX100_RANGE_VOLT_1V As Integer = &H0008
    Public Const DAQMX100_RANGE_VOLT_6VH As Integer = &H0009
    
    ' TC
    Public Const DAQMX100_RANGE_TC_R As Integer = &H0200
    Public Const DAQMX100_RANGE_TC_S As Integer = &H0201
    Public Const DAQMX100_RANGE_TC_B As Integer = &H0202
    Public Const DAQMX100_RANGE_TC_K As Integer = &H0203
    Public Const DAQMX100_RANGE_TC_E As Integer = &H0204
    Public Const DAQMX100_RANGE_TC_J As Integer = &H0205
    Public Const DAQMX100_RANGE_TC_T As Integer = &H0206
    Public Const DAQMX100_RANGE_TC_N As Integer = &H0207
    Public Const DAQMX100_RANGE_TC_W As Integer = &H0208
    Public Const DAQMX100_RANGE_TC_L As Integer = &H0209
    Public Const DAQMX100_RANGE_TC_U As Integer = &H020A
    Public Const DAQMX100_RANGE_TC_KP As Integer = &H020B
    Public Const DAQMX100_RANGE_TC_PL As Integer = &H020C
    Public Const DAQMX100_RANGE_TC_PR As Integer = &H020D
    Public Const DAQMX100_RANGE_TC_NNM As Integer = &H020E
    Public Const DAQMX100_RANGE_TC_WR As Integer = &H020F
    Public Const DAQMX100_RANGE_TC_WWR As Integer = &H0210
    Public Const DAQMX100_RANGE_TC_AWG As Integer = &H0211
    Public Const DAQMX100_RANGE_TC_XK As Integer = &H0212
    
    ' RTD 1mA
    Public Const DAQMX100_RANGE_RTD_1MAPT As Integer = &H0300
    Public Const DAQMX100_RANGE_RTD_1MAJPT As Integer = &H0301
    Public Const DAQMX100_RANGE_RTD_1MAPTH As Integer = &H0302
    Public Const DAQMX100_RANGE_RTD_1MAJPTH As Integer = &H0303
    Public Const DAQMX100_RANGE_RTD_1MANIS As Integer = &H0304
    Public Const DAQMX100_RANGE_RTD_1MANID As Integer = &H0305
    Public Const DAQMX100_RANGE_RTD_1MANI120 As Integer = &H0306
    Public Const DAQMX100_RANGE_RTD_1MAPT50 As Integer = &H0307
    Public Const DAQMX100_RANGE_RTD_1MACU10GE As Integer = &H0308
    Public Const DAQMX100_RANGE_RTD_1MACU10LN As Integer = &H0309
    Public Const DAQMX100_RANGE_RTD_1MACU10WEED As Integer = &H030A
    Public Const DAQMX100_RANGE_RTD_1MACU10BAILEY As Integer = &H030B
    Public Const DAQMX100_RANGE_RTD_1MAJ263B As Integer = &H030C
    Public Const DAQMX100_RANGE_RTD_1MACU10A392 As Integer = &H030D
    Public Const DAQMX100_RANGE_RTD_1MACU10A393 As Integer = &H030E
    Public Const DAQMX100_RANGE_RTD_1MACU25 As Integer = &H030F
    Public Const DAQMX100_RANGE_RTD_1MACU53 As Integer = &H0310
    Public Const DAQMX100_RANGE_RTD_1MACU100 As Integer = &H0311
    Public Const DAQMX100_RANGE_RTD_1MAPT25 As Integer = &H0312
    Public Const DAQMX100_RANGE_RTD_1MACU10GEH As Integer = &H0313
    Public Const DAQMX100_RANGE_RTD_1MACU10LNH As Integer = &H0314
    Public Const DAQMX100_RANGE_RTD_1MACU10WEEDH As Integer = &H0315
    Public Const DAQMX100_RANGE_RTD_1MACU10BAILEYH As Integer = &H0316
    Public Const DAQMX100_RANGE_RTD_1MAPTN As Integer = &H0317
    Public Const DAQMX100_RANGE_RTD_1MAJPTN As Integer = &H0318
    Public Const DAQMX100_RANGE_RTD_1MAPTG As Integer = &H0319
    Public Const DAQMX100_RANGE_RTD_1MACU100G As Integer = &H031A
    Public Const DAQMX100_RANGE_RTD_1MACU50G As Integer = &H031B
    Public Const DAQMX100_RANGE_RTD_1MACU10G As Integer = &H031C
    
    ' RTD 2mA
    Public Const DAQMX100_RANGE_RTD_2MAPT As Integer = &H0400
    Public Const DAQMX100_RANGE_RTD_2MAJPT As Integer = &H0401
    Public Const DAQMX100_RANGE_RTD_2MAPTH As Integer = &H0402
    Public Const DAQMX100_RANGE_RTD_2MAJPTH As Integer = &H0403
    Public Const DAQMX100_RANGE_RTD_2MAPT50 As Integer = &H0404
    Public Const DAQMX100_RANGE_RTD_2MACU10GE As Integer = &H0405
    Public Const DAQMX100_RANGE_RTD_2MACU10LN As Integer = &H0406
    Public Const DAQMX100_RANGE_RTD_2MACU10WEED As Integer = &H0407
    Public Const DAQMX100_RANGE_RTD_2MACU10BAILEY As Integer = &H0408
    Public Const DAQMX100_RANGE_RTD_2MAJ263B As Integer = &H0409
    Public Const DAQMX100_RANGE_RTD_2MACU10A392 As Integer = &H040A
    Public Const DAQMX100_RANGE_RTD_2MACU10A393 As Integer = &H040B
    Public Const DAQMX100_RANGE_RTD_2MACU25 As Integer = &H040C
    Public Const DAQMX100_RANGE_RTD_2MACU53 As Integer = &H040D
    Public Const DAQMX100_RANGE_RTD_2MACU100 As Integer = &H040E
    Public Const DAQMX100_RANGE_RTD_2MAPT25 As Integer = &H040F
    Public Const DAQMX100_RANGE_RTD_2MACU10GEH As Integer = &H0410
    Public Const DAQMX100_RANGE_RTD_2MACU10LNH As Integer = &H0411
    Public Const DAQMX100_RANGE_RTD_2MACU10WEEDH As Integer = &H0412
    Public Const DAQMX100_RANGE_RTD_2MACU10BAILEYH As Integer = &H0413
    Public Const DAQMX100_RANGE_RTD_2MAPTN As Integer = &H0414
    Public Const DAQMX100_RANGE_RTD_2MAJPTN As Integer = &H0415
    Public Const DAQMX100_RANGE_RTD_2MACU100G As Integer = &H0416
    Public Const DAQMX100_RANGE_RTD_2MACU50G As Integer = &H0417
    Public Const DAQMX100_RANGE_RTD_2MACU10G As Integer = &H0418
    
    ' DI : detail
    Public Const DAQMX100_RANGE_DI_LEVEL_AI As Integer = &H0100
    Public Const DAQMX100_RANGE_DI_CONTACT_AI4 As Integer = &H0101
    Public Const DAQMX100_RANGE_DI_CONTACT_AI10 As Integer = &H0102
    Public Const DAQMX100_RANGE_DI_LEVEL_DI As Integer = &H0103
    Public Const DAQMX100_RANGE_DI_CONTACT_DI As Integer = &H0104
    Public Const DAQMX100_RANGE_DI_LEVEL_DI5V As Integer = DAQMX100_RANGE_DI_LEVEL_DI
    Public Const DAQMX100_RANGE_DI_LEVEL_DI24V As Integer = &H0105
    Public Const DAQMX100_RANGE_DI_CONTACT_AI30 As Integer = DAQMX100_RANGE_DI_CONTACT_AI10
    
    ' RTD 0.25mA
    Public Const DAQMX100_RANGE_RTD_025MAPT500 As Integer = &H0500
    Public Const DAQMX100_RANGE_RTD_025MAPT1K As Integer = &H0501
    
    ' RES
    Public Const DAQMX100_RANGE_RES_20 As Integer = &H0600
    Public Const DAQMX100_RANGE_RES_200 As Integer = &H0601
    Public Const DAQMX100_RANGE_RES_2K As Integer = &H0602
    
    ' STR
    Public Const DAQMX100_RANGE_STRAIN_2K As Integer = &H0700
    Public Const DAQMX100_RANGE_STRAIN_20K As Integer = &H0701
    Public Const DAQMX100_RANGE_STRAIN_200K As Integer = &H0702
    
    ' AO
    Public Const DAQMX100_RANGE_AO_10V As Integer = &H1000
    Public Const DAQMX100_RANGE_AO_20MA As Integer = &H1001
    
    ' PWM
    Public Const DAQMX100_RANGE_PWM_1MS As Integer = &H1100
    Public Const DAQMX100_RANGE_PWM_10MS As Integer = &H1101
    
    ' CAN
    Public Const DAQMX100_RANGE_COM_CAN As Integer = &H0800
    
    ' PI
    Public Const DAQMX100_RANGE_PI_LEVEL As Integer = &H0900
    Public Const DAQMX100_RANGE_PI_CONTACT As Integer = &H0901
    
    ' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
    ' Output
    Public Const DAQMX100_OUTPUT_NONE As Integer = 0
    Public Const DAQMX100_OUTPUT_AO_10V As Integer = DAQMX100_RANGE_AO_10V
    Public Const DAQMX100_OUTPUT_AO_20MA As Integer = DAQMX100_RANGE_AO_20MA
    Public Const DAQMX100_OUTPUT_PWM_1MS As Integer = DAQMX100_RANGE_PWM_1MS
    Public Const DAQMX100_OUTPUT_PWM_10MS As Integer = DAQMX100_RANGE_PWM_10MS
    
    ' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
    ' configure item @see DAQMXItems.bas
    ' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
    
    ' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
    ' Connection
    ' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
    
    Public Declare Ansi Function openMX100 Lib "DAQMX100" (ByVal strAddress As String, ByRef errCode As Integer) As Integer
    
    Public Declare Ansi Function closeMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer) As Integer
    
    ' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
    ' FIFO
    ' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
    
    Public Declare Ansi Function measStartMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer) As Integer
    
    Public Declare Ansi Function measStopMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer) As Integer
    
    ' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
    ' Control
    ' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
    
    Public Declare Ansi Function setDateTimeNowMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer) As Integer
    
    Public Declare Ansi Function switchBackupMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal bBackup As Integer) As Integer
    
    Public Declare Ansi Function formatCFMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer) As Integer
    
    Public Declare Ansi Function reconstructMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer) As Integer
    
    Public Declare Ansi Function initSetValueMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer) As Integer
    
    Public Declare Ansi Function ackAlarmMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer) As Integer
    
    Public Declare Ansi Function displaySegmentMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal dispPattern0 As Integer, ByVal dispPattern1 As Integer, ByVal dispType As Integer, ByVal dispTime As Integer) As Integer
    
    Public Declare Ansi Function initDataChMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal chNo As Integer) As Integer
    
    Public Declare Ansi Function initDataFIFOMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal fifoNo As Integer) As Integer
    
    ' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
    ' Setup
    ' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
    
    Public Declare Ansi Function sendConfigMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer) As Integer
    
    Public Declare Ansi Function initBalanceMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer) As Integer
    
    Public Declare Ansi Function clearBalanceMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer) As Integer
    
    ' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
    ' Setting
    ' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
    
    Public Declare Ansi Function setRangeMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal chNo As Integer, ByVal iRange As Integer) As Integer
    
    Public Declare Ansi Function setChDELTAMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal chNo As Integer, ByVal refChNo As Integer, ByVal iRange As Integer) As Integer
    
    Public Declare Ansi Function setChRRJCMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal chNo As Integer, ByVal refChNo As Integer) As Integer
    
    Public Declare Ansi Function setChUnitMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal chNo As Integer, ByVal strUnit As String) As Integer
    
    Public Declare Ansi Function setChTagMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal chNo As Integer, ByVal strTag As String) As Integer
    
    Public Declare Ansi Function setChCommentMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal chNo As Integer, ByVal strComment As String) As Integer
    
    Public Declare Ansi Function setSpanMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal chNo As Integer, ByVal spanMin As Integer, ByVal spanMax As Integer) As Integer
    
    Public Declare Ansi Function setDoubleSpanMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal chNo As Integer, ByVal spanMin As Double, ByVal spanMax As Double) As Integer
    
    Public Declare Ansi Function setScaleMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal chNo As Integer, ByVal scaleMin As Integer, ByVal scaleMax As Integer, ByVal scalePoint As Integer) As Integer
    
    Public Declare Ansi Function setDoubleScaleMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal chNo As Integer, ByVal scaleMin As Double, ByVal scaleMax As Double, ByVal scalePoint As Integer) As Integer
    
    Public Declare Ansi Function setAlarmMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal chNo As Integer, ByVal levelNo As Integer, ByVal iAlarmType As Integer, ByVal value As Integer) As Integer
    
    Public Declare Ansi Function setDoubleAlarmMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal chNo As Integer, ByVal levelNo As Integer, ByVal iAlarmType As Integer, ByVal value As Double) As Integer
    
    Public Declare Ansi Function setAlarmValueMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal chNo As Integer, ByVal levelNo As Integer, ByVal iAlarmType As Integer, ByVal valueON As Integer, ByVal valueOFF As Integer) As Integer
    
    Public Declare Ansi Function setDoubleAlarmValueMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal chNo As Integer, ByVal levelNo As Integer, ByVal iAlarmType As Integer, ByVal valueON As Double, ByVal valueOFF As Double) As Integer
    
    Public Declare Ansi Function setHisterisysMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal chNo As Integer, ByVal levelNo As Integer, ByVal histerisys As Integer) As Integer
    
    Public Declare Ansi Function setDoubleHisterisysMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal chNo As Integer, ByVal levelNo As Integer, ByVal histerisys As Double) As Integer
    
    Public Declare Ansi Function setFilterMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal chNo As Integer, ByVal iFilter As Integer) As Integer
    
    Public Declare Ansi Function setRJCTypeMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal chNo As Integer, ByVal iRJCType As Integer, ByVal volt As Integer) As Integer
    
    Public Declare Ansi Function setBurnoutMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal chNo As Integer, ByVal iBurnout As Integer) As Integer
    
    Public Declare Ansi Function setDeenergizeMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal doNo As Integer, ByVal bDeenergize As Integer) As Integer
    
    Public Declare Ansi Function setHoldMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal doNo As Integer, ByVal bHold As Integer) As Integer
    
    Public Declare Ansi Function setRefAlarmMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal doNo As Integer, ByVal refChNo As Integer, ByVal levelNo As Integer, ByVal bValid As Integer) As Integer
    
    Public Declare Ansi Function setChKindMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal chNo As Integer, ByVal iKind As Integer, ByVal refChNo As Integer) As Integer
    
    Public Declare Ansi Function setChatFilterMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal chNo As Integer, ByVal bChatFilter As Integer) As Integer
    
    ' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
    
    Public Declare Ansi Function setIntervalMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal moduleNo As Integer, ByVal iInterval As Integer) As Integer
    
    Public Declare Ansi Function setIntegralMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal moduleNo As Integer, ByVal iIntegral As Integer) As Integer
    
    Public Declare Ansi Function setUnitNoMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal unitNo As Integer) As Integer
    
    Public Declare Ansi Function setUnitTempMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal iTempUnit As Integer) As Integer
    
    Public Declare Ansi Function setCFWriteModeMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal iCFWriteMode As Integer) As Integer
    
    Public Declare Ansi Function setOutputTypeMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal outputNo As Integer, ByVal iOutput As Integer) As Integer
    
    Public Declare Ansi Function setChoiceMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal outputNo As Integer, ByVal idleChoice As Integer, ByVal errorChoice As Integer, ByVal presetValue As Integer) As Integer
    
    Public Declare Ansi Function setDoubleChoiceMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal outputNo As Integer, ByVal idleChoice As Integer, ByVal errorChoice As Integer, ByVal presetValue As Double) As Integer
    
    Public Declare Ansi Function setPulseTimeMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal outputNo As Integer, ByVal pulseTime As Integer) As Integer
    
    ' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
    ' Data Operation
    ' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
    
    Public Declare Ansi Function createDOMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByRef errorCode As Integer) As Integer
    
    Public Declare Ansi Function deleteDOMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal idDO As Integer) As Integer
    
    Public Declare Ansi Function changeDOMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal idDO As Integer, ByVal doNo As Integer, ByVal bValid As Integer, ByVal bONOFF As Integer) As Integer
    
    Public Declare Ansi Function copyDOMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal idDO As Integer, ByVal idDOSrc As Integer) As Integer
    
    Public Declare Ansi Function commandDOMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal idDO As Integer) As Integer
    
    Public Declare Ansi Function switchDOMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal idDO As Integer, ByVal bONOFF As Integer) As Integer
    
    ' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
    
    Public Declare Ansi Function createAOPWMMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByRef errCode As Integer) As Integer
    
    Public Declare Ansi Function  deleteAOPWMMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal idAOPWM As Integer) As Integer
    
    Public Declare Ansi Function changeAOPWMMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal idAOPWM As Integer, ByVal aopwmNo As Integer, ByVal bValid As Integer, ByVal iAOPWMValue As Integer) As Integer
    
    Public Declare Ansi Function changeAOPWMValueMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal idAOPWM As Integer, ByVal aopwmNo As Integer, ByVal bValid As Integer, ByVal realValue As Double) As Integer
    
    Public Declare Ansi Function copyAOPWMMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal idAOPWM As Integer, ByVal idAOPWMSrc As Integer) As Integer
    
    Public Declare Ansi Function commandAOPWMMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal idAOPWM As Integer) As Integer
    
    ' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
    
    Public Declare Ansi Function createBalanceMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByRef errCode As Integer) As Integer
    
    Public Declare Ansi Function deleteBalanceMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal idBalance As Integer) As Integer
    
    Public Declare Ansi Function changeBalanceMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal idBalance As Integer, ByVal balanceNo As Integer, ByVal bValid As Integer, ByVal iValue As Integer) As Integer
    
    Public Declare Ansi Function copyBalanceMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal idBalance As Integer, ByVal idBalanceSrc As Integer) As Integer
    
    Public Declare Ansi Function commandBalanceMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal idBalance As Integer) As Integer
    
    ' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
    
    Public Declare Ansi Function createTransmitMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByRef errCode As Integer) As Integer
    
    Public Declare Ansi Function deleteTransmitMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal idTrans As Integer) As Integer
    
    Public Declare Ansi Function changeTransmitMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal idTrans As Integer, ByVal aopwmNo As Integer, ByVal iTransmit As Integer) As Integer
    
    Public Declare Ansi Function copyTransmitMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal idTrans As Integer, ByVal idTransSrc As Integer) As Integer
    
    Public Declare Ansi Function commandTransmitMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal idTrans As Integer) As Integer
    
    Public Declare Ansi Function switchTransmitMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal idTrans As Integer, ByVal iTransmit As Integer) As Integer
    
    ' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
    ' Update
    ' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
    
    Public Declare Ansi Function updateStatusMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer) As Integer
    
    Public Declare Ansi Function updateSystemMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer) As Integer
    
    Public Declare Ansi Function updateConfigMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer) As Integer
    
    Public Declare Ansi Function updateDODataMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer) As Integer
    
    Public Declare Ansi Function updateAOPWMDataMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer) As Integer
    
    Public Declare Ansi Function updateInfoChMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal chNo As Integer) As Integer
    
    Public Declare Ansi Function updateBalanceMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer) As Integer
    
    Public Declare Ansi Function updateOutputMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer) As Integer
    
    ' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
    ' Data Aquisition
    ' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
    
    Public Declare Ansi Function measDataChMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal chNo As Integer) As Integer
    
    Public Declare Ansi Function measInstChMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal chNo As Integer) As Integer
    
    Public Declare Ansi Function measDataFIFOMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal fifoNo As Integer) As Integer
    
    Public Declare Ansi Function measInstFIFOMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal fifoNo As Integer) As Integer
    
    ' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
    ' Item
    ' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
    
    Public Declare Ansi Function getItemAllMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer) As Integer
    
    Public Declare Ansi Function setItemAllMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer) As Integer
    
    Public Declare Ansi Function readItemMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal itemNo As Integer, ByVal strItem As String, ByVal lenItem As Integer, ByRef realLen As Integer) As Integer
    
    Public Declare Ansi Function writeItemMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal itemNo As Integer, ByVal strItem As String) As Integer
    
    Public Declare Ansi Function initItemMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer) As Integer
    
    ' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
    ' Get Current Measured Data
    ' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
    
    Public Declare Ansi Function dataValueMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal chNo As Integer) As Integer
    
    Public Declare Ansi Function dataStatusMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal chNo As Integer) As Integer
    
    Public Declare Ansi Function dataAlarmMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal chNo As Integer, ByVal levelNo As Integer) As Integer
    
    Public Declare Ansi Function dataDoubleValueMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal chNo As Integer) As Double
    
    Public Declare Ansi Function dataStringValueMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal chNo As Integer, ByVal strValue As String, ByVal lenValue As Integer) As Integer
    
    Public Declare Ansi Function dataTimeMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal chNo As Integer) As Integer
    
    Public Declare Ansi Function dataMilliSecMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal chNo As Integer) As Integer
    
    Public Declare Ansi Function dataYearMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal chNo As Integer) As Integer
    
    Public Declare Ansi Function dataMonthMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal chNo As Integer) As Integer
    
    Public Declare Ansi Function dataDayMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal chNo As Integer) As Integer
    
    Public Declare Ansi Function dataHourMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal chNo As Integer) As Integer
    
    Public Declare Ansi Function dataMinuteMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal chNo As Integer) As Integer
    
    Public Declare Ansi Function dataSecondMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal chNo As Integer) As Integer
    
    Public Declare Ansi Function dataValidMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal chNo As Integer) As Integer
    
    ' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
    ' Get Channel Information
    ' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
    
    Public Declare Ansi Function channelFIFONoMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal chNo As Integer) As Integer
    
    Public Declare Ansi Function channelFIFOIndexMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal chNo As Integer) As Integer
    
    Public Declare Ansi Function channelDisplayMinMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal chNo As Integer) As Double
    
    Public Declare Ansi Function channelDisplayMaxMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal chNo As Integer) As Double
    
    Public Declare Ansi Function channelRealMinMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal chNo As Integer) As Double
    
    Public Declare Ansi Function channelRealMaxMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal chNo As Integer) As Double
    
    ' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
    ' Get Channel Configure
    ' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
    
    Public Declare Ansi Function channelValidMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal chNo As Integer) As Integer
    
    Public Declare Ansi Function channelPointMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal chNo As Integer) As Integer
    
    Public Declare Ansi Function channelKindMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal chNo As Integer) As Integer
    
    Public Declare Ansi Function channelRangeMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal chNo As Integer) As Integer
    
    Public Declare Ansi Function channelScaleTypeMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal chNo As Integer) As Integer
    
    ' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
    
    Public Declare Ansi Function toChannelUnitMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal chNo As Integer, ByVal strUnit As String, ByVal lenUnit As Integer) As Integer
    
    Public Declare Ansi Function toChannelTagMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal chNo As Integer, ByVal strTag As String, ByVal lenTag As Integer) As Integer
    
    Public Declare Ansi Function toChannelCommentMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal chNo As Integer, ByVal strComment As String, ByVal lenComment As Integer) As Integer
    
    Public Declare Ansi Function channelSpanMinMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal chNo As Integer) As Integer
    
    Public Declare Ansi Function channelDoubleSpanMinMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal chNo As Integer) As Double
    
    Public Declare Ansi Function channelSpanMaxMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal chNo As Integer) As Integer
    
    Public Declare Ansi Function channelDoubleSpanMaxMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal chNo As Integer) As Double
    
    Public Declare Ansi Function channelScaleMinMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal chNo As Integer) As Integer
    
    Public Declare Ansi Function channelDoubleScaleMinMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal chNo As Integer) As Double
    
    Public Declare Ansi Function channelScaleMaxMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal chNo As Integer) As Integer
    
    Public Declare Ansi Function channelDoubleScaleMaxMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal chNo As Integer) As Double
    
    ' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
    
    Public Declare Ansi Function alarmTypeMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal chNo As Integer, ByVal levelNo As Integer) As Integer
    
    Public Declare Ansi Function alarmValueONMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal chNo As Integer, ByVal levelNo As Integer) As Integer
    
    Public Declare Ansi Function alarmDoubleValueONMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal chNo As Integer, ByVal levelNo As Integer) As Double
    
    Public Declare Ansi Function alarmValueOFFMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal chNo As Integer, ByVal levelNo As Integer) As Integer
    
    Public Declare Ansi Function alarmDoubleValueOFFMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal chNo As Integer, ByVal levelNo As Integer) As Double
    
    Public Declare Ansi Function alarmHisterisysMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal chNo As Integer, ByVal levelNo As Integer) As Integer
    
    Public Declare Ansi Function alarmDoubleHisterisysMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal chNo As Integer, ByVal levelNo As Integer) As Double
    
    ' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
    
    Public Declare Ansi Function channelFilterMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal chNo As Integer) As Integer
    
    Public Declare Ansi Function channelRJCTypeMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal chNo As Integer) As Integer
    
    Public Declare Ansi Function channelRJCVoltMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal chNo As Integer) As Integer
    
    Public Declare Ansi Function channelBurnoutMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal chNo As Integer) As Integer
    
    Public Declare Ansi Function channelDeenergizeMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal doNo As Integer) As Integer
    
    Public Declare Ansi Function channelHoldMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal doNo As Integer) As Integer
    
    Public Declare Ansi Function channelRefAlarmMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal doNo As Integer, ByVal refChNo As Integer, ByVal levelNo As Integer) As Integer
    
    Public Declare Ansi Function channelRefChNoMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal chNo As Integer) As Integer
    
    ' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
    
    Public Declare Ansi Function channelBalanceValidMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal balanceNo As Integer) As Integer
    
    Public Declare Ansi Function channelBalanceValueMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal balanceNo As Integer) As Integer
    
    ' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
    
    Public Declare Ansi Function channelOutputTypeMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal outputNo As Integer) As Integer
    
    Public Declare Ansi Function channelIdleChoiceMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal outputNo As Integer) As Integer
    
    Public Declare Ansi Function channelErrorChoiceMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal outputNo As Integer) As Integer
    
    Public Declare Ansi Function channelPresetValueMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal outputNo As Integer) As Integer
    
    Public Declare Ansi Function channelDoublePresetValueMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal outputNo As Integer) As Double
    
    Public Declare Ansi Function channelPulseTimeMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal outputNo As Integer) As Integer
    
    ' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
    
    Public Declare Ansi Function channelChatFilterMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal chNo As Integer) As Integer
    
    ' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
    ' Get Network Data
    ' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
    
    Public Declare Ansi Function toNetHostMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal strHost As String, ByVal lenHost As Integer) As Integer
    
    Public Declare Ansi Function netAddressMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer) As Integer
    
    Public Declare Ansi Function netPortMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer) As Integer
    
    Public Declare Ansi Function netSubmaskMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer) As Integer
    
    Public Declare Ansi Function netGatewayMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer) As Integer
    
    ' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
    ' Get System Data
    ' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
    
    Public Declare Ansi Function moduleTypeMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal moduleNo As Integer) As Integer
    
    Public Declare Ansi Function moduleChNumMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal moduleNo As Integer) As Integer
    
    Public Declare Ansi Function moduleIntervalMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal moduleNo As Integer) As Integer
    
    Public Declare Ansi Function moduleIntegralMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal moduleNo As Integer) As Integer
    
    Public Declare Ansi Function moduleValidMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal moduleNo As Integer) As Integer
    
    Public Declare Ansi Function moduleStandbyTypeMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal moduleNo As Integer) As Integer
    
    Public Declare Ansi Function moduleRealTypeMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal moduleNo As Integer) As Integer
    
    Public Declare Ansi Function moduleTerminalMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal moduleNo As Integer) As Integer
    
    Public Declare Ansi Function moduleVersionMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal moduleNo As Integer) As Integer
    
    Public Declare Ansi Function moduleFIFONoMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal moduleNo As Integer) As Integer
    
    Public Declare Ansi Function toModuleSerialMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal moduleNo As Integer, ByVal strSerial As String, ByVal lenSerial As Integer) As Integer
    
    ' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
    
    Public Declare Ansi Function unitTypeMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer) As Integer
    
    Public Declare Ansi Function unitStyleMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer) As Integer
    
    Public Declare Ansi Function unitNoMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer) As Integer
    
    Public Declare Ansi Function unitTempMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer) As Integer
    
    Public Declare Ansi Function unitFrequencyMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer) As Integer
    
    Public Declare Ansi Function toUnitPartNoMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal strPartNo As String, ByVal lenPartNo As Integer) As Integer
    
    Public Declare Ansi Function unitOptionMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer) As Integer
    
    Public Declare Ansi Function toUnitSerialMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal strSerial As String, ByVal lenSerial As Integer) As Integer
    
    Public Declare Ansi Function unitMACMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal index As Integer) As Integer
    
    Public Declare Ansi Function unitCFWriteModeMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer) As Integer
    
    ' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
    ' Get Status Data
    ' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
    
    Public Declare Ansi Function statusUnitMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer) As Integer
    
    Public Declare Ansi Function statusFIFONumMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer) As Integer
    
    Public Declare Ansi Function statusBackupMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer) As Integer
    
    Public Declare Ansi Function statusFIFOMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal fifoNo As Integer) As Integer
    
    Public Declare Ansi Function statusFIFOIntervalMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal fifoNo As Integer) As Integer
    
    Public Declare Ansi Function statusCFMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer) As Integer
    
    Public Declare Ansi Function statusCFSizeMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer) As Integer
    
    Public Declare Ansi Function statusCFRemainMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer) As Integer
    
    Public Declare Ansi Function statusTimeMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer) As Integer
    
    Public Declare Ansi Function statusMilliSecMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer) As Integer
    
    Public Declare Ansi Function statusYearMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer) As Integer
    
    Public Declare Ansi Function statusMonthMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer) As Integer
    
    Public Declare Ansi Function statusDayMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer) As Integer
    
    Public Declare Ansi Function statusHourMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer) As Integer
    
    Public Declare Ansi Function statusMinuteMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer) As Integer
    
    Public Declare Ansi Function statusSecondMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer) As Integer
    
    ' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
    ' Get Current Data
    ' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
    
    Public Declare Ansi Function currentDOValidMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal doNo As Integer) As Integer
    
    Public Declare Ansi Function currentDOValueMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal doNo As Integer) As Integer
    
    Public Declare Ansi Function currentAOPWMValidMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal aopwmNo As Integer) As Integer
    
    Public Declare Ansi Function currentAOPWMValueMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal aopwmNo As Integer) As Integer
    
    Public Declare Ansi Function currentDoubleAOPWMValueMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal aopwmNo As Integer) As Double
    
    Public Declare Ansi Function currentBalanceValidMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal balanceNo As Integer) As Integer
    
    Public Declare Ansi Function currentBalanceValueMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal balanceNo As Integer) As Integer
    
    Public Declare Ansi Function currentBalanceResultMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal balanceNo As Integer) As Integer
    
    Public Declare Ansi Function currentTransmitMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal aopwmNo As Integer) As Integer
    
    ' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
    ' Get User Data
    ' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
    
    Public Declare Ansi Function userDOValidMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal idDO As Integer, ByVal doNo As Integer) As Integer
    
    Public Declare Ansi Function userDOValueMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal idDO As Integer, ByVal doNo As Integer) As Integer
    
    Public Declare Ansi Function userAOPWMValidMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal idAOPWM As Integer, ByVal aopwmNo As Integer) As Integer
    
    Public Declare Ansi Function userAOPWMValueMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal idAOPWM As Integer, ByVal aopwmNo As Integer) As Integer
    
    Public Declare Ansi Function userDoubleAOPWMValueMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal idAOPWM As Integer, ByVal aopwmNo As Integer) As Double
    
    Public Declare Ansi Function userBalanceValidMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal idBalance As Integer, ByVal balanceNo As Integer) As Integer
    
    Public Declare Ansi Function userBalanceValueMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal idBalance As Integer, ByVal balanceNo As Integer) As Integer
    
    Public Declare Ansi Function userTransmitMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal idTrans As Integer, ByVal aopwmNo As Integer) As Integer
    
    ' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
    ' Utility
    ' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
    
    Public Declare Ansi Function dataNumChMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal chNo As Integer) As Integer
    
    Public Declare Ansi Function dataNumFIFOMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal fifoNo As Integer) As Integer
    
    Public Declare Ansi Function lastErrorMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer) As Integer
    
    Public Declare Ansi Function toErrorMessageMX100 Lib "DAQMX100" (ByVal errCode As Integer, ByVal errStr As String, ByVal errLen As Integer) As Integer
    
    Public Declare Ansi Function errorMaxLengthMX100 Lib "DAQMX100" () As Integer
    
    Public Declare Ansi Function itemErrorMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer) As Integer
    
    Public Declare Ansi Function channelNumberMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal fifoNo As Integer, ByVal fifioIndex As Integer) As Integer
    
    Public Declare Ansi Function rangePointMX100 Lib "DAQMX100" (ByVal daqmx100 As Integer, ByVal iRange As Integer) As Integer
    
    Public Declare Ansi Function toDoubleValueMX100 Lib "DAQMX100" (ByVal dataValue As Integer, ByVal point As Integer) As Double
    
    Public Declare Ansi Function toStringValueMX100 Lib "DAQMX100" (ByVal dataValue As Integer, ByVal point As Integer, ByVal strValue As String, ByVal lenValue As Integer) As Integer
    
    Public Declare Ansi Function toAlarmNameMX100 Lib "DAQMX100" (ByVal iAlarmType As Integer, ByVal strAlarm As String, ByVal lenAlarm As Integer) As Integer
    
    Public Declare Ansi Function alarmMaxLengthMX100 Lib "DAQMX100" () As Integer
    
    Public Declare Ansi Function versionAPIMX100 Lib "DAQMX100" () As Integer
    
    Public Declare Ansi Function revisionAPIMX100 Lib "DAQMX100" () As Integer
    
    Public Declare Ansi Function addressPartMX100 Lib "DAQMX100" (ByVal address As Integer, ByVal index As Integer) As Integer
    
    Public Declare Ansi Function toAOPWMValueMX100 Lib "DAQMX100" (ByVal realValue As Double, ByVal iRangeAOPWM As Integer) As Integer
    
    Public Declare Ansi Function toRealValueMX100 Lib "DAQMX100" (ByVal iAOPWMValue As Integer, ByVal iRangeAOPWM As Integer) As Double
    
    Public Declare Ansi Function toItemNameMX100 Lib "DAQMX100" (ByVal itemNo As Integer, ByVal strItem As String, ByVal lenItem As Integer) As Integer
    
    Public Declare Ansi Function toItemNoMX100 Lib "DAQMX100" (ByVal strItem As String) As Integer
    
    Public Declare Ansi Function itemMaxLengthMX100 Lib "DAQMX100" () As Integer
    
    Public Declare Ansi Function toStyleVersionMX100 Lib "DAQMX100" (ByVal style As Integer) As Integer
    
    ' -- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- --
End Module
