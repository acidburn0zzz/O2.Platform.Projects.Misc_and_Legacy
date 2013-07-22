// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)

using System.Collections.Generic;
using FluentSharp.CoreLib.Interfaces;
using FluentSharp.CoreLib.Interfaces.J2EE;
using FluentSharp.WinForms.O2Findings;
using O2.Core.FileViewers.JoinTraces;

namespace O2.Core.FileViewers.Ascx.tests
{
    partial class ascx_StrutsMappings_ManualMapping
    {
                                
        private List<IO2Finding> mapStrutsFindings(IStrutsMappings strutsMappingsObject, List<IO2Finding> o2Findings, bool createConsolidatedView)
        {
            

            // calculate findings from strutsMappings
            var strutsFindings = StrutsMappingHelpers.createFindingsFromStrutsMappings(strutsMappingsObject);

            // creates a dictionary with the O2JoinSinks as the key (containing a list of Findings that match that key (i.e. O2LostSink))
            var joinSinksDictionary = OzasmtSearch.getDictionaryWithJoinSinks(strutsFindings);

            // creates a list of findings with the root node as the key (containing a list of Findings that match that key (i.e. root node)) 
            var rootFunctions = new Dictionary<string, List<IO2Finding>>();

            foreach(var o2Finding in o2Findings)
            {
                if (o2Finding.o2Traces.Count > 0)
                {
                    var rootFunction = o2Finding.o2Traces[0].clazz;

                    if (rootFunction.StartsWith("jsp_servlet"))                    
                        rootFunction = rootFunction.Replace("jsp_servlet", "").
                                           Replace("_45_", "-").
                                           Replace(".__", "/").
                                           Replace("._", "/") + ".jsp";                    

                    if (rootFunction != "")
                    {
                        if (false == rootFunctions.ContainsKey(rootFunction))
                            rootFunctions.Add(rootFunction, new List<IO2Finding>());
                        rootFunctions[rootFunction].Add(o2Finding);                        
                    }
                }
                
            }
            
            // now map the JoinSinks with the Root Functions
            if (createConsolidatedView)
                return StrutsMappingHelpers.joinTracesUsingConsolidatedView(joinSinksDictionary, rootFunctions);

            return joinTracesUsingExpandedView(o2Findings, joinSinksDictionary, rootFunctions);
            

            /*foreach (var values in rootFunctions.Values)
                foreach (var o2Finding in values)
                {
                    var modifiedFinding = (O2Finding)OzasmtCopy.createCopy(o2Finding);
                    var currentSource = modifiedFinding.getSource();
                    currentSource.traceType = TraceType.Type_4;
                    modifiedFinding.o2Traces[0].traceType = TraceType.Source;
                    mappedFindings.Add(modifiedFinding);
                }
            */            
        }

        private List<IO2Finding> joinTracesUsingExpandedView(List<IO2Finding> originalO2Findings, Dictionary<string, List<O2Finding>> joinSinksDictionary, Dictionary<string, List<IO2Finding>> rootFunctions)
        {
            var mappedFindings = new List<IO2Finding>();

            var setAttributeFindings = new Dictionary<string, IO2Finding>();
            //foreach(var o2Finding in 

            return mappedFindings;
        }
        
        

        

    }
}
