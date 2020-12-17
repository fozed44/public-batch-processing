using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tss.Process.Data.Context;
using Tss.Process.Data.Entities;

namespace Tss.Process.Tests {
        
    
    [TestClass]
    public class When_using_ProcessContext {

        [TestMethod]
        public void Database_is_created() {
            using (var context = new ProcessContext("Server=localhost;Database=Process;user id=sa;Password=Password@123")) {
                context.ProcessMetadata.Add(new ProcessMetadata {
                    AllowMultiple         = true,
                    AllowMultiplePerCycle = false,
                    Name                  = "example-name",
                    Description           = "Example Description"
                });
                context.SaveChanges();
            }
        }
    }
}