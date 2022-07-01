using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mame.Doci.Data.LuceneAccess.Cross.DocumentRulesSpace
{
    class DocumentRules
    {

        List<DocumentRuleField> _rules = new List<DocumentRuleField>();

        public void AddDocumentRuleField(DocumentRuleField newRuleField)
        {
            _rules.Add (newRuleField);
        }




    }
}
