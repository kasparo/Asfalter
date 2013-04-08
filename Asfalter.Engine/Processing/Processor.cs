using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Configuration;
using System.IO;
using System.Xml.Serialization;
using Asfalter.Common.Dao;
using Asfalter.Engine.Data;

namespace Asfalter.Engine.Processing
{
    internal class Processor
    {
        private string DataPath
        {
            get { return ConfigurationManager.AppSettings["DataDirectory"]; }
        }

        private DataProvider _dataProvider;
        private DataProvider DataProvider
        {
            get
            {
                lock (this)
                {
                    return _dataProvider;
                }
            }
        }

        private bool _isStopped;

        public void Start()
        {
            _isStopped = false;

            _dataProvider = new DataProvider();

            string[] directories = Directory.GetDirectories(DataPath);
            foreach (string directory in directories)
            {
                DirectoryInfo dirInfo = new DirectoryInfo(directory);

                Thread thread = new Thread(new ParameterizedThreadStart(ProcessDirectory));
                thread.Start(dirInfo.Name);
            }
        }

        private void ProcessDirectory(object id)
        {
            unit ownerUnit = DataProvider.InsertUnit(new Guid(id.ToString()));
            do
            {
                string[] files = Directory.GetFiles(Path.Combine(DataPath, id.ToString()));
                foreach (string file in files)
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(List<UnitRecord>));
                    using (StreamReader reader = new StreamReader(file))
                    {
                        List<UnitRecord> records = (List<UnitRecord>)serializer.Deserialize(reader);

                        foreach (UnitRecord record in records)
                            DataProvider.InsertUnitRecord(record, ownerUnit);

                        reader.Close();
                    }
                    File.Delete(file);
                }

                Thread.Sleep(1000);
            } while (!_isStopped);
        }

        public void Stop()
        {
            _isStopped = true;
        }
    }
}