using System.Collections.Generic;

namespace ClerkServer.RandomUserAPI.Models {

	public class Catalog {

		public IEnumerable<User> Results { get; set; }
		public Info Info { get; set; }

	}

}