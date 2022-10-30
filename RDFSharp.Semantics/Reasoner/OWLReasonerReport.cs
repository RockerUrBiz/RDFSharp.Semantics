﻿/*
   Copyright 2012-2022 Marco De Salvo
   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at
     http://www.apache.org/licenses/LICENSE-2.0
   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
*/

using RDFSharp.Model;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RDFSharp.Semantics
{
    /// <summary>
    /// OWLReasonerReport represents a detailed report of an ontology reasoner analysis
    /// </summary>
    public class OWLReasonerReport : IEnumerable<OWLReasonerEvidence>
    {
        #region Properties
        /// <summary>
        /// Counter of the evidences
        /// </summary>
        public int EvidencesCount 
            => Evidences.Count;

        /// <summary>
        /// Gets an enumerator on the evidences for iteration
        /// </summary>
        public IEnumerator<OWLReasonerEvidence> EvidencesEnumerator 
            => Evidences.GetEnumerator();

        /// <summary>
        /// List of evidences
        /// </summary>
        internal List<OWLReasonerEvidence> Evidences { get; set; }

        /// <summary>
        /// SyncLock for evidences
        /// </summary>
        internal object SyncLock { get; set; }
        #endregion

        #region Ctors
        /// <summary>
        /// Default-ctor to build an empty reasoner report
        /// </summary>
        internal OWLReasonerReport()
        {
            Evidences = new List<OWLReasonerEvidence>();
            SyncLock = new object();
        }
        #endregion

        #region Interfaces
        /// <summary>
        /// Exposes a typed enumerator on the reasoner report's evidences
        /// </summary>
        IEnumerator<OWLReasonerEvidence> IEnumerable<OWLReasonerEvidence>.GetEnumerator()
            => EvidencesEnumerator;

        /// <summary>
        /// Exposes an untyped enumerator on the reasoner report's evidences
        /// </summary>
        IEnumerator IEnumerable.GetEnumerator()
            => EvidencesEnumerator;
        #endregion

        #region Methods
        /// <summary>
        /// Adds the given evidence to the reasoner report
        /// </summary>
        public OWLReasonerReport AddEvidence(OWLReasonerEvidence evidence)
        {
            if (evidence != null)
            {
                lock (SyncLock)
                {
                    if (!Evidences.Any(evd => evd.EvidenceCategory == evidence.EvidenceCategory 
                                                && evd.EvidenceContent.TripleID == evidence.EvidenceContent.TripleID))
                        Evidences.Add(evidence);
                }   
            }
            return this;
        }

        /// <summary>
        /// Merges the evidences of the given report
        /// </summary>
        internal OWLReasonerReport MergeEvidences(OWLReasonerReport report)
        {
            foreach (OWLReasonerEvidence evidence in report)
                AddEvidence(evidence);
                
            return this;
        }

        /// <summary>
        /// Gets the class model evidences (T-BOX) from the reasoner report
        /// </summary>
        public List<OWLReasonerEvidence> SelectClassModelEvidences()
            => Evidences.FindAll(e => e.EvidenceCategory == OWLSemanticsEnums.OWLReasonerEvidenceCategory.ClassModel);

        /// <summary>
        /// Gets the property model evidences (T-BOX) from the reasoner report
        /// </summary>
        public List<OWLReasonerEvidence> SelectPropertyModelEvidences()
            => Evidences.FindAll(e => e.EvidenceCategory == OWLSemanticsEnums.OWLReasonerEvidenceCategory.PropertyModel);

        /// <summary>
        /// Gets the data evidences (A-BOX) from the reasoner report
        /// </summary>
        public List<OWLReasonerEvidence> SelectDataEvidences()
            => Evidences.FindAll(e => e.EvidenceCategory == OWLSemanticsEnums.OWLReasonerEvidenceCategory.Data);

        /// <summary>
        /// Gets a graph representation of the reasoner report
        /// </summary>
        public RDFGraph ToRDFGraph()
        {
            RDFGraph evidenceGraph = new RDFGraph();
            foreach (OWLReasonerEvidence evidence in this)
                evidenceGraph.AddTriple(evidence.EvidenceContent);
            return evidenceGraph;
        }

        /// <summary>
        /// Asynchronously gets a graph representation of the reasoner report
        /// </summary>
        public Task<RDFGraph> ToRDFGraphAsync()
            => Task.Run(() => ToRDFGraph());

        /// <summary>
        /// Joins the reasoner evidences of this report into the given ontology
        /// </summary>
        public void JoinEvidences(OWLOntology ontology)
        {
            foreach (OWLReasonerEvidence evidence in this)
                switch (evidence.EvidenceCategory)
                {
                    case OWLSemanticsEnums.OWLReasonerEvidenceCategory.ClassModel:
                        ontology.Model.ClassModel.TBoxGraph.AddTriple(evidence.EvidenceContent);
                        break;
                    case OWLSemanticsEnums.OWLReasonerEvidenceCategory.PropertyModel:
                        ontology.Model.PropertyModel.TBoxGraph.AddTriple(evidence.EvidenceContent);
                        break;
                    case OWLSemanticsEnums.OWLReasonerEvidenceCategory.Data:
                        ontology.Data.ABoxGraph.AddTriple(evidence.EvidenceContent);
                        break;
                }
        }

        /// <summary>
        /// Asynchronously joins the reasoner evidences of this report into the given ontology
        /// </summary>
        public Task JoinEvidencesAsync(OWLOntology ontology)
            => Task.Run(() => JoinEvidences(ontology));
        #endregion
    }
}