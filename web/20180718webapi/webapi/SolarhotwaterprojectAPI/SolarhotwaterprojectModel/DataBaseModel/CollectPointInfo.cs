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
	/// 实体类CollectPointInfo 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Table("CollectPointInfo")]
	[Serializable]
	public partial class CollectPointInfo : Entity 
	{
		#region Model
		private string _AreaName;
		private string _ProjectName;
		private string _CollectorName;
		private string _CollectPointName;
		private int? _CollectPointCode;
		private string _CollectDataName;
		private string _CollectDataCode;
		private string _MaxValue;
		private string _MinValue;
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
		public string CollectorName
		{
			get{ return _CollectorName; }
			set
			{
				this.OnPropertyValueChange(_.CollectorName,_CollectorName,value);
				this._CollectorName=value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CollectPointName
		{
			get{ return _CollectPointName; }
			set
			{
				this.OnPropertyValueChange(_.CollectPointName,_CollectPointName,value);
				this._CollectPointName=value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? CollectPointCode
		{
			get{ return _CollectPointCode; }
			set
			{
				this.OnPropertyValueChange(_.CollectPointCode,_CollectPointCode,value);
				this._CollectPointCode=value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CollectDataName
		{
			get{ return _CollectDataName; }
			set
			{
				this.OnPropertyValueChange(_.CollectDataName,_CollectDataName,value);
				this._CollectDataName=value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CollectDataCode
		{
			get{ return _CollectDataCode; }
			set
			{
				this.OnPropertyValueChange(_.CollectDataCode,_CollectDataCode,value);
				this._CollectDataCode=value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public string MaxValue
		{
			get{ return _MaxValue; }
			set
			{
				this.OnPropertyValueChange(_.MaxValue,_MaxValue,value);
				this._MaxValue=value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public string MinValue
		{
			get{ return _MinValue; }
			set
			{
				this.OnPropertyValueChange(_.MinValue,_MinValue,value);
				this._MinValue=value;
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
				_.AreaName,
				_.ProjectName,
				_.CollectorName,
				_.CollectPointName,
				_.CollectPointCode,
				_.CollectDataName,
				_.CollectDataCode,
				_.MaxValue,
				_.MinValue};
		}
		/// <summary>
		/// 获取值信息
		/// </summary>
		public override object[] GetValues()
		{
			return new object[] {
				this._AreaName,
				this._ProjectName,
				this._CollectorName,
				this._CollectPointName,
				this._CollectPointCode,
				this._CollectDataName,
				this._CollectDataCode,
				this._MaxValue,
				this._MinValue};
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
			public readonly static Field All = new Field("*","CollectPointInfo");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field AreaName = new Field("AreaName","CollectPointInfo","AreaName");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field ProjectName = new Field("ProjectName","CollectPointInfo","ProjectName");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field CollectorName = new Field("CollectorName","CollectPointInfo","CollectorName");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field CollectPointName = new Field("CollectPointName","CollectPointInfo","CollectPointName");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field CollectPointCode = new Field("CollectPointCode","CollectPointInfo","CollectPointCode");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field CollectDataName = new Field("CollectDataName","CollectPointInfo","CollectDataName");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field CollectDataCode = new Field("CollectDataCode","CollectPointInfo","CollectDataCode");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field MaxValue = new Field("MaxValue","CollectPointInfo","MaxValue");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field MinValue = new Field("MinValue","CollectPointInfo","MinValue");
		}
		#endregion


	}
}

