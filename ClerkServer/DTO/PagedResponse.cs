namespace ClerkServer.DTO {

	public class PagedResponse<T> {

		public PagedList<T> Results { get; set; }
		public PaginationInfo Info { get; set; }

		public PagedResponse(PagedList<T> results) {
			Results = results;
			Info = new PaginationInfo {
				TotalCount = results.TotalCount,
				PageSize = results.PageSize,
				CurrentPage = results.CurrentPage,
				TotalPages = results.TotalPages,
				HasNext = results.HasNext,
				HasPrevious = results.HasPrevious,
			};
		}

	}

}