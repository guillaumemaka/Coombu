using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coombu.Models.ViewModels
{
    public class ApiResponse<T>
    {
        public IDictionary<string, object> Meta { get; private set; }        
        public T Data { get; private set; }

        public ApiResponse() { }
        public ApiResponse(T data) 
        {
            Meta = new Dictionary<string, object>();
            Data = data;
        }

        public ApiResponse(IDictionary<string,object> meta, T data)
        {
            Meta = meta;
            Data = data;
        }

        public void AddMetatData(string key, object metadata)
        {
            if (Meta.ContainsKey(key))
            {
                Meta[key] = metadata;
            }
            else
            {
                Meta.Add(key, metadata);
            }
        }
    }
}
