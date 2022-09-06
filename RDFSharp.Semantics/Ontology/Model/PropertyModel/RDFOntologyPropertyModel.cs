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

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RDFSharp.Model;

namespace RDFSharp.Semantics
{
    /// <summary>
    /// RDFOntologyPropertyModel represents the T-BOX of application domain properties
    /// </summary>
    public class RDFOntologyPropertyModel : IEnumerable<RDFResource>
    {
        #region Properties
        /// <summary>
        /// Count of the properties
        /// </summary>
        public long PropertiesCount
            => Properties.Count;

        /// <summary>
        /// Count of the deprecated properties
        /// </summary>
        public long DeprecatedPropertiesCount
        {
            get
            {
                long count = 0;
                IEnumerator<RDFResource> deprecatedProperties = DeprecatedPropertiesEnumerator;
                while (deprecatedProperties.MoveNext())
                    count++;
                return count;
            }
        }

        /// <summary>
        /// Count of the annotation properties
        /// </summary>
        public long AnnotationPropertiesCount
        {
            get
            {
                long count = 0;
                IEnumerator<RDFResource> annotationProperties = AnnotationPropertiesEnumerator;
                while (annotationProperties.MoveNext())
                    count++;
                return count;
            }
        }

        /// <summary>
        /// Count of the object properties
        /// </summary>
        public long ObjectPropertiesCount
        {
            get
            {
                long count = 0;
                IEnumerator<RDFResource> objectProperties = ObjectPropertiesEnumerator;
                while (objectProperties.MoveNext())
                    count++;
                return count;
            }
        }

        /// <summary>
        /// Count of the datatype properties
        /// </summary>
        public long DatatypePropertiesCount
        {
            get
            {
                long count = 0;
                IEnumerator<RDFResource> datatypeProperties = DatatypePropertiesEnumerator;
                while (datatypeProperties.MoveNext())
                    count++;
                return count;
            }
        }

        /// <summary>
        /// Count of the functional properties
        /// </summary>
        public long FunctionalPropertiesCount
        {
            get
            {
                long count = 0;
                IEnumerator<RDFResource> functionalProperties = FunctionalPropertiesEnumerator;
                while (functionalProperties.MoveNext())
                    count++;
                return count;
            }
        }

        /// <summary>
        /// Count of the inverse functional properties
        /// </summary>
        public long InverseFunctionalPropertiesCount
        {
            get
            {
                long count = 0;
                IEnumerator<RDFResource> inverseFunctionalProperties = InverseFunctionalPropertiesEnumerator;
                while (inverseFunctionalProperties.MoveNext())
                    count++;
                return count;
            }
        }

        /// <summary>
        /// Count of the symmetric properties
        /// </summary>
        public long SymmetricPropertiesCount
        {
            get
            {
                long count = 0;
                IEnumerator<RDFResource> symmetricProperties = SymmetricPropertiesEnumerator;
                while (symmetricProperties.MoveNext())
                    count++;
                return count;
            }
        }

        /// <summary>
        /// Count of the transitive properties
        /// </summary>
        public long TransitivePropertiesCount
        {
            get
            {
                long count = 0;
                IEnumerator<RDFResource> transitiveProperties = TransitivePropertiesEnumerator;
                while (transitiveProperties.MoveNext())
                    count++;
                return count;
            }
        }

        /// <summary>
        /// Count of the asymmetric properties [OWL2]
        /// </summary>
        public long AsymmetricPropertiesCount
        {
            get
            {
                long count = 0;
                IEnumerator<RDFResource> asymmetricProperties = AsymmetricPropertiesEnumerator;
                while (asymmetricProperties.MoveNext())
                    count++;
                return count;
            }
        }

        /// <summary>
        /// Count of the reflexive properties [OWL2]
        /// </summary>
        public long ReflexivePropertiesCount
        {
            get
            {
                long count = 0;
                IEnumerator<RDFResource> reflexiveProperties = ReflexivePropertiesEnumerator;
                while (reflexiveProperties.MoveNext())
                    count++;
                return count;
            }
        }

        /// <summary>
        /// Count of the irreflexive properties [OWL2]
        /// </summary>
        public long IrreflexivePropertiesCount
        {
            get
            {
                long count = 0;
                IEnumerator<RDFResource> irreflexiveProperties = IrreflexivePropertiesEnumerator;
                while (irreflexiveProperties.MoveNext())
                    count++;
                return count;
            }
        }

