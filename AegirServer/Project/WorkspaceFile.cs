using AegirServer.IO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AegirServer.Project
{
    public class WorkspaceFile
    {
        public List<string> RecentProjectsPaths { get; set; }
        public WorkspaceFile()
        {

        }
        public static WorkspaceFile LoadWorkspace(string path)
        {
            string fileContent = FileReader.ReadAllWithoutControlCharacters(path);
            //TODO: for now escape
            fileContent = fileContent.Replace(@"\", @"\\");
            WorkspaceFile workspace = JsonConvert.DeserializeObject<WorkspaceFile>(fileContent);
            return workspace;
        }
    }
}
