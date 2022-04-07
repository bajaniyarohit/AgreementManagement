using System;
using System.Collections.Generic;
using System.Text;

namespace Services.ViewModels
{
	public class JDataTableResponse<T>
	{
		public string sEcho { get; set; }
		public int iTotalRecords { get; set; }
		public int iTotalDisplayRecords { get; set; }
		public IList<T> aaData { get; set; }
	}
}
