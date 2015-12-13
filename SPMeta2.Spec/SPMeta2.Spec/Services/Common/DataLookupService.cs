using System;
using System.Collections.Generic;
using SPMeta2.Definitions;
using SPMeta2.Enumerations;
using SPMeta2.Extensions;
using SPMeta2.Models;
using SPMeta2.Standard.Enumerations;
using SPMeta2.Syntax.Default;
using SPMeta2.Utils;

namespace SPMeta2.Spec.Services.Common
{
    public class DataLookupService
    {
        #region constructors

        #endregion

        #region static

        static DataLookupService()
        {
            KnownContentTypes = new List<ContentTypeLookupDefinition>();

            foreach (var p in typeof(BuiltInContentTypeId).GetFields())
            {
                var name = p.Name;
                var value = p.GetValue(new object[] { });

                KnownContentTypes.Add(new ContentTypeLookupDefinition
                {
                    Id = value.ToString().ToLower(),
                    Name = name
                });
            }

            foreach (var p in typeof(BuiltInPublishingContentTypeId).GetFields())
            {
                var name = p.Name;
                var value = p.GetValue(new object[] { });

                KnownContentTypes.Add(new ContentTypeLookupDefinition
                {
                    Id = value.ToString().ToLower(),
                    Name = name
                });
            }


            KnownLists = new List<ListLookupDefinition>();

            foreach (var p in typeof(BuiltInListTemplateTypeId).GetFields())
            {
                var name = p.Name;
                var value = p.GetValue(new object[] { });

                KnownLists.Add(new ListLookupDefinition
                {
                    TemplateTypeName = name,
                    TemplateType = (int)value
                });
            }
        }

        public static List<ContentTypeLookupDefinition> KnownContentTypes { get; set; }
        public static List<ListLookupDefinition> KnownLists { get; set; }

        #endregion

        #region properties

        public List<ModelNode> Models { get; set; }

        #endregion

        #region methods

        public virtual string LookupListTemplateTypeName(int templateType)
        {
            if (templateType <= 0)
                return String.Empty;

            // known list?
            foreach (var list in KnownLists)
            {
                if (list.TemplateType == templateType)
                    return list.TemplateTypeName;
            }

            return templateType.ToString();
        }

        public virtual string LookupParentContentTypeName(string contentTypeId)
        {
            return LookupParentContentTypeName(Models, contentTypeId);
        }

        public virtual string LookupParentContentTypeName(IEnumerable<ModelNode> models, string contentTypeId)
        {
            if (string.IsNullOrEmpty(contentTypeId))
                return string.Empty;

            contentTypeId = contentTypeId.ToLower();

            // known content type?
            foreach (var contentType in KnownContentTypes)
            {
                var childId = contentTypeId.Replace(contentType.Id.ToLower() + "00", string.Empty);

                if (ConvertUtils.ToGuid(childId).HasGuidValue())
                {
                    return contentType.Name;
                }
            }

            if (models != null)
            {
                var modelContentTypes = new List<ContentTypeDefinition>();

                foreach (var m in models)
                {
                    m.WithNodesOfType<ContentTypeDefinition>(n =>
                    {
                        modelContentTypes.Add(n.Value as ContentTypeDefinition);
                    });
                }

                foreach (var modelContentType in modelContentTypes)
                {
                    var childId = contentTypeId.Replace(modelContentType.GetContentTypeId().ToLower() + "00", string.Empty);

                    if (ConvertUtils.ToGuid(childId).HasGuidValue())
                    {
                        return modelContentType.Name;
                    }
                }
            }

            return string.Empty;
        }

        #endregion
    }
}
