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
	/// 实体类ProjectDetailInfo 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Table("ProjectDetailInfo")]
	[Serializable]
	public partial class ProjectDetailInfo : Entity 
	{
		#region Model
		private string _ProjectCode;
		private string _AreaName;
		private string _ProjectName;
		private string _ApplicationCompany;
		private string _TecType;
		private string _TotalArea;
		private string _Tec1;
		private string _Num1;
		private string _Tec2;
		private string _Num2;
		private string _Tec3;
		private string _Num3;
		private string _BuildingType;
		private string _HeatingArea1;
		private string _AirconditionalArea1;
		private string _BuildingStorey1;
		private string _FinTime1;
		private string _BuildingName;
		private string _BuildingArea;
		private string _HeatingArea2;
		private string _AirconditionalArea2;
		private string _BuildingStorey2;
		private string _FinTime2;
		private string _TecNum;
		private string _XAxis;
		private string _YAxis;
		private string _ProjectAddress;
		private string _ProjectDescription;
		/// <summary>
		/// 
		/// </summary>
		public string ProjectCode
		{
			get{ return _ProjectCode; }
			set
			{
				this.OnPropertyValueChange(_.ProjectCode,_ProjectCode,value);
				this._ProjectCode=value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public string AreaName
		{
			get{ return _AreaName; }
			set
			{
				this.OnPropertyValueChange(_.AreaName,_AreaName,value);
				this._AreaName=value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ProjectName
		{
			get{ return _ProjectName; }
			set
			{
				this.OnPropertyValueChange(_.ProjectName,_ProjectName,value);
				this._ProjectName=value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ApplicationCompany
		{
			get{ return _ApplicationCompany; }
			set
			{
				this.OnPropertyValueChange(_.ApplicationCompany,_ApplicationCompany,value);
				this._ApplicationCompany=value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public string TecType
		{
			get{ return _TecType; }
			set
			{
				this.OnPropertyValueChange(_.TecType,_TecType,value);
				this._TecType=value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public string TotalArea
		{
			get{ return _TotalArea; }
			set
			{
				this.OnPropertyValueChange(_.TotalArea,_TotalArea,value);
				this._TotalArea=value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Tec1
		{
			get{ return _Tec1; }
			set
			{
				this.OnPropertyValueChange(_.Tec1,_Tec1,value);
				this._Tec1=value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Num1
		{
			get{ return _Num1; }
			set
			{
				this.OnPropertyValueChange(_.Num1,_Num1,value);
				this._Num1=value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Tec2
		{
			get{ return _Tec2; }
			set
			{
				this.OnPropertyValueChange(_.Tec2,_Tec2,value);
				this._Tec2=value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Num2
		{
			get{ return _Num2; }
			set
			{
				this.OnPropertyValueChange(_.Num2,_Num2,value);
				this._Num2=value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Tec3
		{
			get{ return _Tec3; }
			set
			{
				this.OnPropertyValueChange(_.Tec3,_Tec3,value);
				this._Tec3=value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Num3
		{
			get{ return _Num3; }
			set
			{
				this.OnPropertyValueChange(_.Num3,_Num3,value);
				this._Num3=value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BuildingType
		{
			get{ return _BuildingType; }
			set
			{
				this.OnPropertyValueChange(_.BuildingType,_BuildingType,value);
				this._BuildingType=value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public string HeatingArea1
		{
			get{ return _HeatingArea1; }
			set
			{
				this.OnPropertyValueChange(_.HeatingArea1,_HeatingArea1,value);
				this._HeatingArea1=value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public string AirconditionalArea1
		{
			get{ return _AirconditionalArea1; }
			set
			{
				this.OnPropertyValueChange(_.AirconditionalArea1,_AirconditionalArea1,value);
				this._AirconditionalArea1=value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BuildingStorey1
		{
			get{ return _BuildingStorey1; }
			set
			{
				this.OnPropertyValueChange(_.BuildingStorey1,_BuildingStorey1,value);
				this._BuildingStorey1=value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public string FinTime1
		{
			get{ return _FinTime1; }
			set
			{
				this.OnPropertyValueChange(_.FinTime1,_FinTime1,value);
				this._FinTime1=value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BuildingName
		{
			get{ return _BuildingName; }
			set
			{
				this.OnPropertyValueChange(_.BuildingName,_BuildingName,value);
				this._BuildingName=value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BuildingArea
		{
			get{ return _BuildingArea; }
			set
			{
				this.OnPropertyValueChange(_.BuildingArea,_BuildingArea,value);
				this._BuildingArea=value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public string HeatingArea2
		{
			get{ return _HeatingArea2; }
			set
			{
				this.OnPropertyValueChange(_.HeatingArea2,_HeatingArea2,value);
				this._HeatingArea2=value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public string AirconditionalArea2
		{
			get{ return _AirconditionalArea2; }
			set
			{
				this.OnPropertyValueChange(_.AirconditionalArea2,_AirconditionalArea2,value);
				this._AirconditionalArea2=value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BuildingStorey2
		{
			get{ return _BuildingStorey2; }
			set
			{
				this.OnPropertyValueChange(_.BuildingStorey2,_BuildingStorey2,value);
				this._BuildingStorey2=value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public string FinTime2
		{
			get{ return _FinTime2; }
			set
			{
				this.OnPropertyValueChange(_.FinTime2,_FinTime2,value);
				this._FinTime2=value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public string TecNum
		{
			get{ return _TecNum; }
			set
			{
				this.OnPropertyValueChange(_.TecNum,_TecNum,value);
				this._TecNum=value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public string XAxis
		{
			get{ return _XAxis; }
			set
			{
				this.OnPropertyValueChange(_.XAxis,_XAxis,value);
				this._XAxis=value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public string YAxis
		{
			get{ return _YAxis; }
			set
			{
				this.OnPropertyValueChange(_.YAxis,_YAxis,value);
				this._YAxis=value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ProjectAddress
		{
			get{ return _ProjectAddress; }
			set
			{
				this.OnPropertyValueChange(_.ProjectAddress,_ProjectAddress,value);
				this._ProjectAddress=value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ProjectDescription
		{
			get{ return _ProjectDescription; }
			set
			{
				this.OnPropertyValueChange(_.ProjectDescription,_ProjectDescription,value);
				this._ProjectDescription=value;
			}
		}
		#endregion

		#region Method
		/// <summary>
		/// 获取实体中的主键列
		/// </summary>
		public override Field[] GetPrimaryKeyFields()
		{
			return new Field[] {
				_.ProjectName};
		}
		/// <summary>
		/// 获取列信息
		/// </summary>
		public override Field[] GetFields()
		{
			return new Field[] {
				_.ProjectCode,
				_.AreaName,
				_.ProjectName,
				_.ApplicationCompany,
				_.TecType,
				_.TotalArea,
				_.Tec1,
				_.Num1,
				_.Tec2,
				_.Num2,
				_.Tec3,
				_.Num3,
				_.BuildingType,
				_.HeatingArea1,
				_.AirconditionalArea1,
				_.BuildingStorey1,
				_.FinTime1,
				_.BuildingName,
				_.BuildingArea,
				_.HeatingArea2,
				_.AirconditionalArea2,
				_.BuildingStorey2,
				_.FinTime2,
				_.TecNum,
				_.XAxis,
				_.YAxis,
				_.ProjectAddress,
				_.ProjectDescription};
		}
		/// <summary>
		/// 获取值信息
		/// </summary>
		public override object[] GetValues()
		{
			return new object[] {
				this._ProjectCode,
				this._AreaName,
				this._ProjectName,
				this._ApplicationCompany,
				this._TecType,
				this._TotalArea,
				this._Tec1,
				this._Num1,
				this._Tec2,
				this._Num2,
				this._Tec3,
				this._Num3,
				this._BuildingType,
				this._HeatingArea1,
				this._AirconditionalArea1,
				this._BuildingStorey1,
				this._FinTime1,
				this._BuildingName,
				this._BuildingArea,
				this._HeatingArea2,
				this._AirconditionalArea2,
				this._BuildingStorey2,
				this._FinTime2,
				this._TecNum,
				this._XAxis,
				this._YAxis,
				this._ProjectAddress,
				this._ProjectDescription};
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
			public readonly static Field All = new Field("*","ProjectDetailInfo");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field ProjectCode = new Field("ProjectCode","ProjectDetailInfo","ProjectCode");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field AreaName = new Field("AreaName","ProjectDetailInfo","AreaName");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field ProjectName = new Field("ProjectName","ProjectDetailInfo","ProjectName");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field ApplicationCompany = new Field("ApplicationCompany","ProjectDetailInfo","ApplicationCompany");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field TecType = new Field("TecType","ProjectDetailInfo","TecType");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field TotalArea = new Field("TotalArea","ProjectDetailInfo","TotalArea");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field Tec1 = new Field("Tec1","ProjectDetailInfo","Tec1");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field Num1 = new Field("Num1","ProjectDetailInfo","Num1");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field Tec2 = new Field("Tec2","ProjectDetailInfo","Tec2");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field Num2 = new Field("Num2","ProjectDetailInfo","Num2");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field Tec3 = new Field("Tec3","ProjectDetailInfo","Tec3");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field Num3 = new Field("Num3","ProjectDetailInfo","Num3");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field BuildingType = new Field("BuildingType","ProjectDetailInfo","BuildingType");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field HeatingArea1 = new Field("HeatingArea1","ProjectDetailInfo","HeatingArea1");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field AirconditionalArea1 = new Field("AirconditionalArea1","ProjectDetailInfo","AirconditionalArea1");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field BuildingStorey1 = new Field("BuildingStorey1","ProjectDetailInfo","BuildingStorey1");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field FinTime1 = new Field("FinTime1","ProjectDetailInfo","FinTime1");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field BuildingName = new Field("BuildingName","ProjectDetailInfo","BuildingName");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field BuildingArea = new Field("BuildingArea","ProjectDetailInfo","BuildingArea");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field HeatingArea2 = new Field("HeatingArea2","ProjectDetailInfo","HeatingArea2");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field AirconditionalArea2 = new Field("AirconditionalArea2","ProjectDetailInfo","AirconditionalArea2");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field BuildingStorey2 = new Field("BuildingStorey2","ProjectDetailInfo","BuildingStorey2");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field FinTime2 = new Field("FinTime2","ProjectDetailInfo","FinTime2");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field TecNum = new Field("TecNum","ProjectDetailInfo","TecNum");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field XAxis = new Field("XAxis","ProjectDetailInfo","XAxis");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field YAxis = new Field("YAxis","ProjectDetailInfo","YAxis");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field ProjectAddress = new Field("ProjectAddress","ProjectDetailInfo","ProjectAddress");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field ProjectDescription = new Field("ProjectDescription","ProjectDetailInfo","ProjectDescription");
		}
		#endregion


	}
}

