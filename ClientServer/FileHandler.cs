using System.IO;
using Newtonsoft.Json;

namespace ClientServer
{
    public class FileHandler
    {
        public string ReadGameDetailsFromJSON(string getGameDetailsFilePath)
        {
            try
            {
                using (StreamReader streamReader = new StreamReader(getGameDetailsFilePath))
                {
                    return streamReader.ReadToEnd();
                }
            }
            catch (FileNotFoundException fileNotFoundException)
            {
                throw fileNotFoundException;
            }
        }

        public string ReadCreatedTeamsDetailsToJSON(string getCreatedTeamsFilePath)
        {
            try
            {
                using (StreamReader streamReader = new StreamReader(getCreatedTeamsFilePath))
                {
                    return streamReader.ReadToEnd();
                }
            }
            catch(FileNotFoundException fileNotFoundException)
            {
                throw fileNotFoundException;
            }
        }
    }
}
