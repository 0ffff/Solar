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
	/// 实体类log 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Table("log")]
	[Serializable]
	public partial class log : Entity 
	{
		#region Model
		private int _ID;
		private string _time;
		private string _value;
		/// <summary>
		/// 
		/// </summary>
		public int ID
		{
			get{ return _ID; }
			set
			{
				this.OnPropertyValueChange(_.ID,_ID,value);
				this._ID=value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public string time
		{
			get{ return _time; }
			set
			{
				this.OnPropertyValueChange(_.time,_time,value);
				this._time=value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public string value
		{
			get{ return _value; }
			set
			{
				this.OnPropertyValueChange(_.value,_value,value);
				this._value=value;
			}
		}
		#endregion

		#region Method
		/// <summary>
		/// 获取实体中的标识列
		/// </summary>
		public override Field GetIdentityField()
		{
			return _.ID;
		}
		/// <summary>
		/// 获取列信息
		/// </summary>
		public override Field[] GetFields()
		{
			return new Field[] {
				_.ID,
				_.time,
				_.value};
		}
		/// <summary>
		/// 获取值信息
		/// </summary>
		public override object[] GetValues()
		{
			return new object[] {
				this._ID,
				this._time,
				this._value};
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
			public readonly static Field All = new Field("*","log");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field ID = new Field("ID","log","ID");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field time = new Field("time","log","time");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field value = new Field("value","log","value");
		}
		#endregion


	}
}

