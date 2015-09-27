using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AegirServer.HTTP.Controller
{
    public class PlaybackController : HTTPController
    {
        //public override void GetAction(string[] args)
        //{
        //    if (this.Request.QueryString.GetValues("query").Length == 0)
        //    {
        //        throw new HTTPException(System.Net.HttpStatusCode.BadRequest);
        //    }
        //    string searchTerm = this.Request.QueryString.GetValues("query").First();
        //    using (var context = new PersistanceContext())
        //    {
        //        ProjectData[] data = context.Projects.Where(x => x.ProjectName.Contains(searchTerm)).ToArray();

        //        this.SetSuccessfulContent(data, System.Net.HttpStatusCode.OK);
        //    }
        //    base.GetAction(args);
        //}
    }
}
