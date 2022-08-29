using Frontend.Business.ClasesDeValoracion.Core;
using Frontend.Business.ClasesDeValoracion.Searchers;
using Frontend.Business.Commons;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Frontend.Business.ClasesDeValoracion.Synchronizers
{
    public class ClaseDeValoracionSynchronizer
    {
        //private readonly ClaseDeValoracionSearcher claseDeValoracionSearcher;
        //private readonly ClaseDeValoracionGenerator claseDeValoracionGenerator;
        //private readonly ClaseDeValoracionUpdater claseDeValoracionUpdater;

        //public ClaseDeValoracionSynchronizer(ClaseDeValoracionSearcher claseDeValoracionSearcher, ClaseDeValoracionGenerator claseDeValoracionGenerator,
        //    ClaseDeValoracionUpdater claseDeValoracionUpdater)
        //{
        //    this.claseDeValoracionSearcher = claseDeValoracionSearcher;
        //    this.claseDeValoracionGenerator = claseDeValoracionGenerator;
        //    this.claseDeValoracionUpdater = claseDeValoracionUpdater;
        //}

        //public async Task Sync()
        //{
        //    try
        //    {
        //        var json = "[{'Codigo':'GAREPA-100'},{'Codigo':'GREPAR-100'},{'Codigo':'GSOBRA-100'},{'Codigo':'GN-100'},{'Codigo':'GN-100-TR'},{'Codigo':'GREZAG-100'},{'Codigo':'IN-100'}]";
        //        var clasesDeValoracion = JsonConvert.DeserializeObject<IList<ClaseDeValoracion>>(json);
                
        //        foreach (var claseDeValoracion in clasesDeValoracion)
        //        {
        //            if (await claseDeValoracionSearcher.GetByRemoteId(claseDeValoracion.RemoteId) == null)
        //            {
        //                await claseDeValoracionGenerator.Generate(claseDeValoracion);
        //            }
        //            else
        //            {
        //                await claseDeValoracionUpdater.Update(claseDeValoracion);
        //            }
        //        }
        //    }
        //    catch (Exception e)
        //    {

        //        throw;
        //    }
        //}
    }
}
