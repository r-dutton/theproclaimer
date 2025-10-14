[web] GET /api/binder-types/{id:guid}/record-templates  (Workpapers.Next.API.Controllers.Workpapers.BinderTypesController.GetBinderRecordTemplates)  [L56–L92]
  └─ maps_to BinderRecordTemplateDto [L84]
  └─ calls BinderTypeRecordTemplateSetRepository.ReadQuery [L59]
  └─ queries BinderTypeRecordTemplateSet [L59]
    └─ reads_from BinderTypeRecordTemplateSets
  └─ uses_service IControlledRepository<BinderTypeRecordTemplateSet>
    └─ method ReadQuery [L59]

