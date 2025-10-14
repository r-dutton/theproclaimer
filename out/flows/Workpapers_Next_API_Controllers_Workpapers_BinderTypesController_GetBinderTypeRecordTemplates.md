[web] GET /api/binder-types/{id:guid}/record-template-set  (Workpapers.Next.API.Controllers.Workpapers.BinderTypesController.GetBinderTypeRecordTemplates)  [L94–L107]
  └─ calls BinderTypeRecordTemplateSetRepository.ReadQuery [L100]
  └─ queries BinderTypeRecordTemplateSet [L100]
    └─ reads_from BinderTypeRecordTemplateSets
  └─ uses_service IControlledRepository<BinderTypeRecordTemplateSet>
    └─ method ReadQuery [L100]

