using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using WolvenKit.Common.Interfaces;

namespace WolvenKit.Modkit.RED4;

public class Reporter : ITaskReporter
{
    private readonly string reportPath;
    private ConcurrentDictionary<string, string> errors = new ConcurrentDictionary<string, string>();
    private ConcurrentDictionary<string, string> warnings = new ConcurrentDictionary<string, string>();


    public Reporter(string reportPath){
        this.reportPath = reportPath;
    }

    public void ReportError(string entryPath, string stacktrace)
    {
        if(reportPath == "") 
            return;
        
        if(stacktrace != ""){
            errors[entryPath] = stacktrace;
        } else {
            if(!errors.ContainsKey(entryPath)){
                errors[entryPath] = "";
            }
        }
    }

    public void ReportWarning(string entryPath, string warning)
    {
        if (reportPath == "")
            return;

        warnings[entryPath] = warning;
    }
    
    public void WriteReport()
    {

        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
            DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        var jsonString = JsonSerializer.Serialize(errors, options);
        File.WriteAllText(reportPath, jsonString);

    }

}
