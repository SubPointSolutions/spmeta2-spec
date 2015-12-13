using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using SPMeta2.Definitions;
using SPMeta2.Models;
using SPMeta2.Utils;
using SPMeta2.Definitions.Base;

namespace SPMeta2.Spec.ViewModels
{
    public class DefinitionViewModel : ViewModelBase
    {
        #region constructors

        public DefinitionViewModel()
        {
            Definitions = new List<DefinitionViewModel>();
        }

        #endregion

        #region properties

        public virtual string Title
        {
            get
            {
                var prop = RawDefinition.PropertyBag.FirstOrDefault(p => p.Name == "m2-spec-model-name");

                if (prop != null)
                    return ConvertUtils.ToStringAndTrim(prop.Value);

                prop = RawModelNode.PropertyBag.FirstOrDefault(p => p.Name == "m2-spec-model-name");

                if (prop != null)
                    return ConvertUtils.ToStringAndTrim(prop.Value);

                return RawDefinition.GetType().Name.Replace("Definition", String.Empty) + " Model";
            }
            set { }
        }

        public virtual string Description
        {
            get
            {
                var prop = RawDefinition.PropertyBag.FirstOrDefault(p => p.Name == "m2-spec-model-description");

                if (prop != null)
                    return ConvertUtils.ToStringAndTrim(prop.Value);

                prop = RawModelNode.PropertyBag.FirstOrDefault(p => p.Name == "m2-spec-model-description");

                if (prop != null)
                    return ConvertUtils.ToStringAndTrim(prop.Value);

                return string.Empty;
            }
            set { }
        }

        public virtual DefinitionViewModel Parent { get; set; }

        public virtual ModelNode RawModelNode { get; set; }
        public virtual DefinitionBase RawDefinition { get; set; }


        public virtual List<DefinitionViewModel> Definitions { get; set; }

        private List<DefinitionGroupViewModel> groups;

        public virtual List<DefinitionGroupViewModel> Groups
        {
            get
            {
                if (groups == null)
                {
                    groups = new List<DefinitionGroupViewModel>();
                    FillGroups(groups, Definitions);
                }

                return groups;
            }
            set { }
        }

        public override bool IsExpandable
        {
            get
            {
                if (RawDefinition is ListDefinition)
                    return true;

                if (RawDefinition is FolderDefinition)
                    return true;

                if (RawDefinition is PageDefinitionBase)
                    return true;

                if (RawDefinition is ListItemDefinition)
                    return true;

                if (RawDefinition is ModuleFileDefinition)
                    return true;

                if (RawDefinition is WebDefinition)
                    return true;

                if (RawDefinition is WebPartDefinitionBase)
                    return true;

                if (RawDefinition is NavigationNodeDefinitionBase)
                    return true;

                return false;
            }
        }

        public virtual List<DefinitionViewModel> ExpandableDefinitions
        {
            get
            {
                return Definitions.Where(d => d.IsExpandable).ToList();
            }
        }

        public virtual List<DefinitionPropertyValueViewModel> PropertyValues
        {
            get
            {
                var result = new List<DefinitionPropertyValueViewModel>();

                var props = RawDefinition.GetType().GetProperties();

                foreach (var prop in props)
                {
                    if (prop.Name == "PropertyBag")
                        continue;

                    var propValue = new DefinitionPropertyValueViewModel();

                    propValue.DefinitionType = RawDefinition.GetType();

                    propValue.Name = prop.Name;
                    propValue.Value = ReflectionUtils.GetPropertyValue(RawDefinition, prop.Name);

                    result.Add(propValue);
                }

                result = result.OrderBy(d => d.Title).ToList();


                return result;
            }
        }

        public virtual string ExpandableTitle
        {
            get
            {
                if (RawDefinition is ListDefinition)
                    return (RawDefinition as ListDefinition).Title;

                if (RawDefinition is FolderDefinition)
                    return (RawDefinition as FolderDefinition).Name;

                if (RawDefinition is PageDefinitionBase)
                    return (RawDefinition as PageDefinitionBase).FileName;

                if (RawDefinition is ListItemDefinition)
                    return (RawDefinition as ListItemDefinition).Title;

                if (RawDefinition is ModuleFileDefinition)
                    return (RawDefinition as ModuleFileDefinition).FileName;

                if (RawDefinition is WebDefinition)
                    return (RawDefinition as WebDefinition).Title;

                if (RawDefinition is WebPartDefinitionBase)
                {
                    var defType = RawDefinition.GetType().Name.Replace("Definition", String.Empty);
                    var title = (RawDefinition as WebPartDefinitionBase).Title;

                    return string.Format("{0} ({1})", title, defType);
                }

                if (RawDefinition is NavigationNodeDefinitionBase)
                    return (RawDefinition as NavigationNodeDefinitionBase).Title;

                return RawDefinition.GetType().Name.Replace("Definition", String.Empty);
            }
        }

        #endregion

        #region methods