        /// <summary>
        /// Count of the owl:AllDisjointProperties [OWL2]
        /// </summary>
        public long AllDisjointPropertiesCount
        {
            get
            {
                long count = 0;
                IEnumerator<RDFResource> allDisjointProperties = AllDisjointPropertiesEnumerator;
                while (allDisjointProperties.MoveNext())
                    count++;
                return count;
            }
        }

        /// <summary>
        /// Gets the enumerator on the properties for iteration
        /// </summary>
        public IEnumerator<RDFResource> PropertiesEnumerator
            => Properties.Values.GetEnumerator();

        /// <summary>
        /// Gets the enumerator on the deprecated properties for iteration
        /// </summary>
        public IEnumerator<RDFResource> DeprecatedPropertiesEnumerator
        {
            get
            {
                IEnumerator<RDFResource> properties = PropertiesEnumerator;
                while (properties.MoveNext())
                {
                    if (TBoxGraph[properties.Current, RDFVocabulary.RDF.TYPE, RDFVocabulary.OWL.DEPRECATED_PROPERTY, null].Any())
                        yield return properties.Current;
                }
            }
        }

        /// <summary>
        /// Gets the enumerator on the annotation properties for iteration
        /// </summary>
        public IEnumerator<RDFResource> AnnotationPropertiesEnumerator
        {
            get
            {
                IEnumerator<RDFResource> properties = PropertiesEnumerator;
                while (properties.MoveNext())
                {
                    if (TBoxGraph[properties.Current, RDFVocabulary.RDF.TYPE, RDFVocabulary.OWL.ANNOTATION_PROPERTY, null].Any())
                        yield return properties.Current;
                }
            }
        }

        /// <summary>
        /// Gets the enumerator on the object properties for iteration
        /// </summary>
        public IEnumerator<RDFResource> ObjectPropertiesEnumerator
        {
            get
            {
                IEnumerator<RDFResource> properties = PropertiesEnumerator;
                while (properties.MoveNext())
                {
                    if (TBoxGraph[properties.Current, RDFVocabulary.RDF.TYPE, RDFVocabulary.OWL.OBJECT_PROPERTY, null].Any())
                        yield return properties.Current;
                }
            }
        }

        /// <summary>
        /// Gets the enumerator on the datatype properties for iteration
        /// </summary>
        public IEnumerator<RDFResource> DatatypePropertiesEnumerator
        {
            get
            {
                IEnumerator<RDFResource> properties = PropertiesEnumerator;
                while (properties.MoveNext())
                {
                    if (TBoxGraph[properties.Current, RDFVocabulary.RDF.TYPE, RDFVocabulary.OWL.DATATYPE_PROPERTY, null].Any())
                        yield return properties.Current;
                }
            }
        }

        /// <summary>
        /// Gets the enumerator on the functional properties for iteration
        /// </summary>
        public IEnumerator<RDFResource> FunctionalPropertiesEnumerator
        {
            get
            {
                IEnumerator<RDFResource> properties = PropertiesEnumerator;
                while (properties.MoveNext())
                {
                    if (TBoxGraph[properties.Current, RDFVocabulary.RDF.TYPE, RDFVocabulary.OWL.FUNCTIONAL_PROPERTY, null].Any())
                        yield return properties.Current;
                }
            }
        }

        /// <summary>
        /// Gets the enumerator on the inverse functional properties for iteration
        /// </summary>
        public IEnumerator<RDFResource> InverseFunctionalPropertiesEnumerator
        {
            get
            {
                IEnumerator<RDFResource> objectProperties = ObjectPropertiesEnumerator;
                while (objectProperties.MoveNext())
                {
                    if (TBoxGraph[objectProperties.Current, RDFVocabulary.RDF.TYPE, RDFVocabulary.OWL.INVERSE_FUNCTIONAL_PROPERTY, null].Any())
                        yield return objectProperties.Current;
                }
            }
        }

        /// <summary>
        /// Gets the enumerator on the symmetric properties for iteration
        /// </summary>
        public IEnumerator<RDFResource> SymmetricPropertiesEnumerator
        {
            get
            {
                IEnumerator<RDFResource> objectProperties = ObjectPropertiesEnumerator;
                while (objectProperties.MoveNext())
                {
                    if (TBoxGraph[objectProperties.Current, RDFVocabulary.RDF.TYPE, RDFVocabulary.OWL.SYMMETRIC_PROPERTY, null].Any())
                        yield return objectProperties.Current;
                }
            }
        }

