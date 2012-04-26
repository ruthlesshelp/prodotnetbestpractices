namespace Lender.Build.StyleCop.Tasks
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.Build.Framework;
    using Microsoft.Build.Utilities;

    using global::StyleCop;

    /// <summary>
    /// Custom MSBuild task for StyleCop to enable scenarios that are not possible with the default MSBuild task provided with the tool.
    /// This code is based on the StyleCop SDK documentation's article "Creating a Custom MSBuild Task for StyleCop".
    /// </summary>
    public sealed class StyleCopAnalysis : Task
    {
        private const string MSBuildErrorCode = null;
        private const string MSBuildSubCategory = null;
        private const int DefaultViolationLimit = 10000;

        private ITaskItem[] inputSourceFiles = new ITaskItem[0];
        private ITaskItem inputProjectFullPath;
        private ITaskItem[] inputAdditionalAddinPaths = new ITaskItem[0];
        private bool inputForceFullAnalysis;
        private string[] inputDefineConstants = new string[0];
        private bool inputTreatErrorsAsWarnings;
        private bool inputCacheResults;
        private ITaskItem inputOverrideSettingsFile;
        private ITaskItem outputFile;
        private ITaskItem maxViolationCount;

        private bool succeeded = true;
        private int violationCount;
        private int violationLimit;

        public StyleCopAnalysis()
        {
            this.ForceFullAnalysis = true;
        }

        public ITaskItem[] SourceFiles
        {
            get { return this.inputSourceFiles; }
            set { this.inputSourceFiles = value; }
        }

        public ITaskItem ProjectFullPath
        {
            get { return this.inputProjectFullPath; }
            set { this.inputProjectFullPath = value; }
        }

        public ITaskItem[] AdditionalAddinPaths
        {
            get { return this.inputAdditionalAddinPaths; }
            set { this.inputAdditionalAddinPaths = value; }
        }

        public bool ForceFullAnalysis
        {
            get { return this.inputForceFullAnalysis; }
            set { this.inputForceFullAnalysis = value; }
        }

        public string[] DefineConstants
        {
            get { return this.inputDefineConstants; }
            set { this.inputDefineConstants = value; }
        }

        public bool TreatErrorsAsWarnings
        {
            get { return this.inputTreatErrorsAsWarnings; }
            set { this.inputTreatErrorsAsWarnings = value; }
        }

        public bool CacheResults
        {
            get { return this.inputCacheResults; }
            set { this.inputCacheResults = value; }
        }

        public ITaskItem OverrideSettingsFile
        {
            get { return this.inputOverrideSettingsFile; }
            set { this.inputOverrideSettingsFile = value; }
        }

        public ITaskItem OutputFile
        {
            get { return this.outputFile; }
            set { this.outputFile = value; }
        }

        public ITaskItem MaxViolationCount
        {
            get { return this.maxViolationCount; }
            set { this.maxViolationCount = value; }
        }

        public bool IgnoreVoluntaryRules { get; set; }

        public string[] MandatoryRuleIds { get; set; }

        public string[] AlwaysIgnoredRuleIds { get; set; }

        public bool ThrowOnError { get; set; }

        public override bool Execute()
        {
            // Clear the violation count and set the violation limit for the project.
            this.violationCount = 0;
            this.violationLimit = 0;

            if (this.maxViolationCount != null)
            {
                int.TryParse(this.maxViolationCount.ItemSpec, out this.violationLimit);
            }

            if (this.violationLimit == 0)
            {
                this.violationLimit = DefaultViolationLimit;
            }

            Log.LogMessage(MessageImportance.Low, "ViolationLimit: '{0}'", this.violationLimit);

            // Get settings files (if null or empty use null filename so it uses right default).
            string overrideSettingsFileName = null;
            if (this.inputOverrideSettingsFile != null && this.inputOverrideSettingsFile.ItemSpec.Length > 0)
            {
                overrideSettingsFileName = this.inputOverrideSettingsFile.ItemSpec;
            }

            Log.LogMessage(MessageImportance.Low, "OverrideSettingsFile: '{0}'", overrideSettingsFileName ?? "(none)");

            Log.LogMessage(MessageImportance.Low, "CacheResults: '{0}'", CacheResults);

            var outputFile = OutputFile == null ? null : OutputFile.ItemSpec;
            Log.LogMessage(MessageImportance.Low, "OutputFile: '{0}'", outputFile ?? "(none)");

            // Get addin paths.
            List<string> addinPaths = new List<string>();
            foreach (ITaskItem addinPath in this.inputAdditionalAddinPaths)
            {
                addinPaths.Add(addinPath.GetMetadata("FullPath"));
            }

            if (addinPaths.Count > 0)
            {
                Log.LogMessage(MessageImportance.Low, "AdditionalAddins:");
                foreach (var addinPath in addinPaths)
                {
                    Log.LogMessage(MessageImportance.Low, "\t'{0}'", addinPath);
                }
            }

            if (this.IgnoreVoluntaryRules)
            {
                Log.LogMessage(MessageImportance.Low, "IgnoreVoluntaryRules: True");
            }

            if (this.AlwaysIgnoredRuleIds != null)
            {
                Log.LogMessage(MessageImportance.Low, "AlwaysIgnoredRuleIds:");
                foreach (var ruleId in this.AlwaysIgnoredRuleIds)
                {
                    Log.LogMessage(MessageImportance.Low, "\t'{0}'", ruleId);
                }
            }

            if (this.MandatoryRuleIds != null)
            {
                Log.LogMessage(MessageImportance.Low, "MandatoryRuleIds:");
                foreach (var ruleId in this.MandatoryRuleIds)
                {
                    Log.LogMessage(MessageImportance.Low, "\t'{0}'", ruleId);
                }
            }

            // Create the StyleCop console.
            StyleCopConsole console = new StyleCopConsole(
                overrideSettingsFileName,
                this.inputCacheResults,
                this.outputFile == null ? null : this.outputFile.ItemSpec,
                addinPaths,
                true);

            // Create the configuration.
            Configuration configuration = new Configuration(this.inputDefineConstants);

            Log.LogMessage(MessageImportance.Low, "ProjectFullPath: '{0}'", ProjectFullPath == null ? "(null)" : ProjectFullPath.ItemSpec ?? "(none)");

            if (this.inputProjectFullPath != null && this.inputProjectFullPath.ItemSpec != null)
            {
                // Add each source file to this project.
                if (SourceFiles != null && SourceFiles.Length > 0)
                {
                    Log.LogMessage(MessageImportance.Low, "SourceFiles:");
                    foreach (var sourceFile in SourceFiles)
                    {
                        Log.LogMessage(
                            MessageImportance.Low,
                            "\t'{0}'",
                            sourceFile == null ? "(null)" : sourceFile.ItemSpec ?? "(none)");
                    }
                }

                Log.LogMessage(MessageImportance.Low, "ForceFullAnalysis: '{0}'", ForceFullAnalysis);

                // Create a CodeProject object for these files.
                CodeProject project = new CodeProject(
                    this.inputProjectFullPath.ItemSpec.GetHashCode(),
                    this.inputProjectFullPath.ItemSpec,
                    configuration);

                // Add each source file to this project.
                if (SourceFiles != null)
                {
                    foreach (ITaskItem inputSourceFile in this.inputSourceFiles)
                    {
                        console.Core.Environment.AddSourceCode(project, inputSourceFile.ItemSpec, null);
                    }
                }

                try
                {
                    // Subscribe to events
                    console.OutputGenerated += this.OnOutputGenerated;
                    console.ViolationEncountered += this.OnViolationEncountered;

                    // Analyze the source files
                    CodeProject[] projects = new CodeProject[] { project };
                    console.Start(projects, this.inputForceFullAnalysis);
                }
                catch (Exception exception)
                {
                    Log.LogError(string.Format("{0}", exception.Message));

                    if (ThrowOnError) throw;

                    return false;
                }
                finally
                {
                    // Unsubscribe from events
                    console.OutputGenerated -= this.OnOutputGenerated;
                    console.ViolationEncountered -= this.OnViolationEncountered;
                }
            }

            return this.succeeded;
        }

        private void OnViolationEncountered(object sender, ViolationEventArgs e)
        {
            if (this.IsViolationIgnored(e))
            {
                return;
            }

            if (this.violationLimit >= 0 && this.violationCount >= this.violationLimit)
            {
                return;
            }

            this.violationCount++;

            // Does the violation qualify for breaking the build?
            if (!(e.Warning || this.inputTreatErrorsAsWarnings))
            {
                this.succeeded = false;
            }

            string path = string.Empty;
            if (e.SourceCode != null && !string.IsNullOrEmpty(e.SourceCode.Path))
            {
                path = e.SourceCode.Path;
            }
            else if (e.Element != null &&
                     e.Element.Document != null &&
                     e.Element.Document.SourceCode != null &&
                     e.Element.Document.SourceCode.Path != null)
            {
                path = e.Element.Document.SourceCode.Path;
            }

            // Prepend the rule check-id to the message.
            string message = string.Concat(e.Violation.Rule.CheckId, ": ", e.Message);

            lock (this)
            {
                if (e.Warning || this.inputTreatErrorsAsWarnings)
                {
                    this.Log.LogWarning(MSBuildSubCategory, MSBuildErrorCode, null, path, e.LineNumber, 1, 0, 0, message);
                }
                else
                {
                    this.Log.LogError(MSBuildSubCategory, MSBuildErrorCode, null, path, e.LineNumber, 1, 0, 0, message);
                }
            }
        }

        private bool IsViolationIgnored(ViolationEventArgs e)
        {
            if (e == null) throw new ArgumentNullException("e");

            var checkId = e.Violation.Rule.CheckId;

            var alwaysIgnoredRuleIds =
                this.AlwaysIgnoredRuleIds ?? new string[0];
            var mandatoryRuleIds =
                this.MandatoryRuleIds ?? new string[0];

            var ignore = this.IgnoreVoluntaryRules;
            if (!ignore)
            {
                if (alwaysIgnoredRuleIds.Any(c => checkId == c))
                {
                    ignore = true;
                }
            }

            if (ignore)
            {
                // Do not ignore a mandatory rule
                if (mandatoryRuleIds.Any(c => checkId == c))
                {
                    ignore = false;
                }
            }

            return ignore;
        }

        private void OnOutputGenerated(object sender, OutputEventArgs e)
        {
            lock (this)
            {
                Log.LogMessage(e.Output.Trim());
            }
        }
    }
}
