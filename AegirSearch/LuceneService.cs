using AegirDataTypes.Workspace;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AegirSearch
{
    public class LuceneService
    {
        // Note there are many different types of Analyzer that may be used with Lucene, the exact one you use
        // will depend on your requirements
        private Analyzer analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30);
        private Directory luceneIndexDirectory;
        private IndexWriter writer;
        private string indexPath = @"c:\temp\LuceneIndex";

        public LuceneService()
        {
            InitialiseLucene();
        }

        private void InitialiseLucene()
        {
            luceneIndexDirectory = FSDirectory.Open(indexPath);
            if (!System.IO.Directory.Exists(indexPath))
            {
                writer = new IndexWriter(luceneIndexDirectory, analyzer, IndexWriter.MaxFieldLength.UNLIMITED);
                //System.IO.Directory.Delete(indexPath, true);
                BuildIndex();
            }
        }

        public void BuildIndex()
        {
            ProjectData[] foo =
            {
                new ProjectData("SimOne","Awesome Liner"),
                new ProjectData("SimTwo","Awesome Liner"),
                new ProjectData("Presentation on the moon","Awesome Liner"),
                new ProjectData("RollTest","Oasis Of the lands"),
                new ProjectData("monmoon","rover x2"),
                new ProjectData("waypoint","Tempera")
            };
            foreach (var project in foo)
            {
                Document doc = new Document();
                doc.Add(new Field("name", project.ProjectName, Field.Store.YES, Field.Index.ANALYZED));
                doc.Add(new Field("vessel", project.VesselId, Field.Store.YES, Field.Index.ANALYZED));
                writer.AddDocument(doc);
            }
            writer.Optimize();
            writer.Dispose();
            //luceneIndexDirectory.Dispose();
        }

        public ProjectData[] Search(string searchTerm)
        {
            IndexSearcher searcher = new IndexSearcher(luceneIndexDirectory);
            IntegralCollector searcherCollector = new IntegralCollector();
            // Setup the fields to search through
            string[] searchfields = new string[] { "name", "vessel" };

            // Build our booleanquery that will be a combination of all the queries for each individual search term
            var finalQuery = new BooleanQuery();
            var parser = new MultiFieldQueryParser(Lucene.Net.Util.Version.LUCENE_30, searchfields, analyzer);

            // Split the search string into separate search terms by word
            string[] terms = searchTerm.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string term in terms)
                finalQuery.Add(parser.Parse(term.Replace("~", "") + "~"),Occur.SHOULD);

            searcher.Search(finalQuery, searcherCollector);
            var results = new ProjectData[searcherCollector.Docs.Count];
            for (int i = 0; i < searcherCollector.Docs.Count; i++)
            {
                var doc = searcher.Doc(searcherCollector.Docs[i]);
                results[i] = new ProjectData(doc.Get("name"), doc.Get("vessel"));
            }
            return results;
        }
    }
}
