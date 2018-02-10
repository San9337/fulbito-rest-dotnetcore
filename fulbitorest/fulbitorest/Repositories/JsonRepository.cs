using datalayer.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace fulbitorest.Repositories
{
    public class JsonRepository<T> : IRepository<T> where T : class
    {
        static readonly string FileName = typeof(T).Name;

        private List<T> memoryList = new List<T>();

        public JsonRepository()
        {
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
