﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//     Website: http://ITdos.com/Dos/ORM/Index.html
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------


using System;
using System.Data;
using System.Data.Common;
using Dos.ORM;
using Dos.ORM.Common;

namespace SolarhotwaterprojectModel
{

	/// <summary>
	/// 实体类系统采集器Index 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Table("系统采集器Index")]
	[Serializable]
	public partial class 系统采集器Index : Entity 
	{
		#region Model
		private string _TimeStamp;
		private float? _Tet;
		private float? _Eh;
		private float? _Tt;
		private float? _Wl;
		private float? _Tws;
		private float? _Irr;
		private float? _Cit1;
		private float? _Cot1;
		private float? _Cit2;
		private float? _Cot2;
		private float? _Pit1;
		private float? _Pot1;
		private float? _Pit2;
		private float? _Pot2;
		private float? _Cwt;
		private float? _Wst;
		private float? _Rwt;
		private float? _Foc1;
		private float? _Foc2;
		private float? _Poc1;
		private float? _Poc2;
		private float? _Cwf;
		private float? _Wsf;
		private float? _Rwf;
		private float? _Allc;
		private float? _AllMv;
		private float? _AllEsys;
		private float? _Tsp;
		private float? _Tsc;
		private float? _AllEhp;
		private float? _Hpp;
		private float? _Hpc;
		private float? _AllEc;
		private float? _Csp;
		private float? _Csc;
		private float? _AllQs;
		private float? _AllQc;
		private float? _AllQhp;
		private float? _AllQtc;
		private float? _AllQsh;
		private float? _AllQuse;
		private float? _AllQtanks;
		private float? _AllQbm;
		private float? _AllQu;
		private float? _AllQss;
		private float? _Allmco2;
		private float? _Allmso2;
		private float? _Allmnox;
		private float? _Allmfc;
		private float? _fc;
		private float? _ηc;
		private float? _COPc;
		private float? _COPhp;
		private float? _COPsys;
		private float? _COPr;
		private float? _PlcSv;
		private float? _PlcPv;
		private float? _PlcOp;
		private float? _CircleMeterTemp;
		private float? _ColdMeterTemp;
		private float? _SupplyMeterTemp;
		private float? _AllConsum1;
		private float? _AllConsum2;
		private float? _AllP1Consum;
		private float? _AllP2Consum;
		private float? _AllCircleConsum;
		private float? _AllSupplyConsum;
		private float? _GuangZhaoDu;
		/// <summary>
		/// 
		/// </summary>
		public string TimeStamp
		{
			get{ return _TimeStamp; }
			set
			{
				this.OnPropertyValueChange(_.TimeStamp,_TimeStamp,value);
				this._TimeStamp=value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public float? Tet
		{
			get{ return _Tet; }
			set
			{
				this.OnPropertyValueChange(_.Tet,_Tet,value);
				this._Tet=value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public float? Eh
		{
			get{ return _Eh; }
			set
			{
				this.OnPropertyValueChange(_.Eh,_Eh,value);
				this._Eh=value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public float? Tt
		{
			get{ return _Tt; }
			set
			{
				this.OnPropertyValueChange(_.Tt,_Tt,value);
				this._Tt=value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public float? Wl
		{
			get{ return _Wl; }
			set
			{
				this.OnPropertyValueChange(_.Wl,_Wl,value);
				this._Wl=value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public float? Tws
		{
			get{ return _Tws; }
			set
			{
				this.OnPropertyValueChange(_.Tws,_Tws,value);
				this._Tws=value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public float? Irr
		{
			get{ return _Irr; }
			set
			{
				this.OnPropertyValueChange(_.Irr,_Irr,value);
				this._Irr=value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public float? Cit1
		{
			get{ return _Cit1; }
			set
			{
				this.OnPropertyValueChange(_.Cit1,_Cit1,value);
				this._Cit1=value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public float? Cot1
		{
			get{ return _Cot1; }
			set
			{
				this.OnPropertyValueChange(_.Cot1,_Cot1,value);
				this._Cot1=value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public float? Cit2
		{
			get{ return _Cit2; }
			set
			{
				this.OnPropertyValueChange(_.Cit2,_Cit2,value);
				this._Cit2=value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public float? Cot2
		{
			get{ return _Cot2; }
			set
			{
				this.OnPropertyValueChange(_.Cot2,_Cot2,value);
				this._Cot2=value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public float? Pit1
		{
			get{ return _Pit1; }
			set
			{
				this.OnPropertyValueChange(_.Pit1,_Pit1,value);
				this._Pit1=value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public float? Pot1
		{
			get{ return _Pot1; }
			set
			{
				this.OnPropertyValueChange(_.Pot1,_Pot1,value);
				this._Pot1=value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public float? Pit2
		{
			get{ return _Pit2; }
			set
			{
				this.OnPropertyValueChange(_.Pit2,_Pit2,value);
				this._Pit2=value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public float? Pot2
		{
			get{ return _Pot2; }
			set
			{
				this.OnPropertyValueChange(_.Pot2,_Pot2,value);
				this._Pot2=value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public float? Cwt
		{
			get{ return _Cwt; }
			set
			{
				this.OnPropertyValueChange(_.Cwt,_Cwt,value);
				this._Cwt=value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public float? Wst
		{
			get{ return _Wst; }
			set
			{
				this.OnPropertyValueChange(_.Wst,_Wst,value);
				this._Wst=value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public float? Rwt
		{
			get{ return _Rwt; }
			set
			{
				this.OnPropertyValueChange(_.Rwt,_Rwt,value);
				this._Rwt=value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public float? Foc1
		{
			get{ return _Foc1; }
			set
			{
				this.OnPropertyValueChange(_.Foc1,_Foc1,value);
				this._Foc1=value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public float? Foc2
		{
			get{ return _Foc2; }
			set
			{
				this.OnPropertyValueChange(_.Foc2,_Foc2,value);
				this._Foc2=value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public float? Poc1
		{
			get{ return _Poc1; }
			set
			{
				this.OnPropertyValueChange(_.Poc1,_Poc1,value);
				this._Poc1=value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public float? Poc2
		{
			get{ return _Poc2; }
			set
			{
				this.OnPropertyValueChange(_.Poc2,_Poc2,value);
				this._Poc2=value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public float? Cwf
		{
			get{ return _Cwf; }
			set
			{
				this.OnPropertyValueChange(_.Cwf,_Cwf,value);
				this._Cwf=value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public float? Wsf
		{
			get{ return _Wsf; }
			set
			{
				this.OnPropertyValueChange(_.Wsf,_Wsf,value);
				this._Wsf=value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public float? Rwf
		{
			get{ return _Rwf; }
			set
			{
				this.OnPropertyValueChange(_.Rwf,_Rwf,value);
				this._Rwf=value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public float? Allc
		{
			get{ return _Allc; }
			set
			{
				this.OnPropertyValueChange(_.Allc,_Allc,value);
				this._Allc=value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public float? AllMv
		{
			get{ return _AllMv; }
			set
			{
				this.OnPropertyValueChange(_.AllMv,_AllMv,value);
				this._AllMv=value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public float? AllEsys
		{
			get{ return _AllEsys; }
			set
			{
				this.OnPropertyValueChange(_.AllEsys,_AllEsys,value);
				this._AllEsys=value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public float? Tsp
		{
			get{ return _Tsp; }
			set
			{
				this.OnPropertyValueChange(_.Tsp,_Tsp,value);
				this._Tsp=value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public float? Tsc
		{
			get{ return _Tsc; }
			set
			{
				this.OnPropertyValueChange(_.Tsc,_Tsc,value);
				this._Tsc=value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public float? AllEhp
		{
			get{ return _AllEhp; }
			set
			{
				this.OnPropertyValueChange(_.AllEhp,_AllEhp,value);
				this._AllEhp=value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public float? Hpp
		{
			get{ return _Hpp; }
			set
			{
				this.OnPropertyValueChange(_.Hpp,_Hpp,value);
				this._Hpp=value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public float? Hpc
		{
			get{ return _Hpc; }
			set
			{
				this.OnPropertyValueChange(_.Hpc,_Hpc,value);
				this._Hpc=value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public float? AllEc
		{
			get{ return _AllEc; }
			set
			{
				this.OnPropertyValueChange(_.AllEc,_AllEc,value);
				this._AllEc=value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public float? Csp
		{
			get{ return _Csp; }
			set
			{
				this.OnPropertyValueChange(_.Csp,_Csp,value);
				this._Csp=value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public float? Csc
		{
			get{ return _Csc; }
			set
			{
				this.OnPropertyValueChange(_.Csc,_Csc,value);
				this._Csc=value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public float? AllQs
		{
			get{ return _AllQs; }
			set
			{
				this.OnPropertyValueChange(_.AllQs,_AllQs,value);
				this._AllQs=value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public float? AllQc
		{
			get{ return _AllQc; }
			set
			{
				this.OnPropertyValueChange(_.AllQc,_AllQc,value);
				this._AllQc=value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public float? AllQhp
		{
			get{ return _AllQhp; }
			set
			{
				this.OnPropertyValueChange(_.AllQhp,_AllQhp,value);
				this._AllQhp=value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public float? AllQtc
		{
			get{ return _AllQtc; }
			set
			{
				this.OnPropertyValueChange(_.AllQtc,_AllQtc,value);
				this._AllQtc=value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public float? AllQsh
		{
			get{ return _AllQsh; }
			set
			{
				this.OnPropertyValueChange(_.AllQsh,_AllQsh,value);
				this._AllQsh=value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public float? AllQuse
		{
			get{ return _AllQuse; }
			set
			{
				this.OnPropertyValueChange(_.AllQuse,_AllQuse,value);
				this._AllQuse=value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public float? AllQtanks
		{
			get{ return _AllQtanks; }
			set
			{
				this.OnPropertyValueChange(_.AllQtanks,_AllQtanks,value);
				this._AllQtanks=value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public float? AllQbm
		{
			get{ return _AllQbm; }
			set
			{
				this.OnPropertyValueChange(_.AllQbm,_AllQbm,value);
				this._AllQbm=value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public float? AllQu
		{
			get{ return _AllQu; }
			set
			{
				this.OnPropertyValueChange(_.AllQu,_AllQu,value);
				this._AllQu=value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public float? AllQss
		{
			get{ return _AllQss; }
			set
			{
				this.OnPropertyValueChange(_.AllQss,_AllQss,value);
				this._AllQss=value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public float? Allmco2
		{
			get{ return _Allmco2; }
			set
			{
				this.OnPropertyValueChange(_.Allmco2,_Allmco2,value);
				this._Allmco2=value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public float? Allmso2
		{
			get{ return _Allmso2; }
			set
			{
				this.OnPropertyValueChange(_.Allmso2,_Allmso2,value);
				this._Allmso2=value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public float? Allmnox
		{
			get{ return _Allmnox; }
			set
			{
				this.OnPropertyValueChange(_.Allmnox,_Allmnox,value);
				this._Allmnox=value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public float? Allmfc
		{
			get{ return _Allmfc; }
			set
			{
				this.OnPropertyValueChange(_.Allmfc,_Allmfc,value);
				this._Allmfc=value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public float? fc
		{
			get{ return _fc; }
			set
			{
				this.OnPropertyValueChange(_.fc,_fc,value);
				this._fc=value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public float? ηc
		{
			get{ return _ηc; }
			set
			{
				this.OnPropertyValueChange(_.ηc,_ηc,value);
				this._ηc=value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public float? COPc
		{
			get{ return _COPc; }
			set
			{
				this.OnPropertyValueChange(_.COPc,_COPc,value);
				this._COPc=value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public float? COPhp
		{
			get{ return _COPhp; }
			set
			{
				this.OnPropertyValueChange(_.COPhp,_COPhp,value);
				this._COPhp=value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public float? COPsys
		{
			get{ return _COPsys; }
			set
			{
				this.OnPropertyValueChange(_.COPsys,_COPsys,value);
				this._COPsys=value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public float? COPr
		{
			get{ return _COPr; }
			set
			{
				this.OnPropertyValueChange(_.COPr,_COPr,value);
				this._COPr=value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public float? PlcSv
		{
			get{ return _PlcSv; }
			set
			{
				this.OnPropertyValueChange(_.PlcSv,_PlcSv,value);
				this._PlcSv=value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public float? PlcPv
		{
			get{ return _PlcPv; }
			set
			{
				this.OnPropertyValueChange(_.PlcPv,_PlcPv,value);
				this._PlcPv=value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public float? PlcOp
		{
			get{ return _PlcOp; }
			set
			{
				this.OnPropertyValueChange(_.PlcOp,_PlcOp,value);
				this._PlcOp=value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public float? CircleMeterTemp
		{
			get{ return _CircleMeterTemp; }
			set
			{
				this.OnPropertyValueChange(_.CircleMeterTemp,_CircleMeterTemp,value);
				this._CircleMeterTemp=value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public float? ColdMeterTemp
		{
			get{ return _ColdMeterTemp; }
			set
			{
				this.OnPropertyValueChange(_.ColdMeterTemp,_ColdMeterTemp,value);
				this._ColdMeterTemp=value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public float? SupplyMeterTemp
		{
			get{ return _SupplyMeterTemp; }
			set
			{
				this.OnPropertyValueChange(_.SupplyMeterTemp,_SupplyMeterTemp,value);
				this._SupplyMeterTemp=value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public float? AllConsum1
		{
			get{ return _AllConsum1; }
			set
			{
				this.OnPropertyValueChange(_.AllConsum1,_AllConsum1,value);
				this._AllConsum1=value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public float? AllConsum2
		{
			get{ return _AllConsum2; }
			set
			{
				this.OnPropertyValueChange(_.AllConsum2,_AllConsum2,value);
				this._AllConsum2=value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public float? AllP1Consum
		{
			get{ return _AllP1Consum; }
			set
			{
				this.OnPropertyValueChange(_.AllP1Consum,_AllP1Consum,value);
				this._AllP1Consum=value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public float? AllP2Consum
		{
			get{ return _AllP2Consum; }
			set
			{
				this.OnPropertyValueChange(_.AllP2Consum,_AllP2Consum,value);
				this._AllP2Consum=value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public float? AllCircleConsum
		{
			get{ return _AllCircleConsum; }
			set
			{
				this.OnPropertyValueChange(_.AllCircleConsum,_AllCircleConsum,value);
				this._AllCircleConsum=value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public float? AllSupplyConsum
		{
			get{ return _AllSupplyConsum; }
			set
			{
				this.OnPropertyValueChange(_.AllSupplyConsum,_AllSupplyConsum,value);
				this._AllSupplyConsum=value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public float? GuangZhaoDu
		{
			get{ return _GuangZhaoDu; }
			set
			{
				this.OnPropertyValueChange(_.GuangZhaoDu,_GuangZhaoDu,value);
				this._GuangZhaoDu=value;
			}
		}
		#endregion

		#region Method
		/// <summary>
		/// 获取列信息
		/// </summary>
		public override Field[] GetFields()
		{
			return new Field[] {
				_.TimeStamp,
				_.Tet,
				_.Eh,
				_.Tt,
				_.Wl,
				_.Tws,
				_.Irr,
				_.Cit1,
				_.Cot1,
				_.Cit2,
				_.Cot2,
				_.Pit1,
				_.Pot1,
				_.Pit2,
				_.Pot2,
				_.Cwt,
				_.Wst,
				_.Rwt,
				_.Foc1,
				_.Foc2,
				_.Poc1,
				_.Poc2,
				_.Cwf,
				_.Wsf,
				_.Rwf,
				_.Allc,
				_.AllMv,
				_.AllEsys,
				_.Tsp,
				_.Tsc,
				_.AllEhp,
				_.Hpp,
				_.Hpc,
				_.AllEc,
				_.Csp,
				_.Csc,
				_.AllQs,
				_.AllQc,
				_.AllQhp,
				_.AllQtc,
				_.AllQsh,
				_.AllQuse,
				_.AllQtanks,
				_.AllQbm,
				_.AllQu,
				_.AllQss,
				_.Allmco2,
				_.Allmso2,
				_.Allmnox,
				_.Allmfc,
				_.fc,
				_.ηc,
				_.COPc,
				_.COPhp,
				_.COPsys,
				_.COPr,
				_.PlcSv,
				_.PlcPv,
				_.PlcOp,
				_.CircleMeterTemp,
				_.ColdMeterTemp,
				_.SupplyMeterTemp,
				_.AllConsum1,
				_.AllConsum2,
				_.AllP1Consum,
				_.AllP2Consum,
				_.AllCircleConsum,
				_.AllSupplyConsum,
				_.GuangZhaoDu};
		}
		/// <summary>
		/// 获取值信息
		/// </summary>
		public override object[] GetValues()
		{
			return new object[] {
				this._TimeStamp,
				this._Tet,
				this._Eh,
				this._Tt,
				this._Wl,
				this._Tws,
				this._Irr,
				this._Cit1,
				this._Cot1,
				this._Cit2,
				this._Cot2,
				this._Pit1,
				this._Pot1,
				this._Pit2,
				this._Pot2,
				this._Cwt,
				this._Wst,
				this._Rwt,
				this._Foc1,
				this._Foc2,
				this._Poc1,
				this._Poc2,
				this._Cwf,
				this._Wsf,
				this._Rwf,
				this._Allc,
				this._AllMv,
				this._AllEsys,
				this._Tsp,
				this._Tsc,
				this._AllEhp,
				this._Hpp,
				this._Hpc,
				this._AllEc,
				this._Csp,
				this._Csc,
				this._AllQs,
				this._AllQc,
				this._AllQhp,
				this._AllQtc,
				this._AllQsh,
				this._AllQuse,
				this._AllQtanks,
				this._AllQbm,
				this._AllQu,
				this._AllQss,
				this._Allmco2,
				this._Allmso2,
				this._Allmnox,
				this._Allmfc,
				this._fc,
				this._ηc,
				this._COPc,
				this._COPhp,
				this._COPsys,
				this._COPr,
				this._PlcSv,
				this._PlcPv,
				this._PlcOp,
				this._CircleMeterTemp,
				this._ColdMeterTemp,
				this._SupplyMeterTemp,
				this._AllConsum1,
				this._AllConsum2,
				this._AllP1Consum,
				this._AllP2Consum,
				this._AllCircleConsum,
				this._AllSupplyConsum,
				this._GuangZhaoDu};
		}
		#endregion

		#region _Field
		/// <summary>
		/// 字段信息
		/// </summary>
		public class _
		{
			/// <summary>
			/// * 
			/// </summary>
			public readonly static Field All = new Field("*","系统采集器Index");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field TimeStamp = new Field("TimeStamp","系统采集器Index","TimeStamp");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field Tet = new Field("Tet","系统采集器Index","Tet");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field Eh = new Field("Eh","系统采集器Index","Eh");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field Tt = new Field("Tt","系统采集器Index","Tt");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field Wl = new Field("Wl","系统采集器Index","Wl");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field Tws = new Field("Tws","系统采集器Index","Tws");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field Irr = new Field("Irr","系统采集器Index","Irr");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field Cit1 = new Field("Cit1","系统采集器Index","Cit1");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field Cot1 = new Field("Cot1","系统采集器Index","Cot1");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field Cit2 = new Field("Cit2","系统采集器Index","Cit2");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field Cot2 = new Field("Cot2","系统采集器Index","Cot2");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field Pit1 = new Field("Pit1","系统采集器Index","Pit1");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field Pot1 = new Field("Pot1","系统采集器Index","Pot1");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field Pit2 = new Field("Pit2","系统采集器Index","Pit2");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field Pot2 = new Field("Pot2","系统采集器Index","Pot2");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field Cwt = new Field("Cwt","系统采集器Index","Cwt");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field Wst = new Field("Wst","系统采集器Index","Wst");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field Rwt = new Field("Rwt","系统采集器Index","Rwt");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field Foc1 = new Field("Foc1","系统采集器Index","Foc1");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field Foc2 = new Field("Foc2","系统采集器Index","Foc2");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field Poc1 = new Field("Poc1","系统采集器Index","Poc1");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field Poc2 = new Field("Poc2","系统采集器Index","Poc2");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field Cwf = new Field("Cwf","系统采集器Index","Cwf");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field Wsf = new Field("Wsf","系统采集器Index","Wsf");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field Rwf = new Field("Rwf","系统采集器Index","Rwf");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field Allc = new Field("Allc","系统采集器Index","Allc");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field AllMv = new Field("AllMv","系统采集器Index","AllMv");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field AllEsys = new Field("AllEsys","系统采集器Index","AllEsys");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field Tsp = new Field("Tsp","系统采集器Index","Tsp");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field Tsc = new Field("Tsc","系统采集器Index","Tsc");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field AllEhp = new Field("AllEhp","系统采集器Index","AllEhp");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field Hpp = new Field("Hpp","系统采集器Index","Hpp");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field Hpc = new Field("Hpc","系统采集器Index","Hpc");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field AllEc = new Field("AllEc","系统采集器Index","AllEc");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field Csp = new Field("Csp","系统采集器Index","Csp");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field Csc = new Field("Csc","系统采集器Index","Csc");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field AllQs = new Field("AllQs","系统采集器Index","AllQs");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field AllQc = new Field("AllQc","系统采集器Index","AllQc");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field AllQhp = new Field("AllQhp","系统采集器Index","AllQhp");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field AllQtc = new Field("AllQtc","系统采集器Index","AllQtc");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field AllQsh = new Field("AllQsh","系统采集器Index","AllQsh");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field AllQuse = new Field("AllQuse","系统采集器Index","AllQuse");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field AllQtanks = new Field("AllQtanks","系统采集器Index","AllQtanks");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field AllQbm = new Field("AllQbm","系统采集器Index","AllQbm");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field AllQu = new Field("AllQu","系统采集器Index","AllQu");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field AllQss = new Field("AllQss","系统采集器Index","AllQss");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field Allmco2 = new Field("Allmco2","系统采集器Index","Allmco2");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field Allmso2 = new Field("Allmso2","系统采集器Index","Allmso2");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field Allmnox = new Field("Allmnox","系统采集器Index","Allmnox");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field Allmfc = new Field("Allmfc","系统采集器Index","Allmfc");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field fc = new Field("fc","系统采集器Index","fc");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field ηc = new Field("ηc","系统采集器Index","ηc");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field COPc = new Field("COPc","系统采集器Index","COPc");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field COPhp = new Field("COPhp","系统采集器Index","COPhp");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field COPsys = new Field("COPsys","系统采集器Index","COPsys");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field COPr = new Field("COPr","系统采集器Index","COPr");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field PlcSv = new Field("PlcSv","系统采集器Index","PlcSv");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field PlcPv = new Field("PlcPv","系统采集器Index","PlcPv");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field PlcOp = new Field("PlcOp","系统采集器Index","PlcOp");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field CircleMeterTemp = new Field("CircleMeterTemp","系统采集器Index","CircleMeterTemp");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field ColdMeterTemp = new Field("ColdMeterTemp","系统采集器Index","ColdMeterTemp");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field SupplyMeterTemp = new Field("SupplyMeterTemp","系统采集器Index","SupplyMeterTemp");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field AllConsum1 = new Field("AllConsum1","系统采集器Index","AllConsum1");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field AllConsum2 = new Field("AllConsum2","系统采集器Index","AllConsum2");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field AllP1Consum = new Field("AllP1Consum","系统采集器Index","AllP1Consum");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field AllP2Consum = new Field("AllP2Consum","系统采集器Index","AllP2Consum");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field AllCircleConsum = new Field("AllCircleConsum","系统采集器Index","AllCircleConsum");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field AllSupplyConsum = new Field("AllSupplyConsum","系统采集器Index","AllSupplyConsum");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field GuangZhaoDu = new Field("GuangZhaoDu","系统采集器Index","GuangZhaoDu");
		}
		#endregion


	}
}

