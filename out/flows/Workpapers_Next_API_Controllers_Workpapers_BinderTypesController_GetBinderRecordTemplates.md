[web] GET /api/binder-types/{id:guid}/record-templates  (Workpapers.Next.API.Controllers.Workpapers.BinderTypesController.GetBinderRecordTemplates)  [L56–L92] status=200
  └─ maps_to BinderRecordTemplateDto [L84]
  └─ calls BinderTypeRecordTemplateSetRepository.ReadQuery [L59]
  └─ query BinderTypeRecordTemplateSet [L59]
    └─ reads_from BinderTypeRecordTemplateSets
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ BinderTypeRecordTemplateSet writes=0 reads=1
    └─ mappings 1
      └─ BinderRecordTemplateDto

