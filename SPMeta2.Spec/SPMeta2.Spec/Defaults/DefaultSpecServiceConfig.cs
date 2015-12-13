using SPMeta2.Spec.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPMeta2.Definitions;
using SPMeta2.Definitions.ContentTypes;
using SPMeta2.Spec.Themes;
using SPMeta2.Syntax.Default;

namespace SPMeta2.Spec.Defaults
{
    public class DefaultSpecServiceConfig : SpecConfig
    {
        #region constructors

        public DefaultSpecServiceConfig()
        {
            DefinitionColumnsConfig = new List<SpecTypedItemConfig>();
            DefaultDefinitionProperties = new List<string>();

            DefaultDefinitionProperties.Add("Title");
            DefaultDefinitionProperties.Add("Name");
            DefaultDefinitionProperties.Add("FileName");
            DefaultDefinitionProperties.Add("InternalName");
            DefaultDefinitionProperties.Add("Description");
            DefaultDefinitionProperties.Add("Id");
            DefaultDefinitionProperties.Add("Url");

            Theme = new DefaultTheme();

            this
                .MapColumns<UniqueContentTypeOrderDefinition>(def =>
                {
                    def.Column(h => h.ContentTypes, h => h.ContentTypes);
                })
                .MapColumns<ContentTypeLinkDefinition>(def =>
                {
                    def.Column(h => h.ContentTypeName, h => h.ContentTypeName);
                    def.Column(h => h.ContentTypeId, h => h.ContentTypeId);
                })
               .MapColumns<ContentTypeDefinition>(def =>
               {
                   def.Column(h => h.Name, h => h.Name);
                   def.Column(h => h.Description, h => h.Description);
                   def.Column(d => "ParentContentType", d => d.GetContentTypeId());
                   def.Column(h => h.Group, h => h.Group);
               })
                  .MapColumns<FieldDefinition>(def =>
                  {
                      def.Column(h => h.Title, h => h.Title);
                      def.Column(h => h.InternalName, h => h.InternalName);
                      def.Column(h => h.FieldType, h => h.FieldType);
                      def.Column(h => h.Group, h => h.Group);
                  })
                  .MapColumns<ContentTypeFieldLinkDefinition>(def =>
                  {
                      def.Column(h => h.FieldId, h => h.FieldId);
                      def.Column(h => h.FieldInternalName, h => h.FieldInternalName);
                  })
                  .MapColumns<FeatureDefinition>(def =>
                  {
                      def.Column(h => h.Title, h => h.Title);
                      def.Column(h => h.Scope, h => h.Scope);
                      def.Column(h => h.Id, h => h.Id);
                      def.Column(h => h.Enable, h => h.Enable);
                      def.Column(h => h.ForceActivate, h => h.ForceActivate);
                  })
                  .MapColumns<UserCustomActionDefinition>(def =>
                  {
                      def.Column(h => h.Title, h => h.Title);
                      def.Column(h => h.Name, h => h.Name);
                      def.Column(h => h.Description, h => h.Description);
                      def.Column(h => h.Location, h => h.Location);
                      def.Column(h => h.Sequence, h => h.Sequence);
                  })
                  .MapColumns<ListDefinition>(def =>
                  {
                      def.Column(h => h.Title, h => h.Title);
                      def.Column(h => h.TemplateType, h => h.TemplateType);
                      def.Column(h => h.ContentTypesEnabled, h => h.ContentTypesEnabled);
                      def.Column(h => h.Url, h => h.Url);
                      def.Column(h => h.CustomUrl, h => h.CustomUrl);
                  });
        }

        #endregion
    }
}
