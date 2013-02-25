using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using Asfalter.Common.Dao;
using System.Xml.Serialization;
using System.IO;
using System.Threading;

namespace Asfalter.UnitSimulator
{
    class Unit
    {
        /// <summary>
        /// Unit Id
        /// </summary>
        public Guid ID { get; private set; }

        /// <summary>
        /// Directory for data storing
        /// </summary>
        private string StorageDirectory
        {
            get
            {
                return ConfigurationManager.AppSettings["DataDirectory"];
            }
        }

        private bool IsStopped { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public Unit()
        {
            ID = Guid.NewGuid();
            IsStopped = false;
        }

        public void Start()
        {
            Random random = new Random();
            List<UnitRecord> list = new List<UnitRecord>();

            DateTime tempTime = DateTime.Now;
            do
            {
                UnitRecord unit = new UnitRecord()
                {
                    CurrnetTime = DateTime.Now,
                    Speed = random.Next(80, 190),
                    Weight = random.Next(1200, 2000)
                };
                list.Add(unit);

                Thread.Sleep(1500);

                if (DateTime.Now.AddMinutes(-1) > tempTime)
                {
                    SaveRecords(list);

                    tempTime = DateTime.Now;
                    list = new List<UnitRecord>();
                }
            } while (!IsStopped);

            SaveRecords(list);
        }

        private void SaveRecords(List<UnitRecord> records)
        {
            string path = Path.Combine(StorageDirectory, ID.ToString());
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            
            XmlSerializer serializer = new XmlSerializer(typeof(List<UnitRecord>));
            using (StreamWriter writer = new StreamWriter(Path.Combine(path, 
                String.Format( "record_{0}.xml", DateTime.Now.ToString("ddMMyyyyhhmmss")))))
            {
                serializer.Serialize(writer, records);
                writer.Close();
            }
        }

        public void Stop()
        {
            IsStopped = true;
        }
    }
}
