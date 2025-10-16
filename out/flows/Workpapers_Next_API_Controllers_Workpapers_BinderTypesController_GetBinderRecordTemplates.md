[web] GET /api/binder-types/{id:guid}/record-templates  (Workpapers.Next.API.Controllers.Workpapers.BinderTypesController.GetBinderRecordTemplates)  [L56–L92] status=200
  └─ maps_to BinderRecordTemplateDto [L84]
  └─ calls BinderTypeRecordTemplateSetRepository.ReadQuery [L59]
  └─ query BinderTypeRecordTemplateSet [L59]
    └─ reads_from BinderTypeRecordTemplateSets
  └─ uses_service IControlledRepository<BinderTypeRecordTemplateSet>
    └─ method ReadQuery [L59]
      └─ ... (no implementation details available)

