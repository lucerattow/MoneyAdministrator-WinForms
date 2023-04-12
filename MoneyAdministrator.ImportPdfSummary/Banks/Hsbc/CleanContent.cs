namespace MoneyAdministrator.ImportPdfSummary.Banks.Supervielle
{
    public class CleanContent
    {
        public static List<string> FilterTrash(List<string> pages)
        {
            var pagesClean = new List<string>();
            foreach (var page in pages)
            {
                var pageClean = page;
                pageClean = FilteredFooter(pageClean);
                pageClean = FilteredHeader(pageClean);

                pagesClean.Add(pageClean);
            }

            var results = new List<string>();

            foreach (var page in pagesClean)
            {
                results.AddRange(page.Split("\n").ToList());
            }

            results = FilteredLastText(results);

            //para ver la data final
            string plainText = string.Join("\n", results);

            return results;
        }

        private static string FilteredFooter(string page)
        {
            List<string> result = new List<string>();
            var lines = page.Split("\n").ToList();

            foreach (var line in lines)
            {
                if (line.Contains("031 305 8 031 305 8 CERATTO LUCAS EZEQUIEL"))
                    break;

                result.Add(line);
            }

            return string.Join("\n", result.Where(x => !string.IsNullOrEmpty(x)));
        }

        private static string FilteredHeader(string page)
        {
            List<string> result = new List<string>();
            var lines = page.Split("\n").ToList();

            bool copy = !lines.Where(x => x.Contains("031 . 305 . 8")).Any();

            foreach (var line in lines)
            {
                if (line.Contains("031 . 305 . 8"))
                {
                    copy = true;
                    continue;
                }

                if (!copy)
                    continue;

                result.Add(line);
            }

            return string.Join("\n", result.Where(x => !string.IsNullOrEmpty(x)));
        }

        public static List<string> FilteredLastText(List<string> lines)
        {
            List<string> result = new List<string>();

            bool copy = !lines.Where(x => x.Contains("TOTAL TITULAR      CERATTO LUCAS EZEQUIEL")).Any();

            foreach (var line in lines)
            {
                if (line.Contains("TOTAL TITULAR      CERATTO LUCAS EZEQUIEL"))
                    break;

                result.Add(line);
            }

            return result;
        }

        public static List<string> GetSummaryPropertiesSectionString(List<string> lines)
        {
            var results = new List<string>();

            //Obtengo las lineas
            int index = 0;
            for (int i = 0; i < lines.Count; i++)
            {
                if (i == 0 || i == 2 || i == 4 || i == 6)
                    results.Add(lines[i]);
            }

            return results;
        }

        public static List<string> GetConsolidatedSectionString(List<string> lines)
        {
            var results = new List<string>();

            //Obtengo las lineas
            bool copy = false;
            int index = 0;
            foreach (var line in lines)
            {
                if (line.Contains("RESUMEN CONSOLIDADO"))
                    copy = true;

                if (line.Contains("DETALLE DEL MES"))
                    break;

                if (!copy)
                    continue;

                results.Add(line);

                index++;
            }

            //Elimino lineas innecesarias:
            results.RemoveAll(x => x.Contains("RESUMEN CONSOLIDADO"));
            results.RemoveAll(x => x.Contains("----------------------"));
            results.RemoveAll(x => x.Contains("______________________"));
            results.RemoveAll(x => x.Contains("SALDO PENDIENTE"));
            //results.RemoveAll(x => x.Contains("SUBTOTAL"));
            results.RemoveAll(x => x.Contains("TOTAL CONSUMOS DEL MES"));
            results.RemoveAll(x => x.Contains("SALDO ACTUAL"));
            results.RemoveAll(x => x.Contains("PAGO MINIMO"));

            //No elimino "SUBTOTAL" para usarlo de separador.

            return results.Where(x => !string.IsNullOrEmpty(x)).ToList();
        }

        public static List<string> GetDetailsSectionString(List<string> lines)
        {
            var results = new List<string>();

            //Obtengo las lineas
            bool copy = false;
            int index = 0;
            foreach (var line in lines)
            {
                if (line.Contains("DETALLE DEL MES"))
                {
                    copy = true;
                    continue;
                }

                if (!copy)
                    continue;

                results.Add(line);

                index++;
            }

            return results.Where(x => !string.IsNullOrEmpty(x)).ToList();
        }
    }
}
