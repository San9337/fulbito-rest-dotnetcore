﻿using datalayer.Contracts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace datalayer.JsonRepository
{
    public class JsonRepository<T> : IRepository<T> where T : class
    {
        static readonly string FolderName = "fake-database-jsons";
        static readonly string FileName = FolderName+"\\"+typeof(T).Name;

        private List<T> memoryList = new List<T>();

        public JsonRepository()
        {
            if (!Directory.Exists(FolderName))
            {
                Directory.CreateDirectory(FolderName);
            }
            if(!File.Exists(FileName))
            {
                File.OpenWrite(FileName).Close();
            }
        }

        public void Add(T newEntity)
        {
            memoryList.Add(newEntity);

            var json = JsonConvert.SerializeObject(memoryList);
            using (var writer = new StreamWriter(FileName))
            {
                writer.Write(json);
            }
        }

        public IEnumerable<T> All()
        {
            using(var reader = new StreamReader(FileName))
            {
                memoryList = JsonConvert.DeserializeObject<List<T>>(reader.ReadToEnd()) ?? new List<T>();
            }
            return memoryList;
        }
    }
}
