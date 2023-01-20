using EmployeeManagement.Application.Common.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using System.Text;

namespace EmployeeManagement.Application.Common.Helpers
{
    public sealed class CsvOutputFormatter : TextOutputFormatter
    {
        public CsvOutputFormatter() 
        { 
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/csv")); 
            SupportedEncodings.Add(Encoding.UTF8); 
            SupportedEncodings.Add(Encoding.Unicode); 
        }

        protected override bool CanWriteType(Type? type)
        {
            if (typeof(EmployeeDto).IsAssignableFrom(type) || typeof(IEnumerable<EmployeeDto>).IsAssignableFrom(type)) 
            { 
                return base.CanWriteType(type); 
            }

            return false;
        }

        public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            var response = context.HttpContext.Response;
            var buffer = new StringBuilder(); 
            
            if (context.Object is IEnumerable<EmployeeDto>) 
            { 
                foreach (var company in (IEnumerable<EmployeeDto>)context.Object) 
                { 
                    FormatCsv(buffer, company);
                } 
            } 
            else 
            { 
                FormatCsv(buffer, (EmployeeDto)context.Object); 
            }
            
            await response.WriteAsync(buffer.ToString());
        }

        private static void FormatCsv(StringBuilder buffer, EmployeeDto employee) 
        { 
            buffer.AppendLine($"{employee.Id},\"{employee.FirstName}\",\"{employee.LastName}\",\"{employee.Title}\",\"{employee.Email}\",\"{employee.Address}\""); 
        }
    }
}
