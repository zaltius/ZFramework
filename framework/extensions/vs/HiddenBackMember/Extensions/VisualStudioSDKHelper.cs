using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HiddenBackMember.Extensions.VisualStudioSDKHelper
{
    public static class ProjectHelper
    {
        public static async Task<(string path, string text)> GetDocumentTextAsync(this ProjectItem projectItem, string solutionDirectory)
        {
            if (projectItem == null) return (string.Empty, string.Empty);

            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
            string path = await GetFullPathAsync(projectItem.Properties);
            if (string.IsNullOrWhiteSpace(path))
            {
                path = await GetFullPathAsync(projectItem.ContainingProject?.Properties);

                if (string.IsNullOrWhiteSpace(path))
                {
                    path = Path.Combine(solutionDirectory, projectItem.Name);
                }
                else
                {
                    path = Path.Combine(path, projectItem.Name);
                }
            }

            return (path, File.ReadAllText(path));
        }

        public static async Task<ClassProperties> GetItemPropertiesAsync(this UIHierarchyItem uIHierarchy)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

            var file = new ClassProperties();

            var projectItem = uIHierarchy.Object as ProjectItem;

            file.Name = uIHierarchy.Name.Replace(".cs", "");

            var (path, text) = await projectItem.GetDocumentTextAsync(uIHierarchy.Name);

            file.Text = text;

            file.Namespace = file.Text.Replace("\r", "").Split('\n').Where(l => l.StartsWith("namespace")).FirstOrDefault().Replace("namespace ", "").Replace(";", "");

            file.Folder = file.Namespace.Split('.').Last();

            return file;
        }

        public static async Task<Project> GetProjectByNameAsync(this Solution solution, string name)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

            var projects = new List<Project>();

            foreach (Project project in solution.Projects)
            {
                GetSolutionFolderProjects(project, projects);
            }

            return projects.Where(p =>
            {
                ThreadHelper.ThrowIfNotOnUIThread();
                return p.Name == name;
            }).FirstOrDefault();
        }

        public static async Task<IEnumerable<ProjectItem>> GetProjectFilesAsync(this Project project)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

            var projectItems = new List<ProjectItem>();

            foreach (ProjectItem item in project.ProjectItems)
            {
                GetProjectItemFiles(item, projectItems);
            }

            return projectItems;
        }

        public static IEnumerable<ProjectItem> GetProjectItemFiles(ProjectItem projectItem, List<ProjectItem> projectItems = null)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            projectItems = projectItems ?? new List<ProjectItem>();

            if (projectItem.IsKind(ProjectTypes.FOLDER))
            {
                for (var i = 1; i <= projectItem.ProjectItems.Count; i++)
                {
                    var item = projectItem.ProjectItems.Item(i);
                    if (item != null)
                    {
                        GetProjectItemFiles(item, projectItems);
                    }
                }
            }
            else if (projectItem.IsKind(ProjectTypes.FILE))
            {
                projectItems.Add(projectItem);
            }

            return projectItems;
        }

        public static bool IsKind(this Project project, params string[] kindGuids)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            foreach (var guid in kindGuids)
            {
                if (project.Kind.Equals(guid, StringComparison.OrdinalIgnoreCase))
                    return true;
            }

            return false;
        }

        public static bool IsKind(this ProjectItem projectItem, params string[] kindGuids)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            foreach (var guid in kindGuids)
            {
                if (projectItem.Kind.Equals(guid, StringComparison.OrdinalIgnoreCase))
                    return true;
            }

            return false;
        }

        public static void MakeReplacements(ProjectItem projectItem, string token, string replaceWith)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            TextRanges textRanges = null;

            var findOptions = vsFindOptions.vsFindOptionsFromStart |
                vsFindOptions.vsFindOptionsMatchCase |
                vsFindOptions.vsFindOptionsMatchWholeWord;

            var window = projectItem.Open(Constants.vsViewKindTextView);

            var textDocument = window.Document.Object("TextDocument") as TextDocument;

            textDocument.ReplacePattern(token, replaceWith, (int)findOptions, ref textRanges);
        }

        private static async Task<string> GetFullPathAsync(Properties properties)
        {
            try
            {
                await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
                return properties?.Item("FullPath")?.Value?.ToString();
            }
            catch
            {
                return string.Empty;
            }
        }

        private static IEnumerable<Project> GetSolutionFolderProjects(Project project, List<Project> projects = null)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            projects = projects ?? new List<Project>();

            if (project.IsKind(ProjectKinds.vsProjectKindSolutionFolder))
            {
                for (var i = 1; i <= project.ProjectItems.Count; i++)
                {
                    var item = project.ProjectItems.Item(i).SubProject;
                    if (item != null)
                    {
                        GetSolutionFolderProjects(item, projects);
                    }
                }
            }
            else if (project.IsKind(ProjectTypes.CSHARP))
            {
                projects.Add(project);
            }

            return projects;
        }
    }

    public static class ProjectTypes
    {
        public const string ASPNET_5 = "{8BB2217D-0F2D-49D1-97BC-3654ED321F3B}";

        public const string CSHARP = "{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}";

        public const string FILE = "{6BB5F8EE-4483-11D3-8BCF-00C04F8EC28C}";

        public const string FOLDER = "{6BB5F8EF-4483-11D3-8BCF-00C04F8EC28C}";

        public const string MISC = "{66A2671D-8FB5-11D2-AA7E-00C04F688DDE}";

        public const string NODE_JS = "{9092AA53-FB77-4645-B42D-1CCCA6BD08BD}";

        public const string SOLUTION_FOLDER = "{66A26720-8FB5-11D2-AA7E-00C04F688DDE}";

        public const string SSDT = "{00d1a9c2-b5f0-4af3-8072-f6c62b433612}";

        public const string UNIVERSAL_APP = "{262852C6-CD72-467D-83FE-5EEB1973A190}";

        public const string WEBSITE_PROJECT = "{E24C65DC-7377-472B-9ABA-BC803B73C61A}";
    }
}