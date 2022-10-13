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

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RDFSharp.Semantics
{
    /// <summary>
    /// OWLValidator analyzes an ontology in order to discover errors and inconsistencies affecting its model and data
    /// </summary>
    public class OWLValidator
    {
        #region Properties
        /// <summary>
        /// List of standard rules applied by the validator
        /// </summary>
        internal List<OWLSemanticsEnums.OWLValidatorStandardRules> StandardRules { get; set; }

        /// <summary>
        /// List of custom rules applied by the validator
        /// </summary>
        internal List<OWLValidatorRule> CustomRules { get; set; }
        #endregion

        #region Ctors
        /// <summary>
        /// Default-ctor to build an empty validator
        /// </summary>
        public OWLValidator()
        {
            StandardRules = new List<OWLSemanticsEnums.OWLValidatorStandardRules>();
            CustomRules = new List<OWLValidatorRule>();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Adds the given standard rule to the validator
        /// </summary>
        public OWLValidator AddStandardRule(OWLSemanticsEnums.OWLValidatorStandardRules standardRule)
        {
            if (!StandardRules.Contains(standardRule))
                StandardRules.Add(standardRule);
            return this;
        }

        /// <summary>
        /// Adds the given custom rule to the validator
        /// </summary>
        public OWLValidator AddCustomRule(OWLValidatorRule customRule)
        {
            if (customRule != null && !this.CustomRules.Any(r => r.ExecuteRule.Equals(customRule.ExecuteRule)))
                CustomRules.Add(customRule);
            return this;
        }

        /// <summary>
        /// Applies the validator on the given ontology
        /// </summary>
        public OWLValidatorReport Validate(OWLOntology ontology)
        {
            OWLValidatorReport validatorReport = new OWLValidatorReport();

            if (ontology != null)
            {
                OWLSemanticsEvents.RaiseSemanticsInfo(string.Format("Validator is going to be applied on Ontology '{0}': this may require intensive processing, depending on size and complexity of domain knowledge and rules", ontology.URI));

                //Standard Rules
                Parallel.ForEach(StandardRules, 
                    standardRule =>
                    {
                        OWLSemanticsEvents.RaiseSemanticsInfo(string.Format("Launching standard rule '{0}'", standardRule));

                        OWLValidatorReport standardRuleReport = new OWLValidatorReport();
                        switch (standardRule)
                        {
                            case OWLSemanticsEnums.OWLValidatorStandardRules.OWL2_AsymmetricProperty:
                                break;
                            case OWLSemanticsEnums.OWLValidatorStandardRules.OWL2_HasKey:
                                break;
                            case OWLSemanticsEnums.OWLValidatorStandardRules.OWL2_IrreflexiveProperty:
                                break;
                            case OWLSemanticsEnums.OWLValidatorStandardRules.OWL2_NegativeAssertions:
                                break;
                            case OWLSemanticsEnums.OWLValidatorStandardRules.OWL2_PropertyChainAxiom:
                                break;
                            case OWLSemanticsEnums.OWLValidatorStandardRules.OWL2_PropertyDisjoint:
                                break;
                            case OWLSemanticsEnums.OWLValidatorStandardRules.OWLDL_Deprecation:
                                break;
                            case OWLSemanticsEnums.OWLValidatorStandardRules.OWLDL_GlobalCardinalityConstraint:
                                break;
                            case OWLSemanticsEnums.OWLValidatorStandardRules.OWLDL_InverseOf:
                                break;
                            case OWLSemanticsEnums.OWLValidatorStandardRules.OWLDL_LocalCardinalityConstraint:
                                break;
                            case OWLSemanticsEnums.OWLValidatorStandardRules.OWLDL_SymmetricProperty:
                                break;
                            case OWLSemanticsEnums.OWLValidatorStandardRules.OWLDL_Vocabulary_Declaration:
                                break;
                            case OWLSemanticsEnums.OWLValidatorStandardRules.OWLDL_Vocabulary_Disjointness:
                                break;
                            case OWLSemanticsEnums.OWLValidatorStandardRules.OWL_ClassType:
                                break;
                            case OWLSemanticsEnums.OWLValidatorStandardRules.RDFS_Domain_Range:
                                break;
                        }
                        validatorReport.MergeEvidences(standardRuleReport);

                        OWLSemanticsEvents.RaiseSemanticsInfo(string.Format("Completed standard rule '{0}': found {1} evidences", standardRule, standardRuleReport.EvidencesCount));
                    });

                //Custom Rules
                Parallel.ForEach(CustomRules, 
                    customRule =>
                    {
                        OWLSemanticsEvents.RaiseSemanticsInfo(string.Format("Launching custom rule '{0}'", customRule.RuleName));

                        OWLValidatorReport customRuleReport = customRule.ExecuteRule(ontology);
                        validatorReport.MergeEvidences(customRuleReport);

                        OWLSemanticsEvents.RaiseSemanticsInfo(string.Format("Completed custom rule '{0}': found {1} evidences", customRule.RuleName, customRuleReport.EvidencesCount));
                    });

                OWLSemanticsEvents.RaiseSemanticsInfo(string.Format("Validator has been applied on Ontology '{0}': found {1] evidences", ontology.URI, validatorReport.EvidencesCount));
            }

            return validatorReport;
        }

        /// <summary>
        /// Asynchronously applies the validator on the given ontology
        /// </summary>
        public Task<OWLValidatorReport> ValidateAsync(this OWLOntology ontology)
            => Task.Run(() => Validate(ontology));
        #endregion
    }
}