        /// <summary>
        /// Gets the enumerator on the transitive properties for iteration
        /// </summary>
        public IEnumerator<RDFResource> TransitivePropertiesEnumerator
        {
            get
            {
                IEnumerator<RDFResource> objectProperties = ObjectPropertiesEnumerator;
                while (objectProperties.MoveNext())
                {
                    if (TBoxGraph[objectProperties.Current, RDFVocabulary.RDF.TYPE, RDFVocabulary.OWL.TRANSITIVE_PROPERTY, null].Any())
                        yield return objectProperties.Current;
                }
            }
        }

        /// <summary>
        /// Gets the enumerator on the asymmetric properties for iteration [OWL2]
        /// </summary>
        public IEnumerator<RDFResource> AsymmetricPropertiesEnumerator
        {
            get
            {
                IEnumerator<RDFResource> objectProperties = ObjectPropertiesEnumerator;
                while (objectProperties.MoveNext())
                {
                    if (TBoxGraph[objectProperties.Current, RDFVocabulary.RDF.TYPE, RDFVocabulary.OWL.ASYMMETRIC_PROPERTY, null].Any())
                        yield return objectProperties.Current;
                }
            }
        }

        /// <summary>
        /// Gets the enumerator on the reflexive properties for iteration [OWL2]
        /// </summary>
        public IEnumerator<RDFResource> ReflexivePropertiesEnumerator
        {
            get
            {
                IEnumerator<RDFResource> objectProperties = ObjectPropertiesEnumerator;
                while (objectProperties.MoveNext())
                {
                    if (TBoxGraph[objectProperties.Current, RDFVocabulary.RDF.TYPE, RDFVocabulary.OWL.REFLEXIVE_PROPERTY, null].Any())
                        yield return objectProperties.Current;
                }
            }
        }

        /// <summary>
        /// Gets the enumerator on the irreflexive properties for iteration [OWL2]
        /// </summary>
        public IEnumerator<RDFResource> IrreflexivePropertiesEnumerator
        {
            get
            {
                IEnumerator<RDFResource> objectProperties = ObjectPropertiesEnumerator;
                while (objectProperties.MoveNext())
                {
                    if (TBoxGraph[objectProperties.Current, RDFVocabulary.RDF.TYPE, RDFVocabulary.OWL.IRREFLEXIVE_PROPERTY, null].Any())
                        yield return objectProperties.Current;
                }
            }
        }

        /// <summary>
        /// Gets the enumerator on the owl:AllDisjointProperties for iteration [OWL2]
        /// </summary>
        public IEnumerator<RDFResource> AllDisjointPropertiesEnumerator
            => TBoxGraph[null, RDFVocabulary.RDF.TYPE, RDFVocabulary.OWL.ALL_DISJOINT_PROPERTIES, null]
                .Select(t => (RDFResource)t.Subject)
                .GetEnumerator();

        /// <summary>
        /// Collection of properties
        /// </summary>
        internal Dictionary<long, RDFResource> Properties { get; set; }

        /// <summary>
        /// T-BOX knowledge describing properties
        /// </summary>
        internal RDFGraph TBoxGraph { get; set; }

        /// <summary>
        /// T-BOX knowledge inferred
        /// </summary>
        internal RDFGraph TBoxInferenceGraph { get; set; }

        /// <summary>
        /// T-BOX virtual knowledge (comprehensive of both available and inferred)
        /// </summary>
        internal RDFGraph TBoxVirtualGraph
            => TBoxGraph.UnionWith(TBoxInferenceGraph);
        #endregion

        #region Ctors
        /// <summary>
        /// Builds an empty ontology property model
        /// </summary>
        public RDFOntologyPropertyModel()
        {
            Properties = new Dictionary<long, RDFResource>();
            TBoxGraph = new RDFGraph();
            TBoxInferenceGraph = new RDFGraph();
        }
        #endregion

        #region Interfaces
        /// <summary>
        /// Exposes a typed enumerator on the properties for iteration
        /// </summary>
        IEnumerator<RDFResource> IEnumerable<RDFResource>.GetEnumerator()
            => PropertiesEnumerator;

        /// <summary>
        /// Exposes an untyped enumerator on the properties for iteration
        /// </summary>
        IEnumerator IEnumerable.GetEnumerator()
            => PropertiesEnumerator;
        #endregion

