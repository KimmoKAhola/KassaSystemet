using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KassaSystemet.Interfaces;
using KassaSystemet.Models;

namespace KassaSystemet.MenuPages
{
    //public class CsvFileSaveStrategy : IFileSaveStrategy
    //{
    //    public void SaveProductCatalogue()
    //    {
    //        var csvLines = ProductCatalogue.Instance.Products
    //    .OrderBy(x => x.Key)
    //    .Select(item =>
    //        $"{item.Key},{item.Value.ProductName},{item.Value.UnitPrice},{item.Value.PriceType}");

    //        string csvContent = string.Join("\n", csvLines);

    //        File.WriteAllText(FileManagerOperations.CreateProductListFilePath(), csvContent);
    //    }
    //}
}
