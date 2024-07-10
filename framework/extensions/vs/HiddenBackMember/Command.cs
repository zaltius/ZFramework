using EnvDTE;
using EnvDTE100;
using EnvDTE80;
using HiddenBackMember.Extensions.VisualStudioSDKHelper;
using Microsoft.VisualStudio.Shell;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using Task = System.Threading.Tasks.Task;

namespace HiddenBackMember
{
    /// <summary>
    /// Command handler
    /// </summary>
    internal sealed class Command
    {
        /// <summary>
        /// Command ID.
        /// </summary>
        public const int CommandId = 0x0100;

        /// <summary>
        /// Command menu group (command set GUID).
        /// </summary>
        public static readonly Guid CommandSet = new Guid("e4ee56b2-51a0-4102-8bc4-96ee4f8dfce9");

        /// <summary>
        /// VS Package that provides this command, not null.
        /// </summary>
        private readonly AsyncPackage package;

        /// <summary>
        /// Gets the instance of the command.
        /// </summary>
        public static Command Instance
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the service provider from the owner package.
        /// </summary>
        private Microsoft.VisualStudio.Shell.IAsyncServiceProvider ServiceProvider
        {
            get
            {
                return this.package;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Command"/> class.
        /// Adds our command handlers for menu (commands must exist in the command table file)
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        /// <param name="commandService">Command service to add command to, not null.</param>
        private Command(AsyncPackage package, OleMenuCommandService commandService)
        {
            this.package = package ?? throw new ArgumentNullException(nameof(package));
            commandService = commandService ?? throw new ArgumentNullException(nameof(commandService));

            var menuCommandID = new CommandID(CommandSet, CommandId);
            var menuItem = new MenuCommand(this.Execute, menuCommandID);
            commandService.AddCommand(menuItem);
        }

        /// <summary>
        /// Initializes the singleton instance of the command.
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        public static async Task InitializeAsync(AsyncPackage package)
        {
            // Switch to the main thread - the call to AddCommand in Command's constructor requires
            // the UI thread.
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);

            OleMenuCommandService commandService = await package.GetServiceAsync(typeof(IMenuCommandService)) as OleMenuCommandService;
            Instance = new Command(package, commandService);
        }

        private static async Task ApplyTemplateAsync(
                    Solution solution,
                    string className,
                    string projectBaseName,
                    string projectLayer,
                    List<ProjectItem> projectItems)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

            var project = await solution.GetProjectByNameAsync($"{projectBaseName}.{projectLayer}");
            try
            {
                if (project != null)
                {
                    string templatePath = ((Solution4)solution).GetProjectItemTemplate($"Z{projectLayer}.zip", "CSharp");
                    project.ProjectItems.AddFromTemplate($"{templatePath}", className);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{projectLayer}: " + ex);
            }

            projectItems.AddRange(await project.GetProjectFilesAsync());
        }

        /// <summary>
        /// This function is the callback used to execute the command when the menu item is clicked.
        /// See the constructor to see how the menu item is associated with this function using
        /// OleMenuCommandService service and MenuCommand class.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event args.</param>
        private async void Execute(object sender, EventArgs e)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);

            var dte = await package.GetServiceAsync(typeof(DTE)).ConfigureAwait(false) as DTE2;

            UIHierarchy uih = (UIHierarchy)dte.Windows.Item(Constants.vsWindowKindSolutionExplorer).Object;

            Array selectedItems = (Array)uih.SelectedItems;

            var selectedItem = (UIHierarchyItem)selectedItems.GetValue(0);

            var classProperties = await selectedItem.GetItemPropertiesAsync();

            var projectObject = selectedItem.Object as ProjectItem;

            var projectBaseName = projectObject.ContainingProject.Name.Substring(0, projectObject.ContainingProject.Name.Length - (projectObject.ContainingProject.Name.Length - projectObject.ContainingProject.Name.LastIndexOf(".")));

            var projectItems = new List<ProjectItem>();

            try
            {
                await ApplyTemplateAsync(dte.Solution, classProperties.Name, projectBaseName, "Domain", projectItems);

                await ApplyTemplateAsync(dte.Solution, classProperties.Name, projectBaseName, "Infrastructure", projectItems);

                await ApplyTemplateAsync(dte.Solution, classProperties.Name, projectBaseName, "Application", projectItems);

                await ApplyTemplateAsync(dte.Solution, classProperties.Name, projectBaseName, "API", projectItems);

                try
                {
                    projectItems = projectItems.Where(pi =>
                    {
                        ThreadHelper.ThrowIfNotOnUIThread();
                        return pi.Name.Contains(classProperties.Name);
                    }).ToList();

                    foreach (var projectItem in projectItems)
                    {
                        ProjectHelper.MakeReplacements(projectItem, "$entity$", classProperties.Name);
                        ProjectHelper.MakeReplacements(projectItem, "#folder#", classProperties.Folder);
                        ProjectHelper.MakeReplacements(projectItem, "#projectname#", projectBaseName);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}