        #region Methods
        /// <summary>
        /// Declares the existence of the given owl:AnnotationProperty to the model
        /// </summary>
        public RDFOntologyPropertyModel DeclareAnnotationProperty(RDFResource owlAnnotationProperty, RDFOntologyAnnotationPropertyBehavior owlAnnotationPropertyBehavior=null)
        {
            if (owlAnnotationProperty == null)
                throw new RDFSemanticsException("Cannot declare owl:AnnotationProperty to the model because given \"owlAnnotationProperty\" parameter is null");
            if (owlAnnotationPropertyBehavior == null)
                owlAnnotationPropertyBehavior = new RDFOntologyAnnotationPropertyBehavior();

            //Declare property to the model
            if (!Properties.ContainsKey(owlAnnotationProperty.PatternMemberID))
                Properties.Add(owlAnnotationProperty.PatternMemberID, owlAnnotationProperty);

            //Add knowledge to the T-BOX
            TBoxGraph.AddTriple(new RDFTriple(owlAnnotationProperty, RDFVocabulary.RDF.TYPE, RDFVocabulary.OWL.ANNOTATION_PROPERTY));
            if (owlAnnotationPropertyBehavior.Deprecated)
                TBoxGraph.AddTriple(new RDFTriple(owlAnnotationProperty, RDFVocabulary.RDF.TYPE, RDFVocabulary.OWL.DEPRECATED_PROPERTY));

            return this;
        }

        /// <summary>
        /// Declares the existence of the given owl:ObjectProperty to the model
        /// </summary>
        public RDFOntologyPropertyModel DeclareObjectProperty(RDFResource owlObjectProperty, RDFOntologyObjectPropertyBehavior owlObjectPropertyBehavior=null)
        {
            if (owlObjectProperty == null)
                throw new RDFSemanticsException("Cannot declare owl:ObjectProperty to the model because given \"owlObjectProperty\" parameter is null");
            if (owlObjectPropertyBehavior == null)
                owlObjectPropertyBehavior = new RDFOntologyObjectPropertyBehavior();

            //Declare property to the model
            if (!Properties.ContainsKey(owlObjectProperty.PatternMemberID))
                Properties.Add(owlObjectProperty.PatternMemberID, owlObjectProperty);

            //Add knowledge to the T-BOX
            TBoxGraph.AddTriple(new RDFTriple(owlObjectProperty, RDFVocabulary.RDF.TYPE, RDFVocabulary.OWL.OBJECT_PROPERTY));
            if (owlObjectPropertyBehavior.Deprecated)
                TBoxGraph.AddTriple(new RDFTriple(owlObjectProperty, RDFVocabulary.RDF.TYPE, RDFVocabulary.OWL.DEPRECATED_PROPERTY));
            if (owlObjectPropertyBehavior.Functional)
                TBoxGraph.AddTriple(new RDFTriple(owlObjectProperty, RDFVocabulary.RDF.TYPE, RDFVocabulary.OWL.FUNCTIONAL_PROPERTY));
            if (owlObjectPropertyBehavior.InverseFunctional)
                TBoxGraph.AddTriple(new RDFTriple(owlObjectProperty, RDFVocabulary.RDF.TYPE, RDFVocabulary.OWL.INVERSE_FUNCTIONAL_PROPERTY));
            if (owlObjectPropertyBehavior.Transitive)
                TBoxGraph.AddTriple(new RDFTriple(owlObjectProperty, RDFVocabulary.RDF.TYPE, RDFVocabulary.OWL.TRANSITIVE_PROPERTY));
            if (owlObjectPropertyBehavior.Symmetric)
                TBoxGraph.AddTriple(new RDFTriple(owlObjectProperty, RDFVocabulary.RDF.TYPE, RDFVocabulary.OWL.SYMMETRIC_PROPERTY));
            if (owlObjectPropertyBehavior.Asymmetric)  //OWL2
                TBoxGraph.AddTriple(new RDFTriple(owlObjectProperty, RDFVocabulary.RDF.TYPE, RDFVocabulary.OWL.ASYMMETRIC_PROPERTY));
            if (owlObjectPropertyBehavior.Reflexive)   //OWL2
                TBoxGraph.AddTriple(new RDFTriple(owlObjectProperty, RDFVocabulary.RDF.TYPE, RDFVocabulary.OWL.REFLEXIVE_PROPERTY));
            if (owlObjectPropertyBehavior.Irreflexive) //OWL2
                TBoxGraph.AddTriple(new RDFTriple(owlObjectProperty, RDFVocabulary.RDF.TYPE, RDFVocabulary.OWL.IRREFLEXIVE_PROPERTY));
            if (owlObjectPropertyBehavior.Domain != null)
                TBoxGraph.AddTriple(new RDFTriple(owlObjectProperty, RDFVocabulary.RDFS.DOMAIN, owlObjectPropertyBehavior.Domain));
            if (owlObjectPropertyBehavior.Range != null)
                TBoxGraph.AddTriple(new RDFTriple(owlObjectProperty, RDFVocabulary.RDFS.RANGE, owlObjectPropertyBehavior.Range));

            return this;
        }

