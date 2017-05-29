using System;
using System.Collections.Generic;

namespace test
{
	public class RestruantModel
	{
		public RestruantInfo restaurant { get; set; }

		public RestruantModel(RestruantInfo info)
		{
			this.restaurant = info;
		}
	}

	public class RestruantInfo
	{
		public string id { get; set; }
		public string name { get; set; }
		public string thumb { get; set; }
		public string cuisines { get; set; }
		public string average_cost_for_two { get; set; }
		public string address { get; set; }
	}

	public class RestruantsResponse
	{
		public RestruantsResponse()
		{
		}
		public List<RestruantModel> restaurants { get; set; }
	}
}
