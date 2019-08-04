using System.Collections.Generic;

namespace Customer.Register.Application.Models
{
    public class PaginatedResult<T>
    {
        public IEnumerable<T> Result { get; set; }
        public int Total { get; set; }
    }
}
