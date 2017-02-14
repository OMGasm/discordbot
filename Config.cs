using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
namespace Bot
{
    class Config
    {
        public string token;
        public bool bot;
        
        public static async Task<Config> Load(string filename)
        {
            StreamReader file = null;
            try
            {
                file = new StreamReader(filename);
                return JsonConvert.DeserializeObject<Config>(await file.ReadToEndAsync());
            }
            catch(Exception e)
            {
                if (e is FileNotFoundException || e is IOException)
                    return new Config { token = null, bot = false };
                else throw;
            }
            finally
            {
                if (file != null)
                {
                    file.Dispose();
                }
            }
        }

        public async Task<bool> Save(string filename)
        {
            StreamWriter file = null;
            try
            {
                file = new StreamWriter(filename);
                string config = JsonConvert.SerializeObject(this, Formatting.Indented);
                await file.WriteAsync(config);
                await file.FlushAsync();
                return true;
            }
            catch(Exception e)
            {
                if (e is UnauthorizedAccessException || e is IOException || e is System.Security.SecurityException)
                    return false;
                else throw;
            }
            finally
            {
                if (file!=null)
                {
                    file.Dispose();
                }
            }
        }
    }
}