        /// <summary>
        /// Declares the existence of the given owl:DatatypeProperty to the model
        /// </summary>
        public RDFOntologyPropertyModel DeclareDatatypeProperty(RDFResource owlDatatypeProperty, RDFOntologyDatatypePropertyBehavior owlDatatypePropertyBehavior=null)
        {
            if (owlDatatypeProperty == null)
                throw new RDFSemanticsException("Cannot declare owl:DatatypeProperty to the model because given \"owlDatatypeProperty\" parameter is null");
            if (owlDatatypePropertyBehavior == null)
                owlDatatypePropertyBehavior = new RDFOntologyDatatypePropertyBehavior();

            //Declare property to the model
            if (!Properties.ContainsKey(owlDatatypeProperty.PatternMemberID))
                Properties.Add(owlDatatypeProperty.PatternMemberID, owlDatatypeProperty);

            //Add knowledge to the T-BOX
            TBoxGraph.AddTriple(new RDFTriple(owlDatatypeProperty, RDFVocabulary.RDF.TYPE, RDFVocabulary.OWL.DATATYPE_PROPERTY));
            if (owlDatatypePropertyBehavior.Deprecated)
                TBoxGraph.AddTriple(new RDFTriple(owlDatatypeProperty, RDFVocabulary.RDF.TYPE, RDFVocabulary.OWL.DEPRECATED_PROPERTY));
            if (owlDatatypePropertyBehavior.Functional)
                TBoxGraph.AddTriple(new RDFTriple(owlDatatypeProperty, RDFVocabulary.RDF.TYPE, RDFVocabulary.OWL.FUNCTIONAL_PROPERTY));
            if (owlDatatypePropertyBehavior.Domain != null)
                TBoxGraph.AddTriple(new RDFTriple(owlDatatypeProperty, RDFVocabulary.RDFS.DOMAIN, owlDatatypePropertyBehavior.Domain));
            if (owlDatatypePropertyBehavior.Range != null)
                TBoxGraph.AddTriple(new RDFTriple(owlDatatypeProperty, RDFVocabulary.RDFS.RANGE, owlDatatypePropertyBehavior.Range));

            return this;
        }

        //TAXONOMIES

        /// <summary>
        /// Annotates the given property with the given "annotationProperty -> annotationValue"
        /// </summary>
        public RDFOntologyPropertyModel AnnotateProperty(RDFResource owlProperty, RDFResource annotationProperty, RDFResource annotationValue)
        {
            if (owlProperty == null)
                throw new RDFSemanticsException("Cannot annotate owl property because given \"owlProperty\" parameter is null");
            if (annotationProperty == null)
                throw new RDFSemanticsException("Cannot annotate owl property because given \"annotationProperty\" parameter is null");
            if (annotationProperty.IsBlank)
                throw new RDFSemanticsException("Cannot annotate owl property because given \"annotationProperty\" parameter is a blank predicate");
            if (annotationValue == null)
                throw new RDFSemanticsException("Cannot annotate owl property because given \"annotationValue\" parameter is null");

            //Add knowledge to the T-BOX
            TBoxGraph.AddTriple(new RDFTriple(owlProperty, annotationProperty, annotationValue));

            return this;
        }

        /// <summary>
        /// Annotates the given property with the given "annotationProperty -> annotationValue"
        /// </summary>
        public RDFOntologyPropertyModel AnnotateProperty(RDFResource owlProperty, RDFResource annotationProperty, RDFLiteral annotationValue)
        {
            if (owlProperty == null)
                throw new RDFSemanticsException("Cannot annotate owl property because given \"owlProperty\" parameter is null");
            if (annotationProperty == null)
                throw new RDFSemanticsException("Cannot annotate owl property because given \"annotationProperty\" parameter is null");
            if (annotationProperty.IsBlank)
                throw new RDFSemanticsException("Cannot annotate owl property because given \"annotationProperty\" parameter is a blank predicate");
            if (annotationValue == null)
                throw new RDFSemanticsException("Cannot annotate owl property because given \"annotationValue\" parameter is null");

            //Add knowledge to the T-BOX
            TBoxGraph.AddTriple(new RDFTriple(owlProperty, annotationProperty, annotationValue));

            return this;
        }

