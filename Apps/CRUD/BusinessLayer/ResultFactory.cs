using Blogging.Shared;
using BusinessLayer.Abstractionns;
using Datalayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLayer
{

    public static class ResultFactory
    {


        public static MethodResult<T> AsMethodResult<T>(BlogContext ctx, T data) where T: class, new()
        {
            return new MethodResult<T>
            {
                Data = data
            };
        }

        public static MethodResult<T> MethodErrorResult<T>(string error) where T: class, new()
        {
            return new MethodResult<T>
            {
                Errors = new List<string> { error }
            };
        }

    }
}
