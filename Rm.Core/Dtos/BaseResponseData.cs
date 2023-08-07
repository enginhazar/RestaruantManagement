using System.Collections.Generic;

namespace Rm.Core.Dtos
{
    public class BaseResponseData<T>
    {
        public List<string> Errrors { get; set; }
        public T Data { get; set; }
    }
}