        /// <summary>
        /// Declares the given "SubProperty(childProperty,motherProperty)" relation to the model
        /// </summary>
        public RDFOntologyPropertyModel DeclareSubProperty(RDFResource childProperty, RDFResource motherProperty)
        {
            #region OWL-DL Integrity Checks
            bool OWLDLIntegrityChecks()
                => !childProperty.CheckReservedProperty()
                      && !motherProperty.CheckReservedProperty()
                        && this.CheckSubPropertyCompatibility(childProperty, motherProperty);
            #endregion

            if (childProperty == null)
                throw new RDFSemanticsException("Cannot declare rdfs:subPropertyOf relation because given \"childProperty\" parameter is null");
            if (motherProperty == null)
                throw new RDFSemanticsException("Cannot declare rdfs:subPropertyOf relation because given \"motherProperty\" parameter is null");
            if (childProperty.Equals(motherProperty))
                throw new RDFSemanticsException("Cannot declare rdfs:subPropertyOf relation because given \"childProperty\" parameter refers to the same property as the given \"motherProperty\" parameter");

            //Add knowledge to the T-BOX (or raise warning if integrity policy is active and violations are detected)
            if (!RDFSemanticsOptions.ShouldCheckOWLDLIntegrity || OWLDLIntegrityChecks())
                TBoxGraph.AddTriple(new RDFTriple(childProperty, RDFVocabulary.RDFS.SUB_PROPERTY_OF, motherProperty));
            else
                RDFSemanticsEvents.RaiseSemanticsWarning(string.Format("SubProperty relation between property '{0}' and property '{1}' cannot be added to the model because it would violate OWL-DL integrity", childProperty, motherProperty));

            return this;
        }

        /// <summary>
        /// Declares the given "EquivalentProperty(leftProperty,rightProperty)" relation to the model
        /// </summary>
        public RDFOntologyPropertyModel DeclareEquivalentProperties(RDFResource leftProperty, RDFResource rightProperty)
        {
            #region OWL-DL Integrity Checks
            bool OWLDLIntegrityChecks()
                => !leftProperty.CheckReservedProperty()
                      && !rightProperty.CheckReservedProperty()
                        && this.CheckEquivalentPropertyCompatibility(leftProperty, rightProperty);
            #endregion

            if (leftProperty == null)
                throw new RDFSemanticsException("Cannot declare owl:equivalentProperty relation because given \"leftProperty\" parameter is null");
            if (rightProperty == null)
                throw new RDFSemanticsException("Cannot declare owl:equivalentProperty relation because given \"rightProperty\" parameter is null");
            if (leftProperty.Equals(rightProperty))
                throw new RDFSemanticsException("Cannot declare owl:equivalentProperty relation because given \"leftProperty\" parameter refers to the same property as the given \"rightProperty\" parameter");

            //Add knowledge to the T-BOX (or raise warning if integrity policy is active and violations are detected)
            if (!RDFSemanticsOptions.ShouldCheckOWLDLIntegrity || OWLDLIntegrityChecks())
            {
                TBoxGraph.AddTriple(new RDFTriple(leftProperty, RDFVocabulary.OWL.EQUIVALENT_PROPERTY, rightProperty));

                //Also add an automatic T-BOX inference exploiting symmetry of owl:equivalentProperty relation
                TBoxInferenceGraph.AddTriple(new RDFTriple(rightProperty, RDFVocabulary.OWL.EQUIVALENT_PROPERTY, leftProperty));
            }
            else
                RDFSemanticsEvents.RaiseSemanticsWarning(string.Format("EquivalentProperty relation between leftProperty '{0}' and rightProperty '{1}' cannot be added to the model because it would violate OWL-DL integrity", leftProperty, rightProperty));

            return this;
        }

        /// <summary>
        /// Declares the given "PropertyDisjointWith(leftProperty,rightProperty)" relation to the model [OWL2]
        /// </summary>
        public RDFOntologyPropertyModel DeclareDisjointProperties(RDFResource leftProperty, RDFResource rightProperty)
        {
            #region OWL-DL Integrity Checks
            bool OWLDLIntegrityChecks()
                => !leftProperty.CheckReservedProperty()
                      && !rightProperty.CheckReservedProperty()
                        && this.CheckDisjointPropertyCompatibility(leftProperty, rightProperty);
            #endregion

            if (leftProperty == null)
                throw new RDFSemanticsException("Cannot declare owl:propertyDisjointWith relation because given \"leftProperty\" parameter is null");
            if (rightProperty == null)
                throw new RDFSemanticsException("Cannot declare owl:propertyDisjointWith relation because given \"rightProperty\" parameter is null");
            if (leftProperty.Equals(rightProperty))
                throw new RDFSemanticsException("Cannot declare owl:propertyDisjointWith relation because given \"leftProperty\" parameter refers to the same property as the given \"rightProperty\" parameter");

            //Add knowledge to the T-BOX (or raise warning if integrity policy is active and violations are detected)
            if (!RDFSemanticsOptions.ShouldCheckOWLDLIntegrity || OWLDLIntegrityChecks())
            {
                TBoxGraph.AddTriple(new RDFTriple(leftProperty, RDFVocabulary.OWL.PROPERTY_DISJOINT_WITH, rightProperty));

                //Also add an automatic T-BOX inference exploiting symmetry of owl:propertyDisjointWith relation
                TBoxInferenceGraph.AddTriple(new RDFTriple(rightProperty, RDFVocabulary.OWL.PROPERTY_DISJOINT_WITH, leftProperty));
            }
            else
                RDFSemanticsEvents.RaiseSemanticsWarning(string.Format("PropertyDisjointWith relation between leftProperty '{0}' and rightProperty '{1}' cannot be added to the model because it would violate OWL-DL integrity", leftProperty, rightProperty));

            return this;
        }

