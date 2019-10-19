using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCore3._0Base.Data.SeedData
{
    public static class EntitySeedData
    {

        public async static void Seed(ModelBuilder modelBuilder)
        {
            //add here your seed database informations
            DateTime createdOn = DateTime.UtcNow;
            DateTime UpdatedOn = DateTime.UtcNow;
            string createdBy = "administrator";
            string updatedBy = "administrator";

            //modelBuilder.Entity<IntegrationProcess>().HasData(
            //   new IntegrationProcess
            //   {
            //       Id = 1,
            //       DataLeitura = DateTime.UtcNow.AddHours(-1),
            //       DataProgramadaProximaLeitura = DateTime.UtcNow.AddMinutes(20),
            //       Mensagem = "Finished succesfully",
            //       StatusLeitura = "1",
            //       UltimaEntidadeLida = "Seed Information"
            //   });
          
        }
    }
}
