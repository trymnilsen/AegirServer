using AegirServer.IO;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AegirServer.Project
{
    public class SimulationProject
    {
        private SimulationProjectFile projectFile { get; private set; }
        public EFileAvailability AvailabilityStatus { get; private set; }
        public SimulationProject()
        {
            AvailabilityStatus = EFileAvailability.NOTSAVED;
        }
        public SimulationProject(string path)
        {
            try
            {
                string fileContent = File.ReadAllText(path);
                //TODO: for now escape
                fileContent = fileContent.Replace(@"\", @"\\");
                SimulationProjectFile project = JsonConvert.DeserializeObject<SimulationProjectFile>(fileContent);
                this.projectFile = project;
                this.AvailabilityStatus = EFileAvailability.AVAILABLE;
            }
            catch(Exception e)
            {
                if(e is UnauthorizedAccessException)
                {
                    this.AvailabilityStatus = EFileAvailability.ACCESSERROR;
                }
                else if(e is FileNotFoundException)
                {
                    this.AvailabilityStatus = EFileAvailability.NOTFOUND;
                }
                else if(e is IOException)
                {
                    this.AvailabilityStatus = EFileAvailability.IOERROR;
                }
                else
                {
                    this.AvailabilityStatus = EFileAvailability.GENERICERROR;
                }
                //TODO Log exception
            }
        }
    }
}