        /// <summary>
        /// Declares the existence of the given owl:AllDisjointProperties class to the model [OWL2]
        /// </summary>
        public RDFOntologyPropertyModel DeclareAllDisjointProperties(RDFResource owlClass, List<RDFResource> disjointProperties)
        {
            if (owlClass == null)
                throw new RDFSemanticsException("Cannot declare owl:AllDisjointProperties class to the model because given \"owlClass\" parameter is null");
            if (disjointProperties == null)
                disjointProperties = new List<RDFResource>();

            //Add knowledge to the T-BOX
            RDFCollection allDisjointPropertiesCollection = new RDFCollection(RDFModelEnums.RDFItemTypes.Resource);
            disjointProperties.ForEach(disjointProperty => allDisjointPropertiesCollection.AddItem(disjointProperty));
            TBoxGraph.AddCollection(allDisjointPropertiesCollection);
            TBoxGraph.AddTriple(new RDFTriple(owlClass, RDFVocabulary.OWL.MEMBERS, allDisjointPropertiesCollection.ReificationSubject));
            TBoxGraph.AddTriple(new RDFTriple(owlClass, RDFVocabulary.RDF.TYPE, RDFVocabulary.OWL.ALL_DISJOINT_PROPERTIES));

            return this;
        }

        /// <summary>
        /// Declares the given "Inverse(leftProperty,rightProperty)" relation to the model
        /// </summary>
        public RDFOntologyPropertyModel DeclareInverseProperties(RDFResource leftProperty, RDFResource rightProperty)
        {
            #region OWL-DL Integrity Checks
            bool OWLDLIntegrityChecks()
                => !leftProperty.CheckReservedProperty()
                      && !rightProperty.CheckReservedProperty()
                        && this.CheckInversePropertyCompatibility(leftProperty, rightProperty);
            #endregion

            if (leftProperty == null)
                throw new RDFSemanticsException("Cannot declare owl:inverseOf relation because given \"leftProperty\" parameter is null");
            if (rightProperty == null)
                throw new RDFSemanticsException("Cannot declare owl:inverseOf relation because given \"rightProperty\" parameter is null");
            if (leftProperty.Equals(rightProperty))
                throw new RDFSemanticsException("Cannot declare owl:inverseOf relation because given \"leftProperty\" parameter refers to the same property as the given \"rightProperty\" parameter");

            //Add knowledge to the T-BOX (or raise warning if integrity policy is active and violations are detected)
            if (!RDFSemanticsOptions.ShouldCheckOWLDLIntegrity || OWLDLIntegrityChecks())
            {
                TBoxGraph.AddTriple(new RDFTriple(leftProperty, RDFVocabulary.OWL.INVERSE_OF, rightProperty));

                //Also add an automatic T-BOX inference exploiting symmetry of owl:inverseProperty relation
                TBoxInferenceGraph.AddTriple(new RDFTriple(rightProperty, RDFVocabulary.OWL.INVERSE_OF, leftProperty));
            }
            else
                RDFSemanticsEvents.RaiseSemanticsWarning(string.Format("Inverse relation between leftProperty '{0}' and rightProperty '{1}' cannot be added to the model because it would violate OWL-DL integrity", leftProperty, rightProperty));

            return this;
        }

