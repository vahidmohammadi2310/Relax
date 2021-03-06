﻿using Relax;

namespace RelaxDemo
{
    public class DocumentRetriever
    {
        private IDocumentRepository _couch;

        public TestDocument GetById(object id, string rev)
        {
            return _couch.Get<TestDocument>(id, rev);
        }

        public DocumentRetriever(IDocumentRepository couch)
        {
            _couch = couch;
        }
    }
}