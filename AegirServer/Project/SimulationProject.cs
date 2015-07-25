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
        private SimulationProjectFile projectFile;
        public EFileAvailability AvailabilityStatus { get; private set; }
        public SimulationProject()
        {
            AvailabilityStatus = EFileAvailability.NOTSAVED;
        }
        public void Load(string path)
        {
            try
            {
                if(!File.Exists(path))
                {
                    this.AvailabilityStatus = EFileAvailability.NOTFOUND;
                    return;
                }
                string fileContent = File.ReadAllText(path);
                //TODO: for now escape
                fileContent = fileContent.Replace(@"\", @"\\");
                SimulationProjectFile project = JsonConvert.DeserializeObject<SimulationProjectFile>(fileContent);
                this.projectFile = project;
                this.AvailabilityStatus = EFileAvailability.AVAILABLE;
            }
            catch (Exception e)
            {
                if (e is UnauthorizedAccessException)
                {
                    this.AvailabilityStatus = EFileAvailability.ACCESSERROR;
                }
                else if (e is IOException)
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
