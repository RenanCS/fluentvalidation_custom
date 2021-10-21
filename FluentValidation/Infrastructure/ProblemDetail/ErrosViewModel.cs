using System.Collections.Generic;
using Validation.Infrastructure.Domain;

namespace Validation.Infrastructure.ProblemDetail
{
    public class ErrosViewModel
    {
        public ErrosViewModel(List<CustomException> erros)
        {
            Erros = erros;
        }

        public List<CustomException> Erros { get; set; }
    }
}
