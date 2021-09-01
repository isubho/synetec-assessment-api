using System;
using System.Collections.Generic;
using System.Text;

namespace SynetecAssessmentApi.Domain.Dtos
{
    public class Response<T>
    {
        public T Value { get; }
        public bool IsSuccees { get; }
        public string ErrorMessage { get; }

        protected internal Response(T value, bool isSuccees, string errorMessage)
        {
            Value = value;
            IsSuccees = isSuccees;
            ErrorMessage = errorMessage;
        }
        public static Response<T> Ok(T value)
        {
            return new Response<T>(value, true, null);
        }
        public static Response<T> Fail(string errorMessage)
        {
            return new Response<T>(default, false, errorMessage);
        }
    }
}
