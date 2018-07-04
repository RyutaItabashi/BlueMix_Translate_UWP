using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Windows.Storage;

namespace BlueMixTranslator.Assets
{
    class CSV_Writer
    {
        public void csvWrite(string ja, string en, string fileName)
        {
            StorageFolder folder = KnownFolders.PicturesLibrary;
            string picpath = folder.Path;
            if (!Directory.Exists(Directory.GetCurrentDirectory() + @"\CSV\")) Directory.CreateDirectory(Directory.GetCurrentDirectory() + @"\CSV\");
            FileStream fs = new FileStream(Directory.GetCurrentDirectory()+ @"\CSV\" + fileName + ".csv", FileMode.Create, FileAccess.Write);
            using(StreamWriter writer = new StreamWriter(fs))
            {
                writer.Write(ja);
                writer.Write(",");
                writer.WriteLine(en);
            }
        }
    }
}
