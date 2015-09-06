using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AegirSearch
{
    internal class IntegralCollector : Lucene.Net.Search.Collector
    {
        private int _docBase;

        private List<int> _docs = new List<int>();

        public override bool AcceptsDocsOutOfOrder
        {
            get
            {
                return true;
            }
        }

        public List<int> Docs
        {
            get { return _docs; }
        }

        public override void Collect(int doc)
        {
            _docs.Add(_docBase + doc);
        }

        public override void SetNextReader(Lucene.Net.Index.IndexReader reader, int docBase)
        {
            _docBase = docBase;
        }

        public override void SetScorer(Lucene.Net.Search.Scorer scorer)
        {
        }
    }
}
