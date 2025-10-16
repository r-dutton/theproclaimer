[web] GET /api/binder-types/{id:guid}/record-template-set  (Workpapers.Next.API.Controllers.Workpapers.BinderTypesController.GetBinderTypeRecordTemplates)  [L94–L107] status=200
  └─ calls BinderTypeRecordTemplateSetRepository.ReadQuery [L100]
  └─ query BinderTypeRecordTemplateSet [L100]
    └─ reads_from BinderTypeRecordTemplateSets
  └─ uses_service IControlledRepository<BinderTypeRecordTemplateSet>
    └─ method ReadQuery [L100]
      └─ ... (no implementation details available)

