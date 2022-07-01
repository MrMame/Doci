using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Lucene.Net.Documents;

namespace Mame.Doci.Data.LuceneAccess.Cross.DocumentRulesSpace
{
    class DocumentRuleField
    {

        Field _field;
        string _sourceTypeName;
        string _sourcePropertyName;

        DocumentRuleField(Field luceneField,string sourceTypeName, string sourceTypePropertyName)
        {
            _field = luceneField;
            _sourceTypeName = sourceTypeName;
            _sourcePropertyName = sourceTypePropertyName;

        }

    }






}
