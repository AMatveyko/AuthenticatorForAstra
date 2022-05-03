using System.Diagnostics;
using Common;
using Common.Debugging;

namespace PamAuthenticator;

internal class UnixObjectCreator
{

    private readonly string _tool;
    private readonly string _objectName;
    private readonly Func<string,bool> _isExist;
    private readonly IDebugger _debugger;

    public UnixObjectCreator(string tool, string objectName, Func<string,bool> checker, IDebugger debugger) =>
        (_tool, _objectName, _isExist, _debugger) = (tool, objectName, checker, debugger);

    public bool IfNeed(string name) => _isExist(name);

    public void Create(string commandParameters) {
        _debugger.Write("Create", $"{_tool} {commandParameters}");
        RunCommand(commandParameters);
    }

    private void RunCommand(string arguments ) {
        var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = _tool,
                Arguments = arguments,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true,
            }
        };
        process.Start();
        
        var outputs = new[]
            {("output", process.StandardOutput.ReadToEnd()), ("error", process.StandardError.ReadToEnd())};
        
        process.WaitForExit();
        
        foreach (var output in outputs) {
            DebugOutputIfNeed(output.Item1, output.Item2);
        }
    }

    private void DebugOutputIfNeed(string type, string output) {
        if (string.IsNullOrWhiteSpace(output)) {
            return;
        }
        _debugger.Write($"{type}:", output);
    }

    public void CheckCreatedObject(string name) {
        if (_isExist(name) == false) {
            _debugger.Write("CheckCreatedObject", $"Filed to create a {_objectName} {name}");
            throw new ApplicationException($"Filed to create a {_objectName} {name}.");
        }
    }
}