        protected virtual void FillGroups(List<DefinitionGroupViewModel> result, List<DefinitionViewModel> definitions)
        {
            var groups = definitions.GroupBy(g =>
            {
                var type = g.RawDefinition.GetType();

                if (typeof(FieldDefinition).IsAssignableFrom(type))
                    return typeof(FieldDefinition);

                return type;
            });

            foreach (var group in groups)
            {
                var key = group.Key;
                var groupViewModel = new DefinitionGroupViewModel();

                var groupName = key.Name
                    .Replace("Definition", string.Empty)
                    .Trim();

                groupName = SpecServiceFactory.Istance.DisplayService.Pluralize(groupName);
                groupName = SpecServiceFactory.Istance.DisplayService.ToTitleCase(groupName);

                groupViewModel.Config = Config;

                groupViewModel.Title = groupName;
                groupViewModel.Definitions = group.ToList();

                var mapping = Config.DefinitionColumnsConfig.FirstOrDefault(c => c.TargetType == key);

                if (mapping != null)
                {
                    foreach (var exp in mapping.Headers)
                    {
                        var typedExpression = exp as LambdaExpression;

                        if (typedExpression != null)
                        {
                            var propName = string.Empty;

                            if (typedExpression.Body is MemberExpression)
                            {
                                propName = (typedExpression.Body as MemberExpression).Member.Name;
                            }
                            else if (typedExpression.Body is UnaryExpression)
                            {
                                propName =
                                    ((typedExpression.Body as UnaryExpression).Operand as MemberExpression).Member.Name;
                            }
                            else if (typedExpression.Body is ConstantExpression)
                            {
                                var propNameValue = typedExpression.Compile().DynamicInvoke(new object[] { null });

                                if (propNameValue != null)
                                {
                                    propName = propNameValue.ToString();
                                }
                            }

                            var headerViewModel = new DefinitionPropertyTitleViewModel
                            {
                                Name = propName,
                                DefinitionType = key
                            };

                            groupViewModel.Headers.Add(headerViewModel);
                        }
                    }

                    foreach (var def in groupViewModel.Definitions)
                    {
                        var values = new List<DefinitionPropertyValueViewModel>();

                        foreach (var exp in mapping.Values)
                        {
                            //var headerDisplayName = groupViewModel.Headers[mapping.Values.IndexOf(exp)].DisplayName;
                            var headerName = groupViewModel.Headers[mapping.Values.IndexOf(exp)].Name;

                            var typedExpression = exp as LambdaExpression;

                            if (typedExpression != null)
                            {
                                var c = typedExpression.Compile();
                                var value = c.DynamicInvoke(new[] { def.RawDefinition });

                                var valueViewModel = new DefinitionPropertyValueViewModel
                                {
                                    Name = headerName,
                                    DefinitionType = key,
                                    Value = value
                                };

                                values.Add(valueViewModel);
                            }
                        }

                        groupViewModel.Values.Add(def, values);
                    }
                }
                else
                {
                    foreach (var propName in Config.DefaultDefinitionProperties)
                    {
                        var headerName = SpecServiceFactory.Istance.DisplayService.ToTitleCase(propName);

                        var tmpDef = groupViewModel.Definitions.First().RawDefinition;
                        var hasProp = ReflectionUtils.HasProperty(tmpDef, propName);

                        if (hasProp)
                        {
                            var headerViewModel = new DefinitionPropertyTitleViewModel
                            {
                                Name = propName,
                                DefinitionType = key
                            };

                            groupViewModel.Headers.Add(headerViewModel);
                        }
                    }


                    foreach (var def in groupViewModel.Definitions)
                    {
                        var values = new List<DefinitionPropertyValueViewModel>();

                        foreach (var propName in Config.DefaultDefinitionProperties)
                        {
                            var hasProp = ReflectionUtils.HasProperty(def.RawDefinition, propName);

                            if (hasProp)
                            {
                                var value = ReflectionUtils.GetPropertyValue(def.RawDefinition, propName);

                                var valueViewModel = new DefinitionPropertyValueViewModel
                                {
                                    Name = propName,
                                    DefinitionType = key,
                                    Value = value
                                };

                                values.Add(valueViewModel);
                            }
                        }

                        groupViewModel.Values.Add(def, values);
                    }
                }

                // s

                //// ID, Title, Name

                //var propSet = new List<Expression<Func<object, string>>>();

                ////propSet.Add(o => { })

                //if (key == typeof(FeatureDefinition))
                //{
                //    groupViewModel.Headers.Add("Title");
                //    groupViewModel.Headers.Add("Scope");
                //    groupViewModel.Headers.Add("Enable");
                //    groupViewModel.Headers.Add("ForceActivate");

                //    foreach (var def in groupViewModel.Definitions)
                //    {
                //        var typedDef = def.RawDefinition as FeatureDefinition;
                //        var values = new List<object>();

                //        values.Add(typedDef.Title);
                //        values.Add(typedDef.Scope);
                //        values.Add(typedDef.Enable);
                //        values.Add(typedDef.ForceActivate);

                //        groupViewModel.Values.Add(def, values);
                //    }
                //}



                result.Add(groupViewModel);
            }
        }

        #endregion
    }
}
