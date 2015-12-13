using SPMeta2.Definitions;
using SPMeta2.Spec.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SPMeta2.Spec.Themes
{
    /// <summary>
    /// Base class for spec themes.
    /// Inherit it, define a new 'Name' and fill out 'TemplateFiles' collection
    /// </summary>
    public abstract class ThemeBase
    {
        #region constructors
        public ThemeBase()
        {
            TemplateFiles = new List<ThemeTemplateFile>();
        }
        #endregion

        #region properties

        /// <summary>
        /// The name of the theme
        /// </summary>
        public string Name { get; set; }


        private ThemeTemplateFile mainTemplate;

        /// <summary>
        /// An entry point render template, a root render template
        /// By default looks for the ThemeTemplateFile with Name = 'main'
        /// </summary>
        public virtual ThemeTemplateFile MainTemplate
        {
            get
            {
                if (mainTemplate == null)
                {
                    mainTemplate = Templates.First(t => t.Name == "main");
                }

                return mainTemplate;
            }
            set { mainTemplate = value; }
        }

        public List<ThemeTemplateFile> TemplateFiles { get; set; }

        /// <summary>
        /// Template files with '.cshtml' extension
        /// </summary>
        public virtual List<ThemeTemplateFile> Templates
        {
            get
            {
                return TemplateFiles.Where(t => t.Extension == ".cshtml")
                                    .OrderBy(t => t.Name)
                                    .ToList();
            }
            set { }
        }

        /// <summary>
        /// Template files with '.css' extension
        /// </summary>
        public virtual List<ThemeTemplateFile> Css
        {
            get
            {
                return TemplateFiles.Where(t => t.Extension == ".css")
                                    .OrderBy(t => t.Name)
                                    .ToList();
            }
            set { }
        }

        /// <summary>
        /// Template files with '.js' extension
        /// </summary>
        public virtual List<ThemeTemplateFile> Js
        {
            get
            {
                return TemplateFiles.Where(t => t.Extension == ".js")
                                    .OrderBy(t => t.Name)
                                    .ToList();
            }
            set { }
        }

        #endregion

        #region methods

        protected Type GetIEnumerableType(IEnumerable type)
        {
            if (type.GetType().GetGenericArguments().Count() > 0)
            {
                return type.GetType().GetGenericArguments()[0];
            }

            return null;
        }

        public string GetCustomViewForValue(object value)
        {
            return GetCustomViewForValue(null, value);
        }

        protected virtual string GetCustomViewForDefinitionPropertyValueViewModel(
                DefinitionPropertyValueViewModel contextValue,
                object value)
        {
            if (value is bool)
            {
                var typedView = GetTemplateFileByName("Type-Bool");

                if (typedView != null)
                    return typedView.Name;
            }

            var prop = contextValue as DefinitionPropertyValueViewModel;

            if (prop.DefinitionType == typeof(ListDefinition))
            {
                if (prop.Name == "TemplateType")
                {
                    var typedView = GetTemplateFileByName("Type-ListDefinition-TemplateType");

                    if (typedView != null)
                        return typedView.Name;
                }
            }

            if (prop.DefinitionType == typeof(ContentTypeDefinition))
            {
                if (prop.Name == "ParentContentType")
                {
                    var typedView = GetTemplateFileByName("Type-ContentTypeDefinition-ParentContentType");

                    if (typedView != null)
                        return typedView.Name;
                }
            }

            if (prop.DefinitionType == typeof(WebPartPageDefinition) && prop.Name == "CustomPageLayout")
            {
                var typedView = GetTemplateFileByName("Type-WebPartPageDefinition-CustomPageLayout");

                if (typedView != null)
                    return typedView.Name;
            }

            return string.Empty;
        }

        protected virtual string GetCustomViewForSimpleValue(object contextValue, object value)
        {
            if (value is byte[])
            {
                var typedView = GetTemplateFileByName("Type-Array-Byte");

                if (typedView != null)
                    return typedView.Name;
            }

            if (!(value is string)
                && !(value is byte[])
                && (value is IEnumerable))
            {
                var type = GetIEnumerableType(value as IEnumerable);

                var typedView = GetTemplateFileByName("Type-Enumerable-" + type.Name);

                if (typedView != null)
                    return typedView.Name;
            }

            return string.Empty;
        }

        public virtual string GetCustomViewForValue(object contextValue, object value)
        {
            if (value != null)
            {
                if (contextValue is DefinitionPropertyValueViewModel)
                {
                    var typedViewName = GetCustomViewForDefinitionPropertyValueViewModel(
                                                contextValue as DefinitionPropertyValueViewModel,
                                                value);

                    if (!string.IsNullOrEmpty(typedViewName))
                        return typedViewName;
                }

                var simpleTypeViewName = GetCustomViewForSimpleValue(contextValue, value);

                if (!string.IsNullOrEmpty(simpleTypeViewName))
                    return simpleTypeViewName;
            }

            return string.Empty;
        }

        public bool DoesValueHaveCustomView(object viewModelValue)
        {
            return DoesValueHaveCustomView(null, viewModelValue);
        }

        public bool DoesValueHaveCustomView(object viewModel, object viewModelValue)
        {
            var viewContent = GetCustomViewForValue(viewModel, viewModelValue);

            return !string.IsNullOrEmpty(viewContent);
        }

        protected virtual ThemeTemplateFile GetTemplateFileByName(string name)
        {
            return TemplateFiles.FirstOrDefault(r => r.Name.ToLower() == name.ToLower());
        }

        #endregion

        #region static

        public static List<ThemeTemplateFile> FromResourceFolder(Assembly assembly, string resourceFolderPath)
        {
            var result = new List<ThemeTemplateFile>();

            var resourceNames = assembly.GetManifestResourceNames();
            var themeResourceNames = resourceNames.Where(n => n.StartsWith(resourceFolderPath + ".", true, CultureInfo.InvariantCulture));

            foreach (var resourceName in themeResourceNames)
            {
                var fileName = string.Empty;

                var rawLocation = resourceName.Replace(resourceFolderPath + ".", string.Empty);
                var fileParts = rawLocation.Split('.').ToList();

                if (fileParts.Count > 2)
                {
                    // default
                    fileName = string.Format("{0}.{1}", fileParts[fileParts.Count - 2], fileParts[fileParts.Count - 1]);

                    // under _ folder, all items that starts with "_" are folders
                    var folderItem = fileParts.FirstOrDefault(f => f.StartsWith("_"));

                    if (folderItem != null)
                    {
                        var index = fileParts.IndexOf(folderItem);
                        fileName = string.Join(".", fileParts.Skip(index + 1));
                    }
                }
                else
                    fileName = string.Format("{0}.{1}", fileParts[0], fileParts[1]);

                var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
                var extension = Path.GetExtension(fileName).ToLower();

                using (var contentStream = assembly.GetManifestResourceStream(resourceName))
                {
                    using (var contentStreamReader = new StreamReader(contentStream))
                    {
                        var content = contentStreamReader.ReadToEnd();

                        var template = new ThemeTemplateFile()
                        {
                            Name = fileNameWithoutExtension,
                            Content = content,
                            Extension = extension
                        };

                        result.Add(template);
                    }
                }
            }

            return result;
        }

        #endregion
    }
}
