using System;
namespace Halsign.DWM.Domain
{
	public class WlbActionType
	{
		private int _type;
		private string _name;
		private string _description;
		private string _assembly;
		private string _class;
		public int Type
		{
			get
			{
				return this._type;
			}
			set
			{
				this._type = value;
			}
		}
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				this._name = value;
			}
		}
		public string Description
		{
			get
			{
				return this._description;
			}
			set
			{
				this._description = value;
			}
		}
		public string Assembly
		{
			get
			{
				return this._assembly;
			}
			set
			{
				this._assembly = value;
			}
		}
		public string Class
		{
			get
			{
				return this._class;
			}
			set
			{
				this._class = value;
			}
		}
		public override string ToString()
		{
			return this.Name;
		}
	}
}
