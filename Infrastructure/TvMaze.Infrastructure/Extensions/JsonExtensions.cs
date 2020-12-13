using Newtonsoft.Json;
using System;
using TvMaze.Core.DTO.Base;
using TvMaze.Core.Extensions;

namespace TvMaze.Infrastructure.Extensions
{
    public static class JsonExtensions
    {
        public static BaseDTO<T> TryDeserialize<T>(this string json)
        {
            var result = new BaseDTO<T>();

            try
            {
                result.Data = JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception ex)
            {
                result.AddError($"Failed to deserialize json to type of {typeof(T)} - {ex.Message()}");
            }

            return result;
        }
    }
}
