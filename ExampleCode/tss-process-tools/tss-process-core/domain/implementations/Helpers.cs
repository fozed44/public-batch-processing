using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Configuration;

namespace Tss.Process.Core.Domain.Implementations {

    public static class Helpers {
        #region public

        public static string GetRequiredConfiguration(IConfiguration configuration, string s) {
            var result = configuration[s];

            if(string.IsNullOrEmpty(result))
                throw new ValidationException(
                    $"Missing required configuration item : {s}"
                );

            return result;
        }

        #endregion 
    }
}