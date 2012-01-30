using System;
using System.Collections;
using System.Reflection;

namespace logic
{
	/// <summary>
	/// ReverserClass 的摘要说明。
	/// </summary>
	public class ReverserClass:IComparer
	{
		Type type = null; 
		string name = string.Empty; 
		string direction = "ASC"; 
		public ReverserClass(Type type, string name, string direction) 
		{ 
			this.type = type; 
			this.name = name; 
			if(direction.Equals("DESC")) 
				this.direction = "DESC"; 
		} 
 
		int IComparer.Compare( object x, object y )  
		{ 
			object x1 = this.type.InvokeMember(this.name, BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty, null, x, null); 
			object y1 = this.type.InvokeMember(this.name, BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty, null, y, null); 
			if(direction.Equals("DESC")) 
				Swap(ref x1, ref y1); 
			return( (new CaseInsensitiveComparer()).Compare( x1, y1 )); 
		} 
 
		void Swap(ref object x, ref object y ) 
		{ 
			object temp = null; 
			temp = x; 
			x = y; 
			y = temp; 
		} 
	}
}
