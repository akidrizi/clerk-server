using System;

namespace ClerkServer.RandomUserAPI.Models {

	public class Timezone {

		public DateTimeOffset Offset { get; set; }
		public string Description { get; set; }

	}

}