        /// <summary>
        /// Declares the existence of the given "PropertyChainAxiom(owlProperty,chainProperties)" relation to the model [OWL2]
        /// </summary>
        public RDFOntologyPropertyModel DeclarePropertyChainAxiom(RDFResource owlProperty, List<RDFResource> chainProperties)
        {
            #region OWL-DL Integrity Checks
            bool OWLDLIntegrityChecks()
                => !owlProperty.CheckReservedProperty();
            #endregion

            if (owlProperty == null)
                throw new RDFSemanticsException("Cannot declare owl:propertyChainAxiom relation because given \"owlProperty\" parameter is null");
            if (chainProperties == null)
                throw new RDFSemanticsException("Cannot declare owl:propertyChainAxiom relation because given \"chainProperties\" parameter is null");
            if (chainProperties.Any(chainAxiomPropertyStep => chainAxiomPropertyStep.Equals(owlProperty)))
                throw new RDFSemanticsException("Cannot declare owl:propertyChainAxiom relation because given \"owlProperty\" parameter is contained in the given \"chainProperties\" parameter");

            //Add knowledge to the T-BOX (or raise warning if integrity policy is active and violations are detected)
            if (!RDFSemanticsOptions.ShouldCheckOWLDLIntegrity || OWLDLIntegrityChecks())
            {
                RDFCollection chainPropertiesCollection = new RDFCollection(RDFModelEnums.RDFItemTypes.Resource);
                chainProperties.ForEach(chainProperty => chainPropertiesCollection.AddItem(chainProperty));
                TBoxGraph.AddCollection(chainPropertiesCollection);
                TBoxGraph.AddTriple(new RDFTriple(owlProperty, RDFVocabulary.OWL.PROPERTY_CHAIN_AXIOM, chainPropertiesCollection.ReificationSubject));
            }
            else
                RDFSemanticsEvents.RaiseSemanticsWarning(string.Format("PropertyChainAxiom '{0}' cannot be added to the model because it would violate OWL-DL integrity", owlProperty));

            return this;
        }

        //EXPORT

        /// <summary>
        /// Gets a graph representation of the model
        /// </summary>
        public RDFGraph ToRDFGraph(bool includeInferences)
            => includeInferences ? TBoxVirtualGraph : TBoxGraph;

        /// <summary>
        /// Asynchronously gets a graph representation of the model
        /// </summary>
        public Task<RDFGraph> ToRDFGraphAsync(bool includeInferences)
            => Task.Run(() => ToRDFGraph(includeInferences));
        #endregion
    }

    #region Behaviors
    /// <summary>
    /// RDFOntologyAnnotationPropertyBehavior defines the mathematical aspects of an owl:AnnotationProperty instance
    /// </summary>
    public class RDFOntologyAnnotationPropertyBehavior
    {
        #region Properties
        /// <summary>
        /// Defines the property as instance of owl:DeprecatedProperty
        /// </summary>
        public bool Deprecated { get; set; }
        #endregion
    }

    /// <summary>
    /// RDFOntologyDatatypePropertyBehavior defines the mathematical aspects of an owl:DatatypeProperty instance
    /// </summary>
    public class RDFOntologyDatatypePropertyBehavior : RDFOntologyAnnotationPropertyBehavior
    {
        #region Properties
        /// <summary>
        /// Defines the property as instance of owl:FunctionalProperty
        /// </summary>
        public bool Functional { get; set; }

        /// <summary>
        /// Represents the owl:Class being the rdfs:domain of the property
        /// </summary>
        public RDFResource Domain { get; set; }

        /// <summary>
        /// Represents the owl:Class being the rdfs:range of the property
        /// </summary>
        public RDFResource Range { get; set; }
        #endregion
    }

    /// <summary>
    /// RDFOntologyObjectPropertyBehavior defines the mathematical aspects of an owl:ObjectProperty instance
    /// </summary>
    public class RDFOntologyObjectPropertyBehavior : RDFOntologyDatatypePropertyBehavior
    {
        #region Properties
        /// <summary>
        /// Defines the property as instance of owl:InverseFunctionalProperty
        /// </summary>
        public bool InverseFunctional { get; set; }

        /// <summary>
        /// Defines the property as instance of owl:SymmetricProperty
        /// </summary>
        public bool Symmetric { get; set; }

        /// <summary>
        /// Defines the property as instance of owl:TransitiveProperty
        /// </summary>
        public bool Transitive { get; set; }

        /// <summary>
        /// Defines the property as instance of owl:AsymmetricProperty [OWL2]
        /// </summary>
        public bool Asymmetric { get; set; }

        /// <summary>
        /// Defines the property as instance of owl:ReflexiveProperty [OWL2]
        /// </summary>
        public bool Reflexive { get; set; }

        /// <summary>
        /// Defines the property as instance of owl:IrreflexiveProperty [OWL2]
        /// </summary>
        public bool Irreflexive { get; set; }
        #endregion
    }
    #endregion
}