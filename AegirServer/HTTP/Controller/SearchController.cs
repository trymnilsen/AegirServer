using AegirDataTypes.Workspace;
using AegirSearch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AegirServer.HTTP.Controller
{
    class SearchController: HTTPController
    {
        public override void GetAction(string[] args)
        {
            if(this.Request.QueryString.GetValues("query").Length==0)
            {
                throw new HTTPException(System.Net.HttpStatusCode.BadRequest);
            }
            string searchTerm = this.Request.QueryString.GetValues("query").First();
            ProjectData[] hits = this.ServerContext.search.Search(searchTerm);
            this.SetSuccessfulContent(hits, System.Net.HttpStatusCode.OK);
            base.GetAction(args);
        }
    }
}
