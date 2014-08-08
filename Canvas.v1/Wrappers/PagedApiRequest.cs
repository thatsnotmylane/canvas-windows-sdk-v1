using System;
using Canvas.v1.Extensions;

namespace Canvas.v1.Wrappers
{
    public class PagedApiRequest : ApiRequest
    {
        public PagedApiRequest(Uri hostUri, int page, int itemsPerPage) : base(hostUri)
        {
            SetPagingParameters(page, itemsPerPage);
        }

        public PagedApiRequest(Uri hostUri, string path, int page, int itemsPerPage)
            : base(hostUri, path)
        {
            SetPagingParameters(page, itemsPerPage);
        }

        private void SetPagingParameters(int page, int itemsPerPage)
        {
            page.ThrowIfUnassigned("page");
            itemsPerPage.ThrowIfUnassigned("itemsPerPage");

            this.Param("page", page.ToString());
            this.Param("per_page", itemsPerPage.ToString());
        }
    }
}